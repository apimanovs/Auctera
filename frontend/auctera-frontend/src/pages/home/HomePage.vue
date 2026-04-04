<script setup lang="ts">
import { ref, onMounted } from 'vue'
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

const auctions = ref<Auction[]>([
  {
    id: 1,
    brand: 'Hermès',
    title: 'Vintage Leather Sofa - Pristine Condition',
    price: 1000000000,
    imageUrl: 'https://media-photos.depop.com/b1/28724162/3191473831_ade6609dc27340629bc6d4af9ac1b841/P0.jpg',
    timeLeft: '2 дня 14 часов',
  },
  {
    id: 2,
    brand: 'Gucci',
    title: 'Original Oil Painting by Unknown Artist',
    price: 320,
    imageUrl: 'https://media-photos.depop.com/b1/38992639/3121126790_b608df72f79c4f8b9cb99737678b74f5/P0.jpg',
    timeLeft: '3 дня 8 часов',
  },
  {
    id: 3,
    brand: 'Rolex',
    title: 'Rolex Submariner Watch Limited Edition',
    price: 5400,
    imageUrl: 'https://media-photos.depop.com/b1/40086058/3512274199_d42fb73add7043d586f1be825bfb1f68/P0.jpg',
    timeLeft: '1 день 6 часов',
  },
  {
    id: 4,
    brand: 'Chanel',
    title: 'Antique Wooden Cabinet with Mirror',
    price: 280,
    imageUrl: 'https://media-photos.depop.com/b1/43448124/3109199960_e0915b4487004c61a6f7613dca8db4a9/P0.jpg',
    timeLeft: '4 дня 20 часов',
  },
  {
    id: 5,
    brand: 'Tiffany & Co.',
    title: 'Vintage Pearl Necklace - 18K Gold Chain',
    price: 650,
    imageUrl: 'https://media-photos.depop.com/b1/51377749/3251202528_7ca0ed8a90b8496c9b8793bd28d3de17/P0.jpg',
    timeLeft: '5 дней 12 часов',
  },
  {
    id: 6,
    brand: 'Louis Vuitton',
    title: 'Brand New Gaming Laptop RTX 4090',
    price: 2100,
    imageUrl: 'https://images.thebestshops.com/product_images/original/SL12226-044_01-339d21.jpg',
    timeLeft: '18 часов 30 минут',
  },
  {
    id: 7,
    brand: 'The Beatles',
    title: 'Signed Beatles Vinyl Record First Edition',
    price: 3200,
    imageUrl: 'https://media-photos.depop.com/b1/45498419/3517277733_6056232abec546e987925ec83630f810/P0.jpg',
    timeLeft: '2 дня 5 часов',
  },
  {
    id: 8,
    brand: 'Prada',
    title: 'Handmade Italian Ceramic Vase',
    price: 420,
    imageUrl: 'https://media-photos.depop.com/b1/36719830/3498593736_9131744e9156480d90e996a6dcb6ec67/P0.jpg',
    timeLeft: '6 дней 10 часов',
  },
  {
    id: 9,
    brand: 'Yeezy',
    title: 'Vintage Leather Camera Bag',
    price: 180,
    imageUrl: 'https://media-photos.depop.com/b1/448068812/3474488748_5e0e9243711c4cbcb6dce7afdff37d6d/P0.jpg',
    timeLeft: '1 день 12 часов',
  },
  {
    id: 10,
    brand: 'Balenciaga',
    title: 'Classic Analogue Photocamera 35mm',
    price: 240,
    imageUrl: 'https://media-photos.depop.com/r1/341927691/3481391174_2e97b19297a44aae8e7e46d6e737e530/P6.jpg',
    timeLeft: '3 дня 4 часа',
  },
  {
    id: 11,
    brand: 'Cartier',
    title: 'Sterling Silver Coin Collection',
    price: 890,
    imageUrl: 'https://media-photos.depop.com/b1/20411984/2751785262_45ca34b51be54edbad51742f72c8c676/P0.jpg',
    timeLeft: '7 дней 2 часа',
  },
  {
    id: 12,
    brand: 'Gucci',
    title: 'Rare Bird Taxidermy Display',
    price: 1200,
    imageUrl: 'https://media-photos.depop.com/b1/51416371/3553513106_b896da1472a94103a15b55b05dd159f1/P0.jpg',
    timeLeft: '4 дня 18 часов',
  },
])
const loading = ref(false)
const error = ref('')

async function loadAuctions() {
  try {
    loading.value = true
    error.value = ''

    const data = await getAuctions()
    auctions.value = data
  } 
  catch (err) 
  {
    error.value = err instanceof Error ? err.message : 'Unknown error'
  } 
  finally 
  {
    loading.value = false
  }
}

onMounted(loadAuctions)
</script>

<template>
  <div class="">
    <section class="mb-10 overflow-hidden rounded-[28px] bg-neutral-100 dark:bg-neutral-800 px-6 py-8 md:px-10 md:py-10">
      <div class="grid items-center gap-8 md:grid-cols-1.15fr_0.85fr">
        <div class="max-w-2xl">
          <p class="text-[11px] uppercase tracking-[0.22em] text-foreground/60">
            Coutera
          </p>

          <h1 v-if="!isAuthenticated" class="mt-3 text-3xl font-semibold tracking-tight text-foreground sm:text-4xl md:text-5xl">
            Rare pieces, premium brands, and auctions worth watching.
          </h1>

          <h1
            v-if="isAuthenticated"
            class="mt-3 bg-gradient-to-r from-black via-neutral-600 to-neutral-400 bg-clip-text text-3xl font-semibold tracking-tight text-transparent sm:text-4xl md:text-5xl dark:from-white dark:via-neutral-200 dark:to-neutral-400"
          >
            Welcome back, {{ user?.username }}!
          </h1>
 
          <p
            v-if="!isAuthenticated"
            class="mt-4 max-w-xl text-sm leading-6 text-foreground/60 sm:text-base"
          >
            A refined marketplace for designer fashion, collectibles, and standout objects.
            Discover curated lots, place confident bids, and find items that feel worth owning.
          </p>
          <p
            v-else
            class="mt-4 max-w-xl text-sm leading-6 text-foreground/60 sm:text-base"
          >
            Discover what’s new, track your active bids, and uncover pieces that match your style.
            Your next standout item might already be waiting.
          </p>

          <div class="mt-6 flex flex-wrap gap-3">
            <button
              class="rounded-full border bg-background px-5 py-2.5 text-sm font-medium text-foreground transition cursor-pointer"
            >
              Explore auctions
            </button>

            <button
              class="rounded-full border bg-black dark:bg-white px-5 py-2.5 text-sm font-medium text-white dark:text-black transition"
            >
              Browse brands
            </button>
          </div>
        </div>
      </div>
    </section>

      <div class="mb-10 rounded-2xl border py-6 md:px-6">
        <div class="mb-6 flex items-end justify-between gap-2">
          <div>
            <div class="flex items-center gap-2">
              <h1 class="text-2xl font-bold">Trending Auctions</h1>
              <TrendingUpIcon class="h-5 w-5" />
            </div>
            <p class="mt-1 text-foreground/70">
              Discover the most popular auctions right now.
            </p>
          </div>
            <p class="mt-1 text-foreground/70 hover:text-foreground transition cursor-pointer">
              Browse more
            </p>
        </div>

        <Carousel
          :opts="{ loop: true, align: 'start', slidesToScroll: 1 }"
          class="w-full"
        >
            <CarouselContent class="-ml-3">
              <CarouselItem
                v-for="auction in auctions"
                :key="auction.id"
                class="pl-3 basis-1/2 sm:basis-1/2 md:basis-1/3 lg:basis-1/5"
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
            <p class="text-sm text-foreground">
              Scroll through trending picks
            </p>

            <div class="flex items-center gap-2">
              <CarouselPrevious
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
              />
              <CarouselNext
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
              />
            </div>
          </div>
        </Carousel>
      </div>
      
      <div class="mb-10">
        <h2
          id="brand-story-title"
          class="max-w-3xl text-3xl font-semibold leading-tight sm:text-4xl"
        >
          Shop by category
        </h2>
        <p class="text-sm text-foreground/70">
          Explore various categories with the latest releases.
        </p>

        <section
          class="mt-4 grid grid-cols-2 gap-4 sm:grid-cols-3 lg:grid-cols-5"
          aria-labelledby="brand-story-title"
        >
          <div class="border group aspect-square rounded-2xl bg-background p-6 flex flex-col justify-between cursor-pointer transition duration-300 hover:bg-black dark:hover:bg-white hover:text-white dark:hover:text-black">
            <span class="text-sm uppercase tracking-[0.2em] text-foreground group-hover:text-neutral-200 dark:group-hover:text-neutral-600">Category</span>
            <span class="text-xl font-semibold">Vintage</span>
          </div>

          <div class="border group aspect-square rounded-2xl bg-background p-6 flex flex-col justify-between cursor-pointer transition duration-300 hover:bg-black dark:hover:bg-white hover:text-white dark:hover:text-black">
            <span class="text-sm uppercase tracking-[0.2em] text-foreground group-hover:text-neutral-200 dark:group-hover:text-neutral-600">Category</span>
            <span class="text-xl font-semibold">Streetwear</span>
          </div>

          <div class="border group aspect-square rounded-2xl bg-background p-6 flex flex-col justify-between cursor-pointer transition duration-300 hover:bg-black dark:hover:bg-white hover:text-white dark:hover:text-black">
            <span class="text-sm uppercase tracking-[0.2em] text-foreground group-hover:text-neutral-200 dark:group-hover:text-neutral-600">Category</span>
            <span class="text-xl font-semibold">Designer</span>
          </div>

          <div class="border group aspect-square rounded-2xl bg-background p-6 flex flex-col justify-between cursor-pointer transition duration-300 hover:bg-black dark:hover:bg-white hover:text-white dark:hover:text-black">
            <span class="text-sm uppercase tracking-[0.2em] text-foreground group-hover:text-neutral-200 dark:group-hover:text-neutral-600">Category</span>
            <span class="text-xl font-semibold">Sneakers</span>
          </div>

          <div class="border group aspect-square rounded-2xl bg-background p-6 flex flex-col justify-between cursor-pointer transition duration-300 hover:bg-black dark:hover:bg-white hover:text-white dark:hover:text-black">
            <span class="text-sm uppercase tracking-[0.2em] text-foreground group-hover:text-neutral-200 dark:group-hover:text-neutral-600">Category</span>
            <span class="text-xl font-semibold">Accessories</span>
          </div>
        </section>
      </div>

      <div class="mb-12 rounded-2xl border py-6 md:px-6">
        <div class="mb-6 flex items-end justify-between gap-2">
          <div>
            <div class="flex items-center gap-2">
              <h1 class="text-2xl font-bold">New listings</h1>
              <RefreshCwIcon class="h-5 w-5" />
            </div>
            <p class="mt-1 text-foreground/70">
              Discover the most fresh auctions right now.
            </p>
          </div>
            <p class="mt-1 text-foreground/70 hover:text-foreground transition cursor-pointer">
              Browse more
            </p>
        </div>

        <Carousel
          :opts="{ loop: true, align: 'start', slidesToScroll: 1 }"
          class="w-full"
        >
            <CarouselContent class="-ml-3">
              <CarouselItem
                v-for="auction in auctions"
                :key="auction.id"
                class="pl-3 basis-1/2 sm:basis-1/2 md:basis-1/3 lg:basis-1/5"
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
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
              />
              <CarouselNext
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
              />
            </div>
          </div>
        </Carousel>
      </div>

      <div class="mb-10">
        <h2
          id="brand-story-title"
          class="max-w-3xl text-3xl font-semibold leading-tight sm:text-4xl"
        >
          Shop by brand
        </h2>
        <p class="text-sm text-foreground/70">
          Explore standout pieces from top brands and independent sellers.
        </p>

        <section
          class="mt-4 grid grid-cols-2 gap-4 sm:grid-cols-3 lg:grid-cols-5"
          aria-labelledby="brand-story-title"
        >
          <div class="border group aspect-square rounded-2xl bg-background p-6 flex flex-col justify-between cursor-pointer transition duration-300 hover:bg-black dark:hover:bg-white hover:text-white dark:hover:text-black">
            <span class="text-sm uppercase tracking-[0.2em] text-foreground group-hover:text-neutral-200 dark:group-hover:text-neutral-600">Brand</span>
            <span class="text-xl font-semibold">Prada</span>
          </div>

          <div class="border group aspect-square rounded-2xl bg-background p-6 flex flex-col justify-between cursor-pointer transition duration-300 hover:bg-black dark:hover:bg-white hover:text-white dark:hover:text-black">
            <span class="text-sm uppercase tracking-[0.2em] text-foreground group-hover:text-neutral-200 dark:group-hover:text-neutral-600">Brand</span>
            <span class="text-xl font-semibold">Gucci</span>
          </div>

          <div class="border group aspect-square rounded-2xl bg-background p-6 flex flex-col justify-between cursor-pointer transition duration-300 hover:bg-black dark:hover:bg-white hover:text-white dark:hover:text-black">
            <span class="text-sm uppercase tracking-[0.2em] text-foreground group-hover:text-neutral-200 dark:group-hover:text-neutral-600">Brand</span>
            <span class="text-xl font-semibold">Balenciaga</span>
          </div>

          <div class="border group aspect-square rounded-2xl bg-background p-6 flex flex-col justify-between cursor-pointer transition duration-300 hover:bg-black dark:hover:bg-white hover:text-white dark:hover:text-black">
            <span class="text-sm uppercase tracking-[0.2em] text-foreground group-hover:text-neutral-200 dark:group-hover:text-neutral-600">Brand</span>
            <span class="text-xl font-semibold">Vetements</span>
          </div>

          <div class="border group aspect-square rounded-2xl bg-background p-6 flex flex-col justify-between cursor-pointer transition duration-300 hover:bg-black dark:hover:bg-white hover:text-white dark:hover:text-black">
            <span class="text-sm uppercase tracking-[0.2em] text-foreground group-hover:text-neutral-200 dark:group-hover:text-neutral-600">Brand</span>
            <span class="text-xl font-semibold">Louis Vuitton</span>
          </div>
        </section>
      </div>

      <div class="mb-12 rounded-2xl border py-6 md:px-6">
        <div class="mb-6 flex items-end justify-between gap-2">
          <div>
            <div class="flex items-center gap-2">
              <h1 class="text-2xl font-bold">Ending soon</h1>
              <TimerIcon class="h-5 w-5" />
            </div>
            <p class="mt-1 text-foreground/70">
              Explore auctions that are about to end soon and place your bid before it's too late.
            </p>
          </div>
            <p class="mt-1 text-foreground/70 hover:text-foreground transition cursor-pointer">
              Browse more
            </p>
        </div>

        <Carousel
          :opts="{ loop: true, align: 'start', slidesToScroll: 1 }"
          class="w-full"
        >
            <CarouselContent class="-ml-3">
              <CarouselItem
                v-for="auction in auctions"
                :key="auction.id"
                class="pl-3 basis-1/2 sm:basis-1/2 md:basis-1/3 lg:basis-1/5"
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
              Scroll through ending soon auctions.
            </p>

            <div class="flex items-center gap-2">
              <CarouselPrevious
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
              />
              <CarouselNext
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border bg-background text-foreground shadow-sm hover:bg-black hover:text-white"
              />
            </div>
          </div>
        </Carousel>
      </div>
  </div>
  <div class="mt-20 text-sm text-foreground/70 max-w-3xl mx-auto text-center">
  <p>
    Coutera is an online auction marketplace focused on rare fashion, designer clothing, and collectible items.
    Users can browse auctions, place bids, and discover unique pieces from brands and independent sellers worldwide.
  </p>
</div>
</template>