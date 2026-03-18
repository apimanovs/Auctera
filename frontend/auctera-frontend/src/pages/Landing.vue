<script setup lang="ts">
const heroImages = [
  'https://images.unsplash.com/photo-1515886657613-9f3515b0c78f?auto=format&fit=crop&w=1200&q=80',
  'https://images.unsplash.com/photo-1496747611176-843222e1e57c?auto=format&fit=crop&w=1200&q=80',
  'https://images.unsplash.com/photo-1529139574466-a303027c1d8b?auto=format&fit=crop&w=1200&q=80',
]

const editorialImages = [
  'https://images.unsplash.com/photo-1524504388940-b1c1722653e1?auto=format&fit=crop&w=1200&q=80',
  'https://images.unsplash.com/photo-1512436991641-6745cdb1723f?auto=format&fit=crop&w=1200&q=80',
]
</script>

<template>
  <div class="mx-auto max-w-7xl px-4 py-8 sm:px-6 lg:px-8">
    <!-- Hero -->
    <section class="grid items-center gap-10 py-12 lg:grid-cols-[1.1fr_0.9fr] lg:py-20">
      <div>
        <p class="text-[11px] uppercase tracking-[0.4em] text-black/45">
          Archive Fashion Marketplace
        </p>

        <h1 class="mt-5 max-w-4xl text-5xl font-extrabold tracking-tight text-black sm:text-6xl md:text-7xl">
          Coutera
        </h1>

        <p class="mt-6 max-w-2xl text-lg leading-8 text-black/70">
          A curated auction marketplace for rare fashion, designer archive, and hard-to-find garments.
        </p>

        <p class="mt-4 max-w-2xl text-sm leading-7 text-black/55 sm:text-base">
          Built for those who look beyond current seasons — for collectors, stylists,
          archivists, and buyers who understand cultural weight, silhouette, provenance,
          and scarcity.
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

        <div class="mt-10 grid max-w-2xl grid-cols-3 gap-4 border-t border-black/10 pt-6">
          <div>
            <p class="text-[11px] uppercase tracking-[0.2em] text-black/40">Focus</p>
            <p class="mt-2 text-sm font-medium text-black">Rare fashion</p>
          </div>
          <div>
            <p class="text-[11px] uppercase tracking-[0.2em] text-black/40">Core</p>
            <p class="mt-2 text-sm font-medium text-black">Designer archive</p>
          </div>
          <div>
            <p class="text-[11px] uppercase tracking-[0.2em] text-black/40">Model</p>
            <p class="mt-2 text-sm font-medium text-black">Demand-driven auctions</p>
          </div>
        </div>
      </div>

      <div class="grid grid-cols-2 gap-4">
        <div class="overflow-hidden rounded-[2rem] bg-black/5">
          <img
            :src="heroImages[0]"
            alt="Editorial fashion portrait"
            class="h-[320px] w-full object-cover sm:h-[420px] lg:h-[520px]"
          />
        </div>

        <div class="grid gap-4">
          <div class="overflow-hidden rounded-[2rem] bg-black/5">
            <img
              :src="heroImages[1]"
              alt="Runway inspired fashion styling"
              class="h-[152px] w-full object-cover sm:h-[202px] lg:h-[252px]"
            />
          </div>

          <div class="overflow-hidden rounded-[2rem] bg-black/5">
            <img
              :src="heroImages[2]"
              alt="Archive fashion detail"
              class="h-[152px] w-full object-cover sm:h-[202px] lg:h-[252px]"
            />
          </div>
        </div>
      </div>
    </section>

    <!-- Brand statement -->
    <section
      class="my-10 border-y border-black/10 py-16 text-center sm:my-16 sm:py-24"
      aria-labelledby="brand-story-title"
    >
      <p class="text-[11px] uppercase tracking-[0.35em] text-black/40">
        Positioning
      </p>

      <h2
        id="brand-story-title"
        class="mx-auto mt-5 max-w-5xl text-3xl font-semibold leading-tight text-black sm:text-5xl"
      >
        Not mass fashion. Not trend chasing.  
        Archive only.
      </h2>

      <p class="mx-auto mt-6 max-w-3xl text-sm leading-8 text-black/70 sm:text-lg">
        Coutera is shaped around garments with presence — pieces that hold their value
        through design language, scarcity, relevance, and memory. This is not a feed of
        disposable product. It is a space for clothing that remains.
      </p>
    </section>

    <!-- About -->
    <section
      id="about-coutera"
      class="grid gap-10 py-16 md:grid-cols-[0.85fr_1.15fr] md:gap-16"
    >
      <div>
        <p class="text-[11px] uppercase tracking-[0.35em] text-black/40">
          About Coutera
        </p>

        <h2 class="mt-4 text-3xl font-semibold tracking-tight text-black sm:text-4xl">
          Built for archive fashion
        </h2>
      </div>

      <div class="space-y-6 text-sm leading-8 text-black/75 sm:text-base">
        <p>
          Coutera is designed for people who value rare fashion, designer archive,
          and garments that define identity — not fast-moving trends, inflated hype,
          or generic inventory.
        </p>

        <p>
          Every listing should carry weight. From archival outerwear and hard-to-find
          runway references to past-season pieces no longer accessible through traditional retail,
          the platform is built to give them the setting they deserve.
        </p>

        <p>
          We are building a digital marketplace where rarity is not reduced to noise.
          The interface stays minimal, the experience stays focused, and the attention
          stays on the garment itself — its cut, era, context, and demand.
        </p>
      </div>
    </section>

    <!-- Editorial image + text -->
    <section class="grid items-center gap-10 py-8 lg:grid-cols-[1fr_0.9fr] lg:py-16">
      <div class="overflow-hidden rounded-[2rem] bg-black/5">
        <img
          :src="editorialImages[0]"
          alt="Editorial archive fashion styling"
          class="h-[300px] w-full object-cover sm:h-[420px] lg:h-[560px]"
        />
      </div>

      <div>
        <p class="text-[11px] uppercase tracking-[0.35em] text-black/40">
          Why It Matters
        </p>

        <h2 class="mt-4 max-w-2xl text-3xl font-semibold leading-tight text-black sm:text-4xl">
          Some pieces outlive their season.
        </h2>

        <div class="mt-6 space-y-5 text-sm leading-8 text-black/72 sm:text-base">
          <p>
            Archive fashion is not just about age. It is about permanence — garments
            that continue to speak through silhouette, fabrication, construction, and reference.
          </p>

          <p>
            Their value is rarely fixed. It moves through demand, condition, scarcity,
            and who is looking. Auctions make that process visible.
          </p>

          <p>
            Coutera is being built to let those pieces surface in the right environment:
            considered, competitive, and culturally aware.
          </p>
        </div>
      </div>
    </section>

    <!-- Values -->
    <section class="py-16">
      <div class="mb-12 text-center">
        <p class="text-[11px] uppercase tracking-[0.35em] text-black/40">
          Why Coutera
        </p>

        <h2 class="mt-4 text-3xl font-semibold tracking-tight text-black sm:text-4xl">
          Built around rarity and archive value
        </h2>

        <p class="mx-auto mt-4 max-w-2xl text-sm leading-8 text-black/65 sm:text-base">
          The platform is shaped for a niche audience with a sharper eye — people
          who understand fashion beyond retail cycles.
        </p>
      </div>

      <div class="grid gap-6 md:grid-cols-3">
        <div class="rounded-[1.75rem] border border-black/10 p-7 transition hover:border-black/30">
          <h3 class="text-lg font-semibold text-black">Archive</h3>
          <p class="mt-4 text-sm leading-8 text-black/70">
            Focus on designer archive and past-season garments that define fashion history,
            not just current release cycles.
          </p>
        </div>

        <div class="rounded-[1.75rem] border border-black/10 p-7 transition hover:border-black/30">
          <h3 class="text-lg font-semibold text-black">Rare</h3>
          <p class="mt-4 text-sm leading-8 text-black/70">
            Limited pieces with real scarcity — hard to source, hard to replace,
            and meaningful because they are not everywhere.
          </p>
        </div>

        <div class="rounded-[1.75rem] border border-black/10 p-7 transition hover:border-black/30">
          <h3 class="text-lg font-semibold text-black">Market-driven</h3>
          <p class="mt-4 text-sm leading-8 text-black/70">
            Value is defined by demand, not retail tags. Auctions expose what a piece
            is actually worth to those who want it most.
          </p>
        </div>
      </div>
    </section>

    <!-- Second hero -->
    <section class="py-16 text-center sm:py-24">
      <p class="text-[11px] uppercase tracking-[0.35em] text-black/40">
        Editorial Direction
      </p>

      <h2 class="mx-auto mt-4 max-w-5xl text-3xl font-semibold leading-tight text-black sm:text-5xl">
        Archive pieces. Real demand.  
        A quieter kind of luxury.
      </h2>

      <p class="mx-auto mt-6 max-w-3xl text-sm leading-8 text-black/72 sm:text-base">
        Coutera is not built around excess. It is built around selectivity —
        garments with history, form, scarcity, and relevance beyond trend.
      </p>
    </section>

    <!-- Image + quote -->
    <section class="grid items-center gap-10 py-8 lg:grid-cols-[0.95fr_1.05fr] lg:py-16">
      <div>
        <p class="text-[11px] uppercase tracking-[0.35em] text-black/40">
          Fashion Memory
        </p>

        <h2 class="mt-4 max-w-2xl text-3xl font-semibold leading-tight text-black sm:text-4xl">
          The right garment changes how a wardrobe is remembered.
        </h2>

        <div class="mt-6 space-y-5 text-sm leading-8 text-black/72 sm:text-base">
          <p>
            The strongest archive pieces do more than complete an outfit.
            They anchor a wardrobe. They become a point of reference.
          </p>

          <p>
            That is the kind of value Coutera is interested in: clothing with presence,
            history, and enough scarcity to matter.
          </p>
        </div>
      </div>

      <div class="overflow-hidden rounded-[2rem] bg-black/5">
        <img
          :src="editorialImages[1]"
          alt="Luxury fashion runway inspired styling"
          class="h-[300px] w-full object-cover sm:h-[420px] lg:h-[560px]"
        />
      </div>
    </section>

    <!-- Early access -->
    <section
      id="early-access"
      class="my-16 rounded-[2rem] border border-black/10 bg-black px-6 py-12 text-center text-white sm:px-10 sm:py-16"
    >
      <p class="text-[11px] uppercase tracking-[0.35em] text-white/50">
        Early Access
      </p>

      <h2 class="mx-auto mt-4 max-w-3xl text-3xl font-semibold leading-tight sm:text-4xl">
        Access rare fashion before it disappears
      </h2>

      <p class="mx-auto mt-4 max-w-2xl text-sm leading-8 text-white/75 sm:text-base">
        Join early and be among the first to explore archive garments, rare fashion,
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

        <p class="text-xs leading-6 text-white/50">
          By submitting this form, you agree to be contacted about early access and future platform updates.
        </p>
      </form>
    </section>

    <!-- Footer text / SEO -->
    <section class="mx-auto mt-20 max-w-4xl text-center">
      <p class="text-sm leading-8 text-black/60">
        Coutera is a curated online auction marketplace focused on rare fashion,
        designer archive, and hard-to-find garments. The platform is being developed
        to offer a refined, minimal, and demand-driven experience for discovering,
        valuing, and acquiring archive pieces.
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