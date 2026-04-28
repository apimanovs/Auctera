<script setup lang="ts">

defineProps<{
    brand: string
    title: string
    seller?: string
    price: number
    currency?: string
    imageUrl: string | string[]
    timeLeft: string
    location?: string
    year?: number | null
}>()
</script>

<template>
    <div class="w-full h-full bg-background rounded-lg border-foreground/20 border">
        <div class="flex h-full flex-col">
            
            <!-- Image Section -->
            <div class="relative h-64 overflow-hidden rounded-lg bg-background">
                <img
                    v-if="typeof imageUrl === 'string' && imageUrl"
                    :src="imageUrl"
                    alt="Auction lot image"
                    class="h-full w-full object-cover"
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
                <h3 class="mb-3 min-h-[2.5rem] line-clamp-2 text-lg font-medium text-foreground">
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
                <div class="mt-3 space-y-1 text-sm text-foreground/60">
                    <span class="block">{{ timeLeft }}</span>
                    <span v-if="location" class="block truncate">{{ location }}</span>
                    <span v-if="year" class="block">{{ year }}</span>
                </div>
            </div>
        </div>
    </div>
</template>
