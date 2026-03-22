<script setup lang="ts">
import Container from '@/components/ui/Container.vue';
import { RouterLink } from 'vue-router';
</script>

<template>
    <nav class="bg-white border-t">
    <Container>
        <div class="flex justify-between">
            <RouterLink to="/?category=clothing" class="text-gray-700 hover:text-gray-600 transition-colors duration-200 font-medium">
                Woman
            </RouterLink>
            <RouterLink to="/?category=shoes" class="text-gray-700 hover:text-gray-600 transition-colors duration-200 font-medium">
                Man
            </RouterLink>
            <RouterLink to="/?category=accessories" class="text-gray-700 hover:text-gray-600 transition-colors duration-200 font-medium">
                Accessories
            </RouterLink>
            <RouterLink to="/?category=bags" class="text-gray-700 hover:text-gray-600 transition-colors duration-200 font-medium">
                Brands
            </RouterLink>
            <RouterLink to="/?category=brands" class="text-gray-700 hover:text-gray-600 transition-colors duration-200 font-medium">
                Trending
            </RouterLink>
            <RouterLink to="/?category=brands" class="text-gray-700 hover:text-gray-600 transition-colors duration-200 font-medium">
                Ending Soon
            </RouterLink>
        </div>
    </Container>
</nav>
</template>