<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useTenantStore } from '../stores/tenant'
import { 
  UsersIcon, 
  BuildingOfficeIcon, 
  CreditCardIcon, 
  ChartBarIcon,
  ExclamationTriangleIcon,
  CheckCircleIcon,
  ClockIcon,
  CurrencyDollarIcon
} from '@heroicons/vue/24/outline'

const tenantStore = useTenantStore()

const isLoading = ref(false)
const stats = ref({
  totalTenants: 0,
  activeTenants: 0,
  totalUsers: 0,
  totalRevenue: 0,
  pendingInvoices: 0,
  trialEndingSoon: 0
})

const recentActivity = ref([])
const topTenants = ref([])

const fetchDashboardData = async () => {
  isLoading.value = true
  try {
    await tenantStore.fetchTenants()
    
    // Calculate stats from tenant data
    const tenants = tenantStore.tenants
    stats.value = {
      totalTenants: tenants.length,
      activeTenants: tenants.filter(t => t.isActive).length,
      totalUsers: tenants.reduce((sum, t) => sum + (t.maxUsers || 0), 0),
      totalRevenue: tenants.reduce((sum, t) => sum + (t.subscriptionStatus === 'Active' ? 100 : 0), 0), // Mock calculation
      pendingInvoices: tenants.filter(t => t.subscriptionStatus === 'Pending').length,
      trialEndingSoon: tenants.filter(t => t.trialEndsAt && new Date(t.trialEndsAt) < new Date(Date.now() + 7 * 24 * 60 * 60 * 1000)).length
    }
    
    // Mock recent activity
    recentActivity.value = [
      { id: 1, type: 'tenant_created', message: 'New tenant "Acme Corp" registered', time: '2 hours ago', status: 'success' },
      { id: 2, type: 'payment_received', message: 'Payment received from "TechStart Inc"', time: '4 hours ago', status: 'success' },
      { id: 3, type: 'trial_ending', message: 'Trial ending soon for "Demo Company"', time: '6 hours ago', status: 'warning' },
      { id: 4, type: 'user_registered', message: 'New user registered in "Global Solutions"', time: '1 day ago', status: 'info' }
    ]
    
    // Top tenants by activity (mock data)
    topTenants.value = tenants.slice(0, 5).map(tenant => ({
      ...tenant,
      userCount: Math.floor(Math.random() * 50) + 1,
      lastActivity: new Date(Date.now() - Math.random() * 7 * 24 * 60 * 60 * 1000).toISOString()
    }))
    
  } catch (error) {
    console.error('Failed to fetch dashboard data:', error)
  } finally {
    isLoading.value = false
  }
}

const getStatusIcon = (status: string) => {
  switch (status) {
    case 'success':
      return CheckCircleIcon
    case 'warning':
      return ExclamationTriangleIcon
    case 'info':
      return ClockIcon
    default:
      return ClockIcon
  }
}

const getStatusColor = (status: string) => {
  switch (status) {
    case 'success':
      return 'text-green-600'
    case 'warning':
      return 'text-yellow-600'
    case 'info':
      return 'text-blue-600'
    default:
      return 'text-gray-600'
  }
}

const formatCurrency = (amount: number) => {
  return new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD'
  }).format(amount)
}

const formatDate = (dateString: string) => {
  return new Date(dateString).toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric'
  })
}

onMounted(() => {
  fetchDashboardData()
})
</script>

<template>
  <div class="min-h-screen bg-gray-50 py-6">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <!-- Header -->
      <div class="mb-8">
        <h1 class="text-3xl font-bold text-gray-900">Admin Dashboard</h1>
        <p class="mt-2 text-gray-600">Overview of your multi-tenant SaaS platform</p>
      </div>

      <!-- Stats Grid -->
      <div class="grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-4 mb-8">
        <!-- Total Tenants -->
        <div class="bg-white overflow-hidden shadow rounded-lg">
          <div class="p-5">
            <div class="flex items-center">
              <div class="flex-shrink-0">
                <BuildingOfficeIcon class="h-6 w-6 text-gray-400" />
              </div>
              <div class="ml-5 w-0 flex-1">
                <dl>
                  <dt class="text-sm font-medium text-gray-500 truncate">Total Tenants</dt>
                  <dd class="text-lg font-medium text-gray-900">{{ stats.totalTenants }}</dd>
                </dl>
              </div>
            </div>
          </div>
        </div>

        <!-- Active Tenants -->
        <div class="bg-white overflow-hidden shadow rounded-lg">
          <div class="p-5">
            <div class="flex items-center">
              <div class="flex-shrink-0">
                <CheckCircleIcon class="h-6 w-6 text-green-400" />
              </div>
              <div class="ml-5 w-0 flex-1">
                <dl>
                  <dt class="text-sm font-medium text-gray-500 truncate">Active Tenants</dt>
                  <dd class="text-lg font-medium text-gray-900">{{ stats.activeTenants }}</dd>
                </dl>
              </div>
            </div>
          </div>
        </div>

        <!-- Total Users -->
        <div class="bg-white overflow-hidden shadow rounded-lg">
          <div class="p-5">
            <div class="flex items-center">
              <div class="flex-shrink-0">
                <UsersIcon class="h-6 w-6 text-blue-400" />
              </div>
              <div class="ml-5 w-0 flex-1">
                <dl>
                  <dt class="text-sm font-medium text-gray-500 truncate">Total Users</dt>
                  <dd class="text-lg font-medium text-gray-900">{{ stats.totalUsers }}</dd>
                </dl>
              </div>
            </div>
          </div>
        </div>

        <!-- Total Revenue -->
        <div class="bg-white overflow-hidden shadow rounded-lg">
          <div class="p-5">
            <div class="flex items-center">
              <div class="flex-shrink-0">
                <CurrencyDollarIcon class="h-6 w-6 text-green-400" />
              </div>
              <div class="ml-5 w-0 flex-1">
                <dl>
                  <dt class="text-sm font-medium text-gray-500 truncate">Monthly Revenue</dt>
                  <dd class="text-lg font-medium text-gray-900">{{ formatCurrency(stats.totalRevenue) }}</dd>
                </dl>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Alerts -->
      <div class="mb-8">
        <div class="bg-white shadow rounded-lg">
          <div class="px-4 py-5 sm:p-6">
            <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Platform Alerts</h3>
            <div class="space-y-3">
              <div v-if="stats.pendingInvoices > 0" class="flex items-center p-3 bg-yellow-50 border border-yellow-200 rounded-md">
                <ExclamationTriangleIcon class="h-5 w-5 text-yellow-400 mr-3" />
                <span class="text-sm text-yellow-800">
                  {{ stats.pendingInvoices }} tenant(s) have pending invoices
                </span>
              </div>
              <div v-if="stats.trialEndingSoon > 0" class="flex items-center p-3 bg-orange-50 border border-orange-200 rounded-md">
                <ClockIcon class="h-5 w-5 text-orange-400 mr-3" />
                <span class="text-sm text-orange-800">
                  {{ stats.trialEndingSoon }} tenant(s) trial ending soon
                </span>
              </div>
              <div v-if="stats.pendingInvoices === 0 && stats.trialEndingSoon === 0" class="flex items-center p-3 bg-green-50 border border-green-200 rounded-md">
                <CheckCircleIcon class="h-5 w-5 text-green-400 mr-3" />
                <span class="text-sm text-green-800">All systems operational</span>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Main Content Grid -->
      <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
        <!-- Recent Activity -->
        <div class="bg-white shadow rounded-lg">
          <div class="px-4 py-5 sm:p-6">
            <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Recent Activity</h3>
            <div class="flow-root">
              <ul class="-mb-8">
                <li v-for="(activity, index) in recentActivity" :key="activity.id" class="relative pb-8">
                  <div v-if="index !== recentActivity.length - 1" class="absolute top-4 left-4 -ml-px h-full w-0.5 bg-gray-200"></div>
                  <div class="relative flex space-x-3">
                    <div>
                      <span class="h-8 w-8 rounded-full flex items-center justify-center ring-8 ring-white">
                        <component :is="getStatusIcon(activity.status)" class="h-5 w-5" :class="getStatusColor(activity.status)" />
                      </span>
                    </div>
                    <div class="min-w-0 flex-1 pt-1.5 flex justify-between space-x-4">
                      <div>
                        <p class="text-sm text-gray-500">{{ activity.message }}</p>
                      </div>
                      <div class="text-right text-sm whitespace-nowrap text-gray-500">
                        <time>{{ activity.time }}</time>
                      </div>
                    </div>
                  </div>
                </li>
              </ul>
            </div>
          </div>
        </div>

        <!-- Top Tenants -->
        <div class="bg-white shadow rounded-lg">
          <div class="px-4 py-5 sm:p-6">
            <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Top Tenants</h3>
            <div class="flow-root">
              <ul class="-my-5 divide-y divide-gray-200">
                <li v-for="tenant in topTenants" :key="tenant.id" class="py-4">
                  <div class="flex items-center space-x-4">
                    <div class="flex-shrink-0">
                      <div class="h-10 w-10 rounded-full bg-blue-600 flex items-center justify-center">
                        <span class="text-white font-medium text-sm">{{ tenant.name.charAt(0) }}</span>
                      </div>
                    </div>
                    <div class="flex-1 min-w-0">
                      <p class="text-sm font-medium text-gray-900 truncate">{{ tenant.name }}</p>
                      <p class="text-sm text-gray-500">{{ tenant.userCount }} users</p>
                    </div>
                    <div class="flex-shrink-0">
                      <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium"
                            :class="tenant.isActive ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'">
                        {{ tenant.isActive ? 'Active' : 'Inactive' }}
                      </span>
                    </div>
                  </div>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>

      <!-- Quick Actions -->
      <div class="mt-8 bg-white shadow rounded-lg">
        <div class="px-4 py-5 sm:p-6">
          <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Quick Actions</h3>
          <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
            <button class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700">
              <BuildingOfficeIcon class="h-4 w-4 mr-2" />
              Create Tenant
            </button>
            <button class="inline-flex items-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50">
              <UsersIcon class="h-4 w-4 mr-2" />
              Manage Users
            </button>
            <button class="inline-flex items-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50">
              <CreditCardIcon class="h-4 w-4 mr-2" />
              View Billing
            </button>
            <button class="inline-flex items-center px-4 py-2 border border-gray-300 text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50">
              <ChartBarIcon class="h-4 w-4 mr-2" />
              Generate Report
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Additional utility classes */
.grid { display: grid; }
.grid-cols-1 { grid-template-columns: repeat(1, minmax(0, 1fr)); }
.gap-5 { gap: 1.25rem; }
.gap-8 { gap: 2rem; }
.gap-4 { gap: 1rem; }

.sm\:grid-cols-2 { grid-template-columns: repeat(2, minmax(0, 1fr)); }
.lg\:grid-cols-4 { grid-template-columns: repeat(4, minmax(0, 1fr)); }
.lg\:grid-cols-2 { grid-template-columns: repeat(2, minmax(0, 1fr)); }

.overflow-hidden { overflow: hidden; }
.shadow { box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06); }
.rounded-lg { border-radius: 0.5rem; }
.rounded-md { border-radius: 0.375rem; }
.rounded-full { border-radius: 9999px; }

.p-5 { padding: 1.25rem; }
.p-3 { padding: 0.75rem; }
.px-4 { padding-left: 1rem; padding-right: 1rem; }
.py-5 { padding-top: 1.25rem; padding-bottom: 1.25rem; }
.py-4 { padding-top: 1rem; padding-bottom: 1rem; }
.py-2 { padding-top: 0.5rem; padding-bottom: 0.5rem; }
.pb-8 { padding-bottom: 2rem; }
.pt-1\.5 { padding-top: 0.375rem; }

.mb-8 { margin-bottom: 2rem; }
.mb-4 { margin-bottom: 1rem; }
.mt-8 { margin-top: 2rem; }
.mt-2 { margin-top: 0.5rem; }
.ml-5 { margin-left: 1.25rem; }
.mr-3 { margin-right: 0.75rem; }
.mr-2 { margin-right: 0.5rem; }
.-mb-8 { margin-bottom: -2rem; }
.-my-5 { margin-top: -1.25rem; margin-bottom: -1.25rem; }

.text-3xl { font-size: 1.875rem; line-height: 2.25rem; }
.text-lg { font-size: 1.125rem; line-height: 1.75rem; }
.text-sm { font-size: 0.875rem; line-height: 1.25rem; }
.text-xs { font-size: 0.75rem; line-height: 1rem; }

.font-bold { font-weight: 700; }
.font-medium { font-weight: 500; }

.text-gray-900 { color: #111827; }
.text-gray-600 { color: #4b5563; }
.text-gray-500 { color: #6b7280; }
.text-gray-400 { color: #9ca3af; }
.text-green-400 { color: #4ade80; }
.text-blue-400 { color: #60a5fa; }
.text-yellow-400 { color: #facc15; }
.text-orange-400 { color: #fb923c; }
.text-green-800 { color: #166534; }
.text-yellow-800 { color: #92400e; }
.text-orange-800 { color: #9a3412; }
.text-red-800 { color: #991b1b; }

.bg-white { background-color: #ffffff; }
.bg-yellow-50 { background-color: #fefce8; }
.bg-orange-50 { background-color: #fff7ed; }
.bg-green-50 { background-color: #f0fdf4; }
.bg-blue-600 { background-color: #2563eb; }
.bg-green-100 { background-color: #dcfce7; }
.bg-red-100 { background-color: #fee2e2; }

.border-yellow-200 { border-color: #fde68a; }
.border-orange-200 { border-color: #fed7aa; }
.border-green-200 { border-color: #bbf7d0; }
.border-gray-300 { border-color: #d1d5db; }
.border-gray-200 { border-color: #e5e7eb; }

.border { border-width: 1px; }

.flex { display: flex; }
.inline-flex { display: inline-flex; }
.items-center { align-items: center; }
.justify-center { justify-content: center; }
.space-x-3 > * + * { margin-left: 0.75rem; }
.space-x-4 > * + * { margin-left: 1rem; }

.h-6 { height: 1.5rem; }
.h-5 { height: 1.25rem; }
.h-4 { height: 1rem; }
.h-10 { height: 2.5rem; }
.w-6 { width: 1.5rem; }
.w-5 { width: 1.25rem; }
.w-4 { width: 1rem; }
.w-10 { width: 2.5rem; }
.w-0\.5 { width: 0.125rem; }

.ring-8 { box-shadow: 0 0 0 8px rgba(255, 255, 255, 1); }
.ring-white { box-shadow: 0 0 0 1px rgba(255, 255, 255, 1); }

.truncate { overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }
.whitespace-nowrap { white-space: nowrap; }

.relative { position: relative; }
.absolute { position: absolute; }
.top-4 { top: 1rem; }
.left-4 { left: 1rem; }

.divide-y > * + * { border-top-width: 1px; border-top-color: #e5e7eb; }
.divide-gray-200 > * + * { border-top-color: #e5e7eb; }

.flow-root { display: flow-root; }

.min-w-0 { min-width: 0px; }
.flex-1 { flex: 1 1 0%; }
.flex-shrink-0 { flex-shrink: 0; }

.hover\:bg-blue-700:hover { background-color: #1d4ed8; }
.hover\:bg-gray-50:hover { background-color: #f9fafb; }

@media (min-width: 640px) {
  .sm\:grid-cols-2 { grid-template-columns: repeat(2, minmax(0, 1fr)); }
}

@media (min-width: 1024px) {
  .lg\:grid-cols-4 { grid-template-columns: repeat(4, minmax(0, 1fr)); }
  .lg\:grid-cols-2 { grid-template-columns: repeat(2, minmax(0, 1fr)); }
}
</style>
