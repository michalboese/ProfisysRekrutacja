<script setup lang="ts">
import { onMounted, ref } from "vue";
import { FilterMatchMode } from "primevue/api";
import { getDocuments, getDocumentItems } from "../services/api";
import DataTable from "primevue/datatable";
import NavBar from "./NavBar.vue";
import Column from "primevue/column";
import Panel from "primevue/panel";
import IconField from "primevue/iconfield";
import InputIcon from "primevue/inputicon";
import InputText from "primevue/inputtext";

interface Document {
  id: number;
  type: string;
  date: string;
  firstName: string;
  lastName: string;
  city: string;
}

interface DocumentItem {
  documentId: number;
  ordinal: number;
  product: string;
  quantity: number;
  price: number;
  taxRate: number;
}

const columns2 = [
  { field: "ordinal", header: "Ordinal" },
  { field: "product", header: "Product" },
  { field: "quantity", header: "Quantity" },
  { field: "price", header: "Price" },
  { field: "taxRate", header: "Tax Rate" },
];

const columns = [
  { field: "id", header: "ID" },
  { field: "type", header: "Type" },
  { field: "date", header: "Date" },
  { field: "firstName", header: "First Name" },
  { field: "lastName", header: "Last Name" },
  { field: "city", header: "City" },
];

const documents = ref<Document[]>([]);
const documentItems = ref<DocumentItem[]>([]);
const expandedRows = ref([]);
const dataLoaded = ref(false);

const filters = ref({
  global: { value: null, matchMode: FilterMatchMode.CONTAINS },
});

const fetchData = async () => {
  try {
    documents.value = await getDocuments();
    documentItems.value = await getDocumentItems();
  } catch (error) {
    console.error(error);
  } finally {
    dataLoaded.value = true;
  }
};

onMounted(async () => {
  fetchData();
});
</script>

<template>
  <div class="container">
    <NavBar @refresh="fetchData" />
  </div>
  <Panel>
    <DataTable
      v-if="dataLoaded && documents.length > 0"
      v-model:expandedRows="expandedRows"
      v-model:filters="filters"
      :globalFilterFields="[
        'id',
        'type',
        'firstName',
        'lastName',
        'date',
        'city',
      ]"
      dataKey="id"
      :value="documents"
      removableSort
      tableStyle="min-width: 60rem"
      scrollable
      scrollHeight="680px"
      paginator
      :rows="200"
      :rowsPerPageOptions="[200, 500, 1000]"
      paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
      currentPageReportTemplate="{first} to {last} of {totalRecords}"
    >
      <template #header>
        <div class="flex justify-content-end">
          <IconField iconPosition="left">
            <InputIcon>
              <i class="pi pi-search" />
            </InputIcon>
            <InputText v-model="filters['global'].value" placeholder="Search" />
          </IconField>
        </div>
      </template>
      <Column expander style="width: 5rem" />
      <Column
        v-for="col of columns"
        :key="col.field"
        :field="col.field"
        :header="col.header"
        sortable
      ></Column>
      <template #expansion="slotProps">
        <div class="p-3">
          <DataTable
            :value="
              documentItems.filter(
                (item) => item.documentId === slotProps.data.id
              )
            "
            style="padding-bottom: 0em"
            size="small"
            removableSort
          >
            <Column
              v-for="col of columns2"
              :key="col.field"
              :field="col.field"
              :header="col.header"
              sortable
            ></Column>
          </DataTable>
        </div>
      </template>
    </DataTable>
    <div v-else-if="!dataLoaded"><h3>Loading documents...</h3></div>
    <div v-else>
      <h3>Please import your documents first.</h3>
    </div>
  </Panel>
</template>

<style scoped>
:deep(.p-datatable .p-datatable-header) {
  display: flex;
  flex-direction: row-reverse;
}

:deep(.p-panel-header) {
  padding: 0 !important;
  display: none;
}

:deep(.p-datatable .p-datatable-thead > tr > th) {
  background-color: #d6e1ff;
}

.container {
  margin-bottom: 2em;
}
</style>
