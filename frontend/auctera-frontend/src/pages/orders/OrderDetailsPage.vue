<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { RouterLink, useRoute } from 'vue-router'
import { storeToRefs } from 'pinia'

import { orderService } from '@/app/services/orderService'
import { auctionService } from '@/app/services/auctionService'
import { itemService } from '@/app/services/lotService'
import {
  getOrderActionHint,
  getOrderBadgeClass,
  getOrderStatusLabel,
  getUserRoleInOrder,
  normalizePaymentStatus,
  normalizeShippingStatus,
} from '@/app/helpers/orderHelpers'
import { useAuthStore } from '@/stores/authStore'
import type { OrderDetails, OrderUserRole } from '@/types/order'

const route = useRoute()
const authStore = useAuthStore()
const { user } = storeToRefs(authStore)

const isLoading = ref(true)
const errorMessage = ref('')
const order = ref<OrderDetails | null>(null)
const lotTitle = ref('Untitled')
const lotImageUrl = ref('')

const role = computed<OrderUserRole>(() => {
  if (!order.value) return 'unknown'
  return getUserRoleInOrder(order.value, user.value?.id)
})

const roleLabel = computed(() => {
  if (role.value === 'buyer') return 'You are the buyer'
  if (role.value === 'seller') return 'You are the seller'
  return 'Role unavailable'
})

const paymentLabel = computed(() => {
  if (!order.value) return 'Unknown'
  const status = normalizePaymentStatus(order.value.paymentStatus ?? order.value.status ?? order.value.orderStatus)

  if (status === 'paid') return 'Paid'
  if (status === 'pending') return 'Pending'
  if (status === 'failed') return 'Failed'
  return 'Unknown'
})

const shippingLabel = computed(() => {
  if (!order.value) return 'Unknown'

  const status = normalizeShippingStatus(order.value.shippingStatus ?? order.value.status ?? order.value.orderStatus)
  if (status === 'awaiting_shipment') return 'Awaiting shipment'
  if (status === 'shipped') return 'Shipped'
  if (status === 'completed') return 'Completed'
  return 'Unknown'
})

const loadOrder = async () => {
  try {
    isLoading.value = true
    errorMessage.value = ''

    const id = String(route.params.id)
    const details = await orderService.getOrderDetails(id)
    order.value = details

    const auctionId = String(details.auctionId ?? '')
    if (auctionId) {
      const auction = await auctionService.getAuctionDetails(auctionId).catch(() => null)
      if (auction?.lotTitle) {
        lotTitle.value = auction.lotTitle
      }

      if (auction?.lotId) {
        const lot = await itemService.getLot(String(auction.lotId)).catch(() => null)

        if (lot?.title) {
          lotTitle.value = lot.title
        }

        if (lot?.media?.[0]?.url) {
          lotImageUrl.value = lot.media[0].url
        }
      }
    }

    lotTitle.value = String(details.title ?? details.lotTitle ?? lotTitle.value)
    lotImageUrl.value = String(details.imageUrl ?? lotImageUrl.value)
  } catch (error) {
    console.error(error)
    errorMessage.value = 'Failed to load order details.'
  } finally {
    isLoading.value = false
  }
}

onMounted(loadOrder)
</script>

<template>
  <div class="mx-auto max-w-5xl px-4 py-8 sm:px-6 lg:px-8">
    <div class="mb-4">
      <RouterLink to="/orders" class="rounded-lg border px-3 py-2 text-sm transition hover:bg-foreground hover:text-background">
        Back to Orders
      </RouterLink>
    </div>

    <div v-if="isLoading" class="rounded-2xl border p-6 text-sm text-foreground/70">Loading...</div>

    <div v-else-if="errorMessage" class="rounded-2xl border border-red-500/30 bg-red-500/10 p-6 text-sm text-red-300">
      {{ errorMessage }}
    </div>

    <div v-else-if="order" class="rounded-2xl border p-6">
      <div class="flex flex-wrap items-center gap-2">
        <h1 class="text-2xl font-semibold">Order #{{ String(order.id ?? order.orderId) }}</h1>
        <span :class="['rounded-full border px-2.5 py-1 text-xs', getOrderBadgeClass(order)]">
          {{ getOrderStatusLabel(order) }}
        </span>
      </div>

      <div class="mt-4 grid gap-4 md:grid-cols-[120px_1fr]">
        <img :src="lotImageUrl" :alt="lotTitle" class="h-28 w-28 rounded-xl border object-cover" />

        <div>
          <h2 class="text-lg font-semibold">{{ lotTitle }}</h2>
          <p class="mt-1 text-sm text-foreground/70">Price: {{ order.currency ?? 'EUR' }} {{ order.price ?? order.amount ?? 0 }}</p>
          <p class="text-xs text-foreground/50">Created: {{ (order.createdAt ?? order.createdDate ?? order.paymentDeadlineUtc) ? new Date(String(order.createdAt ?? order.createdDate ?? order.paymentDeadlineUtc)).toLocaleString() : '—' }}</p>
        </div>
      </div>

      <div class="mt-6 grid gap-3 md:grid-cols-2">
        <div class="rounded-xl border p-3 text-sm">
          <p class="text-foreground/50">Buyer</p>
          <p class="mt-1 font-medium">{{ order.buyerId ?? '—' }}</p>
        </div>

        <div class="rounded-xl border p-3 text-sm">
          <p class="text-foreground/50">Seller</p>
          <p class="mt-1 font-medium">{{ order.sellerId ?? '—' }}</p>
        </div>

        <div class="rounded-xl border p-3 text-sm">
          <p class="text-foreground/50">Payment status</p>
          <p class="mt-1 font-medium">{{ paymentLabel }}</p>
        </div>

        <div class="rounded-xl border p-3 text-sm">
          <p class="text-foreground/50">Shipping status</p>
          <p class="mt-1 font-medium">{{ shippingLabel }}</p>
        </div>
      </div>

      <div class="mt-6 rounded-xl border p-3 text-sm">
        <p class="text-foreground/50">Role</p>
        <p class="mt-1 font-medium">{{ roleLabel }}</p>
        <p class="mt-2">{{ getOrderActionHint(order, role) }}</p>
      </div>

      <div class="mt-4 flex gap-2">
        <button
          v-if="role === 'buyer'"
          type="button"
          disabled
          class="rounded-lg border px-3 py-2 text-sm opacity-60"
        >
          Payment coming soon
        </button>

        <button
          v-if="role === 'seller'"
          type="button"
          disabled
          class="rounded-lg border px-3 py-2 text-sm opacity-60"
        >
          Shipping update coming soon
        </button>
      </div>
    </div>
  </div>
</template>
