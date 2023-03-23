<template>
  <div class="box mx-auto external-card p-5 container-fluid">
    <h1 class="dark-title fs-4 text-start">Notícias criadas</h1>

    <button
      class="fab green-btn-primary"
      @click="$router.push({ name: 'GerenciaNoticia' })"
    >
      + Criar nova notícia
    </button>

    <div class="row align-items-end mt-4 mb-5 justify-content-between px-2">
      <div class="col-xs-12 col-sm-6 col-md-4 my-2">
        <FilterComponent :content-type="'data'" :datas="[]" />
      </div>

      <div class="col-xs-12 col-sm-6 col-md-4 my-2">
        <SearchComponent
          :items="noticiaStore.allUserNews"
          type="noticia"
          @search-result="filtrarNoticias"
        />
      </div>
    </div>

    <div class="container-fluid row justify-content-between g-0">
      <CardNoticiaCriada
        v-if="rascunho"
        :noticia="rascunho"
        class="col-xs-12 col-sm-6 col-lg-4 p-2"
        @update-list="reloadNoticias"
        :isRascunho="true"
      />
      <CardNoticiaCriada
        v-for="(noticia, index) in noticias"
        :key="index"
        :noticia="noticia"
        class="col-xs-12 col-sm-6 col-lg-4 p-2"
        @update-list="reloadNoticias"
        :isRascunho="false"
      />
    </div>

    <div class="row container-fluid mb-3 mt-5">
      <div class="col-sm-6">
        <button
          class="green-btn-outlined button-specific"
          @click="$router.back()"
        >
          Voltar
        </button>
      </div>

      <div class="col-sm-6">
        <button
          class="green-btn-primary button-specific"
          @click="addNewsToView"
        >
          Ver mais
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { onMounted, reactive, ref } from "vue"
  import FilterComponent from "../../../../components/general/FilterComponent.vue"
  import SearchComponent from "../../../../components/general/SearchComponent.vue"
  import CardNoticiaCriada from "../../../../components/user/noticias/noticiasCriadas/CardNoticiaCriada.vue"
  import { useNoticiaStore } from "../../../../stores/noticias/store"
  import {
    INoticiaSimplificada,
    NoticiaSimplificada,
    Noticia
  } from "../../../../stores/noticias/types"

  const noticiaStore = useNoticiaStore()
  const lastIndex = ref(6)
  let noticias = ref<Array<INoticiaSimplificada>>()

  const rascunho = ref<NoticiaSimplificada>()

  const filtrarNoticias = (noticiasFiltradas: Array<INoticiaSimplificada>) => {
    console.dir(noticiasFiltradas)
    noticias.value = []
    noticias.value = noticiasFiltradas.slice(0, lastIndex.value)
  }

  const addNewsToView = () => {
    lastIndex.value += 3
    noticias.value = noticiaStore.allUserNews.slice(0, lastIndex.value)
  }

  const reloadNoticias = async () => {
    if (!localStorage.getItem("noticiaRascunho")) rascunho.value = undefined
    await noticiaStore.getUserNews()
    noticias.value = noticiaStore.allUserNews.slice(0, lastIndex.value)
  }

  const verificaRascunho = () => {
    const noticiaRascunhoStr = localStorage.getItem("noticiaRascunho")

    if (noticiaRascunhoStr) {
      const noticiaRascunho = JSON.parse(noticiaRascunhoStr)

      rascunho.value = new NoticiaSimplificada(
        noticiaRascunho.noticia.id,
        noticiaRascunho.noticia.titulo,
        noticiaRascunho.noticia.tags,
        noticiaRascunho.noticia.dataPublicacao,
        noticiaRascunho.noticia.arquivo
      )
    }
  }

  onMounted(async () => {
    await noticiaStore.getUserNews()
    noticias.value = noticiaStore.allUserNews
    verificaRascunho()
  })
</script>

<style scoped>
  .external-card {
    width: 80%;
    position: relative;
  }

  .fab {
    position: absolute;
    top: -1.2rem;
    right: 3rem;
  }

  .button-specific {
    width: 100%;
    margin: 1rem 1rem;
    box-sizing: border-box;
  }
</style>
<!-- .noticias-container {
    display: flex;
    position: relative;
    flex-direction: column;
    align-items: flex-start;
    margin: 1.4rem auto;
    width: 90%;
    padding: 3rem;
    border-radius: 10px;
    flex-wrap: wrap;

    .btn-criar-noticia {
      background-color: #639280;
      border: unset;
      border-radius: 30px;
      padding: 0.7rem 2rem;
      font-family: "Montserrat-Bold";
      color: #fff;
      text-transform: uppercase;
      position: absolute;
      right: 3rem;
      top: -1.6rem;
    }

    .btn-criar-noticia:hover {
      background-color: #73b79d;
    }

    .filtrar-data {
      max-width: 300px;
    }

    .cards-container {
      width: 100%;
      display: grid;
      margin: 0 auto;
      grid-template-columns: 1fr 1fr 1fr;
      gap: 48px 32px;

      @media (max-width: 820px) {
        max-width: 520px;
        grid-template-columns: 1fr 1fr;
      }

      @media (max-width: 580px) {
        max-width: 280px;
        grid-template-columns: 1fr;
      }
    }

    .pesquisar {
      @media (max-width: 580px) {
        margin-top: 1.5rem;
      }
    }
    .botoes-container {
      button {
        width: 30%;
        border-radius: 30px;
        padding: 0.8rem 0;
        font-family: "Montserrat-SemiBold";
        color: #fff;
        font-size: 19px;
      }
      .btn-voltar {
        background-color: #fff;
        border: 1px solid #639280;
        color: #639280;
      }

      .btn-voltar:hover {
        background-color: #ececec;
      }

      .btn-ver-mais {
        background-color: #639280;
        border: unset;
      }

      .btn-ver-mais:hover {
        background-color: #73b79d;
      }
    }
  } -->
