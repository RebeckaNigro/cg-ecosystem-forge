<template>
  <div>
    <div class="container-fluid box p-3 my-3 h-100 overflow-hidden">
      <div v-if="!props.isRascunho">
        <img
          :src="
            evento.arquivo
              ? 'data:image/png;base64, ' + evento.arquivo
              : '/public/eventos/eventoExpandido/default-event-cover.svg'
          "
          alt="event-image"
          class="image-limiter"
        />
      </div>
      <div v-else>
        <div class="container-mascara-rascunho">
          <div class="mascara-rascunho">
            <img
              src="/public/eventos/eventoExpandido/default-event-cover.svg"
              alt="event-image"
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

      <div class="container px-0 py-1">
        <div
          class="row mt-2 mx-2 justify-content-between align-items-center d-flex"
        >
          <div class="col-md-8 text-start p-0">
            <span class="text-secondary fs-6">{{ tagsFormatadas }}</span>
          </div>

          <div class="col-md-4 text-end p-0" v-if="!props.isRascunho">
            <img
              src="/public/view_icon.svg"
              alt=""
              class="image-icon-button"
              @click="
                $router.push({
                  name: 'EventoExpandido',
                  params: { eventoId: props.evento.id }
                })
              "
            />
            <img
              src="/public/edit_icon.svg"
              alt=""
              class="image-icon-button"
              @click="
                $router.push({
                  name: 'GerenciaEvento',
                  params: { eventoId: evento.id }
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
        {{ evento.titulo }}
      </p>

      <div>
        <div class="d-flex mt-2 mb-2">
          <img
            src="./../../../../assets/icons/calendar-icon.svg"
            alt="icone pino de endereço"
            class="icone-usuario"
          />
          <div class="d-flex align-items-center mx-2 fw-bold">
            {{ dataFormatada }}
          </div>
        </div>

        <div class="d-flex mt-2 mb-2">
          <img
            src="./../../../../assets/icons/pin-icon.svg"
            alt="icone pino de endereço"
            class="icone-usuario"
          />
          <div class="d-flex align-items-center mx-2 fw-bold">
            {{ evento.local }}
          </div>
        </div>
      </div>

      <div class="text-start mt-4">
        <span class="text-secondary px-1" v-if="evento.ultimaAtualizacao"
          >Atualizado em: {{ dataAtualizacaoFormatada }}</span
        >
      </div>
    </div>
    <ConfirmModal
      v-show="confirmStore.visible"
      @confirm-true="confirmado"
      element-id="confirmEventModal"
      id="confirmEventModal"
    />
  </div>
</template>

<script setup lang="ts">
  import { useEventoStore } from "../../../../stores/eventos/store"
  import { useModalStore } from "../../../../stores/modal/store"
  import { useConfirmStore } from "../../../../stores/confirm/store"
  import { brDateString, friendlyDateTime } from "./../../../../utils/formatacao/datetime"
  import { ref, onMounted, onUpdated } from "vue"
  import ConfirmModal from "../../../general/ConfirmModal.vue"
  import router from "../../../../router"
  import { IEventoSimplificado } from "../../../../stores/eventos/types"
import { Modal } from "bootstrap"

  const eventoStore = useEventoStore()
  const modalStore = useModalStore()
  const confirmStore = useConfirmStore()

  const emit = defineEmits(["update-list"])

  const props = defineProps<{
    evento: IEventoSimplificado
    isRascunho: boolean
  }>()

  const dataFormatada = ref("")
  const dataAtualizacaoFormatada = ref("")
  const tagsFormatadas = ref("")

  const confirmDelete = () => {
	const modalDOM: any = document.querySelector('#confirmEventModal')

  	confirmStore.setConfirmInstance(Modal.getOrCreateInstance(modalDOM))
    confirmStore.showConfirmModal(
      "Tem certeza que desesja remover este evento?",
      props.evento.id
    )
  }

  const confirmDeleteRascunho = () => {
	const modalDOM: any = document.querySelector('#confirmEventModal')

   	confirmStore.setConfirmInstance(modalDOM)
    confirmStore.showConfirmModal(
      "Tem certeza que desesja remover este rascunho?",
      null
    )
  }

  const confirmado = async () => {
    confirmStore.closeConfirm()

    if (confirmStore.options.parameter != null) {
      await eventoStore.deleteEvent(confirmStore.options.parameter as number)

      const res = eventoStore.response.getResponse()
      if (res.code === 200) {
        emit("update-list")
        modalStore.showSuccessModal("Evento removido com sucesso!")
      } else {
        modalStore.showErrorModal("Erro ao remover evento!")
      }
    } else {
      localStorage.removeItem("eventoRascunho")
      emit("update-list")
      modalStore.showSuccessModal("Rascunho removido com sucesso!")
    }
  }

  const loadFormComRascunho = () => {
    eventoStore.loadRascunho = true
    router.push({
      name: "GerenciaEvento"
    })
  }

  onMounted(() => {
    dataFormatada.value = ` ${brDateString(
      props.evento.dataInicio.toString()
    )} -  ${brDateString(props.evento.dataTermino.toString())}`

    if (props.evento.ultimaAtualizacao) {
      dataAtualizacaoFormatada.value = ` ${friendlyDateTime(
        props.evento.ultimaAtualizacao
      )} `
    }

    const prependHashtag = props.evento.tags.map((tag) => "#" + tag.descricao)
    tagsFormatadas.value = prependHashtag.join("  ")
  })

  onUpdated(() => {
    dataFormatada.value = ` ${brDateString(
      props.evento.dataInicio.toString()
    )} -  ${brDateString(props.evento.dataTermino.toString())}`

    if (props.evento.ultimaAtualizacao) {
      dataAtualizacaoFormatada.value = ` ${friendlyDateTime(
        props.evento.ultimaAtualizacao
      )} `
    }
    const prependHashtag = props.evento.tags.map((tag) => "#" + tag.descricao)
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

  .mascara-rascunho {
    display: flex;
    background-color: black;
    opacity: 60%;
    justify-content: center;
    align-items: center;
  }

  .rascunho-img {
    height: 300px;
  }

  .container-mascara-rascunho {
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .mascara-rascunho-text {
    position: absolute;
  }

  .icone-usuario {
    width: 30px;
  }
</style>
