<script setup lang="ts">
import { X } from 'lucide-vue-next'
import {
  ToastClose,
  ToastDescription,
  ToastProvider,
  ToastRoot,
  ToastTitle,
  ToastViewport,
} from 'reka-ui'

import { cn } from '@/lib/utils'
import { useToast, type ToastVariant } from './use-toast'

const { toasts, dismiss } = useToast()

function variantClass(variant: ToastVariant): string {
  if (variant === 'success') {
    return 'border-emerald-500/25 bg-background text-foreground shadow-lg shadow-emerald-950/5 dark:border-emerald-400/20'
  }

  if (variant === 'error') {
    return 'border-destructive/35 bg-background text-foreground shadow-lg shadow-destructive/10'
  }

  return 'border-border bg-background text-foreground shadow-lg'
}
</script>

<template>
  <ToastProvider swipe-direction="right">
    <ToastRoot
      v-for="item in toasts"
      :key="item.id"
      :open="true"
      :duration="item.duration"
      :class="cn(
        'group pointer-events-auto relative grid w-full gap-1 overflow-hidden rounded-md border p-4 pr-10 text-sm transition-all data-[state=open]:animate-in data-[state=closed]:animate-out data-[swipe=end]:translate-x-[var(--reka-toast-swipe-end-x)] data-[swipe=move]:translate-x-[var(--reka-toast-swipe-move-x)] data-[swipe=cancel]:translate-x-0',
        variantClass(item.variant),
      )"
      @update:open="(open) => !open && dismiss(item.id)"
    >
      <ToastTitle class="font-medium">
        {{ item.title }}
      </ToastTitle>
      <ToastDescription v-if="item.description" class="text-muted-foreground">
        {{ item.description }}
      </ToastDescription>
      <ToastClose
        class="absolute right-2 top-2 rounded-md p-1 text-muted-foreground opacity-70 transition hover:text-foreground hover:opacity-100 focus:outline-none focus:ring-1 focus:ring-ring"
        @click="dismiss(item.id)"
      >
        <X class="h-4 w-4" />
      </ToastClose>
    </ToastRoot>

    <ToastViewport class="fixed right-0 top-0 z-[100] flex max-h-screen w-full flex-col gap-2 p-4 sm:bottom-0 sm:right-0 sm:top-auto sm:max-w-sm" />
  </ToastProvider>
</template>
