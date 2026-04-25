<script setup lang="ts">
import { onMounted, ref } from 'vue'

import PendingLotCard from '@/components/PendingLotCard.vue'
import { adminLotService } from '@/services/lotService'
import type { LotPreview } from '@/types/lot'

const isLoading = ref(true)
const isActing = ref(false)
const errorMessage = ref('')
const lots = ref<LotPreview[]>([])

const loadPendingLots = async () => {
  try {
    isLoading.value = true
    errorMessage.value = ''
    lots.value = await adminLotService.getPendingLots()
  } catch (error) {
    console.error(error)
    errorMessage.value = 'Failed to load pending lots.'
  } finally {
    isLoading.value = false
  }
}

const approve = async (lotId: string) => {
  try {
    isActing.value = true
    await adminLotService.approveLot(lotId)
  } finally {
    isActing.value = false
  }
}

const reject = async (lotId: string) => {
  try {
    isActing.value = true
    await adminLotService.rejectLot(lotId)
  } finally {
    isActing.value = false
  }
}

onMounted(loadPendingLots)
</script>

<template>
  <section class="mx-auto max-w-6xl px-4 py-6 sm:px-6 lg:px-8">
    <div class="mb-4">
      <h2 class="text-2xl font-semibold">Pending Lots</h2>
      <p class="text-sm text-slate-600">Review lots awaiting moderation.</p>
    </div>

    <div v-if="isLoading" class="rounded-xl border bg-white p-4 text-sm text-slate-600">
      Loading pending lots...
    </div>

    <div v-else-if="errorMessage" class="rounded-xl border border-red-300 bg-red-50 p-4 text-sm text-red-700">
      {{ errorMessage }}
    </div>

    <div v-else-if="lots.length === 0" class="rounded-xl border bg-white p-4 text-sm text-slate-600">
      No pending lots found.
    </div>

    <div v-else class="space-y-3">
      <PendingLotCard
        v-for="lot in lots"
        :key="lot.id"
        :lot="lot"
        :is-acting="isActing"
        @approve="approve"
        @reject="reject"
      />
    </div>
  </section>
</template>
