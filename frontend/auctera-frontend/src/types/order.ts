export type OrderStatusNormalized =
  | 'unpaid'
  | 'paid'
  | 'shipped'
  | 'completed'
  | 'cancelled'
  | 'failed'
  | 'unknown'

export type PaymentStatusNormalized = 'pending' | 'paid' | 'failed' | 'unknown'

export type ShippingStatusNormalized = 'awaiting_shipment' | 'shipped' | 'completed' | 'unknown'

export type OrderUserRole = 'buyer' | 'seller' | 'unknown'

export type OrderItem = {
  id?: string
  orderId?: string
  auctionId?: string
  sellerId?: string
  buyerId?: string
  status?: string | number | null
  orderStatus?: string | number | null
  paymentStatus?: string | number | null
  shippingStatus?: string | number | null
  price?: number
  amount?: number
  currency?: string
  createdAt?: string | null
  createdDate?: string | null
  paymentDeadlineUtc?: string | null
  paidAtUtc?: string | null
  lotTitle?: string | null
  title?: string | null
  imageUrl?: string | null
  photos?: string[] | null
  media?: Array<{ url?: string | null }>
  [key: string]: unknown
}

export type OrderDetails = OrderItem
