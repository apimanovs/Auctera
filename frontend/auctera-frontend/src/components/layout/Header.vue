<script setup lang="ts">
import Container from '@/components/ui/Container.vue'
import Categories from '@/components/layout/CategoriesNavbar.vue'
</script>

<template>
  <header class="border-b">
    <Container>
      <div class="h-16 flex items-center justify-between">
        <div id="logo-container">
           <a href="/">Logo</a>
        </div>
        <div>
            Search
        </div>
        <div>
            Profile
        </div>
      </div>
    </Container>
  </header>
  <Categories />
</template>