<script setup lang="ts">
defineProps<{
  title?: string
  message?: string
  confirmText?: string
  cancelText?: string
  isLoading?: boolean
}>()

const emit = defineEmits<{
  (e: "cancel"): void
  (e: "confirm"): void
}>()
</script>

<template>
  <div class="fixed inset-0 z-50 flex items-center justify-center px-4">
    <div
        class="absolute inset-0 bg-gradient-to-br backdrop-blur-md transition-all duration-300"
        @click="emit('cancel')"
    />
    <div class="relative z-10 w-full max-w-md rounded-[28px] border bg-background p-6 shadow-xl">
      <h2 class="text-xl font-semibold tracking-tight">
        {{ title ?? "Delete lot?" }}
      </h2>

      <p class="mt-3 text-sm leading-6 text-foreground/70">
        {{
          message ??
          "Are you sure you want to delete this lot? This action cannot be undone."
        }}
      </p>

      <div class="mt-6 flex justify-end gap-3">
        <button
          type="button"
          class="rounded-2xl border px-5 py-3 text-sm font-medium transition hover:cursor-pointer disabled:cursor-not-allowed disabled:opacity-50"
          :disabled="isLoading"
          @click="emit('cancel')"
        >
          {{ cancelText ?? "Cancel" }}
        </button>

        <button
          type="button"
          class="rounded-2xl bg-red-600 px-5 py-3 text-sm font-medium text-white transition hover:cursor-pointer hover:bg-red-700 disabled:cursor-not-allowed disabled:opacity-50"
          :disabled="isLoading"
          @click="emit('confirm')"
        >
          {{ isLoading ? "Deleting..." : confirmText ?? "Delete" }}
        </button>
      </div>
    </div>
  </div>
</template>