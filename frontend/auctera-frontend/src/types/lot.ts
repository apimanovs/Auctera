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
  year?: number | null
  status?: number | string
  statusName?: string
  media: LotMedia[]
  seller: LotSeller
  sellerId?: string
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
  year?: number | null
  status?: number | string
  statusName?: string
  media: LotMedia[]
  sellerId?: string
  seller?: LotSeller
  sellerCity?: string | null
  sellerCountry?: string | null
  createdAt?: string
  auctionId?: string
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
  city?: string | null
  country?: string | null
}
