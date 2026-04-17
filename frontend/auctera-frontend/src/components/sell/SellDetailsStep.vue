<script setup lang="ts">
type DetailsForm = {
  description: string
}

defineProps<{
  form: DetailsForm
}>()
</script>

<template>
  <section class="rounded-[28px] border bg-background p-6 sm:p-8">
    <div>
      <p class="text-xs uppercase tracking-[0.22em] text-foreground/50">
        Step 5
      </p>

      <h2 class="mt-3 text-2xl font-semibold tracking-tight sm:text-3xl">
        Additional details
      </h2>

      <p class="mt-3 text-sm leading-6 text-foreground/70 sm:text-base">
        Describe the item clearly so buyers understand what they are looking at.
      </p>
    </div>

    <div class="mt-8">
      <label class="block text-sm font-medium">Description</label>
      <textarea
        v-model="form.description"
        rows="8"
        placeholder="Describe the item, fit, visible flaws, materials, or anything buyers should know."
        class="mt-2 w-full rounded-2xl border bg-background px-4 py-3 text-sm outline-none transition placeholder:text-foreground/40 focus:ring-2 focus:ring-foreground/20"
      />
    </div>
  </section>
</template>