import api from "@/api"
import type { CreateLotPayload, CreateLotResponse } from "@/types/createLot"

export const itemService = {
  async createLot(payload: CreateLotPayload): Promise<CreateLotResponse> {
    const response = await api.post<CreateLotResponse>("/api/items/create", payload, {
      withCredentials: true,
    })

    return response.data
  },
}
