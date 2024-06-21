<template>
  <div class="box mx-auto py-5 px-4 p-lg-5 container position-relative">
    <h1 class="dark-title fs-4 text-start">Documentos criados</h1>

    <button class="fab green-btn-primary" @click="$router.push({ name: 'GerenciaDocumento' })">
      + CRIAR NOVO DOCUMENTO
    </button>

    <div class="row align-items-end mt-4 mb-5 justify-content-between px-2">
      <div class="col-xs-12 col-sm-6 col-md-4 my-2">
        <FilterComponent
          field="sessão"
          :items="documentoStore.allUserDocs"
          type="documento"
          @filter-result="filtrarDocumentos"
        />
      </div>

      <div class="col-xs-12 col-sm-6 col-md-4 my-2">
        <SearchComponent
          :items="documentoStore.allUserDocs"
          type="documento"
          @search-result="filtrarDocumentos"
        />
      </div>
    </div>

    <div class="container-fluid row justify-content-between g-0">
      <Spinner v-if="loadingDocs" />

      <CardDocumentoCriado
        v-for="(documento, index) in documentosExibidos"
        :key="index"
        :documento="documento"
        class="col-xs-12 col-sm-6 col-lg-4 p-2"
        @update-list="reloadDocumentos"
        :isRascunho="false"
      />
    </div>

    <div class="row container-fluid mb-3 mt-5">
      <div class="col-sm-6">
        <button class="green-btn-outlined button-specific" @click="$router.back()">Voltar</button>
      </div>

      <div class="col-sm-6">
        <button class="green-btn-primary button-specific" @click="addDocsToView">Ver mais</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import FilterComponent from '../../../general/FilterComponent.vue';
import SearchComponent from '../../../general/SearchComponent.vue';
import CardDocumentoCriado from '../../documentos/documentosCriados/CardDocumentoCriado.vue';
import Spinner from '../../../general/Spinner.vue';
import { useDocumentStore } from '../../../../stores/documentos/store';
import { IDocumentoSimplificado } from '../../../../stores/documentos/types';

const documentoStore = useDocumentStore();
const lastIndex = ref(6);
const loadingDocs = ref(false);
let documentos = ref<Array<IDocumentoSimplificado>>();
let documentosExibidos = ref<Array<IDocumentoSimplificado>>();

const filtrarDocumentos = (documentosFiltrados: Array<IDocumentoSimplificado>) => {
  lastIndex.value = 6;
  documentos.value = documentosFiltrados;
  documentosExibidos.value = documentosFiltrados.slice(0, lastIndex.value);
};

const addDocsToView = () => {
  lastIndex.value += 3;
  documentosExibidos.value = documentos.value?.slice(0, lastIndex.value);
};

const reloadDocumentos = async () => {
  loadingDocs.value = true;
  lastIndex.value = 6;

  await documentoStore.getUserLastDocs();
  documentos.value = documentoStore.allUserLastDocs;
  documentosExibidos.value = documentos.value.slice(0, lastIndex.value);
  loadingDocs.value = false;
};

onMounted(async () => {
  loadingDocs.value = true;
  await documentoStore.getUserDocs();
  documentos.value = documentoStore.allUserDocs;
  documentosExibidos.value = documentos.value.slice(0, lastIndex.value);
  loadingDocs.value = false;
});
</script>

<style scoped>
.external-card {
  width: 80%;
  position: relative;
}

.fab {
  position: absolute;
  top: -1.2rem;
  right: 3rem;
  width: 25%;
}

.button-specific {
  width: 100%;
  margin: 1rem 1rem;
  box-sizing: border-box;
}

@media screen and (min-width: 2550px) {
  .fab {
    width: 20%;
  }
}

@media screen and (max-width: 1200px) {
  .fab {
    width: 40%;
  }
}

@media screen and (max-width: 580px) {
  .fab {
    width: 50%;
    font-size: 12px;
    right: 1rem;
  }
}
</style>
