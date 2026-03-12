<script setup lang="ts">
import Button from "@/components/ui/button/Button.vue"
import Input from "@/components/ui/input/Input.vue"
</script>

<template>
  <div class="flex min-h-full flex-col justify-center px-6 py-14 lg:px-8">
    <div class="sm:mx-auto sm:w-full sm:max-w-md">
      <div class="rounded-2xl border border-gray-200 bg-white p-8 shadow-xl">
        <h2 class="text-center text-2xl font-bold tracking-tight text-black">
            Welcome to Coutera
        </h2>
        <p class="text-center text-2xs mt-4 text-gray-600">Register and make your way to exclusive fashion auction</p>

        <form action="#" method="POST" class="mt-5 space-y-6">
          <div>
            <label for="email" class="block text-sm font-medium text-black">
              Email address<span class="text-red-500">*</span>
            </label>
            <div class="mt-2">
              <Input
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
              <Input
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
            <div class="flex items-center justify-between">
              <label for="password" class="block text-sm font-medium text-black">
                Confirm Password<span class="text-red-500">*</span>
              </label>
            </div>

            <div class="mt-2">
              <Input
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
              class="flex w-full justify-center rounded-md bg-black px-3 py-1.5 text-sm font-semibold text-white transition duration-200 hover:shadow-md cursor-pointer"
            >
             Register
            </Button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>