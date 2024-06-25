<template>
  <Banner
    path="/documentos/editais.png"
    img-alt="Pesquisas"
    figcaption="Pesquisas"
    :img-overlay="true"
  >
    <ExternalHeader
      title="Editais"
      paragraph="Tenha acesso aos principais editais disponibilizados pelo Ecossistema Local de Inovação - Campo Grande - MS"
    />
  </Banner>
  <section id="editais-container" class="container my-5">
    <div class="row justify-content-end">
      <!-- <FilterComponent
				:items="[]"
				type="documento"
				field="status"
				class="col-3"
			/> -->

      <SearchComponent
        :items="novosEditais"
        type="documento"
        class="col-lg-3 col-md-5 col-7 align-self-end"
        @search-result="filtrarEditais"
      />
    </div>
    <Spinner v-if="loading" />
    <main class="row mt-5 mb-5" v-if="!loading">
      <div class="d-flex w-100 mb-4">
        <div @click="novosEditaisToggle" class="d-flex hover-pointer align-items-center">
          <span class="dark-title text-nowrap font-semibold"> Novos editais </span>
          <img src="/parceiros/arrow-down.svg" alt="Mostrar editais" class="mx-2" />
        </div>

        <hr class="w-100" />
      </div>

      <div v-if="isNovosEditaisVisible" class="px-4">
        <div v-for="(card, index) of novosEditais">
          <CardEdital :documento="card" />
        </div>

        <Spinner v-if="loadingMoreContent" />

        <button class="green-btn-primary w-25" v-if="!loadingMoreContent" @click="addMoreEditais">
          Ver mais
        </button>
      </div>

      <div class="d-flex w-100 mb-4">
        <div class="d-flex hover-pointer align-items-center">
          <span class="dark-title text-nowrap font-semibold"> Em andamento </span>
          <img src="/parceiros/arrow-down.svg" alt="Mostrar editais" class="mx-2" />
        </div>

        <hr class="w-100" />
      </div>

      <div class="d-flex w-100 mb-4">
        <div class="d-flex hover-pointer align-items-center">
          <span class="dark-title text-nowrap font-semibold"> Concluídos </span>
          <img src="/parceiros/arrow-down.svg" alt="Mostrar editais" class="mx-2" />
        </div>

        <hr class="w-100" />
      </div>
    </main>
  </section>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import CardEdital from '../../../components/documentos/editais/CardEdital.vue';
import GeneralBtn from '../../../components/buttons/GeneralBtn.vue';
import Banner from '../../../components/general/Banner.vue';
import ExternalHeader from '../../../components/general/ExternalHeader.vue';
import SearchComponent from '../../../components/general/SearchComponent.vue';
import { useDocumentStore } from '../../../stores/documentos/store';
import type { IDocumentoSimplificado } from '../../../stores/documentos/types';
import Spinner from '../../../components/general/Spinner.vue';

const loading = ref(false);
const loadingMoreContent = ref(false);
const isNovosEditaisVisible = ref(true);

const documentoStore = useDocumentStore();
const page = ref(1);
const novosEditais = ref<IDocumentoSimplificado[]>([]);

onMounted(async () => {
  loading.value = true;
  await documentoStore.getEditais(page.value);
  novosEditais.value = documentoStore.editais;
  loading.value = false;
});

const filtrarEditais = (documentosFiltrados: Array<IDocumentoSimplificado>) => {
  novosEditais.value = documentosFiltrados;
};

const addMoreEditais = async () => {
  loadingMoreContent.value = true;
  page.value++;
  await documentoStore.getEditais(page.value);
  novosEditais.value = documentoStore.editais;
  loadingMoreContent.value = false;
};

const novosEditaisToggle = () => {
  isNovosEditaisVisible.value = !isNovosEditaisVisible.value;
};
</script>

<style scoped lang="scss"></style>
