<template>
  <Spinner v-if="loading" />
  <section class="destaques-documentos mt-4" v-if="!loading">
    <header>
      <h1 class="font-semibold">DESTAQUES</h1>
    </header>
    <main class="row gap-5 justify-content-center mt-5">
      <CardDocumento
        v-for="(doc, index) in lastDocs"
        :documento="doc"
        :key="index"
        class="col-8 col-md-6 col-lg-3"
        :has-download-option="false"
      />
    </main>
  </section>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import CardDocumento from './CardDocumento.vue';
import { useDocumentStore } from '../../stores/documentos/store';
import { IDocumentoSimplificado } from '../../stores/documentos/types';
import Spinner from '../general/Spinner.vue';

const documentoStore = useDocumentStore();
const loading = ref(false);
const lastDocs = ref<IDocumentoSimplificado[]>([]);

onMounted(async () => {
  loading.value = true;
  await documentoStore.getLastDocs();
  lastDocs.value = documentoStore.lastDocs;
  loading.value = false;
});
</script>

<style scoped lang="scss">
section.destaques-documentos {
  padding-top: 3rem;
  padding-bottom: 3rem;

  h1 {
    color: #1e1e1e;
    font-size: 28px;
  }
}
</style>
