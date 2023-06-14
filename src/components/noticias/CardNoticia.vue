<template>
  <div
    class="card-noticia-content"
    :class="[isRelacionada ? 'is-relacionada' : 'not-relacionada']"
  >
    <img :src="
		noticia?.arquivo 
		? 'data:image/png;base64, ' + noticia?.arquivo
		: '/noticias/noticia-expandida/default-news-cover.svg'	
	
	" alt="" class="card-noticia-image" />
    <span class="tags">#tecnologia #inovação</span>
    <p v-if="!isRelacionada">{{ noticia?.titulo }}</p>
  </div>
</template>

<script setup lang="ts">
  import { not } from "@vuelidate/validators";
import { INoticiaSimplificada } from "../../stores/noticias/types"

  const props = defineProps<{
    noticia: INoticiaSimplificada | null
    isRelacionada: boolean
  }>()
</script>

<style scoped lang="scss">
  .is-relacionada {
    width: 50%;
  }
  .not-relacionada {
    max-width: 385px;
  }
  .card-noticia-content {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    max-height: 400px;
    padding: 10px;
    font-weight: 500;
    min-width: 260px;

    > p {
      font-size: 1rem;
      overflow: hidden;
      text-transform: uppercase;
      margin-bottom: 2px;
      text-overflow: ellipsis;
      text-align: start;
    }

    > span {
      text-align: start;
      text-transform: uppercase;
      font-size: 0.75rem;
      line-height: 20px;
    }
  }
  .card-noticia-content:hover {
    cursor: pointer;
    box-shadow: 0px 0px 50px rgba(0, 0, 0, 0.15);
    border-radius: 10px;
  }
  .card-noticia-image {
    width: 100%;
    height: 220px;

    margin-bottom: 16px;
  }
</style>
