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
import FooterComponent from '../../../components/general/FooterComponent.vue';
import NavBar from '../../../components/general/NavBar.vue';
import ContainerCardsEventosCriados from '../../../components/user/eventos/eventosCriados/ContainerCardsEventosCriados.vue';

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
</style>