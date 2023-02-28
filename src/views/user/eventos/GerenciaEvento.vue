<template>
	<div class="pb-5">
		<div v-if="eventoId">
			<h1 class="mt-5 mb-5 dark-title m-auto fs-2">Edite seu evento</h1>
			<FormEditarEvento :eventoId="eventoId"/>
		</div>
		<div v-else>
			<h1 class="mt-5 mb-5 dark-title m-auto fs-2">Crie seu evento!</h1>
			<FormEvento/>
		</div>
	</div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import FormEditarEvento from '../../../components/user/eventos/FormEditarEvento.vue';
import FormEvento from '../../../components/user/eventos/FormEvento.vue'
import router from '../../../router';
const eventoId = ref<number | null>(null)
onMounted(() => {
	if(router.currentRoute.value.query.eventoId){
		eventoId.value = parseInt(router.currentRoute.value.query.eventoId.toString())
	}
})
</script>

<style scoped>

</style>