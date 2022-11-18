<template>
	<section class="gerencia-container">
		<NavBar 
		:is-transparent="false"
		/>
		<h1 class="mt-5 mb-5 dark-title m-auto title">Crie seu evento!</h1>
		<FormEvento />
		<FooterComponent />
	</section>
</template>

<script setup lang="ts">
import NavBar from '../../../components/general/NavBar.vue';
import FormEvento from '../../../components/user/eventos/FormEvento.vue'

</script>

<style scoped>
.gerencia-container{
	background-color: #fff;
}
.title{
		font-size: 32px;
	}
</style>