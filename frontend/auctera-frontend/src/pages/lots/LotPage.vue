<script setup lang="ts">
const lot = {
  id: 1,
  title: 'Rick Owens Jumbo Lace Puffer Jacket',
  brand: 'Rick Owens',
  category: 'Designer',
  condition: 'Very good',
  size: 'M',
  color: 'Black',
  seller: 'Coutera Archive',
  location: 'Riga, Latvia',
  currentBid: '€820',
  startingPrice: '€500',
  buyNowPrice: '€1,150',
  bidsCount: 14,
  timeLeft: '1 day 12 hours',
  description:
    'A standout Rick Owens puffer jacket with oversized proportions and signature silhouette. Clean overall condition with light signs of wear. Ideal for collectors and anyone building a strong designer wardrobe.',
  images: [
    'https://media-photos.depop.com/b1/40086058/3512274199_d42fb73add7043d586f1be825bfb1f68/P0.jpg',
    'https://media-photos.depop.com/b1/40086058/3512274199_d42fb73add7043d586f1be825bfb1f68/P1.jpg',
    'https://media-photos.depop.com/b1/40086058/3512274199_d42fb73add7043d586f1be825bfb1f68/P2.jpg',
    'https://media-photos.depop.com/b1/40086058/3512274199_d42fb73add7043d586f1be825bfb1f68/P3.jpg',
  ],
}

const specifications = [
  { label: 'Brand', value: lot.brand },
  { label: 'Category', value: lot.category },
  { label: 'Condition', value: lot.condition },
  { label: 'Size', value: lot.size },
  { label: 'Color', value: lot.color },
  { label: 'Location', value: lot.location },
]
</script>

<template>
  <div class="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
    <div class="mb-6 text-sm text-black/50">
      <span class="cursor-pointer transition hover:text-black">Home</span>
      <span class="mx-2">/</span>
      <span class="cursor-pointer transition hover:text-black">Auctions</span>
      <span class="mx-2">/</span>
      <span class="text-black">{{ lot.title }}</span>
    </div>

    <section class="grid gap-8 lg:grid-cols-[1.15fr_0.85fr]">
      <div>
        <div class="overflow-hidden rounded-[28px] bg-neutral-100">
          <img
            :src="lot.images[0]"
            :alt="lot.title"
            class="h-[420px] w-full object-cover md:h-[560px]"
          />
        </div>

        <div class="mt-4 grid grid-cols-4 gap-3">
          <div
            v-for="(image, index) in lot.images"
            :key="index"
            class="overflow-hidden rounded-2xl bg-neutral-100"
          >
            <img
              :src="image"
              :alt="`${lot.title} image ${index + 1}`"
              class="h-24 w-full object-cover md:h-32"
            />
          </div>
        </div>
      </div>

      <div class="flex flex-col gap-5">
        <div class="rounded-[28px] bg-neutral-100 px-6 py-6 md:px-7">
          <p class="text-[11px] uppercase tracking-[0.24em] text-black/40">
            {{ lot.brand }}
          </p>

          <h1 class="mt-3 text-3xl font-semibold tracking-tight text-black sm:text-4xl">
            {{ lot.title }}
          </h1>

          <div class="mt-4 flex flex-wrap gap-2">
            <span class="rounded-full border border-black/10 bg-white px-3 py-1 text-sm text-black/70">
              {{ lot.category }}
            </span>
            <span class="rounded-full border border-black/10 bg-white px-3 py-1 text-sm text-black/70">
              Size {{ lot.size }}
            </span>
            <span class="rounded-full border border-black/10 bg-white px-3 py-1 text-sm text-black/70">
              {{ lot.condition }}
            </span>
          </div>

          <div class="mt-6 grid grid-cols-2 gap-3">
            <div class="rounded-2xl bg-white p-4">
              <p class="text-xs uppercase tracking-[0.2em] text-black/40">
                Current bid
              </p>
              <p class="mt-2 text-2xl font-semibold text-black">
                {{ lot.currentBid }}
              </p>
            </div>

            <div class="rounded-2xl bg-white p-4">
              <p class="text-xs uppercase tracking-[0.2em] text-black/40">
                Time left
              </p>
              <p class="mt-2 text-2xl font-semibold text-black">
                {{ lot.timeLeft }}
              </p>
            </div>

            <div class="rounded-2xl bg-white p-4">
              <p class="text-xs uppercase tracking-[0.2em] text-black/40">
                Starting price
              </p>
              <p class="mt-2 text-lg font-medium text-black">
                {{ lot.startingPrice }}
              </p>
            </div>

            <div class="rounded-2xl bg-white p-4">
              <p class="text-xs uppercase tracking-[0.2em] text-black/40">
                Total bids
              </p>
              <p class="mt-2 text-lg font-medium text-black">
                {{ lot.bidsCount }}
              </p>
            </div>
          </div>

          <div class="mt-6 flex flex-col gap-3 sm:flex-row">
            <button
              class="w-full rounded-full bg-black px-5 py-3 text-sm font-medium text-white transition hover:bg-black/90"
            >
              Place bid
            </button>

            <button
              class="w-full rounded-full border border-black/10 bg-white px-5 py-3 text-sm font-medium text-black transition hover:border-black"
            >
              Add to watchlist
            </button>
          </div>

          <button
            class="mt-3 w-full rounded-full border border-black/10 bg-transparent px-5 py-3 text-sm font-medium text-black transition hover:border-black hover:bg-white"
          >
            Buy now for {{ lot.buyNowPrice }}
          </button>
        </div>

        <div class="rounded-[28px] border border-black/10 bg-white px-6 py-6">
          <div class="flex items-start justify-between gap-3">
            <div>
              <p class="text-xs uppercase tracking-[0.2em] text-black/40">
                Seller
              </p>
              <h2 class="mt-2 text-lg font-semibold text-black">
                {{ lot.seller }}
              </h2>
              <p class="mt-1 text-sm text-black/60">
                Based in {{ lot.location }}
              </p>
            </div>

            <button
              class="rounded-full border border-black/10 px-4 py-2 text-sm font-medium text-black transition hover:border-black"
            >
              View profile
            </button>
          </div>
        </div>

        <div class="rounded-[28px] border border-black/10 bg-white px-6 py-6">
          <p class="text-xs uppercase tracking-[0.2em] text-black/40">
            Buyer protection
          </p>
          <p class="mt-3 text-sm leading-6 text-black/70">
            Every lot goes through a review process before publication. Bids are tracked in real time,
            and order flow is handled through the platform for a more secure experience.
          </p>
        </div>
      </div>
    </section>

    <section class="mt-10 grid gap-6 lg:grid-cols-[0.85fr_1.15fr]">
      <div class="rounded-[28px] border border-black/10 bg-white px-6 py-6">
        <h2 class="text-2xl font-semibold tracking-tight text-black">
          Specifications
        </h2>

        <div class="mt-6 divide-y divide-black/10">
          <div
            v-for="item in specifications"
            :key="item.label"
            class="flex items-center justify-between gap-4 py-4"
          >
            <span class="text-sm text-black/50">{{ item.label }}</span>
            <span class="text-sm font-medium text-black">{{ item.value }}</span>
          </div>
        </div>
      </div>

      <div class="rounded-[28px] bg-neutral-100 px-6 py-6">
        <h2 class="text-2xl font-semibold tracking-tight text-black">
          Description
        </h2>

        <p class="mt-4 max-w-3xl text-sm leading-7 text-black/70 sm:text-base">
          {{ lot.description }}
        </p>

        <div class="mt-8 grid gap-4 sm:grid-cols-2">
          <div class="rounded-2xl bg-white p-5">
            <p class="text-xs uppercase tracking-[0.2em] text-black/40">
              Shipping
            </p>
            <p class="mt-3 text-sm leading-6 text-black/70">
              Domestic and international shipping options can be shown here later.
              For now this block gives the page a finished marketplace look.
            </p>
          </div>

          <div class="rounded-2xl bg-white p-5">
            <p class="text-xs uppercase tracking-[0.2em] text-black/40">
              Returns
            </p>
            <p class="mt-3 text-sm leading-6 text-black/70">
              Return policy, authenticity notes, payment details, or auction rules
              can live here once backend is connected.
            </p>
          </div>
        </div>
      </div>
    </section>

    <section class="my-16 flex flex-col items-center justify-center gap-2 py-10 text-center text-black">
      <h2 class="max-w-3xl text-3xl font-semibold leading-tight sm:text-4xl">
        Not mass market. Never ordinary.
      </h2>
      <p class="mt-2 max-w-2xl text-sm text-black/70 sm:text-base">
        Every lot should feel deliberate, collectible, and worth a second look.
      </p>
    </section>

    <section class="mb-12 rounded-[28px] border border-black/10 bg-white px-6 py-6">
      <div class="flex items-end justify-between gap-3">
        <div>
          <p class="text-[11px] uppercase tracking-[0.22em] text-black/40">
            More to explore
          </p>
          <h2 class="mt-2 text-2xl font-semibold tracking-tight text-black">
            Similar lots
          </h2>
          <p class="mt-1 text-sm text-black/60">
            You can later plug related lots here through the backend.
          </p>
        </div>

        <button
          class="text-sm font-medium text-black transition hover:text-black/60"
        >
          Browse more
        </button>
      </div>

      <div class="mt-6 grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
        <div
          v-for="n in 4"
          :key="n"
          class="group overflow-hidden rounded-2xl bg-neutral-100"
        >
          <div class="overflow-hidden">
            <img
              :src="lot.images[(n - 1) % lot.images.length]"
              alt="Similar lot"
              class="h-64 w-full object-cover transition duration-300 group-hover:scale-[1.02]"
            />
          </div>

          <div class="p-4">
            <p class="text-xs uppercase tracking-[0.2em] text-black/40">
              Designer
            </p>
            <h3 class="mt-2 line-clamp-2 text-base font-medium text-black">
              Minimal archive piece placeholder title
            </h3>
            <div class="mt-3 flex items-center justify-between">
              <span class="text-sm font-semibold text-black">€540</span>
              <span class="text-sm text-black/50">2 days left</span>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>