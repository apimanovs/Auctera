import { createRouter, createWebHistory } from 'vue-router'
import LoginPage from '@/pages/auth/LoginPage.vue'
import RegisterPage from '@/pages/auth/RegisterPage.vue'
import HomePage from '@/pages/home/HomePage.vue'

import MainLayout from '@/layouts/MainLayout.vue'

const routes = [
    { 
      path: '/', 
      component: MainLayout,
      children: [
        { 
          path: '', 
          component: HomePage,
        },
        { 
          path: '/login', 
          component: LoginPage 
        },
        { 
          path: '/register', 
          component: RegisterPage 
        },
      ]
    },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router