import { readonly, ref } from 'vue'

export type ToastVariant = 'default' | 'success' | 'error'

export type ToastOptions = {
  title: string
  description?: string
  variant?: ToastVariant
  duration?: number
}

export type ToastState = Required<Pick<ToastOptions, 'title' | 'variant' | 'duration'>> & {
  id: string
  description?: string
}

const toasts = ref<ToastState[]>([])
const timers = new Map<string, number>()

function createToastId(): string {
  if (typeof crypto !== 'undefined' && 'randomUUID' in crypto) {
    return crypto.randomUUID()
  }

  return `${Date.now()}-${Math.random().toString(36).slice(2)}`
}

function dismiss(id: string): void {
  const timer = timers.get(id)

  if (timer) {
    window.clearTimeout(timer)
    timers.delete(id)
  }

  toasts.value = toasts.value.filter((toast) => toast.id !== id)
}

export function toast(options: ToastOptions): string {
  const id = createToastId()
  const duration = options.duration ?? 4200

  toasts.value = [
    ...toasts.value,
    {
      id,
      title: options.title,
      description: options.description,
      variant: options.variant ?? 'default',
      duration,
    },
  ].slice(-4)

  const timer = window.setTimeout(() => dismiss(id), duration)
  timers.set(id, timer)

  return id
}

export function useToast() {
  return {
    toasts: readonly(toasts),
    toast,
    dismiss,
  }
}
