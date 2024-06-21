<template>
  <div class="mx-auto box p-lg-5 py-4 px-3 mb-5">
    <div class="text-center">
      <h1 class="fs-5 fw-bold">Últimas notícias enviadas</h1>
    </div>

    <Spinner v-if="loadingNews" />

    <div class="container-fluid">
      <div class="custom-list-item" v-for="noticia in noticiaStore.userLastNews" :key="noticia.id">
        <div class="row g-0 px-2">
          <div
            class="col-md-10 col-8 d-flex text-start align-items-center py-2 hover-pointer"
            @click="
              $router.push({
                name: 'NoticiaExpandida',
                params: { noticiaId: noticia.id },
              })
            "
          >
            {{ noticia.titulo }}
          </div>
          <div
            class="col-md-2 col-4 d-flex align-items-center justify-content-center left-separator"
          >
            <div>
              <img
                src="/edit_icon.svg"
                alt=""
                class="img-fluid hover-pointer"
                @click="
                  $router.push({
                    name: 'GerenciaNoticia',
                    params: { noticiaId: noticia.id },
                  })
                "
              />
              <img
                src="/delete_icon.svg"
                alt=""
                class="img-fluid hover-pointer"
                @click="confirmDelete(noticia.id)"
              />
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="text-center row mt-5">
      <div class="col-5 col-xlg-6">
        <button
          class="green-btn-outlined"
          @click="
            $router.push({
              name: 'NoticiasCriadas',
            })
          "
        >
          VER MAIS
        </button>
      </div>
      <div class="col-7 col-xlg-6">
        <button class="green-btn-primary" @click="$router.push({ name: 'GerenciaNoticia' })">
          CRIAR NOTÍCIA
        </button>
      </div>
    </div>
  </div>

  <ConfirmModal
    v-show="confirmStore.visible"
    @confirm-true="confirmado"
    element-id="confirmNewsModal"
    id="confirmNewsModal"
  />
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import Spinner from '../../general/Spinner.vue';
import ConfirmModal from '../../general/ConfirmModal.vue';
import { useNoticiaStore } from '../../../stores/noticias/store';
import { useConfirmStore } from '../../../stores/confirm/store';
import { useModalStore } from '../../../stores/modal/store';
import { Modal } from 'bootstrap';

const noticiaStore = useNoticiaStore();
const confirmStore = useConfirmStore();
const modalStore = useModalStore();
const loadingNews = ref(false);

const confirmDelete = (id: number) => {
  const modalDOM: any = document.querySelector('#confirmNewsModal');

  confirmStore.setConfirmInstance(Modal.getOrCreateInstance(modalDOM)!);

  confirmStore.showConfirmModal('Tem certeza que desesja remover esta notícia?', id);
};

const confirmado = async () => {
  confirmStore.closeConfirm();

  await noticiaStore.deleteNews(confirmStore.options.parameter as number);

  const res = noticiaStore.response.getResponse();
  if (res.code === 200) {
    loadLastNews();
    modalStore.showSuccessModal('Notícia removida com sucesso!');
  } else {
    modalStore.showErrorModal('Erro ao remover notícia!');
  }
};

const loadLastNews = async () => {
  loadingNews.value = true;
  await noticiaStore.getUserLastNews();
  loadingNews.value = false;
};

onMounted(async () => {
  loadLastNews();
});
</script>

<style scoped lang="scss"></style>
