import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import axios from 'axios'

export interface User {
  id: string
  email: string
  firstName: string
  lastName: string
  role: string
  tenantId?: string
  isActive: boolean
  lastLoginAt?: string
}

export interface LoginRequest {
  email: string
  password: string
}

export interface RegisterRequest {
  email: string
  password: string
  firstName: string
  lastName: string
  tenantId?: string
}

export const useAuthStore = defineStore('auth', () => {
  const user = ref<User | null>(null)
  const token = ref<string | null>(localStorage.getItem('authToken'))
  const isLoading = ref(false)
  const error = ref<string | null>(null)

  // Computed properties
  const isAuthenticated = computed(() => !!token.value && !!user.value)
  const isAdmin = computed(() => user.value?.role === 'GlobalAdmin')
  const isTenantAdmin = computed(() => user.value?.role === 'TenantAdmin')
  const fullName = computed(() => {
    if (!user.value) return ''
    return `${user.value.firstName} ${user.value.lastName}`
  })

  // Actions
  const login = async (credentials: LoginRequest) => {
    isLoading.value = true
    error.value = null
    
    try {
      const response = await axios.post('/api/auth/login', credentials)
      const { token: authToken, user: userData } = response.data
      
      token.value = authToken
      user.value = userData
      
      localStorage.setItem('authToken', authToken)
      localStorage.setItem('userRole', userData.role)
      
      // Set default authorization header
      axios.defaults.headers.common['Authorization'] = `Bearer ${authToken}`
      
      return true
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Login failed'
      return false
    } finally {
      isLoading.value = false
    }
  }

  const register = async (userData: RegisterRequest) => {
    isLoading.value = true
    error.value = null
    
    try {
      const response = await axios.post('/api/auth/register', userData)
      const { token: authToken, user: newUser } = response.data
      
      token.value = authToken
      user.value = newUser
      
      localStorage.setItem('authToken', authToken)
      localStorage.setItem('userRole', newUser.role)
      
      axios.defaults.headers.common['Authorization'] = `Bearer ${authToken}`
      
      return true
    } catch (err: any) {
      error.value = err.response?.data?.message || 'Registration failed'
      return false
    } finally {
      isLoading.value = false
    }
  }

  const logout = () => {
    user.value = null
    token.value = null
    error.value = null
    
    localStorage.removeItem('authToken')
    localStorage.removeItem('userRole')
    
    delete axios.defaults.headers.common['Authorization']
  }

  const loadUser = async () => {
    if (!token.value) return false
    
    try {
      const response = await axios.get('/api/auth/me')
      user.value = response.data
      return true
    } catch (err) {
      logout()
      return false
    }
  }

  const clearError = () => {
    error.value = null
  }

  // Initialize auth state
  if (token.value) {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token.value}`
    loadUser()
  }

  return {
    // State
    user,
    token,
    isLoading,
    error,
    
    // Computed
    isAuthenticated,
    isAdmin,
    isTenantAdmin,
    fullName,
    
    // Actions
    login,
    register,
    logout,
    loadUser,
    clearError
  }
})
