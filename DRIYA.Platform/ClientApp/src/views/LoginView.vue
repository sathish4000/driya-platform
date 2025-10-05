<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <div>
        <div class="mx-auto h-12 w-12 bg-blue-600 rounded-lg flex items-center justify-center">
          <span class="text-white font-bold text-xl">D</span>
        </div>
        <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
          {{ isLogin ? 'Sign in to your account' : 'Create your account' }}
        </h2>
        <p class="mt-2 text-center text-sm text-gray-600">
          {{ isLogin ? "Don't have an account?" : "Already have an account?" }}
          <button
            @click="switchMode"
            class="font-medium text-blue-600 hover:text-blue-500"
          >
            {{ isLogin ? 'Sign up' : 'Sign in' }}
          </button>
        </p>
      </div>
      
      <form class="mt-8 space-y-6" @submit.prevent="handleSubmit">
        <div class="space-y-4">
          <!-- Email -->
          <div>
            <label for="email" class="block text-sm font-medium text-gray-700">
              Email address
            </label>
            <input
              id="email"
              v-model="formData.email"
              type="email"
              required
              class="mt-1 appearance-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500 focus:z-10 sm:text-sm"
              :class="{ 'border-red-300': errors.email }"
              placeholder="Enter your email"
            />
            <p v-if="errors.email" class="mt-1 text-sm text-red-600">{{ errors.email }}</p>
          </div>

          <!-- First Name (Registration only) -->
          <div v-if="!isLogin">
            <label for="firstName" class="block text-sm font-medium text-gray-700">
              First name
            </label>
            <input
              id="firstName"
              v-model="formData.firstName"
              type="text"
              required
              class="mt-1 appearance-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500 focus:z-10 sm:text-sm"
              :class="{ 'border-red-300': errors.firstName }"
              placeholder="Enter your first name"
            />
            <p v-if="errors.firstName" class="mt-1 text-sm text-red-600">{{ errors.firstName }}</p>
          </div>

          <!-- Last Name (Registration only) -->
          <div v-if="!isLogin">
            <label for="lastName" class="block text-sm font-medium text-gray-700">
              Last name
            </label>
            <input
              id="lastName"
              v-model="formData.lastName"
              type="text"
              required
              class="mt-1 appearance-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500 focus:z-10 sm:text-sm"
              :class="{ 'border-red-300': errors.lastName }"
              placeholder="Enter your last name"
            />
            <p v-if="errors.lastName" class="mt-1 text-sm text-red-600">{{ errors.lastName }}</p>
          </div>

          <!-- Password -->
          <div>
            <label for="password" class="block text-sm font-medium text-gray-700">
              Password
            </label>
            <div class="mt-1 relative">
              <input
                id="password"
                v-model="formData.password"
                :type="showPassword ? 'text' : 'password'"
                required
                class="appearance-none relative block w-full px-3 py-2 pr-10 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500 focus:z-10 sm:text-sm"
                :class="{ 'border-red-300': errors.password }"
                placeholder="Enter your password"
              />
              <button
                type="button"
                @click="togglePasswordVisibility"
                class="absolute inset-y-0 right-0 pr-3 flex items-center"
              >
                <EyeIcon v-if="!showPassword" class="h-5 w-5 text-gray-400" />
                <EyeSlashIcon v-else class="h-5 w-5 text-gray-400" />
              </button>
            </div>
            <p v-if="errors.password" class="mt-1 text-sm text-red-600">{{ errors.password }}</p>
          </div>

          <!-- Confirm Password (Registration only) -->
          <div v-if="!isLogin">
            <label for="confirmPassword" class="block text-sm font-medium text-gray-700">
              Confirm password
            </label>
            <input
              id="confirmPassword"
              v-model="formData.confirmPassword"
              type="password"
              required
              class="mt-1 appearance-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500 focus:z-10 sm:text-sm"
              :class="{ 'border-red-300': errors.confirmPassword }"
              placeholder="Confirm your password"
            />
            <p v-if="errors.confirmPassword" class="mt-1 text-sm text-red-600">{{ errors.confirmPassword }}</p>
          </div>
        </div>

        <div>
          <button
            type="submit"
            :disabled="isLoading"
            class="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            <span v-if="isLoading" class="absolute left-0 inset-y-0 flex items-center pl-3">
              <svg class="animate-spin h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
              </svg>
            </span>
            {{ isLoading ? 'Processing...' : (isLogin ? 'Sign in' : 'Sign up') }}
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../stores/auth'
import { EyeIcon, EyeSlashIcon } from '@heroicons/vue/24/outline'

const router = useRouter()
const authStore = useAuthStore()

const isLogin = ref(true)
const showPassword = ref(false)
const isLoading = ref(false)

// Form data
const formData = ref({
  email: '',
  password: '',
  firstName: '',
  lastName: '',
  confirmPassword: ''
})

const errors = ref<Record<string, string>>({})

const validateForm = () => {
  errors.value = {}
  
  if (!formData.value.email) {
    errors.value.email = 'Email is required'
  } else if (!/\S+@\S+\.\S+/.test(formData.value.email)) {
    errors.value.email = 'Email is invalid'
  }
  
  if (!formData.value.password) {
    errors.value.password = 'Password is required'
  } else if (formData.value.password.length < 6) {
    errors.value.password = 'Password must be at least 6 characters'
  }
  
  if (!isLogin.value) {
    if (!formData.value.firstName) {
      errors.value.firstName = 'First name is required'
    }
    
    if (!formData.value.lastName) {
      errors.value.lastName = 'Last name is required'
    }
    
    if (formData.value.password !== formData.value.confirmPassword) {
      errors.value.confirmPassword = 'Passwords do not match'
    }
  }
  
  return Object.keys(errors.value).length === 0
}

const handleSubmit = async () => {
  if (!validateForm()) return
  
  isLoading.value = true
  
  try {
    let success = false
    
    if (isLogin.value) {
      success = await authStore.login({
        email: formData.value.email,
        password: formData.value.password
      })
    } else {
      success = await authStore.register({
        email: formData.value.email,
        password: formData.value.password,
        firstName: formData.value.firstName,
        lastName: formData.value.lastName
      })
    }
    
    if (success) {
      router.push('/dashboard')
    }
  } catch (error) {
    console.error('Authentication error:', error)
  } finally {
    isLoading.value = false
  }
}

const togglePasswordVisibility = () => {
  showPassword.value = !showPassword.value
}

const switchMode = () => {
  isLogin.value = !isLogin.value
  errors.value = {}
  formData.value = {
    email: formData.value.email, // Keep email
    password: '',
    firstName: '',
    lastName: '',
    confirmPassword: ''
  }
}

onMounted(() => {
  // Clear any existing errors
  authStore.clearError()
})
</script>

<style scoped>
/* Additional styles for form elements */
input:focus {
  outline: none;
  border-color: #3b82f6;
  box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
}

.border-red-300 {
  border-color: #fca5a5;
}

.text-red-600 {
  color: #dc2626;
}

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
</style>

