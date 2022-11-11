<template>
  <NavBar :is-transparent="false" />
  <div class="containers-container d-flex ghp">
    <ContainerUltimosConteudos
      content-type="noticias"
      container-title="Últimas notícias enviadas"
    />
    <ContainerUltimosEventos />
  </div>
  <FooterComponent />
</template>

<script setup lang="ts">
import ContainerUltimosConteudos from '../../components/user/home/ContainerUltimosConteudos.vue'
import ContainerUltimosEventos from '../../components/user/home/ContainerUltimosEventos.vue'

</script>

<style scoped lang="scss">
  .containers-container {
    min-height: 600px;
  }
</style>