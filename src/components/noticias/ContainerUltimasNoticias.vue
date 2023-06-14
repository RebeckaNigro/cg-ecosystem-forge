<template>
	<div class="noticias-content ghp">
		<h1 class="dark-title">ÚLTIMAS NOTÍCIAS</h1>

		<div v-if="!loadingNews" class="card-noticia-container"> 
			<nav v-for="(container, containerIndex) in noticiaStore.lastNews" :key="containerIndex">
				<CardNoticia 
				:is-relacionada="false"
				:noticia="container"
				@click="$router.push({ name: 'NoticiaExpandida', params: { noticiaId: container.id }})"/>
			</nav>

			<div class="borda-direita"></div>
			
		</div>
		<Spinner
			v-else
			:spinner-color-class="'text-dark'"
		/>
		<div class="divider"></div>
	</div>

</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useNoticiaStore } from '../../stores/noticias/store';
import CardNoticia from './CardNoticia.vue';
import Spinner from '../../components/general/Spinner.vue'

const noticiaStore = useNoticiaStore()
const loadingNews = ref(true)

onMounted(() => {
	noticiaStore.getLastNews()
	loadingNews.value = false
})

</script>


<style scoped lang="scss">

.noticias-content {
	width: 100%;
	margin: 0 auto;
	margin-top: 48px;

	> h1 {
		font-weight: 600;
		font-size: 2rem;
		margin: 2rem 0;
	}
	.card-noticia-container {
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
}

.divider{
	
	height: .5px;
	background-color: #6B6A64;
}




</style>