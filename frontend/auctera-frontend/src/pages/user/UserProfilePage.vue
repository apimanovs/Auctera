<script setup lang="ts">
import { computed, onMounted, ref, watch } from 'vue'
import { RouterLink, useRoute } from 'vue-router'

import { getApiErrorMessage } from '@/app/helpers/apiError'
import { profileService } from '@/app/services/profileService'
import type { UserProfileDto, UserProfileListingDto } from '@/types/userProfile'

const route = useRoute()

const profile = ref<UserProfileDto | null>(null)
const isLoading = ref(true)
const errorMessage = ref('')

const username = computed(() => String(route.params.username ?? ''))
const activeListings = computed(() => profile.value?.activeListings ?? [])
const soldListings = computed(() => profile.value?.soldListings ?? [])
const location = computed(() =>
  [profile.value?.city?.trim(), profile.value?.country?.trim()].filter(Boolean).join(', '),
)

function formatPrice(amount: number, currency: string): string {
  return new Intl.NumberFormat('en', {
    style: 'currency',
    currency,
    maximumFractionDigits: 0,
  }).format(amount)
}

const loadProfile = async () => {
  if (!username.value) return

  try {
    isLoading.value = true
    errorMessage.value = ''
    profile.value = await profileService.getUserProfile(username.value)
  } catch (error) {
    errorMessage.value = getApiErrorMessage(error, 'Failed to load profile.')
  } finally {
    isLoading.value = false
  }
}

const statusLabel = (listing: UserProfileListingDto) => String(listing.status ?? 'Active')

onMounted(loadProfile)
watch(username, loadProfile)
</script>

<template>
  <div class="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
    <div v-if="isLoading" class="rounded-[24px] border bg-background p-6 text-sm text-foreground/70">
      Loading profile...
    </div>

    <div v-else-if="errorMessage" class="rounded-[24px] border border-red-500/30 bg-red-500/10 p-6 text-sm text-red-300">
      {{ errorMessage }}
    </div>

    <template v-else-if="profile">
      <section class="rounded-[28px] border bg-background px-6 py-8 md:px-8 md:py-10">
        <div class="flex flex-col gap-4 md:flex-row md:items-end md:justify-between">
          <div>
            <p class="text-[11px] uppercase tracking-[0.22em] text-foreground/40">
              Seller profile
            </p>
            <h1 class="mt-2 text-3xl font-semibold tracking-tight text-foreground sm:text-4xl">
              {{ profile.name }}
            </h1>
            <div class="mt-4 flex flex-wrap gap-2 text-sm text-foreground/70">
              <span class="rounded-full border bg-background px-3 py-1">
                @{{ profile.username }}
              </span>
              <span class="rounded-full border bg-background px-3 py-1">
                {{ location || 'Location not specified' }}
              </span>
            </div>
          </div>
        </div>
      </section>

      <section class="mt-6 grid gap-4 sm:grid-cols-3">
        <div class="rounded-[24px] border bg-background p-5">
          <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">Active listings</p>
          <p class="mt-3 text-3xl font-semibold text-foreground">{{ profile.stats.activeListingsCount }}</p>
        </div>

        <div class="rounded-[24px] border bg-background p-5">
          <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">Sold items</p>
          <p class="mt-3 text-3xl font-semibold text-foreground">{{ profile.stats.soldItemsCount }}</p>
        </div>

        <div class="rounded-[24px] border bg-background p-5">
          <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">Bids placed</p>
          <p class="mt-3 text-3xl font-semibold text-foreground">{{ profile.stats.bidsPlaced }}</p>
        </div>
      </section>

      <section class="mt-8 grid gap-6 lg:grid-cols-2">
        <div class="rounded-[28px] border bg-background px-6 py-6">
          <div>
            <p class="text-[11px] uppercase tracking-[0.22em] text-foreground/40">
              Live inventory
            </p>
            <h2 class="mt-2 text-2xl font-semibold tracking-tight text-foreground">
              Active listings
            </h2>
          </div>

          <div v-if="activeListings.length" class="mt-6 grid gap-4 sm:grid-cols-2">
            <RouterLink
              v-for="listing in activeListings"
              :key="listing.lotId"
              :to="`/lots/${listing.lotId}`"
              class="group overflow-hidden rounded-[24px] border bg-background"
            >
              <div class="aspect-[4/5] overflow-hidden bg-foreground/5">
                <img
                  v-if="listing.thumbnailUrl"
                  :src="listing.thumbnailUrl"
                  :alt="listing.title"
                  class="h-full w-full object-cover transition duration-300 group-hover:scale-[1.02]"
                />
                <div v-else class="flex h-full items-center justify-center text-sm text-foreground/50">
                  No image
                </div>
              </div>

              <div class="p-4">
                <h3 class="line-clamp-2 text-base font-medium text-foreground">
                  {{ listing.title }}
                </h3>
                <p class="mt-2 text-sm text-foreground/60">{{ listing.brand || 'No brand' }}</p>
                <div class="mt-4 flex items-center justify-between gap-3">
                  <span class="text-sm font-semibold text-foreground">
                    {{ formatPrice(listing.currentPrice, listing.currency) }}
                  </span>
                  <span class="text-sm text-foreground/50">{{ statusLabel(listing) }}</span>
                </div>
              </div>
            </RouterLink>
          </div>

          <div v-else class="mt-6 rounded-[24px] border border-dashed px-6 py-10 text-center text-sm text-foreground/60">
            This seller has no active listings.
          </div>
        </div>

        <div class="rounded-[28px] border bg-background px-6 py-6">
          <div>
            <p class="text-[11px] uppercase tracking-[0.22em] text-foreground/40">
              Seller history
            </p>
            <h2 class="mt-2 text-2xl font-semibold tracking-tight text-foreground">
              Sold items
            </h2>
          </div>

          <div v-if="soldListings.length" class="mt-6 grid gap-4 sm:grid-cols-2">
            <RouterLink
              v-for="listing in soldListings"
              :key="listing.lotId"
              :to="`/lots/${listing.lotId}`"
              class="group overflow-hidden rounded-[24px] border bg-background"
            >
              <div class="relative aspect-[4/5] overflow-hidden bg-foreground/5">
                <img
                  v-if="listing.thumbnailUrl"
                  :src="listing.thumbnailUrl"
                  :alt="listing.title"
                  class="h-full w-full object-cover transition duration-300 group-hover:scale-[1.02]"
                />
                <div v-else class="flex h-full items-center justify-center text-sm text-foreground/50">
                  No image
                </div>
                <div class="absolute left-3 top-3 rounded-full bg-background px-3 py-1 text-xs font-medium shadow-sm">
                  Sold
                </div>
              </div>

              <div class="p-4">
                <h3 class="line-clamp-2 text-base font-medium text-foreground">
                  {{ listing.title }}
                </h3>
                <p class="mt-2 text-sm text-foreground/60">{{ listing.brand || 'No brand' }}</p>
                <p class="mt-4 text-sm font-semibold text-foreground">
                  {{ formatPrice(listing.currentPrice, listing.currency) }}
                </p>
              </div>
            </RouterLink>
          </div>

          <div v-else class="mt-6 rounded-[24px] border border-dashed px-6 py-10 text-center text-sm text-foreground/60">
            This seller has no sold items yet.
          </div>
        </div>
      </section>
    </template>
  </div>
</template>
