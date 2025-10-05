<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { 
  CreditCardIcon,
  DocumentTextIcon,
  CurrencyDollarIcon,
  CalendarIcon,
  CheckCircleIcon,
  ExclamationTriangleIcon,
  ClockIcon,
  PlusIcon
} from '@heroicons/vue/24/outline'

const isLoading = ref(false)
const invoices = ref([])
const currentPlan = ref({
  name: 'Professional Plan',
  price: 99,
  billingCycle: 'monthly',
  nextBillingDate: '2024-02-15',
  status: 'active'
})

const billingStats = ref({
  totalInvoices: 0,
  paidInvoices: 0,
  pendingInvoices: 0,
  totalAmount: 0
})

const fetchBillingData = async () => {
  isLoading.value = true
  try {
    // Mock data
    invoices.value = [
      {
        id: '1',
        invoiceNumber: 'INV-2024-001',
        amount: 99.00,
        status: 'paid',
        dueDate: '2024-01-15',
        paidDate: '2024-01-14',
        description: 'Professional Plan - January 2024'
      },
      {
        id: '2',
        invoiceNumber: 'INV-2024-002',
        amount: 99.00,
        status: 'pending',
        dueDate: '2024-02-15',
        paidDate: null,
        description: 'Professional Plan - February 2024'
      },
      {
        id: '3',
        invoiceNumber: 'INV-2023-012',
        amount: 99.00,
        status: 'paid',
        dueDate: '2023-12-15',
        paidDate: '2023-12-14',
        description: 'Professional Plan - December 2023'
      }
    ]
    
    billingStats.value = {
      totalInvoices: invoices.value.length,
      paidInvoices: invoices.value.filter(i => i.status === 'paid').length,
      pendingInvoices: invoices.value.filter(i => i.status === 'pending').length,
      totalAmount: invoices.value.reduce((sum, i) => sum + i.amount, 0)
    }
  } catch (error) {
    console.error('Failed to fetch billing data:', error)
  } finally {
    isLoading.value = false
  }
}

const getStatusBadge = (status) => {
  switch (status) {
    case 'paid':
      return { text: 'Paid', class: 'bg-green-100 text-green-800', icon: CheckCircleIcon }
    case 'pending':
      return { text: 'Pending', class: 'bg-yellow-100 text-yellow-800', icon: ClockIcon }
    case 'overdue':
      return { text: 'Overdue', class: 'bg-red-100 text-red-800', icon: ExclamationTriangleIcon }
    default:
      return { text: 'Unknown', class: 'bg-gray-100 text-gray-800', icon: ClockIcon }
  }
}

const formatCurrency = (amount) => {
  return new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD'
  }).format(amount)
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
  fetchBillingData()
})
</script>

<template>
  <div class="min-h-screen bg-gray-50 py-6">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <!-- Header -->
      <div class="mb-8">
        <h1 class="text-3xl font-bold text-gray-900">Billing & Invoices</h1>
        <p class="mt-2 text-gray-600">Manage your subscription and view billing history</p>
      </div>

      <!-- Current Plan -->
      <div class="bg-white shadow rounded-lg mb-6">
        <div class="px-4 py-5 sm:p-6">
          <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Current Plan</h3>
          <div class="grid grid-cols-1 md:grid-cols-3 gap-4">
            <div class="flex items-center">
              <CreditCardIcon class="h-8 w-8 text-blue-600 mr-3" />
              <div>
                <p class="text-sm font-medium text-gray-900">{{ currentPlan.name }}</p>
                <p class="text-sm text-gray-500">{{ formatCurrency(currentPlan.price) }}/{{ currentPlan.billingCycle }}</p>
              </div>
            </div>
            <div class="flex items-center">
              <CalendarIcon class="h-8 w-8 text-green-600 mr-3" />
              <div>
                <p class="text-sm font-medium text-gray-900">Next Billing</p>
                <p class="text-sm text-gray-500">{{ formatDate(currentPlan.nextBillingDate) }}</p>
              </div>
            </div>
            <div class="flex items-center">
              <CheckCircleIcon class="h-8 w-8 text-green-600 mr-3" />
              <div>
                <p class="text-sm font-medium text-gray-900">Status</p>
                <p class="text-sm text-gray-500 capitalize">{{ currentPlan.status }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Billing Stats -->
      <div class="grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-4 mb-8">
        <div class="bg-white overflow-hidden shadow rounded-lg">
          <div class="p-5">
            <div class="flex items-center">
              <div class="flex-shrink-0">
                <DocumentTextIcon class="h-6 w-6 text-gray-400" />
              </div>
              <div class="ml-5 w-0 flex-1">
                <dl>
                  <dt class="text-sm font-medium text-gray-500 truncate">Total Invoices</dt>
                  <dd class="text-lg font-medium text-gray-900">{{ billingStats.totalInvoices }}</dd>
                </dl>
              </div>
            </div>
          </div>
        </div>

        <div class="bg-white overflow-hidden shadow rounded-lg">
          <div class="p-5">
            <div class="flex items-center">
              <div class="flex-shrink-0">
                <CheckCircleIcon class="h-6 w-6 text-green-400" />
              </div>
              <div class="ml-5 w-0 flex-1">
                <dl>
                  <dt class="text-sm font-medium text-gray-500 truncate">Paid Invoices</dt>
                  <dd class="text-lg font-medium text-gray-900">{{ billingStats.paidInvoices }}</dd>
                </dl>
              </div>
            </div>
          </div>
        </div>

        <div class="bg-white overflow-hidden shadow rounded-lg">
          <div class="p-5">
            <div class="flex items-center">
              <div class="flex-shrink-0">
                <ClockIcon class="h-6 w-6 text-yellow-400" />
              </div>
              <div class="ml-5 w-0 flex-1">
                <dl>
                  <dt class="text-sm font-medium text-gray-500 truncate">Pending Invoices</dt>
                  <dd class="text-lg font-medium text-gray-900">{{ billingStats.pendingInvoices }}</dd>
                </dl>
              </div>
            </div>
          </div>
        </div>

        <div class="bg-white overflow-hidden shadow rounded-lg">
          <div class="p-5">
            <div class="flex items-center">
              <div class="flex-shrink-0">
                <CurrencyDollarIcon class="h-6 w-6 text-green-400" />
              </div>
              <div class="ml-5 w-0 flex-1">
                <dl>
                  <dt class="text-sm font-medium text-gray-500 truncate">Total Amount</dt>
                  <dd class="text-lg font-medium text-gray-900">{{ formatCurrency(billingStats.totalAmount) }}</dd>
                </dl>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Invoices Table -->
      <div class="bg-white shadow rounded-lg">
        <div class="px-4 py-5 sm:p-6">
          <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Invoice History</h3>
          <div class="overflow-x-auto">
            <table class="min-w-full divide-y divide-gray-200">
              <thead class="bg-gray-50">
                <tr>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Invoice
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Description
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Amount
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Status
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Due Date
                  </th>
                  <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Actions
                  </th>
                </tr>
              </thead>
              <tbody class="bg-white divide-y divide-gray-200">
                <tr v-for="invoice in invoices" :key="invoice.id" class="hover:bg-gray-50">
                  <td class="px-6 py-4 whitespace-nowrap">
                    <div class="text-sm font-medium text-gray-900">{{ invoice.invoiceNumber }}</div>
                  </td>
                  <td class="px-6 py-4">
                    <div class="text-sm text-gray-900">{{ invoice.description }}</div>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap">
                    <div class="text-sm font-medium text-gray-900">{{ formatCurrency(invoice.amount) }}</div>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap">
                    <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium"
                          :class="getStatusBadge(invoice.status).class">
                      <component :is="getStatusBadge(invoice.status).icon" class="h-3 w-3 mr-1" />
                      {{ getStatusBadge(invoice.status).text }}
                    </span>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                    {{ formatDate(invoice.dueDate) }}
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
                    <div class="flex space-x-2">
                      <button
                        class="text-blue-600 hover:text-blue-900"
                      >
                        View
                      </button>
                      <button
                        v-if="invoice.status === 'pending'"
                        class="text-green-600 hover:text-green-900"
                      >
                        Pay Now
                      </button>
                    </div>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>

      <!-- Payment Methods -->
      <div class="mt-8 bg-white shadow rounded-lg">
        <div class="px-4 py-5 sm:p-6">
          <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Payment Methods</h3>
          <div class="space-y-4">
            <div class="flex items-center justify-between p-4 border border-gray-200 rounded-md">
              <div class="flex items-center">
                <CreditCardIcon class="h-8 w-8 text-gray-400 mr-3" />
                <div>
                  <p class="text-sm font-medium text-gray-900">•••• •••• •••• 4242</p>
                  <p class="text-sm text-gray-500">Expires 12/25</p>
                </div>
              </div>
              <div class="flex items-center space-x-2">
                <span class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-green-100 text-green-800">
                  Default
                </span>
                <button class="text-gray-400 hover:text-gray-600">
                  Edit
                </button>
              </div>
            </div>
            <button class="inline-flex items-center px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50">
              <PlusIcon class="h-4 w-4 mr-2" />
              Add Payment Method
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
.gap-4 { gap: 1rem; }
.gap-5 { gap: 1.25rem; }

.sm\:grid-cols-2 { grid-template-columns: repeat(2, minmax(0, 1fr)); }
.lg\:grid-cols-4 { grid-template-columns: repeat(4, minmax(0, 1fr)); }
.md\:grid-cols-3 { grid-template-columns: repeat(3, minmax(0, 1fr)); }

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
.p-4 { padding: 1rem; }
.p-5 { padding: 1.25rem; }

.mb-4 { margin-bottom: 1rem; }
.mb-6 { margin-bottom: 1.5rem; }
.mb-8 { margin-bottom: 2rem; }
.mt-8 { margin-top: 2rem; }
.ml-3 { margin-left: 0.75rem; }
.ml-5 { margin-left: 1.25rem; }
.mr-2 { margin-right: 0.5rem; }
.mr-3 { margin-right: 0.75rem; }
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
.text-green-600 { color: #059669; }
.text-green-400 { color: #4ade80; }
.text-yellow-400 { color: #facc15; }
.text-green-800 { color: #166534; }
.text-yellow-800 { color: #92400e; }
.text-red-800 { color: #991b1b; }
.text-gray-800 { color: #1f2937; }

.bg-white { background-color: #ffffff; }
.bg-gray-50 { background-color: #f9fafb; }
.bg-green-100 { background-color: #dcfce7; }
.bg-yellow-100 { background-color: #fef3c7; }
.bg-red-100 { background-color: #fee2e2; }
.bg-gray-100 { background-color: #f3f4f6; }

.border-gray-300 { border-color: #d1d5db; }
.border-gray-200 { border-color: #e5e7eb; }

.border { border-width: 1px; }

.flex { display: flex; }
.inline-flex { display: inline-flex; }
.items-center { align-items: center; }
.justify-between { justify-content: space-between; }
.space-x-2 > * + * { margin-left: 0.5rem; }
.space-y-4 > * + * { margin-top: 1rem; }

.h-3 { height: 0.75rem; }
.h-4 { height: 1rem; }
.h-6 { height: 1.5rem; }
.h-8 { height: 2rem; }
.w-3 { width: 0.75rem; }
.w-4 { width: 1rem; }
.w-6 { width: 1.5rem; }
.w-8 { width: 2rem; }

.whitespace-nowrap { white-space: nowrap; }

.divide-y > * + * { border-top-width: 1px; border-top-color: #e5e7eb; }
.divide-gray-200 > * + * { border-top-color: #e5e7eb; }

.min-w-full { min-width: 100%; }

.hover\:bg-gray-50:hover { background-color: #f9fafb; }
.hover\:text-blue-900:hover { color: #1e3a8a; }
.hover\:text-green-900:hover { color: #14532d; }
.hover\:text-gray-600:hover { color: #4b5563; }

.capitalize { text-transform: capitalize; }

@media (min-width: 640px) {
  .sm\:grid-cols-2 { grid-template-columns: repeat(2, minmax(0, 1fr)); }
}

@media (min-width: 768px) {
  .md\:grid-cols-3 { grid-template-columns: repeat(3, minmax(0, 1fr)); }
}

@media (min-width: 1024px) {
  .lg\:grid-cols-4 { grid-template-columns: repeat(4, minmax(0, 1fr)); }
}
</style>
