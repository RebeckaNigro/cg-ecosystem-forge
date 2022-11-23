<template>
	<form class="form-noticia form-box dark-body-text">
		<div class="group column-direction">
			<label for="title">Título</label>
			<input type="text" id="title" class="w-100 boring-gray-border form-control" v-model="noticia.titulo">
		</div>
		<div class="group column-direction">
			<label for="title">Subtítulo</label>
			<input type="text" id="subtitle" class="w-100 boring-gray-border form-control"
				v-model="noticia.subTitulo">
		</div>
		<div class="group column-direction">
			<label>Tags</label>
			<TagInput/>
		</div>
		<div class="group column-direction ms-4 dates">

			<div class="labels-date">Publicado em:</div>
			<div class="group row-direction align-center">
				<input type="radio" id="auto-date" class="boring-gray-border " name="data-publicacao" v-model="autoSetDate"
					:value="true" checked>
				<label for="auto-date" class="ms-3">Automático</label>
			</div>
			<div class="group row-direction align-center">
				<input type="radio" id="confirm-chosen-date" class="boring-gray-border" name="data-publicacao"
					v-model="autoSetDate" :value="false">
			<div class="date">
				<input type="date" name="data-publicacao" id="data-publicacao" class="boring-gray-border"
					v-model="dataPublicacao" :disabled="autoSetDate">
			</div>
			<span>às</span>
			<div class="time">
				<input type="time" name="hora-publicacao" id="hora-publicacao" class="boring-gray-border"
					v-model="horaPublicacao" :disabled="autoSetDate" required>
			</div>
			
		</div>
			<!-- <input type="date" name="calendar" id="chosen-date" class="boring-gray-border" v-model="userChosedDate">
      <input type="time" name="chosen-time" id="chosen-time"> -->
			
		</div>
		<div class="group column-direction mb-5">
			<label for="main-text">Corpo do texto</label>
			<textarea name="main-text" id="main-text" cols="30" rows="10" class="w-100 boring-gray-border form-control"
				v-model="noticia.descricao" />
		</div>
		<!--<input type="file" name="news-cover" id="news-cover" placeholder="Capa da Notícia">-->
		<div class="btn-container justify-content-around">
			<button class=" btn-salvar-rascunho">Salvar rascunho</button>
			<button class="btn-pre-visualizar">Pré-visualizar</button>
			<button v-if="!sendingNews" type="button" class="btn-enviar" @click.prevent="handleSubmit()">Enviar</button>
			<div v-else class="spinner-border text-success ml-auto mt-2" role="status">
				<span class="visually-hidden">Loading...</span>
			</div>

		</div>
	</form>
	<ModalComponent
		:title="modalSettings.title"
		:message="modalSettings.message"
		:status="modalSettings.status"
		:modal-id="modalSettings.id"
	/>
</template>

<script setup lang="ts">
import { Modal } from 'bootstrap';
import ModalComponent from '../../../components/general/ModalComponent.vue'
import { inject, reactive, ref } from 'vue';
import { useNoticiaStore } from '../../../stores/noticias/store';
import { INoticia } from '../../../stores/noticias/types';
import TagInput from '../../general/TagInput.vue';

const autoSetDate = ref(true)
const userChosedDate = ref('')
const sendingNews = ref(false)
const noticiaStore = useNoticiaStore();
const userId = inject('userId', '')
//noticiaStore.novaNoticia.id = userId

const noticia: INoticia = reactive({
	id: '1', // provisório até mudar tipo do id no banco de number para string
	titulo: '',
	descricao: '',
	subTitulo: '',
	dataPublicacao: ''
})

const dataPublicacao = ref('')
const horaPublicacao = ref('')

const handleSubmit = async () => {
	sendingNews.value = true
	//const file: HTMLInputElement = document.querySelector('#news-cover')!;

	if (autoSetDate.value) noticia.dataPublicacao = new Date().toISOString()
	else noticia.dataPublicacao = new Date(dataPublicacao.value.concat('T', horaPublicacao.value) + ':00.000Z').toISOString()

	sendingNews.value = await noticiaStore.postNews(noticia)

	const res = noticiaStore.response.getResponse()
	if(res.code === 200){
		openModal('novaNoticiaRes', 'Sucesso', res.message, 'success')
	}else if( res.code === 661){
		console.error(res.message)
	}else{
		openModal('novaNoticiaRes', 'Falha', res.message, 'warning')
	}
}

const modalSettings = reactive({
    title: '',
    message: '',
    status: '',
    id: 'novaNoticiaRes'
  })
  const openModal = (modalId: string, mt: string, mm: string, ms: string) => {
    modalSettings.id = modalId
    modalSettings.title = mt
    modalSettings.message = mm
    modalSettings.status = ms
    const modalDOM: any = document.querySelector('#' + modalId)
    const bsModal = Modal.getOrCreateInstance(modalDOM)!
    bsModal.show()
  }


</script>

<style scoped lang="scss">

.dark-body-text{
	font-size: 1.2rem;
	color: #6B6A64;
}
form.form-noticia {
	display: flex;
	flex-direction: column;
	align-items: flex-start;
	justify-content: space-between;
	max-width: 1000px;
	margin: 1.4rem auto;
	padding: 4rem;

	label{
		margin-left: 1.6rem;
	}

	input{
		padding: 10px;
	}

	.group {
		display: flex;
		align-items: flex-start;
		width: 100%;
		margin-top: 2rem;
	}

	.column-direction {
		flex-direction: column;
	}

	.row-direction {
		flex-direction: row;
	}

	.align-center {
		align-items: center;
	}

	.btn-container {
		margin: 15px 0;
		width: 100%;

		button {
			margin-bottom: 1.5rem;
			margin-right: 1rem;
			margin-left: 1.2rem;
			border-radius: 30px;
			padding: .5rem 1.5rem;
			font-family: 'Montserrat-Bold';
			color: #fff;
			text-transform: uppercase;
			display: inline-block;
		}

		.btn-salvar-rascunho{
			background-color: #9C9B9B;
			border: 1px solid #9C9B9B;
		}

		.btn-pre-visualizar{
			background-color: #fff;
			border: 1px solid #639280;
			color: #639280;
		}

		.btn-enviar{
			background-color: #639280;
			border: 1px solid #639280;
		}

	}

	@media (max-width: 400px){
		
			label{
				margin-left: 0;
			}
			.dates{
				margin-left: 0!important;
			}

			.btn-container{
				button{
					font-size: 1.1rem;
					padding: .5rem 1rem;
				}
		
			}	
	}
}
</style>