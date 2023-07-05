<template>
    <Banner
            path="/documentos/pesquisas.png"
            img-alt="Pesquisas"
            figcaption="Pesquisas"
            :img-overlay="true"
    >

		<ExternalHeader
			title="Pesquisas"
			subtitle=""
			paragraph="Tenha acesso às principais pesquisas disponibilizados pelo Ecossistema Local de Inovação - Campo Grande - MS"
		/>
    </Banner>
	<Spinner v-if="loading"/>
    <section id="pesquisa-documento" class="container" v-if="!loading">
		<div class="row my-5 gap-5 justify-content-end">
			<!-- <FilterComponent
				field="área"
				:items="[]"
				type="documento"
				class="col"
			/>

			<FilterComponent
				field="autor"
				:items="[]"
				type="documento"
				class="col"
			/> -->

			<SearchComponent
				:items="documentoStore.researches"
				type="documento"
				class="col-3"
				@search-result="filtrarDocumentos"
			/>
		</div>

		<div class="pt-3">
			<h3 class="text-start font-semibold">Todos</h3>

			<div class="row">

				<div v-for="(doc, index) in researches" class="col-4">
					<CardDocumento
						:documento="doc"
						class="col w-75"
						:has-download-option="true"
						:key="index"
					/>
				</div>
			</div>
		</div>

		<Spinner v-if="loadingMoreContent"/>
		<button class="green-btn-primary w-25 my-5" @click="addMoreResearchs" v-if="!loadingMoreContent">
			Ver mais
		</button>
    </section>
</template>

<script setup lang="ts">
import { onMounted, ref} from 'vue';
import Banner from '../../../components/general/Banner.vue';
import CardDocumento from '../../../components/documentos/CardDocumento.vue';
import ExternalHeader from '../../../components/general/ExternalHeader.vue';
import SearchComponent from '../../../components/general/SearchComponent.vue';
import { IDocumentoSimplificado } from '../../../stores/documentos/types';
import { useDocumentStore } from '../../../stores/documentos/store';
import Spinner from '../../../components/general/Spinner.vue';

const documentoStore = useDocumentStore()
const researches = ref<IDocumentoSimplificado[]>([])
const page = ref(1)
const loading = ref(false)
const loadingMoreContent = ref(false)


onMounted(async () => {
	loading.value = true
    await documentoStore.getResearches(page.value)
	researches.value = documentoStore.researches
	loading.value = false
})

const addMoreResearchs = async () => {
	loadingMoreContent.value = true
	page.value++
	await documentoStore.getResearches(page.value)
	researches.value = documentoStore.researches
	loadingMoreContent.value = false
}

const filtrarDocumentos = (
    documentosFiltrados: Array<IDocumentoSimplificado>
  ) => {
    researches.value = documentosFiltrados
  }

</script>

<style scoped lang="scss">

section#pesquisa-documento {
  h3{
	font-size: 24px;
  }
}

</style>