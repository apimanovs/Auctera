<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { RouterLink } from 'vue-router'
import { storeToRefs } from 'pinia'

import { orderService } from '@/app/services/orderService'
import { auctionService } from '@/app/services/auctionService'
import { itemService } from '@/app/services/lotService'
import {
  getOrderActionHint,
  getOrderBadgeClass,
  getOrderStatusLabel,
  getUserRoleInOrder,
} from '@/app/helpers/orderHelpers'
import { useAuthStore } from '@/stores/authStore'
import type { OrderItem, OrderUserRole } from '@/types/order'

const authStore = useAuthStore()
const { user, isAuthenticated } = storeToRefs(authStore)

type OrderListRow = {
  order: OrderItem
  title: string
  imageUrl: string
  createdAt: string | null
  role: OrderUserRole
}

const isLoading = ref(true)
const errorMessage = ref('')
const rows = ref<OrderListRow[]>([])

const shortOrderId = (order: OrderItem) => {
  const id = String(order.id ?? order.orderId ?? '—')
  return id.length > 8 ? `${id.slice(0, 8)}...` : id
}

const priceLabel = (order: OrderItem) => {
  const amount = order.price ?? order.amount ?? 0
  const currency = order.currency ?? 'EUR'

  return `${currency} ${amount}`
}

const roleLabel = (role: OrderUserRole) => {
  if (role === 'buyer') return 'You are the buyer'
  if (role === 'seller') return 'You are the seller'
  return 'Role unavailable'
}

const resolveImage = (order: OrderItem, fallbackLotImage?: string) => {
  if (fallbackLotImage) return fallbackLotImage
  if (order.imageUrl) return order.imageUrl
  if (Array.isArray(order.photos) && order.photos.length > 0) return order.photos[0] ?? ''
  if (Array.isArray(order.media) && order.media.length > 0) return order.media[0]?.url ?? ''
  return ''
}

const orderedRows = computed(() =>
  [...rows.value].sort((a, b) => {
    const left = a.createdAt ? new Date(a.createdAt).getTime() : 0
    const right = b.createdAt ? new Date(b.createdAt).getTime() : 0
    return right - left
  }),
)

const loadOrders = async () => {
  try {
    isLoading.value = true
    errorMessage.value = ''

    const orders = await orderService.getMyOrders()

    const auctions = await auctionService.getAuctions().catch(() => [])
    const lotIdByAuctionId = new Map<string, string>()
    for (const auction of auctions) {
      if (auction.auctionId && auction.lotId) {
        lotIdByAuctionId.set(String(auction.auctionId), String(auction.lotId))
      }
    }

    const lotIds = Array.from(new Set(orders.map((order) => lotIdByAuctionId.get(String(order.auctionId ?? ''))).filter(Boolean))) as string[]

    const lotMap = new Map<string, { title: string; imageUrl: string }>()

    await Promise.all(
      lotIds.map(async (lotId) => {
        try {
          const lot = await itemService.getLot(lotId)
          lotMap.set(lotId, {
            title: lot.title,
            imageUrl: lot.media?.[0]?.url ?? '',
          })
        } catch (error) {
          console.error('Failed to enrich order lot', error)
        }
      }),
    )

    rows.value = orders.map((order) => {
      const auctionId = String(order.auctionId ?? '')
      const lotId = lotIdByAuctionId.get(auctionId)
      const lotInfo = lotId ? lotMap.get(lotId) : undefined

      return {
        order,
        title: String(order.title ?? order.lotTitle ?? lotInfo?.title ?? 'Untitled'),
        imageUrl: resolveImage(order, lotInfo?.imageUrl),
        createdAt: (order.createdAt ?? order.createdDate ?? order.paymentDeadlineUtc ?? null) as string | null,
        role: getUserRoleInOrder(order, user.value?.id),
      }
    })
  } catch (error) {
    console.error(error)
    errorMessage.value = 'Failed to load orders.'
  } finally {
    isLoading.value = false
  }
}

onMounted(async () => {
  if (!isAuthenticated.value) {
    await authStore.checkAuth()
  }

  if (!isAuthenticated.value) {
    isLoading.value = false
    return
  }

  await loadOrders()
})
</script>

<template>
  <div class="mx-auto max-w-7xl px-4 py-8 sm:px-6 lg:px-8">
    <div class="mb-6">
      <h1 class="text-3xl font-semibold">Orders</h1>
      <p class="mt-2 text-sm text-foreground/70">Track your buyer and seller order activity.</p>
    </div>

    <div v-if="!isAuthenticated" class="rounded-2xl border p-6 text-sm text-foreground/70">
      Please sign in to view your orders.
    </div>

    <div v-else>
      <div v-if="isLoading" class="rounded-2xl border p-6 text-sm text-foreground/70">Loading...</div>

      <div v-else-if="errorMessage" class="rounded-2xl border border-red-500/30 bg-red-500/10 p-6 text-sm text-red-300">
        {{ errorMessage }}
      </div>

      <div v-else-if="orderedRows.length === 0" class="rounded-2xl border p-6 text-sm text-foreground/70">
        You do not have any orders yet.
      </div>

      <div v-else class="space-y-4">
        <div v-for="row in orderedRows" :key="String(row.order.id ?? row.order.orderId)" class="rounded-2xl border p-4">
          <div class="grid gap-4 md:grid-cols-[100px_1fr_auto] md:items-center">
            <img
              :src="row.imageUrl"
              :alt="row.title"
              class="h-24 w-24 rounded-xl border object-cover"
            />

            <div>
              <div class="flex flex-wrap items-center gap-2">
                <h2 class="text-lg font-semibold">{{ row.title }}</h2>
                <span :class="['rounded-full border px-2.5 py-1 text-xs', getOrderBadgeClass(row.order)]">
                  {{ getOrderStatusLabel(row.order) }}
                </span>
              </div>

              <p class="mt-1 text-sm text-foreground/70">Order #{{ shortOrderId(row.order) }} · {{ priceLabel(row.order) }}</p>
              <p class="text-xs text-foreground/50">{{ roleLabel(row.role) }}</p>
              <p class="text-xs text-foreground/50">Created: {{ row.createdAt ? new Date(row.createdAt).toLocaleDateString() : '—' }}</p>
              <p class="mt-1 text-xs text-foreground/70">{{ getOrderActionHint(row.order, row.role) }}</p>
            </div>

            <RouterLink
              :to="`/orders/${String(row.order.id ?? row.order.orderId)}`"
              class="rounded-lg border px-3 py-2 text-sm transition hover:bg-foreground hover:text-background"
            >
              View details
            </RouterLink>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
