<script setup lang="ts">
import { computed, ref, onMounted } from 'vue'
import type { Auction } from '@/types/auction'
import { getAuctions } from '@/app/services/auctionService'
import AuctionCard from '@/components/auctions/AuctionCard.vue'

const auctions = ref<Auction[]>([
  {
    id: 1,
    title: 'Vintage Leather Sofa - Pristine Condition',
    price: 450,
    imageUrl: 'https://images.unsplash.com/photo-1555597756-5af3a85d28e5?w=500&h=500&fit=crop',
    timeLeft: '2 дня 14 часов',
  },
  {
    id: 2,
    title: 'Original Oil Painting by Unknown Artist',
    price: 320,
    imageUrl: 'https://images.unsplash.com/photo-1561214115-6e846fe7f5af?w=500&h=500&fit=crop',
    timeLeft: '3 дня 8 часов',
  },
  {
    id: 3,
    title: 'Rolex Submariner Watch Limited Edition',
    price: 5400,
    imageUrl: 'https://images.unsplash.com/photo-1523170335684-f042f1b7f4e4?w=500&h=500&fit=crop',
    timeLeft: '1 день 6 часов',
  },
  {
    id: 4,
    title: 'Antique Wooden Cabinet with Mirror',
    price: 280,
    imageUrl: 'https://images.unsplash.com/photo-1531684770318-e90b4b5c8666?w=500&h=500&fit=crop',
    timeLeft: '4 дня 20 часов',
  },
  {
    id: 5,
    title: 'Vintage Pearl Necklace - 18K Gold Chain',
    price: 650,
    imageUrl: 'https://images.unsplash.com/photo-1535542550433-bbde0fc50dc8?w=500&h=500&fit=crop',
    timeLeft: '5 дней 12 часов',
  },
  {
    id: 6,
    title: 'Brand New Gaming Laptop RTX 4090',
    price: 2100,
    imageUrl: 'https://images.unsplash.com/photo-1588872657840-3dbc744d4e2d?w=500&h=500&fit=crop',
    timeLeft: '18 часов 30 минут',
  },
])
const loading = ref(false)
const error = ref('')

const currentPage = ref(1)
const pageSize = ref(8)

const totalItems = computed(() => auctions.value.length)

const paginatedAuctions = computed(() => {
  const start = (currentPage.value - 1) * pageSize.value
  return auctions.value.slice(start, start + pageSize.value)
})

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
    <div class="flex justify-center items-center flex-col text-center mb-12">
        <h1 class="text-2xl font-bold">Coutera</h1>
        <p class="text-gray-700">
            Welcome to Coutera, your premier online auction platform.
            Every bid matters.
            Every second counts.
        </p>
        <p class="text-gray-700">
            Every bid matters.
            Every second counts.
        </p>
    </div>

    <div class="mb-6">
        <h1 class="text-2xl font-bold">Tranding Auctions</h1>
        <p class="text-gray-700 mb-6">
          Discover the most popular auctions right now.
        </p>

        <div class="grid grid-cols-1 gap-8 sm:grid-cols-2 lg:grid-cols-4">
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

    <div class="flex justify-center items-center flex-col text-center mb-12">
        <h1 class="text-2xl font-bold">Coutera</h1>
        <p class="text-gray-700">
          Coutera is not a store. 
          It’s a marketplace of rare finds.
        </p>
    </div>

    <div class="mb-6">
      <h1 class="text-2xl font-bold">New Listings</h1>
      <p class="text-gray-700 mb-6">
        Discover the most new auctions right now.
      </p>

      <div class="grid grid-cols-1 gap-8 sm:grid-cols-2 lg:grid-cols-4">
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

    <div class="flex justify-center items-center flex-col text-center mb-12">
      <h1 class="text-2xl font-bold">Coutera</h1>
      <p class="text-gray-700">
        Coutera is not a store. 
        It’s a marketplace of rare finds.
      </p>
    </div>

    <div class="mb-6">
      <h1 class="text-2xl font-bold">Ending Soon</h1>
      <p class="text-gray-700 mb-6">
        Discover the auctions ending soon.
      </p>

      <div class="grid grid-cols-1 gap-8 sm:grid-cols-2 lg:grid-cols-4">
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
  </div>
</template>