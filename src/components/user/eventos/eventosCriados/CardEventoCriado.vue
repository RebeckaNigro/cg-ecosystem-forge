<template>
	<div class="card-container box p-3 d-flex flex-column align-items-start">
		<div class="img-container">
			<img :src="buildImageSrc(eventoId, nomeEvento, 5)" alt="event-image" v-if="image !== null">
      		<span v-else class="dark-body-text fs-6">{{ nomeEvento }}</span>
		</div>

		<div class="container">

			<div class="row mt-2">
				<!--<div class="tags col">
					<span class="me-2">#Confra</span>
					<span>#Networking</span>
				</div>-->
	
				<div class="d-flex acoes-container col">
					<button class="visualizar" @click="$router.push({name: 'EventoExpandido', params: {eventoId}})">
						<img src="../../../../../public/view_icon.svg" alt="Visualizar evento">
					</button>
					<!---<button class="editar" @click="$router.push({name: 'GerenciaEvento', params: {eventoId: eventoId}})">
						<img src="../../../../../public/edit_icon.svg" alt="Editar evento">
					</button>-->
					<button class="deletar" @click="handleDeleteEvent(eventoId)">
						<img src="../../../../../public/delete_icon.svg" alt="Excluir evento">
					</button>
				</div>
			</div>
		</div>

		<div class="infos-container">

			<div class="titulo mt-2 mb-1">{{nomeEvento}}</div>
	
			<div class="data-evento">{{friendlyDateTime(dataInicio)}} - {{friendlyDateTime(dataTermino)}}</div>
	
			<div class="endereco-evento">{{enderecoEvento}}</div>
		</div>

		<!--<span class="atualizado-em mt-2">Atualizado em: 01/01/2000 às 12:00</span>-->


	</div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { useEventoStore } from '../../../../stores/eventos/store';
import { buildImageSrc } from '../../../../utils/constantes';
import { friendlyDateTime } from '../../../../utils/formatacao/datetime';


const props = defineProps<{
	hasImage: boolean
    image: string
    nomeEvento: string
    dataInicio: string
	dataTermino: string
    enderecoEvento: string
	eventoId: number
}>()

const eventoStore = useEventoStore()

const handleDeleteEvent = async (eventoId: number) => {
	console.log("askdhakd")
	await eventoStore.deleteEvent(eventoId)
	window.location.reload()
  }
onMounted(() => {
	console.log(props.hasImage);
	
})


</script>

<style lang="scss">
.card-container {
	display: flex;
	background: rgba(255, 255, 255, 0.85);
	border: 1px solid #6B6A64;
	box-shadow: 0px 0px 50px rgba(0, 0, 0, 0.15);
	border-radius: 10px;
	cursor: pointer;

	flex-direction: column;
	align-items: flex-start;
	padding: 10px;
	width: 95%;

	.img-container {
      height: 200px;
	  min-height: 200px;
	  width: 100%;
      display: flex;
      justify-content: center;
      align-items: center;
	  
      img {
        width: -webkit-fill-available;
      }
    }

	.tags {
		color: #505050;
		font-size: 14px;
		display: flex;
		flex-direction: row;
		padding-left: 0;

	}

	.acoes-container {
		flex-wrap: wrap;
		flex-direction: row;
		width: 100%;
		justify-content: flex-end;
		padding-right: 0;

		button {
			background-color: transparent;
			border: unset;
		}

		button:hover{
			box-shadow: 0px 0px 50px rgba(0, 0, 0, 0.20);
		}

		img {
			max-width: 26px;
			max-height: 26px;
		}

	}

	.infos-container{
		display: flex;
		flex-direction: column;
		align-items: flex-start;
	
		.titulo {
			white-space: nowrap;
			text-overflow: ellipsis;
			overflow: hidden;
			text-align: start;
			font-family: 'Montserrat-SemiBold';
			font-size: 18px;
			color: #000;
		}
		.data-evento, .endereco-evento {
			  background-repeat: no-repeat;
			  background-position-y: center;
			  background-size: contain;
			  margin: 5px 0;
			  padding-left: 30px;
			  flex-wrap: wrap;
			}
		.data-evento, .endereco-evento {
			  white-space: nowrap;
			  text-overflow: ellipsis;
			  overflow: hidden;

			}
	
		.data-evento{
			background-image: url('/public/calendar_icon.svg');
			font-size: 14px;
			
		}
	
		.endereco-evento{
			background-image: url('/public/location_icon.svg');
			font-size: 13px;
			@media (max-width: 920px) {
				font-size: 12px;
			}
		}
	}


	.atualizado-em {
		color: #505050;
		font-size: 12px;
		margin-bottom: 0;
	}

	@media (max-width:800px){
		width: 300px;
	}

	

}
</style>