<template>
	<div class="d-flex justify-content-between align-items-center">
		<FilterComponent field="área" :items="items" type="parceiro" class="w-25" @filter-result="filtrarParceiros" />

		<div class="w-25 align-self-end">
			<SearchComponent
			:items="items"
			type="parceiro"
			@search-result="filtrarParceiros"
			/>
		</div>
	</div>
	<section class="container-cards-parceiros" v-for="item, index in partners" :key="index">
		<header>
			<h1 class="dark-title me-3">{{ item.tipoInstituicao }}</h1>
			<hr class="w-100">
		</header>
		<main>
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

@media (max-width: 768px) {
	section.container-cards-parceiros {
		header {
			h1 {
				font-size: 1.2rem;
			}
		}
	}
}

@media (max-width: 576px) {
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
}
</style>