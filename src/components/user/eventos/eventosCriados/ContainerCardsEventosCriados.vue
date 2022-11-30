<template>
	<div class="eventos-container box">

		<h1 class="dark-title fs-4 mb-4 mt-4 ms-3">Eventos criados</h1>

		<button class="btn-criar-evento" @click="$router.push({ name: 'GerenciaEvento' })">+ Criar novo evento</button>

		<div class="row justify-content-between w-100 align-items-end mb-5">

			<div class="filtrar-data col">
				<FilterComponent :content-type="'data'" :datas="[]" />
			</div>

			<div class="pesquisar col d-flex justify-content-end">
				<input type="text" name="pesquisar" id="pesquisar" placeholder="Pesquisar" v-model="searchInputText">
				<button class="btn" id="btn-pesquisar" @click="handleSearch">
					<img src="../../../../../public/search_icon.svg" alt="Pesquisar">
				</button>
			</div>
		</div>

		<div class="cards-container">

			<div v-for="(evento, index) in eventos" :key="index">

				<CardEventoCriado :has-image="false" image="" :nome-evento="evento.titulo"
					:data-inicio="evento.dataInicio" :data-termino="evento.dataTermino" :endereco-evento="evento.local"
					:evento-id="evento.id" />
			</div>
		</div>

		<div class="botoes-container w-100 d-flex justify-content-around mt-5">
			<button class="btn-voltar w-100 me-3" @click="$router.back()">Voltar</button>
			<button class="btn-ver-mais w-100 ms-3" @click="addEventsToView">Ver mais</button>
		</div>
	</div>

</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import FilterComponent from '../../../../components/general/FilterComponent.vue';
import CardEventoCriado from '../../../../components/user/eventos/eventosCriados/CardEventoCriado.vue';
import { useEventoStore } from '../../../../stores/eventos/store';
import { IUltimoEvento } from '../../../../stores/eventos/types';

const eventoStore = useEventoStore()
const lastIndex = ref(6)
let eventos = ref<Array<IUltimoEvento>>(eventoStore.eventos.slice(0, lastIndex.value))

const searchInputText = ref('')

const addEventsToView = () => {
	lastIndex.value += 3
	eventos.value = eventoStore.eventos.slice(0, lastIndex.value)
}

const handleSearch = () => {
	const inputTextLowerCase = searchInputText.value.trim().toLowerCase()
	
	eventos.value = eventoStore.eventos.filter((evento) => {
		if(evento.titulo){
			const tituloLowerCase = evento.titulo.toLowerCase()
			return tituloLowerCase.includes(inputTextLowerCase)
		}
		return false
		
	})
	
}

onMounted(() => {
	eventoStore.getAllEvents()
})

</script>

<style setup lang="scss">
.eventos-container {
	display: flex;
	flex-direction: column;
	align-items: flex-start;
	margin: 1.4rem auto;
	max-width: 1300px;
	padding: 3rem;
	border-radius: 10px;
	flex-wrap: wrap;

	.btn-criar-evento {
		background-color: #639280;
		border: unset;
		border-radius: 30px;
		padding: .7rem 2rem;
		font-family: 'Montserrat-Bold';
		color: #fff;
		text-transform: uppercase;
		position: absolute;
		right: 14rem;
		top: 13.4rem;
	}

	.btn-criar-evento:hover {
		background-color: #73b79d;
	}

	.filtrar-data {
		max-width: 300px;
	}

	.cards-container {
		width: 100%;
		display: grid;
		margin: 0 auto;
		grid-template-columns: 1fr 1fr 1fr;
		gap: 48px 32px;

		@media (max-width: 920px) {
			max-width: 520px;
			grid-template-columns: 1fr 1fr;
			margin: 0;
		}

		@media (max-width: 580px) {
			max-width: 280px;
			grid-template-columns: 1fr;
		}
	}

	.pesquisar {
		@media (max-width: 580px) {
			margin-top: 1.5rem;
		}
	}

	#pesquisar {
		border-radius: 40px 0px 0px 40px;
		border: 1px solid #6B6A64;
		border-right: unset;
		padding: .5rem .7rem;
		width: 300px;
	}

	#btn-pesquisar {
		border: 1px solid #6B6A64;
		border-left: unset;
		border-radius: 0 40px 40px 0;
		background-color: #fff;

		img {
			max-width: 26px;
		}
	}

	#pesquisar:focus {
		border-color: #86b7fe;
		outline: 0;
		box-shadow: 0 0 0 .25rem rgba(13, 110, 253, .25);
	}

	.botoes-container {
		button {
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

		.btn-voltar:hover {
			background-color: #ececec;

		}

		.btn-ver-mais {
			background-color: #639280;
			border: unset;
		}

		.btn-ver-mais:hover {
			background-color: #73b79d;
		}
	}
}
</style>