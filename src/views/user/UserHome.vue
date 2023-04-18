<template>
  <div class="containers-container">
    <div class="row container-fluid g-3">
      <div class="col-sm-6">
        <ContainerUltimosDocumentos />
      </div>

      <div class="col-sm-6">
        <ContainerUltimasNoticias />
      </div>
    </div>

    <div class="row container-fluid justify-content-between g-3">
      <ContainerUltimosEventos />
    </div>
  </div>
</template>

<script setup lang="ts">
  import ContainerUltimasNoticias from "../../components/user/home/ContainerUltimasNoticias.vue"
  import ContainerUltimosEventos from "../../components/user/home/ContainerUltimosEventos.vue"
  import ContainerUltimosDocumentos from "../../components/user/home/ContainerUltimosDocumentos.vue"
</script>

<style scoped lang="scss">
  .containers-container {
    width: 80%;
    margin: 30px auto;
  }
</style>
