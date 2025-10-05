<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useTenantStore } from '../stores/tenant'
import { 
  PlusIcon, 
  MagnifyingGlassIcon,
  PencilIcon,
  TrashIcon,
  EyeIcon,
  FunnelIcon,
  BuildingOfficeIcon,
  CheckCircleIcon,
  XCircleIcon,
  ClockIcon
} from '@heroicons/vue/24/outline'

const tenantStore = useTenantStore()

const isLoading = ref(false)
const showCreateModal = ref(false)
const showEditModal = ref(false)
const showDeleteModal = ref(false)
const selectedTenant = ref(null)
const searchTerm = ref('')
const statusFilter = ref('all')

// Form data
const formData = ref({
  tenantId: '',
  name: '',
  description: '',
  domain: '',
  contactEmail: '',
  contactPhone: '',
  address: '',
  primaryColor: '#3B82F6',
  logoUrl: '',
  maxUsers: 10,
  maxStorageGB: 10,
  planId: ''
})

const errors = ref<Record<string, string>>({})

const filteredTenants = computed(() => {
  let tenants = tenantStore.tenants
  
  // Search filter
  if (searchTerm.value) {
    tenants = tenants.filter(tenant => 
      tenant.name.toLowerCase().includes(searchTerm.value.toLowerCase()) ||
      tenant.tenantId.toLowerCase().includes(searchTerm.value.toLowerCase()) ||
      tenant.contactEmail?.toLowerCase().includes(searchTerm.value.toLowerCase())
    )
  }
  
  // Status filter
  if (statusFilter.value !== 'all') {
    tenants = tenants.filter(tenant => {
      if (statusFilter.value === 'active') return tenant.isActive
      if (statusFilter.value === 'inactive') return !tenant.isActive
      if (statusFilter.value === 'trial') return tenant.subscriptionStatus === 'Trial'
      return true
    })
  }
  
  return tenants
})

const validateForm = () => {
  errors.value = {}
  
  if (!formData.value.tenantId) {
    errors.value.tenantId = 'Tenant ID is required'
  }
  
  if (!formData.value.name) {
    errors.value.name = 'Name is required'
  }
  
  if (formData.value.contactEmail && !/\S+@\S+\.\S+/.test(formData.value.contactEmail)) {
    errors.value.contactEmail = 'Email is invalid'
  }
  
  return Object.keys(errors.value).length === 0
}

const openCreateModal = () => {
  formData.value = {
    tenantId: '',
    name: '',
    description: '',
    domain: '',
    contactEmail: '',
    contactPhone: '',
    address: '',
    primaryColor: '#3B82F6',
    logoUrl: '',
    maxUsers: 10,
    maxStorageGB: 10,
    planId: ''
  }
  errors.value = {}
  showCreateModal.value = true
}

const openEditModal = (tenant) => {
  selectedTenant.value = tenant
  formData.value = {
    tenantId: tenant.tenantId,
    name: tenant.name,
    description: tenant.description || '',
    domain: tenant.domain || '',
    contactEmail: tenant.contactEmail || '',
    contactPhone: tenant.contactPhone || '',
    address: tenant.address || '',
    primaryColor: tenant.primaryColor || '#3B82F6',
    logoUrl: tenant.logoUrl || '',
    maxUsers: tenant.maxUsers || 10,
    maxStorageGB: tenant.maxStorageGB || 10,
    planId: tenant.currentPlanId || ''
  }
  errors.value = {}
  showEditModal.value = true
}

const openDeleteModal = (tenant) => {
  selectedTenant.value = tenant
  showDeleteModal.value = true
}

const handleCreate = async () => {
  if (!validateForm()) return
  
  try {
    await tenantStore.createTenant(formData.value)
    showCreateModal.value = false
  } catch (error) {
    console.error('Failed to create tenant:', error)
  }
}

const handleUpdate = async () => {
  if (!validateForm() || !selectedTenant.value) return
  
  try {
    await tenantStore.updateTenant(selectedTenant.value.id, formData.value)
    showEditModal.value = false
  } catch (error) {
    console.error('Failed to update tenant:', error)
  }
}

const handleDelete = async () => {
  if (!selectedTenant.value) return
  
  try {
    await tenantStore.deleteTenant(selectedTenant.value.id)
    showDeleteModal.value = false
  } catch (error) {
    console.error('Failed to delete tenant:', error)
  }
}

const getStatusBadge = (tenant) => {
  if (!tenant.isActive) {
    return { text: 'Inactive', class: 'bg-red-100 text-red-800' }
  }
  
  switch (tenant.subscriptionStatus) {
    case 'Active':
      return { text: 'Active', class: 'bg-green-100 text-green-800' }
    case 'Trial':
      return { text: 'Trial', class: 'bg-blue-100 text-blue-800' }
    case 'Pending':
      return { text: 'Pending', class: 'bg-yellow-100 text-yellow-800' }
    case 'Suspended':
      return { text: 'Suspended', class: 'bg-red-100 text-red-800' }
    default:
      return { text: 'Unknown', class: 'bg-gray-100 text-gray-800' }
  }
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
    await tenantStore.fetchTenants()
  } catch (error) {
    console.error('Failed to fetch tenants:', error)
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
            <h1 class="text-3xl font-bold text-gray-900">Tenant Management</h1>
            <p class="mt-2 text-gray-600">Manage all tenants in your platform</p>
          </div>
          <button
            @click="openCreateModal"
            class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700"
          >
            <PlusIcon class="h-4 w-4 mr-2" />
            Create Tenant
          </button>
        </div>
      </div>

      <!-- Filters -->
      <div class="bg-white shadow rounded-lg mb-6">
        <div class="px-4 py-5 sm:p-6">
          <div class="grid grid-cols-1 gap-4 sm:grid-cols-3">
            <!-- Search -->
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-2">Search</label>
              <div class="relative">
                <MagnifyingGlassIcon class="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-gray-400" />
                <input
                  v-model="searchTerm"
                  type="text"
                  class="pl-10 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  placeholder="Search tenants..."
                />
              </div>
            </div>

            <!-- Status Filter -->
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-2">Status</label>
              <select
                v-model="statusFilter"
                class="block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
              >
                <option value="all">All Statuses</option>
                <option value="active">Active</option>
                <option value="inactive">Inactive</option>
                <option value="trial">Trial</option>
              </select>
            </div>

            <!-- Results Count -->
            <div class="flex items-end">
              <p class="text-sm text-gray-500">
                {{ filteredTenants.length }} tenant{{ filteredTenants.length !== 1 ? 's' : '' }} found
              </p>
            </div>
          </div>
        </div>
      </div>

      <!-- Tenants Table -->
      <div class="bg-white shadow rounded-lg overflow-hidden">
        <div class="overflow-x-auto">
          <table class="min-w-full divide-y divide-gray-200">
            <thead class="bg-gray-50">
              <tr>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Tenant
                </th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Application
                </th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Status
                </th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Contact
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
              <tr v-for="tenant in filteredTenants" :key="tenant.id" class="hover:bg-gray-50">
                <td class="px-6 py-4 whitespace-nowrap">
                  <div class="flex items-center">
                    <div class="flex-shrink-0 h-10 w-10">
                      <div class="h-10 w-10 rounded-full bg-blue-600 flex items-center justify-center">
                        <span class="text-white font-medium text-sm">{{ tenant.name.charAt(0) }}</span>
                      </div>
                    </div>
                    <div class="ml-4">
                      <div class="text-sm font-medium text-gray-900">{{ tenant.name }}</div>
                      <div class="text-sm text-gray-500">{{ tenant.tenantId }}</div>
                    </div>
                  </div>
                </td>
                <td class="px-6 py-4 whitespace-nowrap">
                  <div v-if="tenant.application" class="flex items-center">
                    <div class="w-6 h-6 rounded flex items-center justify-center mr-2" :style="{ backgroundColor: tenant.application.primaryColor || '#3b82f6' }">
                      <i :class="tenant.application.icon || 'pi pi-cog'" class="text-white text-xs"></i>
                    </div>
                    <div>
                      <div class="text-sm font-medium text-gray-900">{{ tenant.application.name }}</div>
                      <div class="text-xs text-gray-500">{{ tenant.application.appKey }}</div>
                    </div>
                  </div>
                  <div v-else class="text-sm text-gray-500">No Application</div>
                </td>
                <td class="px-6 py-4 whitespace-nowrap">
                  <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium"
                        :class="getStatusBadge(tenant).class">
                    {{ getStatusBadge(tenant).text }}
                  </span>
                </td>
                <td class="px-6 py-4 whitespace-nowrap">
                  <div class="text-sm text-gray-900">{{ tenant.contactEmail || 'N/A' }}</div>
                  <div class="text-sm text-gray-500">{{ tenant.contactPhone || 'N/A' }}</div>
                </td>
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ formatDate(tenant.createdAt) }}
                </td>
                <td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
                  <div class="flex space-x-2">
                    <button
                      @click="openEditModal(tenant)"
                      class="text-blue-600 hover:text-blue-900"
                    >
                      <PencilIcon class="h-4 w-4" />
                    </button>
                    <button
                      @click="openDeleteModal(tenant)"
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

      <!-- Create/Edit Modal -->
      <div v-if="showCreateModal || showEditModal" class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full z-50">
        <div class="relative top-20 mx-auto p-5 border w-96 shadow-lg rounded-md bg-white">
          <div class="mt-3">
            <h3 class="text-lg font-medium text-gray-900 mb-4">
              {{ showCreateModal ? 'Create Tenant' : 'Edit Tenant' }}
            </h3>
            
            <form @submit.prevent="showCreateModal ? handleCreate() : handleUpdate()">
              <div class="space-y-4">
                <!-- Tenant ID -->
                <div>
                  <label class="block text-sm font-medium text-gray-700">Tenant ID</label>
                  <input
                    v-model="formData.tenantId"
                    type="text"
                    :disabled="!showCreateModal"
                    class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                    :class="{ 'bg-gray-100': !showCreateModal }"
                  />
                  <p v-if="errors.tenantId" class="mt-1 text-sm text-red-600">{{ errors.tenantId }}</p>
                </div>

                <!-- Name -->
                <div>
                  <label class="block text-sm font-medium text-gray-700">Name</label>
                  <input
                    v-model="formData.name"
                    type="text"
                    class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
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
                  ></textarea>
                </div>

                <!-- Contact Email -->
                <div>
                  <label class="block text-sm font-medium text-gray-700">Contact Email</label>
                  <input
                    v-model="formData.contactEmail"
                    type="email"
                    class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  />
                  <p v-if="errors.contactEmail" class="mt-1 text-sm text-red-600">{{ errors.contactEmail }}</p>
                </div>

                <!-- Max Users -->
                <div>
                  <label class="block text-sm font-medium text-gray-700">Max Users</label>
                  <input
                    v-model.number="formData.maxUsers"
                    type="number"
                    min="1"
                    class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  />
                </div>
              </div>

              <div class="mt-6 flex justify-end space-x-3">
                <button
                  type="button"
                  @click="showCreateModal = false; showEditModal = false"
                  class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  class="px-4 py-2 border border-transparent rounded-md text-sm font-medium text-white bg-blue-600 hover:bg-blue-700"
                >
                  {{ showCreateModal ? 'Create' : 'Update' }}
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>

      <!-- Delete Confirmation Modal -->
      <div v-if="showDeleteModal" class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full z-50">
        <div class="relative top-20 mx-auto p-5 border w-96 shadow-lg rounded-md bg-white">
          <div class="mt-3">
            <h3 class="text-lg font-medium text-gray-900 mb-4">Delete Tenant</h3>
            <p class="text-sm text-gray-500 mb-4">
              Are you sure you want to delete "{{ selectedTenant?.name }}"? This action cannot be undone.
            </p>
            <div class="flex justify-end space-x-3">
              <button
                @click="showDeleteModal = false"
                class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50"
              >
                Cancel
              </button>
              <button
                @click="handleDelete"
                class="px-4 py-2 border border-transparent rounded-md text-sm font-medium text-white bg-red-600 hover:bg-red-700"
              >
                Delete
              </button>
            </div>
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
.gap-4 { gap: 1rem; }

.sm\:grid-cols-3 { grid-template-columns: repeat(3, minmax(0, 1fr)); }

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

.mb-2 { margin-bottom: 0.5rem; }
.mb-4 { margin-bottom: 1rem; }
.mb-6 { margin-bottom: 1.5rem; }
.mb-8 { margin-bottom: 2rem; }
.ml-2 { margin-left: 0.5rem; }
.ml-4 { margin-left: 1rem; }
.mr-2 { margin-right: 0.5rem; }
.mt-1 { margin-top: 0.25rem; }
.mt-2 { margin-top: 0.5rem; }
.mt-3 { margin-top: 0.75rem; }
.mt-6 { margin-top: 1.5rem; }

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
.text-blue-800 { color: #1e40af; }
.text-yellow-800 { color: #92400e; }
.text-gray-800 { color: #1f2937; }

.bg-white { background-color: #ffffff; }
.bg-gray-50 { background-color: #f9fafb; }
.bg-blue-600 { background-color: #2563eb; }
.bg-green-100 { background-color: #dcfce7; }
.bg-red-100 { background-color: #fee2e2; }
.bg-blue-100 { background-color: #dbeafe; }
.bg-yellow-100 { background-color: #fef3c7; }
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
.space-y-4 > * + * { margin-top: 1rem; }

.h-4 { height: 1rem; }
.h-10 { height: 2.5rem; }
.w-4 { width: 1rem; }
.w-10 { width: 2.5rem; }
.w-96 { width: 24rem; }

.whitespace-nowrap { white-space: nowrap; }

.relative { position: relative; }
.absolute { position: absolute; }
.fixed { position: fixed; }
.inset-0 { top: 0; right: 0; bottom: 0; left: 0; }
.top-20 { top: 5rem; }
.left-3 { left: 0.75rem; }
.top-1\/2 { top: 50%; }
.z-50 { z-index: 50; }

.transform { transform: translate(var(--tw-translate-x), var(--tw-translate-y)) rotate(var(--tw-rotate)) skewX(var(--tw-skew-x)) skewY(var(--tw-skew-y)) scaleX(var(--tw-scale-x)) scaleY(var(--tw-scale-y)); }
.-translate-y-1\/2 { --tw-translate-y: -50%; }

.divide-y > * + * { border-top-width: 1px; border-top-color: #e5e7eb; }
.divide-gray-200 > * + * { border-top-color: #e5e7eb; }

.min-w-full { min-width: 100%; }

.hover\:bg-gray-50:hover { background-color: #f9fafb; }
.hover\:bg-blue-700:hover { background-color: #1d4ed8; }
.hover\:bg-gray-50:hover { background-color: #f9fafb; }
.hover\:bg-red-700:hover { background-color: #b91c1c; }
.hover\:text-blue-900:hover { color: #1e3a8a; }
.hover\:text-red-900:hover { color: #7f1d1d; }

.focus\:ring-blue-500:focus { box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1); }
.focus\:border-blue-500:focus { border-color: #3b82f6; }

.bg-opacity-50 { background-color: rgba(75, 85, 99, 0.5); }

.overflow-y-auto { overflow-y: auto; }

@media (min-width: 640px) {
  .sm\:grid-cols-3 { grid-template-columns: repeat(3, minmax(0, 1fr)); }
}
</style>
