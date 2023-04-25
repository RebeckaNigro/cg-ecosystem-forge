<template>
  <div class="mx-auto box p-5 mb-5">
    <div class="text-center">
      <h1 class="fs-5 fw-bold">Últimas notícias enviadas</h1>
    </div>

    <Spinner v-if="loadingNews" />

    <div class="container-fluid">
      <div
        class="custom-list-item"
        v-for="noticia in noticiaStore.userLastNews"
        :key="noticia.id"
      >
        <div class="row g-0 px-2">
          <div
            class="col-lg-10 fw-bold d-flex align-items-center py-2 hover-pointer"
            @click="
              $router.push({
                name: 'NoticiaExpandida',
                params: { noticiaId: noticia.id }
              })
            "
          >
            {{ noticia.titulo }}
          </div>
          <div
            class="col-lg-2 d-flex align-items-center justify-content-end left-separator"
          >
            <div class="container-normal">
              <img
                src="/public/edit_icon.svg"
                alt=""
                class="image-icon-button hover-pointer"
                @click="
                  $router.push({
                    name: 'GerenciaNoticia',
                    params: { noticiaId: noticia.id }
                  })
                "
              />
              <img
                src="/public/delete_icon.svg"
                alt=""
                class="image-icon-button hover-pointer"
                @click="confirmDelete(noticia.id)"
              />
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="text-center row container-fluid mt-5 gy-2">
      <div class="col-lg-6">
        <button
          class="green-btn-outlined"
          @click="
            $router.push({
              name: 'NoticiasCriadas'
            })
          "
        >
          VER MAIS
        </button>
      </div>
      <div class="col-lg-6">
        <button
          class="green-btn-primary"
          @click="$router.push({ name: 'GerenciaNoticia' })"
        >
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
  import { onMounted, ref } from "vue"
  import Spinner from "../../general/Spinner.vue"
  import ConfirmModal from "../../general/ConfirmModal.vue"
  import { useNoticiaStore } from "../../../stores/noticias/store"
  import { useConfirmStore } from "../../../stores/confirm/store"
  import { useModalStore } from "../../../stores/modal/store"

  const noticiaStore = useNoticiaStore()
  const confirmStore = useConfirmStore()
  const modalStore = useModalStore()
  const loadingNews = ref(false)

  const confirmDelete = (id: number) => {
    confirmStore.showConfirmModal(
      "Tem certeza que desesja remover esta notícia?",
      id
    )
  }

  const confirmado = async () => {
    confirmStore.closeConfirm()

    await noticiaStore.deleteNews(confirmStore.options.parameter as number)

    const res = noticiaStore.response.getResponse()
    if (res.code === 200) {
      loadLastNews()
      modalStore.showSuccessModal("Notícia removida com sucesso!")
    } else {
      modalStore.showErrorModal("Erro ao remover notícia!")
    }
  }

  const loadLastNews = async () => {
    loadingNews.value = true
    await noticiaStore.getUserLastNews()
    loadingNews.value = false
  }

  onMounted(async () => {
    loadLastNews()
  })
</script>

<style scoped></style>
