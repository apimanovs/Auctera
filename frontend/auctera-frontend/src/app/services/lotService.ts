import api from "@/api"

export interface CreateLotRequest {
  title: string
  description: string
  amount: number
  currency: string
  category: string
  gender: string
  size: string
  brand: string
  condition: string
  color: string
  photoKeys: string[]
}

export const itemService = 
{
  async createLot(payload: CreateLotRequest): Promise<string> 
  {
    const response = await api.post<string>("/api/items/create", payload, {
      withCredentials: true,
    })

    return response.data
  },
}