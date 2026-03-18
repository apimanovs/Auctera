<script setup lang="ts">
</script>

<template>
  <div class="mx-auto max-w-6xl px-4 py-10 sm:px-6 lg:px-8">
    <!-- Hero -->
    <section class="flex flex-col items-center justify-center py-20 text-center sm:py-28">
      <p class="text-xs uppercase tracking-[0.35em] text-black/45">
        Archive Fashion Marketplace
      </p>

      <h1 class="mt-4 text-5xl font-extrabold tracking-tight sm:text-6xl md:text-7xl">
        Coutera
      </h1>

      <p class="mt-6 max-w-2xl text-base leading-7 text-black/70 sm:text-lg">
        A curated auction marketplace for rare fashion, archive pieces, and collectible garments.
      </p>

      <p class="mt-3 text-sm text-black/50 sm:text-base">
        Not trend-driven. Time-driven.
      </p>

      <div class="mt-8 flex flex-col gap-3 sm:flex-row">
        <a
          href="#early-access"
          class="inline-flex items-center justify-center rounded-full bg-black px-8 py-3 text-sm font-medium text-white transition hover:bg-black/90"
        >
          Get Early Access
        </a>

        <a
          href="#about-coutera"
          class="inline-flex items-center justify-center rounded-full border border-black/15 px-8 py-3 text-sm font-medium text-black transition hover:border-black"
        >
          Learn More
        </a>
      </div>
    </section>

    <!-- Brand statement -->
    <section
      class="my-16 flex flex-col items-center justify-center gap-2 overflow-hidden py-16 text-center"
      aria-labelledby="brand-story-title"
    >
      <h2
        id="brand-story-title"
        class="max-w-4xl text-3xl font-semibold leading-tight text-black sm:text-4xl md:text-5xl"
      >
        Not mass fashion. Archive only.
      </h2>

      <p class="mt-4 max-w-2xl text-sm leading-7 text-black/75 sm:text-base">
        Focused on rare garments, designer archive, and pieces with cultural weight.
      </p>
    </section>

    <!-- About / positioning -->
    <section
      id="about-coutera"
      class="grid gap-10 border-y border-black/10 py-16 md:grid-cols-2 md:gap-16"
    >
      <div>
        <p class="text-xs uppercase tracking-[0.3em] text-black/45">
          About
        </p>

        <h2 class="mt-4 text-2xl font-semibold tracking-tight text-black sm:text-3xl">
          Built for archive fashion
        </h2>
      </div>

      <div class="space-y-5 text-sm leading-7 text-black/75 sm:text-base">
        <p>
          Coutera is designed for people who value rare fashion, designer archive,
          and pieces that define identity — not fast-moving trends.
        </p>

        <p>
          Every listing is meant to carry weight — from archival garments to rare releases
          that are no longer accessible through traditional retail.
        </p>
      </div>
    </section>

    <!-- Values -->
    <section class="py-16">
      <div class="mb-10 text-center">
        <p class="text-xs uppercase tracking-[0.3em] text-black/45">
          Why Coutera
        </p>

        <h2 class="mt-4 text-2xl font-semibold tracking-tight text-black sm:text-3xl">
          Built around rarity and archive value
        </h2>
      </div>

      <div class="grid gap-6 md:grid-cols-3">
        <div class="rounded-2xl border border-black/10 p-6 transition hover:border-black/30">
          <h3 class="text-lg font-semibold text-black">Archive</h3>
          <p class="mt-3 text-sm leading-7 text-black/70">
            Focus on designer archive and past-season pieces that define fashion history.
          </p>
        </div>

        <div class="rounded-2xl border border-black/10 p-6 transition hover:border-black/30">
          <h3 class="text-lg font-semibold text-black">Rare</h3>
          <p class="mt-3 text-sm leading-7 text-black/70">
            Limited pieces with real scarcity — not mass-produced inventory.
          </p>
        </div>

        <div class="rounded-2xl border border-black/10 p-6 transition hover:border-black/30">
          <h3 class="text-lg font-semibold text-black">Market-driven</h3>
          <p class="mt-3 text-sm leading-7 text-black/70">
            Value is defined by demand, not tags or retail pricing.
          </p>
        </div>
      </div>
    </section>

    <!-- Second hero -->
    <section class="my-8 py-16 text-center">
      <h2 class="mx-auto max-w-4xl text-3xl font-semibold leading-tight text-black sm:text-4xl md:text-5xl">
        Archive pieces. Real demand.
      </h2>

      <p class="mx-auto mt-4 max-w-2xl text-sm leading-7 text-black/75 sm:text-base">
        Built for those who understand fashion beyond seasons.
      </p>
    </section>

    <!-- Early access -->
    <section
      id="early-access"
      class="my-16 rounded-3xl border border-black/10 bg-black px-6 py-12 text-center text-white sm:px-10 sm:py-16"
    >
      <p class="text-xs uppercase tracking-[0.3em] text-white/50">
        Early Access
      </p>

      <h2 class="mx-auto mt-4 max-w-3xl text-3xl font-semibold leading-tight sm:text-4xl">
        Access rare fashion before it disappears
      </h2>

      <p class="mx-auto mt-4 max-w-2xl text-sm leading-7 text-white/75 sm:text-base">
        Join early and be among the first to explore archive garments, rare pieces,
        and curated auctions on Coutera.
      </p>

      <form
        action="https://formspree.io/f/mkoqkokp"
        method="POST"
        class="mx-auto mt-8 max-w-2xl space-y-4"
      >
        <div class="flex flex-col gap-3 sm:flex-row">
          <input
            type="email"
            name="email"
            required
            placeholder="Enter your email"
            class="w-full rounded-full border border-white/20 bg-white px-5 py-3 text-sm text-black outline-none placeholder:text-black/40"
          />

          <button
            type="submit"
            class="inline-flex items-center justify-center rounded-full bg-white px-6 py-3 text-sm font-medium text-black transition hover:bg-white/90"
          >
            Request Access
          </button>
        </div>

        <p class="text-xs text-white/50">
          By submitting this form, you agree to be contacted about early access and platform updates.
        </p>
      </form>
    </section>

    <!-- Footer text / SEO -->
    <section class="mx-auto mt-20 max-w-3xl text-center">
      <p class="text-sm leading-7 text-black/60">
        Coutera is a curated online auction marketplace focused on rare fashion,
        designer archive, and collectible garments. The platform is being developed
        to offer a refined, minimal, and demand-driven experience for discovering
        and acquiring archive pieces.
      </p>
    </section>

    <!-- Footer -->
    <footer class="mt-16 border-t border-black/10 pt-8">
      <div class="flex flex-col items-center justify-between gap-4 text-sm text-black/55 sm:flex-row">
        <p>© 2026 Coutera.</p>
        <p>Riga, Latvia. European Union</p>
      </div>
    </footer>
  </div>
</template>