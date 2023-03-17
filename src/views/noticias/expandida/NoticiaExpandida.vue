<template>
  <div class="pt-5 pb-5">
    <Spinner spinnerColorClass="text-dark" v-if="isLoadingContent" />
    <div class="noticia-breadcrumb">
      <nav style="--bs-breadcrumb-divider'>';" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li
            class="breadcrumb-item unactive"
            @click="$router.push({ name: 'Noticias' })"
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
        :src="'data:image/png;base64, ' + noticia.arquivo"
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
        Subir pro topo
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
  .background {
    background-image: url("/public/user/background.svg");
    background-repeat: no-repeat;
    background-size: cover;
    width: 100%;
  }

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
      font-family: "Montserrat-SemiBold", sans-serif;
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
        font-family: "Montserrat-SemiBold", sans-serif;
      }

      p {
        font-family: "Montserrat-Regular", sans-serif;
        text-align: start;
        font-size: 1.2rem;
      }

      time {
        color: #6b6a64;
        font-size: 1rem;
        text-align: start;
      }

      .nome-autor {
        font-family: "Montserrat-SemiBold", sans-serif;
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
      font-family: "Montserrat-Medium", sans-serif;
      text-align: start;
      font-size: 0.8rem;
    }

    .news-body {
      font-family: "Montserrat-Regular", sans-serif;
      text-align: start;
      font-size: 1.25rem;
      margin-top: 1.5rem;
      margin-bottom: 3rem;
    }
  }

  .noticia-breadcrumb {
    display: flex;
    width: fit-content;
    margin-left: 15%;
    font-size: 20px;

    .active {
      font-family: "Montserrat-Medium", sans-serif;
    }

    .unactive {
      cursor: pointer;
      font-family: "Montserrat-Light", sans-serif;
    }

    @media (max-width: 920px) {
      font-size: 17px;
    }
  }

  @media (max-width: 1200px) {
    .noticia-expandida-banner {
      .text-container {
        h1 {
          font-size: 2rem;
        }

        p {
          font-size: 0.8rem;
        }
      }
    }
  }

  @media (max-width: 991px) {
    .noticia-expandida-banner {
      .text-container {
        max-width: 300px;

        h1 {
          font-size: 1.5rem;
        }

        p {
          max-height: 60px;
        }
      }
    }
    .noticia-expandida {
      header {
        margin-top: 2rem;

        h1 {
          font-size: 1.5rem;
        }

        p.dark-body-text {
          font-size: 1.2rem;
        }
      }

      .dark-body-text {
        font-size: 1rem;
        text-align: justify;
      }

      main {
        h3 {
          font-size: 1.5rem;
        }
      }
    }
  }

  @media (max-width: 768px) {
    .noticia-expandida-banner {
      .text-container {
        max-width: 200px;

        h1 {
          font-size: 1rem;
        }

        p {
          max-height: 40px;
          font-size: 0.7rem;
        }
      }
    }
  }

  @media (max-width: 576px) {
    .noticia-expandida-banner {
      .text-container {
        h1 {
          font-size: 0.8rem;
        }
      }
    }
    .noticia-expandida {
      header {
        h1 {
          font-size: 1.3rem;
        }

        p.dark-body-text {
          font-size: 1rem;
        }
      }

      .dark-body-text {
        font-size: 1rem;
        text-align: justify;
      }

      main {
        h3 {
          font-size: 1.3rem;
        }
      }
    }
  }
</style>
