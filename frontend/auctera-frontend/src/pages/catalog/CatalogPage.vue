<script setup lang="ts">
import { computed, onMounted, reactive, watch } from 'vue'
import { RouterLink, useRoute, useRouter } from 'vue-router'

import AuctionCard from '@/components/auctions/AuctionCard.vue'
import { getApiErrorMessage } from '@/app/helpers/apiError'
import { itemService } from '@/app/services/lotService'
import {
  CATEGORY_OPTIONS,
  CONDITION_OPTIONS,
  GENDER_OPTIONS,
  SIZE_OPTIONS,
} from '@/features/lots/create-lot/options'
import type { LotPreview } from '@/types/lot'

const route = useRoute()
const router = useRouter()

const state = reactive({
  lots: [] as LotPreview[],
  isLoading: true,
  errorMessage: '',
})

const filters = reactive({
  search: '',
  category: '',
  brand: '',
  size: '',
  gender: '',
  condition: '',
  minPrice: '',
  maxPrice: '',
  minYear: '',
  maxYear: '',
  location: '',
  status: '',
})

const statusOptions = ['Published', 'Listed', 'Sold']

const hasActiveFilters = computed(() => Object.values(filters).some((value) => value.trim()))

const locationLabel = (lot: LotPreview) =>
  [lot.sellerCity?.trim(), lot.sellerCountry?.trim()].filter(Boolean).join(', ')

const toNumberOrUndefined = (value: string) => {
  if (!value.trim()) return undefined
  const parsed = Number(value)
  return Number.isFinite(parsed) ? parsed : undefined
}

const syncFiltersFromRoute = () => {
  const query = route.query

  filters.search = String(query.search ?? query.Search ?? '')
  filters.category = String(query.category ?? query.Category ?? '')
  filters.brand = String(query.brand ?? query.Brand ?? '')
  filters.size = String(query.size ?? query.Size ?? '')
  filters.gender = String(query.gender ?? query.Gender ?? '')
  filters.condition = String(query.condition ?? query.Condition ?? '')
  filters.minPrice = String(query.minPrice ?? query.MinPrice ?? '')
  filters.maxPrice = String(query.maxPrice ?? query.MaxPrice ?? '')
  filters.minYear = String(query.minYear ?? query.MinYear ?? '')
  filters.maxYear = String(query.maxYear ?? query.MaxYear ?? '')
  filters.location = String(query.location ?? query.Location ?? '')
  filters.status = String(query.status ?? query.Status ?? '')
}

const buildApiFilters = () => ({
  Search: filters.search.trim() || undefined,
  Category: toNumberOrUndefined(filters.category),
  Brand: filters.brand.trim() || undefined,
  Size: toNumberOrUndefined(filters.size),
  Gender: toNumberOrUndefined(filters.gender),
  Condition: toNumberOrUndefined(filters.condition),
  MinPrice: toNumberOrUndefined(filters.minPrice),
  MaxPrice: toNumberOrUndefined(filters.maxPrice),
  MinYear: toNumberOrUndefined(filters.minYear),
  MaxYear: toNumberOrUndefined(filters.maxYear),
  Location: filters.location.trim() || undefined,
  Status: filters.status || undefined,
})

const loadLots = async () => {
  try {
    state.isLoading = true
    state.errorMessage = ''
    state.lots = await itemService.getLots(buildApiFilters())
  } catch (error) {
    state.errorMessage = getApiErrorMessage(error, 'Failed to load catalog.')
  } finally {
    state.isLoading = false
  }
}

const applyFilters = async () => {
  const query = Object.fromEntries(
    Object.entries(filters)
      .filter(([, value]) => value.trim())
      .map(([key, value]) => [key, value.trim()]),
  )

  await router.push({ path: '/auctions', query })
}

const clearFilters = async () => {
  Object.assign(filters, {
    search: '',
    category: '',
    brand: '',
    size: '',
    gender: '',
    condition: '',
    minPrice: '',
    maxPrice: '',
    minYear: '',
    maxYear: '',
    location: '',
    status: '',
  })

  await router.push({ path: '/auctions' })
}

watch(
  () => route.query,
  async () => {
    syncFiltersFromRoute()
    await loadLots()
  },
)

onMounted(async () => {
  syncFiltersFromRoute()
  await loadLots()
})
</script>

<template>
  <div class="mx-auto max-w-7xl px-4 py-8 sm:px-6 lg:px-8">
    <div class="mb-6 flex flex-wrap items-end justify-between gap-4">
      <div>
        <p class="text-xs uppercase tracking-[0.22em] text-foreground/50">
          Marketplace
        </p>
        <h1 class="mt-2 text-3xl font-semibold tracking-tight">
          Catalog
        </h1>
        <p class="mt-3 text-sm leading-6 text-foreground/70">
          Search public listings by item, seller location, year, brand, and condition.
        </p>
      </div>

      <button
        v-if="hasActiveFilters"
        class="rounded-full border bg-background px-4 py-2 text-sm font-medium transition hover:bg-accent"
        @click="clearFilters"
      >
        Clear filters
      </button>
    </div>

    <form class="mb-6 rounded-[28px] border bg-background p-4 sm:p-5" @submit.prevent="applyFilters">
      <div class="grid gap-3 md:grid-cols-4">
        <input v-model="filters.search" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20 md:col-span-2" placeholder="Search title, brand, description" />
        <input v-model="filters.brand" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20" placeholder="Brand" />
        <input v-model="filters.location" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20" placeholder="Location" />

        <select v-model="filters.category" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20">
          <option value="">Category</option>
          <option v-for="option in CATEGORY_OPTIONS" :key="option.value" :value="String(option.value)">{{ option.label }}</option>
        </select>

        <select v-model="filters.gender" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20">
          <option value="">Gender</option>
          <option v-for="option in GENDER_OPTIONS" :key="option.value" :value="String(option.value)">{{ option.label }}</option>
        </select>

        <select v-model="filters.size" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20">
          <option value="">Size</option>
          <option v-for="option in SIZE_OPTIONS" :key="option.value" :value="String(option.value)">{{ option.label }}</option>
        </select>

        <select v-model="filters.condition" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20">
          <option value="">Condition</option>
          <option v-for="option in CONDITION_OPTIONS" :key="option.value" :value="String(option.value)">{{ option.label }}</option>
        </select>

        <input v-model="filters.minPrice" type="number" min="0" step="0.01" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20" placeholder="Min price" />
        <input v-model="filters.maxPrice" type="number" min="0" step="0.01" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20" placeholder="Max price" />
        <input v-model="filters.minYear" type="number" min="1900" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20" placeholder="Min year" />
        <input v-model="filters.maxYear" type="number" min="1900" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20" placeholder="Max year" />

        <select v-model="filters.status" class="h-10 rounded-2xl border bg-background px-3 text-sm outline-none focus:ring-2 focus:ring-foreground/20">
          <option value="">Status</option>
          <option v-for="status in statusOptions" :key="status" :value="status">{{ status }}</option>
        </select>

        <button class="h-10 rounded-2xl bg-foreground px-4 text-sm font-medium text-background transition hover:opacity-90 md:col-start-4">
          Apply filters
        </button>
      </div>
    </form>

    <div v-if="state.isLoading" class="rounded-[24px] border bg-background p-6 text-sm text-foreground/70">
      Loading catalog...
    </div>

    <div v-else-if="state.errorMessage" class="rounded-[24px] border border-red-500/30 bg-red-500/10 p-6 text-sm text-red-300">
      {{ state.errorMessage }}
    </div>

    <div v-else-if="state.lots.length === 0" class="rounded-[24px] border bg-background p-8 text-center text-sm text-foreground/70">
      No listings match these filters.
    </div>

    <div v-else class="grid gap-5 sm:grid-cols-2 xl:grid-cols-4">
      <RouterLink
        v-for="lot in state.lots"
        :key="lot.id"
        :to="`/lots/${lot.id}`"
        class="group overflow-hidden"
      >
        <AuctionCard
          :brand="lot.brand"
          :title="lot.title"
          :price="lot.price"
          :currency="lot.currency"
          :image-url="lot.media?.[0]?.url ?? ''"
          :time-left="lot.statusName ?? 'Live'"
          :location="locationLabel(lot)"
          :year="lot.year"
        />
      </RouterLink>
    </div>
  </div>
</template>
