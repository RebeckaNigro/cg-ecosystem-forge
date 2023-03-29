<template>
  <div>
    <div
      class="container-fluid box p-3 my-3 card-alignment h-100 overflow-hidden"
    >
      <div v-if="!props.isRascunho">
        <img
          :src="'data:image/png;base64, ' + evento.arquivo"
          alt="event-image"
          class="w-100"
        />
      </div>
      <div v-else>
        <div class="container-mascara-rascunho">
          <div class="mascara-rascunho">
            <img
              src="./../../../../assets/icons/news.svg"
              alt="event-image"
              class="w-50"
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
              src="../../../../../public/view_icon.svg"
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
              src="../../../../../public/edit_icon.svg"
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
              src="../../../../../public/delete_icon.svg"
              alt=""
              class="image-icon-button"
              @click="confirmDelete"
            />
          </div>

          <div class="col-md-4 text-end" v-else>
            <img
              src="../../../../../public/edit_icon.svg"
              alt=""
              class="image-icon-button"
              @click="loadFormComRascunho"
            />
            <img
              src="../../../../../public/delete_icon.svg"
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
            {{ props.evento.local }}
          </div>
        </div>
      </div>

      <div class="footer-atualizado">
        <span class="text-secondary px-1">Atualizado em: </span>
      </div>
    </div>
    <ConfirmModal
      v-show="confirmStore.visible"
      @confirm-true="confirmado"
      element-id="confirmModal"
      id="confirmModal"
    />
  </div>
</template>

<script setup lang="ts">
  import { useEventoStore } from "../../../../stores/eventos/store"
  import { useModalStore } from "../../../../stores/modal/store"
  import { useConfirmStore } from "../../../../stores/confirm/store"
  import { brDateString } from "./../../../../utils/formatacao/datetime"
  import { ref, onMounted, onUpdated } from "vue"
  import ConfirmModal from "../../../general/ConfirmModal.vue"
  import router from "../../../../router"
  import { IEventoSimplificado } from "../../../../stores/eventos/types"

  const eventoStore = useEventoStore()
  const modalStore = useModalStore()
  const confirmStore = useConfirmStore()

  const emit = defineEmits(["update-list"])

  const props = defineProps<{
    evento: IEventoSimplificado
    isRascunho: boolean
  }>()

  const dataFormatada = ref("")
  const tagsFormatadas = ref("")

  const confirmDelete = () => {
    confirmStore.showConfirmModal(
      "Tem certeza que desesja remover este evento?",
      props.evento.id
    )
  }

  const confirmDeleteRascunho = () => {
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

    const prependHashtag = props.evento.tags.map((tag) => "#" + tag.descricao)
    tagsFormatadas.value = prependHashtag.join("  ")
  })

  onUpdated(() => {
    dataFormatada.value = ` ${brDateString(
      props.evento.dataInicio.toString()
    )} -  ${brDateString(props.evento.dataTermino.toString())}`
    const prependHashtag = props.evento.tags.map((tag) => "#" + tag.descricao)
    tagsFormatadas.value = prependHashtag.join("  ")
  })
</script>

<style scoped>
  .card-alignment {
    position: relative;
  }

  .footer-atualizado {
    text-align: left;
    position: absolute;
    bottom: 15px;
  }

  .image-icon-button {
    cursor: pointer;
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

  .icone-usuario {
    width: 30px;
  }
</style>
