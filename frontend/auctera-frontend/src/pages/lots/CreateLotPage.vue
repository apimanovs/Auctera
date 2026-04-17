<script setup lang="ts">
import { computed, reactive, ref } from "vue"

import SellIntroStep from "@/components/sell/SellIntroStep.vue"
import SellPhotosStep from "@/components/sell/SellPhotosStep.vue"
import SellBasicInfoStep from "@/components/sell/SellBasicInfoStep.vue"
import SellConditionStep from "@/components/sell/SellConditionStep.vue"
import SellPricingStep from "@/components/sell/SellPricingStep.vue"
import SellDetailsStep from "@/components/sell/SellDetailsStep.vue"
import SellReviewStep from "@/components/sell/SellReviewStep.vue"

import { mediaService } from "@/app/services/mediaService"
import { itemService } from "@/app/services/lotService"

import {
  CATEGORY_OPTIONS,
  CONDITION_OPTIONS,
  DEFAULT_CREATE_LOT_FORM,
  GENDER_OPTIONS,
  SELL_STEPS,
  SIZE_OPTIONS,
  type CreateLotFormState,
  type SellStepKey,
  type UploadedPhoto,
} from "@/types/createLot"

const currentStepIndex = ref(0)

const form = reactive<CreateLotFormState>({
  ...DEFAULT_CREATE_LOT_FORM,
})

const photos = ref<UploadedPhoto[]>([])

const isSubmitting = ref(false)
const isUploadingPhotos = ref(false)
const errorMessage = ref("")
const successMessage = ref("")

const currentStep = computed<SellStepKey>(() => {
  return SELL_STEPS[currentStepIndex.value] ?? "intro"
})

const isFirstStep = computed(() => currentStepIndex.value === 0)
const isLastStep = computed(() => currentStepIndex.value === SELL_STEPS.length - 1)

const visibleStepNumber = computed(() => {
  if (currentStep.value === "intro") {
    return 0
  }

  return currentStepIndex.value
})

const progressPercent = computed(() => {
  if (currentStep.value === "intro") {
    return 0
  }

  return Math.round((currentStepIndex.value / (SELL_STEPS.length - 1)) * 100)
})

const uploadedPhotoKeys = computed(() => {
  return photos.value
    .map((photo) => photo.key)
    .filter((key): key is string => Boolean(key))
})

const handleStart = () => {
  errorMessage.value = ""
  successMessage.value = ""
  currentStepIndex.value = 1
}

const handleFilesSelected = async (files: FileList) => {
  errorMessage.value = ""
  successMessage.value = ""

  const selectedFiles = Array.from(files)

  if (selectedFiles.length === 0) {
    return
  }

  isUploadingPhotos.value = true

  try {
    for (const file of selectedFiles) {
      const previewUrl = URL.createObjectURL(file)
      const photoId = crypto.randomUUID()

      const tempPhoto: UploadedPhoto = {
        id: photoId,
        key: "",
        previewUrl,
        fileName: file.name,
      }

      photos.value.push(tempPhoto)

      try {
        const response = await mediaService.uploadPhoto(file)

        const targetPhoto = photos.value.find((photo) => photo.id === photoId)

        if (targetPhoto) {
          targetPhoto.key = response.key
        }
      } catch (error) {
        console.error("Failed to upload photo", error)

        const failedPhoto = photos.value.find((photo) => photo.id === photoId)

        if (failedPhoto) {
          URL.revokeObjectURL(failedPhoto.previewUrl)
        }

        photos.value = photos.value.filter((photo) => photo.id !== photoId)
        errorMessage.value = `Failed to upload ${file.name}`
      }
    }

    form.photoKeys = uploadedPhotoKeys.value
  } finally {
    isUploadingPhotos.value = false
  }
}

const removePhoto = (photoId: string) => {
  errorMessage.value = ""
  successMessage.value = ""

  const photoToRemove = photos.value.find((photo) => photo.id === photoId)

  if (photoToRemove) {
    URL.revokeObjectURL(photoToRemove.previewUrl)
  }

  photos.value = photos.value.filter((photo) => photo.id !== photoId)
  form.photoKeys = uploadedPhotoKeys.value
}

const validateCurrentStep = (): boolean => {
  errorMessage.value = ""

  switch (currentStep.value) {
    case "photos":
      if (photos.value.length === 0) {
        errorMessage.value = "Please upload at least one photo."
        return false
      }

      if (isUploadingPhotos.value) {
        errorMessage.value = "Please wait until all photos finish uploading."
        return false
      }

      if (photos.value.some((photo) => !photo.key)) {
        errorMessage.value = "Some photos are not uploaded yet."
        return false
      }

      return true

    case "basic":
      if (!form.title.trim()) {
        errorMessage.value = "Title is required."
        return false
      }

      if (!form.brand.trim()) {
        errorMessage.value = "Brand is required."
        return false
      }

      if (form.category === null) {
        errorMessage.value = "Category is required."
        return false
      }

      if (form.gender === null) {
        errorMessage.value = "Gender is required."
        return false
      }

      if (form.size === null) {
        errorMessage.value = "Size is required."
        return false
      }

      return true

    case "condition":
      if (form.condition === null) {
        errorMessage.value = "Condition is required."
        return false
      }

      if (!form.color.trim()) {
        errorMessage.value = "Color is required."
        return false
      }

      return true

    case "pricing":
      if (form.amount === null || Number.isNaN(form.amount)) {
        errorMessage.value = "Amount is required."
        return false
      }

      if (form.amount <= 0) {
        errorMessage.value = "Amount must be greater than 0."
        return false
      }

      if (!form.currency.trim()) {
        errorMessage.value = "Currency is required."
        return false
      }

      return true

    case "details":
      if (!form.description.trim()) {
        errorMessage.value = "Description is required."
        return false
      }

      return true

    default:
      return true
  }
}

const goToNextStep = () => {
  successMessage.value = ""

  if (!validateCurrentStep()) {
    return
  }

  if (currentStepIndex.value < SELL_STEPS.length - 1) {
    currentStepIndex.value += 1
  }
}

const goToPreviousStep = () => {
  errorMessage.value = ""
  successMessage.value = ""

  if (currentStepIndex.value > 0) {
    currentStepIndex.value -= 1
  }
}

const handleSubmit = async () => {
  errorMessage.value = ""
  successMessage.value = ""

  if (!validateCurrentStep()) {
    return
  }

  isSubmitting.value = true

  try {
  const payload = {
      title: form.title,
      description: form.description,
      amount: form.amount!,
      currency: form.currency,
      category: typeof form.category === "object" ? form.category?.value ?? form.category : form.category,
      gender: typeof form.gender === "object" ? form.gender?.value ?? form.gender : form.gender,
      size: typeof form.size === "object" ? form.size?.value ?? form.size : form.size,
      brand: form.brand,
      condition: typeof form.condition === "object" ? form.condition?.value ?? form.condition : form.condition,
      color: form.color,
      photoKeys: uploadedPhotoKeys.value,
    }

    console.log("Create lot payload:", payload)
    console.log("category:", payload.category, typeof payload.category)
    console.log("gender:", payload.gender, typeof payload.gender)
    console.log("size:", payload.size, typeof payload.size)
    console.log("condition:", payload.condition, typeof payload.condition)

    const lotId = await itemService.createLot(payload)

    successMessage.value = `Listing created successfully. Lot ID: ${lotId}`

    // если захочешь, потом можно сделать redirect
    // await router.push(`/lots/${lotId}`)
  } catch (error) {
    console.error("Failed to create listing", error)
    errorMessage.value = "Failed to submit listing."
  } finally {
    isSubmitting.value = false
  }
}
</script>

<template>
  <div class="min-h-screen bg-background text-foreground">
    <div class="mx-auto max-w-6xl px-4 py-8 sm:px-6 lg:px-8">
      <div class="mb-8 flex flex-wrap items-start justify-between gap-4">
        <div>
          <p class="text-xs uppercase tracking-[0.22em] text-foreground/50">
            Sell on Coutera
          </p>

          <h1 class="mt-2 text-3xl font-semibold tracking-tight sm:text-4xl">
            Create listing
          </h1>

          <p class="mt-3 max-w-2xl text-sm leading-6 text-foreground/70 sm:text-base">
            Submit your item for review and prepare it to go live on the marketplace.
          </p>
        </div>

        <div
          v-if="currentStep !== 'intro'"
          class="min-w-[220px] rounded-[24px] border bg-background p-4"
        >
          <p class="text-xs uppercase tracking-[0.16em] text-foreground/50">
            Progress
          </p>

          <p class="mt-2 text-sm font-medium">
            Step {{ visibleStepNumber }} of 6
          </p>

          <div class="mt-3 h-2 overflow-hidden rounded-full bg-foreground/10">
            <div
              class="h-full rounded-full bg-foreground transition-all duration-300"
              :style="{ width: `${progressPercent}%` }"
            />
          </div>
        </div>
      </div>

      <div
        v-if="errorMessage"
        class="mb-6 rounded-[20px] border border-red-500/20 bg-red-500/10 px-4 py-3 text-sm text-red-300"
      >
        {{ errorMessage }}
      </div>

      <div
        v-if="successMessage"
        class="mb-6 rounded-[20px] border border-emerald-500/20 bg-emerald-500/10 px-4 py-3 text-sm text-emerald-300"
      >
        {{ successMessage }}
      </div>

      <SellIntroStep
        v-if="currentStep === 'intro'"
        @start="handleStart"
      />

      <SellPhotosStep
        v-else-if="currentStep === 'photos'"
        :photos="photos"
        @files-selected="handleFilesSelected"
        @remove-photo="removePhoto"
      />

      <SellBasicInfoStep
        v-else-if="currentStep === 'basic'"
        :form="form"
        :category-options="CATEGORY_OPTIONS"
        :gender-options="GENDER_OPTIONS"
        :size-options="SIZE_OPTIONS"
      />

      <SellConditionStep
        v-else-if="currentStep === 'condition'"
        :form="form"
        :condition-options="CONDITION_OPTIONS"
      />

      <SellPricingStep
        v-else-if="currentStep === 'pricing'"
        :form="form"
        :currency-options="['EUR']"
      />

      <SellDetailsStep
        v-else-if="currentStep === 'details'"
        :form="form"
      />

      <SellReviewStep
        v-else-if="currentStep === 'review'"
        :form="form"
        :photos="photos"
      />

      <div
        v-if="currentStep !== 'intro'"
        class="mt-6 flex flex-wrap items-center justify-between gap-3"
      >
        <button
          type="button"
          class="inline-flex items-center justify-center rounded-2xl border px-5 py-3 text-sm font-medium transition hover:bg-foreground hover:text-background disabled:cursor-not-allowed disabled:opacity-50"
          :disabled="isFirstStep || isSubmitting || isUploadingPhotos"
          @click="goToPreviousStep"
        >
          Back
        </button>

        <button
          v-if="!isLastStep"
          type="button"
          class="inline-flex items-center justify-center rounded-2xl bg-foreground px-5 py-3 text-sm font-medium text-background transition hover:opacity-90 disabled:cursor-not-allowed disabled:opacity-50"
          :disabled="isSubmitting || isUploadingPhotos"
          @click="goToNextStep"
        >
          {{ isUploadingPhotos ? "Uploading..." : "Continue" }}
        </button>

        <button
          v-else
          type="button"
          class="inline-flex items-center justify-center rounded-2xl bg-foreground px-5 py-3 text-sm font-medium text-background transition hover:opacity-90 disabled:cursor-not-allowed disabled:opacity-50"
          :disabled="isSubmitting || isUploadingPhotos"
          @click="handleSubmit"
        >
          {{ isSubmitting ? "Submitting..." : "Submit for review" }}
        </button>
      </div>
    </div>
  </div>
</template>