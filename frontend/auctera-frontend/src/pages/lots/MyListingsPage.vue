<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { RouterLink } from 'vue-router'
import { storeToRefs } from 'pinia'

import { useAuthStore } from '@/stores/authStore'
import { itemService } from '@/app/services/lotService'
import { auctionService } from '@/app/services/auctionService'
import type { LotPreview } from '@/types/lot'
import type { AuctionListItem, StartAuctionDuration } from '@/types/auction'

import ConfirmDeleteModal from '@/components/modals/ConfirmDeleteModal.vue'
import { toast } from "vue-sonner"

const isDeleteModalOpen = ref(false)
const lotToDeleteId = ref<string | null>(null)


const authStore = useAuthStore()
const { user, isAuthenticated } = storeToRefs(authStore)

const listings = ref<LotPreview[]>([])
const auctions = ref<AuctionListItem[]>([])
const selectedDurationByLot = ref<Record<string, StartAuctionDuration>>({})

const isLoading = ref(true)
const errorMessage = ref('')
const successMessage = ref('')

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

const canEdit = (normalizedStatus: string) => normalizedStatus === 'draft' || normalizedStatus === 'rejected'
const canDelete = (normalizedStatus: string) => ['draft', 'rejected', 'pending'].includes(normalizedStatus)
const canPublish = (normalizedStatus: string) => normalizedStatus === 'draft' || normalizedStatus === 'rejected'

const canStartAuction = (lot: LotPreview, normalizedStatus: string) => {
  const auction = getAuctionByLotId(lot.id)

  if (auction?.auctionId) {
    const auctionStatus = String(auction.status ?? '').toLowerCase()
    if (['active', 'sold', 'expired'].includes(auctionStatus)) {
      return false
    }
  }

  return ['draft', 'approved', 'active', 'rejected'].includes(normalizedStatus)
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
    console.error(error)
    errorMessage.value = 'Failed to load your listings.'
  } finally {
    isLoading.value = false
  }
}

const openDeleteModal = (lotId: string) => {
  lotToDeleteId.value = lotId
  isDeleteModalOpen.value = true
}

const closeDeleteModal = () => {
  lotToDeleteId.value = null
  isDeleteModalOpen.value = false
}

const confirmDeleteLot = async () => {
  if (!lotToDeleteId.value) return

  try {
    await itemService.deleteLot(lotToDeleteId.value)

    listings.value = listings.value.filter(
      (item) => item.id !== lotToDeleteId.value
    )

    successMessage.value = 'Listing deleted.'
    errorMessage.value = ''
    toast.success("Your listing has been succesfully deleted.", { position: "bottom-right" })

    closeDeleteModal()
  } catch (error: any) {
    errorMessage.value =
      error?.response?.data?.message ??
      'Delete failed. This lot may no longer be deletable.'

    successMessage.value = ''
    toast.error(errorMessage.value)
  }
}

const publishLot = async (lotId: string) => {
  try {
    await itemService.publishLot(lotId)
    successMessage.value = 'Listing submitted for review.'
    errorMessage.value = ''
    await loadData()
  } catch (error: any) {
    errorMessage.value = error?.response?.data?.message ?? 'Failed to submit listing.'
    successMessage.value = ''
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
    await loadData()
  } catch (error: any) {
    errorMessage.value = error?.response?.data?.message ?? 'Failed to start auction.'
    successMessage.value = ''
  }
}

const formatDate = (value?: string) => {
  if (!value) return '—'

  return new Intl.DateTimeFormat('en-GB', {
    day: '2-digit',
    month: 'short',
    year: 'numeric',
  }).format(new Date(value))
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
      <div v-if="isLoading" class="rounded-2xl border p-6 text-sm text-foreground/70">Loading...</div>

      <div v-else-if="myListings.length === 0" class="rounded-2xl border p-6 text-sm text-foreground/70">
        You do not have any listings yet.
      </div>

      <div v-else class="space-y-4">
        <div v-for="lot in myListings" :key="lot.id" class="rounded-2xl border p-4">
          <div class="grid gap-4 md:grid-cols-[100px_1fr_auto] md:items-center">
            <img :src="lot.media?.[0]?.url" :alt="lot.title" class="h-24 w-24 rounded-xl object-cover border" />

            <div>
              <div class="flex flex-wrap items-center gap-2">
                <h2 class="text-lg font-semibold">{{ lot.title }}</h2>
                <span :class="['rounded-full border px-2.5 py-1 text-xs', statusBadgeClass(normalizeStatus(lot))]">
                  {{ lot.statusName ?? normalizeStatus(lot) }}
                </span>
              </div>
              <p class="mt-1 text-sm text-foreground/70">{{ lot.brand }} · {{ lot.currency }} {{ lot.price }}</p>
              <p class="text-xs text-foreground/50">  Listed {{ formatDate(lot.createdAt) }}</p>
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
                @click="openDeleteModal(lot.id)"
              >
                Delete
              </button>

              <ConfirmDeleteModal
                v-if="isDeleteModalOpen"
                :title='`Delete "${lot.title}" ?`'
                message="Are you sure you want to delete this lot? This action cannot be undone."
                @cancel="closeDeleteModal"
                @confirm="confirmDeleteLot"
              />

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
