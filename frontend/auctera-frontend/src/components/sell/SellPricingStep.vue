<script setup lang="ts">
type PricingForm = {
  amount: number | null
  currency: string
}

defineProps<{
  form: PricingForm
  currencyOptions?: string[]
}>()
</script>

<template>
  <section class="rounded-[28px] border bg-background p-6 sm:p-8">
    <div>
      <p class="text-xs uppercase tracking-[0.22em] text-foreground/50">
        Step 4
      </p>

      <h2 class="mt-3 text-2xl font-semibold tracking-tight sm:text-3xl">
        Pricing
      </h2>

      <p class="mt-3 text-sm leading-6 text-foreground/70 sm:text-base">
        Set the amount for your listing.
      </p>
    </div>

    <div class="mt-8 grid grid-cols-1 gap-6 md:grid-cols-2">
      <div>
        <label class="block text-sm font-medium">Amount</label>
        <input
          v-model.number="form.amount"
          type="number"
          min="0"
          step="0.01"
          placeholder="e.g. 250"
          class="mt-2 w-full rounded-2xl border bg-background px-4 py-3 text-sm outline-none transition placeholder:text-foreground/40 focus:ring-2 focus:ring-foreground/20"
        />
      </div>

      <div>
        <label class="block text-sm font-medium">Currency</label>
        <select
          v-model="form.currency"
          class="mt-2 w-full rounded-2xl border bg-background px-4 py-3 text-sm outline-none transition focus:ring-2 focus:ring-foreground/20"
        >
          <option
            v-for="currency in currencyOptions ?? ['EUR']"
            :key="currency"
            :value="currency"
          >
            {{ currency }}
          </option>
        </select>
      </div>
    </div>
  </section>
</template>