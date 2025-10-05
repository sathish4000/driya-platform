<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from './stores/auth'
import { useTenantStore } from './stores/tenant'
import { 
  Bars3Icon, 
  XMarkIcon,
  HomeIcon,
  UserGroupIcon,
  Cog6ToothIcon,
  CreditCardIcon,
  KeyIcon,
  ChartBarIcon,
  BuildingOfficeIcon,
  UserIcon
} from '@heroicons/vue/24/outline'

const router = useRouter()
const authStore = useAuthStore()
const tenantStore = useTenantStore()

const isAuthenticated = computed(() => authStore.isAuthenticated)
const isAdmin = computed(() => authStore.isAdmin)
const currentUser = computed(() => authStore.user)
const currentTenant = computed(() => tenantStore.currentTenant)

// Mobile menu state
const mobileMenuOpen = ref(false)

// Navigation items
const navigation = computed(() => {
  const items = [
    { name: 'Dashboard', href: '/dashboard', icon: HomeIcon, requiresAuth: true },
    { name: 'Users', href: '/users', icon: UserGroupIcon, requiresAuth: true },
    { name: 'Features', href: '/features', icon: Cog6ToothIcon, requiresAuth: true },
    { name: 'Billing', href: '/billing', icon: CreditCardIcon, requiresAuth: true },
    { name: 'API Keys', href: '/api-keys', icon: KeyIcon, requiresAuth: true },
    { name: 'Settings', href: '/settings', icon: Cog6ToothIcon, requiresAuth: true }
  ]

  // Admin-only items
  if (isAdmin.value) {
    items.splice(1, 0, 
      { name: 'Admin Dashboard', href: '/admin', icon: ChartBarIcon, requiresAuth: true },
      { name: 'Tenants', href: '/tenants', icon: BuildingOfficeIcon, requiresAuth: true }
    )
  }

  return items
})

const handleLogout = () => {
  authStore.logout()
  router.push('/login')
}

onMounted(async () => {
  if (isAuthenticated.value) {
    await tenantStore.fetchCurrentTenant()
  }
})
</script>

<template>
  <div id="app" class="min-h-screen bg-gray-50">
    <!-- Navigation -->
    <nav v-if="isAuthenticated" class="bg-white shadow-sm border-b border-gray-200">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex justify-between h-16">
          <!-- Logo and primary nav -->
          <div class="flex">
            <div class="flex-shrink-0 flex items-center">
              <div class="flex items-center">
                <div class="w-8 h-8 bg-blue-600 rounded-lg flex items-center justify-center">
                  <span class="text-white font-bold text-sm">D</span>
                </div>
                <span class="ml-2 text-xl font-semibold text-gray-900">DRIYA Platform</span>
              </div>
            </div>
            
            <!-- Desktop navigation -->
            <div class="hidden sm:ml-6 sm:flex sm:space-x-8">
              <router-link
                v-for="item in navigation"
                :key="item.name"
                :to="item.href"
                class="inline-flex items-center px-1 pt-1 border-b-2 text-sm font-medium transition-colors duration-200"
                :class="[
                  $route.path === item.href
                    ? 'border-blue-500 text-gray-900'
                    : 'border-transparent text-gray-500 hover:border-gray-300 hover:text-gray-700'
                ]"
              >
                <component :is="item.icon" class="w-4 h-4 mr-2" />
                {{ item.name }}
              </router-link>
            </div>
          </div>

          <!-- Right side -->
          <div class="hidden sm:ml-6 sm:flex sm:items-center">
            <!-- Tenant selector -->
            <div v-if="currentTenant" class="mr-4">
              <span class="text-sm text-gray-500">Tenant:</span>
              <span class="ml-1 text-sm font-medium text-gray-900">{{ currentTenant.name }}</span>
            </div>

            <!-- User menu -->
            <div class="ml-3 relative">
              <div class="flex items-center space-x-3">
                <div class="text-sm text-gray-700">
                  <span class="font-medium">{{ currentUser?.firstName }} {{ currentUser?.lastName }}</span>
                  <span class="text-gray-500 ml-1">({{ currentUser?.role }})</span>
                </div>
                <button
                  @click="handleLogout"
                  class="inline-flex items-center px-3 py-2 border border-transparent text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 transition-colors duration-200"
                >
                  <UserIcon class="w-4 h-4 mr-1" />
                  Logout
                </button>
              </div>
            </div>
          </div>

          <!-- Mobile menu button -->
          <div class="flex items-center sm:hidden">
            <button
              @click="mobileMenuOpen = !mobileMenuOpen"
              class="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-gray-500 hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-blue-500"
            >
              <Bars3Icon v-if="!mobileMenuOpen" class="block h-6 w-6" />
              <XMarkIcon v-else class="block h-6 w-6" />
            </button>
          </div>
        </div>
      </div>

      <!-- Mobile menu -->
      <div v-show="mobileMenuOpen" class="sm:hidden">
        <div class="pt-2 pb-3 space-y-1">
          <router-link
            v-for="item in navigation"
            :key="item.name"
            :to="item.href"
            class="flex items-center px-3 py-2 text-base font-medium transition-colors duration-200"
            :class="[
              $route.path === item.href
                ? 'bg-blue-50 border-blue-500 text-blue-700'
                : 'border-transparent text-gray-600 hover:bg-gray-50 hover:border-gray-300 hover:text-gray-800'
            ]"
            @click="mobileMenuOpen = false"
          >
            <component :is="item.icon" class="w-5 h-5 mr-3" />
            {{ item.name }}
          </router-link>
        </div>
        
        <!-- Mobile user info -->
        <div class="pt-4 pb-3 border-t border-gray-200">
          <div class="flex items-center px-4">
            <div class="flex-shrink-0">
              <div class="w-8 h-8 bg-blue-600 rounded-full flex items-center justify-center">
                <span class="text-white font-medium text-sm">
                  {{ currentUser?.firstName?.charAt(0) }}{{ currentUser?.lastName?.charAt(0) }}
                </span>
              </div>
            </div>
            <div class="ml-3">
              <div class="text-base font-medium text-gray-800">
                {{ currentUser?.firstName }} {{ currentUser?.lastName }}
              </div>
              <div class="text-sm font-medium text-gray-500">{{ currentUser?.email }}</div>
              <div class="text-sm text-gray-500">{{ currentUser?.role }}</div>
            </div>
          </div>
          <div class="mt-3 space-y-1">
            <button
              @click="handleLogout"
              class="block w-full text-left px-4 py-2 text-base font-medium text-gray-500 hover:text-gray-800 hover:bg-gray-100"
            >
              Logout
            </button>
          </div>
        </div>
      </div>
    </nav>

    <!-- Main content -->
    <main class="flex-1">
      <router-view />
    </main>

    <!-- Global error display -->
    <div
      v-if="authStore.error"
      class="fixed top-4 right-4 z-50 bg-red-50 border border-red-200 rounded-md p-4 max-w-sm"
    >
      <div class="flex">
        <div class="flex-shrink-0">
          <svg class="h-5 w-5 text-red-400" viewBox="0 0 20 20" fill="currentColor">
            <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clip-rule="evenodd" />
          </svg>
        </div>
        <div class="ml-3">
          <p class="text-sm text-red-800">{{ authStore.error }}</p>
        </div>
        <div class="ml-auto pl-3">
          <button
            @click="authStore.clearError"
            class="inline-flex text-red-400 hover:text-red-600"
          >
            <svg class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
              <path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd" />
            </svg>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style>
/* Global styles */
* {
  box-sizing: border-box;
}

body {
  margin: 0;
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen',
    'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue',
    sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

#app {
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen',
    'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue',
    sans-serif;
}

/* Tailwind-like utility classes */
.min-h-screen { min-height: 100vh; }
.bg-gray-50 { background-color: #f9fafb; }
.bg-white { background-color: #ffffff; }
.bg-blue-600 { background-color: #2563eb; }
.bg-blue-50 { background-color: #eff6ff; }
.bg-red-50 { background-color: #fef2f2; }
.bg-gray-100 { background-color: #f3f4f6; }

.text-white { color: #ffffff; }
.text-gray-900 { color: #111827; }
.text-gray-700 { color: #374151; }
.text-gray-600 { color: #4b5563; }
.text-gray-500 { color: #6b7280; }
.text-gray-400 { color: #9ca3af; }
.text-blue-500 { color: #3b82f6; }
.text-blue-700 { color: #1d4ed8; }
.text-red-400 { color: #f87171; }
.text-red-600 { color: #dc2626; }
.text-red-800 { color: #991b1b; }

.border-gray-200 { border-color: #e5e7eb; }
.border-gray-300 { border-color: #d1d5db; }
.border-blue-500 { border-color: #3b82f6; }
.border-red-200 { border-color: #fecaca; }
.border-transparent { border-color: transparent; }

.shadow-sm { box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.05); }

.max-w-7xl { max-width: 80rem; }
.max-w-sm { max-width: 24rem; }

.mx-auto { margin-left: auto; margin-right: auto; }
.ml-2 { margin-left: 0.5rem; }
.ml-3 { margin-left: 0.75rem; }
.ml-4 { margin-left: 1rem; }
.ml-6 { margin-left: 1.5rem; }
.mr-1 { margin-right: 0.25rem; }
.mr-2 { margin-right: 0.5rem; }
.mr-3 { margin-right: 0.75rem; }
.mr-4 { margin-right: 1rem; }
.mt-3 { margin-top: 0.75rem; }
.mt-4 { margin-top: 1rem; }
.pt-2 { padding-top: 0.5rem; }
.pt-3 { padding-top: 0.75rem; }
.pt-4 { padding-top: 1rem; }
.pb-3 { padding-bottom: 0.75rem; }
.px-1 { padding-left: 0.25rem; padding-right: 0.25rem; }
.px-3 { padding-left: 0.75rem; padding-right: 0.75rem; }
.px-4 { padding-left: 1rem; padding-right: 1rem; }
.px-6 { padding-left: 1.5rem; padding-right: 1.5rem; }
.px-8 { padding-left: 2rem; padding-right: 2rem; }
.py-2 { padding-top: 0.5rem; padding-bottom: 0.5rem; }
.py-3 { padding-top: 0.75rem; padding-bottom: 0.75rem; }
.py-4 { padding-top: 1rem; padding-bottom: 1rem; }

.flex { display: flex; }
.flex-1 { flex: 1 1 0%; }
.flex-shrink-0 { flex-shrink: 0; }
.inline-flex { display: inline-flex; }
.block { display: block; }
.hidden { display: none; }

.items-center { align-items: center; }
.justify-center { justify-content: center; }
.justify-between { justify-content: space-between; }

.h-4 { height: 1rem; }
.h-5 { height: 1.25rem; }
.h-6 { height: 1.5rem; }
.h-8 { height: 2rem; }
.h-16 { height: 4rem; }

.w-4 { width: 1rem; }
.w-5 { width: 1.25rem; }
.w-6 { width: 1.5rem; }
.w-8 { width: 2rem; }

.rounded-lg { border-radius: 0.5rem; }
.rounded-md { border-radius: 0.375rem; }
.rounded-full { border-radius: 9999px; }

.border-b { border-bottom-width: 1px; }
.border-b-2 { border-bottom-width: 2px; }
.border { border-width: 1px; }

.text-sm { font-size: 0.875rem; line-height: 1.25rem; }
.text-base { font-size: 1rem; line-height: 1.5rem; }
.text-lg { font-size: 1.125rem; line-height: 1.75rem; }
.text-xl { font-size: 1.25rem; line-height: 1.75rem; }

.font-medium { font-weight: 500; }
.font-semibold { font-weight: 600; }
.font-bold { font-weight: 700; }

.space-x-3 > * + * { margin-left: 0.75rem; }
.space-x-8 > * + * { margin-left: 2rem; }
.space-y-1 > * + * { margin-top: 0.25rem; }

.transition-colors { transition-property: color, background-color, border-color, text-decoration-color, fill, stroke; }
.duration-200 { transition-duration: 200ms; }

.hover\:bg-gray-50:hover { background-color: #f9fafb; }
.hover\:bg-gray-100:hover { background-color: #f3f4f6; }
.hover\:border-gray-300:hover { border-color: #d1d5db; }
.hover\:text-gray-700:hover { color: #374151; }
.hover\:text-gray-500:hover { color: #6b7280; }
.hover\:text-gray-800:hover { color: #1f2937; }
.hover\:text-red-600:hover { color: #dc2626; }

.focus\:outline-none:focus { outline: 2px solid transparent; outline-offset: 2px; }
.focus\:ring-2:focus { box-shadow: 0 0 0 2px rgba(59, 130, 246, 0.5); }
.focus\:ring-inset:focus { box-shadow: inset 0 0 0 2px rgba(59, 130, 246, 0.5); }
.focus\:ring-offset-2:focus { box-shadow: 0 0 0 2px rgba(59, 130, 246, 0.5), 0 0 0 4px rgba(255, 255, 255, 0.5); }

.sm\:hidden { display: none; }
.sm\:flex { display: flex; }
.sm\:ml-6 { margin-left: 1.5rem; }
.sm\:px-6 { padding-left: 1.5rem; padding-right: 1.5rem; }
.sm\:space-x-8 > * + * { margin-left: 2rem; }

.lg\:px-8 { padding-left: 2rem; padding-right: 2rem; }

@media (min-width: 640px) {
  .sm\:hidden { display: none; }
  .sm\:flex { display: flex; }
  .sm\:ml-6 { margin-left: 1.5rem; }
  .sm\:px-6 { padding-left: 1.5rem; padding-right: 1.5rem; }
  .sm\:space-x-8 > * + * { margin-left: 2rem; }
}

@media (min-width: 1024px) {
  .lg\:px-8 { padding-left: 2rem; padding-right: 2rem; }
}

.fixed { position: fixed; }
.relative { position: relative; }
.top-4 { top: 1rem; }
.right-4 { right: 1rem; }
.z-50 { z-index: 50; }

.w-full { width: 100%; }
.text-left { text-align: left; }

.fill-rule-evenodd { fill-rule: evenodd; }
.clip-rule-evenodd { clip-rule: evenodd; }
</style>
