<template>
  <section id="eventos-content" class="pt-5">
    <div class="container">
      <header>
        <h1 class="dark-title">EVENTOS</h1>
      </header>

      <Spinner spinner-color-class="text-dark" v-if="loadingContent" />
      <div class="row pb-3">
        <div class="col mb-4" v-for="(container, containerIndex) in eventos" :key="containerIndex">
          <CardEvento :evento="container" />
        </div>
      </div>

      <footer>
        <button type="button" class="green-btn-primary w-25 mb-4" @click="addEventsToView">
          VER MAIS
        </button>
        <Spinner spinner-color-class="text-dark" v-if="loadingMoreContent" />
      </footer>
    </div>
  </section>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import CardEvento from './CardEvento.vue';
import { useEventoStore } from '../../stores/eventos/store';
import type { IEventoSimplificado } from '../../stores/eventos/types';
import Spinner from '../general/Spinner.vue';

const eventoStore = useEventoStore();

const loadingContent = ref(false);
const loadingMoreContent = ref(false);
const page = ref(1);
let eventos = ref<Array<IEventoSimplificado>>();

const addEventsToView = async () => {
  page.value++;
  loadingMoreContent.value = true;
  await eventoStore.getAllEvents(page.value);
  eventos.value = eventoStore.eventos;
  loadingMoreContent.value = false;
};

onMounted(async () => {
  loadingContent.value = true;
  await eventoStore.getAllEvents(page.value);
  eventos.value = eventoStore.eventos;
  loadingContent.value = false;
});
</script>

<style scoped lang="scss">
#eventos-content {
  background: linear-gradient(180deg, rgba(255, 255, 255, 0.2) 0%, rgba(0, 0, 0, 0.2) 100%), #f8f9fa;

  header {
    h1 {
      font-size: 2rem;
      font-weight: 600;
      margin-bottom: 3rem;
    }
  }

  @media (max-width: 991px) {
    section#eventos-content {
      header {
        h1.dark-title {
          font-size: 1.2rem;
        }
      }
    }
  }
}
</style>
