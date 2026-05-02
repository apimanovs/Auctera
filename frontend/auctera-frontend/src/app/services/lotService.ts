import api from '@/api'
import type { CreateLotPayload, CreateLotResponse } from '@/types/createLot'
import type { Lot, LotPreview } from '@/types/lot'

export type EditLotPayload = {
  id: string
  sellerId: string
  title: string
  description: string
  price: {
    amount: number
    currency: string
  }
  size: number
  brand: string
  category: number
  gender: number
  condition: number
  color: string
  country: string
  city: string
  age: string
  style: string
}

export const itemService = {
  async createLot(payload: CreateLotPayload): Promise<CreateLotResponse> {
    const response = await api.post<CreateLotResponse>('/api/items/create', payload)
    return response.data
  },

  async getLot(id: string): Promise<Lot> {
    const response = await api.get<Lot>(`/api/items/${id}`)
    return response.data
  },

  async getLots(filters?: Record<string, string | number | boolean | undefined>): Promise<LotPreview[]> {
    const response = await api.get<LotPreview[]>('/api/items', {
      params: filters,
    })
    return response.data
  },

  async getMyLots(): Promise<LotPreview[]> {
    const response = await api.get<LotPreview[]>('/api/items/my')
    return response.data
  },

  async updateLot(id: string, payload: EditLotPayload): Promise<void> {
    await api.patch(`/api/items/${id}/update`, payload)
  },

  async deleteLot(id: string): Promise<void> {
    await api.delete(`/api/items/${id}/delete`)
  },

  async publishLot(id: string): Promise<void> {
    await api.post(`/api/items/${id}/publish`)
  },
}
