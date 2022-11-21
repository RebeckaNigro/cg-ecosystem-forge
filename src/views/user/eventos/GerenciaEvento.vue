<template>
	<div class="background pb-5">
		<NavBar 
		:is-transparent="false"
		/>
		<h1 class="mt-5 mb-5 dark-title m-auto title">Crie seu evento!</h1>
		<FormEvento/>
	</div>
	<FooterComponent />
</template>

<script setup lang="ts">
import NavBar from '../../../components/general/NavBar.vue';
import FormEvento from '../../../components/user/eventos/FormEvento.vue'

</script>

<style scoped>

.background{
	background-color: #fff;
	background-image: url("background.svg") ;
	background-size: cover;
	width: 100%;
}
.title{
		font-size: 32px;
	}
</style>