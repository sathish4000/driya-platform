<script setup lang="ts">
import { ref } from 'vue'
import { 
  Cog6ToothIcon,
  UserIcon,
  BellIcon,
  ShieldCheckIcon,
  GlobeAltIcon
} from '@heroicons/vue/24/outline'

const activeTab = ref('profile')

const tabs = [
  { id: 'profile', name: 'Profile', icon: UserIcon },
  { id: 'notifications', name: 'Notifications', icon: BellIcon },
  { id: 'security', name: 'Security', icon: ShieldCheckIcon },
  { id: 'preferences', name: 'Preferences', icon: Cog6ToothIcon }
]
</script>

<template>
  <div class="min-h-screen bg-gray-50 py-6">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <!-- Header -->
      <div class="mb-8">
        <h1 class="text-3xl font-bold text-gray-900">Settings</h1>
        <p class="mt-2 text-gray-600">Manage your account settings and preferences</p>
      </div>

      <div class="bg-white shadow rounded-lg">
        <!-- Tabs -->
        <div class="border-b border-gray-200">
          <nav class="-mb-px flex space-x-8 px-6">
            <button
              v-for="tab in tabs"
              :key="tab.id"
              @click="activeTab = tab.id"
              class="py-4 px-1 border-b-2 font-medium text-sm"
              :class="[
                activeTab === tab.id
                  ? 'border-blue-500 text-blue-600'
                  : 'border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300'
              ]"
            >
              <component :is="tab.icon" class="h-5 w-5 inline mr-2" />
              {{ tab.name }}
            </button>
          </nav>
        </div>

        <!-- Tab Content -->
        <div class="px-6 py-6">
          <!-- Profile Tab -->
          <div v-if="activeTab === 'profile'">
            <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Profile Information</h3>
            <div class="grid grid-cols-1 gap-6 sm:grid-cols-2">
              <div>
                <label class="block text-sm font-medium text-gray-700">First Name</label>
                <input
                  type="text"
                  class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  placeholder="John"
                />
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700">Last Name</label>
                <input
                  type="text"
                  class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  placeholder="Doe"
                />
              </div>
              <div class="sm:col-span-2">
                <label class="block text-sm font-medium text-gray-700">Email</label>
                <input
                  type="email"
                  class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  placeholder="john.doe@example.com"
                />
              </div>
            </div>
          </div>

          <!-- Notifications Tab -->
          <div v-if="activeTab === 'notifications'">
            <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Notification Preferences</h3>
            <div class="space-y-4">
              <div class="flex items-center justify-between">
                <div>
                  <p class="text-sm font-medium text-gray-900">Email Notifications</p>
                  <p class="text-sm text-gray-500">Receive email notifications for important updates</p>
                </div>
                <button class="relative inline-flex h-6 w-11 flex-shrink-0 cursor-pointer rounded-full border-2 border-transparent transition-colors duration-200 ease-in-out focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 bg-blue-600">
                  <span class="translate-x-5 pointer-events-none inline-block h-5 w-5 transform rounded-full bg-white shadow ring-0 transition duration-200 ease-in-out"></span>
                </button>
              </div>
              <div class="flex items-center justify-between">
                <div>
                  <p class="text-sm font-medium text-gray-900">SMS Notifications</p>
                  <p class="text-sm text-gray-500">Receive SMS notifications for urgent alerts</p>
                </div>
                <button class="relative inline-flex h-6 w-11 flex-shrink-0 cursor-pointer rounded-full border-2 border-transparent transition-colors duration-200 ease-in-out focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 bg-gray-200">
                  <span class="translate-x-0 pointer-events-none inline-block h-5 w-5 transform rounded-full bg-white shadow ring-0 transition duration-200 ease-in-out"></span>
                </button>
              </div>
            </div>
          </div>

          <!-- Security Tab -->
          <div v-if="activeTab === 'security'">
            <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Security Settings</h3>
            <div class="space-y-6">
              <div>
                <label class="block text-sm font-medium text-gray-700">Current Password</label>
                <input
                  type="password"
                  class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  placeholder="Enter current password"
                />
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700">New Password</label>
                <input
                  type="password"
                  class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  placeholder="Enter new password"
                />
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700">Confirm New Password</label>
                <input
                  type="password"
                  class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                  placeholder="Confirm new password"
                />
              </div>
              <div class="flex items-center justify-between">
                <div>
                  <p class="text-sm font-medium text-gray-900">Two-Factor Authentication</p>
                  <p class="text-sm text-gray-500">Add an extra layer of security to your account</p>
                </div>
                <button class="inline-flex items-center px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50">
                  Enable
                </button>
              </div>
            </div>
          </div>

          <!-- Preferences Tab -->
          <div v-if="activeTab === 'preferences'">
            <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Preferences</h3>
            <div class="space-y-6">
              <div>
                <label class="block text-sm font-medium text-gray-700">Language</label>
                <select class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm">
                  <option>English</option>
                  <option>Spanish</option>
                  <option>French</option>
                </select>
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700">Time Zone</label>
                <select class="mt-1 block w-full border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500 sm:text-sm">
                  <option>UTC-8 (Pacific Time)</option>
                  <option>UTC-5 (Eastern Time)</option>
                  <option>UTC+0 (GMT)</option>
                </select>
              </div>
              <div class="flex items-center justify-between">
                <div>
                  <p class="text-sm font-medium text-gray-900">Dark Mode</p>
                  <p class="text-sm text-gray-500">Use dark theme for the interface</p>
                </div>
                <button class="relative inline-flex h-6 w-11 flex-shrink-0 cursor-pointer rounded-full border-2 border-transparent transition-colors duration-200 ease-in-out focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 bg-gray-200">
                  <span class="translate-x-0 pointer-events-none inline-block h-5 w-5 transform rounded-full bg-white shadow ring-0 transition duration-200 ease-in-out"></span>
                </button>
              </div>
            </div>
          </div>

          <!-- Save Button -->
          <div class="mt-8 flex justify-end">
            <button class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700">
              Save Changes
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
.gap-6 { gap: 1.5rem; }

.sm\:grid-cols-2 { grid-template-columns: repeat(2, minmax(0, 1fr)); }
.sm\:col-span-2 { grid-column: span 2 / span 2; }

.overflow-hidden { overflow: hidden; }
.shadow { box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06); }
.rounded-lg { border-radius: 0.5rem; }
.rounded-md { border-radius: 0.375rem; }
.rounded-full { border-radius: 9999px; }

.px-4 { padding-left: 1rem; padding-right: 1rem; }
.px-6 { padding-left: 1.5rem; padding-right: 1.5rem; }
.py-2 { padding-top: 0.5rem; padding-bottom: 0.5rem; }
.py-4 { padding-top: 1rem; padding-bottom: 1rem; }
.py-6 { padding-top: 1.5rem; padding-bottom: 1.5rem; }

.mb-4 { margin-bottom: 1rem; }
.mb-8 { margin-bottom: 2rem; }
.mt-8 { margin-top: 2rem; }
.mt-1 { margin-top: 0.25rem; }
.mr-2 { margin-right: 0.5rem; }
.mt-2 { margin-top: 0.5rem; }

.text-3xl { font-size: 1.875rem; line-height: 2.25rem; }
.text-lg { font-size: 1.125rem; line-height: 1.75rem; }
.text-sm { font-size: 0.875rem; line-height: 1.25rem; }

.font-bold { font-weight: 700; }
.font-medium { font-weight: 500; }

.text-gray-900 { color: #111827; }
.text-gray-600 { color: #4b5563; }
.text-gray-500 { color: #6b7280; }
.text-gray-700 { color: #374151; }
.text-blue-600 { color: #2563eb; }
.text-blue-500 { color: #3b82f6; }

.bg-white { background-color: #ffffff; }
.bg-gray-50 { background-color: #f9fafb; }
.bg-blue-600 { background-color: #2563eb; }
.bg-gray-200 { background-color: #e5e7eb; }

.border-gray-300 { border-color: #d1d5db; }
.border-gray-200 { border-color: #e5e7eb; }
.border-blue-500 { border-color: #3b82f6; }
.border-transparent { border-color: transparent; }

.border-b { border-bottom-width: 1px; }
.border-b-2 { border-bottom-width: 2px; }
.border { border-width: 1px; }
.border-2 { border-width: 2px; }

.flex { display: flex; }
.inline-flex { display: inline-flex; }
.items-center { align-items: center; }
.justify-between { justify-content: space-between; }
.justify-end { justify-content: flex-end; }
.space-x-8 > * + * { margin-left: 2rem; }
.space-y-4 > * + * { margin-top: 1rem; }
.space-y-6 > * + * { margin-top: 1.5rem; }

.h-5 { height: 1.25rem; }
.h-6 { height: 1.5rem; }
.w-5 { width: 1.25rem; }
.w-6 { width: 1.5rem; }
.w-11 { width: 2.75rem; }

.whitespace-nowrap { white-space: nowrap; }

.relative { position: relative; }
.absolute { position: absolute; }

.translate-x-0 { transform: translateX(0); }
.translate-x-5 { transform: translateX(1.25rem); }

.pointer-events-none { pointer-events: none; }
.cursor-pointer { cursor: pointer; }

.transition-colors { transition-property: color, background-color, border-color, text-decoration-color, fill, stroke; }
.transition-duration-200 { transition-duration: 200ms; }
.ease-in-out { transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1); }

.hover\:bg-gray-50:hover { background-color: #f9fafb; }
.hover\:bg-blue-700:hover { background-color: #1d4ed8; }
.hover\:text-gray-700:hover { color: #374151; }
.hover\:border-gray-300:hover { border-color: #d1d5db; }

.focus\:outline-none:focus { outline: 2px solid transparent; outline-offset: 2px; }
.focus\:ring-2:focus { box-shadow: 0 0 0 2px rgba(59, 130, 246, 0.5); }
.focus\:ring-blue-500:focus { box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1); }
.focus\:border-blue-500:focus { border-color: #3b82f6; }
.focus\:ring-offset-2:focus { box-shadow: 0 0 0 2px rgba(59, 130, 246, 0.5), 0 0 0 4px rgba(255, 255, 255, 0.5); }

.inline { display: inline; }

@media (min-width: 640px) {
  .sm\:grid-cols-2 { grid-template-columns: repeat(2, minmax(0, 1fr)); }
  .sm\:col-span-2 { grid-column: span 2 / span 2; }
}
</style>
