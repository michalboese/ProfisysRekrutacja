<script setup lang="ts">
import Table from "./components/Table.vue";
import Toast from "primevue/toast";
import ConfirmDialog from "primevue/confirmdialog";
</script>

<template>
  <div id="app">
    <ConfirmDialog />
    <Toast position="bottom-right" />
    <div class="container">
      <Table />
    </div>
  </div>
</template>

<style scoped>
.container {
  margin-top: 0.2em;
}
</style>
