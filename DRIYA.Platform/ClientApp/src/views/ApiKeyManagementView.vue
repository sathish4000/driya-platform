<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { 
  PlusIcon, 
  KeyIcon,
  EyeIcon,
  EyeSlashIcon,
  TrashIcon,
  CheckCircleIcon,
  XCircleIcon
} from '@heroicons/vue/24/outline'

const isLoading = ref(false)
const apiKeys = ref([])
const showCreateModal = ref(false)

const fetchApiKeys = async () => {
  isLoading.value = true
  try {
    // Mock data
    apiKeys.value = [
      {
        id: '1',
        name: 'Production API Key',
        description: 'Used for production integrations',
        keyPrefix: 'pk_live_',
        isActive: true,
        lastUsedAt: '2024-01-15T10:30:00Z',
        createdAt: '2024-01-01T00:00:00Z',
        permissions: ['read', 'write']
      },
      {
        id: '2',
        name: 'Development API Key',
        description: 'Used for development and testing',
        keyPrefix: 'pk_test_',
        isActive: true,
        lastUsedAt: '2024-01-14T15:45:00Z',
        createdAt: '2024-01-05T00:00:00Z',
        permissions: ['read']
      }
    ]
  } catch (error) {
    console.error('Failed to fetch API keys:', error)
  } finally {
    isLoading.value = false
  }
}

const getStatusBadge = (isActive) => {
  return isActive 
    ? { text: 'Active', class: 'bg-green-100 text-green-800', icon: CheckCircleIcon }
    : { text: 'Inactive', class: 'bg-red-100 text-red-800', icon: XCircleIcon }
}

const formatDate = (dateString) => {
  if (!dateString) return 'N/A'
  return new Date(dateString).toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric'
  })
}

onMounted(() => {
  fetchApiKeys()
})
</script>

<template>
  <div class="min-h-screen bg-gray-50 py-6">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <!-- Header -->
      <div class="mb-8">
        <div class="flex justify-between items-center">
          <div>
            <h1 class="text-3xl font-bold text-gray-900">API Key Management</h1>
            <p class="mt-2 text-gray-600">Manage your API keys for integrations</p>
          </div>
          <button class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700">
            <PlusIcon class="h-4 w-4 mr-2" />
            Create API Key
          </button>
        </div>
      </div>

      <!-- API Keys Table -->
      <div class="bg-white shadow rounded-lg">
        <div class="px-4 py-5 sm:p-6">
          <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">API Keys ({{ apiKeys.length }})</h3>
          <div class="overflow-x-auto">
            <table class="min-w-full divide-y divide-gray-200">
              <thead class="bg-gray-50">
                <tr>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    API Key
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Description
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Status
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Last Used
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Created
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Actions
                  </th>
                </tr>
              </thead>
              <tbody class="bg-white divide-y divide-gray-200">
                <tr v-for="apiKey in apiKeys" :key="apiKey.id" class="hover:bg-gray-50">
                  <td class="px-6 py-4 whitespace-nowrap">
                    <div class="flex items-center">
                      <KeyIcon class="h-5 w-5 text-gray-400 mr-2" />
                      <div>
                        <div class="text-sm font-medium text-gray-900">{{ apiKey.name }}</div>
                        <div class="text-sm text-gray-500">{{ apiKey.keyPrefix }}••••••••••••••••</div>
                      </div>
                    </div>
                  </td>
                  <td class="px-6 py-4">
                    <div class="text-sm text-gray-900">{{ apiKey.description }}</div>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap">
                    <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium"
                          :class="getStatusBadge(apiKey.isActive).class">
                      <component :is="getStatusBadge(apiKey.isActive).icon" class="h-3 w-3 mr-1" />
                      {{ getStatusBadge(apiKey.isActive).text }}
                    </span>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                    {{ formatDate(apiKey.lastUsedAt) }}
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                    {{ formatDate(apiKey.createdAt) }}
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
                    <div class="flex space-x-2">
                      <button class="text-blue-600 hover:text-blue-900">
                        <EyeIcon class="h-4 w-4" />
                      </button>
                      <button class="text-red-600 hover:text-red-900">
                        <TrashIcon class="h-4 w-4" />
                      </button>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* Additional utility classes */
.overflow-hidden { overflow: hidden; }
.overflow-x-auto { overflow-x: auto; }
.shadow { box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06); }
.rounded-lg { border-radius: 0.5rem; }
.rounded-md { border-radius: 0.375rem; }

.px-4 { padding-left: 1rem; padding-right: 1rem; }
.py-2 { padding-top: 0.5rem; padding-bottom: 0.5rem; }
.py-3 { padding-top: 0.75rem; padding-bottom: 0.75rem; }
.py-4 { padding-top: 1rem; padding-bottom: 1rem; }
.py-5 { padding-top: 1.25rem; padding-bottom: 1.25rem; }
.px-6 { padding-left: 1.5rem; padding-right: 1.5rem; }

.mb-4 { margin-bottom: 1rem; }
.mb-8 { margin-bottom: 2rem; }
.mr-2 { margin-right: 0.5rem; }
.mt-2 { margin-top: 0.5rem; }

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
.text-blue-600 { color: #2563eb; }
.text-red-600 { color: #dc2626; }
.text-green-800 { color: #166534; }
.text-red-800 { color: #991b1b; }

.bg-white { background-color: #ffffff; }
.bg-gray-50 { background-color: #f9fafb; }
.bg-blue-600 { background-color: #2563eb; }
.bg-green-100 { background-color: #dcfce7; }
.bg-red-100 { background-color: #fee2e2; }

.border-gray-200 { border-color: #e5e7eb; }

.border { border-width: 1px; }

.flex { display: flex; }
.inline-flex { display: inline-flex; }
.items-center { align-items: center; }
.justify-between { justify-content: space-between; }
.space-x-2 > * + * { margin-left: 0.5rem; }

.h-3 { height: 0.75rem; }
.h-4 { height: 1rem; }
.h-5 { height: 1.25rem; }
.w-3 { width: 0.75rem; }
.w-4 { width: 1rem; }
.w-5 { width: 1.25rem; }

.whitespace-nowrap { white-space: nowrap; }

.divide-y > * + * { border-top-width: 1px; border-top-color: #e5e7eb; }
.divide-gray-200 > * + * { border-top-color: #e5e7eb; }

.min-w-full { min-width: 100%; }

.hover\:bg-gray-50:hover { background-color: #f9fafb; }
.hover\:bg-blue-700:hover { background-color: #1d4ed8; }
.hover\:text-blue-900:hover { color: #1e3a8a; }
.hover\:text-red-900:hover { color: #7f1d1d; }
</style>
