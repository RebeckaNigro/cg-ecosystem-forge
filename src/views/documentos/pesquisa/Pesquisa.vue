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

				<div v-for="(doc, index) in researches" class="col-4">
					<CardDocumento
						:documento="doc"
						class="col w-75"
						:has-download-option="true"
					/>
				</div>
			</div>
		</div>
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

const dummyResearchs = reactive([
    {
        title: 'NOME DA PESQUISA',
        description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
        downloadPath: ''
    },
    {
        title: 'NOME DA PESQUISA',
        description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
        downloadPath: ''
    },
    {
        title: 'NOME DA PESQUISA',
        description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
        downloadPath: ''
    },
    {
        title: 'NOME DA PESQUISA',
        description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
        downloadPath: ''
    },
    {
        title: 'NOME DA PESQUISA',
        description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
        downloadPath: ''
    },
    {
        title: 'NOME DA PESQUISA',
        description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
        downloadPath: ''
    },
    {
        title: 'NOME DA PESQUISA',
        description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
        downloadPath: ''
    },
    {
        title: 'NOME DA PESQUISA',
        description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
        downloadPath: ''
    },
    {
        title: 'NOME DA PESQUISA',
        description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
        downloadPath: ''
    }
])

const documentoStore = useDocumentStore()
const researches = ref<IDocumentoSimplificado[]>([])
const documento = ref<IDocumentoSimplificado>({
	id: 1,
	nome: "Alo",
	descricao: "Lorem ipsum dolor sit amet consectetur. Tortor non aliquam enim in nunc at aliquam.",
	documentoArea: "Pesquisa",
	ultimaOperacao: "2023-07-03T10:58:54.14",
	autor: "Genilsin",
	tags: [{descricao: 'inovacao', id: 7}],
	data: '2023-07-03T14:58:54.067'
})


onMounted(async () => {
    researches.value = await documentoStore.getResearches()
	console.log(researches.value);
	
})
</script>

<style scoped lang="scss">

section#pesquisa-documento {
  h3{
	font-size: 24px;
  }
}

</style>