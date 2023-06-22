<template>
  <div class="pt-5 pb-5 container">
    <Spinner spinnerColorClass="text-dark" v-if="isLoadingContent" />
    <div class="noticia-breadcrumb">
      <nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li
            class="breadcrumb-item unactive"
          >
            Notícias
          </li>
          <li class="breadcrumb-item unactive" aria-current="page">
            Destaques
          </li>
          <li class="breadcrumb-item active" aria-current="page">
            {{ noticia.titulo }}
          </li>
        </ol>
      </nav>
    </div>
    <section class="noticia-expandida ghp box pt-3">
      <header class="d-flex">
        <h1>{{ noticia.titulo }}</h1>
        <p>{{ noticia.subTitulo }}</p>
        <!--<p class="nome-autor">Nome do autor</p>-->
        <time>{{ friendlyDateTime(noticia.dataPublicacao) }}</time>
      </header>
      <img
        class="w-100"
        :src="
          noticia.arquivo
            ? 'data:image/png;base64, ' + noticia.arquivo
            : '/public/noticias/noticia-expandida/default-news-cover.svg'
        "
        alt="capa da notícia"
      />
      <p id="first-paraghap">
        {{ noticia.subTitulo }}
      </p>
      <main>
        <article
          class="news-body ql-editor"
          v-html="noticia.descricao"
        ></article>

        <!--<img src="/noticias/noticia-expandida/fakevideo.png" alt="fake-video" class="w-100">-->
      </main>
      <!--<NoticiasRelacionadas />-->
    </section>

    <div class="btn-up-container" @click="handleNavigateUp">
      <button class="btn-up">
        <img
          src="/public/noticias/noticia-expandida/arrow_up.svg"
          alt="Seta para cima"
        />
        Subir ao topo
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { onMounted, ref } from "vue"
  import router from "../../../router"
  import { useNoticiaStore } from "../../../stores/noticias/store"
  import { INoticia } from "../../../stores/noticias/types"
  import { friendlyDateTime } from "../../../utils/formatacao/datetime"
  import Spinner from "../../../components/general/Spinner.vue"

  const store = useNoticiaStore()
  const noticia = ref<INoticia>({
    id: 0,
    titulo: "",
    descricao: "",
    tags: [],
    subTitulo: "",
    dataPublicacao: "",
    arquivo: {} as File
  })

  const isLoadingContent = ref(false)
  const handleNavigateUp = () => {
    window.scrollTo(0, 0)
  }

  onMounted(async () => {
    isLoadingContent.value = true
    await store.getNewsById(
      parseInt(router.currentRoute.value.params.noticiaId.toString())
    )

    noticia.value = store.response.dado
    isLoadingContent.value = false
  })
</script>

<style scoped lang="scss">
  .btn-up-container {
    margin: 0 auto;
    display: flex;
    justify-content: flex-end;
    max-width: 1300px;

    .btn-up {
      background-color: #fff;
      padding: 0.7rem;
      border: 1px solid #505050;
      border-radius: 10px;
      font-weight: 600;
    }

    .btn-up:hover {
      background-color: rgb(232, 229, 229);
    }
  }

  .noticia-expandida {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    margin: 3rem auto;
    max-width: 1300px;

    header {
      flex-direction: column;
      margin-top: 3rem;
      margin-bottom: 3rem;
      width: 100%;

      h1 {
        color: #000;
        font-size: 2rem;
        text-align: start;
        font-weight: 600;
      }

      p {
        font-weight: 400;
        text-align: start;
        font-size: 1.2rem;
      }

      time {
        color: #6b6a64;
        font-size: 1rem;
        text-align: start;
      }

      .nome-autor {
        font-weight: 600;
        font-size: 1rem;
        color: #000;
        background: url("/public/noticias/noticia-expandida/user.svg");
        background-repeat: no-repeat;
        background-position-y: center;
        background-size: contain;
        padding-left: 2rem;
      }
    }

    #first-paraghap {
      font-weight: 500;
      text-align: start;
      font-size: 0.8rem;
    }

    .news-body {
      font-weight: 400;
      text-align: start;
      font-size: 1.25rem;
      margin-top: 1.5rem;
      margin-bottom: 3rem;
    }
  }
</style>
