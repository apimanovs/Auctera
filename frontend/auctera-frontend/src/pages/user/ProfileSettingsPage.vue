<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { storeToRefs } from 'pinia'

import { getApiErrorMessage } from '@/app/helpers/apiError'
import { profileService } from '@/app/services/profileService'
import { useAuthStore } from '@/stores/authStore'
import { Button } from '@/components/ui/button'
import { toast } from '@/components/ui/toast'

const router = useRouter()
const authStore = useAuthStore()
const { isAuthenticated } = storeToRefs(authStore)

const isLoading = ref(true)
const isSaving = ref(false)
const errorMessage = ref('')

const form = reactive({
  name: '',
  username: '',
  email: '',
  city: '',
  country: '',
})

const loadSettings = async () => {
  try {
    isLoading.value = true
    errorMessage.value = ''

    if (!isAuthenticated.value) {
      await authStore.checkAuth()
    }

    if (!isAuthenticated.value) {
      await router.push('/login')
      return
    }

    const settings = await profileService.getSettings()
    form.name = settings.name
    form.username = settings.username
    form.email = settings.email
    form.city = settings.city ?? ''
    form.country = settings.country ?? ''
  } catch (error) {
    errorMessage.value = getApiErrorMessage(error, 'Failed to load profile settings.')
  } finally {
    isLoading.value = false
  }
}

const saveSettings = async () => {
  errorMessage.value = ''

  if (!form.name.trim() || !form.username.trim()) {
    errorMessage.value = 'Name and username are required.'
    toast({ title: 'Failed to update profile', description: errorMessage.value, variant: 'error' })
    return
  }

  try {
    isSaving.value = true

    const settings = await profileService.updateSettings({
      name: form.name.trim(),
      username: form.username.trim(),
      city: form.city.trim() || null,
      country: form.country.trim() || null,
    })

    form.name = settings.name
    form.username = settings.username
    form.email = settings.email
    form.city = settings.city ?? ''
    form.country = settings.country ?? ''

    await authStore.checkAuth()
    toast({ title: 'Profile updated successfully', variant: 'success' })
  } catch (error) {
    const message = getApiErrorMessage(error, 'Failed to update profile.')
    errorMessage.value = message
    toast({ title: 'Failed to update profile', description: message, variant: 'error' })
  } finally {
    isSaving.value = false
  }
}

onMounted(loadSettings)
</script>

<template>
  <div class="mx-auto max-w-3xl px-4 py-8 sm:px-6 lg:px-8">
    <div class="mb-8">
      <p class="text-xs uppercase tracking-[0.22em] text-foreground/50">
        Account
      </p>
      <h1 class="mt-2 text-3xl font-semibold tracking-tight">
        Profile settings
      </h1>
      <p class="mt-3 max-w-2xl text-sm leading-6 text-foreground/70">
        Keep your public seller details accurate for buyers.
      </p>
    </div>

    <div v-if="isLoading" class="rounded-[24px] border bg-background p-6 text-sm text-foreground/70">
      Loading settings...
    </div>

    <form v-else class="space-y-6 rounded-[28px] border bg-background p-6 sm:p-8" @submit.prevent="saveSettings">
      <div v-if="errorMessage" class="rounded-[18px] border border-red-500/30 bg-red-500/10 px-4 py-3 text-sm text-red-300">
        {{ errorMessage }}
      </div>

      <div class="grid gap-5 sm:grid-cols-2">
        <div>
          <label class="text-sm font-medium">Display name</label>
          <input
            v-model="form.name"
            type="text"
            class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition placeholder:text-foreground/40 focus:ring-2 focus:ring-foreground/20"
            :disabled="isSaving"
          />
        </div>

        <div>
          <label class="text-sm font-medium">Username</label>
          <input
            v-model="form.username"
            type="text"
            class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition placeholder:text-foreground/40 focus:ring-2 focus:ring-foreground/20"
            :disabled="isSaving"
          />
        </div>

        <div class="sm:col-span-2">
          <label class="text-sm font-medium">Email</label>
          <input
            v-model="form.email"
            type="email"
            class="mt-2 h-11 w-full rounded-2xl border bg-muted px-4 text-sm text-foreground/60"
            disabled
          />
        </div>

        <div>
          <label class="text-sm font-medium">City</label>
          <input
            v-model="form.city"
            type="text"
            placeholder="Riga"
            class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition placeholder:text-foreground/40 focus:ring-2 focus:ring-foreground/20"
            :disabled="isSaving"
          />
        </div>

        <div>
          <label class="text-sm font-medium">Country</label>
          <input
            v-model="form.country"
            type="text"
            placeholder="Latvia"
            class="mt-2 h-11 w-full rounded-2xl border bg-background px-4 text-sm outline-none transition placeholder:text-foreground/40 focus:ring-2 focus:ring-foreground/20"
            :disabled="isSaving"
          />
        </div>
      </div>

      <div class="flex justify-end">
        <Button type="submit" :disabled="isSaving">
          {{ isSaving ? 'Saving...' : 'Save changes' }}
        </Button>
      </div>
    </form>
  </div>
</template>
