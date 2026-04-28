<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { RouterLink } from 'vue-router'
import { storeToRefs } from 'pinia'

import { useAuthStore } from '@/stores/authStore'
import { getApiErrorMessage } from '@/app/helpers/apiError'
import { itemService } from '@/app/services/lotService'
import { auctionService } from '@/app/services/auctionService'
import { toast } from '@/components/ui/toast'
import type { LotPreview } from '@/types/lot'
import type { AuctionListItem, StartAuctionDuration } from '@/types/auction'

const authStore = useAuthStore()
const { isAuthenticated } = storeToRefs(authStore)

const listings = ref<LotPreview[]>([])
const auctions = ref<AuctionListItem[]>([])
const selectedDurationByLot = ref<Record<string, StartAuctionDuration>>({})

const isLoading = ref(true)
const errorMessage = ref('')
const successMessage = ref('')
const confirmingDeleteLotId = ref<string | null>(null)

const normalizeStatus = (lot: LotPreview) => {
  const raw = String(lot.statusName ?? lot.status ?? '').toLowerCase()

  if (raw.includes('draft')) return 'draft'
  if (raw.includes('pend')) return 'pending'
  if (raw.includes('reject')) return 'rejected'
  if (raw.includes('active')) return 'active'
  if (raw.includes('approve') || raw.includes('publish')) return 'approved'
  if (raw.includes('sold')) return 'sold'
  if (raw.includes('expir')) return 'expired'

  return 'unknown'
}

const statusBadgeClass = (normalizedStatus: string) => {
  if (normalizedStatus === 'draft') return 'bg-gray-100 text-gray-700 border-gray-300'
  if (normalizedStatus === 'pending') return 'bg-amber-100 text-amber-800 border-amber-300'
  if (normalizedStatus === 'approved' || normalizedStatus === 'active') return 'bg-emerald-100 text-emerald-800 border-emerald-300'
  if (normalizedStatus === 'rejected') return 'bg-red-100 text-red-700 border-red-300'
  return 'bg-zinc-100 text-zinc-700 border-zinc-300'
}

const myListings = computed(() => listings.value)

const getAuctionByLotId = (lotId: string) => auctions.value.find((auction) => auction.lotId === lotId)

const canEdit = (normalizedStatus: string) => normalizedStatus === 'draft'
const canDelete = (normalizedStatus: string) => normalizedStatus === 'draft'
const canPublish = (normalizedStatus: string) => normalizedStatus === 'draft'

const canStartAuction = (lot: LotPreview, normalizedStatus: string) => {
  const auction = getAuctionByLotId(lot.id)

  if (auction?.auctionId) {
    const auctionStatus = String(auction.status ?? '').toLowerCase()
    if (['active', 'sold', 'expired'].includes(auctionStatus)) {
      return false
    }
  }

  return normalizedStatus === 'approved'
}

const loadData = async () => {
  try {
    isLoading.value = true
    errorMessage.value = ''

    const [lotsResponse, auctionsResponse] = await Promise.all([
      itemService.getMyLots(),
      auctionService.getAuctions().catch(() => []),
    ])

    listings.value = lotsResponse
    auctions.value = auctionsResponse

    for (const lot of listings.value) {
      selectedDurationByLot.value[lot.id] = selectedDurationByLot.value[lot.id] ?? '7'
    }
  } catch (error) {
    errorMessage.value = getApiErrorMessage(error, 'Failed to load your listings.')
  } finally {
    isLoading.value = false
  }
}

const deleteLot = async (lot: LotPreview) => {
  if (confirmingDeleteLotId.value !== lot.id) {
    confirmingDeleteLotId.value = lot.id
    return
  }

  try {
    await itemService.deleteLot(lot.id)
    listings.value = listings.value.filter((item) => item.id !== lot.id)
    confirmingDeleteLotId.value = null
    successMessage.value = 'Lot deleted successfully'
    errorMessage.value = ''
    toast({ title: 'Lot deleted successfully', variant: 'success' })
  } catch (error) {
    confirmingDeleteLotId.value = null
    errorMessage.value = getApiErrorMessage(error, 'Failed to delete lot')
    successMessage.value = ''
    toast({ title: 'Failed to delete lot', description: errorMessage.value, variant: 'error' })
  }
}

const publishLot = async (lotId: string) => {
  try {
    await itemService.publishLot(lotId)
    successMessage.value = 'Listing submitted for review.'
    errorMessage.value = ''
    toast({ title: 'Listing submitted for review', variant: 'success' })
    await loadData()
  } catch (error) {
    errorMessage.value = getApiErrorMessage(error, 'Failed to submit listing.')
    successMessage.value = ''
    toast({ title: 'Failed to submit listing', description: errorMessage.value, variant: 'error' })
  }
}

const startAuctionForLot = async (lot: LotPreview) => {
  try {
    let targetAuctionId = getAuctionByLotId(lot.id)?.auctionId

    if (!targetAuctionId) {
      targetAuctionId = await auctionService.createAuction({ lotId: lot.id })
    }

    const duration = selectedDurationByLot.value[lot.id] ?? '7'
    await auctionService.startAuction(targetAuctionId, duration)

    successMessage.value = 'Auction started successfully.'
    errorMessage.value = ''
    toast({ title: 'Auction started successfully', variant: 'success' })
    await loadData()
  } catch (error) {
    errorMessage.value = getApiErrorMessage(error, 'Failed to start auction.')
    successMessage.value = ''
    toast({ title: 'Failed to start auction', description: errorMessage.value, variant: 'error' })
  }
}

onMounted(async () => {
  if (!isAuthenticated.value) {
    await authStore.checkAuth()
  }

  if (!isAuthenticated.value) {
    isLoading.value = false
    return
  }

  await loadData()
})
</script>

<template>
  <div class="mx-auto max-w-7xl px-4 py-8 sm:px-6 lg:px-8">
    <div class="mb-6">
      <h1 class="text-3xl font-semibold">My Listings</h1>
      <p class="mt-2 text-sm text-foreground/70">Manage your listings and auction actions.</p>
    </div>

    <div v-if="!isAuthenticated" class="rounded-2xl border p-6 text-sm text-foreground/70">
      Please sign in to view your listings.
    </div>

    <div v-else>
      <div v-if="successMessage" class="mb-4 rounded-xl border border-emerald-500/30 bg-emerald-500/10 p-3 text-sm text-emerald-300">{{ successMessage }}</div>
      <div v-if="errorMessage" class="mb-4 rounded-xl border border-red-500/30 bg-red-500/10 p-3 text-sm text-red-300">{{ errorMessage }}</div>

      <div v-if="isLoading" class="rounded-2xl border p-6 text-sm text-foreground/70">Loading...</div>

      <div v-else-if="myListings.length === 0" class="rounded-2xl border p-6 text-sm text-foreground/70">
        You do not have any listings yet.
      </div>

      <div v-else class="space-y-4">
        <div v-for="lot in myListings" :key="lot.id" class="rounded-2xl border p-4">
          <div class="grid gap-4 md:grid-cols-[100px_1fr_auto] md:items-center">
            <div class="flex h-24 w-24 overflow-hidden rounded-xl border bg-foreground/5">
              <img v-if="lot.media?.[0]?.url" :src="lot.media[0].url" :alt="lot.title" class="h-full w-full object-cover" />
              <span v-else class="m-auto text-xs text-foreground/50">No image</span>
            </div>

            <div>
              <div class="flex flex-wrap items-center gap-2">
                <h2 class="text-lg font-semibold">{{ lot.title }}</h2>
                <span :class="['rounded-full border px-2.5 py-1 text-xs', statusBadgeClass(normalizeStatus(lot))]">
                  {{ lot.statusName ?? normalizeStatus(lot) }}
                </span>
              </div>
              <p class="mt-1 text-sm text-foreground/70">{{ lot.brand }} / {{ lot.currency }} {{ lot.price }}</p>
              <p class="text-xs text-foreground/50">{{ lot.year ? `Year: ${lot.year}` : 'Year not specified' }}</p>
            </div>

            <div class="flex flex-wrap gap-2">
              <RouterLink
                :to="`/lots/${lot.id}/edit`"
                class="rounded-lg border px-3 py-2 text-sm"
                :class="canEdit(normalizeStatus(lot)) ? 'hover:bg-foreground hover:text-background' : 'cursor-not-allowed opacity-50'"
                :title="canEdit(normalizeStatus(lot)) ? 'Edit listing' : 'Approved lots cannot be edited'"
                :aria-disabled="!canEdit(normalizeStatus(lot))"
                @click.prevent="!canEdit(normalizeStatus(lot))"
              >
                Edit
              </RouterLink>

              <button
                class="rounded-lg border px-3 py-2 text-sm disabled:cursor-not-allowed disabled:opacity-50"
                :disabled="!canDelete(normalizeStatus(lot))"
                @click="deleteLot(lot)"
              >
                {{ confirmingDeleteLotId === lot.id ? 'Confirm delete' : 'Delete' }}
              </button>

              <button
                class="rounded-lg border px-3 py-2 text-sm disabled:cursor-not-allowed disabled:opacity-50"
                :disabled="!canPublish(normalizeStatus(lot))"
                @click="publishLot(lot.id)"
              >
                Submit for review
              </button>

              <select
                v-model="selectedDurationByLot[lot.id]"
                class="rounded-lg border bg-background px-2 py-2 text-sm"
                :disabled="!canStartAuction(lot, normalizeStatus(lot))"
              >
                <option value="3">3 days</option>
                <option value="5">5 days</option>
                <option value="7">7 days</option>
              </select>

              <button
                class="rounded-lg bg-foreground px-3 py-2 text-sm text-background disabled:cursor-not-allowed disabled:opacity-50"
                :disabled="!canStartAuction(lot, normalizeStatus(lot))"
                @click="startAuctionForLot(lot)"
              >
                Start Auction
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
