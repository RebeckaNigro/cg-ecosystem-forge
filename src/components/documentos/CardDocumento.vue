<template>
  <div class="card-container d-flex flex-column justify-content-evenly align-items-start p-3">
    <div class="d-flex justify-content-between w-100">
      <span v-for="(tag, index) in documento.tags" :key="index" class="font-light">
        #{{ tag.descricao }}
      </span>
      <div>
        <img
          src="/view_icon.svg"
          alt="Visualizar documento"
          class="hover-pointer me-3"
          @click="
            $router.push({ name: 'DocumentoExpandido', params: { documentoId: documento.id } })
          "
        />

        <a :href="downloadLink" class="hover-pointer" v-if="hasDownloadOption">
          <img src="/icons/download-icon.svg" alt="Download" />
        </a>
      </div>
    </div>
    <div class="nome-documento">{{ documento.nome }}</div>
    <div class="nome-autor font-normal">{{ documento.autor }}</div>
    <p class="descricao-documento font-normal text-start overflow-hidden">
      {{ documento.descricao }}
    </p>
    <div class="atualizacao text-start font-normal">
      Atualizado em: {{ friendlyDateTime(documento.ultimaOperacao) }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useDocumentStore } from '../../stores/documentos/store';
import type { IDocumentoSimplificado } from '../../stores/documentos/types';
import { friendlyDateTime } from '../../utils/formatacao/datetime';

const props = defineProps<{
  documento: IDocumentoSimplificado;
  hasDownloadOption: boolean;
}>();

const downloadLink = ref('');
const documentoStore = useDocumentStore();

onMounted(async () => {
  if (props.hasDownloadOption) {
    await documentoStore.getDocDetailsById(props.documento.id);
    downloadLink.value = documentoStore.response.dado.download;
  }
});
</script>

<style scoped lang="scss">
.card-container {
  background-color: #fff;
  border: 1px solid #6b6a64;
  border-radius: 10px;
  height: 270px;
  margin: 10px 0;

  .nome-documento {
    font-weight: 600;
    font-size: 28px;
    text-align: start;
  }

  .nome-autor {
    font-size: 20px;
  }

  .descricao-documento {
    font-size: 16px;
  }

  .atualizacao {
    font-size: 15px;
  }
}

@media screen and (max-width: 580px) {
  .card-container {
    .nome-documento {
      font-size: 24px;
    }

    .nome-autor {
      font-size: 18px;
    }

    .descricao-documento {
      font-size: 14px;
    }

    .atualizacao {
      font-size: 13px;
    }
  }
}
</style>
