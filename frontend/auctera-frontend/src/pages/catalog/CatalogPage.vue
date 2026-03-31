<script setup lang="ts">
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

    <div class="grid gap-8 lg:grid-cols-[280px_minmax(0,1fr)]">
      <aside class="h-fit rounded-[28px] border border-black/10 bg-white p-6">
        <div>
          <p class="text-[11px] uppercase tracking-[0.22em] text-black/40">
            Filters
          </p>
          <h2 class="mt-2 text-2xl font-semibold tracking-tight text-black">
            Refine
          </h2>
        </div>

        <div class="mt-6 space-y-6">
          <div>
            <label class="mb-2 block text-sm font-medium text-black">Category</label>
            <select v-model="selectedCategory" class="h-11 w-full rounded-2xl border border-black/10 bg-neutral-100 px-4 text-sm text-black outline-none">
              <option v-for="category in categories" :key="category" :value="category">
                {{ category }}
              </option>
            </select>
          </div>

          <div>
            <label class="mb-2 block text-sm font-medium text-black">Brand</label>
            <select v-model="selectedBrand" class="h-11 w-full rounded-2xl border border-black/10 bg-neutral-100 px-4 text-sm text-black outline-none">
              <option v-for="brand in brands" :key="brand" :value="brand">
                {{ brand }}
              </option>
            </select>
          </div>

          <div>
            <label class="mb-2 block text-sm font-medium text-black">Gender</label>
            <select v-model="selectedGender" class="h-11 w-full rounded-2xl border border-black/10 bg-neutral-100 px-4 text-sm text-black outline-none">
              <option v-for="gender in genders" :key="gender" :value="gender">
                {{ gender }}
              </option>
            </select>
          </div>

          <div>
            <label class="mb-2 block text-sm font-medium text-black">Condition</label>
            <select v-model="selectedCondition" class="h-11 w-full rounded-2xl border border-black/10 bg-neutral-100 px-4 text-sm text-black outline-none">
              <option v-for="condition in conditions" :key="condition" :value="condition">
                {{ condition }}
              </option>
            </select>
          </div>
        </div>
      </aside>

      <div>
        <div class="mb-6 flex flex-col gap-4 rounded-[28px] border border-black/10 bg-white p-5 sm:flex-row sm:items-center sm:justify-between">
          <div>
            <p class="text-sm text-black/50">
              {{ filteredLots.length }} lots found
            </p>
          </div>

          <div class="flex items-center gap-3">
            <label class="text-sm text-black/60">Sort by</label>
            <select v-model="selectedSort" class="h-11 rounded-2xl border border-black/10 bg-neutral-100 px-4 text-sm text-black outline-none">
              <option v-for="option in sortOptions" :key="option" :value="option">
                {{ option }}
              </option>
            </select>
          </div>
        </div>

        <div class="grid gap-5 sm:grid-cols-2 xl:grid-cols-3">
          <article
            v-for="lot in filteredLots"
            :key="lot.id"
            class="group overflow-hidden rounded-[28px] border border-black/10 bg-white"
          >
            <div class="overflow-hidden bg-neutral-100">
              <img
                :src="lot.image"
                :alt="lot.title"
                class="h-80 w-full object-cover transition duration-300 group-hover:scale-[1.02]"
              />
            </div>

            <div class="p-5">
              <p class="text-[11px] uppercase tracking-[0.2em] text-black/40">
                {{ lot.brand }}
              </p>

              <h3 class="mt-2 line-clamp-2 text-lg font-medium text-black">
                {{ lot.title }}
              </h3>

              <div class="mt-4 flex items-center justify-between gap-3">
                <span class="text-sm font-semibold text-black">€{{ lot.price }}</span>
                <span class="text-sm text-black/50">{{ lot.timeLeft }}</span>
              </div>

              <div class="mt-4 flex flex-wrap gap-2">
                <span class="rounded-full bg-neutral-100 px-3 py-1 text-xs text-black/65">
                  {{ lot.category }}
                </span>
                <span class="rounded-full bg-neutral-100 px-3 py-1 text-xs text-black/65">
                  {{ lot.condition }}
                </span>
              </div>
            </div>
          </article>
        </div>
      </div>
    </div>
  </div>
</template>