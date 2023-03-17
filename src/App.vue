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
    <FooterComponent />
  </div>
</template>

<style lang="scss">
  #app {
    font-family: "Montserrat-Regular", Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  }

  @font-face {
    font-family: "Montserrat-Bold";
    src: url(/fonts/Montserrat-Bold.ttf);
  }

  @font-face {
    font-family: "Montserrat-Regular";
    src: url(/fonts/Montserrat-Regular.ttf);
  }

  @font-face {
    font-family: "Montserrat-SemiBold";
    src: url(/fonts/Montserrat-SemiBold.ttf);
  }

  @font-face {
    font-family: "Montserrat-Medium";
    src: url(/fonts/Montserrat-Medium.ttf);
  }

  @font-face {
    font-family: "Montserrat-Light";
    src: url(/fonts/Montserrat-Light.ttf);
  }

  .background {
    background-image: url("/user/background.svg");
    background-repeat: no-repeat;
    background-size: cover;
    width: 100%;
  }

  .dark-title,
  .light-title {
    font-family: "Montserrat-Bold";
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
    font-family: "Montserrat-SemiBold";
    color: #000;
  }

  .light-body-text {
    font-family: "Montserrat-Regular";
    color: #fff;
  }

  .green-btn {
    border: none;
    font-family: "Montserrat-Bold";
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

  @media (max-width: 1200px) {
    .banner-text-container {
      h1 {
        font-size: 2rem;
      }

      p {
        text-align: start;
        font-size: 1rem !important;
        max-height: 60px;
      }
    }
  }

  @media (max-width: 991px) {
    .ghp {
      padding: 0 100px !important;
    }
    .banner-text-container {
      max-width: 300px;

      h1 {
        font-size: 1.5rem;
      }

      p {
        text-align: start;
        font-size: 0.8rem !important;
        max-height: 60px;
      }
    }
  }

  @media (max-width: 768px) {
    .ghp {
      padding: 0 50px !important;
    }
    .banner-text-container {
      max-width: 200px;

      h1 {
        font-size: 1rem;
      }

      p {
        text-align: start;
        font-size: 0.7rem !important;
        max-height: 60px;
      }
    }
  }

  @media (max-width: 576px) {
    .ghp {
      padding: 0 15px !important;
    }
    .banner-text-container {
      max-width: 150px;

      h1 {
        font-size: 0.8rem;
      }

      p {
        text-align: start;
        font-size: 0.6rem !important;
        max-height: 40px;
      }
    }
  }
</style>
