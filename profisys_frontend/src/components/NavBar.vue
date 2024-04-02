<script setup lang="ts">
import { ref } from "vue";
import { useToast } from "primevue/usetoast";
import { useConfirm } from "primevue/useconfirm";
import FileUpload from "primevue/fileupload";
import Menubar from "primevue/menubar";

const toast = useToast();
const confirm = useConfirm();
const uploader = ref();
const emit = defineEmits(["refresh"]);

const deleteData = async () => {
  const response = await fetch(
    "http://localhost:5179/api/UploadFiles/DeleteData",
    {
      method: "POST",
    }
  );
  if (response.ok) {
    emit("refresh");
  } else {
    toast.add({
      severity: "error",
      summary: "Error",
      detail: "Error deleting documents",
      life: 3000,
    });
  }
};

const items = ref([
  {
    label: "Rekrutacja Profisys",
    icon: "pi pi-home",
  },
  {
    label: "Documents",
    icon: "pi pi-file",
    items: [
      {
        label: "Import",
        icon: "pi pi-plus",
        command: () => {
          if (uploader.value) {
            uploader.value.choose();
          }
        },
      },
      {
        label: "Delete All",
        icon: "pi pi-trash",
        command: () => {
          deleteDataConfirm();
        },
      },
    ],
  },
]);

const deleteDataConfirm = () => {
  confirm.require({
    message: "Do you want to delete all documents?",
    header: "Danger Zone",
    icon: "pi pi-info-circle",
    rejectLabel: "Cancel",
    acceptLabel: "Delete",
    rejectClass: "p-button-secondary p-button-outlined",
    acceptClass: "p-button-danger",
    accept: () => {
      deleteData();
      toast.add({
        severity: "info",
        summary: "Confirmed",
        detail: "Record deleted",
        life: 3000,
      });
    },
  });
};

const onUpload = () => {
  toast.add({
    severity: "success",
    summary: "Success",
    detail: "File Uploaded",
    life: 3000,
  });
};

function handleUpload() {
  onUpload();
  emit("refresh");
}
</script>

<template>
  <Menubar :model="items">
    <template #end>
      <i class="pi pi-user user-icon"></i>
      <span class="user">Michał Boese</span>
    </template>
  </Menubar>
  <FileUpload
    ref="uploader"
    mode="basic"
    auto
    url="http://localhost:5179/api/UploadFiles/UploadFile"
    accept=".csv"
    :maxFileSize="10000000"
    style="display: none"
    @upload="handleUpload"
    :name="'File'"
  />
</template>

<style scoped>
.user {
  font-weight: 500;
}
.user-icon {
  margin-right: 10px;
}
</style>
