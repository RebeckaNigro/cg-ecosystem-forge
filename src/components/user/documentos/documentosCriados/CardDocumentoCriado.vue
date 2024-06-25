<template>
  <div>
    <div class="container-fluid box p-3 my-3 h-100 overflow-hidden">
      <div class="container">
        <div class="row mt-2 mx-2 justify-content-between align-items-center d-flex">
          <div class="col-6 col-md-8 text-start">
            <span class="text-secondary fs-6">{{ tagsFormatadas }}</span>
          </div>

          <div class="col-6 col-md-4 text-end d-flex justify-content-end">
            <img
              src="/view_icon.svg"
              alt=""
              class="image-icon-button"
              @click="
                $router.push({
                  name: 'DocumentoExpandido',
                  params: { documentoId: props.documento.id },
                })
              "
            />
            <img
              src="/edit_icon.svg"
              alt=""
              class="image-icon-button"
              @click="
                $router.push({
                  name: 'GerenciaDocumento',
                  params: { documentoId: documento.id },
                })
              "
            />
            <img src="/delete_icon.svg" alt="" class="image-icon-button" @click="confirmDelete" />
          </div>
        </div>
      </div>

      <p class="fs-3 display-3 mt-2 mb-0 text-start p-2">
        {{ documento.nome }}
      </p>

      <p class="fs-5 display-3 text-start p-2 my-0">
        {{ documento.autor }}
      </p>

      <p class="fs-6 display-3 mt-2 text-start p-2 text-truncate">
        {{ documento.descricao }}
      </p>

      <div class="text-start mt-4">
        <span class="text-secondary px-1">Atualizado em: {{ dataFormatada }}</span>
      </div>
    </div>
    <ConfirmModal
      v-show="confirmStore.visible"
      @confirm-true="confirmado"
      element-id="confirmDocModal"
      id="confirmDocModal"
    />
  </div>
</template>

<script setup lang="ts">
import type { INoticiaSimplificada } from '../../../../stores/noticias/types';
import { useDocumentStore } from '../../../../stores/documentos/store';
import { useModalStore } from '../../../../stores/modal/store';
import { useConfirmStore } from '../../../../stores/confirm/store';
import { brDateString, friendlyDateTime } from '../../../../utils/formatacao/datetime';
import { ref, onMounted, onUpdated } from 'vue';
import ConfirmModal from '../../../general/ConfirmModal.vue';
import router from '../../../../router';
import type { IDocumentoSimplificado } from '../../../../stores/documentos/types';
import { Modal } from 'bootstrap';

const documentoStore = useDocumentStore();
const modalStore = useModalStore();
const confirmStore = useConfirmStore();

const emit = defineEmits(['update-list']);

const props = defineProps<{
  documento: IDocumentoSimplificado;
}>();

const dataFormatada = ref('');
const tagsFormatadas = ref('');

const confirmDelete = () => {
  const modalDOM: any = document.querySelector('#confirmDocModal');
  confirmStore.setConfirmInstance(Modal.getOrCreateInstance(modalDOM));
  confirmStore.showConfirmModal(
    'Tem certeza que desesja remover este documento?',
    props.documento.id
  );
};

const confirmado = async () => {
  confirmStore.closeConfirm();

  if (confirmStore.options.parameter != null) {
    await documentoStore.deleteDoc(confirmStore.options.parameter as number);

    const res = documentoStore.response.getResponse();
    if (res.code === 200) {
      emit('update-list');
      modalStore.showSuccessModal('Documento removido com sucesso!');
    } else {
      modalStore.showErrorModal('Erro ao remover documento!');
    }
  }
};

onMounted(() => {
  console.log(props.documento);
  dataFormatada.value = friendlyDateTime(props.documento.data.toString());

  const prependHashtag = props.documento.tags.map((tag) => '#' + tag.descricao);
  tagsFormatadas.value = prependHashtag.join('  ');
});

onUpdated(() => {
  dataFormatada.value = friendlyDateTime(props.documento.data.toString());
  // const prependHashtag = props.documento.tags.map(
  //   (tag) => "#" + tag.descricao
  // )
  // tagsFormatadas.value = prependHashtag.join("  ")
});
</script>

<style scoped>
.image-icon-button {
  cursor: pointer;
}

.image-limiter {
  width: 100%;
  height: 300px;
}

.mascara-rascunho {
  display: flex;
  background-color: black;
  opacity: 20%;
  padding: 20px;
  justify-content: center;
  align-items: center;
}

.container-mascara-rascunho {
  display: flex;
  justify-content: center;
  align-items: center;
}

.mascara-rascunho-text {
  position: absolute;
}
</style>
