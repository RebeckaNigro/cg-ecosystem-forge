<template>
  <section :id="'ultimos-conteudos' + contentType" class="p-4 ultimos-conteudos boring-gray-border d-flex">
    <header>
      <h1 class="dark-title">
        {{ containerTitle }}
      </h1>
    </header>
    <main v-if="!loadingNews">
      <div v-for="(content, index) in  noticiaStore.lastNews" :key="index" class="d-flex w-100 m-3">
        <div class="title-box boring-gray-border">
          {{ content.titulo }}
        </div>
		<div class="actions-container">
			<button type="button" :id="'delete-content-btn' + content.id" @click="handleDeleteNews(content.id)">
			  <img src="/delete-icon.png" alt="delete_icon">
			</button>

			<!--<button type="button" :id="'edit-content-btn' + content.id">
			  <img src="/pen-icon.svg" alt="pen_icon">
			</button>-->
		</div>
      </div>
    </main>
	<div v-else class="spinner-border text-dark align-self-center mt-4 mb-4" role="status">
      <span class="visually-hidden">Loading...</span>
    </div>
    <footer>
      <button type="button" id="see-more" class="btn-primary light-title" @click="$router.push({ name: 'NoticiasCriadas'})">Ver mais</button>
      <button type="button" id="create-new" class="green-btn light-title" @click="$router.push({ path: contentType })">Criar {{ contentType }}</button>
    </footer>
  </section>
</template>

<script setup lang="ts">import { onMounted, ref } from 'vue';
import { useNoticiaStore } from '../../../stores/noticias/store';

const props = defineProps<{
  contentType: string
  containerTitle: string
}>()

const noticiaStore = useNoticiaStore()
const loadingNews = ref(true)

const handleDeleteNews = async (noticiaId: number) => {
	await noticiaStore.deleteNews(noticiaId) 
	window.location.reload()
}

onMounted(() => {
	noticiaStore.getLastNews()
	loadingNews.value = false
})
</script>

<style scoped lang="scss">
  .ultimos-conteudos {
    max-width: 500px;
    width: 100%;
    max-height: 450px;
    justify-content: center;
    flex-direction: column;
    
    header > h1 {
      font-size: 1rem;
    }
    main > .d-flex {
      justify-content: space-between;

      .title-box {
        width: 75%;
        white-space: nowrap;
        text-overflow: ellipsis;
        overflow: hidden;
      }
    }
    footer {
      button {
        font-size: 0.7rem;
        border-radius: 25px;
        height: 40px;
        margin: 0 5px;
      }
    }
  }
  .actions-container{
	flex-direction: row;
	button{
		background-color: unset;
		border: 0;
	}
    img {
      max-width: 25px;
    }
  }
</style>