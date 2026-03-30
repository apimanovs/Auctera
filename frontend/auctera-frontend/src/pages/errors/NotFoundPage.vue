<script setup lang="ts">
import { RouterLink } from 'vue-router'
</script>

<template>
  <div class="mx-auto flex min-h-[70vh] max-w-7xl items-center justify-center px-4 py-10 sm:px-6 lg:px-8">
    <section
      class="relative w-full max-w-4xl overflow-hidden rounded-[32px] bg-neutral-100 px-6 py-10 text-center md:px-10 md:py-14"
    >
      <div class="relative z-10">
        <p class="text-[12px] uppercase tracking-[0.28em] text-black/40">
          Coutera
        </p>

        <h1 class="mt-4 text-6xl font-semibold tracking-tight text-black sm:text-7xl md:text-8xl">
          404
        </h1>

        <h2 class="mt-4 text-2xl font-semibold tracking-tight text-black sm:text-3xl md:text-4xl">
          This page does not exist.
        </h2>

        <p class="mx-auto mt-4 max-w-2xl text-sm leading-6 text-black/60 sm:text-base">
          The page you are looking for may have been removed, renamed, or never existed in the first place.
        </p>

        <div class="mt-8 flex flex-col items-center justify-center gap-3 sm:flex-row">
          <RouterLink
            to="/"
            class="rounded-full bg-black px-5 py-3 text-sm font-medium text-white transition hover:bg-black/90"
          >
            Back home
          </RouterLink>

          <RouterLink
            to="/auctions"
            class="rounded-full border border-black/10 bg-white px-5 py-3 text-sm font-medium text-black transition hover:border-black"
          >
            Browse auctions
          </RouterLink>
        </div>

        <div class="mt-10 grid gap-4 sm:grid-cols-3">
          <div class="rounded-[24px] bg-white p-5 text-left">
            <p class="text-[11px] uppercase tracking-[0.22em] text-black/40">
              Explore
            </p>
            <h3 class="mt-3 text-lg font-semibold text-black">
              Trending lots
            </h3>
            <p class="mt-2 text-sm leading-6 text-black/60">
              Discover the pieces currently getting the most attention.
            </p>
          </div>

          <div class="rounded-[24px] bg-white p-5 text-left">
            <p class="text-[11px] uppercase tracking-[0.22em] text-black/40">
              Discover
            </p>
            <h3 class="mt-3 text-lg font-semibold text-black">
              New listings
            </h3>
            <p class="mt-2 text-sm leading-6 text-black/60">
              Browse the latest lots added to the marketplace.
            </p>
          </div>

          <div class="rounded-[24px] bg-white p-5 text-left">
            <p class="text-[11px] uppercase tracking-[0.22em] text-black/40">
              Refine
            </p>
            <h3 class="mt-3 text-lg font-semibold text-black">
              Categories
            </h3>
            <p class="mt-2 text-sm leading-6 text-black/60">
              Go back through designer, vintage, streetwear, and more.
            </p>
          </div>
        </div>

        <div class="mt-10 border-t border-black/10 pt-6">
          <p class="text-sm text-black/50">
            Not mass market. Never ordinary.
          </p>
        </div>
      </div>
    </section>
  </div>
</template>