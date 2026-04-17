<script setup lang="ts">
const emit = defineEmits<{
  (e: "start"): void
}>()

const handleStart = () => {
  emit("start")
}
</script>

<template>
  <section class="rounded-[32px] border bg-background px-6 py-8 sm:px-8 sm:py-10 lg:px-12 lg:py-14">
    <div class="mx-auto max-w-6xl">
      <!-- Top / hero -->
      <div class="grid grid-cols-1 gap-10 lg:grid-cols-12 lg:items-end">
        <div class="lg:col-span-8">
          <p class="text-[11px] uppercase tracking-[0.24em] text-foreground/45">
            Sell on Coutera
          </p>

          <h2 class="mt-4 max-w-4xl text-3xl font-semibold tracking-tight sm:text-4xl lg:text-5xl">
            A better way to sell pieces that deserve attention
          </h2>
        </div>
      </div>

      <!-- Steps -->
      <div class="mt-12 grid grid-cols-1 gap-5 md:grid-cols-3">
        <div class="rounded-[24px] border p-6">
          <p class="text-[11px] uppercase tracking-[0.2em] text-foreground/40">
            Step 01
          </p>
          <h3 class="mt-3 text-base font-medium">Create your listing</h3>
          <p class="mt-3 text-sm leading-6 text-foreground/70">
            Add strong photos, write a clear title, and include the details buyers expect.
          </p>
        </div>

        <div class="rounded-[24px] border p-6">
          <p class="text-[11px] uppercase tracking-[0.2em] text-foreground/40">
            Step 02
          </p>
          <h3 class="mt-3 text-base font-medium">Reviewed before publishing</h3>
          <p class="mt-3 text-sm leading-6 text-foreground/70">
            Each submission is checked before it appears publicly to keep the marketplace curated.
          </p>
        </div>

        <div class="rounded-[24px] border p-6">
          <p class="text-[11px] uppercase tracking-[0.2em] text-foreground/40">
            Step 03
          </p>
          <h3 class="mt-3 text-base font-medium">Go live to the right audience</h3>
          <p class="mt-3 text-sm leading-6 text-foreground/70">
            Once approved, your piece becomes visible to buyers looking for something distinctive.
          </p>
        </div>
      </div>

      <!-- Bottom section -->
      <div class="mt-12 grid grid-cols-1 gap-10 lg:grid-cols-12">

        <div class="lg:col-span-7">
          <p class="text-[11px] uppercase tracking-[0.2em] text-foreground/40">
            Listing guidelines
          </p>

          <div class="mt-5 grid grid-cols-1 gap-x-10 gap-y-4 sm:grid-cols-2">
            <div class="flex items-start gap-3">
              <span class="mt-2 h-1.5 w-1.5 rounded-full bg-foreground/40" />
              <p class="text-sm leading-6 text-foreground/70">
                Only authentic items are allowed
              </p>
            </div>

            <div class="flex items-start gap-3">
              <span class="mt-2 h-1.5 w-1.5 rounded-full bg-foreground/40" />
              <p class="text-sm leading-6 text-foreground/70">
                Use clear, well-lit photos that show the item properly
              </p>
            </div>

        <div class="flex items-start gap-3">
          <span class="mt-2 h-1.5 w-1.5 rounded-full bg-foreground/40" />
          <p class="text-sm leading-6 text-foreground/70">
            Describe the condition honestly, including visible flaws or wear
          </p>
        </div>

        <div class="flex items-start gap-3">
          <span class="mt-2 h-1.5 w-1.5 rounded-full bg-foreground/40" />
          <p class="text-sm leading-6 text-foreground/70">
            Provide accurate item details such as brand, size, color, and category
          </p>
        </div>

        <div class="flex items-start gap-3">
          <span class="mt-2 h-1.5 w-1.5 rounded-full bg-foreground/40" />
          <p class="text-sm leading-6 text-foreground/70">
            Use a clean and specific title that matches the piece
          </p>
        </div>

        <div class="flex items-start gap-3">
          <span class="mt-2 h-1.5 w-1.5 rounded-full bg-foreground/40" />
          <p class="text-sm leading-6 text-foreground/70">
            Listings may be reviewed, approved, or declined before going live
          </p>
        </div>
      </div>
    </div>
        <div class="lg:col-span-5 lg:pl-8">
          <p class="text-[11px] uppercase tracking-[0.2em] text-foreground/40">
            Before you begin
          </p>

          <p class="mt-5 text-sm leading-7 text-foreground/70 sm:text-base">
            The best listings feel intentional from the start. Prepare a few strong photos,
            gather the key item details, and present the piece as clearly as possible.
          </p>

          <p class="mt-4 text-sm leading-7 text-foreground/70 sm:text-base">
            Better presentation builds trust. Better trust attracts better buyers.
          </p>

          <div class="mt-8 flex flex-wrap items-center gap-4">
            <button
              type="button"
              class="inline-flex items-center justify-center rounded-2xl bg-foreground px-5 py-3 text-sm font-medium text-background transition hover:opacity-90"
              @click="handleStart"
            >
              Start listing
            </button>

            <span class="text-sm text-foreground/55">
              Curated submissions only
            </span>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>