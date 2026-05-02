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
  country: '',
  city: '',
  age: '',
  style: '',
})

console.log('EditLotPage initialized with lot ID:', form)

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
    form.country = data.country
    form.city = data.city
    form.age = data.age
    form.style = data.style
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

  if (!user.value?.userId) {
    errorMessage.value = 'You need to sign in before editing.'
    return
  }

  if (
    !form.title.trim() ||
    !form.description.trim() ||
    form.amount <= 0 ||
    !form.brand.trim() ||
    !form.color.trim() ||
    !form.country.trim() ||
    !form.city.trim() ||
    !form.age.trim() ||
    !form.style.trim()
  ) {
    errorMessage.value = 'Please complete all required fields with valid values.'
    return
  }

  try {
    isSubmitting.value = true

    await itemService.updateLot(lot.value.id, {
      id: lot.value.id,
      sellerId: user.value?.userId,
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
      country: form.country.trim(),
      city: form.city.trim(),
      age: form.age.trim(),
      style: form.style.trim(),
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
  <div class="min-h-screen bg-background text-foreground">
    <div class="mx-auto max-w-5xl px-4 py-10 sm:px-6 lg:px-8">
      <div class="mb-8 flex flex-col gap-3">
        <p class="text-xs uppercase tracking-[0.22em] text-foreground/50">
          Seller dashboard
        </p>

        <h1 class="text-3xl font-semibold tracking-tight sm:text-4xl">
          Edit listing
        </h1>

        <p class="max-w-2xl text-sm leading-6 text-foreground/60">
          Update your listing details before it is approved and published on Coutera.
        </p>
      </div>

      <div
        v-if="isLoading"
        class="rounded-[28px] border bg-background p-8 text-sm text-foreground/60"
      >
        Loading listing...
      </div>

      <div v-else class="space-y-6">
        <div
          v-if="errorMessage"
          class="rounded-[20px] border border-red-500/20 bg-red-500/10 px-4 py-3 text-sm text-red-300"
        >
          {{ errorMessage }}
        </div>

        <div
          v-if="successMessage"
          class="rounded-[20px] border border-emerald-500/20 bg-emerald-500/10 px-4 py-3 text-sm text-emerald-300"
        >
          {{ successMessage }}
        </div>

        <div
          v-if="!canEdit"
          class="rounded-[24px] border bg-foreground/[0.03] p-5 text-sm text-foreground/70"
        >
          This listing cannot be edited because it is currently
          <span class="font-medium text-foreground">
            {{ lot?.statusName ?? normalizeStatus }}
          </span>.
        </div>

        <form
          class="overflow-hidden rounded-[32px] border bg-background shadow-sm"
          @submit.prevent="submit"
        >
          <div class="border-b p-6 sm:p-8">
            <h2 class="text-xl font-semibold tracking-tight">
              Basic information
            </h2>

            <p class="mt-2 text-sm leading-6 text-foreground/60">
              Add the main details buyers will see first.
            </p>

            <div class="mt-6 grid gap-5">
              <div>
                <label class="mb-2 block text-sm font-medium">
                  Title
                </label>

                <input
                  v-model="form.title"
                  :disabled="!canEdit || isSubmitting"
                  class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                  placeholder="Example: Rick Owens DRKSHDW hoodie"
                />
              </div>

              <div>
                <label class="mb-2 block text-sm font-medium">
                  Description
                </label>

                <textarea
                  v-model="form.description"
                  :disabled="!canEdit || isSubmitting"
                  class="min-h-[150px] w-full resize-none rounded-2xl border bg-background p-4 text-sm leading-6 outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                  placeholder="Describe condition, fit, material, flaws, authenticity, and anything important."
                />
              </div>

              <div class="border-b p-6 sm:p-8">
                <h2 class="text-xl font-semibold tracking-tight">
                  Location and style
                </h2>

                <p class="mt-2 text-sm leading-6 text-foreground/60">
                  Tell buyers where the item ships from and what style it belongs to.
                </p>

                <div class="mt-6 grid gap-5 md:grid-cols-2">
                  <div>
                    <label class="mb-2 block text-sm font-medium">
                      Country
                    </label>

                    <input
                      v-model="form.country"
                      :disabled="!canEdit || isSubmitting"
                      class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                      placeholder="Latvia"
                    />
                  </div>

                  <div>
                    <label class="mb-2 block text-sm font-medium">
                      City
                    </label>

                    <input
                      v-model="form.city"
                      :disabled="!canEdit || isSubmitting"
                      class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                      placeholder="Riga"
                    />
                  </div>

                  <div>
                    <label class="mb-2 block text-sm font-medium">
                      Age / year
                    </label>

                    <input
                      v-model="form.age"
                      :disabled="!canEdit || isSubmitting"
                      class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                      placeholder="2020, 90s, vintage..."
                    />
                  </div>

                  <div>
                    <label class="mb-2 block text-sm font-medium">
                      Style
                    </label>

                    <input
                      v-model="form.style"
                      :disabled="!canEdit || isSubmitting"
                      class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                      placeholder="Archive, streetwear, minimalist..."
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="border-b p-6 sm:p-8">
            <h2 class="text-xl font-semibold tracking-tight">
              Item details
            </h2>

            <p class="mt-2 text-sm leading-6 text-foreground/60">
              Help buyers understand what type of item this is.
            </p>

            <div class="mt-6 grid gap-5 md:grid-cols-2">
              <div>
                <label class="mb-2 block text-sm font-medium">
                  Brand
                </label>

                <input
                  v-model="form.brand"
                  :disabled="!canEdit || isSubmitting"
                  class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                  placeholder="Brand name"
                />
              </div>

              <div>
                <label class="mb-2 block text-sm font-medium">
                  Color
                </label>

                <input
                  v-model="form.color"
                  :disabled="!canEdit || isSubmitting"
                  class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                  placeholder="Black, grey, navy..."
                />
              </div>

              <div>
                <label class="mb-2 block text-sm font-medium">
                  Category
                </label>

                <select
                  v-model.number="form.category"
                  :disabled="!canEdit || isSubmitting"
                  class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                >
                  <option
                    v-for="option in CATEGORY_OPTIONS"
                    :key="option.value"
                    :value="option.value"
                  >
                    {{ option.label }}
                  </option>
                </select>
              </div>

              <div>
                <label class="mb-2 block text-sm font-medium">
                  Gender
                </label>

                <select
                  v-model.number="form.gender"
                  :disabled="!canEdit || isSubmitting"
                  class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                >
                  <option
                    v-for="option in GENDER_OPTIONS"
                    :key="option.value"
                    :value="option.value"
                  >
                    {{ option.label }}
                  </option>
                </select>
              </div>

              <div>
                <label class="mb-2 block text-sm font-medium">
                  Size
                </label>

                <select
                  v-model.number="form.size"
                  :disabled="!canEdit || isSubmitting"
                  class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                >
                  <option
                    v-for="option in SIZE_OPTIONS"
                    :key="option.value"
                    :value="option.value"
                  >
                    {{ option.label }}
                  </option>
                </select>
              </div>

              <div>
                <label class="mb-2 block text-sm font-medium">
                  Condition
                </label>

                <select
                  v-model.number="form.condition"
                  :disabled="!canEdit || isSubmitting"
                  class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                >
                  <option
                    v-for="option in CONDITION_OPTIONS"
                    :key="option.value"
                    :value="option.value"
                  >
                    {{ option.label }}
                  </option>
                </select>
              </div>
            </div>
          </div>

          <div class="p-6 sm:p-8">
            <h2 class="text-xl font-semibold tracking-tight">
              Pricing
            </h2>

            <p class="mt-2 text-sm leading-6 text-foreground/60">
              Set the starting price for your listing.
            </p>

            <div class="mt-6 grid gap-5 md:grid-cols-2">
              <div>
                <label class="mb-2 block text-sm font-medium">
                  Amount
                </label>

                <input
                  v-model.number="form.amount"
                  type="number"
                  min="0"
                  step="0.01"
                  :disabled="!canEdit || isSubmitting"
                  class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                  placeholder="0.00"
                />
              </div>

              <div>
                <label class="mb-2 block text-sm font-medium">
                  Currency
                </label>

                <input
                  v-model="form.currency"
                  :disabled="!canEdit || isSubmitting"
                  class="h-12 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition focus:border-foreground disabled:cursor-not-allowed disabled:opacity-50"
                  placeholder="EUR"
                />
              </div>
            </div>

            <div class="mt-8 flex justify-end">
              <button
                type="submit"
                class="inline-flex items-center justify-center rounded-2xl bg-foreground px-6 py-3 text-sm font-medium text-background transition hover:opacity-90 disabled:cursor-not-allowed disabled:opacity-50"
                :disabled="!canEdit || isSubmitting"
              >
                {{ isSubmitting ? 'Saving changes...' : 'Save changes' }}
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
