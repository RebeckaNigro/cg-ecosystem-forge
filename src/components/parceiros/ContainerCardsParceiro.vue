<template>
	<div class="d-flex justify-content-between align-items-center">
		<div class="row w-100 justify-content-between">
			<div class="col-12 col-md-6 filter-container">
				<FilterComponent field="área" :items="items" type="parceiro" @filter-result="filtrarParceiros" />
			</div>
			<div class="col-12 col-md-6 justify-content-end align-self-end filter-container">

				<SearchComponent :items="items" type="parceiro" @search-result="filtrarParceiros" />

			</div>
		</div>
	</div>
	<section class="container-cards-parceiros" v-for="item, index in partners" :key="index">
		<header>
			<div @click="item.toggle = !item.toggle" class="d-flex hover-pointer">
				<h1 class="dark-title">{{ item.tipoInstituicao }}</h1>
				<img src="/parceiros/arrow-down.svg" alt="Mostrar parceiros" class="align-self-start mx-2" :class="item.toggle ? 'reverse': ''" >
			</div>
			<hr class="w-100">
		</header>
		<main v-if="item.toggle">
			<!-- TODO: fix card distance based on amount of cards -->
			<CardParceiro v-for="(parceiro, index) in item.parceiros" :key="index" :card-parceiro="parceiro" />
		</main>

		
	</section>
</template>

<script setup lang="ts">
import CardParceiro from './CardParceiro.vue';
import FilterComponent from '../general/FilterComponent.vue';
import { IPartnerSeccionado } from '../../stores/parceiros/types';
import { onMounted, ref } from 'vue';
import SearchComponent from '../general/SearchComponent.vue';

const props = defineProps<{
	items: IPartnerSeccionado[]
}>()
const partners = ref<IPartnerSeccionado[]>()

onMounted(() => {
	partners.value = props.items
	
})

const filtrarParceiros = (
	parceirosFiltrados: Array<IPartnerSeccionado>
) => {
	partners.value = parceirosFiltrados
}
</script>

<style scoped lang="scss">
.filter-container {
	width: 25%;
}

.reverse{
	transform: scaleY(-1);
}

.container-cards-parceiros {
	header {
		display: flex;
		flex-direction: row;
		width: 100%;
		margin-top: 70px;
		margin-bottom: 10px;

		h1 {
			font-size: 1.5rem;
			white-space: nowrap;
		}
	}

	main {
		display: grid;
		grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
		grid-template-rows: 220px;
		gap: 20px 9%;
	}
}

@media (max-width: 1200px) {
	.filter-container {
		width: 40%;
	}

	section.container-cards-parceiros {
		header {
			h1 {
				font-size: 1.2rem;
			}
		}
	}
}

@media (max-width: 580px) {

	.filter-container {
		width: 100%;
		margin-bottom: 1.5rem;
	}

	.container-cards-parceiros {
		main {
			justify-items: center;
		}

		header>h1 {
			white-space: normal;
		}
	}

	section.container-cards-parceiros {
		header {
			h1 {
				font-size: 1rem;
			}
		}
	}
}</style>