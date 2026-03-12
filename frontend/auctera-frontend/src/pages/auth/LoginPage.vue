<script setup lang="ts">
import Button from "@/components/ui/button/Button.vue"
</script>

<template>
  <div class="flex min-h-full flex-col justify-center px-6 py-14 lg:px-8">
    <div class="sm:mx-auto sm:w-full sm:max-w-md">
      <div class="rounded-2xl border border-gray-200 bg-white p-8 shadow-xl">
        <h2 class="text-center text-2xl font-bold tracking-tight text-black">
          Sign in to Coutera
        </h2>

        <form action="#" method="POST" class="mt-8 space-y-6">
          <div>
            <label for="email" class="block text-sm font-medium text-black">
              Email address<span class="text-red-500">*</span>
            </label>
            <div class="mt-2">
              <input
                id="email"
                type="email"
                name="email"
                required
                autocomplete="email"
                class="block w-full rounded-md border border-black bg-white px-3 py-1.5 text-black placeholder:text-gray-500 focus:outline-none focus:ring-2 focus:ring-black"
              />
            </div>
          </div>

          <div>
            <div class="flex items-center justify-between">
              <label for="password" class="block text-sm font-medium text-black">
                Password<span class="text-red-500">*</span>
              </label>
            </div>

            <div class="mt-2">
              <input
                id="password"
                type="password"
                name="password"
                required
                autocomplete="current-password"
                class="block w-full rounded-md border border-black bg-white px-3 py-1.5 text-black placeholder:text-gray-500 focus:outline-none focus:ring-2 focus:ring-black"
              />
            </div>
          </div>

          <div>
            <Button
              type="submit"
              class="flex w-full justify-center rounded-md bg-black px-3 py-1.5 text-sm font-semibold text-white transition duration-200 hover:shadow-md"
            >
              Sign in
            </Button>
          </div>

          <div class="flex justify-center">
            <div class="text-sm">
              <a href="#" class="font-semibold text-black transition duration-200 hover:text-gray-800">
                Forgot password?
              </a>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>