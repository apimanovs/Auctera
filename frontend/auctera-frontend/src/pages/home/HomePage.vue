<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { Auction } from '@/types/auction'
import { getAuctions } from '@/app/services/auctionService'
import AuctionCard from '@/components/auctions/AuctionCard.vue'

const auctions = ref<Auction[]>([])
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
  <div class="p-6">
    <h1 class="text-2xl font-bold">Home page</h1>
    
    <div class="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3">
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
</template>