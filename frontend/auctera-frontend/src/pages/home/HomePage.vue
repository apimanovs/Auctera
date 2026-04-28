<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { RouterLink } from 'vue-router'
import { Icon } from '@iconify/vue'
import { storeToRefs } from 'pinia'

import { getApiErrorMessage } from '@/app/helpers/apiError'
import { itemService } from '@/app/services/lotService'
import { useAuthStore } from '@/stores/authStore'
import AuctionCard from '@/components/auctions/AuctionCard.vue'
import TrendingUpIcon from '@/components/ui/icons/TrendingUpIcon.vue'
import RefreshCwIcon from '@/components/ui/icons/RefreshCwIcon.vue'
import TimerIcon from '@/components/ui/icons/TimerIcon.vue'
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from '@/components/ui/carousel'
import type { LotPreview } from '@/types/lot'

const authStore = useAuthStore()
const { user, isAuthenticated } = storeToRefs(authStore)

const lots = ref<LotPreview[]>([])
const loading = ref(false)
const error = ref('')

const exploreCollections = [
  {
    id: 1,
    label: 'Curated edit',
    title: 'Archive fashion',
    subtitle: 'Rare garments, iconic silhouettes, and collector-worthy pieces with lasting appeal.',
    imageUrl: 'https://images.unsplash.com/photo-1515886657613-9f3515b0c78f?auto=format&fit=crop&w=1400&q=80',
    to: '/auctions?search=archive',
  },
  {
    id: 2,
    label: 'Luxury selection',
    title: 'Designer bags',
    subtitle: 'Statement pieces, everyday icons, and timeless house classics.',
    imageUrl: 'https://images.unsplash.com/photo-1584917865442-de89df76afd3?auto=format&fit=crop&w=1400&q=80',
    to: '/auctions?category=4&search=bag',
  },
  {
    id: 3,
    label: 'Collector picks',
    title: 'Sneakers',
    subtitle: 'Coveted pairs, limited drops, and standout pairs worth watching.',
    imageUrl: 'https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=1400&q=80',
    to: '/auctions?category=3&search=sneakers',
  },
]

const popularBrands = ['Prada', 'Gucci', 'Balenciaga', 'Vetements', 'Louis Vuitton', 'Chanel']

const styles = [
  { label: 'Archive', icon: 'radix-icons:archive', query: 'archive' },
  { label: 'Minimal', icon: 'radix-icons:dash', query: 'minimal' },
  { label: 'Streetwear', icon: 'radix-icons:rocket', query: 'streetwear' },
  { label: 'Luxury', icon: 'radix-icons:diamond', query: 'luxury' },
  { label: 'Vintage', icon: 'radix-icons:counter-clockwise-clock', query: 'vintage' },
  { label: 'Avant-garde', icon: 'radix-icons:magic-wand', query: 'avant-garde' },
]

const categories = [
  { label: 'Tops', to: '/auctions?category=0' },
  { label: 'Bottoms', to: '/auctions?category=1' },
  { label: 'Outerwear', to: '/auctions?category=2' },
  { label: 'Footwear', to: '/auctions?category=3' },
  { label: 'Accessories', to: '/auctions?category=4' },
]

const trendingLots = computed(() => lots.value.slice(0, 10))
const newListings = computed(() => lots.value.slice(0, 10))
const endingSoon = computed(() => lots.value.filter((lot) => String(lot.statusName ?? '').toLowerCase().includes('listed')).slice(0, 10))

const locationLabel = (lot: LotPreview) =>
  [lot.sellerCity?.trim(), lot.sellerCountry?.trim()].filter(Boolean).join(', ')

const loadLots = async () => {
  try {
    loading.value = true
    error.value = ''
    lots.value = await itemService.getLots()
  } catch (loadError) {
    error.value = getApiErrorMessage(loadError, 'Failed to load listings.')
  } finally {
    loading.value = false
  }
}

onMounted(loadLots)
</script>

<template>
  <div class="space-y-16">
    <section class="relative overflow-hidden rounded-[32px] border bg-neutral-100 px-6 py-10 dark:bg-neutral-900 md:px-10 md:py-14">
      <div class="absolute inset-0 bg-gradient-to-br from-white via-transparent to-neutral-200/50 dark:from-white/5 dark:to-white/[0.03]" />

      <div class="relative max-w-3xl">
        <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/55">
          Coutera
        </p>

        <h1 v-if="!isAuthenticated" class="mt-4 text-4xl font-semibold leading-tight tracking-tight text-foreground sm:text-5xl md:text-6xl">
          Rare fashion, premium brands, and auctions worth watching.
        </h1>

        <h1 v-else class="mt-4 text-4xl font-semibold leading-tight tracking-tight sm:text-5xl md:text-6xl">
          Welcome back, <span class="text-foreground/55">{{ user?.username }}</span>
        </h1>

        <p class="mt-5 max-w-xl text-sm leading-7 text-foreground/65 sm:text-base">
          A refined auction marketplace for designer fashion, standout accessories, and collectible pieces with character.
        </p>

        <div class="mt-7 flex flex-wrap gap-3">
          <RouterLink to="/auctions" class="rounded-full bg-black px-5 py-2.5 text-sm font-medium text-white transition hover:opacity-90 dark:bg-white dark:text-black">
            Explore auctions
          </RouterLink>
          <RouterLink to="/sell" class="rounded-full border bg-background px-5 py-2.5 text-sm font-medium text-foreground transition hover:bg-black hover:text-white dark:hover:bg-white dark:hover:text-black">
            Sell an item
          </RouterLink>
        </div>
      </div>
    </section>

    <section class="space-y-6">
      <div class="flex items-end justify-between gap-4">
        <div class="max-w-2xl">
          <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/50">Explore</p>
          <h2 class="mt-2 text-3xl font-semibold tracking-tight sm:text-4xl">
            Discover the world of Coutera
          </h2>
          <p class="mt-3 text-sm leading-6 text-foreground/70 sm:text-base">
            Curated routes into fashion, rare finds, and collectible detail.
          </p>
        </div>
      </div>

      <div class="grid grid-cols-1 gap-4 lg:grid-cols-3">
        <RouterLink
          v-for="collection in exploreCollections"
          :key="collection.id"
          :to="collection.to"
          class="group relative overflow-hidden rounded-[28px] border bg-black"
        >
          <img :src="collection.imageUrl" :alt="collection.title" class="h-[240px] w-full object-cover opacity-80 transition duration-700 group-hover:scale-[1.04] group-hover:opacity-100" />
          <div class="absolute inset-0 bg-gradient-to-t from-black/85 via-black/30 to-transparent" />
          <div class="absolute bottom-0 left-0 p-5">
            <p class="text-[11px] uppercase tracking-[0.24em] text-white/60">{{ collection.label }}</p>
            <h3 class="mt-2 text-2xl font-semibold text-white">{{ collection.title }}</h3>
            <p class="mt-2 max-w-sm text-sm leading-6 text-white/70">{{ collection.subtitle }}</p>
          </div>
        </RouterLink>
      </div>
    </section>

    <section class="rounded-[28px] border px-4 py-6 md:px-6">
      <div class="mb-6 flex items-end justify-between gap-3">
        <div>
          <div class="flex items-center gap-2">
            <h2 class="text-2xl font-semibold tracking-tight">Trending Auctions</h2>
            <TrendingUpIcon class="h-5 w-5" />
          </div>
          <p class="mt-2 text-sm text-foreground/70 sm:text-base">The lots drawing attention right now.</p>
        </div>

        <RouterLink to="/auctions" class="text-sm text-foreground/70 transition hover:text-foreground">
          Browse more
        </RouterLink>
      </div>

      <div v-if="error" class="mb-4 rounded-2xl border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-600 dark:border-red-900/40 dark:bg-red-950/30 dark:text-red-300">
        {{ error }}
      </div>

      <div v-if="loading" class="rounded-2xl border p-5 text-sm text-foreground/60">
        Loading listings...
      </div>

      <div v-else-if="trendingLots.length === 0" class="rounded-2xl border p-5 text-sm text-foreground/60">
        No public listings yet.
      </div>

      <Carousel v-else :opts="{ loop: true, align: 'start', slidesToScroll: 1 }" class="w-full">
        <CarouselContent class="-ml-3">
          <CarouselItem v-for="lot in trendingLots" :key="lot.id" class="basis-[78%] pl-3 sm:basis-1/2 md:basis-1/3 lg:basis-1/5">
            <RouterLink :to="`/lots/${lot.id}`">
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
          </CarouselItem>
        </CarouselContent>

        <div class="mt-6 flex items-center justify-between">
          <p class="text-sm text-foreground/70">Scroll through trending picks</p>
          <div class="flex items-center gap-2">
            <CarouselPrevious class="static h-10 w-10 translate-x-0 translate-y-0 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white" />
            <CarouselNext class="static h-10 w-10 translate-x-0 translate-y-0 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white" />
          </div>
        </div>
      </Carousel>
    </section>

    <section>
      <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/50">Brands</p>
      <h2 class="mt-2 text-3xl font-semibold tracking-tight sm:text-4xl">Popular brands</h2>
      <div class="mt-5 grid grid-cols-2 gap-4 sm:grid-cols-3 lg:grid-cols-6">
        <RouterLink
          v-for="brand in popularBrands"
          :key="brand"
          :to="`/auctions?brand=${encodeURIComponent(brand)}`"
          class="group flex min-h-28 flex-col justify-between rounded-[24px] border bg-background p-5 transition duration-300 hover:bg-black hover:text-white dark:hover:bg-white dark:hover:text-black"
        >
          <span class="text-[11px] uppercase tracking-[0.22em] text-foreground/55 group-hover:text-white/70 dark:group-hover:text-black/70">Brand</span>
          <span class="text-lg font-semibold">{{ brand }}</span>
        </RouterLink>
      </div>
    </section>

    <section>
      <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/50">Styles</p>
      <h2 class="mt-2 text-3xl font-semibold tracking-tight sm:text-4xl">Shop by style</h2>
      <div class="mt-5 grid grid-cols-2 gap-4 sm:grid-cols-3 lg:grid-cols-6">
        <RouterLink
          v-for="style in styles"
          :key="style.label"
          :to="`/auctions?search=${encodeURIComponent(style.query)}`"
          class="group rounded-[24px] border bg-background p-5 transition duration-300 hover:bg-black hover:text-white dark:hover:bg-white dark:hover:text-black"
        >
          <div class="flex h-11 w-11 items-center justify-center rounded-2xl bg-foreground/5 text-foreground/70 group-hover:bg-white/10 group-hover:text-white dark:group-hover:bg-black/10 dark:group-hover:text-black">
            <Icon :icon="style.icon" class="h-5 w-5" />
          </div>
          <p class="mt-5 text-base font-semibold">{{ style.label }}</p>
        </RouterLink>
      </div>
    </section>

    <section>
      <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/50">Categories</p>
      <h2 class="mt-2 text-3xl font-semibold tracking-tight sm:text-4xl">Shop by category</h2>
      <div class="mt-5 grid grid-cols-2 gap-4 sm:grid-cols-3 lg:grid-cols-5">
        <RouterLink
          v-for="category in categories"
          :key="category.label"
          :to="category.to"
          class="group flex aspect-square flex-col justify-between rounded-[24px] border bg-background p-6 transition duration-300 hover:bg-black hover:text-white dark:hover:bg-white dark:hover:text-black"
        >
          <span class="text-[11px] uppercase tracking-[0.22em] text-foreground/55 group-hover:text-white/70 dark:group-hover:text-black/70">Category</span>
          <span class="text-xl font-semibold">{{ category.label }}</span>
        </RouterLink>
      </div>
    </section>

    <section v-if="newListings.length" class="rounded-[28px] border px-4 py-6 md:px-6">
      <div class="mb-6 flex items-end justify-between gap-3">
        <div>
          <div class="flex items-center gap-2">
            <h2 class="text-2xl font-semibold tracking-tight">New Listings</h2>
            <RefreshCwIcon class="h-5 w-5" />
          </div>
          <p class="mt-2 text-sm text-foreground/70 sm:text-base">Fresh arrivals worth a closer look.</p>
        </div>
      </div>

      <div class="grid gap-5 sm:grid-cols-2 lg:grid-cols-5">
        <RouterLink v-for="lot in newListings.slice(0, 5)" :key="lot.id" :to="`/lots/${lot.id}`">
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
    </section>

    <section v-if="endingSoon.length" class="rounded-[28px] border px-4 py-6 md:px-6">
      <div class="mb-6 flex items-center gap-2">
        <h2 class="text-2xl font-semibold tracking-tight">Ending Soon</h2>
        <TimerIcon class="h-5 w-5" />
      </div>

      <div class="grid gap-5 sm:grid-cols-2 lg:grid-cols-5">
        <RouterLink v-for="lot in endingSoon.slice(0, 5)" :key="lot.id" :to="`/lots/${lot.id}`">
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
    </section>
  </div>
</template>
