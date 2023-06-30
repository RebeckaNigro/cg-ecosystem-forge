<template>
  <div class="container mt-5">
	<h1 class="fs-4 text-start ms-5 mb-4">Home</h1>
    <div class="row g-3">
      <div class="col-12 col-lg-6">
        <ContainerUltimosDocumentos />
      </div>

      <div class="col-12 col-lg-6">
        <ContainerUltimasNoticias />
      </div>
    </div>

    <div class="row g-3">
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
h1{
	font-weight: 600!important;
}
</style>
