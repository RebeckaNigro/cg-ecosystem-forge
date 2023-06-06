<template>
  <section class="container-fluid">
    <main>
      <Banner
        path="/noticias/banner.png"
        figcaption="workspace_image"
        img-alt="workspace_image"
      >
        <div class="banner-noticia-content ghp">
          <div>
            <h1 class="dark-title">NOTÍCIAS</h1>
            <p class="fs-5">
              Confira as principais notícias do Ecossistema Local de Inovação -
              Campo Grande - MS
            </p>
          </div>
        </div>
      </Banner>

      <div class="container mt-5">
        <div class="filters row justify-content-end align-items-end">
          <!--<div class="filtrar-area col">
            <FilterComponent content-type="área" :datas="[]" class="mx-4" />
          </div>

          <div class="filtrar-organizador col">
            <FilterComponent content-type="organizador" :datas="[]" class="mx-4" />

          </div>-->
          <div class="pesquisar col">
            <input
              type="text"
              name="pesquisar"
              id="pesquisar"
              placeholder="Pesquisar"
              v-model="searchInputText"
            />
            <button class="btn" id="btn-pesquisar" @click="handleSearch">
              <img src="/public/search_icon.svg" alt="Pesquisar" />
            </button>
          </div>
        </div>
      </div>

      <div
        class="search-results-container dark-body-text"
        v-if="isResultsVisible"
      >
        <nav
          v-for="(container, containerIndex) in searchResults"
          :key="containerIndex"
        >
          <CardNoticia
            :is-relacionada="false"
            :noticia="container"
            @click="
              $router.push({
                name: 'NoticiaExpandida',
                params: { noticiaId: container.id }
              })
            "
          />
        </nav>
      </div>

      <div v-else>
        <ContainerUltimasNoticias />
        <ContainerCardsNoticia />
      </div>
    </main>
  </section>
</template>

<script setup lang="ts">
  import { ref } from "vue"
  import Banner from "../../components/general/Banner.vue"
  import ContainerCardsNoticia from "../../components/noticias/ContainerCardsNoticia.vue"
  import ContainerUltimasNoticias from "../../components/noticias/ContainerUltimasNoticias.vue"
  import { useNoticiaStore } from "../../stores/noticias/store"
  import { INoticiaSimplificada } from "../../stores/noticias/types"
  import CardNoticia from "../../components/noticias/CardNoticia.vue"

  const noticiaStore = useNoticiaStore()
  const searchResults = ref<Array<INoticiaSimplificada>>()
  const searchInputText = ref("")
  const isResultsVisible = ref(false)

  const handleSearch = () => {
    const inputTextLowerCase = searchInputText.value.trim().toLowerCase()
    if (inputTextLowerCase !== "") {
      searchResults.value = noticiaStore.allNews.filter((noticia) => {
        if (noticia.titulo) {
          const tituloLowerCase = noticia.titulo.toLowerCase()
          return tituloLowerCase.includes(inputTextLowerCase)
        }
        return false
      })
      isResultsVisible.value = true
    } else {
      searchResults.value = []
      isResultsVisible.value = false
    }
  }
</script>

<style scoped lang="scss">
  section.container-fluid {
    padding: 0;
    margin-bottom: 40px;
    overflow: hidden;

    .filtrar-area,
    .filtrar-organizador {
      max-width: 300px;
    }

    .filters {
      margin: 0 auto;
    }

    .pesquisar {
      max-width: 400px;

      #pesquisar {
        border-radius: 40px 0px 0px 40px;
        border: 1px solid #6b6a64;
        border-right: unset;
        padding: 0.35rem 0.7rem 0.52rem 0.7rem;
        width: 300px;

        @media (max-width: 1000px) {
          width: 200px;
        }
      }

      #btn-pesquisar {
        border: 1px solid #6b6a64;

        border-radius: 0 40px 40px 0;
        background-color: #fff;
        height: 100%;

        img {
          max-width: 26px;
        }
      }

      #pesquisar:focus {
        border-color: #86b7fe;
        outline: 0;
        box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
      }

      @media (max-width: 1000px) {
        margin: 0 auto;
      }
      @media (max-width: 580px) {
        margin-top: 1.5rem;
      }
    }

    .search-results-container {
      width: 100%;
      display: grid;
      margin: 1.5rem 2rem;
      grid-template-columns: 1fr 1fr 1fr;
      gap: 48px 28px;

      @media (max-width: 820px) {
        max-width: 520px;
        grid-template-columns: 1fr 1fr;
      }

      @media (max-width: 580px) {
        max-width: 280px;
        grid-template-columns: 1fr;
      }
    }
  }

  .banner-noticia-content {
    position: absolute;
    top: 0;
    width: 100%;
    height: 100%;
    padding-left: 64px;

    display: flex;
    align-items: center;

    .dark-title {
      font-size: 2rem;
    }

    > img {
      width: 10%;
      min-width: 60px;
      max-width: 128px;
      margin-right: 24px;
    }

    > div {
      display: flex;
      flex-direction: column;
      align-items: flex-start;
      width: 100%;
      max-width: 500px;

      p {
        font-weight: 500;
        text-align: start;
        max-height: 100px;
        height: 100%;
        overflow-y: auto;
      }
    }
  }

  @media (max-width: 1200px) {
    .banner-noticia-content {
      .dark-title {
        font-size: 2rem;
      }

      .dark-body-text {
        font-size: 0.8rem;
      }
    }
  }

  @media (max-width: 991px) {
    .banner-noticia-content {
      .dark-title {
        margin-bottom: 0;
        font-size: 1.5rem;
      }

      > div {
        max-width: 300px;

        p {
          max-height: 60px;
          margin-bottom: 0;
        }
      }
    }
  }

  @media (max-width: 768px) {
    .banner-noticia-content {
      .dark-title {
        font-size: 1rem;
      }

      > div {
        max-width: 200px;

        p {
          max-height: 40px;
        }
      }

      .dark-body-text {
        font-size: 0.7rem;
      }
    }
  }

  @media (max-width: 576px) {
    .banner-noticia-content {
      .dark-title {
        font-size: 0.8rem;
      }

      .dark-body-text {
        font-size: 0.8rem;
      }
    }
  }
</style>
