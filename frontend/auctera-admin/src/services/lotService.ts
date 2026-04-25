import api from '@/api'
import type { LotPreview } from '@/types/lot'

const normalizeStatus = (lot: LotPreview): string => {
  return String(lot.statusName ?? lot.status ?? '').toLowerCase()
}

export const adminLotService = {
  async getPendingLots(): Promise<LotPreview[]> {
    const response = await api.get<LotPreview[]>('/api/items')

    return response.data.filter((lot) => {
      const status = normalizeStatus(lot)
      return status.includes('pending')
    })
  },

  async approveLot(lotId: string): Promise<void> {
    console.log('Approve lot placeholder. No approve endpoint found in backend.', lotId)
  },

  async rejectLot(lotId: string): Promise<void> {
    console.log('Reject lot placeholder. No reject endpoint found in backend.', lotId)
  },
}
