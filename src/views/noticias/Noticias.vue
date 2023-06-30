<template>
	<section class="mb-5">
		<Banner path="/noticias/banner.png" figcaption="workspace_image" img-alt="workspace_image">
			<ExternalHeader title="Notícias" subtitle="" paragraph="Confira as principais notícias do Ecossistema Local de Inovação -
						Campo Grande - MS" />
		</Banner>

		<div class="container mt-5">
			<div class="row justify-content-between align-items-end">
				<div class="col-sm-12 col-md-4 col-lg-3 mb-4 mb-md-0">
					<!-- <FilterComponent 
						:items="noticiaStore.allNews" 
						type="noticiaExterna" 
						field="autor"
						@filter-result="filtrarNoticias" /> -->
				</div>
				<div class="col-sm-12 col-md-4 col-lg-3">
					<SearchComponent :items="noticiaStore.allNews" type="noticia" @search-result="filtrarNoticias" />
				</div>

		

			</div>
		</div>

		<div class="search-results-container dark-body-text" v-if="isResultsVisible">
			<div v-for="(container, containerIndex) in searchResults" :key="containerIndex">
				<CardNoticia :is-relacionada="false" :noticia="container" @click="
					$router.push({
						name: 'NoticiaExpandida',
						params: { noticiaId: container.id }
					})
				" />
			</div>
		</div>

		<div v-if="!isResultsVisible">
			<ContainerUltimasNoticias />
			<ContainerCardsNoticia />
		</div>
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
import ExternalHeader from "../../components/general/ExternalHeader.vue"
import SearchComponent from "../../components/general/SearchComponent.vue"
import FilterComponent from "../../components/general/FilterComponent.vue"

const noticiaStore = useNoticiaStore()
const searchResults = ref<Array<INoticiaSimplificada>>()
const isResultsVisible = ref(false)

const filtrarNoticias = (noticiasFiltradas: INoticiaSimplificada[], inputVazio: boolean) => {
	if(!inputVazio){
		searchResults.value = noticiasFiltradas
		isResultsVisible.value = true
	}else{
		isResultsVisible.value = false
	}
}
</script>

<style scoped lang="scss">

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


</style>
