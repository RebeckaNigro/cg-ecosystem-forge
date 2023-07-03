<template>
	<div class="card-container d-flex flex-column justify-content-evenly align-items-start p-3">
		<div class="d-flex justify-content-between w-100">
			<span v-for="tag, index in documento.tags" :key="index" class="font-light">
				#{{ tag.descricao }}
			</span>
			<img src="/public/view_icon.svg" alt="Visualizar documento" class="hover-pointer"
				@click="$router.push({ name: 'DocumentoExpandido', params: { documentoId: documento.id } })">
		</div>
		<div class="nome-documento">{{ documento.nome }}</div>
		<div class="nome-autor font-normal">{{ documento.autor }}</div>
		<div class="descricao-documento font-normal text-start">{{ documento.descricao }}</div>
		<div class="atualizacao text-start font-normal">Atualizado em: {{ friendlyDateTime(documento.ultimaOperacao) }}
		</div>
	</div>
</template>

<script setup lang="ts">
import { IDocumentoSimplificado } from '../../stores/documentos/types';
import { friendlyDateTime } from '../../utils/formatacao/datetime';

const props = defineProps<{
	documento: IDocumentoSimplificado
}>()
</script>

<style scoped lang="scss">
.card-container {
	background-color: #fff;
	border: 1px solid #6B6A64;
	border-radius: 10px;
	height: 270px;
	margin: 10px 0;


	.nome-documento {
		font-weight: 600;
		font-size: 28px;
		text-align: start;
	}

	.nome-autor {
		font-size: 20px;
	}

	.descricao-documento {
		font-size: 16px;
	}

	.atualizacao {
		font-size: 15px;
	}
}

@media screen and (max-width: 580px) {
	.card-container {
		.nome-documento {
			font-size: 24px;
		}

		.nome-autor {
			font-size: 18px;
		}

		.descricao-documento {
			font-size: 14px;
		}

		.atualizacao {
			font-size: 13px;
		}
	}
}</style>