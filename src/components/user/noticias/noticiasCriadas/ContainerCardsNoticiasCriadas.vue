<template>
<div class="noticias-container box">
	
	<h1 class="dark-title fs-4 mb-4 mt-4 ms-3">Notícias criadas</h1>

	<button class="btn-criar-noticia" @click="$router.push({name: 'GerenciaNoticia'})">+ Criar nova notícia</button>

	<div class="row justify-content-between w-100 align-items-end mb-5">

		<!--<div class="filtrar-data col">
			<FilterComponent :content-type="'data'" :datas="[]" />
		</div>-->

		<div class="pesquisar col d-flex justify-content-end">
			<SearchComponent />
		</div>
	</div>

	<div class="cards-container">

		<div v-for="(noticia, index) in noticias" :key="index" >

			<CardNoticiaCriada
			:has-image="false"
			:titulo-noticia="noticia.titulo"
			image=""
			:id-noticia="noticia.id"
			/>
		</div>
	</div>

	<div class="botoes-container w-100 d-flex justify-content-around mt-5">
		<button class="btn-voltar w-100 me-3" @click="$router.back()">Voltar</button>
		<button class="btn-ver-mais w-100 ms-3" @click="addNewsToView">Ver mais</button>
	</div>

</div>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import FilterComponent from '../../../../components/general/FilterComponent.vue';
import SearchComponent from '../../../../components/general/SearchComponent.vue'
import CardNoticiaCriada from '../../../../components/user/noticias/noticiasCriadas/CardNoticiaCriada.vue';
import { useNoticiaStore } from '../../../../stores/noticias/store';
import { IUltimaNoticia } from '../../../../stores/noticias/types';

const noticiaStore = useNoticiaStore()
const lastIndex = ref(6)
let noticias = ref<Array<IUltimaNoticia>>(noticiaStore.allNews.slice(0, lastIndex.value))

const addNewsToView = () => {
	lastIndex.value += 3
	noticias.value = noticiaStore.allNews.slice(0, lastIndex.value)
}

onMounted(() => {
	noticiaStore.getAllNews()
})


const dummyContainer = reactive([
	{
		tags: ['Pesquisas', 'Doutorado'],
		titulo: 'Twitter falido',
		dataAtualizacao: '24/11/2022',
		horaAtualizacao: '11:20'
	},
	{
		tags: ['Pesquisas', 'Doutorado'],
		titulo: 'Twitter falido',
		dataAtualizacao: '24/11/2022',
		horaAtualizacao: '11:20'
	},
	{
		tags: ['Pesquisas', 'Doutorado'],
		titulo: 'Twitter falido',
		dataAtualizacao: '24/11/2022',
		horaAtualizacao: '11:20'
	},
	{
		tags: ['Pesquisas', 'Doutorado'],
		titulo: 'Twitter falido',
		dataAtualizacao: '24/11/2022',
		horaAtualizacao: '11:20'
	},
	{
		tags: ['Pesquisas', 'Doutorado'],
		titulo: 'Twitter falido',
		dataAtualizacao: '24/11/2022',
		horaAtualizacao: '11:20'
	},
	{
		tags: ['Pesquisas', 'Doutorado'],
		titulo: 'Twitter falido',
		dataAtualizacao: '24/11/2022',
		horaAtualizacao: '11:20'
	},
	{
		tags: ['Pesquisas', 'Doutorado'],
		titulo: 'Twitter falido',
		dataAtualizacao: '24/11/2022',
		horaAtualizacao: '11:20'
	},
])
</script>

<style scoped lang="scss">
.noticias-container {
	display: flex;
	flex-direction: column;
	align-items: flex-start;
	margin: 1.4rem auto;
	max-width: 1300px;
	padding: 3rem;
	border-radius: 10px;
	flex-wrap: wrap;

	.btn-criar-noticia{
		background-color: #639280;
		border: unset;
		border-radius: 30px;
		padding: .7rem 2rem;
		font-family: 'Montserrat-Bold';
		color: #fff;
		text-transform: uppercase;
		position: absolute;
		right: 11rem;
		top: 11.4rem;
	}

	.btn-criar-noticia:hover{
		background-color: #73b79d;
	}

	.filtrar-data {
		max-width: 300px;
	}

	.cards-container{
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

	.pesquisar{
		@media (max-width: 580px){
			margin-top: 1.5rem;
		}
	}
	.botoes-container{
		button{
			width: 30%;
			border-radius: 30px;
			padding: .8rem 0;
			font-family: 'Montserrat-SemiBold';
			color: #fff;
			font-size: 19px;
		}
		.btn-voltar {
			background-color: #fff;
			border: 1px solid #639280;
			color: #639280;
		}

		.btn-voltar:hover{
			background-color: #ececec;
			
		}
		
		.btn-ver-mais {
			background-color: #639280;
			border: unset;
		}

		.btn-ver-mais:hover{
			background-color: #73b79d;
		}
	}

}
</style>