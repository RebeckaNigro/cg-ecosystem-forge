<template>
  <div class="mt-5 container">
    <div v-if="!loadingNews" class="row my-4 mx-auto dark-body-text">
      <div
        v-for="(container, containerIndex) in noticias"
        :key="containerIndex"
		class="col mb-4"
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
      </div>
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

</style>
