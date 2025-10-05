<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useToast } from 'primevue/usetoast'
import { useConfirm } from 'primevue/useconfirm'
import { useApplicationStore } from '../stores/application'
import axios from 'axios'

// PrimeVue Components
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import InputText from 'primevue/inputtext'
import Button from 'primevue/button'
import Tag from 'primevue/tag'
import Dialog from 'primevue/dialog'
import Card from 'primevue/card'
import Toolbar from 'primevue/toolbar'
import Dropdown from 'primevue/dropdown'
import Textarea from 'primevue/textarea'

// Icons
import { 
  PlusIcon, 
  MagnifyingGlassIcon,
  PencilIcon,
  TrashIcon
} from '@heroicons/vue/24/outline'

const toast = useToast()
const confirm = useConfirm()
const applicationStore = useApplicationStore()

// Data
const features = ref([])
const loading = ref(false)
const selectedFeatures = ref([])
const globalFilter = ref('')
const filters = ref({
  global: { value: null, matchMode: 'contains' },
  name: { value: null, matchMode: 'startsWith' },
  featureKey: { value: null, matchMode: 'contains' },
  featureType: { value: null, matchMode: 'equals' }
})

// Dialog states
const featureDialog = ref(false)
const deleteFeatureDialog = ref(false)
const feature = ref({})
const submitted = ref(false)

// Feature type options
const featureTypes = ref([
  { label: 'Boolean', value: 'Boolean' },
  { label: 'String', value: 'String' },
  { label: 'Number', value: 'Number' },
  { label: 'JSON', value: 'JSON' }
])

// Computed properties
const hasSelectedFeatures = computed(() => selectedFeatures.value.length > 0)
const currentApplication = computed(() => applicationStore.currentApplication)
const currentApplicationId = computed(() => currentApplication.value?.id)

// Methods
const fetchFeatures = async () => {
  if (!currentApplicationId.value) {
    toast.add({ severity: 'warn', summary: 'No Application Selected', detail: 'Please select an application first', life: 3000 })
    return
  }

  try {
    loading.value = true
    const response = await axios.get(`/api/feature?applicationId=${currentApplicationId.value}`)
    features.value = response.data
  } catch (error: any) {
    toast.add({ severity: 'error', summary: 'Error', detail: error.response?.data?.message || 'Failed to fetch features', life: 3000 })
  } finally {
    loading.value = false
  }
}

const openNew = () => {
  if (!currentApplicationId.value) {
    toast.add({ severity: 'warn', summary: 'No Application Selected', detail: 'Please select an application first', life: 3000 })
    return
  }

  feature.value = {
    name: '',
    description: '',
    featureKey: '',
    featureType: 'Boolean',
    defaultValue: 'true',
    isSystemFeature: false,
    applicationId: currentApplicationId.value
  }
  submitted.value = false
  featureDialog.value = true
}

const hideDialog = () => {
  featureDialog.value = false
  submitted.value = false
}

const saveFeature = async () => {
  submitted.value = true
  
  if (feature.value.name && feature.value.featureKey) {
    try {
      if (feature.value.id) {
        // Update feature
        const response = await axios.put(`/api/feature/${feature.value.id}`, feature.value)
        const updatedFeature = response.data
        
        const index = features.value.findIndex(f => f.id === feature.value.id)
        if (index !== -1) {
          features.value[index] = updatedFeature
        }
        
        toast.add({ severity: 'success', summary: 'Success', detail: 'Feature Updated', life: 3000 })
      } else {
        // Create new feature
        const response = await axios.post('/api/feature', feature.value)
        const newFeature = response.data
        
        features.value.push(newFeature)
        toast.add({ severity: 'success', summary: 'Success', detail: 'Feature Created', life: 3000 })
      }
      featureDialog.value = false
      feature.value = {}
    } catch (error: any) {
      toast.add({ severity: 'error', summary: 'Error', detail: error.response?.data?.message || 'Failed to save feature', life: 3000 })
    }
  }
}

const editFeature = (selectedFeature) => {
  feature.value = { ...selectedFeature }
  featureDialog.value = true
}

const confirmDeleteFeature = (selectedFeature) => {
  feature.value = selectedFeature
  deleteFeatureDialog.value = true
}

const deleteFeature = async () => {
  try {
    await axios.delete(`/api/feature/${feature.value.id}`)
    
    features.value = features.value.filter(f => f.id !== feature.value.id)
    deleteFeatureDialog.value = false
    feature.value = {}
    toast.add({ severity: 'success', summary: 'Success', detail: 'Feature Deleted', life: 3000 })
  } catch (error: any) {
    toast.add({ severity: 'error', summary: 'Error', detail: error.response?.data?.message || 'Failed to delete feature', life: 3000 })
  }
}

const confirmDeleteSelected = () => {
  confirm.require({
    message: 'Are you sure you want to delete the selected features?',
    header: 'Confirm Deletion',
    icon: 'pi pi-exclamation-triangle',
    accept: async () => {
      try {
        for (const feat of selectedFeatures.value) {
          await axios.delete(`/api/feature/${feat.id}`)
        }
        features.value = features.value.filter(f => !selectedFeatures.value.includes(f))
        selectedFeatures.value = []
        toast.add({ severity: 'success', summary: 'Success', detail: 'Features Deleted', life: 3000 })
      } catch (error: any) {
        toast.add({ severity: 'error', summary: 'Error', detail: error.response?.data?.message || 'Failed to delete features', life: 3000 })
      }
    }
  })
}

const getFeatureTypeTagSeverity = (type) => {
  switch (type) {
    case 'Boolean': return 'success'
    case 'String': return 'info'
    case 'Number': return 'warning'
    case 'JSON': return 'danger'
    default: return 'info'
  }
}

// Watch for application changes
const watchApplication = () => {
  if (currentApplicationId.value) {
    fetchFeatures()
  } else {
    features.value = []
  }
}

onMounted(() => {
  watchApplication()
})

// Watch for application changes
import { watch } from 'vue'
watch(currentApplicationId, watchApplication)
</script>

<template>
  <div class="p-6">
    <!-- Application Selection Notice -->
    <div v-if="!currentApplication" class="mb-6 p-4 bg-yellow-50 border border-yellow-200 rounded-lg">
      <div class="flex items-center">
        <div class="flex-shrink-0">
          <svg class="h-5 w-5 text-yellow-400" viewBox="0 0 20 20" fill="currentColor">
            <path fill-rule="evenodd" d="M8.257 3.099c.765-1.36 2.722-1.36 3.486 0l5.58 9.92c.75 1.334-.213 2.98-1.742 2.98H4.42c-1.53 0-2.493-1.646-1.743-2.98l5.58-9.92zM11 13a1 1 0 11-2 0 1 1 0 012 0zm-1-8a1 1 0 00-1 1v3a1 1 0 002 0V6a1 1 0 00-1-1z" clip-rule="evenodd" />
          </svg>
        </div>
        <div class="ml-3">
          <h3 class="text-sm font-medium text-yellow-800">No Application Selected</h3>
          <div class="mt-2 text-sm text-yellow-700">
            <p>Please select an application from the dropdown in the top right to manage its features.</p>
          </div>
        </div>
      </div>
    </div>

    <!-- Current Application Info -->
    <div v-if="currentApplication" class="mb-6 p-4 bg-blue-50 border border-blue-200 rounded-lg">
      <div class="flex items-center">
        <div class="w-10 h-10 rounded-lg flex items-center justify-center mr-4" :style="{ backgroundColor: currentApplication.primaryColor || '#3b82f6' }">
          <i :class="currentApplication.icon || 'pi pi-cog'" class="text-white text-lg"></i>
        </div>
        <div>
          <h3 class="text-lg font-semibold text-blue-900">{{ currentApplication.name }}</h3>
          <p class="text-sm text-blue-700">Managing features for {{ currentApplication.appKey }}</p>
        </div>
      </div>
    </div>

    <Card class="shadow-sm border border-gray-200">
      <template #title>
        <Toolbar class="mb-4 border-0 p-0">
          <template #start>
            <h2 class="text-2xl font-bold text-gray-900">Feature Management</h2>
            <span v-if="currentApplication" class="ml-2 text-sm text-gray-500">for {{ currentApplication.name }}</span>
          </template>
          <template #end>
            <Button 
              label="New Feature" 
              icon="pi pi-plus" 
              severity="primary" 
              class="p-button-sm" 
              @click="openNew"
              :disabled="!currentApplication"
            />
          </template>
        </Toolbar>
      </template>
      <template #content>
        <DataTable 
          :value="features" 
          v-model:selection="selectedFeatures" 
          dataKey="id" 
          :paginator="true" 
          :rows="10" 
          :rowsPerPageOptions="[5, 10, 25]"
          :loading="loading" 
          :filters="filters" 
          filterDisplay="menu" 
          :globalFilterFields="['name', 'featureKey', 'featureType']"
          class="p-datatable-sm"
        >
          <template #header>
            <div class="flex flex-wrap gap-2 items-center justify-between">
              <div class="flex items-center space-x-2">
                <Button 
                  label="Delete Selected" 
                  icon="pi pi-trash" 
                  severity="danger" 
                  class="p-button-sm" 
                  @click="confirmDeleteSelected" 
                  :disabled="!hasSelectedFeatures" 
                />
              </div>
              <div class="relative">
                <span class="p-input-icon-left">
                  <MagnifyingGlassIcon class="h-4 w-4 text-gray-400 absolute left-3 top-1/2 -translate-y-1/2" />
                  <InputText 
                    v-model="filters['global'].value" 
                    placeholder="Global Search" 
                    class="pl-10 pr-3 py-2 border border-gray-300 rounded-md focus:ring-primary-500 focus:border-primary-500"
                  />
                </span>
              </div>
            </div>
          </template>
          <Column selectionMode="multiple" headerStyle="width: 3rem"></Column>
          <Column field="name" header="Name" sortable style="min-width: 12rem">
            <template #body="{ data }">
              <div class="font-medium text-gray-900">{{ data.name }}</div>
              <div class="text-sm text-gray-500">{{ data.featureKey }}</div>
            </template>
            <template #filter="{ filterModel, filterCallback }">
              <InputText v-model="filterModel.value" type="text" @input="filterCallback()" placeholder="Search by name" class="p-column-filter" />
            </template>
          </Column>
          <Column field="description" header="Description" sortable style="min-width: 16rem">
            <template #body="{ data }">
              <div class="text-gray-600">{{ data.description || 'No description' }}</div>
            </template>
          </Column>
          <Column field="featureType" header="Type" sortable style="min-width: 10rem">
            <template #body="{ data }">
              <Tag :value="data.featureType" :severity="getFeatureTypeTagSeverity(data.featureType)" />
            </template>
            <template #filter="{ filterModel, filterCallback }">
              <Dropdown v-model="filterModel.value" :options="featureTypes" optionLabel="label" optionValue="value" placeholder="Select a Type" class="p-column-filter" @change="filterCallback()" />
            </template>
          </Column>
          <Column field="defaultValue" header="Default Value" sortable style="min-width: 10rem">
            <template #body="{ data }">
              <div class="text-gray-600">{{ data.defaultValue || 'Not set' }}</div>
            </template>
          </Column>
          <Column field="isSystemFeature" header="System Feature" sortable style="min-width: 10rem">
            <template #body="{ data }">
              <Tag :value="data.isSystemFeature ? 'Yes' : 'No'" :severity="data.isSystemFeature ? 'success' : 'info'" />
            </template>
          </Column>
          <Column field="createdAt" header="Created" sortable style="min-width: 10rem">
            <template #body="{ data }">
              <div class="text-gray-600">{{ new Date(data.createdAt).toLocaleDateString() }}</div>
            </template>
          </Column>
          <Column :exportable="false" header="Actions" style="min-width: 8rem">
            <template #body="{ data }">
              <div class="flex space-x-2">
                <Button icon="pi pi-pencil" severity="info" text rounded @click="editFeature(data)" />
                <Button icon="pi pi-trash" severity="danger" text rounded @click="confirmDeleteFeature(data)" />
              </div>
            </template>
          </Column>
        </DataTable>
      </template>
    </Card>

    <!-- Feature Create/Edit Dialog -->
    <Dialog 
      v-model:visible="featureDialog" 
      :style="{ width: '600px' }" 
      :header="feature.id ? 'Edit Feature' : 'Create Feature'" 
      :modal="true" 
      class="p-fluid"
    >
      <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div class="p-field">
          <label for="name" class="font-semibold mb-2 block">Name *</label>
          <InputText 
            id="name" 
            v-model.trim="feature.name" 
            required="true" 
            autofocus 
            :class="{ 'p-invalid': submitted && !feature.name }" 
          />
          <small class="p-error" v-if="submitted && !feature.name">Name is required.</small>
        </div>
        
        <div class="p-field">
          <label for="featureKey" class="font-semibold mb-2 block">Feature Key *</label>
          <InputText 
            id="featureKey" 
            v-model.trim="feature.featureKey" 
            required="true" 
            :class="{ 'p-invalid': submitted && !feature.featureKey }" 
          />
          <small class="p-error" v-if="submitted && !feature.featureKey">Feature Key is required.</small>
        </div>
        
        <div class="p-field md:col-span-2">
          <label for="description" class="font-semibold mb-2 block">Description</label>
          <Textarea 
            id="description" 
            v-model.trim="feature.description" 
            placeholder="Enter feature description"
            rows="3"
          />
        </div>
        
        <div class="p-field">
          <label for="featureType" class="font-semibold mb-2 block">Type</label>
          <Dropdown 
            id="featureType" 
            v-model="feature.featureType" 
            :options="featureTypes" 
            optionLabel="label" 
            optionValue="value" 
            placeholder="Select a Type" 
          />
        </div>
        
        <div class="p-field">
          <label for="defaultValue" class="font-semibold mb-2 block">Default Value</label>
          <InputText 
            id="defaultValue" 
            v-model.trim="feature.defaultValue" 
            placeholder="true, false, or other value"
          />
        </div>
        
        <div class="p-field">
          <label class="font-semibold mb-2 block">System Feature</label>
          <div class="flex items-center">
            <input 
              type="checkbox" 
              v-model="feature.isSystemFeature" 
              class="mr-2"
            />
            <span class="text-sm text-gray-600">This is a system-wide feature</span>
          </div>
        </div>
      </div>

      <template #footer>
        <Button label="Cancel" icon="pi pi-times" text @click="hideDialog" />
        <Button label="Save" icon="pi pi-check" text @click="saveFeature" />
      </template>
    </Dialog>

    <!-- Feature Delete Dialog -->
    <Dialog 
      v-model:visible="deleteFeatureDialog" 
      :style="{ width: '450px' }" 
      header="Confirm" 
      :modal="true"
    >
      <div class="flex items-center justify-center">
        <i class="pi pi-exclamation-triangle mr-3 text-4xl text-red-500" />
        <span v-if="feature">Are you sure you want to delete <b>{{ feature.name }}</b>?</span>
      </div>
      <template #footer>
        <Button label="No" icon="pi pi-times" text @click="deleteFeatureDialog = false" />
        <Button label="Yes" icon="pi pi-check" text @click="deleteFeature" />
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