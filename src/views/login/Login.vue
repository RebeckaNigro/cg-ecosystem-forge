<template>
  <LoginContainer />
</template>

<script setup lang="ts">
  import { onMounted } from "vue"
  import LoginContainer from "../../components/login/LoginContainer.vue"
  import { useUserStore } from "../../stores/user/store"
  import { useAlertStore } from "../../stores/alert/store"

  const userStore = useUserStore()
  const alertStore = useAlertStore()

  onMounted(() => {
    if (userStore.disconnected) {
      alertStore.options = {
        message: "Seu login expirou",
        type: "warning",
        dispensable: true,
        timeout: true
      }
      alertStore.visible = true
    }

    userStore.disconnected = false
  })
</script>

<style scoped lang="scss"></style>
