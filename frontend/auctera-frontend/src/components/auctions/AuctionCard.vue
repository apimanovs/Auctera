<script setup lang="ts"> 
defineProps<{
    title: string,
    seller: string,
    price: number,
    currency: string,
    imageUrl: string,
    timeLeft: string
}>()
</script>

<template>
    <div class="flex min-h-full flex-col justify-center">
        <div class="sm:w-full sm:max-w-md">
            <div class="rounded-2xl border border-gray-100 bg-white p-8 shadow-xl">        
                <img
                    :src="imageUrl"
                    alt="Auction lot image"
                    class="mb-4 h-40 w-full rounded-xl object-cover"
                />
                <div class="text-2xl font-semibold">
                    {{ title }}
                </div>
                <div class="text-lg font-semibold">
                    {{ seller }}
                </div>
                <div class="text-s">
                    {{ price }}
                </div>
                <div class="mt-2 text-sm text-gray-500">
                    {{ timeLeft }}
                </div>
            </div>
        </div>
    </div>
</template>