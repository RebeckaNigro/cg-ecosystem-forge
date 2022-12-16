<template>

	<section id="editais-container" class="container-fluid">

		<!--
		Filtros e pesquisa
	-->
		<main class="ghp row mt-5 mb-5">

			<div class="d-flex w-100 mb-4">
				<div>
					<a class="dropdown-toggle" href="#" @click="novosEditaisToggle">
						Novos editais
					</a>
				</div>
				
				<hr class="w-100 ms-3">
			</div>

			<div v-if="isNovosEditaisVisible">
				<div v-for="card, index of dummyEdicts" >
					<CardEdital/>
				</div>
	
				<GeneralBtn
					btnText="Ver mais"
					:isExternalLink="false"
					link=""
					bgColor="#639280"
					width="150px"
					textColor="#fff"
					height="45px"
					id="ver-mais"
				/>
			</div>

			<div class="d-flex w-100 mb-4">
				<div>
					<a class="dropdown-toggle" href="#">
						Em andamento
					</a>
				</div>
				
				<hr class="w-100 ms-3">
			</div>

			<div class="d-flex w-100 mb-4">
				<div>
					<a class="dropdown-toggle" href="#">
						Concluídos
					</a>
				</div>
				
				<hr class="w-100 ms-3">
			</div>

		</main>
	</section>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue';
import CardEdital from '../../../components/documentos/editais/CardEdital.vue';
import GeneralBtn from '../../../components/buttons/GeneralBtn.vue';

const isNovosEditaisVisible = ref(true)

const dummyEdicts = reactive([
	{
		title: 'NOME DO EDITAL',
		description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
		downloadPath: ''
	},
	{
		title: 'NOME DO EDITAL',
		description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
		downloadPath: ''
	},
	{
		title: 'NOME DO EDITAL',
		description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
		downloadPath: ''
	},
	{
		title: 'NOME DO EDITAL',
		description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
		downloadPath: ''
	}
	
])

const novosEditaisToggle = () => {
	isNovosEditaisVisible.value = !isNovosEditaisVisible.value
}

</script>

<style scoped lang="scss">

#editais-container{

	.dropdown-toggle{
		text-decoration: none;
		color: #000;
		font-family: 'Montserrat-SemiBold', sans-serif;
		font-size: 1.4rem;
	}
}
</style>