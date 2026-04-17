<script setup lang="ts">
import { ref } from 'vue'
import { RouterLink, useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'
import { storeToRefs } from 'pinia'

const router = useRouter()

import Container from '@/components/ui/Container.vue'
import Input from '@/components/ui/input/Input.vue'
import ModeToggle from '@/components/theme/ModeToggle.vue'
import Categories from '@/components/layout/CategoriesNavbar.vue'
import UserProfileIcon from '../ui/icons/UserProfileIcon.vue'

import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'


const searchQuery = ref('')
const authStore = useAuthStore()
const { user, isAuthenticated } = storeToRefs(authStore)

function handleLogout() {
  try
  {
    authStore.logout()
    router.push('/home')    
  }
  catch (error)
  {
    console.error('Logout failed:', error)
  }
}

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
  <header class="sticky top-0 z-50 border-b border-border bg-background/95 backdrop-blur-md">
    <Container>
      <div class="flex min-h-76px items-center justify-between gap-4 py-3">
        <div class="flex shrink-0 items-center">
          <RouterLink
            to="/home"
            class="text-2xl font-semibold tracking-tight text-foreground transition hover:opacity-80"
          >
            Coutera
          </RouterLink>
        </div>

        <div class="hidden flex-1 justify-center px-4 md:flex">
          <div class="w-full max-w-lg">
            <div class="relative">
              <Input
                v-model="searchQuery"
                @keydown="handleKeyPress"
                placeholder="Search lots, brands, and categories..."
                class="h-10 rounded-full border border-input bg-muted pl-10 pr-20 text-sm text-foreground placeholder:text-muted-foreground focus-visible:border-ring focus-visible:ring-0"
              />

              <div class="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3.5">
                <svg
                  class="h-4 w-4 text-muted-foreground"
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
                class="absolute right-1 top-1/2 -translate-y-1/2 rounded-full bg-primary px-3 py-1.5 text-xs font-medium text-primary-foreground transition hover:opacity-90"
              >
                Search
              </button>
            </div>
          </div>
        </div>

        <div class="flex shrink-0 items-center gap-2 sm:gap-3">
          <RouterLink
            v-if="!isAuthenticated"
            to="/login"
            class="rounded-full bg-primary px-4 py-2 text-sm font-medium text-primary-foreground transition hover:opacity-90 inline-flex"
          >
            Sell with us
          </RouterLink>

          <RouterLink
            v-if="isAuthenticated"
            to="/sell"
            class="rounded-full bg-primary px-4 py-2 text-sm font-medium text-primary-foreground transition hover:opacity-90 inline-flex"
          >
            Sell with us
          </RouterLink>

          <RouterLink
            v-if="!isAuthenticated"
            to="/login"
            class="rounded-full border border-border px-4 py-2 text-sm font-medium text-foreground transition hover:bg-accent hover:text-accent-foreground inline-flex"
          >
            Sign in
          </RouterLink>

          <div v-if="isAuthenticated" >
            <Select>
              <SelectTrigger class="h-10 w-full rounded-full border border-border bg-background px-3 text-sm">
                <SelectValue>
                  <UserProfileIcon />
                </SelectValue>
              </SelectTrigger>
              <SelectContent>
                <SelectItem>
                  <RouterLink :to="`/profile/${user?.username}`">
                    Profile
                  </RouterLink>
                </SelectItem>
                <SelectItem @click="handleLogout()">
                   Logout
                </SelectItem>
              </SelectContent>
            </Select>
          </div>

          <ModeToggle />
        </div>
      </div>

      <div class="pb-3 md:hidden">
        <div class="relative">
          <Input
            v-model="searchQuery"
            @keydown="handleKeyPress"
            placeholder="Search lots, brands..."
            class="h-11 rounded-full border border-input bg-muted pl-11 pr-4 text-sm text-foreground placeholder:text-muted-foreground focus-visible:border-ring focus-visible:ring-0"
          />

          <div class="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-4">
            <svg
              class="h-4 w-4 text-muted-foreground"
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