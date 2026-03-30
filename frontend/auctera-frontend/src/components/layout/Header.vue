<script setup lang="ts">
import { ref } from 'vue'
import { RouterLink } from 'vue-router'
import Container from '@/components/ui/Container.vue'
import Input from '@/components/ui/input/Input.vue'
import Categories from '@/components/layout/CategoriesNavbar.vue'

const searchQuery = ref('')

const handleSearch = () => {
  if (searchQuery.value.trim()) {
    console.log('Searching for:', searchQuery.value)
  }
}

const handleKeyPress = (event: KeyboardEvent) => {
  if (event.key === 'Enter') {
    handleSearch()
  }
}
</script>

<template>
  <header class="sticky top-0 z-50 border-b border-black/10 bg-white/95 backdrop-blur-md">
    <Container>
      <div class="flex min-h-76px items-center justify-between gap-4 py-3">
        <div class="flex shrink-0 items-center">
          <RouterLink
            to="/home"
            class="text-2xl font-semibold tracking-tight text-black transition hover:opacity-80"
          >
            Coutera
          </RouterLink>
        </div>

        <div class="hidden flex-1 justify-center px-4 md:flex">
          <div class="w-full max-w-2xl">
            <div class="relative">
              <Input
                v-model="searchQuery"
                @keydown="handleKeyPress"
                placeholder="Search lots, brands, and categories..."
                class="h-12 rounded-full border border-black/10 bg-neutral-100 pl-11 pr-24 text-sm text-black placeholder:text-black/40 focus-visible:border-black focus-visible:ring-0"
              />

              <div class="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-4">
                <svg
                  class="h-4 w-4 text-black/40"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"
                  />
                </svg>
              </div>

              <button
                @click="handleSearch"
                class="absolute right-1.5 top-1/2 -translate-y-1/2 rounded-full bg-black px-4 py-2 text-sm font-medium text-white transition hover:bg-black/90"
              >
                Search
              </button>
            </div>
          </div>
        </div>

        <div class="flex shrink-0 items-center gap-2 sm:gap-3">
          <RouterLink
            to="/login"
            class="hidden rounded-full border border-black/10 px-4 py-2 text-sm font-medium text-black transition hover:border-black hover:bg-neutral-100 sm:inline-flex"
          >
            Sign in
          </RouterLink>

          <RouterLink
            to="/register"
            class="rounded-full bg-black px-4 py-2 text-sm font-medium text-white transition hover:bg-black/90"
          >
            Sell with us
          </RouterLink>
        </div>
      </div>

      <div class="pb-3 md:hidden">
        <div class="relative">
          <Input
            v-model="searchQuery"
            @keydown="handleKeyPress"
            placeholder="Search lots, brands..."
            class="h-11 rounded-full border border-black/10 bg-neutral-100 pl-11 pr-4 text-sm text-black placeholder:text-black/40 focus-visible:border-black focus-visible:ring-0"
          />

          <div class="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-4">
            <svg
              class="h-4 w-4 text-black/40"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"
              />
            </svg>
          </div>
        </div>
      </div>
    </Container>

    <Categories />
  </header>
</template>