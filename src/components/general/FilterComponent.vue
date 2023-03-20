<template>
  <div class="text-start">
    <label for="filtro-data" class="ms-3 mb-2"
      >Filtrar por {{ contentType }}:</label
    >
    <div>
      <select
        name="filtro"
        id="filtro"
        class="form-input-primary borda-cinza-claro cinza"
      >
        <option value="">Todas</option>
        <option>Últimos 7 dias</option>
        <option>Últimos 15 dias</option>
        <option>Últimos 30 dias</option>
        <option>Últimos 3 meses</option>
      </select>
    </div>
  </div>
</template>

<script setup lang="ts">
  const props = defineProps<{
    contentType: string
    datas: string[]
  }>()
</script>

<style scoped></style>
