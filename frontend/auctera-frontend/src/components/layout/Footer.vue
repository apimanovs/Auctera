<script setup lang="ts">
import Container from '@/components/ui/Container.vue';
import { RouterLink } from 'vue-router';

const currentYear = new Date().getFullYear()
</script>

<template>
    <footer class="border-t py-8">
        <Container>
            <div class="flex flex-col md:flex-row justify-between items-center">
                <div class="mb-4 md:mb-0">
                    <p class="text-sm text-black/60">© {{ currentYear }} Coutera</p>
                </div>
                <div class="flex flex-wrap justify-center md:justify-end space-x-6 mb-4 md:mb-0">
                    <RouterLink to="/about" class="text-sm text-black/60 hover:text-gray-600 transition-colors duration-200">
                        About
                    </RouterLink>
                    <RouterLink to="/terms" class="text-sm text-black/60 hover:text-gray-600 transition-colors duration-200">
                        Terms
                    </RouterLink>
                    <RouterLink to="/privacy-policy" class="text-sm text-black/60 hover:text-gray-600 transition-colors duration-200">
                        Privacy Policy
                    </RouterLink>
                </div>
                <!-- <div class="flex items-center space-x-4">
                    <RouterLink to="/how-to-sell" class="text-sm text-black/60 hover:text-gray-600 transition-colors duration-200">
                        How to Sell
                    </RouterLink>
                </div> -->
            </div>
        </Container>
    </footer>
</template>