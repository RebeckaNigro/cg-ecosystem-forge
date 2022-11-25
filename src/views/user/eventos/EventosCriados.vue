<template>
	<NavBar
	:is-transparent="false"
	/>
	<div class="background pb-5 pt-5">
		<div class="eventos-breadcrumb">

			<nav style="--bs-breadcrumb-divider:'>';" aria-label="breadcrumb">
				<ol class="breadcrumb">
					<li class="breadcrumb-item unactive" @click="$router.push({name: 'Eventos'})">Eventos</li>
					<li class="breadcrumb-item active" aria-current="page">Eventos criados</li>
				</ol>
			</nav>
		</div>

		<ContainerCardsEventosCriados/>
	</div>

	<FooterComponent/>

</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import FilterComponent from '../../../components/general/FilterComponent.vue';
import FooterComponent from '../../../components/general/FooterComponent.vue';
import NavBar from '../../../components/general/NavBar.vue';
import SearchComponent from '../../../components/general/SearchComponent.vue'
import CardEventoCriado from '../../../components/user/eventos/eventosCriados/CardEventoCriado.vue';
import ContainerCardsEventosCriados from '../../../components/user/eventos/eventosCriados/ContainerCardsEventosCriados.vue';
import { useEventoStore } from '../../../stores/eventos/store';
import { IUltimoEvento } from '../../../stores/eventos/types';

const eventoStore = useEventoStore()
const lastIndex = ref(6)
let eventos = ref<Array<IUltimoEvento>>(eventoStore.eventos.slice(0, lastIndex.value))

const addEventsToView = () => {
	lastIndex.value += 3
	eventos.value = eventoStore.eventos.slice(0, lastIndex.value)
}

onMounted(() => {
	eventoStore.getAllEvents()
})

</script>

<style scoped lang="scss">
.background{
	background-image: url('../../../../public/user/background.svg');
	background-repeat: no-repeat;
	background-size: cover;
	width: 100%;

	.eventos-breadcrumb{
		display: flex;
		width: fit-content;
		margin-left:15%;
		font-size: 20px;

		.active{
			font-family: 'Montserrat-Medium', sans-serif;
		}

		.unactive{
			cursor: pointer;
			font-family: 'Montserrat-Light', sans-serif;
		}

		@media(max-width: 920px){
			font-size: 17px;
		}
	}
}
.eventos-container {
	display: flex;
	flex-direction: column;
	align-items: flex-start;
	margin: 1.4rem auto;
	max-width: 1300px;
	padding: 3rem;
	border-radius: 10px;
	flex-wrap: wrap;

	.btn-criar-evento{
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

	.btn-criar-evento:hover{
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