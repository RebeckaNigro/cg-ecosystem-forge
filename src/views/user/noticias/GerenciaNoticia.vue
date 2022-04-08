<template>
  <NavBar :is-transparent="false" />
  <header>
    <h1 class="dark-title mt-5">Envie sua notícia</h1>
  </header>
  <main>
    <FormNoticia />
  </main>
  <FooterComponent />
</template>

<script setup lang="ts">
import FormNoticia from '../../../components/user/noticias/FormNoticia.vue';
</script>

<style scoped lang="scss">

</style>