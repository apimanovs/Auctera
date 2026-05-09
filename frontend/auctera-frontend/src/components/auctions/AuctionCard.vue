<script setup lang="ts">

defineProps<{
    brand: string
    title: string
    seller?: string
    price: number
    currency?: string
    imageUrl: string | string[]
    timeLeft: string
}>()
</script>

<template>
    <div class="w-full h-full bg-background rounded-lg border-foreground/20 border">
        <div class="flex h-full flex-col">
            
            <!-- Image Section -->
            <div class="relative h-64 overflow-hidden rounded-lg bg-background">
                <img
                    v-if="typeof imageUrl === 'string'"
                    :src="imageUrl"
                    alt="Auction lot image"
                    class="h-full w-full"
                />
                <div v-else class="flex h-full items-center justify-center text-foreground text-sm">
                    No image
                </div>
            </div>

            <!-- Info Section -->
            <div class="p-3 flex flex-1 flex-col justify-between">
                <p class="text-[11px] uppercase tracking-[0.2em] text-foreground/70">
                    {{ brand }}
                </p>
                <!-- Title -->
                <h3 class="mb-3 line-clamp-2 text-lg font-medium text-foreground min-h-2.5rem">
                    {{ title }}
                </h3>

                <!-- Price -->
                <p class="text-sm text-foreground/60">Current bid</p>
                <div class="mt-1 flex items-center justify-between gap-3">
                    <div class="flex items-baseline gap-1">
                        <span class="text-xl font-semibold text-foreground">{{ price }}</span>
                        <span v-if="currency" class="text-sm text-foreground/60">{{ currency }}</span>
                    </div>
                </div>

                <!-- Time Left -->
                <span class="text-sm text-foreground/60">{{ timeLeft }}</span>
            </div>
        </div>
    </div>
</template>