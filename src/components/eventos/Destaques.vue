<template>
  <section class="destaques ghp">
    <header>
      <h1 class="dark-title my-4">DESTAQUES</h1>
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
    
    h1 {
	  font-family: 'Montserrat-SemiBold', sans-serif;
      font-size: 2rem;
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