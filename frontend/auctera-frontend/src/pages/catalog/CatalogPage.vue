<script setup lang="ts">
import AuctionCard from '@/components/auctions/AuctionCard.vue'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'

import { computed, ref } from 'vue'

const selectedCategory = ref('All')
const selectedBrand = ref('All')
const selectedGender = ref('All')
const selectedCondition = ref('All')
const selectedSort = ref('Newest')

const categories = ['All', 'Designer', 'Archive', 'Accessories', 'Outerwear', 'Footwear']
const brands = ['All', 'Rick Owens', 'Prada', 'Balenciaga', 'Maison Margiela', 'Helmut Lang']
const genders = ['All', 'Women', 'Men', 'Unisex']
const conditions = ['All', 'New', 'Very good', 'Good']
const sortOptions = ['Newest', 'Ending soon', 'Price: low to high', 'Price: high to low']

const lots = ref([
  {
    id: 1,
    title: 'Rick Owens Jumbo Lace Puffer',
    brand: 'Rick Owens',
    category: 'Outerwear',
    gender: 'Men',
    condition: 'Very good',
    price: 820,
    timeLeft: '1 day 12 hours',
    image:
      'https://media-photos.depop.com/b1/40086058/3512274199_d42fb73add7043d586f1be825bfb1f68/P0.jpg',
  },
  {
    id: 2,
    title: 'Maison Margiela Archive Knit',
    brand: 'Maison Margiela',
    category: 'Archive',
    gender: 'Unisex',
    condition: 'Good',
    price: 310,
    timeLeft: '2 days 4 hours',
    image:
      'https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?q=80&w=1200&auto=format&fit=crop',
  },
  {
    id: 3,
    title: 'Prada Leather Derby Shoes',
    brand: 'Prada',
    category: 'Footwear',
    gender: 'Men',
    condition: 'Very good',
    price: 420,
    timeLeft: '8 hours',
    image:
      'https://images.unsplash.com/photo-1542291026-7eec264c27ff?q=80&w=1200&auto=format&fit=crop',
  },
  {
    id: 4,
    title: 'Balenciaga Leather Bag',
    brand: 'Balenciaga',
    category: 'Accessories',
    gender: 'Women',
    condition: 'New',
    price: 950,
    timeLeft: '3 days 6 hours',
    image:
      'https://images.unsplash.com/photo-1584917865442-de89df76afd3?q=80&w=1200&auto=format&fit=crop',
  },
  {
    id: 5,
    title: 'Helmut Lang Utility Jacket',
    brand: 'Helmut Lang',
    category: 'Designer',
    gender: 'Men',
    condition: 'Good',
    price: 280,
    timeLeft: '12 hours',
    image:
      'https://images.unsplash.com/photo-1515886657613-9f3515b0c78f?q=80&w=1200&auto=format&fit=crop',
  },
  {
    id: 6,
    title: 'Archive Silver Chain',
    brand: 'All',
    category: 'Accessories',
    gender: 'Unisex',
    condition: 'Very good',
    price: 140,
    timeLeft: '1 day 3 hours',
    image:
      'https://images.unsplash.com/photo-1617038260897-41a1f14a8ca1?q=80&w=1200&auto=format&fit=crop',
  },
])

const filteredLots = computed(() => {
  let result = [...lots.value]

  if (selectedCategory.value !== 'All') {
    result = result.filter((lot) => lot.category === selectedCategory.value)
  }

  if (selectedBrand.value !== 'All') {
    result = result.filter((lot) => lot.brand === selectedBrand.value)
  }

  if (selectedGender.value !== 'All') {
    result = result.filter((lot) => lot.gender === selectedGender.value)
  }

  if (selectedCondition.value !== 'All') {
    result = result.filter((lot) => lot.condition === selectedCondition.value)
  }

  if (selectedSort.value === 'Price: low to high') {
    result.sort((a, b) => a.price - b.price)
  }

  if (selectedSort.value === 'Price: high to low') {
    result.sort((a, b) => b.price - a.price)
  }

  return result
})
</script>

<template>
  <div class="mx-auto max-w-7xl px-4 py-8 sm:px-6 lg:px-8">
    <div class="space-y-8">
      <section class="rounded-[28px] border bg-background p-5 sm:p-6">
        <div class="flex flex-col gap-6">
          <div class="flex flex-col gap-2 sm:flex-row sm:items-end sm:justify-between">
            <div>
              <p class="text-[11px] uppercase tracking-[0.22em] text-foreground/40">
                Catalog
              </p>
              <h2 class="mt-1 text-2xl font-semibold tracking-tight text-foreground">
                Discover pieces
              </h2>
            </div>

            <p class="text-sm text-foreground/60">
              {{ filteredLots.length }} items
            </p>
          </div>

          <div class="grid gap-4 sm:grid-cols-2 xl:grid-cols-5">
            <!-- Category -->
            <div>
              <label class="mb-2 block text-sm font-medium text-foreground">
                Category
              </label>
              <Select v-model="selectedCategory">
                <SelectTrigger class="h-11 w-full rounded-2xl border border-foreground/20 bg-background px-4 text-sm">
                  <SelectValue placeholder="Select category" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem
                    v-for="category in categories"
                    :key="category"
                    :value="category"
                  >
                    {{ category }}
                  </SelectItem>
                </SelectContent>
              </Select>
            </div>

            <!-- Brand -->
            <div>
              <label class="mb-2 block text-sm font-medium text-foreground">
                Brand
              </label>
              <Select v-model="selectedBrand">
                <SelectTrigger class="h-11 w-full rounded-2xl border border-foreground/20 bg-background px-4 text-sm">
                  <SelectValue placeholder="Select brand" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem
                    v-for="brand in brands"
                    :key="brand"
                    :value="brand"
                  >
                    {{ brand }}
                  </SelectItem>
                </SelectContent>
              </Select>
            </div>

            <!-- Gender -->
            <div>
              <label class="mb-2 block text-sm font-medium text-foreground">
                Gender
              </label>
              <Select v-model="selectedGender">
                <SelectTrigger class="h-11 w-full rounded-2xl border border-foreground/20 bg-background px-4 text-sm">
                  <SelectValue placeholder="Select gender" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem
                    v-for="gender in genders"
                    :key="gender"
                    :value="gender"
                  >
                    {{ gender }}
                  </SelectItem>
                </SelectContent>
              </Select>
            </div>

            <!-- Condition -->
            <div>
              <label class="mb-2 block text-sm font-medium text-foreground">
                Condition
              </label>
              <Select v-model="selectedCondition">
                <SelectTrigger class="h-11 w-full rounded-2xl border border-foreground/20 bg-background px-4 text-sm">
                  <SelectValue placeholder="Select condition" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem
                    v-for="condition in conditions"
                    :key="condition"
                    :value="condition"
                  >
                    {{ condition }}
                  </SelectItem>
                </SelectContent>
              </Select>
            </div>

            <!-- Sort -->
            <div>
              <label class="mb-2 block text-sm font-medium text-foreground">
                Sort by
              </label>
              <Select v-model="selectedSort">
                <SelectTrigger class="h-11 w-full rounded-2xl border border-foreground/20 bg-background px-4 text-sm">
                  <SelectValue placeholder="Sort lots" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem
                    v-for="option in sortOptions"
                    :key="option"
                    :value="option"
                  >
                    {{ option }}
                  </SelectItem>
                </SelectContent>
              </Select>
            </div>
          </div>
        </div>
      </section>

      <section>
        <div class="grid gap-5 sm:grid-cols-2 xl:grid-cols-4">
          <article
            v-for="lot in filteredLots"
            :key="lot.id"
            class="group overflow-hidden"
          >
            <AuctionCard
              :brand="lot.brand"
              :title="lot.title"
              :price="lot.price"
              :image-url="lot.image"
              :time-left="lot.timeLeft"
            />
          </article>
        </div>
      </section>
    </div>
  </div>
</template>