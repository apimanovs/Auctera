<script setup lang="ts">
import Container from '@/components/ui/Container.vue'

const currentYear = new Date().getFullYear()
</script>

<template>
  <footer class="border-t border-black">
    <Container>
      <div class="py-6 flex flex-col gap-6">
        <div id="pages-menu" class="flex flex-col gap-3 w-10">
          <a href="/">About</a>
          <a href="/">Terms</a>
          <a href="/">Privacy</a>
        </div>

        <div id="bottom">
          © {{ currentYear }} Auctera
        </div>
      </div>
    </Container>
  </footer>
</template>