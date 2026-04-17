export type NumericOption = {
  label: string
  value: number
}

export const CATEGORY_OPTIONS: NumericOption[] = [
  { label: "Tops", value: 0 },
  { label: "Bottoms", value: 1 },
  { label: "Outerwear", value: 2 },
  { label: "Footwear", value: 3 },
  { label: "Accessories", value: 4 },
  { label: "Other", value: 5 },
]

export const GENDER_OPTIONS: NumericOption[] = [
  { label: "Women", value: 1 },
  { label: "Men", value: 2 },
  { label: "Unisex", value: 3 },
]

export const SIZE_OPTIONS: NumericOption[] = [
  { label: "XXS", value: 0 },
  { label: "XS", value: 1 },
  { label: "S", value: 2 },
  { label: "M", value: 3 },
  { label: "L", value: 4 },
  { label: "XL", value: 5 },
  { label: "XXL", value: 6 },
]

export const CONDITION_OPTIONS: NumericOption[] = [
  { label: "New", value: 0 },
  { label: "Worn", value: 1 },
  { label: "Refurbished", value: 2 },
]

export const getOptionLabel = (
  value: number | null,
  options: NumericOption[],
): string => {
  if (value === null) {
    return ""
  }

  return options.find((option) => option.value === value)?.label ?? ""
}
