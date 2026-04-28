<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { storeToRefs } from 'pinia'

import { getApiErrorMessage } from '@/app/helpers/apiError'
import { auctionService } from '@/app/services/auctionService'
import { bidService, type Bid } from '@/app/services/bidService'
import { itemService } from '@/app/services/lotService'
import { useAuthStore } from '@/stores/authStore'
import { toast } from '@/components/ui/toast'
import type { AuctionDetails } from '@/types/auction'
import type { Lot } from '@/types/lot'

const route = useRoute()
const authStore = useAuthStore()
const { user, isAuthenticated } = storeToRefs(authStore)

const lot = ref<Lot | null>(null)
const auction = ref<AuctionDetails | null>(null)
const bids = ref<Bid[]>([])

const isLoading = ref(true)
const isBidLoading = ref(false)
const isBidSubmitting = ref(false)
const errorMessage = ref('')
const bidError = ref('')
const bidAmount = ref<number | null>(null)

const lotIdParam = computed(() => String(route.params.lotId ?? route.params.id ?? ''))
const isOwner = computed(() => !!lot.value?.seller?.id && lot.value.seller.id === user.value?.id)

const normalizedLotStatus = computed(() => {
  const raw = String(lot.value?.statusName ?? lot.value?.status ?? '').toLowerCase()
  if (raw.includes('draft')) return 'draft'
  if (raw.includes('pend')) return 'pending'
  if (raw.includes('reject')) return 'rejected'
  if (raw.includes('sold')) return 'sold'
  if (raw.includes('expir')) return 'expired'
  if (raw.includes('active') || raw.includes('listed')) return 'active'
  if (raw.includes('publish') || raw.includes('approve')) return 'approved'
  return 'unknown'
})

const isPublicLot = computed(() => ['approved', 'active'].includes(normalizedLotStatus.value))
const canSeeLot = computed(() => isPublicLot.value || isOwner.value)
const isAuctionActive = computed(() => auction.value?.status?.toLowerCase() === 'active')
const canBid = computed(() => isAuthenticated.value && !isOwner.value && !!auction.value?.auctionId && isAuctionActive.value)

const sellerLocation = computed(() =>
  [lot.value?.seller?.city?.trim(), lot.value?.seller?.country?.trim()].filter(Boolean).join(', '),
)

const sortedBids = computed(() =>
  [...bids.value].sort((a, b) => new Date(b.placedAt).getTime() - new Date(a.placedAt).getTime()),
)

const formatMoney = (amount: number | null | undefined, currency: string | null | undefined) => {
  if (amount === null || amount === undefined) return '-'
  return new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: currency ?? 'EUR',
    maximumFractionDigits: 2,
  }).format(amount)
}

const loadAuctionAndBids = async () => {
  if (!lot.value?.id) return

  isBidLoading.value = true

  try {
    const linkedAuction = await auctionService.getAuctionByLotId(lot.value.id)

    if (!linkedAuction?.auctionId) {
      auction.value = null
      bids.value = []
      return
    }

    auction.value = linkedAuction
    bids.value = await bidService.getBidsByAuction(linkedAuction.auctionId)
  } finally {
    isBidLoading.value = false
  }
}

const loadLot = async () => {
  try {
    isLoading.value = true
    errorMessage.value = ''

    lot.value = await itemService.getLot(lotIdParam.value)

    if (!canSeeLot.value) {
      return
    }

    await loadAuctionAndBids()
  } catch (error) {
    errorMessage.value = getApiErrorMessage(error, 'Failed to load listing.')
  } finally {
    isLoading.value = false
  }
}

const submitBid = async () => {
  bidError.value = ''

  if (!canBid.value || !auction.value?.auctionId || !lot.value) {
    bidError.value = isOwner.value ? 'You cannot bid on your own listing.' : 'Bidding is currently unavailable.'
    toast({ title: 'Failed to place bid', description: bidError.value, variant: 'error' })
    return
  }

  if (bidAmount.value === null || Number.isNaN(bidAmount.value) || bidAmount.value <= 0) {
    bidError.value = 'Please enter a valid bid amount.'
    toast({ title: 'Failed to place bid', description: bidError.value, variant: 'error' })
    return
  }

  if (auction.value.minimumBid !== null && bidAmount.value < auction.value.minimumBid) {
    bidError.value = `Minimum next bid is ${formatMoney(auction.value.minimumBid, auction.value.currency ?? lot.value.currency)}.`
    toast({ title: 'Failed to place bid', description: bidError.value, variant: 'error' })
    return
  }

  try {
    isBidSubmitting.value = true

    await bidService.placeBid(auction.value.auctionId, {
      amount: bidAmount.value,
      currency: lot.value.currency,
    })

    toast({ title: 'Bid placed successfully', variant: 'success' })
    bidAmount.value = null

    await loadAuctionAndBids()
  } catch (error) {
    bidError.value = getApiErrorMessage(error, 'Failed to place bid.')
    toast({ title: 'Failed to place bid', description: bidError.value, variant: 'error' })
  } finally {
    isBidSubmitting.value = false
  }
}

onMounted(loadLot)
</script>

<template>
  <div class="mx-auto max-w-7xl px-4 py-8 sm:px-6 lg:px-8">
    <div v-if="isLoading" class="rounded-[24px] border bg-background p-6 text-sm text-foreground/70">
      Loading listing...
    </div>

    <div v-else-if="errorMessage" class="rounded-[24px] border border-red-500/30 bg-red-500/10 p-6 text-sm text-red-300">
      {{ errorMessage }}
    </div>

    <div v-else-if="lot && !canSeeLot" class="rounded-[24px] border bg-background p-6 text-sm text-foreground/70">
      This listing is not available.
    </div>

    <div v-else-if="lot" class="grid gap-8 lg:grid-cols-[1.2fr_0.8fr]">
      <section>
        <div class="flex h-[420px] overflow-hidden rounded-[28px] border bg-background">
          <img
            v-if="lot.media?.[0]?.url"
            :src="lot.media[0].url"
            :alt="lot.title"
            class="h-full w-full object-cover"
          />
          <div v-else class="flex flex-1 items-center justify-center text-sm text-foreground/60">
            No image
          </div>
        </div>

        <div v-if="lot.media?.length" class="mt-4 grid grid-cols-2 gap-3 md:grid-cols-4">
          <div v-for="(image, index) in lot.media" :key="image.key || index" class="overflow-hidden rounded-2xl border bg-foreground/5">
            <img v-if="image.url" :src="image.url" :alt="`${lot.title} ${index + 1}`" class="h-24 w-full object-cover" />
          </div>
        </div>
      </section>

      <section class="space-y-4">
        <div class="rounded-[28px] border bg-background p-6">
          <p class="text-xs uppercase tracking-[0.2em] text-foreground/50">{{ lot.brand }}</p>
          <h1 class="mt-2 text-3xl font-semibold">{{ lot.title }}</h1>
          <p class="mt-4 text-sm leading-6 text-foreground/70">{{ lot.description }}</p>

          <div class="mt-4 flex flex-wrap gap-2 text-xs text-foreground/65">
            <span v-if="lot.year" class="rounded-full border px-3 py-1">Year {{ lot.year }}</span>
            <span v-if="sellerLocation" class="rounded-full border px-3 py-1">Ships from {{ sellerLocation }}</span>
            <span v-else class="rounded-full border px-3 py-1">Location not specified</span>
          </div>

          <div class="mt-5 grid grid-cols-2 gap-3 text-sm">
            <div class="rounded-xl border p-3">
              <p class="text-foreground/50">Current price</p>
              <p class="mt-1 font-semibold">{{ formatMoney(auction?.currentPrice ?? lot.price, auction?.currency ?? lot.currency) }}</p>
            </div>
            <div class="rounded-xl border p-3">
              <p class="text-foreground/50">Auction status</p>
              <p class="mt-1 font-semibold">{{ auction?.status ?? 'Not started' }}</p>
            </div>
            <div class="rounded-xl border p-3">
              <p class="text-foreground/50">Minimum next bid</p>
              <p class="mt-1 font-semibold">{{ formatMoney(auction?.minimumBid ?? null, auction?.currency ?? lot.currency) }}</p>
            </div>
            <div class="rounded-xl border p-3">
              <p class="text-foreground/50">Bids</p>
              <p class="mt-1 font-semibold">{{ bids.length }}</p>
            </div>
            <div class="rounded-xl border p-3">
              <p class="text-foreground/50">Starts</p>
              <p class="mt-1 font-semibold">{{ auction?.startsAt ? new Date(auction.startsAt).toLocaleString() : '-' }}</p>
            </div>
            <div class="rounded-xl border p-3">
              <p class="text-foreground/50">Ends</p>
              <p class="mt-1 font-semibold">{{ auction?.endsAt ? new Date(auction.endsAt).toLocaleString() : '-' }}</p>
            </div>
          </div>
        </div>

        <div class="rounded-[28px] border bg-background p-6">
          <h2 class="text-lg font-semibold">Place a bid</h2>

          <div v-if="bidError" class="mt-3 rounded-xl border border-red-500/30 bg-red-500/10 p-3 text-sm text-red-300">
            {{ bidError }}
          </div>

          <div class="mt-4 flex gap-3">
            <input
              v-model.number="bidAmount"
              type="number"
              step="0.01"
              :min="auction?.minimumBid ?? 0"
              class="h-11 w-full rounded-xl border bg-background px-3"
              :placeholder="auction?.minimumBid ? `Min. ${formatMoney(auction.minimumBid, auction.currency ?? lot.currency)}` : 'Enter bid amount'"
              :disabled="!canBid || isBidSubmitting"
            />
            <button
              class="h-11 rounded-xl bg-foreground px-4 text-sm font-medium text-background disabled:cursor-not-allowed disabled:opacity-50"
              :disabled="!canBid || isBidSubmitting || !bidAmount || bidAmount <= 0"
              @click="submitBid"
            >
              {{ isBidSubmitting ? 'Placing...' : 'Place bid' }}
            </button>
          </div>

          <p v-if="!isAuthenticated" class="mt-2 text-xs text-foreground/60">Sign in to place a bid.</p>
          <p v-else-if="isOwner" class="mt-2 text-xs text-foreground/60">You cannot bid on your own listing.</p>
          <p v-else-if="auction && !isAuctionActive" class="mt-2 text-xs text-foreground/60">Bidding is enabled only when auction is active.</p>
        </div>

        <div class="rounded-[28px] border bg-background p-6">
          <h2 class="text-lg font-semibold">Bid history</h2>
          <p v-if="isBidLoading" class="mt-2 text-sm text-foreground/60">Loading bids...</p>
          <p v-else-if="sortedBids.length === 0" class="mt-2 text-sm text-foreground/60">No bids yet.</p>
          <ul v-else class="mt-3 space-y-2 text-sm">
            <li v-for="bid in sortedBids" :key="bid.bidId" class="flex items-center justify-between rounded-xl border p-3">
              <span>{{ formatMoney(bid.amount, bid.currency) }}</span>
              <span class="text-foreground/60">{{ new Date(bid.placedAt).toLocaleString() }}</span>
            </li>
          </ul>
        </div>
      </section>
    </div>
  </div>
</template>
