<template>
  <NavBar :is-transparent="false" />
  <div class="containers-container d-flex ghp">
    <ContainerUltimosConteudos
      content-type="noticias"
      container-title="Últimas notícias enviadas"
      :last-contents="dummyContent"
    />
  </div>
  <FooterComponent />
</template>

<script setup lang="ts">
import ContainerUltimosConteudos from '../../components/user/home/ContainerUltimosConteudos.vue'

interface lastContent {
  title: string,
  path: string,
  id: number
}

const dummyContent: Array<lastContent> = [
  {
    title: 'testando home testando home',
    path: '...',
    id: 1
  },
  {
    title: 'testando home testando home',
    path: '...',
    id: 2
  },
  {
    title: 'testando home testando home',
    path: '...',
    id: 3
  }]

</script>

<style scoped lang="scss">
  .containers-container {
    min-height: 600px;
  }
</style>