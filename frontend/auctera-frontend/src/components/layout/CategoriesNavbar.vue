<script setup lang="ts">
import Container from '@/components/ui/Container.vue'
import { RouterLink } from 'vue-router'

const navItems = [
  { label: 'Women', to: '/auctions?gender=1' },
  { label: 'Men', to: '/auctions?gender=2' },
  { label: 'Accessories', to: '/auctions?category=4' },
  { label: 'Brands', to: '/auctions' },
  { label: 'Trending', to: '/auctions?status=Published' },
  { label: 'Ending Soon', to: '/auctions?status=Listed' },
]
</script>

<template>
  <nav class="border-y border-border/10 bg-background/95 backdrop-blur-md">
    <Container>
      <div class="overflow-x-auto scrollbar-none">
        <div class="flex min-w-max items-center gap-12 py-3 sm:justify-center">
          <RouterLink
            v-for="item in navItems"
            :key="item.label"
            :to="item.to"
            class="relative whitespace-nowrap text-sm font-medium tracking-[0.01em] text-foreground transition hover:text-foreground/80"
          >
            <span>{{ item.label }}</span>
            <span class="mt-1 block h-px w-0 transition-all duration-300 hover:w-full" />
          </RouterLink>
        </div>
      </div>
    </Container>
  </nav>
</template>
