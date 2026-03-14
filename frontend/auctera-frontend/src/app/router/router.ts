import { createRouter, createWebHistory } from 'vue-router'
import LoginPage from '@/pages/auth/LoginPage.vue'
import RegisterPage from '@/pages/auth/RegisterPage.vue'
import HomePage from '@/pages/home/HomePage.vue'
import AboutPage from '@/pages/AboutPage.vue'
import TermsPage from '@/pages/TermsPage.vue'
import PrivacyPolicyPage from '@/pages/PrivacyPolicyPage.vue'
import HowToSellPage from '@/pages/HowToSellPage.vue'

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
        { 
          path: '/about', 
          component: AboutPage 
        },
        { 
          path: '/terms', 
          component: TermsPage 
        },
        { 
          path: '/privacy-policy', 
          component: PrivacyPolicyPage 
        },
        { 
          path: '/how-to-sell', 
          component: HowToSellPage 
        },
      ]
    },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router