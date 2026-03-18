<script setup lang="ts">
import { ref } from 'vue'
import Container from '@/components/ui/Container.vue'
import Input from '@/components/ui/input/Input.vue'
import Categories from '@/components/layout/CategoriesNavbar.vue'
import ProfileIcon from '@/components/ui/icons/ProfileIcon.vue'

const searchQuery = ref('')

const handleSearch = () => {
  if (searchQuery.value.trim()) {
    // TODO: Implement search logic
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
  <header class="border-b sticky top-0 bg-white z-50">
    <Container>
      <div class="h-16 flex items-center justify-between gap-4">
        <!-- Logo -->
        <div id="logo-container" class="flex-shrink-0">
           <a href="/" class="text-xl font-bold">Coutera</a>
        </div>

        <!-- Search Bar -->
        <div class="flex-1 max-w-md">
          <div class="relative">
            <Input
              v-model="searchQuery"
              @keydown="handleKeyPress"
              placeholder="Search auctions..."
              class="pl-10 pr-4"
            />
            <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <svg class="h-4 w-4 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
              </svg>
            </div>
          </div>
        </div>

        <!-- Right Icons -->
        <div class="flex items-center gap-2">
          <ProfileIcon />
        </div>
      </div>
    </Container>
  </header>
  <Categories />
</template>