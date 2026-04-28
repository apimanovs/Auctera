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
  year: number | null
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
  year: number | null
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
  year: null,
  photoKeys: [],
}
