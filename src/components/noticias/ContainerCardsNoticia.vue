<template>
  <div class="noticias-content ghp">
    <div v-if="!loadingNews" class="card-noticia-container dark-body-text">
      <nav
        v-for="(container, containerIndex) in noticias"
        :key="containerIndex"
      >
        <div class="card-noticia">
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
        </div>
      </nav>
    </div>
	<Spinner v-else :spinner-color-class="'text-dark'" />
    
    <GeneralBtn
      btnText="VER MAIS"
      :isExternalLink="false"
      link="#"
      bgColor="#639280"
      width="200px"
      textColor="#fff"
      height="40px"
      id="know_more"
      @click="addNewsToView()"
      :class="{ 'd-none': (page*6) > noticiaStore.allNews.length }"
    />
	<Spinner v-if="loadingMoreNews" :spinner-color-class="'text-dark'" />
  </div>
</template>

<script setup lang="ts">
  import { onMounted, ref } from "vue"
  import { useNoticiaStore } from "../../stores/noticias/store"
  import CardNoticia from "./CardNoticia.vue"
  import Spinner from "../../components/general/Spinner.vue"
  import { INoticiaSimplificada } from "../../stores/noticias/types"
  import GeneralBtn from "../buttons/GeneralBtn.vue"

  const noticiaStore = useNoticiaStore()
  const loadingNews = ref(true)
  const loadingMoreNews = ref(false)
  const page = ref(1)
  let noticias = ref<Array<INoticiaSimplificada>>()

  const addNewsToView = async () => {
    page.value++
	loadingMoreNews.value = true
	await noticiaStore.getAllNews(page.value)
	noticias.value = noticiaStore.allNews
	loadingMoreNews.value = false
  }

  onMounted(async () => {
	loadingNews.value = true
	await noticiaStore.getAllNews(page.value)
	noticias.value = noticiaStore.allNews
    loadingNews.value = false
  })
</script>

<style scoped lang="scss">
  .noticias-content {
    width: 100%;
    margin: 0 auto;
    margin-top: 48px;
  }

  .card-noticia-container {
    width: 100%;
    display: grid;
    margin: 1rem auto;
    grid-template-columns: 1fr 1fr 1fr 1fr;
    gap: 48px 32px;

    .card-notitcia {
      width: 200px;
    }

    @media (max-width: 1300px) {
      max-width: 520px;
      grid-template-columns: 1fr 1fr;
    }

    @media (max-width: 580px) {
      max-width: 280px;
      grid-template-columns: 1fr;
    }
  }
</style>
