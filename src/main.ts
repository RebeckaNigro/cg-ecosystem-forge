import { QuillEditor } from '@vueup/vue-quill';
import '@vueup/vue-quill/dist/vue-quill.snow.css';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import { createPinia } from 'pinia';
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate';
import { createApp } from 'vue';
import App from './App.vue';
import './assets/css/global.scss';
import router from './router';
import { useUserStore } from './stores/user/store';
// Global components
// import { AxiosKey, RouterKey } from './utils/keys';
// import { httpRequest } from './utils/http';

const pinia = createPinia();
// const providedUseRoute = useRoute()
pinia.use(piniaPluginPersistedstate);

const ecossistema = createApp(App);

ecossistema.use(pinia);
ecossistema.use(router);
ecossistema.component('QuillEditor', QuillEditor);

// ecossistema.provide(AxiosKey, httpRequest)
// ecossistema.provide(RouterKey, providedUseRoute)

const userStore = useUserStore();
ecossistema.provide('userId', userStore.$state.loggedUser.id || '');
// ecossistema.use(userStore)

ecossistema.mount('#app');
