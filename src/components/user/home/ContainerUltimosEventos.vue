<template>
  <div class="mx-auto box p-lg-5 py-4 px-3 mb-5">
    <div class="text-center">
      <h1 class="fs-5 fw-bold">Últimos eventos enviados</h1>
    </div>

    <Spinner v-if="loadingEvents" />

    <div class="row container-fluid justify-content-between g-0">
      <!-- ÚLTIMOS CARDS DO USUÁRIO -->
      <CardEventoCriado
        v-for="(evento, index) in eventoStore.ultimosEventosUsuarioLogado"
        :key="index"
        :evento="evento"
        class="col-12 col-xl-4 p-2 p-xl-2 p-md-4"
        @update-list="loadLastEvents"
        :isRascunho="false"
      />
    </div>

    <div class="text-center row container-fluid mt-5 gy-2">
      <div class="col-lg-6">
        <button
          class="green-btn-outlined"
          @click="
            $router.push({
              name: 'EventosCriados',
            })
          "
        >
          VER MAIS
        </button>
      </div>
      <div class="col-lg-6">
        <button class="green-btn-primary" @click="$router.push({ name: 'GerenciaEvento' })">
          CRIAR EVENTO
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useEventoStore } from '../../../stores/eventos/store';
import CardEventoCriado from '../../../components/user/eventos/eventosCriados/CardEventoCriado.vue';
import Spinner from '../../general/Spinner.vue';

const eventoStore = useEventoStore();
const loadingEvents = ref(false);

const loadLastEvents = async () => {
  loadingEvents.value = true;
  await eventoStore.getUserLastEvents();

  console.dir(eventoStore.ultimosEventos);
  loadingEvents.value = false;
};

onMounted(async () => {
  await loadLastEvents();
});
</script>

<style scoped lang="scss"></style>
