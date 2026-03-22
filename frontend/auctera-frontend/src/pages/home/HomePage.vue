<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { Auction } from '@/types/auction'
import { getAuctions } from '@/app/services/auctionService'
import AuctionCard from '@/components/auctions/AuctionCard.vue'
import { Card, CardContent } from '@/components/ui/card'
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
    imageUrl: 'https://media-photos.depop.com/b1/36719830/3498593736_9131744e9156480d90e996a6dcb6ec67/P0.jpg',
    timeLeft: '2 дня 14 часов',
  },
  {
    id: 2,
    title: 'Original Oil Painting by Unknown Artist',
    price: 320,
    imageUrl: 'https://media-photos.depop.com/b1/448068812/3474488748_5e0e9243711c4cbcb6dce7afdff37d6d/P0.jpg',
    timeLeft: '3 дня 8 часов',
  },
  {
    id: 3,
    title: 'Rolex Submariner Watch Limited Edition',
    price: 5400,
    imageUrl: 'https://media-photos.depop.com/r1/341927691/3481391174_2e97b19297a44aae8e7e46d6e737e530/P6.jpg',
    timeLeft: '1 день 6 часов',
  },
  {
    id: 4,
    title: 'Antique Wooden Cabinet with Mirror',
    price: 280,
    imageUrl: 'https://media-photos.depop.com/b1/20411984/2751785262_45ca34b51be54edbad51742f72c8c676/P0.jpg',
    timeLeft: '4 дня 20 часов',
  },
  {
    id: 5,
    title: 'Vintage Pearl Necklace - 18K Gold Chain',
    price: 650,
    imageUrl: 'https://media-photos.depop.com/b1/45498419/3517277733_6056232abec546e987925ec83630f810/P0.jpg',
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
    imageUrl: 'https://media-photos.depop.com/b1/45498419/3517277733_6056232abec546e987925ec83630f810/P0.jpg',
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
  <div class="mb-12 rounded-2xl border border-black/10 py-6 md:px-6">
    <div class="mb-6 flex items-end justify-between gap-2">
      <div>
        <h1 class="text-2xl font-bold">Trending Auctions</h1>
        <p class="mt-1 text-gray-700">
          Discover the most popular auctions right now.
        </p>
      </div>
    </div>

    <Carousel
      :opts="{ loop: true, align: 'start', slidesToScroll: 1 }"
      class="w-full"
    >
        <CarouselContent class="-ml-3">
          <CarouselItem
            v-for="auction in auctions"
            :key="auction.id"
            class="pl-3 basis-1/2 sm:basis-1/2 md:basis-1/3 lg:basis-1/6 min-w-0"
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

    <div class="mb-6">
      <h1 class="text-2xl font-bold">New Listings</h1>
      <p class="text-gray-700 mb-6">
        Discover the most new auctions right now.
      </p>

      <div class="grid grid-cols-2 gap-2 sm:grid-cols-2 lg:grid-cols-6">
        <AuctionCard
          v-for="auction in auctions"
          :key="auction.id"
          :title="auction.title"
          :price="auction.price"
          :image-url="auction.imageUrl"
          :time-left="auction.timeLeft"
        />
      </div>
    </div>

      <section
          class="my-16 overflow-hidden text-black flex flex-col justify-center items-center py-16 gap-2"
          aria-labelledby="brand-story-title"
        ><h2 id="brand-story-title" class="max-w-3xl text-3xl font-semibold leading-tight sm:text-4xl">
            Not mass market. Never ordinary.
          </h2>
          <p class="mt-4 max-w-2xl text-sm text-black/80 sm:text-base">
              Curated auctions for rare fashion, collectible pieces, and objects worth noticing.
          </p>
      </section>

    <section class="mb-12">
      <h2 class="text-2xl font-bold">Ending Soon</h2>
      <p class="mb-6 text-black/60">
        Explore auctions that are closing soon.
      </p>

      <div class="grid grid-cols-2 gap-8 sm:grid-cols-2 lg:grid-cols-4">
        <AuctionCard
          v-for="auction in auctions"
          :key="auction.id"
          :title="auction.title"
          :price="auction.price"
          :image-url="auction.imageUrl"
          :time-left="auction.timeLeft"
        />
      </div>
    </section>
  </div>
  <div class="mt-20 text-sm text-black/60 max-w-3xl mx-auto text-center">
  <p>
    Coutera is an online auction marketplace focused on rare fashion, designer clothing, and collectible items.
    Users can browse auctions, place bids, and discover unique pieces from brands and independent sellers worldwide.
  </p>
</div>
</template>