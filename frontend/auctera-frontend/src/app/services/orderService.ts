import api from '@/api'
import type { OrderDetails, OrderItem } from '@/types/order'

export const orderService = {
  async getMyOrders(): Promise<OrderItem[]> {
    const response = await api.get<OrderItem[]>('/api/orders/my')
    return response.data ?? []
  },

  async getOrderDetails(orderId: string): Promise<OrderDetails> {
    const response = await api.get<OrderDetails>(`/api/orders/details/${orderId}`)
    return response.data
  },
}