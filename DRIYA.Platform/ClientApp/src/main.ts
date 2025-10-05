import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import { createPinia } from 'pinia'
import App from './App.vue'

// Views
import HomeView from './views/HomeView.vue'
import LoginView from './views/LoginView.vue'
import DashboardView from './views/DashboardView.vue'
import NotFoundView from './views/NotFoundView.vue'
import TenantManagementView from './views/TenantManagementView.vue'
import UserManagementView from './views/UserManagementView.vue'
import FeatureManagementView from './views/FeatureManagementView.vue'
import BillingView from './views/BillingView.vue'
import ApiKeyManagementView from './views/ApiKeyManagementView.vue'
import AdminDashboardView from './views/AdminDashboardView.vue'
import SettingsView from './views/SettingsView.vue'

// Create Pinia store
const pinia = createPinia()

// Create router
const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: HomeView },
    { path: '/login', component: LoginView },
    { path: '/dashboard', component: DashboardView, meta: { requiresAuth: true } },
    { path: '/admin', component: AdminDashboardView, meta: { requiresAuth: true, requiresAdmin: true } },
    { path: '/tenants', component: TenantManagementView, meta: { requiresAuth: true, requiresAdmin: true } },
    { path: '/users', component: UserManagementView, meta: { requiresAuth: true } },
    { path: '/features', component: FeatureManagementView, meta: { requiresAuth: true } },
    { path: '/billing', component: BillingView, meta: { requiresAuth: true } },
    { path: '/api-keys', component: ApiKeyManagementView, meta: { requiresAuth: true } },
    { path: '/settings', component: SettingsView, meta: { requiresAuth: true } },
    { path: '/:pathMatch(.*)*', component: NotFoundView }
  ]
})

// Navigation guard
router.beforeEach((to, from, next) => {
  const isAuthenticated = localStorage.getItem('authToken')
  const userRole = localStorage.getItem('userRole')
  
  if (to.meta.requiresAuth && !isAuthenticated) {
    next('/login')
  } else if (to.meta.requiresAdmin && userRole !== 'GlobalAdmin') {
    next('/dashboard')
  } else {
    next()
  }
})

// Create and mount app
const app = createApp(App)
app.use(pinia)
app.use(router)
app.mount('#app')
