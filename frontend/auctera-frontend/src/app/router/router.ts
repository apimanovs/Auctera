import { createRouter, createWebHistory } from 'vue-router'
import LoginPage from '@/pages/auth/LoginPage.vue'
import RegisterPage from '@/pages/auth/RegisterPage.vue'
import ForgotPasswordPage from '@/pages/auth/ForgotPasswordPage.vue'
import ResetPasswordPage from '@/pages/auth/ResetPasswordPage.vue'
import HomePage from '@/pages/home/HomePage.vue'
import AboutPage from '@/pages/info/AboutPage.vue'
import TermsPage from '@/pages/info/TermsPage.vue'
import PrivacyPolicyPage from '@/pages/info/PrivacyPolicyPage.vue'
import HowToSellPage from '@/pages/info/HowToSellPage.vue'
import LandingPage from '@/pages/Landing.vue'
import MainLayout from '@/layouts/MainLayout.vue'
import LotPage from '@/pages/lots/LotPage.vue'
import UserProfilePage from '@/pages/user/UserProfilePage.vue'
import ProfileSettingsPage from '@/pages/user/ProfileSettingsPage.vue'
import NotFoundPage from '@/pages/errors/NotFoundPage.vue'
import CatalogPage from '@/pages/catalog/CatalogPage.vue'
import CreateLotPage from '@/pages/lots/CreateLotPage.vue'
import MyListingsPage from '@/pages/lots/MyListingsPage.vue'
import EditLotPage from '@/pages/lots/EditLotPage.vue'
import OrdersPage from '@/pages/orders/OrdersPage.vue'
import OrderDetailsPage from '@/pages/orders/OrderDetailsPage.vue'

const isDev = import.meta.env.DEV

const routes = isDev
  ? [
      {
        path: '/',
        component: MainLayout,
        children: [
          {
            path: '',
            redirect: '/home',
          },
          {
            path: 'home',
            component: HomePage,
          },
          {
            path: 'profile/settings',
            component: ProfileSettingsPage,
          },
          {
            path: 'settings/profile',
            component: ProfileSettingsPage,
          },
          {
            path: 'profile/:username',
            component: UserProfilePage,
          },
          {
            path: 'lot/:lotId',
            component: LotPage,
          },
          {
            path: 'lots/:id',
            component: LotPage,
          },
          {
            path: 'lots/:id/edit',
            component: EditLotPage,
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
            path: 'auctions',
            component: CatalogPage,
          },
          {
            path: 'sell',
            component: CreateLotPage,
          },
          {
            path: 'my-listings',
            component: MyListingsPage,
          },
          {
            path: 'orders',
            component: OrdersPage,
          },
          {
            path: 'orders/:id',
            component: OrderDetailsPage,
          },
        ],
      },
      {
        path: '/login',
        component: LoginPage,
      },
      {
        path: '/register',
        component: RegisterPage,
      },
      {
        path: '/reset-password',
        component: ResetPasswordPage,
      },
      {
        path: '/forgot-password',
        component: ForgotPasswordPage,
      },
      {
        path: '/:pathMatch(.*)*',
        component: NotFoundPage,
      },
    ]
  : [
      {
        path: '/',
        component: LandingPage,
      },
      {
        path: '/:pathMatch(.*)*',
        component: NotFoundPage,
      },
    ]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
