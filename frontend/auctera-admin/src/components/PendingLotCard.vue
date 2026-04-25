<script setup lang="ts">
import type { LotPreview } from '@/types/lot'

defineProps<{
  lot: LotPreview
  isActing: boolean
}>()

const emit = defineEmits<{
  approve: [lotId: string]
  reject: [lotId: string]
}>()

const imageUrl = (lot: LotPreview) => lot.media?.[0]?.url ?? ''
const createdDate = (lot: LotPreview) => {
  const value = lot.createdAt ?? lot.createdDate
  if (!value) return '—'
  return new Date(value).toLocaleString()
}

const priceLabel = (lot: LotPreview) => {
  const amount = lot.price ?? lot.amount ?? 0
  return `${lot.currency ?? 'EUR'} ${amount}`
}
</script>

<template>
  <article class="rounded-xl border bg-white p-4">
    <div class="grid gap-4 sm:grid-cols-[120px_1fr_auto] sm:items-center">
      <img :src="imageUrl(lot)" :alt="lot.title ?? 'Lot image'" class="h-24 w-24 rounded-lg border object-cover" />

      <div>
        <h2 class="text-lg font-semibold">{{ lot.title ?? 'Untitled lot' }}</h2>
        <p class="text-sm text-slate-600">{{ lot.brand ?? 'Unknown brand' }}</p>
        <p class="text-sm text-slate-700">{{ priceLabel(lot) }}</p>
        <p class="text-xs text-slate-500">Seller: {{ lot.sellerId ?? '—' }}</p>
        <p class="text-xs text-slate-500">Created: {{ createdDate(lot) }}</p>
      </div>

      <div class="flex gap-2">
        <button
          type="button"
          class="rounded-md bg-emerald-600 px-3 py-2 text-sm font-medium text-white disabled:cursor-not-allowed disabled:opacity-50"
          :disabled="isActing"
          @click="emit('approve', lot.id)"
        >
          Approve
        </button>

        <button
          type="button"
          class="rounded-md bg-red-600 px-3 py-2 text-sm font-medium text-white disabled:cursor-not-allowed disabled:opacity-50"
          :disabled="isActing"
          @click="emit('reject', lot.id)"
        >
          Reject
        </button>
      </div>
    </div>
  </article>
</template>
