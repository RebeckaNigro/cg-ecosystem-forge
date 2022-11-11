<template>
  <section class="destaques ghp">
    <header>
      <h1 class="dark-title">DESTAQUES</h1>
    </header>
    <main>
      <DestaquesCarousel
        :carousel-data="eventoStore.ultimosEventos"
      />
    </main>
  </section>
</template>

<script setup lang="ts">
import { onMounted, reactive } from 'vue';
import { useEventoStore } from '../../stores/eventos/store';
import DestaquesCarousel from './DestaquesCarousel.vue';
const eventoStore = useEventoStore()

onMounted(() => {
	eventoStore.getLastEvents()
})
</script>

<style scoped lang="scss">
  section.destaques {
    background-color: #f6f6f6;
    h1 {
      font-size: 1.5rem;
    }
    padding-top: 50px !important;
    padding-bottom: 50px !important;
  }
  @media (max-width: 991px) {
    section.destaques {
      header {
        h1.dark-title {
          font-size: 1.2rem;
        }
      }   
    }
  }
</style>