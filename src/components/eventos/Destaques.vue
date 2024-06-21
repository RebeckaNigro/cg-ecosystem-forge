<template>
  <section class="destaques container">
    <header>
      <h1 class="dark-title my-4">DESTAQUES</h1>
    </header>
    <main>
      <Spinner spinnerColorClass="text-dark" v-if="loadingContent" />
      <DestaquesCarousel :carousel-data="eventoStore.ultimosEventos" />
    </main>
  </section>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import { useEventoStore } from '../../stores/eventos/store';
import Spinner from '../general/Spinner.vue';
import DestaquesCarousel from './DestaquesCarousel.vue';
const eventoStore = useEventoStore();
const loadingContent = ref(false);

onMounted(async () => {
  loadingContent.value = true;
  await eventoStore.getLastEvents();
  loadingContent.value = false;
});
</script>

<style scoped lang="scss">
section.destaques {
  h1 {
    font-weight: 600;
    font-size: 2rem;
  }
  padding-top: 50px !important;
  padding-bottom: 50px !important;
}
@media (max-width: 991px) {
  section.destaques {
    header {
      h1.dark-title {
        font-size: 1.5rem;
      }
    }
  }
}
</style>
