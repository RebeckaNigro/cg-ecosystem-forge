<template>
	<div class="d-flex align-items-start flex-column">

		<label for="filtro-data" class="ms-3 mb-2">Filtrar por {{ contentType }}:</label>
		<div>
			<select name="filtro" id="filtro" class="w-100 boring-gray-border">
				<option>Todos</option>
				<option>23/11/2022</option>
				<option>22/11/2022</option>
				<option>21/11/2022</option>
			</select>
		</div>
	</div>
</template>

<script setup lang="ts">

const props = defineProps<{
	contentType: string,
	datas: string[]
}>()
</script>

<style scoped lang="scss">
label {
	font-family: 'Montserrat-Medium', sans-serif;
}

div{
	width: 100%
}

#filtro {
	padding: .5rem .7rem;
}

#filtro:focus{
	border-color: #86b7fe;
	outline: 0;
	box-shadow: 0 0 0 .25rem rgba(13,110,253,.25);
}
</style>