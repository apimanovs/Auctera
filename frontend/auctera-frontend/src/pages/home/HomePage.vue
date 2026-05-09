<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import type { Auction } from '@/types/auction'
import { useAuthStore } from '@/stores/authStore'
import { storeToRefs } from 'pinia'
import { getAuctions } from '@/app/services/auctionService'

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

const authStore = useAuthStore()
const { user, isAuthenticated } = storeToRefs(authStore)

const exploreCollections = ref([
  {
    id: 1,
    label: 'Archive Direction',
    title: 'Avant-Garde',
    subtitle: 'Rick Owens, Margiela, layered silhouettes, and experimental cuts.',
    imageUrl: 'https://pub-d44c1b06b612479f8e654eedc923ffa1.r2.dev/display/collections/Avant_Garde_Collection_Photo.png',
    query: '?style=avant-garde',
  },
  {
    id: 2,
    label: 'Modern Luxury',
    title: 'Minimal Designer',
    subtitle: 'Clean lines, neutral tones, Prada-style essentials and refined fits.',
    imageUrl: 'https://pub-d44c1b06b612479f8e654eedc923ffa1.r2.dev/display/collections/Minimal_Designer_Collection_Photo.png',
    query: '?style=minimal',
  },
  {
    id: 3,
    label: 'Street Culture',
    title: 'Elevated Streetwear',
    subtitle: 'Oversized fits, sneakers, hoodies, and statement everyday pieces.',
    imageUrl: 'https://pub-d44c1b06b612479f8e654eedc923ffa1.r2.dev/display/collections/Elevated_Streetwear_Collection_Photo.png',
    query: '?style=streetwear',
  },
])

const categories = ref([
  {
    name: 'Vintage',
    imageUrl: 'https://pub-d44c1b06b612479f8e654eedc923ffa1.r2.dev/display/Vintage_Category_Icon.png',
  },
  {
    name: 'Streetwear',
    imageUrl: 'https://pub-d44c1b06b612479f8e654eedc923ffa1.r2.dev/display/Streetwear_Category_Icon.png',
  },
  {
    name: 'Designer',
    imageUrl: 'https://pub-d44c1b06b612479f8e654eedc923ffa1.r2.dev/display/Designer_Category_Icon.png',
  },
  {
    name: 'Sneakers',
    imageUrl: 'https://pub-d44c1b06b612479f8e654eedc923ffa1.r2.dev/display/Shoes_Category_Icon.png',
  },
  {
    name: 'Accessories',
    imageUrl: 'https://pub-d44c1b06b612479f8e654eedc923ffa1.r2.dev/display/Acessories_Categorie_Icon.png',
  },
])

const brands = ref([
  { name: 'Prada', logo: '/brands/prada-logo.svg' },
  { name: 'Gucci', logo: '/brands/gucci-logo.svg' },
  { name: 'Dolce Gabbana', logo: '/brands/dolce-gabbana-logo.svg' },
  { name: 'Dior', logo: '/brands/dior-logo.svg' },
  { name: 'Chanel', logo: '/brands/chanel-2-logo.svg' },
])

const auctions = ref<Auction[]>([
  {
    id: 1,
    brand: 'Hermès',
    title: 'Vintage Leather Sofa - Pristine Condition',
    price: 1000000000,
    imageUrl:
      'https://media-photos.depop.com/b1/28724162/3191473831_ade6609dc27340629bc6d4af9ac1b841/P0.jpg',
    timeLeft: '2 дня 14 часов',
  },
  {
    id: 2,
    brand: 'Gucci',
    title: 'Original Oil Painting by Unknown Artist',
    price: 320,
    imageUrl:
      'https://media-photos.depop.com/b1/38992639/3121126790_b608df72f79c4f8b9cb99737678b74f5/P0.jpg',
    timeLeft: '3 дня 8 часов',
  },
  {
    id: 3,
    brand: 'Rolex',
    title: 'Rolex Submariner Watch Limited Edition',
    price: 5400,
    imageUrl:
      'https://media-photos.depop.com/b1/40086058/3512274199_d42fb73add7043d586f1be825bfb1f68/P0.jpg',
    timeLeft: '1 день 6 часов',
  },
  {
    id: 4,
    brand: 'Chanel',
    title: 'Antique Wooden Cabinet with Mirror',
    price: 280,
    imageUrl:
      'https://media-photos.depop.com/b1/43448124/3109199960_e0915b4487004c61a6f7613dca8db4a9/P0.jpg',
    timeLeft: '4 дня 20 часов',
  },
  {
    id: 5,
    brand: 'Tiffany & Co.',
    title: 'Vintage Pearl Necklace - 18K Gold Chain',
    price: 650,
    imageUrl:
      'https://media-photos.depop.com/b1/51377749/3251202528_7ca0ed8a90b8496c9b8793bd28d3de17/P0.jpg',
    timeLeft: '5 дней 12 часов',
  },
  {
    id: 6,
    brand: 'Louis Vuitton',
    title: 'Brand New Gaming Laptop RTX 4090',
    price: 2100,
    imageUrl:
      'https://images.thebestshops.com/product_images/original/SL12226-044_01-339d21.jpg',
    timeLeft: '18 часов 30 минут',
  },
  {
    id: 7,
    brand: 'The Beatles',
    title: 'Signed Beatles Vinyl Record First Edition',
    price: 3200,
    imageUrl:
      'https://media-photos.depop.com/b1/45498419/3517277733_6056232abec546e987925ec83630f810/P0.jpg',
    timeLeft: '2 дня 5 часов',
  },
  {
    id: 8,
    brand: 'Prada',
    title: 'Handmade Italian Ceramic Vase',
    price: 420,
    imageUrl:
      'https://media-photos.depop.com/b1/36719830/3498593736_9131744e9156480d90e996a6dcb6ec67/P0.jpg',
    timeLeft: '6 дней 10 часов',
  },
  {
    id: 9,
    brand: 'Yeezy',
    title: 'Vintage Leather Camera Bag',
    price: 180,
    imageUrl:
      'https://media-photos.depop.com/b1/448068812/3474488748_5e0e9243711c4cbcb6dce7afdff37d6d/P0.jpg',
    timeLeft: '1 день 12 часов',
  },
  {
    id: 10,
    brand: 'Balenciaga',
    title: 'Classic Analogue Photocamera 35mm',
    price: 240,
    imageUrl:
      'https://media-photos.depop.com/r1/341927691/3481391174_2e97b19297a44aae8e7e46d6e737e530/P6.jpg',
    timeLeft: '3 дня 4 часа',
  },
  {
    id: 11,
    brand: 'Cartier',
    title: 'Sterling Silver Coin Collection',
    price: 890,
    imageUrl:
      'https://media-photos.depop.com/b1/20411984/2751785262_45ca34b51be54edbad51742f72c8c676/P0.jpg',
    timeLeft: '7 дней 2 часа',
  },
  {
    id: 12,
    brand: 'Gucci',
    title: 'Rare Bird Taxidermy Display',
    price: 1200,
    imageUrl:
      'https://media-photos.depop.com/b1/51416371/3553513106_b896da1472a94103a15b55b05dd159f1/P0.jpg',
    timeLeft: '4 дня 18 часов',
  },
])

const loading = ref(false)
const error = ref('')

const trendingAuctions = computed(() => auctions.value.slice(0, 10))
const newListings = computed(() => auctions.value.slice(2, 12))
const endingSoon = computed(() => auctions.value.slice(0, 10))
</script>

<template>
  <div class="space-y-16">
    <section
      class="relative overflow-hidden rounded-[32px] border bg-neutral-100 px-6 py-10 dark:bg-neutral-900 md:px-10 md:py-14"
    >
      <div class="absolute inset-0 bg-gradient-to-br from-white via-transparent to-neutral-200/50 dark:from-white/5 dark:to-white/[0.03]" />

      <div class="relative grid gap-10 lg:grid-cols-[1.15fr_0.85fr] lg:items-center">
        <div class="max-w-2xl">
          <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/55">
            Coutera
          </p>

          <h1
            v-if="!isAuthenticated"
            class="mt-4 text-4xl font-semibold leading-tight tracking-tight text-foreground sm:text-5xl md:text-6xl"
          >
            Rare fashion,
            <span class="text-foreground/55">premium brands,</span>
            and auctions worth watching.
          </h1>

          <h1
            v-else
            class="mt-4 text-4xl font-semibold leading-tight tracking-tight sm:text-5xl md:text-6xl"
          >
            Welcome back,
            <span class="bg-gradient-to-r from-black via-neutral-600 to-neutral-400 bg-clip-text text-transparent dark:from-white dark:via-neutral-200 dark:to-neutral-500">
              {{ user?.username }}
            </span>
          </h1>

          <p
            v-if="!isAuthenticated"
            class="mt-5 max-w-xl text-sm leading-7 text-foreground/65 sm:text-base"
          >
            A refined auction marketplace for designer fashion, standout accessories,
            and collectible pieces with character.
          </p>

          <p
            v-else
            class="mt-5 max-w-xl text-sm leading-7 text-foreground/65 sm:text-base"
          >
            Discover fresh arrivals, track pieces worth bidding on, and explore a more curated side of fashion.
          </p>

          <div class="mt-7 flex flex-wrap gap-3">
            <button
              class="rounded-full bg-black px-5 py-2.5 text-sm font-medium text-white transition hover:opacity-90 dark:bg-white dark:text-black cursor-pointer"
            >
              Explore auctions
            </button>

            <button
              class="cursor-pointer rounded-full border bg-background px-5 py-2.5 text-sm font-medium text-foreground transition hover:bg-black hover:text-white dark:hover:bg-white dark:hover:text-black"
            >
              Browse brands
            </button>
          </div>
        </div>
      </div>
    </section>

    <section class="space-y-6">
      <div class="flex items-end justify-between gap-4">
        <div class="max-w-2xl">
          <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/50">
            Explore
          </p>
          <h2 class="mt-2 text-3xl font-semibold tracking-tight sm:text-4xl">
            Discover the world of Coutera
          </h2>
          <p class="mt-3 text-sm leading-6 text-foreground/70 sm:text-base">
            Step into curated categories shaped by premium fashion, rare finds, and collector taste.
          </p>
        </div>

        <button
          class="hidden rounded-full border bg-background px-5 py-2.5 text-sm font-medium text-foreground transition hover:bg-black hover:text-white dark:hover:bg-white dark:hover:text-black md:inline-flex"
        >
          View all categories
        </button>
      </div>

      <div class="grid grid-cols-1 gap-4 lg:grid-cols-3">
        <article
          v-for="collection in exploreCollections"
          :key="collection.id"
          class="group relative overflow-hidden rounded-[32px] border bg-black"
        >
          <img
            :src="collection.imageUrl"
            :alt="collection.title"
            class="h-[240px] w-full object-cover transition duration-700 group-hover:scale-[1.06]"
          />

          <div class="absolute bottom-0 left-0 right-0 h-[70%] bg-gradient-to-t from-black/80 via-black/30 to-transparent transition duration-300 group-hover:from-black/60" />

          <div class="absolute bottom-0 left-0 p-6">
            <p class="text-[10px] uppercase tracking-[0.26em] text-white/60">
              {{ collection.label }}
            </p>

            <h3 class="mt-3 text-2xl font-semibold text-white">
              {{ collection.title }}
            </h3>

            <p class="mt-2 max-w-sm text-sm text-white/70">
              {{ collection.subtitle }}
            </p>
          </div>
        </article>
      </div>
    </section>

    <section class="rounded-[28px] border px-4 py-6 md:px-6">
      <div class="mb-6 flex items-end justify-between gap-3">
        <div>
          <div class="flex items-center gap-2">
            <h2 class="text-2xl font-semibold tracking-tight">Trending Auctions</h2>
            <TrendingUpIcon class="h-5 w-5" />
          </div>
          <p class="mt-2 text-sm text-foreground/70 sm:text-base">
            The lots drawing the most attention right now.
          </p>
        </div>

        <button class="text-sm text-foreground/70 transition hover:text-foreground">
          Browse more
        </button>
      </div>

      <div v-if="error" class="mb-4 rounded-2xl border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-600 dark:border-red-900/40 dark:bg-red-950/30 dark:text-red-300">
        {{ error }}
      </div>

      <Carousel
        :opts="{ loop: true, align: 'start', slidesToScroll: 1 }"
        class="w-full"
      >
        <CarouselContent class="-ml-3">
          <CarouselItem
            v-for="auction in trendingAuctions"
            :key="auction.id"
            class="basis-[78%] pl-3 sm:basis-1/2 md:basis-1/3 lg:basis-1/5"
          >
            <AuctionCard
              :brand="auction.brand"
              :title="auction.title"
              :price="auction.price"
              :image-url="auction.imageUrl"
              :time-left="auction.timeLeft"
            />
          </CarouselItem>
        </CarouselContent>

        <div class="mt-6 flex items-center justify-between">
          <p class="text-sm text-foreground/70">
            Scroll through trending picks
          </p>

          <div class="flex items-center gap-2">
            <CarouselPrevious
              class="static h-10 w-10 translate-x-0 translate-y-0 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
            />
            <CarouselNext
              class="static h-10 w-10 translate-x-0 translate-y-0 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
            />
          </div>
        </div>
      </Carousel>
    </section>

    <section>
      <div>
        <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/50">
          Categories
        </p>
        <h2 class="mt-2 text-3xl font-semibold tracking-tight sm:text-4xl">
          Shop by category
        </h2>
        <p class="mt-3 max-w-xl text-sm leading-6 text-foreground/70 sm:text-base">
          Explore standout styles across fashion, streetwear, and collectible detail.
        </p>
      </div>

      <div class="mt-5 flex gap-4 overflow-x-auto pb-3 sm:grid sm:grid-cols-2 sm:overflow-visible sm:pb-0 lg:grid-cols-5">
        <article
          v-for="category in categories"
          :key="category.name"
          class="group relative h-[220px] min-w-[230px] cursor-pointer overflow-hidden rounded-[28px] border bg-neutral-100 dark:bg-neutral-900 sm:min-w-0"
        >
          <img
            :src="category.imageUrl"
            :alt="category.name"
            class="h-full w-full object-cover transition duration-700 group-hover:scale-105"
          />

          <div class="absolute bottom-0 left-0 right-0 h-[65%] bg-gradient-to-t from-black/70 via-black/25 to-transparent transition duration-300 group-hover:from-black/55" />

          <div class="absolute bottom-0 left-0 p-5">
            <p class="text-[11px] uppercase tracking-[0.22em] text-white/65 drop-shadow">
              Category
            </p>

            <h3 class="mt-2 text-xl font-semibold text-white drop-shadow-[0_2px_8px_rgba(0,0,0,0.65)]">
              {{ category.name }}
            </h3>
          </div>
        </article>
      </div>
    </section>

    <section>
      <div class="max-w-3xl">
        <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/50">
          Why Coutera
        </p>
        <h2 class="mt-2 text-3xl font-semibold tracking-tight sm:text-4xl">
          A marketplace shaped around taste, trust, and standout pieces
        </h2>
        <p class="mt-3 text-sm leading-6 text-foreground/70 sm:text-base">
          Built for people who want more than endless listings — a place where discovery feels curated.
        </p>
      </div>

      <div class="mt-5 grid grid-cols-1 gap-5 md:grid-cols-2 xl:grid-cols-4">
        <article class="rounded-[24px] border bg-background px-7 py-8">
          <div class="mb-6 flex h-14 w-14 items-center justify-center rounded-2xl bg-neutral-100 text-foreground/70 dark:bg-neutral-800">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-7 w-7" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="1.5">
              <path stroke-linecap="round" stroke-linejoin="round" d="M17 20h5V4H2v16h5m10 0v-4a3 3 0 10-6 0v4m6 0H9" />
            </svg>
          </div>
          <h3 class="text-xl font-semibold">12K+ active collectors</h3>
          <p class="mt-3 text-sm leading-6 text-foreground/70">
            A growing community of buyers and sellers following rare fashion, premium labels, and standout finds.
          </p>
        </article>

        <article class="rounded-[24px] border bg-background px-7 py-8">
          <div class="mb-6 flex h-14 w-14 items-center justify-center rounded-2xl bg-neutral-100 text-foreground/70 dark:bg-neutral-800">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-7 w-7" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="1.5">
              <path stroke-linecap="round" stroke-linejoin="round" d="M6 3h12l1 6-7 12L5 9l1-6z" />
            </svg>
          </div>
          <h3 class="text-xl font-semibold">3.8K curated listings</h3>
          <p class="mt-3 text-sm leading-6 text-foreground/70">
            From archive garments and designer bags to premium sneakers and collectible accessories.
          </p>
        </article>

        <article class="rounded-[24px] border bg-background px-7 py-8">
          <div class="mb-6 flex h-14 w-14 items-center justify-center rounded-2xl bg-neutral-100 text-foreground/70 dark:bg-neutral-800">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-7 w-7" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="1.5">
              <path stroke-linecap="round" stroke-linejoin="round" d="M12 8c-1.657 0-3 1.343-3 3v1H8a2 2 0 00-2 2v4h12v-4a2 2 0 00-2-2h-1v-1c0-1.657-1.343-3-3-3z" />
            </svg>
          </div>
          <h3 class="text-xl font-semibold">98% seller verification rate</h3>
          <p class="mt-3 text-sm leading-6 text-foreground/70">
            A more trusted marketplace environment built around stronger quality control and presentation.
          </p>
        </article>

        <article class="rounded-[24px] border bg-background px-7 py-8">
          <div class="mb-6 flex h-14 w-14 items-center justify-center rounded-2xl bg-neutral-100 text-foreground/70 dark:bg-neutral-800">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-7 w-7" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="1.5">
              <path stroke-linecap="round" stroke-linejoin="round" d="M17 20h5V4H2v16h5m10 0a3 3 0 01-6 0m6 0a3 3 0 00-6 0" />
            </svg>
          </div>
          <h3 class="text-xl font-semibold">4.9/5 buyer satisfaction</h3>
          <p class="mt-3 text-sm leading-6 text-foreground/70">
            Trusted by users who value smooth bidding, premium selection, and a more refined buying experience.
          </p>
        </article>
      </div>
    </section>

    <section class="rounded-[28px] border px-4 py-6 md:px-6">
      <div class="mb-6 flex items-end justify-between gap-3">
        <div>
          <div class="flex items-center gap-2">
            <h2 class="text-2xl font-semibold tracking-tight">New Listings</h2>
            <RefreshCwIcon class="h-5 w-5" />
          </div>
          <p class="mt-2 text-sm text-foreground/70 sm:text-base">
            Fresh arrivals worth a closer look.
          </p>
        </div>

        <button class="text-sm text-foreground/70 transition hover:text-foreground">
          Browse more
        </button>
      </div>

      <Carousel
        :opts="{ loop: true, align: 'start', slidesToScroll: 1 }"
        class="w-full"
      >
        <CarouselContent class="-ml-3">
          <CarouselItem
            v-for="auction in newListings"
            :key="auction.id"
            class="basis-[78%] pl-3 sm:basis-1/2 md:basis-1/3 lg:basis-1/5"
          >
            <AuctionCard
              :brand="auction.brand"
              :title="auction.title"
              :price="auction.price"
              :image-url="auction.imageUrl"
              :time-left="auction.timeLeft"
            />
          </CarouselItem>
        </CarouselContent>

        <div class="mt-6 flex items-center justify-between">
          <p class="text-sm text-foreground/70">
            Scroll through newly listed auctions
          </p>

          <div class="flex items-center gap-2">
            <CarouselPrevious
              class="static h-10 w-10 translate-x-0 translate-y-0 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
            />
            <CarouselNext
              class="static h-10 w-10 translate-x-0 translate-y-0 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
            />
          </div>
        </div>
      </Carousel>
    </section>

    <section>
      <div>
        <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/50">
          Brands
        </p>
        <h2 class="mt-2 text-3xl font-semibold tracking-tight sm:text-4xl">
          Shop by brand
        </h2>
        <p class="mt-3 max-w-xl text-sm leading-6 text-foreground/70 sm:text-base">
          Explore standout pieces from globally recognized labels and highly desirable names.
        </p>
      </div>

        <div class="mt-5 flex gap-4 overflow-x-auto pb-3 sm:grid sm:grid-cols-3 sm:overflow-visible sm:pb-0 lg:grid-cols-5">        <article
          v-for="brand in brands"
          :key="brand.name"
          class="group flex aspect-square cursor-pointer flex-col items-center justify-center rounded-[28px] border bg-background p-6 text-center transition duration-300 hover:bg-black dark:hover:bg-white"
        >
          <img
            :src="brand.logo"
            :alt="brand.name"
            class="max-h-30 max-w-[150px] object-contain transition duration-300  group-hover:invert dark:invert group-hover:dark:invert-0"
          />

          <p class="mt-5 text-[11px] uppercase tracking-[0.22em] text-foreground/45 group-hover:text-white/60 dark:group-hover:text-black/60">
            Brand
          </p>
        </article>
      </div>
    </section>

    <section class="rounded-[28px] border px-4 py-6 md:px-6">
      <div class="mb-6 flex items-end justify-between gap-3">
        <div>
          <div class="flex items-center gap-2">
            <h2 class="text-2xl font-semibold tracking-tight">Ending Soon</h2>
            <TimerIcon class="h-5 w-5" />
          </div>
          <p class="mt-2 text-sm text-foreground/70 sm:text-base">
            Auctions nearing the finish line — last chance to place a bid.
          </p>
        </div>

        <button class="text-sm text-foreground/70 transition hover:text-foreground">
          Browse more
        </button>
      </div>

      <Carousel
        :opts="{ loop: true, align: 'start', slidesToScroll: 1 }"
        class="w-full"
      >
        <CarouselContent class="-ml-3">
          <CarouselItem
            v-for="auction in endingSoon"
            :key="auction.id"
            class="basis-[78%] pl-3 sm:basis-1/2 md:basis-1/3 lg:basis-1/5"
          >
            <AuctionCard
              :brand="auction.brand"
              :title="auction.title"
              :price="auction.price"
              :image-url="auction.imageUrl"
              :time-left="auction.timeLeft"
            />
          </CarouselItem>
        </CarouselContent>

        <div class="mt-6 flex items-center justify-between">
          <p class="text-sm text-foreground/70">
            Scroll through ending soon auctions
          </p>

          <div class="flex items-center gap-2">
            <CarouselPrevious
              class="static h-10 w-10 translate-x-0 translate-y-0 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
            />
            <CarouselNext
              class="static h-10 w-10 translate-x-0 translate-y-0 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
            />
          </div>
        </div>
      </Carousel>
    </section>

    <section class="mx-auto max-w-3xl border-t pt-10 text-center">
      <p class="text-xs leading-7 text-foreground/65 sm:text-base">
        Coutera is an online auction marketplace focused on rare fashion, designer clothing,
        and collectible pieces. Browse curated listings, place bids with confidence, and discover
        items that feel distinctive, memorable, and worth owning.
      </p>
    </section>
  </div>
</template>