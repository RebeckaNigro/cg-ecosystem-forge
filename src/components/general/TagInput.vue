<template>

	<input
		id="input-tag"
		type="text"
		class="boring-gray-border form-control"
		v-model="tag"
		@keydown="addTag"
	/>
	<div class="tags-container">

		<div v-for="(tag, index) in tags"
		:key="tag" class="tag">
			<span>{{ tag }}</span>
			<img src="/close_icon.svg" alt="Remover tag" class="remove-tag ms-1" @click="removeTag(index)">
		</div>
	</div>

</template>

<script setup lang="ts">
import { ref } from 'vue';

const tags = ref<Array<string>>([])
const tag = ref('')

const addTag = (event: KeyboardEvent) => {
	if(event.code == 'Comma' || event.code == 'Enter'){
		event.preventDefault()
		let inputValue =  tag.value.trim()
		if(inputValue.length > 0){
			tags.value.push(inputValue)
			tag.value = ''
		}
	}
}

const removeTag = (tagIndex: number) => {
	tags.value.splice(tagIndex, 1)
}
</script>

<style scoped lang="scss">

input{
	padding: 10px;
}

.tags-container{
	margin-left: 1rem;
}

.tag{
	background-color: #9C9B9B;
	width: fit-content;
	padding: .6rem 1rem;
	border-radius: 10px;
	color: #fff;
	float: left;
	font-family: 'Montserrat-Medium';
	margin: 1rem;
}

.remove-tag{
	max-width: 24px;
	cursor: pointer;
}

</style>