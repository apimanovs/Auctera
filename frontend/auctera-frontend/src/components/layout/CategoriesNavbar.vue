<script setup lang="ts">
import Container from '@/components/ui/Container.vue'
import { RouterLink } from 'vue-router'

const navItems = [
  { label: 'Women', to: '/?category=women' },
  { label: 'Men', to: '/?category=men' },
  { label: 'Accessories', to: '/?category=accessories' },
  { label: 'Brands', to: '/?category=brands' },
  { label: 'Trending', to: '/?sort=trending' },
  { label: 'Ending Soon', to: '/?sort=ending-soon' },
]
</script>

<template>
  <nav class="border-y border-black/10 bg-white">
    <Container>
      <div class="overflow-x-auto scrollbar-none">
        <div class="flex min-w-max items-center gap-12 py-3 sm:justify-center">
          <RouterLink
            v-for="item in navItems"
            :key="item.label"
            :to="item.to"
            class="relative whitespace-nowrap text-sm font-medium tracking-[0.01em] text-black/60 transition hover:text-black"
          >
            <span>{{ item.label }}</span>
            <span class="mt-1 block h-px w-0 bg-black transition-all duration-300 hover:w-full" />
          </RouterLink>
        </div>
      </div>
    </Container>
  </nav>
</template>