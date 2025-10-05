<script setup lang="ts">
import { computed, onMounted, ref, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { useAuthStore } from './stores/auth'
import { useTenantStore } from './stores/tenant'

// PrimeVue Components
import Sidebar from 'primevue/sidebar'
import Button from 'primevue/button'
import Avatar from 'primevue/avatar'
import Menu from 'primevue/menu'
import TieredMenu from 'primevue/tieredmenu'
import Toast from 'primevue/toast'
import ConfirmDialog from 'primevue/confirmdialog'
import TabMenu from 'primevue/tabmenu'

// Icons
import { 
  XMarkIcon,
  MagnifyingGlassIcon
} from '@heroicons/vue/24/outline'

const router = useRouter()
const route = useRoute()
const authStore = useAuthStore()
const tenantStore = useTenantStore()

const isAuthenticated = computed(() => authStore.isAuthenticated)
const isAdmin = computed(() => authStore.isAdmin)
const currentUser = computed(() => authStore.user)
const currentTenant = computed(() => tenantStore.currentTenant)

// Responsive state
const sidebarVisible = ref(false)
const isMobile = ref(false)
const isTablet = ref(false)

// Check screen size
const checkScreenSize = () => {
  isMobile.value = window.innerWidth < 768
  isTablet.value = window.innerWidth >= 768 && window.innerWidth < 1024
}

// Main navigation items (Top menu)
const mainNavigationItems = computed(() => {
  const items = [
    {
      label: 'Dashboard',
      icon: 'pi pi-home',
      route: '/dashboard',
      requiresAuth: true
    },
    {
      label: 'Management',
      icon: 'pi pi-cog',
      items: [
        {
          label: 'Users',
          icon: 'pi pi-users',
          route: '/users',
          requiresAuth: true
        },
        {
          label: 'Features',
          icon: 'pi pi-cog',
          route: '/features',
          requiresAuth: true
        },
        {
          label: 'API Keys',
          icon: 'pi pi-key',
          route: '/api-keys',
          requiresAuth: true
        }
      ]
    },
    {
      label: 'Business',
      icon: 'pi pi-briefcase',
      items: [
        {
          label: 'Billing',
          icon: 'pi pi-credit-card',
          route: '/billing',
          requiresAuth: true
        },
        {
          label: 'Settings',
          icon: 'pi pi-cog',
          route: '/settings',
          requiresAuth: true
        }
      ]
    }
  ]

  // Admin-only items
  if (isAdmin.value) {
    items.splice(1, 0, {
      label: 'Administration',
      icon: 'pi pi-shield',
      items: [
        {
          label: 'Admin Dashboard',
          icon: 'pi pi-chart-bar',
          route: '/admin',
          requiresAuth: true
        },
        {
          label: 'Tenants',
          icon: 'pi pi-building',
          route: '/tenants',
          requiresAuth: true
        }
      ]
    })
  }

  return items
})

// Sidebar navigation items (for mobile/tablet)
const sidebarNavigationItems = computed(() => {
  const items: any[] = []
  
  mainNavigationItems.value.forEach(item => {
    if (item.items) {
      items.push({
        label: item.label,
        icon: item.icon,
        items: item.items
      })
    } else {
      items.push(item)
    }
  })
  
  return items
})

// Sub-navigation items based on current route
const subNavigationItems = computed(() => {
  const currentPath = route.path
  
  // Define sub-navigation for each main section
  const subNavs: { [key: string]: any[] } = {
    '/users': [
      { label: 'All Users', route: '/users', icon: 'pi pi-users' },
      { label: 'User Roles', route: '/users/roles', icon: 'pi pi-shield' },
      { label: 'Permissions', route: '/users/permissions', icon: 'pi pi-key' },
      { label: 'Activity Log', route: '/users/activity', icon: 'pi pi-history' }
    ],
    '/features': [
      { label: 'Feature Flags', route: '/features', icon: 'pi pi-flag' },
      { label: 'System Features', route: '/features/system', icon: 'pi pi-cog' },
      { label: 'Tenant Features', route: '/features/tenant', icon: 'pi pi-building' },
      { label: 'User Features', route: '/features/user', icon: 'pi pi-user' }
    ],
    '/billing': [
      { label: 'Overview', route: '/billing', icon: 'pi pi-chart-line' },
      { label: 'Invoices', route: '/billing/invoices', icon: 'pi pi-file-text' },
      { label: 'Payment Methods', route: '/billing/payment-methods', icon: 'pi pi-credit-card' },
      { label: 'Usage', route: '/billing/usage', icon: 'pi pi-chart-bar' }
    ],
    '/admin': [
      { label: 'System Overview', route: '/admin', icon: 'pi pi-chart-bar' },
      { label: 'System Health', route: '/admin/health', icon: 'pi pi-heart' },
      { label: 'Logs', route: '/admin/logs', icon: 'pi pi-file-text' },
      { label: 'Analytics', route: '/admin/analytics', icon: 'pi pi-chart-line' }
    ],
    '/tenants': [
      { label: 'All Tenants', route: '/tenants', icon: 'pi pi-building' },
      { label: 'Create Tenant', route: '/tenants/create', icon: 'pi pi-plus' },
      { label: 'Tenant Analytics', route: '/tenants/analytics', icon: 'pi pi-chart-bar' },
      { label: 'Billing Overview', route: '/tenants/billing', icon: 'pi pi-credit-card' }
    ]
  }
  
  return subNavs[currentPath] || []
})

const handleLogout = () => {
  authStore.logout()
  router.push('/login')
}

// User menu items
const userMenuItems = ref([
  {
    label: 'Profile',
    icon: 'pi pi-user',
    command: () => router.push('/profile')
  },
  {
    label: 'Settings',
    icon: 'pi pi-cog',
    command: () => router.push('/settings')
  },
  {
    separator: true
  },
  {
    label: 'Logout',
    icon: 'pi pi-sign-out',
    command: handleLogout
  }
])

const navigateTo = (route: string) => {
  router.push(route)
  sidebarVisible.value = false
}

// Watch for route changes to close mobile sidebar
watch(route, () => {
  if (isMobile.value) {
    sidebarVisible.value = false
  }
})

onMounted(async () => {
  checkScreenSize()
  window.addEventListener('resize', checkScreenSize)
  
  if (isAuthenticated.value) {
    await tenantStore.fetchCurrentTenant()
  }
})
</script>

<template>
  <div id="app" class="min-h-screen bg-gray-50">
    <!-- Toast and Confirm Dialog -->
    <Toast />
    <ConfirmDialog />
    
    <!-- Public Layout (Login, Home) -->
    <div v-if="!isAuthenticated" class="min-h-screen">
      <router-view />
    </div>

    <!-- Authenticated Layout -->
    <div v-else class="min-h-screen bg-gray-50">
      <!-- Mobile Sidebar -->
      <Sidebar 
        v-model:visible="sidebarVisible" 
        class="w-80"
        :modal="true"
        position="left"
        v-if="isMobile"
      >
        <template #header>
          <div class="flex items-center justify-between p-4">
            <div class="flex items-center">
              <div class="w-8 h-8 bg-primary-600 rounded-lg flex items-center justify-center">
                <span class="text-white font-bold text-sm">D</span>
              </div>
              <span class="ml-3 text-lg font-semibold text-gray-900">DRIYA Platform</span>
            </div>
          </div>
        </template>

        <div class="p-4">
          <!-- Tenant Info -->
          <div v-if="currentTenant" class="mb-6 p-3 bg-gray-50 rounded-lg">
            <div class="text-sm font-medium text-gray-700">Current Tenant</div>
            <div class="text-lg font-semibold text-gray-900">{{ currentTenant.name }}</div>
            <div class="text-xs text-gray-500">{{ currentTenant.tenantId }}</div>
          </div>

          <!-- Mobile Navigation Menu -->
          <Menu :model="sidebarNavigationItems" class="w-full border-0">
            <template #item="{ item }">
              <div 
                v-if="item.route"
                @click="navigateTo(item.route)"
                class="flex items-center p-3 rounded-lg cursor-pointer transition-colors duration-150"
                :class="[
                  route.path === item.route 
                    ? 'bg-primary-50 text-primary-700 border-l-4 border-primary-600' 
                    : 'text-gray-700 hover:bg-gray-50'
                ]"
              >
                <i :class="item.icon" class="mr-3 text-lg"></i>
                <span class="font-medium">{{ item.label }}</span>
              </div>
              <div v-else class="mb-2">
                <div class="text-xs font-semibold text-gray-500 uppercase tracking-wider px-3 py-2">
                  {{ item.label }}
                </div>
                <div v-for="subItem in item.items" :key="subItem.route" 
                     @click="navigateTo(subItem.route)"
                     class="flex items-center p-3 ml-4 rounded-lg cursor-pointer transition-colors duration-150"
                     :class="[
                       route.path === subItem.route 
                         ? 'bg-primary-50 text-primary-700 border-l-4 border-primary-600' 
                         : 'text-gray-700 hover:bg-gray-50'
                     ]">
                  <i :class="subItem.icon" class="mr-3 text-lg"></i>
                  <span class="font-medium">{{ subItem.label }}</span>
                </div>
              </div>
            </template>
          </Menu>
        </div>
      </Sidebar>

      <!-- Main Layout -->
      <div class="flex flex-col lg:flex-row min-h-screen">
        <!-- Desktop Sidebar (Hidden on mobile) -->
        <aside v-if="!isMobile" class="hidden lg:flex lg:w-64 lg:flex-col lg:fixed lg:inset-y-0">
          <div class="flex flex-col flex-grow bg-white border-r border-gray-200 shadow-sm">
            <!-- Logo -->
            <div class="flex items-center px-6 py-4 border-b border-gray-200">
              <div class="w-8 h-8 bg-primary-600 rounded-lg flex items-center justify-center">
                <span class="text-white font-bold text-sm">D</span>
              </div>
              <span class="ml-3 text-lg font-semibold text-gray-900">DRIYA Platform</span>
            </div>

            <!-- Tenant Info -->
            <div v-if="currentTenant" class="p-4 border-b border-gray-200">
              <div class="p-3 bg-gray-50 rounded-lg">
                <div class="text-sm font-medium text-gray-700">Current Tenant</div>
                <div class="text-lg font-semibold text-gray-900">{{ currentTenant.name }}</div>
                <div class="text-xs text-gray-500">{{ currentTenant.tenantId }}</div>
              </div>
            </div>

            <!-- Desktop Navigation -->
            <nav class="flex-1 px-4 py-4 space-y-2">
              <div v-for="item in mainNavigationItems" :key="item.label">
                <div v-if="item.route" 
                     @click="navigateTo(item.route)"
                     class="flex items-center p-3 rounded-lg cursor-pointer transition-colors duration-150"
                     :class="[
                       route.path === item.route 
                         ? 'bg-primary-50 text-primary-700 border-l-4 border-primary-600' 
                         : 'text-gray-700 hover:bg-gray-50'
                     ]">
                  <i :class="item.icon" class="mr-3 text-lg"></i>
                  <span class="font-medium">{{ item.label }}</span>
                </div>
                <div v-else>
                  <div class="text-xs font-semibold text-gray-500 uppercase tracking-wider px-3 py-2">
                    {{ item.label }}
                  </div>
                  <div v-for="subItem in item.items" :key="subItem.route" 
                       @click="navigateTo(subItem.route)"
                       class="flex items-center p-3 ml-4 rounded-lg cursor-pointer transition-colors duration-150"
                       :class="[
                         route.path === subItem.route 
                           ? 'bg-primary-50 text-primary-700 border-l-4 border-primary-600' 
                           : 'text-gray-700 hover:bg-gray-50'
                       ]">
                    <i :class="subItem.icon" class="mr-3 text-lg"></i>
                    <span class="font-medium">{{ subItem.label }}</span>
                  </div>
                </div>
              </div>
            </nav>
          </div>
        </aside>

        <!-- Main Content Area -->
        <div class="flex-1 lg:pl-64">
          <!-- Top Navigation Bar -->
          <header class="bg-white shadow-lg border-b border-gray-200">
            <!-- Main Header -->
            <div class="h-16 flex items-center justify-between px-4 lg:px-6">
              <!-- Left side -->
              <div class="flex items-center">
                <Button 
                  icon="pi pi-bars" 
                  text 
                  rounded 
                  @click="sidebarVisible = true"
                  class="lg:hidden mr-4"
                />
                
                <!-- Page Title -->
                <div class="hidden sm:block">
                  <h1 class="text-xl font-semibold text-gray-900">
                    {{ route.meta?.title || route.name }}
                  </h1>
                  <p class="text-sm text-gray-500 mt-0.5">
                    {{ route.meta?.description || 'Manage your platform' }}
                  </p>
                </div>
              </div>

              <!-- Right side -->
              <div class="flex items-center space-x-3">
                <!-- Search (Desktop only) -->
                <div class="hidden lg:block relative">
                  <div class="relative">
                    <input 
                      type="text" 
                      placeholder="Search..."
                      class="w-72 pl-10 pr-4 py-2.5 border border-gray-300 rounded-xl focus:ring-2 focus:ring-primary-500 focus:border-primary-500 bg-gray-50 focus:bg-white transition-all duration-200"
                    />
                    <MagnifyingGlassIcon class="absolute left-3 top-3 h-5 w-5 text-gray-400" />
                  </div>
                </div>

                <!-- Notifications -->
                <Button 
                  icon="pi pi-bell" 
                  text 
                  rounded 
                  class="relative p-2 hover:bg-gray-100 transition-colors duration-200"
                >
                  <span class="absolute -top-1 -right-1 h-5 w-5 bg-red-500 text-white text-xs rounded-full flex items-center justify-center font-medium">3</span>
                </Button>

                <!-- User Profile -->
                <div class="flex items-center space-x-3 pl-3 border-l border-gray-200">
                  <div class="hidden sm:block text-right">
                    <div class="text-sm font-medium text-gray-900">
                      {{ currentUser?.firstName }} {{ currentUser?.lastName }}
                    </div>
                    <div class="text-xs text-gray-500">
                      {{ currentUser?.role || 'User' }}
                    </div>
                  </div>
                  
                  <TieredMenu :model="userMenuItems" popup>
                    <template #item="{ item }">
                      <div class="flex items-center p-3 hover:bg-gray-50 transition-colors duration-150">
                        <i :class="item.icon" class="mr-3 text-gray-600"></i>
                        <span class="text-gray-900">{{ item.label }}</span>
                      </div>
                    </template>
                  </TieredMenu>
                  
                  <Button 
                    class="p-0 border-0 bg-transparent hover:bg-gray-100 rounded-full p-1 transition-colors duration-200"
                  >
                    <Avatar 
                      :label="currentUser?.firstName?.charAt(0) || 'U'"
                      size="large"
                      shape="circle"
                      class="bg-gradient-to-r from-primary-500 to-primary-600 text-white font-semibold"
                    />
                  </Button>
                </div>
              </div>
            </div>

          </header>

          <!-- Sub Navigation (when available) -->
          <div v-if="subNavigationItems.length > 0" class="bg-white border-b border-gray-200">
            <div class="px-4 lg:px-6">
              <TabMenu :model="subNavigationItems" class="border-0">
                <template #item="{ item }">
                  <div @click="navigateTo(item.route)" class="flex items-center p-3 cursor-pointer">
                    <i :class="item.icon" class="mr-2"></i>
                    <span>{{ item.label }}</span>
                  </div>
                </template>
              </TabMenu>
            </div>
          </div>

          <!-- Main Content -->
          <main class="flex-1 overflow-auto bg-gray-50">
            <div class="p-4 lg:p-6">
              <router-view />
            </div>
          </main>
        </div>
      </div>
    </div>

    <!-- Global Error Display -->
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
          <h3 class="text-sm font-medium text-red-800">Error</h3>
          <div class="mt-2 text-sm text-red-700">
            <p>{{ authStore.error }}</p>
          </div>
        </div>
        <div class="ml-auto pl-3">
          <div class="-mx-1.5 -my-1.5">
            <button
              @click="authStore.clearError()"
              class="inline-flex bg-red-50 rounded-md p-1.5 text-red-500 hover:bg-red-100 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-red-50 focus:ring-red-600"
            >
              <span class="sr-only">Dismiss</span>
              <XMarkIcon class="h-5 w-5" />
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Custom animations */
.animate-spin {
  animation: spin 1s linear infinite;
}

@keyframes spin {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}

/* PrimeVue customizations */
:deep(.p-sidebar) {
  border-right: 1px solid #e5e7eb;
  box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06);
}

:deep(.p-sidebar-header) {
  border-bottom: 1px solid #e5e7eb;
}

:deep(.p-menu) {
  border: 0;
  background: transparent;
}

:deep(.p-menuitem-link) {
  border: 0;
}

:deep(.p-tieredmenu) {
  border: 1px solid #e5e7eb;
  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06);
}
</style>