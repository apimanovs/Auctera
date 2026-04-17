export type SellStepKey =
  | "intro"
  | "photos"
  | "basic"
  | "condition"
  | "pricing"
  | "details"
  | "review"

export type UploadedPhoto = {
  id: string
  key: string
  previewUrl: string
  fileName: string
}

export type CreateLotPayload = {
  title: string
  description: string
  amount: number
  currency: string
  category: number
  gender: number
  size: number
  brand: string
  condition: number
  color: string
  photoKeys: string[]
}

export type CreateLotResponse = string

export type CreateLotFormState = {
  title: string
  description: string
  amount: number | null
  currency: string
  category: number | null
  gender: number | null
  size: number | null
  brand: string
  condition: number | null
  color: string
  photoKeys: string[]
}

export const SELL_STEPS: SellStepKey[] = [
  "intro",
  "photos",
  "basic",
  "condition",
  "pricing",
  "details",
  "review",
]

export const CATEGORY_OPTIONS = [
  { label: "Tops", value: 0 },
  { label: "Bottoms", value: 1 },
  { label: "Outerwear", value: 2 },
  { label: "Footwear", value: 3 },
  { label: "Accessories", value: 4 },
  { label: "Other", value: 5 },
]

export const GENDER_OPTIONS = [
  { label: "Women", value: 1 },
  { label: "Men", value: 2 },
  { label: "Unisex", value: 3 },
]

export const SIZE_OPTIONS = [
  { label: "XXS", value: 0 },
  { label: "XS", value: 1 },
  { label: "S", value: 2 },
  { label: "M", value: 3 },
  { label: "L", value: 4 },
  { label: "XL", value: 5 },
  { label: "XXL", value: 6 },
]

export const CONDITION_OPTIONS = [
  { label: "New", value: 0 },
  { label: "Worn", value: 1 },
  { label: "Refurbished", value: 2 },
]

export const DEFAULT_CREATE_LOT_FORM: CreateLotFormState = {
  title: "",
  description: "",
  amount: null,
  currency: "EUR",
  category: null,
  gender: null,
  size: null,
  brand: "",
  condition: null,
  color: "",
  photoKeys: [],
}