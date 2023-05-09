<template>
  <div>
    <div class="container-fluid box p-3 my-3 h-100 overflow-hidden">
      <div v-if="!props.isRascunho">
        <img
          :src="
            noticia.arquivo
              ? 'data:image/png;base64, ' + noticia.arquivo
              : '/public/noticias/noticia-expandida/default-news-cover.svg'
          "
          alt="news-image"
          class="image-limiter"
        />
        <!-- <span class="dark-body-text fs-6">{{ noticia.titulo }}</span> -->
      </div>
      <div v-else>
        <div class="container-mascara-rascunho">
          <div class="mascara-rascunho">
            <img
              src="/public/noticias/noticia-expandida/default-news-cover.svg"
              alt="news-image"
              class="rascunho-img"
            />
          </div>
          <div
            class="text-danger fs-5 my-2 fw-bold fst-italic mascara-rascunho-text"
          >
            (Rascunho)
          </div>
        </div>
      </div>

      <div class="container">
        <div
          class="row mt-2 mx-2 justify-content-between align-items-center d-flex"
        >
          <div class="col-md-8 text-start">
            <span class="text-secondary fs-6">{{ tagsFormatadas }}</span>
          </div>

          <div class="col-md-4 text-end" v-if="!props.isRascunho">
            <img
              src="/public/view_icon.svg"
              alt=""
              class="image-icon-button"
              @click="
                $router.push({
                  name: 'NoticiaExpandida',
                  params: { noticiaId: props.noticia.id }
                })
              "
            />
            <img
              src="/public/edit_icon.svg"
              alt=""
              class="image-icon-button"
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
              class="image-icon-button"
              @click="confirmDelete"
            />
          </div>

          <div class="col-md-4 text-end" v-else>
            <img
              src="/public/edit_icon.svg"
              alt=""
              class="image-icon-button"
              @click="loadFormComRascunho"
            />
            <img
              src="/public/delete_icon.svg"
              alt=""
              class="image-icon-button"
              @click="confirmDeleteRascunho"
            />
          </div>
        </div>
      </div>

      <p class="fs-3 display-3 mt-2 text-start p-2">
        {{ noticia.titulo }}
      </p>

      <div class="text-start mt-4">
        <span class="text-secondary px-1" v-if="noticia.dataOperacao"
          >Atualizada em: {{ dataOperacaoFormatada }}</span
        >
      </div>
    </div>
    <ConfirmModal
      v-show="confirmStore.visible"
      @confirm-true="confirmado"
      element-id="confirmNewsModal"
      id="confirmNewsModal"
    />
  </div>
</template>

<script setup lang="ts">
  import { INoticiaSimplificada } from "./../../../../stores/noticias/types"
  import { useNoticiaStore } from "../../../../stores/noticias/store"
  import { useModalStore } from "../../../../stores/modal/store"
  import { useConfirmStore } from "../../../../stores/confirm/store"
  import { brDateString } from "./../../../../utils/formatacao/datetime"
  import { ref, onMounted, onUpdated } from "vue"
  import ConfirmModal from "../../../general/ConfirmModal.vue"
  import router from "../../../../router"
  import { Modal } from "bootstrap"

  const noticiaStore = useNoticiaStore()
  const modalStore = useModalStore()
  const confirmStore = useConfirmStore()

  const emit = defineEmits(["update-list"])

  const props = defineProps<{
    noticia: INoticiaSimplificada
    isRascunho: boolean
  }>()

  const dataFormatada = ref("")
  const dataOperacaoFormatada = ref("")
  const tagsFormatadas = ref("")

  const confirmDelete = () => {
	const modalDOM: any = document.querySelector('#confirmNewsModal')

    confirmStore.setConfirmInstance(Modal.getOrCreateInstance(modalDOM))
    confirmStore.showConfirmModal(
      "Tem certeza que desesja remover esta notícia?",
      props.noticia.id
    )
  }

  const confirmDeleteRascunho = () => {
	const modalDOM: any = document.querySelector('#confirmNewsModal')

    confirmStore.setConfirmInstance(Modal.getOrCreateInstance(modalDOM))
    confirmStore.showConfirmModal(
      "Tem certeza que desesja remover este rascunho?",
      null
    )
  }

  const confirmado = async () => {
    confirmStore.closeConfirm()

    if (confirmStore.options.parameter != null) {
      await noticiaStore.deleteNews(confirmStore.options.parameter as number)

      const res = noticiaStore.response.getResponse()
      if (res.code === 200) {
        emit("update-list")
        modalStore.showSuccessModal("Notícia removida com sucesso!")
      } else {
        modalStore.showErrorModal("Erro ao remover notícia!")
      }
    } else {
      localStorage.removeItem("noticiaRascunho")
      emit("update-list")
      modalStore.showSuccessModal("Rascunho removido com sucesso!")
    }
  }

  const loadFormComRascunho = () => {
    noticiaStore.loadRascunho = true
    router.push({
      name: "GerenciaNoticia"
    })
  }

  onMounted(() => {
    dataFormatada.value = brDateString(props.noticia.dataPublicacao.toString())
    if (props.noticia.dataOperacao)
      dataOperacaoFormatada.value = brDateString(props.noticia.dataOperacao)

    const prependHashtag = props.noticia.tags.map((tag) => "#" + tag.descricao)
    tagsFormatadas.value = prependHashtag.join("  ")
  })

  onUpdated(() => {
    dataFormatada.value = brDateString(props.noticia.dataPublicacao.toString())

    if (props.noticia.dataOperacao)
      dataOperacaoFormatada.value = brDateString(props.noticia.dataOperacao)
    const prependHashtag = props.noticia.tags.map((tag) => "#" + tag.descricao)
    tagsFormatadas.value = prependHashtag.join("  ")
  })
</script>

<style scoped>
  .image-icon-button {
    cursor: pointer;
  }

  .image-limiter {
    width: 100%;
    height: 300px;
  }

  .rascunho-img {
    height: 300px;
  }

  .mascara-rascunho {
    display: flex;
    background-color: black;
    opacity: 20%;
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
