<script setup lang="ts">
import { computed, onMounted, ref } from "vue"
import { useRoute } from "vue-router"
import { ProfileService } from "@/app/services/profileService"
import type { UserProfileDto } from "@/types/userProfile"

const route = useRoute()
const profileService = ProfileService()

const profile = ref<UserProfileDto | null>(null)
const isLoading = ref(true)
const errorMessage = ref("")

const loadProfile = async () => {
  try {
    isLoading.value = true
    errorMessage.value = ""

    const username = route.params.username as string
    profile.value = await profileService.getUserProfile(username)
  } catch (error) {
    console.error("Failed to load profile", error)
    errorMessage.value = "Failed to load profile."
  } finally {
    isLoading.value = false
  }
}

onMounted(loadProfile)

const activeListings = computed(() => profile.value?.activeListings ?? [])
const soldListings = computed(() => profile.value?.soldListings ?? [])

function formatPrice(amount: number, currency: string): string {
  return new Intl.NumberFormat("en", {
    style: "currency",
    currency,
    maximumFractionDigits: 0,
  }).format(amount)
}

function resolveImageUrl(path: string | null | undefined): string {
  if (!path) {
    return "/placeholder-image.jpg"
  }

  return `https://your-public-r2-url/${path}`
}
</script>

<template>
  <div class="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
    <section class="overflow-hidden rounded-[32px] bg-background px-6 py-8 md:px-8 md:py-10">
      <div class="grid gap-8 lg:grid-cols-[1.15fr_0.85fr] lg:items-end">
        <div class="flex flex-col gap-6 sm:flex-row sm:items-center">
          <div class="max-w-2xl">
            <p class="text-[11px] uppercase tracking-[0.22em] text-foreground/40">
              User profile
            </p>

            <h1 class="mt-2 text-3xl font-semibold tracking-tight text-foreground sm:text-4xl md:text-5xl">
              {{ profile?.name }}
            </h1>

            <div class="mt-3 flex flex-wrap gap-2">
              <span class="rounded-full border bg-background px-3 py-1 text-sm text-foreground/70">
                @{{ profile?.username }}
              </span>
              <!-- <span class="rounded-full border bg-background px-3 py-1 text-sm text-foreground/70">
                {{ user.location }}
              </span>
              <span class="rounded-full border bg-background px-3 py-1 text-sm text-foreground/70">
                {{ user.joined }}
              </span> -->
            </div>

            <!-- <p class="mt-4 max-w-xl text-sm leading-6 text-foreground/70 sm:text-base">
              {{ user.bio }}
            </p> -->

            <!-- <div class="mt-5 flex flex-wrap gap-3 text-sm text-foreground/60">
              <span class="rounded-full bg-background px-3 py-1.5">
                Rating {{ user.rating }} / 5
              </span>
              <span class="rounded-full bg-background px-3 py-1.5">
                {{ user.reviewsCount }} reviews
              </span>
              <span class="rounded-full bg-background px-3 py-1.5">
                {{ user.responseTime }}
              </span>
            </div> -->
          </div>
        </div>

        <div class="flex flex-col gap-3 sm:flex-row lg:justify-end">
          <button
            class="rounded-full bg-background px-5 py-3 text-sm font-medium text-foreground transition hover:bg-background/90"
          >
            Follow
          </button>

          <button
            class="rounded-full border bg-background px-5 py-3 text-sm font-medium text-foreground transition hover:border-foreground hover:bg-background/90"
          >
            Message
          </button>
        </div>
      </div>
    </section>

    <section class="mt-8 grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
      <div class="rounded-[24px] border bg-background p-5">
        <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">Active listings</p>
        <p class="mt-3 text-3xl font-semibold text-foreground">{{ profile?.stats.activeListingsCount }}</p>
      </div>

      <div class="rounded-[24px] border bg-background p-5">
        <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">Sold items</p>
        <p class="mt-3 text-3xl font-semibold text-foreground">{{ profile?.stats.soldItemsCount }}</p>
      </div>

      <div class="rounded-[24px] border bg-background p-5">
        <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">Bids placed</p>
        <p class="mt-3 text-3xl font-semibold text-foreground">{{ profile?.stats.bidsPlaced }}</p>
      </div>

      <!-- <div class="rounded-[24px] border bg-background p-5">
        <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">Followers</p>
        <p class="mt-3 text-3xl font-semibold text-foreground">{{ profile.stats.followersCount }}</p>
      </div>

      <div class="rounded-[24px] border bg-background p-5">
        <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">Following</p>
        <p class="mt-3 text-3xl font-semibold text-foreground">{{ profile.stats.followingCount }}</p>
      </div> -->
    </section>

    <section class="mt-10 grid gap-6 lg:grid-cols-[0.78fr_1.22fr]">
      <div class="space-y-6">
        <div class="rounded-[28px] border bg-background px-6 py-6">
          <h2 class="text-2xl font-semibold tracking-tight text-foreground">
            About seller
          </h2>

          <div class="mt-6 space-y-5">
            <div>
              <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">Focus</p>
              <p class="mt-2 text-sm leading-6 text-foreground/70">
                Archive fashion, designer outerwear, accessories, and selected collectible pieces.
              </p>
            </div>

            <!-- <div>
              <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">Based in</p>
              <p class="mt-2 text-sm leading-6 text-foreground/70">
                {{ user.location }}
              </p>
            </div> -->

            <div>
              <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">Account status</p>
              <div class="mt-3 flex flex-wrap gap-2">
                <span class="rounded-full bg-background px-3 py-1 text-sm text-foreground/70">
                  Verified seller
                </span>
                <span class="rounded-full bg-background px-3 py-1 text-sm text-foreground/70">
                  Secure payments
                </span>
                <span class="rounded-full bg-background px-3 py-1 text-sm text-foreground/70">
                  Trusted history
                </span>
              </div>
            </div>
          </div>
        </div>

        <!-- <div class="rounded-[28px] border bg-background px-6 py-6">
          <div class="flex items-end justify-between gap-3">
            <div>
              <p class="text-[11px] uppercase tracking-[0.22em] text-foreground/40">
                Reviews
              </p>
              <h2 class="mt-2 text-2xl font-semibold tracking-tight text-foreground">
                Buyer feedback
              </h2>
            </div>
            <button class="text-sm font-medium text-foreground transition hover:text-foreground/60">
              View all
            </button>
          </div>

          <div class="mt-6 space-y-4">
            <div
              v-for="review in reviews"
              :key="review.id"
              class="rounded-2xl bg-background p-4"
            >
              <div class="flex items-center justify-between gap-3">
                <p class="text-sm font-medium text-foreground">{{ review.author }}</p>
                <span class="text-xs uppercase tracking-[0.18em] text-foreground/40">Verified purchase</span>
              </div>
              <p class="mt-3 text-sm leading-6 text-foreground/70">
                {{ review.text }}
              </p>
            </div>
          </div>
        </div> -->
      </div>

      <div class="space-y-6">
        <div class="rounded-[28px] bg-background px-6 py-6">
          <div class="flex items-end justify-between gap-3">
            <div>
              <p class="text-[11px] uppercase tracking-[0.22em] text-foreground/40">
                Live inventory
              </p>
              <h2 class="mt-2 text-2xl font-semibold tracking-tight text-foreground">
                Active listings
              </h2>
              <p class="mt-1 text-sm text-foreground/60">
                Lots currently available for bidding.
              </p>
            </div>

            <button class="text-sm font-medium text-foreground transition hover:text-foreground/60">
              View all
            </button>
          </div>

          <div v-if="activeListings.length" class="mt-6 grid gap-4 sm:grid-cols-2">
            <article
              v-for="listing in activeListings"
              :key="listing.lotId"
              class="group overflow-hidden rounded-[24px] bg-background"
            >
              <div class="overflow-hidden">
                <img
                  :src="resolveImageUrl(listing.thumbnailUrl)"
                  :alt="listing.title"
                  class="h-72 w-full object-cover transition duration-300 group-hover:scale-[1.02]"
                />
              </div>

              <div class="p-4">
                <h3 class="line-clamp-2 text-base font-medium text-foreground">
                  {{ listing.title }}
                </h3>

                <p class="mt-2 text-sm text-foreground/60">
                  {{ listing.brand || 'No brand' }}
                </p>

                <div class="mt-4 flex items-center justify-between gap-3">
                  <span class="text-sm font-semibold text-foreground">
                    {{ formatPrice(listing.currentPrice, listing.currency) }}
                  </span>
                  <span class="text-sm text-foreground/50">
                    Active
                  </span>
                </div>
              </div>
            </article>
          </div>
        </div>

        <div class="rounded-[28px] border border-black/10 bg-background px-6 py-6">
          <div class="flex items-end justify-between gap-3">
            <div>
              <p class="text-[11px] uppercase tracking-[0.22em] text-foreground/40">
                Seller history
              </p>
              <h2 class="mt-2 text-2xl font-semibold tracking-tight text-foreground">
                Sold items
              </h2>
              <p class="mt-1 text-sm text-foreground/60">
                Pieces already sold through the platform.
              </p>
            </div>

            <button class="text-sm font-medium text-foreground transition hover:text-foreground/60">
              View archive
            </button>
          </div>

          <div v-if="soldListings.length" class="mt-6 grid gap-4 sm:grid-cols-2">
            <article
              v-for="listing in soldListings"
              :key="listing.lotId"
              class="group overflow-hidden rounded-[24px] bg-background"
            >
              <div class="relative overflow-hidden">
                <img
                  :src="resolveImageUrl(listing.thumbnailUrl)"
                  :alt="listing.title"
                  class="h-72 w-full object-cover transition duration-300 group-hover:scale-[1.02]"
                />
                <div class="absolute left-3 top-3 rounded-full bg-background px-3 py-1 text-xs font-medium text-foreground/70">
                  Sold
                </div>
              </div>

              <div class="p-4">
                <h3 class="line-clamp-2 text-base font-medium text-foreground">
                  {{ listing.title }}
                </h3>

                <p class="mt-2 text-sm text-foreground/60">
                  {{ listing.brand || 'No brand' }}
                </p>

                <div class="mt-4 flex items-center justify-between gap-3">
                  <span class="text-sm font-semibold text-foreground">
                    {{ formatPrice(listing.currentPrice, listing.currency) }}
                  </span>
                  <span class="text-sm text-foreground/50">
                    Sold
                  </span>
                </div>
              </div>
            </article>
          </div>
        </div>
      </div>
    </section>

    <section class="my-16 flex flex-col items-center justify-center gap-2 py-10 text-center text-foreground/60">
      <h2 class="max-w-3xl text-3xl font-semibold leading-tight sm:text-4xl">
        Clean profile. Strong inventory. Real history.
      </h2>
      <p class="mt-2 max-w-2xl text-sm text-foreground/70 sm:text-base">
        A seller page should show not just taste, but proof of activity and trust.
      </p>
    </section>
  </div>
</template>