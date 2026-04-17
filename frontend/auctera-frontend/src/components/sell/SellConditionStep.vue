<script setup lang="ts">
import type { NumericOption } from "@/features/lots/create-lot/options"

type ConditionForm = {
  condition: number | null
  color: string
}

defineProps<{
  form: ConditionForm
  conditionOptions: NumericOption[]
}>()
</script>

<template>
  <section class="rounded-[28px] border bg-background p-6 sm:p-8">
    <div>
      <p class="text-xs uppercase tracking-[0.22em] text-foreground/50">
        Step 3
      </p>

      <h2 class="mt-3 text-2xl font-semibold tracking-tight sm:text-3xl">
        Condition and appearance
      </h2>

      <p class="mt-3 text-sm leading-6 text-foreground/70 sm:text-base">
        Be accurate about the condition and visible details of the item.
      </p>
    </div>

    <div class="mt-8 grid grid-cols-1 gap-6 md:grid-cols-2">
      <div>
        <label class="block text-sm font-medium">Condition</label>
        <select
          v-model="form.condition"
          class="mt-2 w-full rounded-2xl border bg-background px-4 py-3 text-sm outline-none transition focus:ring-2 focus:ring-foreground/20"
        >
          <option :value="null" disabled>
            Select condition
          </option>
          <option
            v-for="option in conditionOptions"
            :key="option.value"
            :value="option.value"
          >
            {{ option.label }}
          </option>
        </select>
      </div>

      <div>
        <label class="block text-sm font-medium">Color</label>
        <input
          v-model="form.color"
          type="text"
          placeholder="e.g. Black"
          class="mt-2 w-full rounded-2xl border bg-background px-4 py-3 text-sm outline-none transition placeholder:text-foreground/40 focus:ring-2 focus:ring-foreground/20"
        />
      </div>
    </div>
  </section>
</template>
