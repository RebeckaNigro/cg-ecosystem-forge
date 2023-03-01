import { createApp } from "vue"
import { createPinia } from "pinia"
import piniaPluginPersistedstate from "pinia-plugin-persistedstate"
import App from "./App.vue"
import router from "./router"
import "./assets/css/global.css"
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"
import { useUserStore } from "./stores/user/store"
// Global components
// import { AxiosKey, RouterKey } from './utils/keys';
// import { httpRequest } from './utils/http';

const pinia = createPinia()
// const providedUseRoute = useRoute()
pinia.use(piniaPluginPersistedstate)

const ecossistema = createApp(App)

ecossistema.use(pinia)
ecossistema.use(router)

// ecossistema.provide(AxiosKey, httpRequest)
// ecossistema.provide(RouterKey, providedUseRoute)

const userStore = useUserStore()
ecossistema.provide("userId", userStore.$state.loggedUser.id || "")
// ecossistema.use(userStore)

ecossistema.mount("#app")
