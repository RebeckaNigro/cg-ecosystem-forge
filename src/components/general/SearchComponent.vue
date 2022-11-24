<template>
	<input type="text" name="pesquisar" id="pesquisar" placeholder="Pesquisar">
	<button class="btn" id="btn-pesquisar">
		<img src="../../../public/search_icon.svg" alt="Pesquisar">
	</button>
</template>

<script setup lang="ts">
</script>

<style lang="scss">
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
</style>