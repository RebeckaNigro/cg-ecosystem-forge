<template>
	<div class="card-noticia-content" :class="[isRelacionada ? 'is-relacionada' : 'not-relacionada']">
		<div class="card-noticia-image"></div>
		<p v-if="!isRelacionada">{{noticia.titulo}}</p>
	</div>
</template>

<script setup lang="ts">
import { IUltimaNoticia } from '../../stores/noticias/types';

  const props = defineProps<{
	noticia: IUltimaNoticia
    isRelacionada: boolean
  }>()
</script>

<style scoped lang="scss">

.is-relacionada {
  width: 50%;
}
.not-relacionada {
  max-width: 385px;
}
.card-noticia-content {
	display: flex;
	flex-direction: column;
	align-items: flex-start;
  max-height: 315px;
  padding: 10px;
  min-width: 260px;

	> p {
		font-size: 24px;
		font-weight: 600;
		text-transform: uppercase;
		margin-bottom: 2px;
	}

	> span {
		text-align: start;
		line-height: 20px;
	}
}
.card-noticia-content:hover {
  cursor: pointer;
  box-shadow: 0 0 30px 5px gray;
}
.card-noticia-image {
	width: 100%;
	height: 220px;
	border: 1px solid black;
	margin-bottom: 16px;
}
</style>
