<script setup lang="ts">
import { RouterLink } from 'vue-router'
</script>

<template>
  <div class="mx-auto flex min-h-[70vh] max-w-7xl items-center justify-center px-4 py-10 sm:px-6 lg:px-8">
    <section
      class="relative w-full max-w-4xl overflow-hidden rounded-[32px] border border-border bg-background px-6 py-10 text-center md:px-10 md:py-14"
    >
      <div class="relative z-10">
        <p class="text-[12px] uppercase tracking-[0.28em] text-muted-foreground">
          Coutera
        </p>

        <h1 class="mt-4 text-6xl font-semibold tracking-tight text-foreground sm:text-7xl md:text-8xl">
          404
        </h1>

        <h2 class="mt-4 text-2xl font-semibold tracking-tight text-foreground sm:text-3xl md:text-4xl">
          This page does not exist.
        </h2>

        <p class="mx-auto mt-4 max-w-2xl text-sm leading-6 text-muted-foreground sm:text-base">
          The page you are looking for may have been removed, renamed, or never existed in the first place.
        </p>

        <div class="mt-8 flex flex-col items-center justify-center gap-3 sm:flex-row">
          <RouterLink
            to="/"
            class="rounded-full bg-primary px-5 py-3 text-sm font-medium text-primary-foreground transition hover:opacity-90"
          >
            Back home
          </RouterLink>

          <RouterLink
            to="/auctions"
            class="rounded-full border border-border bg-card px-5 py-3 text-sm font-medium text-card-foreground transition hover:bg-accent hover:text-accent-foreground"
          >
            Browse auctions
          </RouterLink>
        </div>

        <div class="mt-10 grid gap-4 sm:grid-cols-3">
          <div class="rounded-[24px] border border-border bg-card p-5 text-left">
            <p class="text-[11px] uppercase tracking-[0.22em] text-muted-foreground">
              Explore
            </p>
            <h3 class="mt-3 text-lg font-semibold text-card-foreground">
              Trending lots
            </h3>
            <p class="mt-2 text-sm leading-6 text-muted-foreground">
              Discover the pieces currently getting the most attention.
            </p>
          </div>

          <div class="rounded-[24px] border border-border bg-card p-5 text-left">
            <p class="text-[11px] uppercase tracking-[0.22em] text-muted-foreground">
              Discover
            </p>
            <h3 class="mt-3 text-lg font-semibold text-card-foreground">
              New listings
            </h3>
            <p class="mt-2 text-sm leading-6 text-muted-foreground">
              Browse the latest lots added to the marketplace.
            </p>
          </div>

          <div class="rounded-[24px] border border-border bg-card p-5 text-left">
            <p class="text-[11px] uppercase tracking-[0.22em] text-muted-foreground">
              Refine
            </p>
            <h3 class="mt-3 text-lg font-semibold text-card-foreground">
              Categories
            </h3>
            <p class="mt-2 text-sm leading-6 text-muted-foreground">
              Go back through designer, vintage, streetwear, and more.
            </p>
          </div>
        </div>

        <div class="mt-10 border-t border-border pt-6">
          <p class="text-sm text-muted-foreground">
            Not mass market. Never ordinary.
          </p>
        </div>
      </div>
    </section>
  </div>
</template>