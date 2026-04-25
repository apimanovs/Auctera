<script setup lang="ts">
import { useRoute } from "vue-router"
import { computed, onMounted, ref } from "vue"
import { LotPageService } from "@/app/services/lotPageService.ts"
import type { Lot } from "@/types/lot"

const route = useRoute()
const lotService = LotPageService()

const lot = ref<Lot | null>(null)
const isLoading = ref(true)
const errorMessage = ref("")

const loadLotInformation = async () => {
  try 
  {
    isLoading.value = true
    errorMessage.value = ""

    const lotId = route.params.lotId as string
    const response = await lotService.getlotInformation(lotId)
    lot.value = response

    console.log(lot.value.media)
  } 
  catch (error) 
  {
    console.error('Error loading lot information:', error)
    errorMessage.value = "Failed to load profile."
  }
  finally 
  {
    isLoading.value = false
  }
}

onMounted(loadLotInformation)
</script>

<template>
  <div v-if="lot" class="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
    <div class="mb-6 text-sm text-foreground/60">
      <span class="cursor-pointer transition hover:text-black">Home</span>
      <span class="mx-2">/</span>
      <span class="cursor-pointer transition hover:text-black">Auctions</span>
      <span class="mx-2">/</span>
      <span class="text-foreground">{{ lot?.title }}</span>
    </div>

    <section class="grid gap-8 lg:grid-cols-[1.15fr_0.85fr]">
      <div>
        <div class="overflow-hidden rounded-[28px] bg-background">
          <img
            :src="lot?.media[0]?.url"
            :alt="lot?.title"
            class="h-[420px] w-full object-cover md:h-[560px]"
          />
        </div>

        <div class="mt-4 grid grid-cols-4 gap-3">
          <div
            v-for="(image, index) in lot?.media"
            :key="index"
            class="overflow-hidden rounded-2xl bg-background"
          >
            <img
              :src="image.url"
              :alt="`${ lot?.title} image ${index + 1}`"
              class="h-24 w-full object-cover md:h-32"
            />
          </div>
        </div>
      </div>

      <div class="flex flex-col gap-5">
        <div class="rounded-[28px] bg-background px-6 py-6 md:px-7">
          <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/40">
            {{ lot?.brand }}
          </p>

          <h1 class="mt-3 text-3xl font-semibold tracking-tight text-foreground sm:text-4xl">
            {{ lot?.title }}
          </h1>

          <div class="mt-4 flex flex-wrap gap-2">
            <span class="rounded-full border border-foreground/20 bg-background px-3 py-1 text-sm text-foreground/70">
              {{ lot?.categoryName }}
            </span>
            <span class="rounded-full border border-foreground/20 bg-background px-3 py-1 text-sm text-foreground/70">
              Size {{ lot?.sizeName }}
            </span>
            <span class="rounded-full border border-foreground/20 bg-background px-3 py-1 text-sm text-foreground/70">
              {{ lot?.conditionName }}
            </span>
          </div>

          <div class="mt-6 grid grid-cols-2 gap-3">
            <!-- <div class="rounded-2xl bg-background p-4">
              <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">
                Current bid
              </p>
              <p class="mt-2 text-2xl font-semibold text-foreground">
                {{ lot?.currentBid }}
              </p>
            </div> -->

            <!-- <div class="rounded-2xl bg-background p-4">
              <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">
                Time left
              </p>
              <p class="mt-2 text-2xl font-semibold text-foreground">
                {{ lot?.timeLeft }}
              </p>
            </div> -->

            <!-- <div class="rounded-2xl bg-background p-4">
              <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">
                Starting price
              </p>
              <p class="mt-2 text-lg font-medium text-foreground">
                {{ lot?.startingPrice }}
              </p>
            </div> -->

            <!-- <div class="rounded-2xl bg-background p-4">
              <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">
                Total bids
              </p>
              <p class="mt-2 text-lg font-medium text-foreground">
                {{ lot?.bidsCount }}
              </p>
            </div> -->
          </div>

          <div class="mt-6 flex flex-col gap-3 sm:flex-row">
            <button
              class="w-full rounded-full bg-black px-5 py-3 text-sm font-medium text-white transition hover:bg-black/90 dark:bg-white dark:text-black dark:hover:bg-white/90 cursor-pointer"
            >
              Place bid
            </button>

            <button
              class="w-full rounded-full border bg-background px-5 py-3 text-sm font-medium text-foreground transition hover:bg-white/10 cursor-pointer"
            >
              Add to watchlist
            </button>
          </div>

          <button
            class="disabled mt-3 w-full  rounded-full border bg-background px-5 py-3 text-sm font-medium text-foreground transition cursor-pointer hover:bg-white/10"
          >
            Buy now for
          </button>
        </div>

        <div class="rounded-[28px] border border-foreground/20 bg-background px-6 py-6">
          <div class="flex items-start justify-between gap-3">
            <div>
              <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">
                Seller
              </p>
              <h2 class="mt-2 text-lg font-semibold text-foreground">
                {{ lot?.seller?.name }}
              </h2>
              <p class="mt-1 text-sm text-foreground/60">
                @{{ lot?.seller?.username }}
              </p>
              <!-- <p class="mt-1 text-sm text-foreground/60">
                Based in {{ lot.location }}
              </p> -->
            </div>

            <button
              class="rounded-full border border-foreground/20 px-4 py-2 text-sm font-medium text-foreground transition hover:border-foreground cursor-pointer"
            >
              View profile
            </button>
          </div>
        </div>

        <div class="rounded-[28px] border border-foreground/20 bg-background px-6 py-6">
          <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">
            Buyer protection
          </p>
          <p class="mt-3 text-sm leading-6 text-foreground/70">
            Every lot goes through a review process before publication. Bids are tracked in real time,
            and order flow is handled through the platform for a more secure experience.
          </p>
        </div>
      </div>
    </section>

    <section class="mt-10 grid gap-6 lg:grid-cols-[0.85fr_1.15fr]">
      <div class="rounded-[28px] border border-foreground/20 bg-background px-6 py-6">
        <h2 class="text-2xl font-semibold tracking-tight text-foreground">
          Specifications
        </h2>

        <div class="mt-6 divide-y divide-foreground/20">
          <div class="flex items-center justify-between gap-4 py-4">
            <span class="text-sm text-foreground/50">Brand</span>
            <span class="text-sm font-medium text-foreground">{{ lot?.brand }}</span>
            
            <span class="text-sm text-foreground/50">Size</span>
            <span class="text-sm font-medium text-foreground">{{ lot?.sizeName }}</span>

            <span class="text-sm text-foreground/50">Condition</span>
            <span class="text-sm font-medium text-foreground">{{ lot?.conditionName }}</span>

            <span class="text-sm text-foreground/50">Color</span>
            <span class="text-sm font-medium text-foreground">{{ lot?.color }}</span>

            <span class="text-sm text-foreground/50">Category</span>
            <span class="text-sm font-medium text-foreground">{{ lot?.categoryName }}</span>
          </div>
        </div>
      </div>

      <div class="rounded-[28px] bg-background px-6 py-6 border border-foreground/20">
        <h2 class="text-2xl font-semibold tracking-tight text-foreground">
          Description
        </h2>

        <p class="mt-4 max-w-3xl text-sm leading-7 text-foreground/70 sm:text-base">
          {{  lot?.description }}
        </p>

        <div class="mt-8 grid gap-4 sm:grid-cols-2 border rounded-2xl">
          <div class="rounded-2xl bg-background p-5">
            <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">
              Shipping
            </p>
            <p class="mt-3 text-sm leading-6 text-foreground/70">
              Domestic and international shipping options can be shown here later.
              For now this block gives the page a finished marketplace look.
            </p>
          </div>

          <div class="rounded-2xl bg-background p-5">
            <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">
              Returns
            </p>
            <p class="mt-3 text-sm leading-6 text-foreground/70">
              Return policy, authenticity notes, payment details, or auction rules
              can live here once backend is connected.
            </p>
          </div>
        </div>
      </div>
    </section>

    <section class="my-16 flex flex-col items-center justify-center gap-2 py-10 text-center text-foreground">
      <h2 class="max-w-3xl text-3xl font-semibold leading-tight sm:text-4xl">
        Not mass market. Never ordinary.
      </h2>
      <p class="mt-2 max-w-2xl text-sm text-foreground/70 sm:text-base">
        Every lot should feel deliberate, collectible, and worth a second look.
      </p>
    </section>

    <section class="mb-12 rounded-[28px] border border-foreground/20 bg-background px-6 py-6">
      <div class="flex items-end justify-between gap-3">
        <div>
          <p class="text-[11px] uppercase tracking-[0.22em] text-foreground/40">
            More to explore
          </p>
          <h2 class="mt-2 text-2xl font-semibold tracking-tight text-foreground">
            Similar lots
          </h2>
          <p class="mt-1 text-sm text-foreground/60">
            You can later plug related lots here through the backend.
          </p>
        </div>

        <button
          class="text-sm font-medium text-foreground transition hover:text-foreground/60"
        >
          Browse more
        </button>
      </div>

      <div class="mt-6 grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
        <div
          v-for="n in 4"
          :key="n"
          class="group overflow-hidden rounded-2xl bg-background transition hover:shadow-lg cursor-pointer"
        >
          <div class="overflow-hidden">
            <img
              :src="lot?.media[(n - 1) % lot?.media.length]?.url"
              alt="Similar lot"
              class="h-64 w-full object-cover transition duration-300 group-hover:scale-[1.02]"
            />
          </div>

          <div class="p-4">
            <p class="text-xs uppercase tracking-[0.2em] text-foreground/40">
              Designer
            </p>
            <h3 class="mt-2 line-clamp-2 text-base font-medium text-foreground">
              Minimal archive piece placeholder title
            </h3>
            <div class="mt-3 flex items-center justify-between">
              <span class="text-sm font-semibold text-foreground">€540</span>
              <span class="text-sm text-foreground/50">2 days left</span>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>