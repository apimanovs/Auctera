<script setup lang="ts">
import { computed, onMounted, reactive, ref } from 'vue'
import { useRoute } from 'vue-router'
import { storeToRefs } from 'pinia'

import { itemService } from '@/app/services/lotService'
import { useAuthStore } from '@/stores/authStore'
import {
  CATEGORY_OPTIONS,
  CONDITION_OPTIONS,
  GENDER_OPTIONS,
  SIZE_OPTIONS,
} from '@/features/lots/create-lot/options'
import type { Lot } from '@/types/lot'

const route = useRoute()
const authStore = useAuthStore()
const { user } = storeToRefs(authStore)

const lot = ref<Lot | null>(null)
const isLoading = ref(true)
const isSubmitting = ref(false)
const errorMessage = ref('')
const successMessage = ref('')

const form = reactive({
  title: '',
  description: '',
  amount: 0,
  currency: 'EUR',
  category: 0,
  gender: 1,
  size: 0,
  brand: '',
  condition: 0,
  color: '',
})

const normalizeStatus = computed(() => {
  const raw = String(lot.value?.statusName ?? lot.value?.status ?? '').toLowerCase()
  if (raw.includes('draft')) return 'draft'
  if (raw.includes('reject')) return 'rejected'
  if (raw.includes('pend')) return 'pending'
  if (raw.includes('active')) return 'active'
  if (raw.includes('approve') || raw.includes('publish')) return 'approved'
  if (raw.includes('sold')) return 'sold'
  if (raw.includes('expir')) return 'expired'
  return 'unknown'
})

const canEdit = computed(() => ['draft', 'rejected'].includes(normalizeStatus.value))

const loadLot = async () => {
  try {
    isLoading.value = true
    const lotId = String(route.params.id)
    const data = await itemService.getLot(lotId)

    lot.value = data

    form.title = data.title
    form.description = data.description
    form.amount = data.price
    form.currency = data.currency
    form.category = data.category
    form.gender = data.gender
    form.size = data.size
    form.brand = data.brand
    form.condition = data.condition
    form.color = data.color
  } catch (error) {
    console.error(error)
    errorMessage.value = 'Failed to load listing for editing.'
  } finally {
    isLoading.value = false
  }
}

const submit = async () => {
  if (!lot.value) return

  errorMessage.value = ''
  successMessage.value = ''

  if (!canEdit.value) {
    errorMessage.value = 'This listing cannot be edited in its current status.'
    return
  }

  if (!user.value?.id) {
    errorMessage.value = 'You need to sign in before editing.'
    return
  }

  if (!form.title.trim() || !form.description.trim() || form.amount <= 0 || !form.brand.trim() || !form.color.trim()) {
    errorMessage.value = 'Please complete all required fields with valid values.'
    return
  }

  try {
    isSubmitting.value = true

    await itemService.updateLot(lot.value.id, {
      id: lot.value.id,
      sellerId: user.value.id,
      title: form.title.trim(),
      description: form.description.trim(),
      price: {
        amount: form.amount,
        currency: form.currency,
      },
      category: form.category,
      gender: form.gender,
      size: form.size,
      brand: form.brand.trim(),
      condition: form.condition,
      color: form.color.trim(),
    })

    successMessage.value = 'Listing updated successfully.'
  } catch (error: any) {
    errorMessage.value = error?.response?.data?.message ?? 'Failed to update listing.'
  } finally {
    isSubmitting.value = false
  }
}

onMounted(loadLot)
</script>

<template>
  <div class="mx-auto max-w-4xl px-4 py-8 sm:px-6 lg:px-8">
    <h1 class="text-3xl font-semibold">Edit Listing</h1>

    <div v-if="isLoading" class="mt-4 rounded-2xl border p-6 text-sm text-foreground/70">Loading...</div>

    <div v-else class="mt-4 space-y-4">
      <div v-if="errorMessage" class="rounded-xl border border-red-500/30 bg-red-500/10 p-3 text-sm text-red-300">{{ errorMessage }}</div>
      <div v-if="successMessage" class="rounded-xl border border-emerald-500/30 bg-emerald-500/10 p-3 text-sm text-emerald-300">{{ successMessage }}</div>

      <div v-if="!canEdit" class="rounded-xl border p-4 text-sm text-foreground/70">
        This listing cannot be edited because it is currently {{ lot?.statusName ?? normalizeStatus }}.
      </div>

      <form class="space-y-3 rounded-2xl border p-4" @submit.prevent="submit">
        <input v-model="form.title" :disabled="!canEdit || isSubmitting" class="h-11 w-full rounded-lg border bg-background px-3" placeholder="Title" />
        <textarea v-model="form.description" :disabled="!canEdit || isSubmitting" class="min-h-[120px] w-full rounded-lg border bg-background p-3" placeholder="Description" />

        <div class="grid gap-3 md:grid-cols-2">
          <input v-model.number="form.amount" type="number" min="0" step="0.01" :disabled="!canEdit || isSubmitting" class="h-11 rounded-lg border bg-background px-3" placeholder="Amount" />
          <input v-model="form.currency" :disabled="!canEdit || isSubmitting" class="h-11 rounded-lg border bg-background px-3" placeholder="Currency" />
          <input v-model="form.brand" :disabled="!canEdit || isSubmitting" class="h-11 rounded-lg border bg-background px-3" placeholder="Brand" />
          <input v-model="form.color" :disabled="!canEdit || isSubmitting" class="h-11 rounded-lg border bg-background px-3" placeholder="Color" />

          <select v-model.number="form.category" :disabled="!canEdit || isSubmitting" class="h-11 rounded-lg border bg-background px-3">
            <option v-for="option in CATEGORY_OPTIONS" :key="option.value" :value="option.value">{{ option.label }}</option>
          </select>

          <select v-model.number="form.gender" :disabled="!canEdit || isSubmitting" class="h-11 rounded-lg border bg-background px-3">
            <option v-for="option in GENDER_OPTIONS" :key="option.value" :value="option.value">{{ option.label }}</option>
          </select>

          <select v-model.number="form.size" :disabled="!canEdit || isSubmitting" class="h-11 rounded-lg border bg-background px-3">
            <option v-for="option in SIZE_OPTIONS" :key="option.value" :value="option.value">{{ option.label }}</option>
          </select>

          <select v-model.number="form.condition" :disabled="!canEdit || isSubmitting" class="h-11 rounded-lg border bg-background px-3">
            <option v-for="option in CONDITION_OPTIONS" :key="option.value" :value="option.value">{{ option.label }}</option>
          </select>
        </div>

        <button
          type="submit"
          class="rounded-lg bg-foreground px-4 py-2 text-sm text-background disabled:cursor-not-allowed disabled:opacity-50"
          :disabled="!canEdit || isSubmitting"
        >
          {{ isSubmitting ? 'Saving...' : 'Save changes' }}
        </button>
      </form>
    </div>
  </div>
</template>
