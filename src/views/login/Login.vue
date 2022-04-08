<template>
 <NavBar 
  :is-transparent="false"
 />
 <LoginContainer />
 <FooterComponent />
</template>

<script setup lang="ts">
import LoginContainer from '../../components/login/LoginContainer.vue';
</script>

<style scoped lang="scss">

</style>