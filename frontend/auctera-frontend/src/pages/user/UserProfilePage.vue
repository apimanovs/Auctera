<script setup lang="ts">
const user = {
  name: 'Artjoms Archive',
  username: '@artjoms',
  location: 'Riga, Latvia',
  joined: 'Member since March 2026',
  bio: 'Collector of archive designer fashion, understated essentials, and pieces with strong silhouette. Focused on quality, rarity, and clean presentation.',
  avatar:
    'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?q=80&w=1200&auto=format&fit=crop',
  rating: '4.9',
  reviewsCount: 38,
  responseTime: 'Usually replies in 1 hour',
  stats: {
    activeListings: 12,
    soldItems: 47,
    followers: 132,
    following: 58,
  },
}

const activeListings = [
  {
    id: 1,
    title: 'Rick Owens Jumbo Lace Puffer',
    price: '€820',
    timeLeft: '1 day 12 hours',
    image:
      'https://media-photos.depop.com/b1/40086058/3512274199_d42fb73add7043d586f1be825bfb1f68/P0.jpg',
  },
  {
    id: 2,
    title: 'Vintage Leather Camera Bag',
    price: '€180',
    timeLeft: '2 days 5 hours',
    image:
      'https://media-photos.depop.com/b1/448068812/3474488748_5e0e9243711c4cbcb6dce7afdff37d6d/P0.jpg',
  },
  {
    id: 3,
    title: 'Maison Margiela Archive Knit',
    price: '€310',
    timeLeft: '3 days 2 hours',
    image:
      'https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?q=80&w=1200&auto=format&fit=crop',
  },
  {
    id: 4,
    title: 'Prada Technical Overshirt',
    price: '€460',
    timeLeft: '5 days 9 hours',
    image:
      'https://images.unsplash.com/photo-1515886657613-9f3515b0c78f?q=80&w=1200&auto=format&fit=crop',
  },
]

const soldListings = [
  {
    id: 1,
    title: 'Chrome Hearts Ring',
    soldPrice: 'Sold for €290',
    soldDate: 'Sold 3 days ago',
    image:
      'https://images.unsplash.com/photo-1617038220319-276d3cfab638?q=80&w=1200&auto=format&fit=crop',
  },
  {
    id: 2,
    title: 'Helmut Lang Bomber Jacket',
    soldPrice: 'Sold for €560',
    soldDate: 'Sold 1 week ago',
    image:
      'https://images.unsplash.com/photo-1523398002811-999ca8dec234?q=80&w=1200&auto=format&fit=crop',
  },
  {
    id: 3,
    title: 'Prada Leather Derby Shoes',
    soldPrice: 'Sold for €420',
    soldDate: 'Sold 2 weeks ago',
    image:
      'https://images.unsplash.com/photo-1542291026-7eec264c27ff?q=80&w=1200&auto=format&fit=crop',
  },
  {
    id: 4,
    title: 'Vintage Silver Chain',
    soldPrice: 'Sold for €150',
    soldDate: 'Sold 3 weeks ago',
    image:
      'https://images.unsplash.com/photo-1617038260897-41a1f14a8ca1?q=80&w=1200&auto=format&fit=crop',
  },
]

const reviews = [
  {
    id: 1,
    author: 'Marek',
    text: 'Fast communication, item exactly as described, and shipping was handled properly.',
  },
  {
    id: 2,
    author: 'Elina',
    text: 'Very clean presentation and trustworthy seller. Would buy again.',
  },
  {
    id: 3,
    author: 'Daniel',
    text: 'Great experience overall. The piece arrived in excellent condition.',
  },
]
</script>

<template>
  <div class="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
    <section class="overflow-hidden rounded-[32px] bg-neutral-100 px-6 py-8 md:px-8 md:py-10">
      <div class="grid gap-8 lg:grid-cols-[1.15fr_0.85fr] lg:items-end">
        <div class="flex flex-col gap-6 sm:flex-row sm:items-center">
          <div class="h-28 w-28 shrink-0 overflow-hidden rounded-full bg-white ring-1 ring-black/10 sm:h-32 sm:w-32">
            <img
              :src="user.avatar"
              :alt="user.name"
              class="h-full w-full object-cover"
            />
          </div>

          <div class="max-w-2xl">
            <p class="text-[11px] uppercase tracking-[0.22em] text-black/40">
              Seller profile
            </p>

            <h1 class="mt-2 text-3xl font-semibold tracking-tight text-black sm:text-4xl md:text-5xl">
              {{ user.name }}
            </h1>

            <div class="mt-3 flex flex-wrap gap-2">
              <span class="rounded-full border border-black/10 bg-white px-3 py-1 text-sm text-black/70">
                {{ user.username }}
              </span>
              <span class="rounded-full border border-black/10 bg-white px-3 py-1 text-sm text-black/70">
                {{ user.location }}
              </span>
              <span class="rounded-full border border-black/10 bg-white px-3 py-1 text-sm text-black/70">
                {{ user.joined }}
              </span>
            </div>

            <p class="mt-4 max-w-xl text-sm leading-6 text-black/70 sm:text-base">
              {{ user.bio }}
            </p>

            <div class="mt-5 flex flex-wrap gap-3 text-sm text-black/60">
              <span class="rounded-full bg-white px-3 py-1.5">
                Rating {{ user.rating }} / 5
              </span>
              <span class="rounded-full bg-white px-3 py-1.5">
                {{ user.reviewsCount }} reviews
              </span>
              <span class="rounded-full bg-white px-3 py-1.5">
                {{ user.responseTime }}
              </span>
            </div>
          </div>
        </div>

        <div class="flex flex-col gap-3 sm:flex-row lg:justify-end">
          <button
            class="rounded-full bg-black px-5 py-3 text-sm font-medium text-white transition hover:bg-black/90"
          >
            Follow
          </button>

          <button
            class="rounded-full border border-black/10 bg-white px-5 py-3 text-sm font-medium text-black transition hover:border-black"
          >
            Message
          </button>
        </div>
      </div>
    </section>

    <section class="mt-8 grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
      <div class="rounded-[24px] border border-black/10 bg-white p-5">
        <p class="text-xs uppercase tracking-[0.2em] text-black/40">Active listings</p>
        <p class="mt-3 text-3xl font-semibold text-black">{{ user.stats.activeListings }}</p>
      </div>

      <div class="rounded-[24px] border border-black/10 bg-white p-5">
        <p class="text-xs uppercase tracking-[0.2em] text-black/40">Sold items</p>
        <p class="mt-3 text-3xl font-semibold text-black">{{ user.stats.soldItems }}</p>
      </div>

      <div class="rounded-[24px] border border-black/10 bg-white p-5">
        <p class="text-xs uppercase tracking-[0.2em] text-black/40">Followers</p>
        <p class="mt-3 text-3xl font-semibold text-black">{{ user.stats.followers }}</p>
      </div>

      <div class="rounded-[24px] border border-black/10 bg-white p-5">
        <p class="text-xs uppercase tracking-[0.2em] text-black/40">Following</p>
        <p class="mt-3 text-3xl font-semibold text-black">{{ user.stats.following }}</p>
      </div>
    </section>

    <section class="mt-10 grid gap-6 lg:grid-cols-[0.78fr_1.22fr]">
      <div class="space-y-6">
        <div class="rounded-[28px] border border-black/10 bg-white px-6 py-6">
          <h2 class="text-2xl font-semibold tracking-tight text-black">
            About seller
          </h2>

          <div class="mt-6 space-y-5">
            <div>
              <p class="text-xs uppercase tracking-[0.2em] text-black/40">Focus</p>
              <p class="mt-2 text-sm leading-6 text-black/70">
                Archive fashion, designer outerwear, accessories, and selected collectible pieces.
              </p>
            </div>

            <div>
              <p class="text-xs uppercase tracking-[0.2em] text-black/40">Based in</p>
              <p class="mt-2 text-sm leading-6 text-black/70">
                {{ user.location }}
              </p>
            </div>

            <div>
              <p class="text-xs uppercase tracking-[0.2em] text-black/40">Account status</p>
              <div class="mt-3 flex flex-wrap gap-2">
                <span class="rounded-full bg-neutral-100 px-3 py-1 text-sm text-black/70">
                  Verified seller
                </span>
                <span class="rounded-full bg-neutral-100 px-3 py-1 text-sm text-black/70">
                  Secure payments
                </span>
                <span class="rounded-full bg-neutral-100 px-3 py-1 text-sm text-black/70">
                  Trusted history
                </span>
              </div>
            </div>
          </div>
        </div>

        <div class="rounded-[28px] border border-black/10 bg-white px-6 py-6">
          <div class="flex items-end justify-between gap-3">
            <div>
              <p class="text-[11px] uppercase tracking-[0.22em] text-black/40">
                Reviews
              </p>
              <h2 class="mt-2 text-2xl font-semibold tracking-tight text-black">
                Buyer feedback
              </h2>
            </div>
            <button class="text-sm font-medium text-black transition hover:text-black/60">
              View all
            </button>
          </div>

          <div class="mt-6 space-y-4">
            <div
              v-for="review in reviews"
              :key="review.id"
              class="rounded-2xl bg-neutral-100 p-4"
            >
              <div class="flex items-center justify-between gap-3">
                <p class="text-sm font-medium text-black">{{ review.author }}</p>
                <span class="text-xs uppercase tracking-[0.18em] text-black/40">Verified purchase</span>
              </div>
              <p class="mt-3 text-sm leading-6 text-black/70">
                {{ review.text }}
              </p>
            </div>
          </div>
        </div>
      </div>

      <div class="space-y-6">
        <div class="rounded-[28px] bg-neutral-100 px-6 py-6">
          <div class="flex items-end justify-between gap-3">
            <div>
              <p class="text-[11px] uppercase tracking-[0.22em] text-black/40">
                Live inventory
              </p>
              <h2 class="mt-2 text-2xl font-semibold tracking-tight text-black">
                Active listings
              </h2>
              <p class="mt-1 text-sm text-black/60">
                Lots currently available for bidding.
              </p>
            </div>

            <button class="text-sm font-medium text-black transition hover:text-black/60">
              View all
            </button>
          </div>

          <div class="mt-6 grid gap-4 sm:grid-cols-2">
            <article
              v-for="listing in activeListings"
              :key="listing.id"
              class="group overflow-hidden rounded-[24px] bg-white"
            >
              <div class="overflow-hidden">
                <img
                  :src="listing.image"
                  :alt="listing.title"
                  class="h-72 w-full object-cover transition duration-300 group-hover:scale-[1.02]"
                />
              </div>

              <div class="p-4">
                <h3 class="line-clamp-2 text-base font-medium text-black">
                  {{ listing.title }}
                </h3>

                <div class="mt-4 flex items-center justify-between gap-3">
                  <span class="text-sm font-semibold text-black">{{ listing.price }}</span>
                  <span class="text-sm text-black/50">{{ listing.timeLeft }}</span>
                </div>
              </div>
            </article>
          </div>
        </div>

        <div class="rounded-[28px] border border-black/10 bg-white px-6 py-6">
          <div class="flex items-end justify-between gap-3">
            <div>
              <p class="text-[11px] uppercase tracking-[0.22em] text-black/40">
                Seller history
              </p>
              <h2 class="mt-2 text-2xl font-semibold tracking-tight text-black">
                Sold items
              </h2>
              <p class="mt-1 text-sm text-black/60">
                Pieces already sold through the platform.
              </p>
            </div>

            <button class="text-sm font-medium text-black transition hover:text-black/60">
              View archive
            </button>
          </div>

          <div class="mt-6 grid gap-4 sm:grid-cols-2">
            <article
              v-for="listing in soldListings"
              :key="listing.id"
              class="group overflow-hidden rounded-[24px] bg-neutral-100"
            >
              <div class="relative overflow-hidden">
                <img
                  :src="listing.image"
                  :alt="listing.title"
                  class="h-72 w-full object-cover transition duration-300 group-hover:scale-[1.02]"
                />
                <div class="absolute left-3 top-3 rounded-full bg-black px-3 py-1 text-xs font-medium text-white">
                  Sold
                </div>
              </div>

              <div class="p-4">
                <h3 class="line-clamp-2 text-base font-medium text-black">
                  {{ listing.title }}
                </h3>

                <div class="mt-4 flex items-center justify-between gap-3">
                  <span class="text-sm font-semibold text-black">{{ listing.soldPrice }}</span>
                  <span class="text-sm text-black/50">{{ listing.soldDate }}</span>
                </div>
              </div>
            </article>
          </div>
        </div>
      </div>
    </section>

    <section class="my-16 flex flex-col items-center justify-center gap-2 py-10 text-center text-black">
      <h2 class="max-w-3xl text-3xl font-semibold leading-tight sm:text-4xl">
        Clean profile. Strong inventory. Real history.
      </h2>
      <p class="mt-2 max-w-2xl text-sm text-black/70 sm:text-base">
        A seller page should show not just taste, but proof of activity and trust.
      </p>
    </section>
  </div>
</template>