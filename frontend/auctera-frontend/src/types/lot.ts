export type Lot = {
    id: string
    title: string
    description: string
    price: number
    currency: string
    category: number
    categoryName: string
    gender: number
    genderName: string
    size: number
    sizeName: string
    brand: string
    condition: number
    conditionName: string
    color: string
    status: number
    statusName: string
    media: LotMedia[]
    seller: LotSeller
}

export type LotMedia = {
  type: string
  key: string
  url: string
}

export type LotSeller = {
  id: string
  username: string
  name: string
}
