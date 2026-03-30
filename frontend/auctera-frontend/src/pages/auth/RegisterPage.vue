<script setup lang="ts">
import { RouterLink } from 'vue-router'
import Button from '@/components/ui/button/Button.vue'
import Input from '@/components/ui/input/Input.vue'
</script>

<template>
  <div class="min-h-screen bg-white px-4 py-6 sm:px-6 lg:px-8">
    <div class="mx-auto flex min-h-[calc(100vh-3rem)] max-w-5xl overflow-hidden rounded-[32px] border border-black/10 bg-white">
      <div class="flex w-full flex-col lg:flex-row">
        <section class="flex w-full items-center justify-center px-6 py-10 sm:px-10 lg:w-[48%] lg:px-12 xl:px-16">
          <div class="w-full max-w-md">
            <div class="flex justify-center">
              <RouterLink
                to="/home"
                class="inline-flex text-4xl font-semibold tracking-tight text-black transition hover:opacity-80"
              >
                Coutera
              </RouterLink>
            </div>

            <div class="mt-8">
              <p class="text-[11px] uppercase tracking-[0.24em] text-black/40">
                Register
              </p>

              <h1 class="mt-3 text-3xl font-semibold tracking-tight text-black sm:text-4xl">
                Create your account.
              </h1>

              <p class="mt-3 text-sm leading-6 text-black/60 sm:text-base">
                Start saving standout lots, following sellers, and building your place inside Coutera.
              </p>
            </div>

            <form action="#" method="POST" class="mt-8 space-y-5">
              <div>
                <label for="email" class="block text-sm font-medium text-black">
                  Email address
                </label>
                <div class="mt-2">
                  <Input
                    id="email"
                    type="email"
                    name="email"
                    required
                    autocomplete="email"
                    placeholder="you@example.com"
                    class="block h-12 w-full rounded-2xl border border-black/10 bg-neutral-100 px-4 text-black placeholder:text-black/35 focus:border-black focus:bg-white focus:outline-none"
                  />
                </div>
              </div>

              <div>
                <label for="password" class="block text-sm font-medium text-black">
                  Password
                </label>
                <div class="mt-2">
                  <Input
                    id="password"
                    type="password"
                    name="password"
                    required
                    autocomplete="new-password"
                    placeholder="Create a password"
                    class="block h-12 w-full rounded-2xl border border-black/10 bg-neutral-100 px-4 text-black placeholder:text-black/35 focus:border-black focus:bg-white focus:outline-none"
                  />
                </div>
              </div>

              <div>
                <label for="confirmPassword" class="block text-sm font-medium text-black">
                  Confirm password
                </label>
                <div class="mt-2">
                  <Input
                    id="confirmPassword"
                    type="password"
                    name="confirmPassword"
                    required
                    autocomplete="new-password"
                    placeholder="Repeat your password"
                    class="block h-12 w-full rounded-2xl border border-black/10 bg-neutral-100 px-4 text-black placeholder:text-black/35 focus:border-black focus:bg-white focus:outline-none"
                  />
                </div>
              </div>

              <div class="pt-2">
                <Button
                  type="submit"
                  class="flex h-12 w-full items-center justify-center rounded-full bg-black px-5 text-sm font-medium text-white transition hover:bg-black/90"
                >
                  Create account
                </Button>
              </div>
            </form>

            <div class="mt-6 text-sm text-black/55">
              Already have an account?
              <RouterLink
                to="/login"
                class="font-medium text-black transition hover:text-black/70"
              >
                Sign in
              </RouterLink>
            </div>
          </div>
        </section>

        <section class="flex w-full items-center bg-neutral-100 px-6 py-10 sm:px-10 lg:w-[52%] lg:px-12 xl:px-16">
          <div class="max-w-xl">
            <p class="text-[11px] uppercase tracking-[0.24em] text-black/40">
              Join Coutera
            </p>

            <h2 class="mt-4 text-4xl font-semibold tracking-tight text-black sm:text-5xl xl:text-6xl">
              Save better pieces.
              <br />
              Follow better taste.
            </h2>

            <p class="mt-5 max-w-lg text-sm leading-7 text-black/65 sm:text-base">
              Create an account to keep track of lots, discover selected sellers, and stay closer to what’s worth owning.
            </p>
          </div>
        </section>
      </div>
    </div>
  </div>
</template>