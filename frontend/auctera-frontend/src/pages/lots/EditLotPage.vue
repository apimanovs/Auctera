<script setup lang="ts">
import { computed, onMounted, reactive, ref } from 'vue'
import { RouterLink, useRoute } from 'vue-router'
import { storeToRefs } from 'pinia'

import { getApiErrorMessage } from '@/app/helpers/apiError'
import { itemService } from '@/app/services/lotService'
import { useAuthStore } from '@/stores/authStore'
import {
  CATEGORY_OPTIONS,
  CONDITION_OPTIONS,
  GENDER_OPTIONS,
  SIZE_OPTIONS,
} from '@/features/lots/create-lot/options'
import { Button } from '@/components/ui/button'
import { toast } from '@/components/ui/toast'
import type { Lot } from '@/types/lot'

const route = useRoute()
const authStore = useAuthStore()
const { user } = storeToRefs(authStore)

const lot = ref<Lot | null>(null)
const isLoading = ref(true)
const isSubmitting = ref(false)
const errorMessage = ref('')

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
  year: null as number | null,
})

const normalizedStatus = computed(() => {
  const raw = String(lot.value?.statusName ?? lot.value?.status ?? '').toLowerCase()
  if (raw.includes('draft')) return 'draft'
  if (raw.includes('reject')) return 'rejected'
  if (raw.includes('pend')) return 'pending'
  if (raw.includes('active')) return 'active'
  if (raw.includes('approve') || raw.includes('publish')) return 'published'
  if (raw.includes('sold')) return 'sold'
  if (raw.includes('expir')) return 'expired'
  return 'unknown'
})

const canEdit = computed(() => normalizedStatus.value === 'draft')
const coverImage = computed(() => lot.value?.media?.find((media) => media.url)?.url ?? '')

const validateForm = () => {
  if (!form.title.trim() || form.title.trim().length < 5) {
    return 'Title must be at least 5 characters.'
  }

  if (!form.description.trim() || form.description.trim().length < 20) {
    return 'Description must be at least 20 characters.'
  }

  if (form.amount <= 0) {
    return 'Price must be greater than 0.'
  }

  if (!form.brand.trim() || !form.color.trim()) {
    return 'Brand and color are required.'
  }

  if (form.year !== null) {
    const nextYear = new Date().getFullYear()

    if (form.year < 1900 || form.year > nextYear) {
      return `Year must be between 1900 and ${nextYear}.`
    }
  }

  return ''
}

const loadLot = async () => {
  try {
    isLoading.value = true
    errorMessage.value = ''
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
    form.color = data.color ?? ''
    form.year = data.year ?? null
  } catch (error) {
    errorMessage.value = getApiErrorMessage(error, 'Failed to load listing for editing.')
  } finally {
    isLoading.value = false
  }
}

const submit = async () => {
  if (!lot.value) return

  errorMessage.value = ''

  if (!canEdit.value) {
    errorMessage.value = 'Only draft listings can be edited.'
    toast({ title: 'Failed to update lot', description: errorMessage.value, variant: 'error' })
    return
  }

  if (!user.value?.id) {
    errorMessage.value = 'You need to sign in before editing.'
    toast({ title: 'Failed to update lot', description: errorMessage.value, variant: 'error' })
    return
  }

  const validationError = validateForm()

  if (validationError) {
    errorMessage.value = validationError
    toast({ title: 'Failed to update lot', description: validationError, variant: 'error' })
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
      year: form.year,
    })

    toast({ title: 'Lot updated successfully', variant: 'success' })
    await loadLot()
  } catch (error) {
    const message = getApiErrorMessage(error, 'Failed to update lot.')
    errorMessage.value = message
    toast({ title: 'Failed to update lot', description: message, variant: 'error' })
  } finally {
    isSubmitting.value = false
  }
}

onMounted(loadLot)
</script>

<template>
  <div class="mx-auto max-w-6xl px-4 py-8 sm:px-6 lg:px-8">
    <div class="mb-8 flex flex-wrap items-start justify-between gap-4">
      <div>
        <p class="text-xs uppercase tracking-[0.22em] text-foreground/50">
          Seller tools
        </p>
        <h1 class="mt-2 text-3xl font-semibold tracking-tight">
          Edit listing
        </h1>
        <p class="mt-3 max-w-2xl text-sm leading-6 text-foreground/70">
          Update draft listing details before sending the lot to review.
        </p>
      </div>

      <RouterLink
        to="/my-listings"
        class="rounded-full border bg-background px-4 py-2 text-sm font-medium transition hover:bg-accent"
      >
        Back to listings
      </RouterLink>
    </div>

    <div v-if="isLoading" class="rounded-[24px] border bg-background p-6 text-sm text-foreground/70">
      Loading listing...
    </div>

    <div v-else class="grid gap-6 lg:grid-cols-[0.85fr_1.15fr]">
      <aside class="space-y-4">
        <div class="overflow-hidden rounded-[28px] border bg-background">
          <div class="aspect-[4/5] bg-foreground/5">
            <img
              v-if="coverImage"
              :src="coverImage"
              :alt="lot?.title"
              class="h-full w-full object-cover"
            />
            <div v-else class="flex h-full items-center justify-center text-sm text-foreground/50">
              No image
            </div>
          </div>
        </div>

        <div v-if="lot?.media?.length" class="grid grid-cols-4 gap-2">
          <div
            v-for="media in lot.media"
            :key="media.key"
            class="aspect-square overflow-hidden rounded-2xl border bg-foreground/5"
          >
            <img
              v-if="media.url"
              :src="media.url"
              :alt="lot.title"
              class="h-full w-full object-cover"
            />
          </div>
        </div>

        <div class="rounded-[24px] border bg-background p-4 text-sm">
          <p class="text-xs uppercase tracking-[0.18em] text-foreground/45">
            Status
          </p>
          <p class="mt-2 font-medium">
            {{ lot?.statusName ?? normalizedStatus }}
          </p>
          <p v-if="!canEdit" class="mt-2 leading-6 text-foreground/60">
            This listing cannot be edited in its current status.
          </p>
        </div>
      </aside>

      <form class="rounded-[28px] border bg-background p-6 sm:p-8" @submit.prevent="submit">
        <div v-if="errorMessage" class="mb-6 rounded-[18px] border border-red-500/30 bg-red-500/10 px-4 py-3 text-sm text-red-300">
          {{ errorMessage }}
        </div>

        <div class="grid gap-5 md:grid-cols-2">
          <div class="md:col-span-2">
            <label class="text-sm font-medium">Title</label>
            <input
              v-model="form.title"
              :disabled="!canEdit || isSubmitting"
              class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition placeholder:text-foreground/40 focus:ring-2 focus:ring-foreground/20 disabled:opacity-60"
              placeholder="Title"
            />
          </div>

          <div>
            <label class="text-sm font-medium">Brand</label>
            <input
              v-model="form.brand"
              :disabled="!canEdit || isSubmitting"
              class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition placeholder:text-foreground/40 focus:ring-2 focus:ring-foreground/20 disabled:opacity-60"
              placeholder="Brand"
            />
          </div>

          <div>
            <label class="text-sm font-medium">Color</label>
            <input
              v-model="form.color"
              :disabled="!canEdit || isSubmitting"
              class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition placeholder:text-foreground/40 focus:ring-2 focus:ring-foreground/20 disabled:opacity-60"
              placeholder="Color"
            />
          </div>

          <div>
            <label class="text-sm font-medium">Price</label>
            <input
              v-model.number="form.amount"
              type="number"
              min="0"
              step="0.01"
              :disabled="!canEdit || isSubmitting"
              class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:ring-2 focus:ring-foreground/20 disabled:opacity-60"
            />
          </div>

          <div>
            <label class="text-sm font-medium">Currency</label>
            <select
              v-model="form.currency"
              :disabled="!canEdit || isSubmitting"
              class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:ring-2 focus:ring-foreground/20 disabled:opacity-60"
            >
              <option value="EUR">EUR</option>
            </select>
          </div>

          <div>
            <label class="text-sm font-medium">Year</label>
            <input
              v-model.number="form.year"
              type="number"
              min="1900"
              :max="new Date().getFullYear()"
              :disabled="!canEdit || isSubmitting"
              class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:ring-2 focus:ring-foreground/20 disabled:opacity-60"
              placeholder="Optional"
            />
          </div>

          <div>
            <label class="text-sm font-medium">Category</label>
            <select v-model.number="form.category" :disabled="!canEdit || isSubmitting" class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:ring-2 focus:ring-foreground/20 disabled:opacity-60">
              <option v-for="option in CATEGORY_OPTIONS" :key="option.value" :value="option.value">{{ option.label }}</option>
            </select>
          </div>

          <div>
            <label class="text-sm font-medium">Gender</label>
            <select v-model.number="form.gender" :disabled="!canEdit || isSubmitting" class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:ring-2 focus:ring-foreground/20 disabled:opacity-60">
              <option v-for="option in GENDER_OPTIONS" :key="option.value" :value="option.value">{{ option.label }}</option>
            </select>
          </div>

          <div>
            <label class="text-sm font-medium">Size</label>
            <select v-model.number="form.size" :disabled="!canEdit || isSubmitting" class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:ring-2 focus:ring-foreground/20 disabled:opacity-60">
              <option v-for="option in SIZE_OPTIONS" :key="option.value" :value="option.value">{{ option.label }}</option>
            </select>
          </div>

          <div>
            <label class="text-sm font-medium">Condition</label>
            <select v-model.number="form.condition" :disabled="!canEdit || isSubmitting" class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:ring-2 focus:ring-foreground/20 disabled:opacity-60">
              <option v-for="option in CONDITION_OPTIONS" :key="option.value" :value="option.value">{{ option.label }}</option>
            </select>
          </div>

          <div class="md:col-span-2">
            <label class="text-sm font-medium">Description</label>
            <textarea
              v-model="form.description"
              rows="7"
              :disabled="!canEdit || isSubmitting"
              class="mt-2 w-full rounded-2xl border bg-background px-4 py-3 text-sm outline-none transition placeholder:text-foreground/40 focus:ring-2 focus:ring-foreground/20 disabled:opacity-60"
              placeholder="Describe condition, fit, materials, and flaws."
            />
          </div>
        </div>

        <div class="mt-6 flex justify-end">
          <Button type="submit" :disabled="!canEdit || isSubmitting">
            {{ isSubmitting ? 'Saving...' : 'Save changes' }}
          </Button>
        </div>
      </form>
    </div>
  </div>
</template>
