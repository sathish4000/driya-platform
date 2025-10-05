<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { 
  PlusIcon, 
  Cog6ToothIcon,
  ArrowsRightLeftIcon,
  CheckIcon,
  XMarkIcon,
  EyeIcon,
  EyeSlashIcon,
  BuildingOfficeIcon,
  UserIcon,
  TrashIcon
} from '@heroicons/vue/24/outline'

const isLoading = ref(false)
const features = ref([])
const featureFlags = ref([])
const showCreateModal = ref(false)
const selectedFeature = ref(null)

// Form data
const formData = ref({
  key: '',
  name: '',
  description: '',
  type: 'boolean',
  defaultValue: 'true',
  value: '',
  level: 'system', // system, tenant, user
  tenantId: '',
  userId: ''
})

const errors = ref<Record<string, string>>({})

const systemFeatures = computed(() => features.value.filter(f => f.type === 'system'))
const tenantFeatures = computed(() => features.value.filter(f => f.type === 'tenant'))
const userFeatures = computed(() => features.value.filter(f => f.type === 'user'))

const validateForm = () => {
  errors.value = {}
  
  if (!formData.value.key) {
    errors.value.key = 'Feature key is required'
  }
  
  if (!formData.value.name) {
    errors.value.name = 'Feature name is required'
  }
  
  return Object.keys(errors.value).length === 0
}

const openCreateModal = () => {
  formData.value = {
    key: '',
    name: '',
    description: '',
    type: 'boolean',
    defaultValue: 'true',
    value: '',
    level: 'system',
    tenantId: '',
    userId: ''
  }
  errors.value = {}
  showCreateModal.value = true
}

const handleCreate = async () => {
  if (!validateForm()) return
  
  try {
    // Mock API call
    const newFeature = {
      id: Date.now().toString(),
      ...formData.value,
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    }
    
    features.value.push(newFeature)
    showCreateModal.value = false
  } catch (error) {
    console.error('Failed to create feature:', error)
  }
}

const toggleFeature = async (feature) => {
  try {
    // Mock API call
    feature.value = feature.value === 'true' ? 'false' : 'true'
    feature.updatedAt = new Date().toISOString()
  } catch (error) {
    console.error('Failed to toggle feature:', error)
  }
}

const deleteFeature = async (featureId) => {
  try {
    // Mock API call
    features.value = features.value.filter(f => f.id !== featureId)
  } catch (error) {
    console.error('Failed to delete feature:', error)
  }
}

const getFeatureIcon = (feature) => {
  return feature.value === 'true' ? CheckIcon : XMarkIcon
}

const getFeatureColor = (feature) => {
  return feature.value === 'true' ? 'text-green-600' : 'text-red-600'
}

const getFeatureBadge = (feature) => {
  return feature.value === 'true' 
    ? { text: 'Enabled', class: 'bg-green-100 text-green-800' }
    : { text: 'Disabled', class: 'bg-red-100 text-red-800' }
}

const formatDate = (dateString) => {
  if (!dateString) return 'N/A'
  return new Date(dateString).toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric'
  })
}

onMounted(async () => {
  isLoading.value = true
  try {
    // Mock data
    features.value = [
      {
        id: '1',
        key: 'advanced_analytics',
        name: 'Advanced Analytics',
        description: 'Enable advanced analytics dashboard',
        type: 'boolean',
        defaultValue: 'true',
        value: 'true',
        level: 'system',
        createdAt: '2024-01-15T10:00:00Z',
        updatedAt: '2024-01-15T10:00:00Z'
      },
      {
        id: '2',
        key: 'api_rate_limiting',
        name: 'API Rate Limiting',
        description: 'Enable rate limiting for API calls',
        type: 'boolean',
        defaultValue: 'true',
        value: 'false',
        level: 'tenant',
        createdAt: '2024-01-15T10:00:00Z',
        updatedAt: '2024-01-15T10:00:00Z'
      },
      {
        id: '3',
        key: 'custom_branding',
        name: 'Custom Branding',
        description: 'Allow tenants to customize branding',
        type: 'boolean',
        defaultValue: 'false',
        value: 'true',
        level: 'tenant',
        createdAt: '2024-01-15T10:00:00Z',
        updatedAt: '2024-01-15T10:00:00Z'
      },
      {
        id: '4',
        key: 'max_users',
        name: 'Maximum Users',
        description: 'Maximum number of users per tenant',
        type: 'number',
        defaultValue: '10',
        value: '25',
        level: 'tenant',
        createdAt: '2024-01-15T10:00:00Z',
        updatedAt: '2024-01-15T10:00:00Z'
      }
    ]
  } catch (error) {
    console.error('Failed to fetch features:', error)
  } finally {
    isLoading.value = false
  }
})
</script>

<template>
  <div class="min-h-screen bg-gray-50 py-6">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <!-- Header -->
      <div class="mb-8">
        <div class="flex justify-between items-center">
          <div>
            <h1 class="text-3xl font-bold text-gray-900">Feature Management</h1>
            <p class="mt-2 text-gray-600">Manage feature flags and system capabilities</p>
          </div>
          <button
            @click="openCreateModal"
            class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700"
          >
            <PlusIcon class="h-4 w-4 mr-2" />
            Add Feature
          </button>
        </div>
      </div>

      <!-- Feature Categories -->
      <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <!-- System Features -->
        <div class="bg-white shadow rounded-lg">
          <div class="px-4 py-5 sm:p-6">
            <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">
              <Cog6ToothIcon class="h-5 w-5 inline mr-2 text-blue-600" />
              System Features
            </h3>
            <div class="space-y-3">
              <div v-for="feature in systemFeatures" :key="feature.id" class="flex items-center justify-between p-3 border border-gray-200 rounded-md">
                <div class="flex-1">
                  <div class="flex items-center">
                    <component :is="getFeatureIcon(feature)" class="h-4 w-4 mr-2" :class="getFeatureColor(feature)" />
                    <div>
                      <p class="text-sm font-medium text-gray-900">{{ feature.name }}</p>
                      <p class="text-xs text-gray-500">{{ feature.description }}</p>
                    </div>
                  </div>
                </div>
                <div class="flex items-center space-x-2">
                  <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium"
                        :class="getFeatureBadge(feature).class">
                    {{ getFeatureBadge(feature).text }}
                  </span>
                  <button
                    @click="toggleFeature(feature)"
                    class="text-gray-400 hover:text-gray-600"
                  >
                    <ArrowsRightLeftIcon class="h-4 w-4" />
                  </button>
                </div>
              </div>
              <div v-if="systemFeatures.length === 0" class="text-center py-4 text-gray-500">
                No system features configured
              </div>
            </div>
          </div>
        </div>

        <!-- Tenant Features -->
        <div class="bg-white shadow rounded-lg">
          <div class="px-4 py-5 sm:p-6">
            <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">
              <BuildingOfficeIcon class="h-5 w-5 inline mr-2 text-green-600" />
              Tenant Features
            </h3>
            <div class="space-y-3">
              <div v-for="feature in tenantFeatures" :key="feature.id" class="flex items-center justify-between p-3 border border-gray-200 rounded-md">
                <div class="flex-1">
                  <div class="flex items-center">
                    <component :is="getFeatureIcon(feature)" class="h-4 w-4 mr-2" :class="getFeatureColor(feature)" />
                    <div>
                      <p class="text-sm font-medium text-gray-900">{{ feature.name }}</p>
                      <p class="text-xs text-gray-500">{{ feature.description }}</p>
                      <p v-if="feature.type === 'number'" class="text-xs text-blue-600">Value: {{ feature.value }}</p>
                    </div>
                  </div>
                </div>
                <div class="flex items-center space-x-2">
                  <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium"
                        :class="getFeatureBadge(feature).class">
                    {{ getFeatureBadge(feature).text }}
                  </span>
                  <button
                    @click="toggleFeature(feature)"
                    class="text-gray-400 hover:text-gray-600"
                  >
                    <ArrowsRightLeftIcon class="h-4 w-4" />
                  </button>
                </div>
              </div>
              <div v-if="tenantFeatures.length === 0" class="text-center py-4 text-gray-500">
                No tenant features configured
              </div>
            </div>
          </div>
        </div>

        <!-- User Features -->
        <div class="bg-white shadow rounded-lg">
          <div class="px-4 py-5 sm:p-6">
            <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">
              <UserIcon class="h-5 w-5 inline mr-2 text-purple-600" />
              User Features
            </h3>
            <div class="space-y-3">
              <div v-for="feature in userFeatures" :key="feature.id" class="flex items-center justify-between p-3 border border-gray-200 rounded-md">
                <div class="flex-1">
                  <div class="flex items-center">
                    <component :is="getFeatureIcon(feature)" class="h-4 w-4 mr-2" :class="getFeatureColor(feature)" />
                    <div>
                      <p class="text-sm font-medium text-gray-900">{{ feature.name }}</p>
                      <p class="text-xs text-gray-500">{{ feature.description }}</p>
                    </div>
                  </div>
                </div>
                <div class="flex items-center space-x-2">
                  <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium"
                        :class="getFeatureBadge(feature).class">
                    {{ getFeatureBadge(feature).text }}
                  </span>
                  <button
                    @click="toggleFeature(feature)"
                    class="text-gray-400 hover:text-gray-600"
                  >
                    <ArrowsRightLeftIcon class="h-4 w-4" />
                  </button>
                </div>
              </div>
              <div v-if="userFeatures.length === 0" class="text-center py-4 text-gray-500">
                No user features configured
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Feature Flags Table -->
      <div class="mt-8 bg-white shadow rounded-lg">
        <div class="px-4 py-5 sm:p-6">
          <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">All Feature Flags</h3>
          <div class="overflow-x-auto">
            <table class="min-w-full divide-y divide-gray-200">
              <thead class="bg-gray-50">
                <tr>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Feature
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Type
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Level
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Status
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Updated
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Actions
                  </th>
                </tr>
              </thead>
              <tbody class="bg-white divide-y divide-gray-200">
                <tr v-for="feature in features" :key="feature.id" class="hover:bg-gray-50">
                  <td class="px-6 py-4 whitespace-nowrap">
                    <div>
                      <div class="text-sm font-medium text-gray-900">{{ feature.name }}</div>
                      <div class="text-sm text-gray-500">{{ feature.key }}</div>
                      <div class="text-xs text-gray-400">{{ feature.description }}</div>
                    </div>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap">
                    <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-gray-100 text-gray-800">
                      {{ feature.type }}
                    </span>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap">
                    <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium"
                          :class="{
                            'bg-blue-100 text-blue-800': feature.level === 'system',
                            'bg-green-100 text-green-800': feature.level === 'tenant',
                            'bg-purple-100 text-purple-800': feature.level === 'user'
                          }">
                      {{ feature.level }}
                    </span>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap">
                    <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium"
                          :class="getFeatureBadge(feature).class">
                      {{ getFeatureBadge(feature).text }}
                    </span>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                    {{ formatDate(feature.updatedAt) }}
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
                    <div class="flex space-x-2">
                      <button
                        @click="toggleFeature(feature)"
                        class="text-blue-600 hover:text-blue-900"
                      >
                        <ArrowsRightLeftIcon class="h-4 w-4" />
                      </button>
                      <button
                        @click="deleteFeature(feature.id)"
                        class="text-red-600 hover:text-red-900"
                      >
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

      <!-- Create Feature Modal -->
      <div v-if="showCreateModal" class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full z-50">
        <div class="relative top-20 mx-auto p-5 border w-96 shadow-lg rounded-md bg-white">
          <div class="mt-3">
            <h3 class="text-lg font-medium text-gray-900 mb-4">Create Feature Flag</h3>
            
            <form @submit.prevent="handleCreate">
              <div class="space-y-4">
                <!-- Feature Key -->
                <div>
                  <label class="block text-sm font-medium text-gray-700">Feature Key</label>
                  <input
                    v-model="formData.key"
                    type="text"
                    class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                    placeholder="e.g., advanced_analytics"
                  />
                  <p v-if="errors.key" class="mt-1 text-sm text-red-600">{{ errors.key }}</p>
                </div>

                <!-- Name -->
                <div>
                  <label class="block text-sm font-medium text-gray-700">Name</label>
                  <input
                    v-model="formData.name"
                    type="text"
                    class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                    placeholder="Advanced Analytics"
                  />
                  <p v-if="errors.name" class="mt-1 text-sm text-red-600">{{ errors.name }}</p>
                </div>

                <!-- Description -->
                <div>
                  <label class="block text-sm font-medium text-gray-700">Description</label>
                  <textarea
                    v-model="formData.description"
                    rows="3"
                    class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                    placeholder="Enable advanced analytics dashboard"
                  ></textarea>
                </div>

                <!-- Type -->
                <div>
                  <label class="block text-sm font-medium text-gray-700">Type</label>
                  <select
                    v-model="formData.type"
                    class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  >
                    <option value="boolean">Boolean</option>
                    <option value="number">Number</option>
                    <option value="string">String</option>
                  </select>
                </div>

                <!-- Level -->
                <div>
                  <label class="block text-sm font-medium text-gray-700">Level</label>
                  <select
                    v-model="formData.level"
                    class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  >
                    <option value="system">System</option>
                    <option value="tenant">Tenant</option>
                    <option value="user">User</option>
                  </select>
                </div>

                <!-- Default Value -->
                <div>
                  <label class="block text-sm font-medium text-gray-700">Default Value</label>
                  <input
                    v-model="formData.defaultValue"
                    :type="formData.type === 'number' ? 'number' : 'text'"
                    class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  />
                </div>
              </div>

              <div class="mt-6 flex justify-end space-x-3">
                <button
                  type="button"
                  @click="showCreateModal = false"
                  class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  class="px-4 py-2 border border-transparent rounded-md text-sm font-medium text-white bg-blue-600 hover:bg-blue-700"
                >
                  Create
                </button>
              </div>
            </form>
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
.gap-6 { gap: 1.5rem; }

.lg\:grid-cols-3 { grid-template-columns: repeat(3, minmax(0, 1fr)); }

.overflow-hidden { overflow: hidden; }
.overflow-x-auto { overflow-x: auto; }
.shadow { box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06); }
.rounded-lg { border-radius: 0.5rem; }
.rounded-md { border-radius: 0.375rem; }
.rounded-full { border-radius: 9999px; }

.px-4 { padding-left: 1rem; padding-right: 1rem; }
.py-2 { padding-top: 0.5rem; padding-bottom: 0.5rem; }
.py-3 { padding-top: 0.75rem; padding-bottom: 0.75rem; }
.py-4 { padding-top: 1rem; padding-bottom: 1rem; }
.py-5 { padding-top: 1.25rem; padding-bottom: 1.25rem; }
.px-6 { padding-left: 1.5rem; padding-right: 1.5rem; }
.p-3 { padding: 0.75rem; }

.mb-4 { margin-bottom: 1rem; }
.mb-8 { margin-bottom: 2rem; }
.mt-8 { margin-top: 2rem; }
.mt-6 { margin-top: 1.5rem; }
.ml-2 { margin-left: 0.5rem; }
.mr-2 { margin-right: 0.5rem; }
.mt-1 { margin-top: 0.25rem; }
.mt-2 { margin-top: 0.5rem; }
.mt-3 { margin-top: 0.75rem; }

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
.text-green-600 { color: #059669; }
.text-purple-600 { color: #7c3aed; }
.text-red-600 { color: #dc2626; }
.text-green-800 { color: #166534; }
.text-red-800 { color: #991b1b; }
.text-blue-800 { color: #1e40af; }
.text-purple-800 { color: #5b21b6; }
.text-gray-800 { color: #1f2937; }

.bg-white { background-color: #ffffff; }
.bg-gray-50 { background-color: #f9fafb; }
.bg-blue-600 { background-color: #2563eb; }
.bg-green-100 { background-color: #dcfce7; }
.bg-red-100 { background-color: #fee2e2; }
.bg-blue-100 { background-color: #dbeafe; }
.bg-purple-100 { background-color: #f3e8ff; }
.bg-gray-100 { background-color: #f3f4f6; }

.border-gray-300 { border-color: #d1d5db; }
.border-gray-200 { border-color: #e5e7eb; }

.border { border-width: 1px; }

.flex { display: flex; }
.inline-flex { display: inline-flex; }
.items-center { align-items: center; }
.justify-between { justify-content: space-between; }
.justify-end { justify-content: flex-end; }
.space-x-2 > * + * { margin-left: 0.5rem; }
.space-x-3 > * + * { margin-left: 0.75rem; }
.space-y-3 > * + * { margin-top: 0.75rem; }
.space-y-4 > * + * { margin-top: 1rem; }

.h-4 { height: 1rem; }
.h-5 { height: 1.25rem; }
.w-4 { width: 1rem; }
.w-5 { width: 1.25rem; }
.w-96 { width: 24rem; }

.whitespace-nowrap { white-space: nowrap; }

.relative { position: relative; }
.absolute { position: absolute; }
.fixed { position: fixed; }
.inset-0 { top: 0; right: 0; bottom: 0; left: 0; }
.top-20 { top: 5rem; }
.z-50 { z-index: 50; }

.divide-y > * + * { border-top-width: 1px; border-top-color: #e5e7eb; }
.divide-gray-200 > * + * { border-top-color: #e5e7eb; }

.min-w-full { min-width: 100%; }

.hover\:bg-gray-50:hover { background-color: #f9fafb; }
.hover\:bg-blue-700:hover { background-color: #1d4ed8; }
.hover\:bg-gray-50:hover { background-color: #f9fafb; }
.hover\:text-blue-900:hover { color: #1e3a8a; }
.hover\:text-red-900:hover { color: #7f1d1d; }
.hover\:text-gray-600:hover { color: #4b5563; }

.focus\:ring-blue-500:focus { box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1); }
.focus\:border-blue-500:focus { border-color: #3b82f6; }

.bg-opacity-50 { background-color: rgba(75, 85, 99, 0.5); }

.overflow-y-auto { overflow-y: auto; }

.inline { display: inline; }

@media (min-width: 1024px) {
  .lg\:grid-cols-3 { grid-template-columns: repeat(3, minmax(0, 1fr)); }
}
</style>
