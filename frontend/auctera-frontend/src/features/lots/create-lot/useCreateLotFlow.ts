import { computed, reactive, ref } from "vue"

import { mediaService } from "@/app/services/mediaService"
import { itemService } from "@/app/services/lotService"
import {
  DEFAULT_CREATE_LOT_FORM,
  SELL_STEPS,
  type CreateLotFormState,
  type SellStepKey,
  type UploadedPhoto,
} from "@/types/createLot"

export const useCreateLotFlow = () => {
  const currentStepIndex = ref(0)
  const form = reactive<CreateLotFormState>({
    ...DEFAULT_CREATE_LOT_FORM,
  })
  const photos = ref<UploadedPhoto[]>([])

  const isSubmitting = ref(false)
  const isUploadingPhotos = ref(false)
  const errorMessage = ref("")
  const successMessage = ref("")

  const currentStep = computed<SellStepKey>(() => SELL_STEPS[currentStepIndex.value] ?? "intro")
  const isFirstStep = computed(() => currentStepIndex.value === 0)
  const isLastStep = computed(() => currentStepIndex.value === SELL_STEPS.length - 1)

  const visibleStepNumber = computed(() => (currentStep.value === "intro" ? 0 : currentStepIndex.value))
  const progressPercent = computed(() => {
    if (currentStep.value === "intro") {
      return 0
    }

    return Math.round((currentStepIndex.value / (SELL_STEPS.length - 1)) * 100)
  })

  const uploadedPhotoKeys = computed(() =>
    photos.value.map((photo) => photo.key).filter((key): key is string => Boolean(key)),
  )

  const clearMessages = () => {
    errorMessage.value = ""
    successMessage.value = ""
  }

  const handleStart = () => {
    clearMessages()
    currentStepIndex.value = 1
  }

  const handleFilesSelected = async (files: FileList) => {
    clearMessages()

    const selectedFiles = Array.from(files)

    if (selectedFiles.length === 0) {
      return
    }

    isUploadingPhotos.value = true

    try {
      for (const file of selectedFiles) {
        const previewUrl = URL.createObjectURL(file)
        const photoId = crypto.randomUUID()

        photos.value.push({
          id: photoId,
          key: "",
          previewUrl,
          fileName: file.name,
        })

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
    clearMessages()

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
    clearMessages()

    if (currentStepIndex.value > 0) {
      currentStepIndex.value -= 1
    }
  }

  const handleSubmit = async () => {
    clearMessages()

    if (!validateCurrentStep()) {
      return
    }

    if (
      form.amount === null ||
      form.category === null ||
      form.gender === null ||
      form.size === null ||
      form.condition === null
    ) {
      errorMessage.value = "Please fill in all required fields before submit."
      return
    }

    isSubmitting.value = true

    try {
      const payload = {
        title: form.title.trim(),
        description: form.description.trim(),
        amount: form.amount,
        currency: form.currency.trim(),
        category: form.category,
        gender: form.gender,
        size: form.size,
        brand: form.brand.trim(),
        condition: form.condition,
        color: form.color.trim(),
        photoKeys: uploadedPhotoKeys.value,
      }

      const lotId = await itemService.createLot(payload)
      successMessage.value = `Listing created successfully. Lot ID: ${lotId}`
    } catch (error) {
      console.error("Failed to create listing", error)
      errorMessage.value = "Failed to submit listing."
    } finally {
      isSubmitting.value = false
    }
  }

  return {
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
  }
}
