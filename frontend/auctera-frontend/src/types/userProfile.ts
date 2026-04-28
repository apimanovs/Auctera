export interface UserProfileStatsDto {
  bidsPlaced: number
  activeListingsCount: number
  soldItemsCount: number
}

export interface UserProfileListingDto {
  lotId: string
  title: string
  brand?: string | null
  thumbnailUrl?: string | null
  currentPrice: number
  currency: string
  status: string | number
}

export interface UserProfileDto {
  id: string
  username: string
  name: string
  city?: string | null
  country?: string | null
  stats: UserProfileStatsDto
  activeListings: UserProfileListingDto[]
  soldListings: UserProfileListingDto[]
}

export interface ProfileSettingsDto {
  id: string
  name: string
  username: string
  email: string
  city?: string | null
  country?: string | null
}

export interface UpdateProfileSettingsPayload {
  name: string
  username: string
  city?: string | null
  country?: string | null
}
