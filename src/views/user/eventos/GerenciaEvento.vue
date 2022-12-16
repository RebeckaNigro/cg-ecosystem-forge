<template>
	<div class="background pb-5">
		<NavBar 
		:is-transparent="false"
		/>
		<div v-if="eventoId">
			<h1 class="mt-5 mb-5 dark-title m-auto fs-2">Edite seu evento</h1>
			<FormEditarEvento :eventoId="eventoId"/>
		</div>
		<div v-else>
			<h1 class="mt-5 mb-5 dark-title m-auto fs-2">Crie seu evento!</h1>
			<FormEvento :eventoId="eventoId" />
		</div>
	</div>
	<FooterComponent />
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import NavBar from '../../../components/general/NavBar.vue';
import FormEditarEvento from '../../../components/user/eventos/FormEditarEvento.vue';
import FormEvento from '../../../components/user/eventos/FormEvento.vue'
import router from '../../../router';
const eventoId = ref<number | null>(null)
onMounted(() => {
	if(parseInt(router.currentRoute.value.params.eventoId.toString()) != 0){
		eventoId.value = parseInt(router.currentRoute.value.params.eventoId.toString())
	}
})
</script>

<style scoped>

.background{
	background-color: #fff;
	background-image: url("background.svg") ;
	background-size: cover;
	width: 100%;
}
</style>