<script setup lang="ts">
type UploadedPhoto = {
  id: string
  key: string
  previewUrl: string
  fileName: string
}

const props = defineProps<{
  photos: UploadedPhoto[]
  isUploading?: boolean
}>()

const emit = defineEmits<{
  (e: "files-selected", files: FileList): void
  (e: "remove-photo", photoId: string): void
}>()

const handleFileChange = (event: Event) => {
  const input = event.target as HTMLInputElement
  const files = input.files

  if (!files || files.length === 0) {
    return
  }

  emit("files-selected", files)
  input.value = ""
}

const removePhoto = (photoId: string) => {
  emit("remove-photo", photoId)
}
</script>

<template>
  <section class="rounded-[28px] border bg-background p-6 sm:p-8">
    <div>
      <p class="text-xs uppercase tracking-[0.22em] text-foreground/50">
        Step 1
      </p>

      <h2 class="mt-3 text-2xl font-semibold tracking-tight sm:text-3xl">
        Upload photos
      </h2>

      <p class="mt-3 text-sm leading-6 text-foreground/70 sm:text-base">
        Add clear and well-lit photos of your item. Better photos make your listing
        stronger and easier to review.
      </p>
    </div>

    <label
      class="mt-8 flex min-h-[220px] cursor-pointer flex-col items-center justify-center rounded-[24px] border border-dashed px-6 py-10 text-center transition hover:bg-foreground/5"
    >
      <span class="text-sm font-medium">
        {{ isUploading ? "Uploading..." : "Click to upload photos" }}
      </span>

      <span class="mt-2 text-sm text-foreground/60">
        PNG, JPG, WEBP up to 10MB
      </span>

      <span class="mt-2 text-xs text-foreground/50">
        You can select multiple images
      </span>

      <input
        type="file"
        multiple
        accept="image/png,image/jpeg,image/webp"
        class="hidden"
        @change="handleFileChange"
      />
    </label>

    <div v-if="photos.length > 0" class="mt-8">
      <div class="mb-4 flex items-center justify-between gap-4">
        <div>
          <h3 class="text-sm font-medium">Uploaded photos</h3>
          <p class="mt-1 text-sm text-foreground/60">
            {{ photos.length }} photo{{ photos.length === 1 ? "" : "s" }} added
          </p>
        </div>
      </div>

      <div class="grid grid-cols-2 gap-4 sm:grid-cols-3 lg:grid-cols-4">
        <article
          v-for="photo in photos"
          :key="photo.id"
          class="group overflow-hidden rounded-[24px] border bg-background"
        >
          <div class="aspect-[4/5] w-full overflow-hidden bg-foreground/5">
            <img
              :src="photo.previewUrl"
              :alt="photo.fileName"
              class="h-full w-full object-cover transition duration-300 group-hover:scale-[1.02]"
            />
          </div>

          <div class="p-3">
            <p class="truncate text-xs text-foreground/60">
              {{ photo.fileName }}
            </p>

            <button
              type="button"
              class="mt-3 inline-flex items-center justify-center rounded-xl border px-3 py-2 text-xs font-medium transition hover:bg-foreground hover:text-background"
              @click="removePhoto(photo.id)"
            >
              Remove
            </button>
          </div>
        </article>
      </div>
    </div>

    <div v-else class="mt-8 rounded-[24px] border border-dashed p-5 text-sm text-foreground/60">
      No photos uploaded yet.
    </div>
  </section>
</template>