<template>
  <h1 class="dark-title mt-5 mb-5 fs-2 text-center" v-if="evento.id < 0">
    CRIE SEU EVENTO!
  </h1>
  <h1 class="dark-title mt-5 mb-5 fs-2 text-center" v-else>
    EDITE SEU EVENTO!
  </h1>
  <form class="mx-auto card-position box p-5 mb-5" v-if="!isVisualizacao">
    <!-- TIPO -->
    <div class="mb-3 text-start">
      <label for="titulo" class="form-label-primary">Tipo</label>
      <div class="my-2 px-3">
        <input
          type="radio"
          name="tipoEvento"
          id="tipoEventoPresencial"
          value="1"
          v-model="evento.tipoEventoId"
        />
        <label
          for="tipoEventoPresencial"
          class="form-label-primary d-inline mx-3"
          >Presencial</label
        >
        <input
          type="radio"
          name="tipoEvento"
          id="tipoEventoOnline"
          value="2"
          v-model="evento.tipoEventoId"
        />
        <label for="tipoEventoOnline" class="form-label-primary d-inline mx-3"
          >Online</label
        >
      </div>
    </div>

    <!-- NOME -->
    <div class="mb-3">
      <label for="titulo" class="form-label-primary">Nome*</label>
      <input
        type="text"
        id="titulo"
        class="form-input-primary"
        :class="v$.titulo.$error ? 'is-invalid' : ''"
        v-model="evento.titulo"
      />
      <div v-if="v$.titulo.$error" class="invalid-feedback">
        {{ v$.titulo.$errors[0].$message }}
      </div>
    </div>

    <!-- RESPONSÁVEL -->
    <div class="mb-3">
      <label for="local" class="form-label-primary">Responsável*</label>
      <input
        type="text"
        id="responsavel"
        class="form-input-primary"
        :class="v$.responsavel.$error ? 'is-invalid' : ''"
        v-model="evento.responsavel"
      />
      <div v-if="v$.responsavel.$error" class="invalid-feedback">
        {{ v$.responsavel.$errors[0].$message }}
      </div>
    </div>

    <!-- INSTITUIÇÕES -->
    <div class="mb-3">
      <label for="tags" class="form-label-primary">Instituição*</label>
      <AutocompleteComponent
        type="Instituicao"
        :items="instituicoes"
        @selected-value="setInstituicao"
        :class="v$.instituicaoId.$error ? 'is-invalid' : ''"
      />
      <div v-if="v$.instituicaoId.$error" class="invalid-feedback">
        {{ v$.instituicaoId.$errors[0].$message }}
      </div>
    </div>

    <div class="container-fluid text-start" v-if="selectedValue">
      <div class="tag-element mx-1 mb-4">
        <span>{{ selectedValue.razaoSocial }}</span>
        <img
          src="./../../../assets/icons/close-white.svg"
          alt="Remover Instituicao"
          @click="deleteSelectedInstituicao"
        />
      </div>
    </div>

    <!-- IMAGEM DE CAPA -->
    <div class="mb-3">
      <label for="arquivo" class="form-label-primary">Imagem de capa*</label>
      <div class="imagem-divulgacao d-flex">
        <div>
          <label for="imagem-input" class="borda-cinza" />
          <input
            class="form-input-primary"
            type="file"
            name="imagem-input"
            accept="image/png, image/jpg,image/jpeg"
            id="imagem-input"
            @change="(e) => onFileChanged(e)"
          />
        </div>
        <span id="nome-imagem" class="form-label-primary">{{ fileName }}</span>
      </div>
    </div>

    <!-- DATAS -->
    <div class="row mb-3">
      <!-- DATA E HORA DE INICIO -->
      <div class="col-sm-6">
        <label for="dataInicio" class="form-label-primary mb-2"
          >DATA DE INÍCIO*:</label
        >
        <div class="row px-2">
          <input
            type="date"
            id="dataInicio"
            class="form-input-primary col-sm-6"
            :class="v$.dataInicio.$error ? 'is-invalid' : ''"
            v-model="evento.dataInicio"
          />
          <div v-if="v$.dataInicio.$error" class="invalid-feedback">
            {{ v$.dataInicio.$errors[0].$message }}
          </div>
          <div
            class="col-sm-1 d-flex justify-content-center align-items-center p-0"
          >
            às
          </div>
          <input
            type="time"
            id="horaInicio"
            class="form-input-primary col-sm-5"
            :class="invalidHoraInicio ? 'is-invalid' : ''"
            v-model="horaInicio"
          />
          <div v-if="invalidHoraInicio" class="invalid-feedback">
            Hora é obrigatória
          </div>
        </div>
      </div>

      <div class="col-sm-6">
        <!-- DATA E HORA DE FIM -->
        <label for="dataFim" class="form-label-primary mb-2"
          >DATA DE TÉRMINO*:</label
        >
        <div class="row px-2">
          <input
            type="date"
            id="dataFim"
            class="form-input-primary col-sm-6"
            :class="v$.dataTermino.$error ? 'is-invalid' : ''"
            v-model="evento.dataTermino"
          />
          <div v-if="v$.dataTermino.$error" class="invalid-feedback">
            {{ v$.dataTermino.$errors[0].$message }}
          </div>
          <div
            class="col-sm-1 d-flex justify-content-center align-items-center p-0"
          >
            às
          </div>
          <input
            type="time"
            id="horaFim"
            class="form-input-primary col-sm-5"
            :class="invalidHoraFim ? 'is-invalid' : ''"
            v-model="horaFim"
          />
          <div v-if="invalidHoraFim" class="invalid-feedback">
            Hora é obrigatória
          </div>
        </div>
      </div>
    </div>

    <!-- TAGS -->
    <div class="mb-3">
      <label for="tags" class="form-label-primary">Tags</label>
      <AutocompleteComponent
        type="CustomTag"
        :items="allTags"
        @selected-value="addTag"
      />
    </div>

    <div class="container-fluid text-start">
      <div
        class="tag-element mx-1 mb-4"
        v-for="(tag, index) in evento.tags"
        :key="index"
      >
        <span>{{ tag.descricao }}</span>
        <img
          src="./../../../assets/icons/close-white.svg"
          alt="Remover tag"
          @click="deleteTag(index)"
        />
      </div>
    </div>

    <!-- DESCRIÇÃO -->
    <label for="data" class="form-label-primary mt-5 mb-2">DESCRIÇÃO*</label>

    <div class="mb-4">
      <QuillEditor
        ref="customEditor"
        theme="snow"
        :toolbar="customToolbar"
        v-model:content="evento.descricao"
        contentType="html"
      />

      <div v-if="v$.descricao.$error" class="mx-2 text-danger fs-6">
        {{ v$.descricao.$errors[0].$message }}
      </div>
    </div>

    <!-- ENDEREÇO -->
    <div class="row">
      <Spinner v-if="gettingAddress" />
      <!-- CEP -->
      <div class="mb-3" v-if="evento.tipoEventoId == 1">
        <div class="text-start">
          <label for="cep" class="form-label-primary d-inline"
            >CEP
            <a
              href="https://buscacepinter.correios.com.br/app/endereco/index.php"
              >(Não sabe o CEP?)</a
            ></label
          >
        </div>

        <input
          type="text"
          id="cep"
          class="form-input-primary"
          v-model="evento.endereco.cep"
          @blur="buscarCEP"
          v-if="evento.tipoEventoId == 1"
        />
      </div>

      <!-- LOGRADOURO -->
      <div class="mb-3" v-if="evento.tipoEventoId == 1">
        <label for="logradouro" class="form-label-primary">Logradouro</label>
        <input
          type="text"
          id="logradouro"
          class="form-input-primary"
          v-model="evento.endereco.logradouro"
        />
      </div>

      <!-- NÚMERO -->
      <div class="mb-3" v-if="evento.tipoEventoId == 1">
        <label for="numero" class="form-label-primary">Número</label>
        <input
          type="text"
          id="numero"
          class="form-input-primary"
          v-model="evento.endereco.numero"
        />
      </div>

      <!-- BAIRRO -->
      <div class="mb-3" v-if="evento.tipoEventoId == 1">
        <label for="bairro" class="form-label-primary">Bairro</label>
        <input
          type="text"
          id="bairro"
          class="form-input-primary"
          v-model="evento.endereco.bairro"
        />
      </div>
    </div>

    <div class="row mb-3" v-if="evento.tipoEventoId == 1">
      <div class="col-sm-4">
        <label for="estado" class="form-label-primary">Estado</label>
        <input
          id="estado"
          class="form-input-primary"
          v-model="evento.endereco.uf"
        />
      </div>
      <div class="col-sm-8">
        <label for="cidade" class="form-label-primary">Cidade</label>
        <input
          type="text"
          id="cidade"
          class="form-input-primary"
          v-model="evento.endereco.cidade"
        />
      </div>
    </div>

    <!-- COMPLEMENTO -->
    <div class="mb-3" v-if="evento.tipoEventoId == 1">
      <label for="complemento" class="form-label-primary">Complemento</label>
      <input
        type="text"
        id="complemento"
        class="form-input-primary"
        v-model="evento.endereco.complemento"
      />
    </div>

    <!-- LOCAL DO EVENTO -->
    <div class="mb-3">
      <label for="local" class="form-label-primary">Nome do Local</label>
      <input
        type="text"
        id="local"
        class="form-input-primary"
        v-model="evento.local"
      />
    </div>

    <!-- LINK -->
    <div class="mb-3">
      <label for="link" class="form-label-primary"
        >Link para mais informações</label
      >
      <input
        type="text"
        id="link"
        class="form-input-primary"
        v-model="evento.linkExterno"
      />
    </div>

    <!-- LOADING SPINNER -->
    <Spinner v-if="sendingEvent" />

    <!-- PREVIEW -->
    <div class="row container-fluid">
      <div class="col-sm-4" v-if="!editFlag">
        <button
          type="button"
          class="gray-btn-primary button-specific"
          @click="salvarRascunho"
        >
          SALVAR RASCUNHO
        </button>
      </div>

      <div :class="!editFlag ? 'col-sm-4' : 'col-sm-6'">
        <button
          type="button"
          class="green-btn-outlined button-specific"
          @click="visualizar"
        >
          PRÉ-VISUALIZAR
        </button>
      </div>

      <div :class="!editFlag ? 'col-sm-4' : 'col-sm-6'" class="fs-xs-1">
        <button
          type="button"
          class="green-btn-primary button-specific"
          @click="cadastrar"
        >
          ENVIAR
        </button>
      </div>
    </div>
  </form>

  <div v-else>
    <div class="breadcrumb-container">
      <nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li
            class="breadcrumb-item unactive"
            @click="$router.push({ name: 'EventosCriados' })"
          >
            Eventos
          </li>
          <li class="breadcrumb-item inactive" aria-current="page">
            Criar Evento
          </li>
          <li class="breadcrumb-item active" aria-current="page">
            Pré-visualizar
          </li>
        </ol>
      </nav>
    </div>

    <div class="mx-auto card-position box p-5 mb-5">
      <div class="mb-5">
        <img
          :src="base64Image"
          class="w-100 max-image-width"
          alt="capa evento"
        />
      </div>

      <h1 class="text-start">{{ evento.titulo }}</h1>
      <div class="d-flex mt-4 mb-2">
        <img
          src="./../../../assets/icons/user-circle.svg"
          alt="icone usuario"
          class="icone-usuario"
        />
        <div class="d-flex align-items-center mx-2 fw-bold">
          {{ evento.responsavel }}
        </div>
      </div>

      <div class="d-flex mt-4 mb-2">
        <img
          src="./../../../assets/icons/calendar-icon.svg"
          alt="icone pino de endereço"
          class="icone-usuario"
        />
        <div class="d-flex align-items-center mx-2 fw-bold">
          {{ dataFormatada }}
        </div>
      </div>

      <div class="d-flex mt-4 mb-2">
        <img
          src="./../../../assets/icons/pin-icon.svg"
          alt="icone pino de endereço"
          class="icone-usuario"
        />
        <div class="d-flex align-items-center mx-2 fw-bold">
          {{
            evento.tipoEventoId == 1
              ? `${evento.local} - ${enderecoFormatado}`
              : evento.linkExterno
          }}
        </div>
      </div>

      <hr class="w-100 my-4" />

      <div id="corpo" class="content ql-editor" v-html="evento.descricao"></div>

      <Spinner v-if="sendingEvent" />

      <div class="row container-fluid justify-content-center my-5">
        <button
          type="button"
          class="green-btn-outlined col-sm-5 mx-2"
          @click="isVisualizacao = false"
        >
          Voltar
        </button>
        <button
          type="button"
          class="green-btn-primary col-sm-5 mx-2"
          @click="cadastrar"
        >
          ENVIAR
        </button>
      </div>
    </div>
  </div>
  <ConfirmModal
    v-show="confirmStore.visible"
    @confirm-true="confirmado"
    element-id="confirmFormModal"
    id="confirmFormModal"
  />
</template>

<script setup lang="ts">
// @ts-nocheck
  // import BlotFormatter from "quill-blot-formatter"
  // import ImageUploader from "quill-image-uploader"

  import useValidate from "@vuelidate/core"
  import { ref, onMounted } from "vue"
  import { useRoute } from "vue-router"
  import { useEventoStore } from "../../../stores/eventos/store"
  import { useModalStore } from "../../../stores/modal/store"
  import { useAlertStore } from "../../../stores/alert/store"
  import { useUserStore } from "../../../stores/user/store"
  import { useConfirmStore } from "../../../stores/confirm/store"
  import { useTagStore } from "../../../stores/tag/store"
  import { useInstituicaoStore } from "../../../stores/instituicao/store"
  import { required, helpers, minValue } from "@vuelidate/validators"
  import { getFromCEP } from "../../../utils/http"
  import { brDateString } from "../../../utils/formatacao/datetime"
  import { dateAndTimeToDatetime } from "../../../utils/formatacao/datetime"
  import { fileToBase64 } from "../../../utils/image/converter"
  import ConfirmModal from "../../general/ConfirmModal.vue"
  import AutocompleteComponent from "../../general/AutocompleteComponent.vue"
  import Spinner from "../../general/Spinner.vue"
  import { CustomTag } from "../../../stores/tag/types"
  import {
    Evento,
    EventoRascunho,
    NovoEndereco
  } from "../../../stores/eventos/types"
  import { Instituicao } from "../../../stores/instituicao/types"
  import { end } from "@popperjs/core"
import { Modal } from "bootstrap"
  
  const eventoStore = useEventoStore()
  const alertStore = useAlertStore()
  const userStore = useUserStore()
  const modalStore = useModalStore()
  const instituicaoStore = useInstituicaoStore()
  const confirmStore = useConfirmStore()
  const tagStore = useTagStore()
  const route = useRoute()

  const customEditor = ref()
  const instituicoes = ref<Array<Instituicao>>()
  const selectedValue = ref<Instituicao>()
  const sendingEvent = ref(false)
  const gettingAddress = ref(false)
  const deleteRascunhoAfter = ref(false)
  const arquivo = ref<File | null>()
  const authorName = ref<string | null>()
  const invalidHoraInicio = ref(false)
  const invalidHoraFim = ref(false)
  const editFlag = ref(false)
  const enderecoFormatado = ref("")
  const dataFormatada = ref<string | null>()
  const dataInicioFormatada = ref<string | null>()
  const dataTerminoFormatada = ref<string | null>()
  const horaInicio = ref("")
  const horaFim = ref("")
  const base64Image = ref("")

  const evento = ref({
    id: -1,
    instituicaoId: -1,
    tags: new Array<CustomTag>(),
    tipoEventoId: 1,
    titulo: "",
    descricao: "",
    dataInicio: "",
    dataTermino: "",
    local: "",
    enderecoId: null,
    endereco: new NovoEndereco(-1, "", "", "", "", "", "", "", ""),
    linkExterno: "",
    exibirMaps: false,
    responsavel: "",
    arquivo: File
  })

  const eventoRules = ref({
    id: -1,
    titulo: {
      required: helpers.withMessage("Nome é obrigatório.", required)
    },
    descricao: {
      required: helpers.withMessage(
        "Descrição do evento obrigatória.",
        required
      )
    },
    instituicaoId: {
      minValueValue: helpers.withMessage(
        "Instituição é obrigatória.",
        minValue(0)
      )
    },

    dataInicio: {
      required: helpers.withMessage("Data de início é obrigatória.", required)
    },
    dataTermino: {
      required: helpers.withMessage("Data de término é obrigatória.", required)
    },
    responsavel: {
      required: helpers.withMessage(
        "Nome do responsável é obrigatório.",
        required
      )
    }
  })

  const v$ = useValidate(eventoRules, evento)

  const allTags = ref<CustomTag[]>()
  const isVisualizacao = ref(false)
  const fileName = ref("")
  const customToolbar = ref([
    [{ font: [] }],
    [{ header: [false, 1, 2, 3, 4, 5, 6] }],
    [{ size: ["small", false, "large", "huge"] }],
    ["bold", "italic", "underline", "strike"],
    [
      { align: "" },
      { align: "center" },
      { align: "right" },
      { align: "justify" }
    ],
    [{ header: 1 }, { header: 2 }],
    ["blockquote", "code-block"],
    [{ list: "ordered" }, { list: "bullet" }, { list: "check" }],
    [{ script: "sub" }, { script: "super" }],
    [{ indent: "-1" }, { indent: "+1" }],
    [{ color: [] }, { background: [] }],
    ["link", "video", "formula"],
    [{ direction: "rtl" }],
    ["clean"]
  ])

  onMounted(async () => {
    const id = route.params.eventoId

    await buscarInstituicoes()
    await buscarTags()

    if (id) {
      editFlag.value = true
      await eventoStore.getEventById(Number(id))

      if (eventoStore.response.code == 200) {
        evento.value = eventoStore.response.dado
        horaInicio.value = evento.value.dataInicio.substring(
          evento.value.dataInicio.length - 8,
          evento.value.dataInicio.length
        )
        horaFim.value = evento.value.dataTermino.substring(
          evento.value.dataTermino.length - 8,
          evento.value.dataTermino.length
        )

        evento.value.dataInicio = evento.value.dataInicio.substring(0, 10)
        evento.value.dataTermino = evento.value.dataTermino.substring(0, 10)

        selectedValue.value = instituicoes.value?.find(
          (item) => item.id == evento.value.instituicaoId
        )
      } else {
        alertStore.showTimeoutErrorMessage("Erro ao carregar evento!")
      }
    }

    if (eventoStore.loadRascunho) verificaRascunho()

    authorName.value = userStore.getUserName
  })

  const confirmado = () => {
    confirmStore.closeConfirm()
    let rascunho = new EventoRascunho(
      evento.value,
      selectedValue.value,
      horaInicio.value,
      horaFim.value
    )
    localStorage.setItem("eventoRascunho", JSON.stringify(rascunho))

    modalStore.showSuccessModal("Rascunho salvo com sucesso!")
  }

  const buscarTags = async () => {
    await tagStore.getTags()
    allTags.value = tagStore.allTags
  }

  const buscarInstituicoes = async () => {
    await instituicaoStore.getInstituicoes()

    if (instituicaoStore.response.code == 200) {
      instituicoes.value = instituicaoStore.allInstituicoes
    } else {
      alertStore.showTimeoutErrorMessage("Erro ao carregar instituições!")
    }
  }

  const fileSizeValidation = () => {
    if (!arquivo.value) return false

    return arquivo.value.size < 5 * 1024 * 1024
  }

  const onFileChanged = (event: Event) => {
    const target = event.target as HTMLInputElement
    if (target && target.files) {
      arquivo.value = target.files[0]
      fileName.value = target.files[0].name
    }

    if (!fileSizeValidation()) {
      alertStore.showTimeoutWarningMessage("Imagem deve ter no máximo 5MB")
      arquivo.value = null
      fileName.value = ""
      return
    }
  }

  const cadastrar = async () => {
    const formValidation = await v$.value.$validate()
    if (!horaInicio.value) invalidHoraInicio.value = true
    else invalidHoraInicio.value = false

    if (!horaFim.value) invalidHoraFim.value = true
    else invalidHoraFim.value = false

    if (!formValidation || invalidHoraInicio.value || invalidHoraFim.value) {
      alertStore.showTimeoutErrorMessage(
        "Preencha todos os campos obrigatórios."
      )
      return
    }

    if (
      evento.value.dataInicio.length <= 10 &&
      evento.value.dataTermino.length <= 10
    ) {
      const dataInicioDateTime = dateAndTimeToDatetime(
        evento.value.dataInicio,
        horaInicio.value
      )
      const dataTerminoDateTime = dateAndTimeToDatetime(
        evento.value.dataTermino,
        horaFim.value
      )
      if (new Date(dataInicioDateTime) < new Date()) {
        alertStore.showTimeoutErrorMessage(
          "Data de início do evento já passou!"
        )
        return
      }

      if (new Date(dataInicioDateTime) > new Date(dataTerminoDateTime)) {
        alertStore.showTimeoutErrorMessage(
          "Data de início não pode ser maior que a data de término!"
        )
        return
      }

      evento.value.dataInicio = dataInicioDateTime
      evento.value.dataTermino = dataTerminoDateTime
    } else {
      if (new Date(evento.value.dataInicio) < new Date()) {
        alertStore.showTimeoutErrorMessage(
          "Data de início do evento já passou!"
        )
        return
      }

      if (
        new Date(evento.value.dataInicio) > new Date(evento.value.dataTermino)
      ) {
        alertStore.showTimeoutErrorMessage(
          "Data de início não pode ser maior que a data de término!"
        )
        return
      }
    }

    evento.value.arquivo = arquivo.value

    if (evento.value.tipoEventoId == 2) evento.value.endereco = null

    console.log(`ENVIANDO A REQUISIÇÃO ASSIM: `)
    console.dir(evento.value)

    if (evento.value.id > 0) {
      sendingEvent.value = true

      await eventoStore.putEvent(evento.value)
      sendingEvent.value = false

      const res = eventoStore.response.getResponse()
      if (res.code === 200) {
        if (deleteRascunhoAfter.value) limparRascunho()
        resetarEvento()
        modalStore.showSuccessModal("Evento editado com sucesso!")
      } else if (res.code === 661) {
        console.error(res.message)
      } else {
        modalStore.showErrorModal("Erro ao editar evento!")
      }
    } else {
      sendingEvent.value = true

      await eventoStore.postEvent(evento.value)
      sendingEvent.value = false

      const res = eventoStore.response.getResponse()
      //console.dir(res)
      if (res.code === 200) {
        if (deleteRascunhoAfter.value) limparRascunho()
        resetarEvento()
        modalStore.showSuccessModal("Evento cadastrado com sucesso!")
      } else if (res.code === 661) {
        console.error(res.message)
      } else {
        modalStore.showErrorModal("Erro ao cadastrar evento!")
      }
    }
  }

  const visualizar = async () => {
    if (arquivo.value) {
      const resultado = await fileToBase64(arquivo.value)
      base64Image.value = resultado as string
    }

    dataInicioFormatada.value = brDateString(evento.value.dataInicio.toString())
    dataTerminoFormatada.value = brDateString(
      evento.value.dataTermino.toString()
    )

    dataFormatada.value = `${dataInicioFormatada.value} - ${dataTerminoFormatada.value}`
    enderecoFormatado.value = `${evento.value.endereco.logradouro}, ${evento.value.endereco.numero} - ${evento.value.endereco.bairro}, ${evento.value.endereco.uf}, ${evento.value.endereco.cep}`
    isVisualizacao.value = true
  }

  const salvarRascunho = () => {
    if (localStorage.getItem("eventoRascunho")) {
	  const modalDOM: any = document.querySelector('#confirmFormModal')
	  confirmStore.setConfirmInstance(Modal.getOrCreateInstance(modalDOM))
      confirmStore.showConfirmModal(
        "Rascunho já existente, ao prosseguir o rascunho anterior será perdido. Tem certeza?",
        null
      )
    } else {
      evento.value.dataInicio = dateAndTimeToDatetime(
        evento.value.dataInicio,
        horaInicio.value
      )
      evento.value.dataTermino = dateAndTimeToDatetime(
        evento.value.dataTermino,
        horaFim.value
      )
      let rascunho = new EventoRascunho(
        evento.value,
        selectedValue.value,
        horaInicio.value,
        horaFim.value
      )
      localStorage.setItem("eventoRascunho", JSON.stringify(rascunho))

      modalStore.showSuccessModal("Rascunho salvo com sucesso!")
    }
  }

  const verificaRascunho = () => {
    const rascunhoStr = localStorage.getItem("eventoRascunho")

    if (rascunhoStr) {
      const rascunhoFinal = JSON.parse(rascunhoStr)
      console.dir(rascunhoFinal)
      evento.value = rascunhoFinal.evento
      selectedValue.value = rascunhoFinal.instituicao
      evento.value.dataInicio = evento.value.dataInicio.substring(0, 10)
      evento.value.dataTermino = evento.value.dataTermino.substring(0, 10)
      horaInicio.value = rascunhoFinal.horaInicio
      horaFim.value = rascunhoFinal.horaTermino
      alertStore.showTimeoutInfoMessage("Rascunho carregado com sucesso!")
    }
    deleteRascunhoAfter.value = true
    eventoStore.loadRascunho = false
  }

  const limparRascunho = () => {
    localStorage.removeItem("eventoRascunho")
  }

  const resetarEvento = () => {
    evento.value = {
      id: -1,
      instituicaoId: -1,
      tags: [],
      tipoEventoId: 1,
      titulo: "",
      descricao: "",
      dataInicio: "",
      dataTermino: "",
      local: "",
      enderecoId: null,
      endereco: new NovoEndereco(-1, "", "", "", "", "", "", "", ""),
      linkExterno: "",
      exibirMaps: false,
      responsavel: "",
      arquivo: File
    }
    selectedValue.value = null
    customEditor.value.setHTML("")
  }

  const buscarCEP = async () => {
    if (evento.value.endereco.cep) {
      gettingAddress.value = true
      try {
        const enderecoCompleto = await getFromCEP(evento.value.endereco.cep)

        if (enderecoCompleto.errors) {
          alertStore.showTimeoutErrorMessage("Erro ao buscar CEP!")
        } else {
          evento.value.endereco.cidade = enderecoCompleto.city
          evento.value.endereco.uf = enderecoCompleto.state
          evento.value.endereco.bairro = enderecoCompleto.neighborhood
          evento.value.endereco.logradouro = enderecoCompleto.street
        }
      } catch (error) {
        console.log(error)
        alertStore.showTimeoutErrorMessage(
          "CEP Incorreto, digite um valor válido."
        )
      }
      gettingAddress.value = false
    }
  }

  const deleteTag = (index: number) => {
    evento.value.tags.splice(index, 1)
  }

  const addTag = (selectedTag: CustomTag) => {
    const tagFound = evento.value.tags.find((tag: CustomTag) => {
      return tag.descricao == selectedTag.descricao
    })

    if (!tagFound) {
      evento.value.tags.push(selectedTag)
    }
  }

  const setInstituicao = (selectedInstituicao: Instituicao) => {
    selectedValue.value = selectedInstituicao
    if (selectedInstituicao) evento.value.instituicaoId = selectedInstituicao.id
  }

  const deleteSelectedInstituicao = () => {
    selectedValue.value = undefined
    evento.value.instituicaoId = -1
  }
</script>

<style scoped>
  .button-specific {
    width: 100%;
    margin: 2rem 1rem;
    box-sizing: border-box;
  }

  .align-correction {
    margin-bottom: 0.2rem;
  }

  .icone-usuario {
    width: 30px;
  }

  .imagem-divulgacao div {
    margin-top: 0;
    width: fit-content;
  }
  .imagem-divulgacao label {
    height: 200px;
    width: 100%;
    max-width: 400px;
    margin-left: 0;
    background-color: #fff;
    background-image: url("/user/eventos/cloud_icon.svg");
    background-repeat: no-repeat;
    background-position: center;
  }

  input#imagem-input {
    height: 0;
    padding: 0;
    border: 0;
  }

  span#nome-imagem {
    align-self: center;
    text-align: center;
  }

  .max-image-width {
    max-width: 400px;
  }
</style>
