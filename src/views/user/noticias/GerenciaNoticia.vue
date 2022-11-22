<template>
	<div class="background pb-5">
		<NavBar :is-transparent="false" />
		<header>
			<h1 class="dark-title mt-5 mb-5 fs-2">Envie sua notícia!</h1>
		</header>
		<main>
			<FormNoticia />
		</main>
	</div>
	<FooterComponent />
 
</template>

<script setup lang="ts">
import FormNoticia from '../../../components/user/noticias/FormNoticia.vue';
</script>

<style scoped lang="scss">

.background{
	background-color: #fff;
	background-image: url("background.svg") ;
	background-size: cover;
	width: 100%;
}
</style>