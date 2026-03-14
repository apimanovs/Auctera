import type { Auction } from '@/types/auction.ts'

export async function getAuctions(): Promise<Auction[]> {
  const response = await fetch('https://localhost:5001/api/auctions')

  if (!response.ok) {
    throw new Error('Failed to load auctions')
  }

  return await response.json()
}