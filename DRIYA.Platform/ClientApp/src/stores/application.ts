import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import axios from 'axios'

export interface Application {
  id: string
  name: string
  description?: string
  appKey: string
  domain?: string
  subdomain?: string
  isActive: boolean
  status: string
  icon?: string
  primaryColor?: string
  createdAt: string
  updatedAt: string
  tenants?: any[]
  features?: any[]
}

export const useApplicationStore = defineStore('application', () => {
  // State
  const applications = ref<Application[]>([])
  const currentApplication = ref<Application | null>(null)
  const loading = ref(false)
  const error = ref<string | null>(null)

  // Getters
  const isApplicationSelected = computed(() => currentApplication.value !== null)
  const currentApplicationId = computed(() => currentApplication.value?.id || null)
  const currentApplicationName = computed(() => currentApplication.value?.name || 'Select Application')

  // Actions
  const fetchApplications = async () => {
    try {
      loading.value = true
      error.value = null
      
      const response = await axios.get('/api/application')
      applications.value = response.data
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Failed to fetch applications'
      console.error('Error fetching applications:', err)
    } finally {
      loading.value = false
    }
  }

  const selectApplication = (application: Application) => {
    currentApplication.value = application
    localStorage.setItem('selectedApplication', JSON.stringify(application))
  }

  const clearSelectedApplication = () => {
    currentApplication.value = null
    localStorage.removeItem('selectedApplication')
  }

  const createApplication = async (applicationData: Partial<Application>) => {
    try {
      loading.value = true
      error.value = null
      
      const response = await axios.post('/api/application', applicationData)
      const newApplication = response.data
      
      applications.value.push(newApplication)
      return newApplication
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Failed to create application'
      console.error('Error creating application:', err)
      throw err
    } finally {
      loading.value = false
    }
  }

  const updateApplication = async (id: string, applicationData: Partial<Application>) => {
    try {
      loading.value = true
      error.value = null
      
      const response = await axios.put(`/api/application/${id}`, applicationData)
      const updatedApplication = response.data
      
      const index = applications.value.findIndex(app => app.id === id)
      if (index !== -1) {
        applications.value[index] = updatedApplication
      }
      
      // Update current application if it's the one being updated
      if (currentApplication.value?.id === id) {
        currentApplication.value = updatedApplication
        localStorage.setItem('selectedApplication', JSON.stringify(updatedApplication))
      }
      
      return updatedApplication
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Failed to update application'
      console.error('Error updating application:', err)
      throw err
    } finally {
      loading.value = false
    }
  }

  const deleteApplication = async (id: string) => {
    try {
      loading.value = true
      error.value = null
      
      await axios.delete(`/api/application/${id}`)
      
      applications.value = applications.value.filter(app => app.id !== id)
      
      // Clear current application if it's the one being deleted
      if (currentApplication.value?.id === id) {
        clearSelectedApplication()
      }
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Failed to delete application'
      console.error('Error deleting application:', err)
      throw err
    } finally {
      loading.value = false
    }
  }

  const checkAppKeyUnique = async (appKey: string, excludeId?: string) => {
    try {
      const response = await axios.get(`/api/application/check-app-key/${appKey}`, {
        params: { excludeId }
      })
      return response.data.isUnique
    } catch (err: any) {
      console.error('Error checking app key uniqueness:', err)
      return false
    }
  }

  const checkSubdomainUnique = async (subdomain: string, excludeId?: string) => {
    try {
      const response = await axios.get(`/api/application/check-subdomain/${subdomain}`, {
        params: { excludeId }
      })
      return response.data.isUnique
    } catch (err: any) {
      console.error('Error checking subdomain uniqueness:', err)
      return false
    }
  }

  const loadSelectedApplication = () => {
    const saved = localStorage.getItem('selectedApplication')
    if (saved) {
      try {
        currentApplication.value = JSON.parse(saved)
      } catch (err) {
        console.error('Error parsing saved application:', err)
        localStorage.removeItem('selectedApplication')
      }
    }
  }

  const clearError = () => {
    error.value = null
  }

  return {
    // State
    applications,
    currentApplication,
    loading,
    error,
    
    // Getters
    isApplicationSelected,
    currentApplicationId,
    currentApplicationName,
    
    // Actions
    fetchApplications,
    selectApplication,
    clearSelectedApplication,
    createApplication,
    updateApplication,
    deleteApplication,
    checkAppKeyUnique,
    checkSubdomainUnique,
    loadSelectedApplication,
    clearError
  }
})
