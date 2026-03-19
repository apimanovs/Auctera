import { createRouter, createWebHistory } from 'vue-router'
import LoginPage from '@/pages/auth/LoginPage.vue'
import RegisterPage from '@/pages/auth/RegisterPage.vue'
import HomePage from '@/pages/home/HomePage.vue'
import AboutPage from '@/pages/info/AboutPage.vue'
import TermsPage from '@/pages//info/TermsPage.vue'
import PrivacyPolicyPage from '@/pages//info/PrivacyPolicyPage.vue'
import HowToSellPage from '@/pages/info/HowToSellPage.vue'
import LandingPage from '@/pages/Landing.vue'
import MainLayout from '@/layouts/MainLayout.vue'

const isDev = import.meta.env.DEV

const routes = isDev
  ? [
      {
        path: '/',
        component: MainLayout,
        children: [
          {
            path: 'home',
            component: HomePage,
          },
          {
            path: 'about',
            component: AboutPage,
          },
          {
            path: 'terms',
            component: TermsPage,
          },
          {
            path: 'privacy-policy',
            component: PrivacyPolicyPage,
          },
          {
            path: 'how-to-sell',
            component: HowToSellPage,
          },
          {
            path: 'login',
            component: LoginPage,
          },
          {
            path: 'register',
            component: RegisterPage,
          },
        ],
      },
      {
        path: '/:pathMatch(.*)*',
        redirect: '/',
      },
    ]
  : [
      {
        path: '/',
        component: LandingPage,
      },
      {
        path: '/:pathMatch(.*)*',
        redirect: '/',
      },
    ]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router