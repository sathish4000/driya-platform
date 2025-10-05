import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import axios from 'axios'

export interface Tenant {
  id: string
  tenantId: string
  name: string
  description?: string
  domain?: string
  isActive: boolean
  subscriptionStatus: string
  currentPlanId?: string
  trialEndsAt?: string
  createdAt: string
  updatedAt: string
  primaryColor?: string
  logoUrl?: string
  contactEmail?: string
  contactPhone?: string
  address?: string
  maxUsers?: number
  maxStorageGB?: number
  features: string[]
  // Application information
  applicationId?: string
  application?: {
    id: string
    name: string
    appKey: string
    icon?: string
    primaryColor?: string
  }
}

export interface CreateTenantRequest {
  tenantId: string
  name: string
  description?: string
  domain?: string
  contactEmail?: string
  contactPhone?: string
  address?: string
  primaryColor?: string
  logoUrl?: string
  maxUsers?: number
  maxStorageGB?: number
  planId?: string
}

export interface UpdateTenantRequest {
  name?: string
  description?: string
  domain?: string
  isActive?: boolean
  contactEmail?: string
  contactPhone?: string
  address?: string
  primaryColor?: string
  logoUrl?: string
  maxUsers?: number
  maxStorageGB?: number
}

export const useTenantStore = defineStore('tenant', () => {
  const tenants = ref<Tenant[]>([])
  const currentTenant = ref<Tenant | null>(null)
  const isLoading = ref(false)
  const error = ref<string | null>(null)

  // Computed properties
  const activeTenants = computed(() => tenants.value.filter(t => t.isActive))
  const inactiveTenants = computed(() => tenants.value.filter(t => !t.isActive))

  // Actions
  const fetchTenants = async () => {
    isLoading.value = true
    error.value = null
    
    try {
      const response = await axios.get('/api/tenant')
      tenants.value = response.data
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Failed to fetch tenants'
    } finally {
      isLoading.value = false
    }
  }

  const fetchCurrentTenant = async () => {
    try {
      const response = await axios.get('/api/tenant/current')
      currentTenant.value = response.data
    } catch (err: any) {
      console.error('Failed to fetch current tenant:', err)
    }
  }

  const createTenant = async (tenantData: CreateTenantRequest) => {
    isLoading.value = true
    error.value = null
    
    try {
      const response = await axios.post('/api/tenant', tenantData)
      const newTenant = response.data
      tenants.value.push(newTenant)
      return newTenant
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Failed to create tenant'
      throw err
    } finally {
      isLoading.value = false
    }
  }

  const updateTenant = async (id: string, tenantData: UpdateTenantRequest) => {
    isLoading.value = true
    error.value = null
    
    try {
      const response = await axios.put(`/api/tenant/${id}`, tenantData)
      const updatedTenant = response.data
      
      const index = tenants.value.findIndex(t => t.id === id)
      if (index !== -1) {
        tenants.value[index] = updatedTenant
      }
      
      if (currentTenant.value?.id === id) {
        currentTenant.value = updatedTenant
      }
      
      return updatedTenant
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Failed to update tenant'
      throw err
    } finally {
      isLoading.value = false
    }
  }

  const deleteTenant = async (id: string) => {
    isLoading.value = true
    error.value = null
    
    try {
      await axios.delete(`/api/tenant/${id}`)
      tenants.value = tenants.value.filter(t => t.id !== id)
      
      if (currentTenant.value?.id === id) {
        currentTenant.value = null
      }
      
      return true
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Failed to delete tenant'
      throw err
    } finally {
      isLoading.value = false
    }
  }

  const getTenantById = async (id: string) => {
    try {
      const response = await axios.get(`/api/tenant/${id}`)
      return response.data
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Failed to fetch tenant'
      throw err
    }
  }

  const getTenantStatistics = async (id: string) => {
    try {
      const response = await axios.get(`/api/tenant/${id}/statistics`)
      return response.data
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Failed to fetch tenant statistics'
      throw err
    }
  }

  const clearError = () => {
    error.value = null
  }

  return {
    // State
    tenants,
    currentTenant,
    isLoading,
    error,
    
    // Computed
    activeTenants,
    inactiveTenants,
    
    // Actions
    fetchTenants,
    fetchCurrentTenant,
    createTenant,
    updateTenant,
    deleteTenant,
    getTenantById,
    getTenantStatistics,
    clearError
  }
})
