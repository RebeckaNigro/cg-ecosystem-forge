<template>
  <NavBar 
  :is-transparent="false"
  />
  <h1 class="mt-5 dark-title m-auto">Crie seu evento</h1>
  <FormEvento />
  <FooterComponent />
</template>

<script setup lang="ts">
import NavBar from '../../../components/general/NavBar.vue';
import FormEvento from '../../../components/user/eventos/FormEvento.vue'

</script>

<style scoped>

</style>