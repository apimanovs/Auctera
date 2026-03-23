<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { Auction } from '@/types/auction'
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



const auctions = ref<Auction[]>([
  {
    id: 1,
    title: 'Vintage Leather Sofa - Pristine Condition',
    price: 450,
    imageUrl: 'https://media-photos.depop.com/b1/28724162/3191473831_ade6609dc27340629bc6d4af9ac1b841/P0.jpg',
    timeLeft: '2 дня 14 часов',
  },
  {
    id: 2,
    title: 'Original Oil Painting by Unknown Artist',
    price: 320,
    imageUrl: 'https://media-photos.depop.com/b1/38992639/3121126790_b608df72f79c4f8b9cb99737678b74f5/P0.jpg',
    timeLeft: '3 дня 8 часов',
  },
  {
    id: 3,
    title: 'Rolex Submariner Watch Limited Edition',
    price: 5400,
    imageUrl: 'https://media-photos.depop.com/b1/40086058/3512274199_d42fb73add7043d586f1be825bfb1f68/P0.jpg',
    timeLeft: '1 день 6 часов',
  },
  {
    id: 4,
    title: 'Antique Wooden Cabinet with Mirror',
    price: 280,
    imageUrl: 'https://media-photos.depop.com/b1/43448124/3109199960_e0915b4487004c61a6f7613dca8db4a9/P0.jpg',
    timeLeft: '4 дня 20 часов',
  },
  {
    id: 5,
    title: 'Vintage Pearl Necklace - 18K Gold Chain',
    price: 650,
    imageUrl: 'https://media-photos.depop.com/b1/51377749/3251202528_7ca0ed8a90b8496c9b8793bd28d3de17/P0.jpg',
    timeLeft: '5 дней 12 часов',
  },
  {
    id: 6,
    title: 'Brand New Gaming Laptop RTX 4090',
    price: 2100,
    imageUrl: 'https://images.thebestshops.com/product_images/original/SL12226-044_01-339d21.jpg',
    timeLeft: '18 часов 30 минут',
  },
  {
    id: 7,
    title: 'Signed Beatles Vinyl Record First Edition',
    price: 3200,
    imageUrl: 'https://media-photos.depop.com/b1/45498419/3517277733_6056232abec546e987925ec83630f810/P0.jpg',
    timeLeft: '2 дня 5 часов',
  },
  {
    id: 8,
    title: 'Handmade Italian Ceramic Vase',
    price: 420,
    imageUrl: 'https://media-photos.depop.com/b1/36719830/3498593736_9131744e9156480d90e996a6dcb6ec67/P0.jpg',
    timeLeft: '6 дней 10 часов',
  },
  {
    id: 9,
    title: 'Vintage Leather Camera Bag',
    price: 180,
    imageUrl: 'https://media-photos.depop.com/b1/448068812/3474488748_5e0e9243711c4cbcb6dce7afdff37d6d/P0.jpg',
    timeLeft: '1 день 12 часов',
  },
  {
    id: 10,
    title: 'Classic Analogue Photocamera 35mm',
    price: 240,
    imageUrl: 'https://media-photos.depop.com/r1/341927691/3481391174_2e97b19297a44aae8e7e46d6e737e530/P6.jpg',
    timeLeft: '3 дня 4 часа',
  },
  {
    id: 11,
    title: 'Sterling Silver Coin Collection',
    price: 890,
    imageUrl: 'https://media-photos.depop.com/b1/20411984/2751785262_45ca34b51be54edbad51742f72c8c676/P0.jpg',
    timeLeft: '7 дней 2 часа',
  },
  {
    id: 12,
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
    <section class="mb-10 overflow-hidden rounded-[28px] bg-neutral-100 px-6 py-8 md:px-10 md:py-10">
      <div class="grid items-center gap-8 md:grid-cols-[1.15fr_0.85fr]">
        <div class="max-w-2xl">
          <p class="text-[11px] uppercase tracking-[0.22em] text-black/40">
            Coutera
          </p>

          <h1 class="mt-3 text-3xl font-semibold tracking-tight text-black sm:text-4xl md:text-5xl">
            Rare pieces, premium brands, and auctions worth watching.
          </h1>

          <p class="mt-4 max-w-xl text-sm leading-6 text-black/60 sm:text-base">
            A refined marketplace for designer fashion, collectibles, and standout objects.
            Discover curated lots, place confident bids, and find items that feel worth owning.
          </p>

          <div class="mt-6 flex flex-wrap gap-3">
            <button
              class="rounded-full bg-black px-5 py-2.5 text-sm font-medium text-white transition hover:bg-black/90"
            >
              Explore auctions
            </button>

            <button
              class="rounded-full border border-black/10 bg-white px-5 py-2.5 text-sm font-medium text-black transition hover:border-black"
            >
              Browse brands
            </button>
          </div>
        </div>

        <div class="relative">
          <div class="overflow-hidden rounded-[24px] bg-white">
              Add slider with timer
          </div>
        </div>
      </div>
    </section>

      <div class="mb-12 rounded-2xl border border-black/10 py-6 md:px-6">
        <div class="mb-6 flex items-end justify-between gap-2">
          <div>
            <div class="flex items-center gap-2">
              <h1 class="text-2xl font-bold">Trending Auctions</h1>
              <TrendingUpIcon class="h-5 w-5" />
            </div>
            <p class="mt-1 text-gray-700">
              Discover the most popular auctions right now.
            </p>
          </div>
            <p class="mt-1 text-black hover:underline hover:text-gray-700 transition cursor-pointer">
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
                  :title="auction.title"
                  :price="auction.price"
                  :image-url="auction.imageUrl"
                  :time-left="auction.timeLeft"
                />
              </CarouselItem>
            </CarouselContent>

          <div class="mt-6 flex items-center justify-between">
            <p class="text-sm text-black/50">
              Scroll through trending picks
            </p>

            <div class="flex items-center gap-2">
              <CarouselPrevious
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border border-black/10 bg-white text-black shadow-sm hover:bg-black hover:text-white"
              />
              <CarouselNext
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border border-black/10 bg-white text-black shadow-sm hover:bg-black hover:text-white"
              />
            </div>
          </div>
        </Carousel>
      </div>
  
      <section
          class="my-16 overflow-hidden text-black flex flex-col items-start gap-6 sm:gap-10 lg:flex-row lg:items-center py-16"
          aria-labelledby="brand-story-title"
        >
          <h2 id="brand-story-title" class="max-w-3xl text-3xl font-semibold leading-tight sm:text-4xl">
            Rare pieces. Real demand. Timeless value.
          </h2>
          <p class="mt-4 max-w-2xl text-sm text-black/80 sm:text-base">
            Coutera brings together curated auctions, premium brands, and collectible finds in a refined digital marketplace built for discovery and confident bidding.
          </p>
      </section>

      <div class="mb-12 rounded-2xl border border-black/10 py-6 md:px-6">
        <div class="mb-6 flex items-end justify-between gap-2">
          <div>
            <div class="flex items-center gap-2">
              <h1 class="text-2xl font-bold">New listings</h1>
              <RefreshCwIcon class="h-5 w-5" />
            </div>
            <p class="mt-1 text-gray-700">
              Discover the most fresh auctions right now.
            </p>
          </div>
            <p class="mt-1 text-black hover:underline hover:text-gray-700 transition cursor-pointer">
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
                  :title="auction.title"
                  :price="auction.price"
                  :image-url="auction.imageUrl"
                  :time-left="auction.timeLeft"
                />
              </CarouselItem>
            </CarouselContent>

          <div class="mt-6 flex items-center justify-between">
            <p class="text-sm text-black/50">
              Scroll through .
            </p>

            <div class="flex items-center gap-2">
              <CarouselPrevious
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border border-black/10 bg-white text-black shadow-sm hover:bg-black hover:text-white"
              />
              <CarouselNext
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border border-black/10 bg-white text-black shadow-sm hover:bg-black hover:text-white"
              />
            </div>
          </div>
        </Carousel>
      </div>

      <section
          class="my-16 overflow-hidden text-black flex flex-col justify-center items-center py-16 gap-2"
          aria-labelledby="brand-story-title">
          <h2 id="brand-story-title" class="max-w-3xl text-3xl font-semibold leading-tight sm:text-4xl">
            Not mass market. Never ordinary.
          </h2>
          <p class="mt-4 max-w-2xl text-sm text-black/80 sm:text-base">
              Curated auctions for rare fashion, collectible pieces, and objects worth noticing.
          </p>
      </section>

          <div class="mb-12 rounded-2xl border border-black/10 py-6 md:px-6">
        <div class="mb-6 flex items-end justify-between gap-2">
          <div>
            <div class="flex items-center gap-2">
              <h1 class="text-2xl font-bold">Ending soon</h1>
              <TimerIcon class="h-5 w-5" />
            </div>
            <p class="mt-1 text-gray-700">
              Explore auctions that are about to end soon and place your bid before it's too late.
            </p>
          </div>
            <p class="mt-1 text-black hover:underline hover:text-gray-700 transition cursor-pointer">
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
                  :title="auction.title"
                  :price="auction.price"
                  :image-url="auction.imageUrl"
                  :time-left="auction.timeLeft"
                />
              </CarouselItem>
            </CarouselContent>

          <div class="mt-6 flex items-center justify-between">
            <p class="text-sm text-black/50">
              Scroll through ending soon auctions.
            </p>

            <div class="flex items-center gap-2">
              <CarouselPrevious
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border border-black/10 bg-white text-black shadow-sm hover:bg-black hover:text-white"
              />
              <CarouselNext
                class="static translate-x-0 translate-y-0 h-10 w-10 rounded-full border border-black/10 bg-white text-black shadow-sm hover:bg-black hover:text-white"
              />
            </div>
          </div>
        </Carousel>
      </div>
  </div>
  <div class="mt-20 text-sm text-black/60 max-w-3xl mx-auto text-center">
  <p>
    Coutera is an online auction marketplace focused on rare fashion, designer clothing, and collectible items.
    Users can browse auctions, place bids, and discover unique pieces from brands and independent sellers worldwide.
  </p>
</div>
</template>