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
    <section id="pesquisa-documento" class="container">
		<div class="row my-5 gap-5">
			<FilterComponent
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
			/>

			<SearchComponent
				:items="[]"
				type="documento"
				class="col align-self-end"
			/>
		</div>

		<div class="pt-3">
			<h3 class="text-start font-semibold">Todos</h3>

			<div class="row">

				<div v-for="(doc, index) in documentoStore.researches" class="col-4">
					<CardDocumento
						:documento="doc"
						class="col w-75"
						:has-download-option="true"
						:key="index"
					/>
				</div>
			</div>
		</div>

		<button class="green-btn-primary w-25 my-5" @click="addMoreResearchs">
			Ver mais
		</button>
    </section>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref} from 'vue';
import Banner from '../../../components/general/Banner.vue';
import CardDocumento from '../../../components/documentos/CardDocumento.vue';
import ExternalHeader from '../../../components/general/ExternalHeader.vue';
import FilterComponent from '../../../components/general/FilterComponent.vue';
import SearchComponent from '../../../components/general/SearchComponent.vue';
import { IDocumentoSimplificado } from '../../../stores/documentos/types';
import { useDocumentStore } from '../../../stores/documentos/store';

const documentoStore = useDocumentStore()
const researches = ref<IDocumentoSimplificado[]>([])
const page = ref(1)

onMounted(async () => {
    await documentoStore.getResearches(page.value)
	researches.value = documentoStore.researches
	console.log(researches.value);
	
})

const addMoreResearchs = async () => {
	page.value++
	await documentoStore.getResearches(page.value)
	researches.value.concat(documentoStore.researches)
	console.log(researches.value);
	
}
</script>

<style scoped lang="scss">

section#pesquisa-documento {
  h3{
	font-size: 24px;
  }
}

</style>