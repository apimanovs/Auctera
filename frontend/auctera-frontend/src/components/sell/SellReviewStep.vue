<script setup lang="ts">
type UploadedPhoto = {
  id: string
  key: string
  previewUrl: string
  fileName: string
}

type ReviewForm = {
  title: string
  description: string
  amount: number | null
  currency: string
  category: string
  gender: string
  size: string
  brand: string
  condition: string
  color: string
}

const props = defineProps<{
  form: ReviewForm
  photos: UploadedPhoto[]
}>()

console.log(props);
</script>

<template>
  <section class="rounded-[28px] border bg-background p-6 sm:p-8">
    <div>
      <p class="text-xs uppercase tracking-[0.22em] text-foreground/50">
        Step 6
      </p>

      <h2 class="mt-3 text-2xl font-semibold tracking-tight sm:text-3xl">
        Review your listing
      </h2>

      <p class="mt-3 text-sm leading-6 text-foreground/70 sm:text-base">
        Check everything before submitting your item for review.
      </p>
    </div>

    <div class="mt-8 grid grid-cols-1 gap-8 lg:grid-cols-[1.1fr_0.9fr]">
      <div>
        <div
          v-if="photos.length > 0"
          class="grid grid-cols-2 gap-4 sm:grid-cols-3"
        >
          <article
            v-for="photo in photos"
            :key="photo.id"
            class="overflow-hidden rounded-[24px] border"
          >
            <div class="aspect-[4/5] w-full overflow-hidden bg-foreground/5">
              <img
                :src="photo.previewUrl"
                :alt="photo.fileName"
                class="h-full w-full object-cover"
              />
            </div>
          </article>
        </div>

        <div
          v-else
          class="rounded-[24px] border border-dashed p-5 text-sm text-foreground/60"
        >
          No photos uploaded.
        </div>
      </div>

      <div class="space-y-4">
        <div class="rounded-[24px] border p-5">
          <p class="text-xs uppercase tracking-[0.16em] text-foreground/50">
            Item
          </p>
          <h3 class="mt-2 text-xl font-semibold">
            {{ form.title || "Untitled item" }}
          </h3>
          <p class="mt-2 text-sm text-foreground/70">
            {{ form.brand || "No brand selected" }}
          </p>
        </div>

        <div class="rounded-[24px] border p-5">
          <p class="text-sm font-medium">Listing details</p>

          <div class="mt-4 space-y-3 text-sm text-foreground/70">
            <div class="flex items-start justify-between gap-4">
              <span>Category</span>
              <span class="text-right text-foreground">{{ form.category || "-" }}</span>
            </div>

            <div class="flex items-start justify-between gap-4">
              <span>Gender</span>
              <span class="text-right text-foreground">{{ props.form.gender || "-" }}</span>
            </div>

            <div class="flex items-start justify-between gap-4">
              <span>Size</span>
              <span class="text-right text-foreground">{{ form.size || "-" }}</span>
            </div>

            <div class="flex items-start justify-between gap-4">
              <span>Condition</span>
              <span class="text-right text-foreground">{{ form.condition || "-" }}</span>
            </div>

            <div class="flex items-start justify-between gap-4">
              <span>Color</span>
              <span class="text-right text-foreground">{{ form.color || "-" }}</span>
            </div>

            <div class="flex items-start justify-between gap-4">
              <span>Price</span>
              <span class="text-right text-foreground">
                {{ form.amount ? `${form.amount} ${form.currency}` : "-" }}
              </span>
            </div>
          </div>
        </div>

        <div class="rounded-[24px] border p-5">
          <p class="text-sm font-medium">Description</p>
          <p class="mt-3 whitespace-pre-line text-sm leading-6 text-foreground/70">
            {{ form.description || "No description provided." }}
          </p>
        </div>
      </div>
    </div>
  </section>
</template>