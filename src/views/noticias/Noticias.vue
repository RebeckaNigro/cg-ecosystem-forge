<template>
	<section class="mb-5">
		<Banner path="/noticias/banner.png" figcaption="workspace_image" img-alt="workspace_image">
			<ExternalHeader title="Notícias" subtitle="" paragraph="Confira as principais notícias do Ecossistema Local de Inovação -
						Campo Grande - MS" />
		</Banner>

		<div class="container mt-5">
			<div class="row justify-content-between align-items-end">
				<div class="col-3">
					<FilterComponent :items="noticiaStore.allNews" type="noticiaExterna" @filter-result="filtrarNoticias" />
				</div>
				<div class="col-3">
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

	>img {
		width: 10%;
		min-width: 60px;
		max-width: 128px;
		margin-right: 24px;
	}

	>div {
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

		>div {
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

		>div {
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
