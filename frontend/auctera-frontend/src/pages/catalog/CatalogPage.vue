<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { RouterLink } from 'vue-router'

import AuctionCard from '@/components/auctions/AuctionCard.vue'
import { itemService } from '@/app/services/lotService'
import type { LotPreview } from '@/types/lot'

const lots = ref<LotPreview[]>([])
const isLoading = ref(true)
const errorMessage = ref('')

const normalizedStatus = (lot: LotPreview) => String(lot.statusName ?? lot.status ?? '').toLowerCase()

const visibleLots = computed(() =>
  lots.value.filter((lot) => {
    const status = normalizedStatus(lot)
    return status.includes('active') || status.includes('approve') || status.includes('publish')
  }),
)

const loadLots = async () => {
  try {
    isLoading.value = true
    errorMessage.value = ''
    lots.value = await itemService.getLots()
  } catch (error) {
    console.error(error)
    errorMessage.value = 'Failed to load catalog.'
  } finally {
    isLoading.value = false
  }
}

onMounted(loadLots)
</script>

<template>
  <div class="mx-auto max-w-7xl px-4 py-8 sm:px-6 lg:px-8">
    <div class="mb-6">
      <h1 class="text-3xl font-semibold">Catalog</h1>
      <p class="mt-2 text-sm text-foreground/70">Discover active and approved listings.</p>
    </div>

    <div v-if="isLoading" class="rounded-2xl border p-6 text-sm text-foreground/70">Loading...</div>
    <div v-else-if="errorMessage" class="rounded-2xl border border-red-500/30 bg-red-500/10 p-6 text-sm text-red-300">{{ errorMessage }}</div>
    <div v-else-if="visibleLots.length === 0" class="rounded-2xl border p-6 text-sm text-foreground/70">No public listings available.</div>

    <div v-else class="grid gap-5 sm:grid-cols-2 xl:grid-cols-4">
      <RouterLink
        v-for="lot in visibleLots"
        :key="lot.id"
        :to="`/lots/${lot.id}`"
        class="group overflow-hidden"
      >
        <AuctionCard
          :brand="lot.brand"
          :title="lot.title"
          :price="lot.price"
          :image-url="lot.media?.[0]?.url ?? []"
          :time-left="lot.statusName ?? 'Live'"
        />
      </RouterLink>
    </div>
  </div>
</template>
