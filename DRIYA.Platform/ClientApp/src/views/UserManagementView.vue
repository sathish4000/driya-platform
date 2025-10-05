<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useToast } from 'primevue/usetoast'
import { useConfirm } from 'primevue/useconfirm'

// PrimeVue Components
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import Dropdown from 'primevue/dropdown'
import Tag from 'primevue/tag'
import Avatar from 'primevue/avatar'
import Dialog from 'primevue/dialog'
import Card from 'primevue/card'
import Toolbar from 'primevue/toolbar'
// Remove problematic imports - we'll handle filtering manually

// Icons
import { 
  PlusIcon, 
  MagnifyingGlassIcon,
  FunnelIcon,
  UserPlusIcon,
  PencilIcon,
  TrashIcon,
  EyeIcon,
  ShieldCheckIcon
} from '@heroicons/vue/24/outline'

const toast = useToast()
const confirm = useConfirm()

// Data
const users = ref([])
const loading = ref(false)
const selectedUsers = ref([])
const globalFilter = ref('')
const filters = ref({
  global: { value: null, matchMode: 'contains' },
  firstName: { value: null, matchMode: 'startsWith' },
  lastName: { value: null, matchMode: 'startsWith' },
  email: { value: null, matchMode: 'contains' },
  role: { value: null, matchMode: 'equals' },
  status: { value: null, matchMode: 'equals' }
})

// Dialog states
const userDialog = ref(false)
const deleteUserDialog = ref(false)
const user = ref({})
const submitted = ref(false)

// Role options
const roles = ref([
  { label: 'Global Admin', value: 'GlobalAdmin' },
  { label: 'Tenant Admin', value: 'TenantAdmin' },
  { label: 'Manager', value: 'Manager' },
  { label: 'User', value: 'User' }
])

// Status options
const statuses = ref([
  { label: 'Active', value: 'active' },
  { label: 'Inactive', value: 'inactive' },
  { label: 'Pending', value: 'pending' },
  { label: 'Suspended', value: 'suspended' }
])

// Computed
const filteredUsers = computed(() => {
  if (!globalFilter.value) return users.value
  
  return users.value.filter(user => 
    user.firstName?.toLowerCase().includes(globalFilter.value.toLowerCase()) ||
    user.lastName?.toLowerCase().includes(globalFilter.value.toLowerCase()) ||
    user.email?.toLowerCase().includes(globalFilter.value.toLowerCase())
  )
})

// Methods
const loadUsers = async () => {
  loading.value = true
  try {
    // Mock data - replace with actual API call
    users.value = [
      {
        id: '1',
        firstName: 'John',
        lastName: 'Doe',
        email: 'john.doe@example.com',
        role: 'GlobalAdmin',
        status: 'active',
        lastLogin: '2024-01-15T10:30:00Z',
        createdAt: '2024-01-01T00:00:00Z',
        avatar: null
      },
      {
        id: '2',
        firstName: 'Jane',
        lastName: 'Smith',
        email: 'jane.smith@example.com',
        role: 'TenantAdmin',
        status: 'active',
        lastLogin: '2024-01-14T15:45:00Z',
        createdAt: '2024-01-02T00:00:00Z',
        avatar: null
      },
      {
        id: '3',
        firstName: 'Bob',
        lastName: 'Johnson',
        email: 'bob.johnson@example.com',
        role: 'User',
        status: 'pending',
        lastLogin: null,
        createdAt: '2024-01-10T00:00:00Z',
        avatar: null
      }
    ]
  } catch (error) {
    toast.add({ severity: 'error', summary: 'Error', detail: 'Failed to load users', life: 3000 })
  } finally {
    loading.value = false
  }
}

const openNew = () => {
  user.value = {
    firstName: '',
    lastName: '',
    email: '',
    role: 'User',
    status: 'pending'
  }
  submitted.value = false
  userDialog.value = true
}

const editUser = (editUser) => {
  user.value = { ...editUser }
  userDialog.value = true
}

const hideDialog = () => {
  userDialog.value = false
  submitted.value = false
}

const saveUser = async () => {
  submitted.value = true
  
  if (user.value.firstName && user.value.lastName && user.value.email) {
    try {
      if (user.value.id) {
        // Update existing user
        const index = users.value.findIndex(u => u.id === user.value.id)
        users.value[index] = { ...user.value }
        toast.add({ severity: 'success', summary: 'Success', detail: 'User updated successfully', life: 3000 })
      } else {
        // Create new user
        user.value.id = Date.now().toString()
        user.value.createdAt = new Date().toISOString()
        users.value.push({ ...user.value })
        toast.add({ severity: 'success', summary: 'Success', detail: 'User created successfully', life: 3000 })
      }
      
      userDialog.value = false
      user.value = {}
    } catch (error) {
      toast.add({ severity: 'error', summary: 'Error', detail: 'Failed to save user', life: 3000 })
    }
  }
}

const confirmDeleteUser = (userToDelete) => {
  user.value = userToDelete
  deleteUserDialog.value = true
}

const deleteUser = async () => {
  try {
    users.value = users.value.filter(u => u.id !== user.value.id)
    deleteUserDialog.value = false
    user.value = {}
    toast.add({ severity: 'success', summary: 'Success', detail: 'User deleted successfully', life: 3000 })
  } catch (error) {
    toast.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete user', life: 3000 })
  }
}

const getRoleSeverity = (role) => {
  switch (role) {
    case 'GlobalAdmin': return 'danger'
    case 'TenantAdmin': return 'warning'
    case 'Manager': return 'info'
    case 'User': return 'success'
    default: return 'secondary'
  }
}

const getStatusSeverity = (status) => {
  switch (status) {
    case 'active': return 'success'
    case 'inactive': return 'secondary'
    case 'pending': return 'warning'
    case 'suspended': return 'danger'
    default: return 'secondary'
  }
}

const formatDate = (dateString) => {
  if (!dateString) return 'Never'
  return new Date(dateString).toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  })
}

// Toolbar template
const leftToolbarTemplate = () => {
  return `
    <div class="flex flex-wrap gap-2">
      <Button label="New User" icon="pi pi-plus" severity="success" @click="openNew" />
      <Button 
        v-if="selectedUsers.length > 0"
        label="Delete Selected" 
        icon="pi pi-trash" 
        severity="danger" 
        @click="confirmDeleteSelected" 
      />
    </div>
  `
}

const rightToolbarTemplate = () => {
  return `
    <div class="flex items-center gap-2">
      <span class="p-input-icon-left">
        <i class="pi pi-search" />
        <InputText 
          v-model="globalFilter" 
          placeholder="Search users..." 
          class="w-64"
        />
      </span>
    </div>
  `
}

onMounted(() => {
  loadUsers()
})
</script>

<template>
  <div class="space-y-6">
    <!-- Page Header -->
    <div class="flex items-center justify-between">
      <div>
        <h1 class="text-2xl font-bold text-gray-900">User Management</h1>
        <p class="text-gray-600 mt-1">Manage users, roles, and permissions across your organization</p>
      </div>
      <div class="flex items-center space-x-3">
        <Button 
          label="Export" 
          icon="pi pi-download" 
          severity="secondary" 
          outlined 
        />
        <Button 
          label="Import" 
          icon="pi pi-upload" 
          severity="secondary" 
          outlined 
        />
      </div>
    </div>

    <!-- Stats Cards -->
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
      <Card class="p-4">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-sm font-medium text-gray-600">Total Users</p>
            <p class="text-2xl font-bold text-gray-900">{{ users.length }}</p>
          </div>
          <div class="w-12 h-12 bg-blue-100 rounded-lg flex items-center justify-center">
            <i class="pi pi-users text-blue-600 text-xl"></i>
          </div>
        </div>
      </Card>
      
      <Card class="p-4">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-sm font-medium text-gray-600">Active Users</p>
            <p class="text-2xl font-bold text-green-600">{{ users.filter(u => u.status === 'active').length }}</p>
            </div>
          <div class="w-12 h-12 bg-green-100 rounded-lg flex items-center justify-center">
            <i class="pi pi-check-circle text-green-600 text-xl"></i>
          </div>
        </div>
      </Card>
      
      <Card class="p-4">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-sm font-medium text-gray-600">Pending</p>
            <p class="text-2xl font-bold text-yellow-600">{{ users.filter(u => u.status === 'pending').length }}</p>
                        </div>
          <div class="w-12 h-12 bg-yellow-100 rounded-lg flex items-center justify-center">
            <i class="pi pi-clock text-yellow-600 text-xl"></i>
                      </div>
                    </div>
      </Card>
      
      <Card class="p-4">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-sm font-medium text-gray-600">Admins</p>
            <p class="text-2xl font-bold text-purple-600">{{ users.filter(u => u.role.includes('Admin')).length }}</p>
                    </div>
          <div class="w-12 h-12 bg-purple-100 rounded-lg flex items-center justify-center">
            <i class="pi pi-shield text-purple-600 text-xl"></i>
          </div>
        </div>
      </Card>
    </div>

    <!-- Data Table -->
    <Card>
      <template #content>
        <DataTable
          v-model:selection="selectedUsers"
          :value="filteredUsers"
          :loading="loading"
          dataKey="id"
          :paginator="true"
          :rows="10"
          :rowsPerPageOptions="[5, 10, 25, 50]"
          :filters="filters"
          filterDisplay="row"
          :globalFilterFields="['firstName', 'lastName', 'email', 'role']"
          paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
          currentPageReportTemplate="Showing {first} to {last} of {totalRecords} users"
          responsiveLayout="scroll"
          class="p-datatable-sm"
        >
          <template #header>
            <div class="flex flex-wrap items-center justify-between gap-4">
              <div class="flex items-center gap-2">
                <h3 class="text-lg font-semibold text-gray-900">Users</h3>
                <Tag :value="`${users.length} total`" severity="info" />
      </div>
              <div class="flex items-center gap-2">
                <span class="p-input-icon-left">
                  <i class="pi pi-search" />
                  <InputText 
                    v-model="globalFilter" 
                    placeholder="Search users..." 
                    class="w-64"
                  />
                </span>
                <Button 
                  label="New User" 
                  icon="pi pi-plus" 
                  severity="success" 
                  @click="openNew" 
                />
    </div>
  </div>
</template>

          <Column selectionMode="multiple" headerStyle="width: 3rem"></Column>
          
          <Column field="firstName" header="User" sortable style="min-width: 200px">
            <template #body="slotProps">
              <div class="flex items-center space-x-3">
                <Avatar 
                  :label="slotProps.data.firstName?.charAt(0) + slotProps.data.lastName?.charAt(0)"
                  shape="circle"
                  size="normal"
                  class="bg-primary-100 text-primary-700"
                />
                <div>
                  <div class="font-medium text-gray-900">
                    {{ slotProps.data.firstName }} {{ slotProps.data.lastName }}
                  </div>
                  <div class="text-sm text-gray-500">{{ slotProps.data.email }}</div>
                </div>
              </div>
            </template>
          </Column>

          <Column field="role" header="Role" sortable style="min-width: 120px">
            <template #body="slotProps">
              <Tag 
                :value="slotProps.data.role" 
                :severity="getRoleSeverity(slotProps.data.role)"
              />
            </template>
          </Column>

          <Column field="status" header="Status" sortable style="min-width: 100px">
            <template #body="slotProps">
              <Tag 
                :value="slotProps.data.status" 
                :severity="getStatusSeverity(slotProps.data.status)"
              />
            </template>
          </Column>

          <Column field="lastLogin" header="Last Login" sortable style="min-width: 150px">
            <template #body="slotProps">
              <span class="text-sm text-gray-600">
                {{ formatDate(slotProps.data.lastLogin) }}
              </span>
            </template>
          </Column>

          <Column field="createdAt" header="Created" sortable style="min-width: 120px">
            <template #body="slotProps">
              <span class="text-sm text-gray-600">
                {{ formatDate(slotProps.data.createdAt) }}
              </span>
            </template>
          </Column>

          <Column header="Actions" style="min-width: 120px">
            <template #body="slotProps">
              <div class="flex items-center space-x-2">
                <Button 
                  icon="pi pi-eye" 
                  severity="info" 
                  text 
                  rounded 
                  @click="editUser(slotProps.data)"
                  v-tooltip.top="'View Details'"
                />
                <Button 
                  icon="pi pi-pencil" 
                  severity="warning" 
                  text 
                  rounded 
                  @click="editUser(slotProps.data)"
                  v-tooltip.top="'Edit User'"
                />
                <Button 
                  icon="pi pi-trash" 
                  severity="danger" 
                  text 
                  rounded 
                  @click="confirmDeleteUser(slotProps.data)"
                  v-tooltip.top="'Delete User'"
                />
              </div>
            </template>
          </Column>
        </DataTable>
      </template>
    </Card>

    <!-- User Dialog -->
    <Dialog 
      v-model:visible="userDialog" 
      :style="{ width: '600px' }" 
      header="User Details" 
      :modal="true" 
      class="p-fluid"
    >
      <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div class="field">
          <label for="firstName" class="font-medium">First Name *</label>
          <InputText 
            id="firstName" 
            v-model="user.firstName" 
            required="true" 
            autofocus 
            :class="{ 'p-invalid': submitted && !user.firstName }"
          />
          <small v-if="submitted && !user.firstName" class="p-error">First name is required.</small>
        </div>

        <div class="field">
          <label for="lastName" class="font-medium">Last Name *</label>
          <InputText 
            id="lastName" 
            v-model="user.lastName" 
            required="true" 
            :class="{ 'p-invalid': submitted && !user.lastName }"
          />
          <small v-if="submitted && !user.lastName" class="p-error">Last name is required.</small>
        </div>

        <div class="field md:col-span-2">
          <label for="email" class="font-medium">Email *</label>
          <InputText 
            id="email" 
            v-model="user.email" 
            required="true" 
            type="email"
            :class="{ 'p-invalid': submitted && !user.email }"
          />
          <small v-if="submitted && !user.email" class="p-error">Email is required.</small>
        </div>

        <div class="field">
          <label for="role" class="font-medium">Role</label>
          <Dropdown 
            id="role" 
            v-model="user.role" 
            :options="roles" 
            optionLabel="label" 
            optionValue="value"
            placeholder="Select a role"
          />
        </div>

        <div class="field">
          <label for="status" class="font-medium">Status</label>
          <Dropdown 
            id="status" 
            v-model="user.status" 
            :options="statuses" 
            optionLabel="label" 
            optionValue="value"
            placeholder="Select status"
          />
        </div>
      </div>

      <template #footer>
        <Button 
          label="Cancel" 
          icon="pi pi-times" 
          text 
          @click="hideDialog"
        />
        <Button 
          label="Save" 
          icon="pi pi-check" 
          @click="saveUser"
        />
      </template>
    </Dialog>

    <!-- Delete User Dialog -->
    <Dialog 
      v-model:visible="deleteUserDialog" 
      :style="{ width: '450px' }" 
      header="Confirm" 
      :modal="true"
    >
      <div class="flex items-center space-x-3">
        <i class="pi pi-exclamation-triangle text-red-500 text-2xl"></i>
        <span>Are you sure you want to delete <b>{{ user.firstName }} {{ user.lastName }}</b>?</span>
      </div>
      <template #footer>
        <Button 
          label="No" 
          icon="pi pi-times" 
          text 
          @click="deleteUserDialog = false"
        />
        <Button 
          label="Yes" 
          icon="pi pi-check" 
          severity="danger" 
          @click="deleteUser"
        />
      </template>
    </Dialog>
  </div>
</template>

<style scoped>
/* Custom styles for better integration */
:deep(.p-datatable .p-datatable-header) {
  background-color: #f9fafb;
  border-bottom: 1px solid #e5e7eb;
}

:deep(.p-datatable .p-datatable-thead > tr > th) {
  background-color: #f9fafb;
  color: #374151;
  font-weight: 600;
  border-bottom: 1px solid #e5e7eb;
}

:deep(.p-datatable .p-datatable-tbody > tr) {
  border-bottom: 1px solid #f3f4f6;
  transition: background-color 0.15s ease-in-out;
}

:deep(.p-datatable .p-datatable-tbody > tr:hover) {
  background-color: #f9fafb;
}

:deep(.p-datatable .p-datatable-tbody > tr > td) {
  color: #111827;
}

:deep(.p-datatable .p-paginator) {
  background-color: white;
  border-top: 1px solid #e5e7eb;
}
</style>