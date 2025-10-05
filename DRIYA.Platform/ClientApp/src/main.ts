import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import { createPinia } from 'pinia'
import PrimeVue from 'primevue/config'
import App from './App.vue'
import './assets/main.css'

// PrimeVue CSS
import 'primeicons/primeicons.css'

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

// Create app
const app = createApp(App)

// Configure PrimeVue
app.use(PrimeVue)

// Create router
const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: HomeView },
    { path: '/login', component: LoginView },
    { 
      path: '/dashboard', 
      component: DashboardView, 
      meta: { 
        requiresAuth: true, 
        title: 'Dashboard',
        description: 'Overview of your platform activity and metrics'
      } 
    },
    { 
      path: '/admin', 
      component: AdminDashboardView, 
      meta: { 
        requiresAuth: true, 
        requiresAdmin: true,
        title: 'Admin Dashboard',
        description: 'System administration and monitoring'
      } 
    },
    { 
      path: '/tenants', 
      component: TenantManagementView, 
      meta: { 
        requiresAuth: true, 
        requiresAdmin: true,
        title: 'Tenant Management',
        description: 'Manage multi-tenant organizations'
      } 
    },
    { 
      path: '/users', 
      component: UserManagementView, 
      meta: { 
        requiresAuth: true,
        title: 'User Management',
        description: 'Manage users, roles, and permissions'
      } 
    },
    { 
      path: '/features', 
      component: FeatureManagementView, 
      meta: { 
        requiresAuth: true,
        title: 'Feature Management',
        description: 'Configure feature flags and system features'
      } 
    },
    { 
      path: '/billing', 
      component: BillingView, 
      meta: { 
        requiresAuth: true,
        title: 'Billing',
        description: 'Manage subscriptions and payments'
      } 
    },
    { 
      path: '/api-keys', 
      component: ApiKeyManagementView, 
      meta: { 
        requiresAuth: true,
        title: 'API Key Management',
        description: 'Manage your API keys for integrations'
      } 
    },
    { 
      path: '/settings', 
      component: SettingsView, 
      meta: { 
        requiresAuth: true,
        title: 'Settings',
        description: 'Configure your account and preferences'
      } 
    },
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

// Use plugins and mount app
app.use(pinia)
app.use(router)
app.mount('#app')
