<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useToast } from 'primevue/usetoast'
import { useConfirm } from 'primevue/useconfirm'
import { useApplicationStore } from '../stores/application'

// PrimeVue Components
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import InputText from 'primevue/inputtext'
import Button from 'primevue/button'
import Tag from 'primevue/tag'
import Avatar from 'primevue/avatar'
import Dialog from 'primevue/dialog'
import Card from 'primevue/card'
import Toolbar from 'primevue/toolbar'
import Dropdown from 'primevue/dropdown'

// Icons
import { 
  MagnifyingGlassIcon
} from '@heroicons/vue/24/outline'

const toast = useToast()
const confirm = useConfirm()
const applicationStore = useApplicationStore()

// Data
const applications = computed(() => applicationStore.applications)
const loading = computed(() => applicationStore.loading)
const selectedApplications = ref([])
const globalFilter = ref('')
const filters = ref({
  global: { value: null, matchMode: 'contains' },
  name: { value: null, matchMode: 'startsWith' },
  appKey: { value: null, matchMode: 'contains' },
  status: { value: null, matchMode: 'equals' }
})

// Dialog states
const applicationDialog = ref(false)
const deleteApplicationDialog = ref(false)
const application = ref<any>({})
const submitted = ref(false)

// Status options
const statuses = ref([
  { label: 'Active', value: 'Active' },
  { label: 'Inactive', value: 'Inactive' },
  { label: 'Maintenance', value: 'Maintenance' }
])

// Computed properties
const hasSelectedApplications = computed(() => selectedApplications.value.length > 0)

// Methods
const fetchApplications = async () => {
  await applicationStore.fetchApplications()
}

const openNew = () => {
  application.value = {
    name: '',
    description: '',
    appKey: '',
    domain: '',
    subdomain: '',
    isActive: true,
    status: 'Active',
    icon: 'pi pi-cog',
    primaryColor: '#3b82f6'
  }
  submitted.value = false
  applicationDialog.value = true
}

const hideDialog = () => {
  applicationDialog.value = false
  submitted.value = false
}

const saveApplication = async () => {
  submitted.value = true
  
  if (application.value.name && application.value.appKey) {
    try {
      if (application.value.id) {
        // Update application
        await applicationStore.updateApplication(application.value.id, application.value)
        toast.add({ severity: 'success', summary: 'Success', detail: 'Application Updated', life: 3000 })
      } else {
        // Create new application
        await applicationStore.createApplication(application.value)
        toast.add({ severity: 'success', summary: 'Success', detail: 'Application Created', life: 3000 })
      }
      applicationDialog.value = false
      application.value = {}
    } catch (error: any) {
      toast.add({ severity: 'error', summary: 'Error', detail: error.message || 'Failed to save application', life: 3000 })
    }
  }
}

const editApplication = (selectedApplication: any) => {
  application.value = { ...selectedApplication }
  applicationDialog.value = true
}

const confirmDeleteApplication = (selectedApplication: any) => {
  application.value = selectedApplication
  deleteApplicationDialog.value = true
}

const deleteApplication = async () => {
  try {
    await applicationStore.deleteApplication(application.value.id)
    deleteApplicationDialog.value = false
    application.value = {}
    toast.add({ severity: 'success', summary: 'Success', detail: 'Application Deleted', life: 3000 })
  } catch (error: any) {
    toast.add({ severity: 'error', summary: 'Error', detail: error.message || 'Failed to delete application', life: 3000 })
  }
}

const confirmDeleteSelected = () => {
  confirm.require({
    message: 'Are you sure you want to delete the selected applications?',
    header: 'Confirm Deletion',
    icon: 'pi pi-exclamation-triangle',
    accept: async () => {
      try {
        for (const app of selectedApplications.value) {
          await applicationStore.deleteApplication((app as any).id)
        }
        selectedApplications.value = []
        toast.add({ severity: 'success', summary: 'Success', detail: 'Applications Deleted', life: 3000 })
      } catch (error: any) {
        toast.add({ severity: 'error', summary: 'Error', detail: error.message || 'Failed to delete applications', life: 3000 })
      }
    }
  })
}

const getStatusTagSeverity = (status: string) => {
  switch (status) {
    case 'Active': return 'success'
    case 'Inactive': return 'danger'
    case 'Maintenance': return 'warning'
    default: return 'info'
  }
}

const viewApplication = (app: any) => {
  // Navigate to application details or select it
  applicationStore.selectApplication(app)
  toast.add({ severity: 'info', summary: 'Application Selected', detail: `Now managing ${app.name}`, life: 3000 })
}

onMounted(fetchApplications)
</script>

<template>
  <div class="p-6">
    <Card class="shadow-sm border border-gray-200">
      <template #title>
        <Toolbar class="mb-4 border-0 p-0 bg-transparent">
          <template #start>
            <h2 class="text-2xl font-bold text-gray-900">Application Management</h2>
          </template>
          <template #end>
            <Button 
              label="New Application" 
              icon="pi pi-plus" 
              class="p-button-primary" 
              @click="openNew" 
            />
          </template>
        </Toolbar>
      </template>
      <template #content>
        <DataTable 
          :value="applications" 
          v-model:selection="selectedApplications" 
          dataKey="id" 
          :paginator="true" 
          :rows="10" 
          :rowsPerPageOptions="[5, 10, 25]"
          :loading="loading" 
          :filters="filters" 
          filterDisplay="menu" 
          :globalFilterFields="['name', 'appKey', 'status']"
          class="p-datatable-sm"
          responsiveLayout="scroll"
        >
          <template #header>
            <div class="flex flex-wrap gap-2 items-center justify-between">
              <div class="flex items-center space-x-2">
                <Button 
                  label="Delete Selected" 
                  icon="pi pi-trash" 
                  severity="danger" 
                  size="small"
                  @click="confirmDeleteSelected" 
                  :disabled="!hasSelectedApplications" 
                />
              </div>
              <div class="relative">
                <span class="p-input-icon-left">
                  <MagnifyingGlassIcon class="h-4 w-4 text-gray-400 absolute left-3 top-1/2 -translate-y-1/2" />
                  <InputText 
                    v-model="filters['global'].value" 
                    placeholder="Search applications..." 
                    class="pl-10"
                  />
                </span>
              </div>
            </div>
          </template>
          <Column selectionMode="multiple" headerStyle="width: 3rem" />
          <Column field="name" header="Name" sortable style="min-width: 14rem">
            <template #body="{ data }">
              <div class="flex items-center">
                <Avatar 
                  :icon="data.icon || 'pi pi-cog'" 
                  class="mr-3" 
                  size="normal"
                  shape="circle"
                  :style="{ backgroundColor: data.primaryColor || '#93c5fd', color: '#ffffff' }"
                />
                <div>
                  <div class="font-medium text-gray-900">{{ data.name }}</div>
                  <div class="text-sm text-gray-500">{{ data.appKey }}</div>
                </div>
              </div>
            </template>
            <template #filter="{ filterModel, filterCallback }">
              <InputText v-model="filterModel.value" type="text" @input="filterCallback()" placeholder="Search by name" class="p-column-filter" />
            </template>
          </Column>
          <Column field="description" header="Description" sortable style="min-width: 16rem">
            <template #body="{ data }">
              <div class="text-gray-700">{{ data.description || 'No description' }}</div>
            </template>
          </Column>
          <Column field="subdomain" header="Subdomain" sortable style="min-width: 10rem">
            <template #body="{ data }">
              <div class="text-gray-600 text-sm">{{ data.subdomain || 'Not set' }}</div>
            </template>
          </Column>
          <Column field="status" header="Status" sortable style="min-width: 10rem">
            <template #body="{ data }">
              <Tag :value="data.status" :severity="getStatusTagSeverity(data.status)" />
            </template>
            <template #filter="{ filterModel, filterCallback }">
              <Dropdown v-model="filterModel.value" :options="statuses" optionLabel="label" optionValue="value" placeholder="Select a Status" class="p-column-filter" @change="filterCallback()" />
            </template>
          </Column>
          <Column field="createdAt" header="Created" sortable style="min-width: 10rem">
            <template #body="{ data }">
              <div class="text-gray-600 text-sm">{{ new Date(data.createdAt).toLocaleDateString() }}</div>
            </template>
          </Column>
          <Column header="Actions" style="min-width: 10rem">
            <template #body="{ data }">
              <div class="flex space-x-2">
                <Button icon="pi pi-eye" severity="info" text rounded @click="viewApplication(data)" v-tooltip.top="'Select Application'" />
                <Button icon="pi pi-pencil" severity="info" text rounded @click="editApplication(data)" v-tooltip.top="'Edit'" />
                <Button icon="pi pi-trash" severity="danger" text rounded @click="confirmDeleteApplication(data)" v-tooltip.top="'Delete'" />
              </div>
            </template>
          </Column>
        </DataTable>
      </template>
    </Card>

    <!-- Application Create/Edit Dialog -->
    <Dialog 
      v-model:visible="applicationDialog" 
      :style="{ width: '650px' }" 
      :header="application.id ? 'Edit Application' : 'New Application'" 
      :modal="true" 
      class="p-fluid"
    >
      <div class="grid grid-cols-1 gap-4 mt-4">
        <div class="grid grid-cols-2 gap-4">
          <div class="field">
            <label for="name" class="font-semibold mb-2 block">Name *</label>
            <InputText 
              id="name" 
              v-model.trim="application.name" 
              required
              autofocus 
              :class="{ 'p-invalid': submitted && !application.name }" 
              placeholder="Enter application name"
            />
            <small class="p-error" v-if="submitted && !application.name">Name is required.</small>
          </div>
          
          <div class="field">
            <label for="appKey" class="font-semibold mb-2 block">App Key *</label>
            <InputText 
              id="appKey" 
              v-model.trim="application.appKey" 
              required
              :class="{ 'p-invalid': submitted && !application.appKey }" 
              placeholder="e.g., ecommerce"
            />
            <small class="p-error" v-if="submitted && !application.appKey">App Key is required.</small>
          </div>
        </div>
        
        <div class="field">
          <label for="description" class="font-semibold mb-2 block">Description</label>
          <InputText 
            id="description" 
            v-model.trim="application.description" 
            placeholder="Enter application description"
          />
        </div>
        
        <div class="grid grid-cols-2 gap-4">
          <div class="field">
            <label for="subdomain" class="font-semibold mb-2 block">Subdomain</label>
            <InputText 
              id="subdomain" 
              v-model.trim="application.subdomain" 
              placeholder="app1"
            />
          </div>
          
          <div class="field">
            <label for="icon" class="font-semibold mb-2 block">Icon</label>
            <InputText 
              id="icon" 
              v-model.trim="application.icon" 
              placeholder="pi pi-cog"
            />
          </div>
        </div>
        
        <div class="grid grid-cols-2 gap-4">
          <div class="field">
            <label for="status" class="font-semibold mb-2 block">Status</label>
            <Dropdown 
              id="status" 
              v-model="application.status" 
              :options="statuses" 
              optionLabel="label" 
              optionValue="value" 
              placeholder="Select a Status"
            />
          </div>
          
          <div class="field">
            <label for="primaryColor" class="font-semibold mb-2 block">Primary Color</label>
            <div class="flex items-center space-x-2">
              <InputText 
                id="primaryColor" 
                v-model="application.primaryColor" 
                placeholder="#3b82f6"
                class="flex-1"
              />
              <div 
                class="w-10 h-10 rounded border border-gray-300" 
                :style="{ backgroundColor: application.primaryColor || '#3b82f6' }"
              />
            </div>
          </div>
        </div>
        
        <div class="field flex items-center">
          <input 
            type="checkbox" 
            id="isActive"
            v-model="application.isActive" 
            class="mr-2"
          />
          <label for="isActive" class="text-sm font-medium text-gray-700">Application is active</label>
        </div>
      </div>

      <template #footer>
        <Button label="Cancel" icon="pi pi-times" text @click="hideDialog" />
        <Button label="Save" icon="pi pi-check" @click="saveApplication" />
      </template>
    </Dialog>

    <!-- Application Delete Dialog -->
    <Dialog 
      v-model:visible="deleteApplicationDialog" 
      :style="{ width: '550px' }" 
      header="Confirm Deletion" 
      :modal="true"
    >
      <div class="flex items-start gap-4">
        <i class="pi pi-exclamation-triangle text-4xl text-amber-500" />
        <div class="flex-1">
          <p class="text-lg font-semibold text-gray-900 mb-3">
            Delete <span class="text-red-600">{{ application.name }}</span>?
          </p>
          <div class="bg-red-50 border border-red-200 rounded-md p-3 mb-3">
            <p class="text-sm text-red-800 font-medium mb-2">⚠️ Warning: This action will permanently delete:</p>
            <ul class="text-sm text-red-700 list-disc list-inside space-y-1 ml-2">
              <li>All tenants associated with this application</li>
              <li>All features configured for this application</li>
              <li>All user access records</li>
              <li>All billing and subscription data</li>
            </ul>
          </div>
          <p class="text-sm text-gray-600">
            This action cannot be undone. Please confirm to proceed.
          </p>
        </div>
      </div>
      <template #footer>
        <Button label="Cancel" icon="pi pi-times" text @click="deleteApplicationDialog = false" />
        <Button label="Delete Application" icon="pi pi-trash" severity="danger" @click="deleteApplication" />
      </template>
    </Dialog>
  </div>
</template>
