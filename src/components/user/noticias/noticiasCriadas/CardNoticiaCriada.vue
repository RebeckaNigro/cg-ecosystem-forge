<template>
	<div class="card-container box mt-4 p-3 d-flex flex-column align-items-start">
		<div class="img-container">
			<img :src="image" alt="event-image" v-if="hasImage">
      		<span v-else class="dark-body-text fs-6">{{ tituloNoticia }}</span>
		</div>

		<div class="container">

			<div class="row mt-2">
				<div class="tags col">
					<span class="me-2">#Pesquisas</span>
					<span>#Doutorado</span>
				</div>
	
				<div class="d-flex acoes-container col">
					<button class="visualizar" @click="$router.push({ name: 'NoticiaExpandida', params: { noticiaId: idNoticia } })">
						<img src="../../../../../public/view_icon.svg" alt="">
					</button>
					<!--<button class="editar">
						<img src="../../../../../public/edit_icon.svg" alt="">
					</button>-->
					<button class="deletar">
						<img src="../../../../../public/delete_icon.svg" alt="">
					</button>
				</div>
			</div>
		</div>

		<p class="titulo mt-2">{{tituloNoticia}}</p>

		<span class="atualizado-em">Atualizado em: 01/01/2000 às 12:00</span>


	</div>
</template>

<script setup lang="ts">
const props = defineProps<{
	hasImage: boolean
    image: string
    tituloNoticia: string
	idNoticia: number
}>()
</script>

<style scoped lang="scss">
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
	

	@media (max-width:800px){
		width: 300px;
	}

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
		margin-right: 0;
		button {
			background-color: transparent;
			border: unset;
		}

		img {
			max-width: 26px;
			max-height: 26px;
		}



	}

	.titulo {
		max-width: 300px;
		text-align: start;
		font-family: 'Montserrat-SemiBold';
		font-size: 18px;
		color: #000;
	}

	.atualizado-em {
		color: #505050;
		font-size: 12px;
	}

}
</style>