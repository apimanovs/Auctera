<script setup lang="ts">

defineProps<{
    title: string
    seller?: string
    price: number
    currency?: string
    imageUrl: string | string[]
    timeLeft: string
}>()
</script>

<template>
    <div class="w-full h-full bg-white">
        <div class="flex h-full flex-col">
            
            <!-- Image Section -->
            <div class="relative h-64 overflow-hidden rounded-lg bg-gray-50">
                <img
                    v-if="typeof imageUrl === 'string'"
                    :src="imageUrl"
                    alt="Auction lot image"
                    class="object-coverh h-full w-full"
                />
                <div v-else class="flex h-full items-center justify-center text-gray-300 text-sm">
                    No image
                </div>
            </div>

            <!-- Info Section -->
            <div class="p-3 flex flex-1 flex-col justify-between">
                <!-- Title -->
                <h3 class="mb-3 line-clamp-2 text-sm font-medium text-gray-900 min-h-2.5rem">
                    {{ title }}
                </h3>

                <!-- Price -->
                <div>
                    <p class="text-xs text-gray-500 mb-1">Current bid</p>
                    <div class="flex items-baseline gap-1">
                        <span class="text-lg font-semibold text-gray-900">{{ price }}</span>
                        <span v-if="currency" class="text-xs text-gray-600">{{ currency }}</span>
                    </div>
                </div>

                <!-- Time Left -->
                <div class="text-xs text-gray-500">
                    {{ timeLeft }}
                </div>
            </div>
        </div>
    </div>
</template>