<script setup lang="ts">
import Container from '@/components/ui/Container.vue';
import { RouterLink } from 'vue-router';
</script>

<template>
    <nav class="border-b bg-white shadow-sm">
    <Container>
        <div class="flex justify-between">
            <RouterLink to="/?category=clothing" class="text-gray-700 hover:text-gray-600 transition-colors duration-200 font-medium">
                Clothing
            </RouterLink>
            <RouterLink to="/?category=shoes" class="text-gray-700 hover:text-gray-600 transition-colors duration-200 font-medium">
                Shoes
            </RouterLink>
            <RouterLink to="/?category=accessories" class="text-gray-700 hover:text-gray-600 transition-colors duration-200 font-medium">
                Accessories
            </RouterLink>
            <RouterLink to="/?category=bags" class="text-gray-700 hover:text-gray-600 transition-colors duration-200 font-medium">
                Bags
            </RouterLink>
            <RouterLink to="/?category=brands" class="text-gray-700 hover:text-gray-600 transition-colors duration-200 font-medium">
                Brands
            </RouterLink>
        </div>
    </Container>
</nav>
</template>