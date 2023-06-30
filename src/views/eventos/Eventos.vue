<template>
	<section>
		<Banner path="/eventos/banner.png" img-alt="agenda de eventos" figcaption="eventos do ecossistema"
			:img-overlay="true">

			<ExternalHeader title="Eventos" subtitle="" paragraph="Tenha acesso aos principais documentos disponibilizados pelo Ecossistema Local de
                    Inovação - Campo Grande - MS" />
		</Banner>

		<div class="container mt-5">

			<div class="row justify-content-between align-items-end">
				<div class="col-sm-12 col-md-4 col-lg-3 mb-4 mb-md-0">
					<!-- <FilterComponent
						:items="eventoStore.eventos"
						type="eventoExterno"
						field="organizador"
						@filter-result="filtrarEventos"
					/> -->
				</div>

				<div class="col-sm-12 col-md-4 col-lg-3">
					<SearchComponent
						:items="eventoStore.eventos"
						type="evento"
						@search-result="filtrarEventos"
						origin="externo"
					/>
				</div>
			</div>
		</div>

		<div class="search-results-container" v-if="isSearchResultsVisible">
			<nav v-for="(container, containerIndex) in searchResults" :key="containerIndex">
				<CardEvento :evento="container" />
			</nav>
		</div>

		<div v-if="!isSearchResultsVisible">
			<Destaques v-if="eventoStore.eventos.length > 1" />
			<ContainerEventos />
		</div>
	</section>
</template>

<script setup lang="ts">
//@ts-nocheck
import Banner from '../../components/general/Banner.vue';
import Destaques from '../../components/eventos/Destaques.vue';
import ContainerEventos from '../../components/eventos/ContainerEventos.vue';
import { ref } from 'vue';
import { IEventoSimplificado } from '../../stores/eventos/types';
import { useEventoStore } from '../../stores/eventos/store';
import CardEvento from '../../components/eventos/CardEvento.vue';
import ExternalHeader from '../../components/general/ExternalHeader.vue';
import FilterComponent from '../../components/general/FilterComponent.vue';
import SearchComponent from '../../components/general/SearchComponent.vue';

const eventoStore = useEventoStore()
const searchResults = ref<Array<IEventoSimplificado>>([])
const isSearchResultsVisible = ref(false)

const filtrarEventos = (eventosFiltrados: IEventoSimplificado[], inputVazio: boolean) => {
	if(!inputVazio){
		searchResults.value = eventosFiltrados
		isSearchResultsVisible.value = true
	}else{
		isSearchResultsVisible.value = false
	}

}
</script>

<style scoped lang="scss">
section {
	background-color: #FBFBFB;

	.search-results-container {
		background: linear-gradient(180deg, rgba(255, 255, 255, 0.2) 0%, rgba(0, 0, 0, 0.2) 100%), #F8F9FA;
		width: 100%;
		display: grid;
		margin-top: 2rem;
		grid-template-columns: 1fr 1fr 1fr 1fr;
		gap: 48px 28px;
		padding-left: 1.7rem;
		padding-bottom: 1.5rem;

		@media(max-width: 1024px) {
			grid-template-columns: 1fr 1fr 1fr;
		}

		@media (max-width: 820px) {

			grid-template-columns: 1fr 1fr;
		}

		@media (max-width: 580px) {

			grid-template-columns: 1fr;
		}

	}
}

</style>