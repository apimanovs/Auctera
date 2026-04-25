import { createRouter, createWebHistory } from 'vue-router'
import PendingLotsPage from '@/pages/PendingLotsPage.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      redirect: '/admin/pending',
    },
    {
      path: '/admin/pending',
      component: PendingLotsPage,
    },
  ],
})

export default router
