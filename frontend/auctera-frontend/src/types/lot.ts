export type LotStatusLabel =
  | 'Draft'
  | 'Pending'
  | 'Approved'
  | 'Active'
  | 'Rejected'
  | 'Sold'
  | 'Expired'
  | 'Unknown'

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
  status?: number | string
  statusName?: string
  media: LotMedia[]
  seller: LotSeller
  sellerId?: string
  country: string
  city: string
  age: string
  style: string
  createdAt?: string
}

export type LotPreview = {
  id: string
  title: string
  price: number
  currency: string
  category?: number
  categoryName?: string
  gender?: number
  genderName?: string
  size?: number
  sizeName?: string
  brand: string
  condition?: number
  conditionName?: string
  color?: string
  status?: number | string
  statusName?: string
  media: LotMedia[]
  sellerId?: string
  seller?: LotSeller
  auctionId?: string
  country?: string
  city?: string
  age?: string
  style?: string
  createdAt?: string
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
