<script setup lang="ts">
  import { onMounted, getCurrentInstance, watch } from "vue"
  import Alert from "../src/components/general/Alert.vue"
  import Cookies from "./components/general/Cookies.vue"
  import NavBar from "./components/general/NavBar.vue"
  import FooterComponent from "./components/general/FooterComponent.vue"
  import ModalComponent from "./components/general/ModalComponent.vue"
  import { Modal } from "bootstrap"
  import { useAlertStore } from "./stores/alert/store"
  import { useUserStore } from "./stores/user/store"
  import { useModalStore } from "./stores/modal/store"
  import { useRoute, useRouter } from "vue-router"

  const alertStore = useAlertStore()
  const userStore = useUserStore()
  const modalStore = useModalStore()

  const route = useRoute()
  const router = useRouter()

  onMounted(() => {
    if (userStore.disconnected) {
      userStore.logout()
      router.push({ name: "Login" })
    }

    watch(
      () => route.path,
      async (path) => {
        if (path == "/login") {
          const instance = getCurrentInstance()
          instance?.proxy?.$forceUpdate()
        }
      }
    )
  })
</script>

<template>
  <div class="background">
    <Alert v-if="alertStore.visible" />
    <ModalComponent
      v-show="modalStore.visible"
      id="generalModal"
    ></ModalComponent>
    <NavBar />
    <router-view />
    <Cookies />
    <FooterComponent/>
  </div>
</template>

<style lang="scss">
  #app {
    font-family: "Montserrat", Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
	overflow-x: hidden;
	font-weight: 500;
  }

  .background {
    background-image: url("/user/background.svg");
    background-repeat: no-repeat;
    background-size: cover;
    height: 100%;
  }

  .dark-title,
  .light-title {
    font-weight: 700;
    font-size: 1.5rem;
  }

  .light-title {
    color: #fff;
  }

  .dark-title {
    color: #000;
  }

  .dark-body-text,
  .light-body-text {
    font-size: 0.6rem;
  }

  .dark-body-text {
    font-weight: 600;
    color: #000;
  }

  .light-body-text {
    font-weight: 400;
    color: #fff;
  }

  .green-btn {
    border: none;
    font-weight: 700;
    background-color: #49907f;
  }

  .green-btn:hover {
    background-color: aquamarine;
  }

  .ghp {
    // general horizontal padding
    padding: 0 150px !important;
  }

  .boring-gray-border {
    border-radius: 10px !important;
    border: 1px solid #6b6a64 !important;
  }

  .ml-auto {
    margin-left: auto;
  }

  .mr-auto {
    margin-right: auto;
  }

  .banner-text-container {
    width: 100%;
    max-width: 500px;

    h1 {
      font-size: 2.5rem;
      margin-bottom: 0;
    }

    p {
      text-align: start;
      font-size: 1rem !important;
      max-height: 100px;
      height: 100%;
      margin-bottom: 0;
      overflow-y: auto;
    }
  }
</style>
