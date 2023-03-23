<template>
  <h1 class="dark-title mt-5 mb-5 fs-2 text-center">Envie sua notícia!</h1>
  <form class="mx-auto card-position box p-5 mb-5" v-if="!isVisualizacao">
    <div class="mb-3">
      <label for="titulo" class="form-label-primary">Título*</label>
      <input
        type="text"
        id="titulo"
        class="form-input-primary"
        :class="v$.titulo.$error ? 'is-invalid' : ''"
        v-model="noticia.titulo"
      />
      <div v-if="v$.titulo.$error" class="invalid-feedback">
        {{ v$.titulo.$errors[0].$message }}
      </div>
    </div>

    <div class="mb-3">
      <label for="subtitulo" class="form-label-primary">Subtitulo</label>
      <input
        type="text"
        id="subtitulo"
        class="form-input-primary"
        :class="v$.subTitulo.$error ? 'is-invalid' : ''"
        v-model="noticia.subTitulo"
      />
      <div v-if="v$.subTitulo.$error" class="invalid-feedback">
        {{ v$.subTitulo.$errors[0].$message }}
      </div>
    </div>

    <div class="mb-3">
      <label for="arquivo" class="form-label-primary">Imagem de capa*</label>
      <div class="imagem-divulgacao d-flex">
        <div>
          <label
            for="imagem-input"
            class="borda-cinza"
            :class="v$.arquivo.$error ? 'error-border' : ''"
          />
          <input
            class="form-input-primary"
            :class="v$.arquivo.$error ? 'is-invalid' : ''"
            type="file"
            name="imagem-input"
            accept="image/png, image/jpg,image/jpeg"
            id="imagem-input"
            @change="(e) => onFileChanged(e)"
          />
          <div v-if="v$.arquivo.$error" class="invalid-feedback">
            {{ v$.arquivo.$errors[0].$message }}
          </div>
        </div>
        <span id="nome-imagem" class="form-label-primary">{{ fileName }}</span>
      </div>
    </div>

    <!-- <div class="mb-3">
      <input
        type="text"
        id="tags"
        class="form-input-primary"
        :class="v$.tags.$error ? 'is-invalid' : ''"
        v-model="newTag"
        @keyup.enter="addTag"
      />
    </div> -->

    <div class="mb-3">
      <label for="tags" class="form-label-primary">Tags</label>
      <AutocompleteComponent
        type="NewsTag"
        :items="allTags"
        @selected-value="addTag"
      />
    </div>

    <div class="container-fluid text-start">
      <div
        class="tag-element mx-1 mb-4"
        v-for="(tag, index) in noticia.tags"
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

    <label for="data" class="form-label-primary mb-2">PUBLICADA EM:</label>

    <div class="row mb-3">
      <div class="col-sm-4">
        <input
          type="date"
          id="data"
          class="form-input-primary"
          :class="v$.dataPublicacao.$error ? 'is-invalid' : ''"
          v-model="noticia.dataPublicacao"
        />
        <div v-if="v$.dataPublicacao.$error" class="invalid-feedback">
          {{ v$.dataPublicacao.$errors[0].$message }}
        </div>
      </div>
      <div class="col-sm-1 d-flex justify-content-center align-items-center">
        às
      </div>
      <div class="col-sm-4">
        <input
          type="time"
          id="hora"
          class="form-input-primary"
          :class="v$.hora.$error ? 'is-invalid' : ''"
          v-model="noticia.hora"
        />
        <div v-if="v$.hora.$error" class="invalid-feedback">
          {{ v$.hora.$errors[0].$message }}
        </div>
      </div>
    </div>

    <label for="data" class="form-label-primary mt-5 mb-2"
      >CORPO DO TEXTO*</label
    >

    <div>
      <QuillEditor
        theme="snow"
        :modules="modules"
        :toolbar="customToolbar"
        v-model:content="noticia.descricao"
        contentType="html"
      />

      <div v-if="v$.descricao.$error" class="mx-2 text-danger fs-6">
        {{ v$.descricao.$errors[0].$message }}
      </div>
    </div>

    <div class="text-start my-3">
      <input type="checkbox" id="concorda" class="mx-2" v-model="termosDeUso" />
      <span>Li e concordo com todos os termos de uso.</span>
    </div>

    <div
      v-if="sendingNews"
      class="spinner-border text-success ml-auto mt-2"
      role="status"
    >
      <span class="visually-hidden">Loading...</span>
    </div>

    <div class="row container-fluid">
      <div class="col-sm-4">
        <button
          type="button"
          class="gray-btn-primary button-specific"
          @click="salvarRascunho"
        >
          SALVAR RASCUNHO
        </button>
      </div>

      <div class="col-sm-4">
        <button
          type="button"
          class="green-btn-outlined button-specific"
          @click="visualizar"
        >
          PRÉ-VISUALIZAR
        </button>
      </div>

      <div class="col-sm-4 fs-xs-1">
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
            @click="$router.push({ name: 'Noticias' })"
          >
            Notícias
          </li>
          <li class="breadcrumb-item inactive" aria-current="page">
            Criar Notícia
          </li>
          <li class="breadcrumb-item active" aria-current="page">
            Pré-visualizar
          </li>
        </ol>
      </nav>
    </div>
    <div class="mx-auto card-position box p-5 mb-5">
      <h1 class="text-start">{{ noticia.titulo }}</h1>
      <h5 class="text-start mx-3">{{ noticia.subTitulo }}</h5>
      <div class="d-flex mt-4 mb-2">
        <img
          src="./../../../assets/icons/user-circle.svg"
          alt="icone usuario"
          class="icone-usuario"
        />
        <div class="d-flex align-items-center mx-2 fw-bold">
          {{ authorName }}
        </div>
      </div>
      <div class="text-start mx-2">{{ dataFormatada }}</div>
      <div class="mb-5">
        <img
          :src="base64Image"
          class="w-100 max-image-width"
          alt="capa noticia"
        />
      </div>
      <div
        id="corpo"
        class="content ql-editor"
        v-html="noticia.descricao"
      ></div>
      <div
        v-if="sendingNews"
        class="spinner-border text-success ml-auto mt-2"
        role="status"
      ></div>
      <div class="text-center" v-if="sendingNews">Loading...</div>
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
    element-id="confirmModal"
    id="confirmModal"
  />
</template>

<script setup lang="ts">
  // import BlotFormatter from "quill-blot-formatter"
  // import ImageUploader from "quill-image-uploader"
  import useValidate from "@vuelidate/core"
  import { ref, onMounted } from "vue"
  import { useNoticiaStore } from "../../../stores/noticias/store"
  import { useModalStore } from "../../../stores/modal/store"
  import { NoticiaRascunho, NewsTag } from "../../../stores/noticias/types"
  import { useAlertStore } from "../../../stores/alert/store"
  import { useUserStore } from "../../../stores/user/store"
  import { useConfirmStore } from "../../../stores/confirm/store"
  import { required, helpers } from "@vuelidate/validators"
  import { brDateString } from "../../../utils/formatacao/datetime"
  import { dateAndTimeToDatetime } from "../../../utils/formatacao/datetime"
  import { fileToBase64 } from "../../../utils/image/converter"
  import ConfirmModal from "../../general/ConfirmModal.vue"
  import AutocompleteComponent from "../../general/AutocompleteComponent.vue"

  const noticiaStore = useNoticiaStore()
  const alertStore = useAlertStore()
  const userStore = useUserStore()
  const modalStore = useModalStore()
  const confirmStore = useConfirmStore()

  const sendingNews = ref(false)
  const arquivo = ref<File | null>()
  const authorName = ref<string | null>()
  const termosDeUso = ref(false)
  const dataFormatada = ref<string | null>()
  const base64Image = ref("")

  const noticia = ref({
    id: -1,
    titulo: "",
    descricao: "",
    subTitulo: "",
    tags: [],
    dataPublicacao: "",
    hora: "",
    arquivo: File
  })

  const fileSizeValidation = () => {
    if (!arquivo.value) return false
    return arquivo.value.size < 5 * 1024 * 1024
  }

  const noticiaRules = ref({
    id: -1,
    titulo: {
      required: helpers.withMessage("Título é obrigatório.", required)
    },
    descricao: {
      required: helpers.withMessage("Corpo da notícia é obrigatório.", required)
    },
    subTitulo: "",
    tags: "",
    hora: {
      required: helpers.withMessage("Hora é obrigatória.", required)
    },
    dataPublicacao: {
      required: helpers.withMessage("Data é obrigatória.", required)
    },
    arquivo: {
      fileValidation: helpers.withMessage(
        "Imagem obrigatória. (Deve ter no máximo 5MB)",
        fileSizeValidation
      )
    }
  })

  //MÓDULOS DE UPLOAD DE IMAGEM AO SERVIDOR E RESIZE DE IMAGEM PARA O EDITOR DE TEXTO
  const modules = ref([
    //   {
    //     name: "blotFormatter",
    //     module: BlotFormatter,
    //     options: {}
    //   },
    //   {
    //     name: "imageUploader",
    //     module: ImageUploader,
    //     options: {
    //       upload: (file: File) => {
    //         return new Promise((resolve, reject) => {
    //           const formData = new FormData()
    //           formData.append("image", file)
    //           axios
    //             .post("/upload-image", formData)
    //             .then((res) => {
    //               console.log(res)
    //               resolve(res.data.url)
    //             })
    //             .catch((err) => {
    //               reject("Upload failed")
    //               console.error("Error:", err)
    //             })
    //         })
    //       }
    //     }
    //   }
  ])

  const v$ = useValidate(noticiaRules, noticia)

  const allTags = ref([{ NewsTag }])
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
    if (noticiaStore.loadRascunho) verificaRascunho()
    buscarTags()

    authorName.value = userStore.getUserName
  })

  const confirmado = () => {
    confirmStore.closeConfirm()
    let rascunho = new NoticiaRascunho(noticia.value, termosDeUso.value)
    localStorage.setItem("noticiaRascunho", JSON.stringify(rascunho))

    modalStore.showSuccessModal("Rascunho salvo com sucesso!")
  }

  const buscarTags = () => {
    noticiaStore.getTags()
    allTags.value = noticiaStore.allTags
  }

  const onFileChanged = (event: Event) => {
    const target = event.target as HTMLInputElement
    if (target && target.files) {
      arquivo.value = target.files[0]
      fileName.value = target.files[0].name
    }
  }

  const cadastrar = async () => {
    if (!termosDeUso.value) {
      alertStore.showWarningMessage("Você precisa aceitar os termos de uso!")
    } else {
      noticia.value.dataPublicacao = dateAndTimeToDatetime(
        data.value,
        hora.value
      )

      const formValidation = await v$.value.$validate()
      if (!formValidation) {
        alertStore.showTimeoutErrorMessage(
          "Preencha todos os campos obrigatórios."
        )
        return
      }

      if (new Date(noticia.value.dataPublicacao) > new Date()) {
        console.log("DATA MAIOR")

        alertStore.showTimeoutErrorMessage(
          "Data e hora não podem ser maior que as atuais!"
        )
        return
      }

      noticia.value.arquivo = arquivo.value

      sendingNews.value = true

      await noticiaStore.postNews(noticia.value)
      sendingNews.value = false

      const res = noticiaStore.response.getResponse()
      if (res.code === 200) {
        limparRascunho()
        resetarNoticia()
        modalStore.showSuccessModal("Notícia cadastrada com sucesso!")
      } else if (res.code === 661) {
        console.error(res.message)
      } else {
        modalStore.showErrorModal("Erro ao cadastrar notícia!")
      }
    }
  }

  const visualizar = async () => {
    if (arquivo.value) {
      const resultado = await fileToBase64(arquivo.value)
      base64Image.value = resultado as string
    }

    dataFormatada.value = brDateString(noticia.value.dataPublicacao.toString())
    isVisualizacao.value = true
  }

  const salvarRascunho = () => {
    if (localStorage.getItem("noticiaRascunho")) {
      confirmStore.showConfirmModal(
        "Rascunho já existente, ao prosseguir o rascunho anterior será perdido. Tem certeza?",
        null
      )
    } else {
      noticia.value.dataPublicacao = dateAndTimeToDatetime(
        data.value,
        hora.value
      )
      let rascunho = new NoticiaRascunho(noticia.value, termosDeUso.value)
      localStorage.setItem("noticiaRascunho", JSON.stringify(rascunho))

      modalStore.showSuccessModal("Rascunho salvo com sucesso!")
    }
  }

  const verificaRascunho = () => {
    const rascunhoStr = localStorage.getItem("noticiaRascunho")

    if (rascunhoStr) {
      const rascunhoFinal = JSON.parse(rascunhoStr)
      noticia.value = rascunhoFinal.noticia
      data.value = noticia.value.dataPublicacao.substring(0, 10)
      hora.value = noticia.value.dataPublicacao.substring(
        noticia.value.dataPublicacao.length - 13,
        noticia.value.dataPublicacao.length - 5
      )
      termosDeUso.value = rascunhoFinal.termosDeUso
      alertStore.showTimeoutInfoMessage("Rascunho carregado com sucesso!")
    }
    deleteRascunhoAfter.value = true
    noticiaStore.loadRascunho = false
  }

  const limparRascunho = () => {
    localStorage.removeItem("noticiaRascunho")
  }

  const resetarNoticia = () => {
    noticia.value = {
      id: -1,
      titulo: "",
      descricao: "",
      subTitulo: "",
      tags: [],
      dataPublicacao: Date,
      hora: "",
      arquivo: File
    }
    termosDeUso.value = false
  }

  const deleteTag = (index: number) => {
    noticia.value.tags.splice(index, 1)
  }

  const addTag = (selectedTag: NewsTag) => {
    const tagFound = noticia.value.tags.find((tag: NewsTag) => {
      return tag.descricao == selectedTag.descricao
    })

    if (!tagFound) {
      noticia.value.tags.push(selectedTag)
    }
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
