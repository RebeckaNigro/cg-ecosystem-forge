<template>
  <div class="pt-5 pb-5 container">
    <Spinner spinnerColorClass="text-dark" v-if="isLoadingContent" />
    <div class="evento-breadcrumb">
      <nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li
            class="breadcrumb-item unactive"
            @click="$router.push({ name: 'EventosCriados' })"
          >
            Eventos
          </li>
          <li class="breadcrumb-item unactive" aria-current="page">
            Destaques
          </li>
          <li class="breadcrumb-item active" aria-current="page">
            {{ evento.titulo }}
          </li>
        </ol>
      </nav>
    </div>
    <section class="evento-expandido ghp box pt-3">
      <div class="mb-5">
        <img
          class="w-100"
          :src="
            evento.arquivos[0]
              ? 'data:image/png;base64, ' + evento.arquivos[0].arquivo
              : '/public/eventos/eventoExpandido/default-event-cover.jpeg'
          "
          alt="capa do evento"
        />
      </div>

      <h1 class="text-start">{{ evento.titulo }}</h1>
      <div class="row container-fluid">
        <div class="col-sm-8">
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
                  ? enderecoFormatado
                  : `Online: ${evento.linkExterno}`
              }}
            </div>
          </div>
        </div>
        <div class="d-flex col-sm-4 align-items-end justify-content-end">
          <button type="button" class="green-btn-primary mx-2">
            <a :href="generatedLink" class="link-btn">Acessar Link</a>
          </button>
        </div>
      </div>

      <hr class="w-100 my-4" />

      <div id="corpo" class="content ql-editor" v-html="evento.descricao"></div>
    </section>

    <div class="btn-up-container" @click="handleNavigateUp">
      <button class="btn-up">
        <img src="/public/arrow_up.svg" alt="Seta para cima" />
        Subir ao topo
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
//@ts-nocheck
  import { onMounted, ref } from "vue"
  import router from "../../../router"
  import { useEventoStore } from "../../../stores/eventos/store"
  import { IEvento } from "../../../stores/eventos/types"
  import { brDateString } from "../../../utils/formatacao/datetime"
  import Spinner from "../../../components/general/Spinner.vue"

  const store = useEventoStore()
  const evento = ref<IEvento>({
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
    endereco: null,
    linkExterno: "",
    exibirMaps: false,
    responsavel: "",
    arquivos: []
  })
  const dataInicioFormatada = ref("")
  const dataTerminoFormatada = ref("")
  const dataFormatada = ref("")
  const enderecoFormatado = ref("")
  const generatedLink = ref("")
  const isLoadingContent = ref(false)

  const handleNavigateUp = () => {
    window.scrollTo(0, 0)
  }

  onMounted(async () => {
    isLoadingContent.value = true

    await store.getEventById(
      parseInt(router.currentRoute.value.params.eventoId.toString())
    )

    evento.value = store.response.dado
    isLoadingContent.value = false

    if (
      evento.value.linkExterno.startsWith("http://") ||
      evento.value.linkExterno.startsWith("https://")
    ) {
      generatedLink.value = evento.value.linkExterno
    } else {
      generatedLink.value = `http://${evento.value.linkExterno}`
    }

    dataInicioFormatada.value = brDateString(evento.value.dataInicio.toString())
    dataTerminoFormatada.value = brDateString(
      evento.value.dataTermino.toString()
    )

    dataFormatada.value = `${dataInicioFormatada.value} - ${dataTerminoFormatada.value}`
    enderecoFormatado.value = `${evento.value.endereco.logradouro}, ${evento.value.endereco.numero} - ${evento.value.endereco.bairro}, ${evento.value.endereco.uf}, ${evento.value.endereco.cep}`
  })
</script>

<style scoped lang="scss">
  .link-btn {
    text-decoration: none;
    color: white;
  }
  .btn-up-container {
    margin: 20px auto;
    display: flex;
    justify-content: flex-end;
    max-width: 1300px;

    .btn-up {
      background-color: #fff;
      padding: 0.7rem;
      border: 1px solid #505050;
      border-radius: 10px;
      font-family: "Montserrat-SemiBold", sans-serif;
    }

    .btn-up:hover {
      background-color: rgb(232, 229, 229);
    }
  }

  .icone-usuario {
    width: 30px;
  }
</style>
