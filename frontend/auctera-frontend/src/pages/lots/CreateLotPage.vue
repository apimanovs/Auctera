<script setup lang="ts">
import { computed } from "vue"

import SellIntroStep from "@/components/sell/SellIntroStep.vue"
import SellPhotosStep from "@/components/sell/SellPhotosStep.vue"
import SellBasicInfoStep from "@/components/sell/SellBasicInfoStep.vue"
import SellConditionStep from "@/components/sell/SellConditionStep.vue"
import SellPricingStep from "@/components/sell/SellPricingStep.vue"
import SellDetailsStep from "@/components/sell/SellDetailsStep.vue"
import SellReviewStep from "@/components/sell/SellReviewStep.vue"
import {
  CATEGORY_OPTIONS,
  CONDITION_OPTIONS,
  GENDER_OPTIONS,
  getOptionLabel,
  SIZE_OPTIONS,
} from "@/features/lots/create-lot/options"
import { useCreateLotFlow } from "@/features/lots/create-lot/useCreateLotFlow"

const {
  form,
  photos,
  currentStep,
  isFirstStep,
  isLastStep,
  visibleStepNumber,
  progressPercent,
  isSubmitting,
  isUploadingPhotos,
  errorMessage,
  successMessage,
  handleStart,
  handleFilesSelected,
  removePhoto,
  goToNextStep,
  goToPreviousStep,
  handleSubmit,
} = useCreateLotFlow()

const reviewForm = computed(() => ({
  ...form,
  category: getOptionLabel(form.category, CATEGORY_OPTIONS),
  gender: getOptionLabel(form.gender, GENDER_OPTIONS),
  size: getOptionLabel(form.size, SIZE_OPTIONS),
  condition: getOptionLabel(form.condition, CONDITION_OPTIONS),
}))
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
        :form="reviewForm"
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
