<template>
  <div class="box mx-auto external-card p-5 container-fluid">
    <h1 class="dark-title fs-4 text-start">Notícias criadas</h1>

    <button
      class="fab green-btn-primary max-w-25"
      @click="$router.push({ name: 'GerenciaNoticia' })"
    >
      + CRIAR NOVA NOTÍCIA
    </button>

    <div class="row align-items-end mt-4 mb-5 justify-content-between px-2">
      <div class="col-xs-12 col-sm-6 col-md-4 my-2">
        <FilterComponent
          field="data"
          :items="noticiaStore.allUserNews"
          type="noticia"
          @filter-result="filtrarNoticias"
        />
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
      <Spinner v-if="loadingNews" />

      <CardNoticiaCriada
        v-if="rascunho"
        :noticia="rascunho"
        class="col-xs-12 col-sm-6 col-lg-4 p-2"
        @update-list="reloadNoticias"
        :isRascunho="true"
      />
      <CardNoticiaCriada
        v-for="(noticia, index) in noticias"
        :key="noticia.id"
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
  import Spinner from "../../../../components/general/Spinner.vue"
  import { useNoticiaStore } from "../../../../stores/noticias/store"
  import {
    INoticiaSimplificada,
    NoticiaSimplificada
  } from "../../../../stores/noticias/types"

  const noticiaStore = useNoticiaStore()
  const lastIndex = ref(6)
  const loadingNews = ref(false)
  const page = ref(1)
  let noticias = ref<Array<INoticiaSimplificada>>()
  let noticiasExibidas = ref<Array<INoticiaSimplificada>>()

  const rascunho = ref<NoticiaSimplificada>()

  const filtrarNoticias = (noticiasFiltradas: Array<INoticiaSimplificada>) => {
    noticias.value = noticiasFiltradas
    noticiasExibidas.value = noticiasFiltradas.slice(0, lastIndex.value)
  }

  const addNewsToView = async () => {
	page.value++
	loadingNews.value = true
	await noticiaStore.getUserNews(page.value)
	noticias.value = noticiaStore.allUserNews
	loadingNews.value = false
  }

  const reloadNoticias = async () => {
    loadingNews.value = true

    if (!localStorage.getItem("noticiaRascunho")) rascunho.value = undefined
    await noticiaStore.getUserNews(page.value)
    noticias.value = noticiaStore.allUserNews
    noticiasExibidas.value = noticias.value.slice(0, lastIndex.value)
    loadingNews.value = false
  }

  const verificaRascunho = () => {
    const noticiaRascunhoStr = localStorage.getItem("noticiaRascunho")

    if (noticiaRascunhoStr) {
      const noticiaRascunho = JSON.parse(noticiaRascunhoStr)

      rascunho.value = new NoticiaSimplificada(
        noticiaRascunho.noticia.id,
        noticiaRascunho.noticia.titulo,
        noticiaRascunho.noticia.tags,
        "",
        noticiaRascunho.noticia.dataPublicacao,
        noticiaRascunho.noticia.arquivo
      )
    }
  }

  onMounted(async () => {
    loadingNews.value = true
    await noticiaStore.getUserNews(page.value)
    noticias.value = noticiaStore.allUserNews
    noticiasExibidas.value = noticias.value.slice(0, lastIndex.value)
    verificaRascunho()
    loadingNews.value = false
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
