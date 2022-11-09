<template>
	<form class="form-noticia boring-gray-border" @submit.prevent="handleSubmit()">
		<div class="group column-direction">
			<label for="title">Título</label>
			<input type="text" id="title" class="w-100 boring-gray-border" v-model="noticiaStore.novaNoticia.titulo">
		</div>
		<div class="group column-direction">
			<label for="title">Subtítulo</label>
			<input type="text" id="subtitle" class="w-100 boring-gray-border"
				v-model="noticiaStore.novaNoticia.subTitulo">
		</div>
		<div class="labels-date">Publicado em</div>
		<div class="group row-direction align-center">
			<input type="radio" id="auto-date" class="boring-gray-border" name="data-publicacao" v-model="autoSetDate"
				:value="true" checked>
			<label for="auto-date">Automático</label>
		</div>
		<div class="group row-direction align-center">
			<input type="radio" id="confirm-chosen-date" class="boring-gray-border" name="data-publicacao"
				v-model="autoSetDate" :value="false">
			<!-- <input type="date" name="calendar" id="chosen-date" class="boring-gray-border" v-model="userChosedDate">
      <input type="time" name="chosen-time" id="chosen-time"> -->
			<input type="datetime-local" name="chosen-date" id="chosen-date" class="boring-gray-border"
				v-model="userChosedDate">
		</div>
		<div class="group column-direction">
			<label for="main-text">Corpo do texto</label>
			<textarea name="main-text" id="main-text" cols="30" rows="10" class="w-100"
				v-model="noticiaStore.novaNoticia.descricao" />
		</div>
		<input type="file" name="news-cover" id="news-cover" placeholder="Capa da Notícia">
		<div class="btn-container">
			<button type="submit" class="btn-primary btn">Salvar rascunho</button>
			<button type="submit" class="btn-primary btn">Pré-visualizar</button>
			<button type="button" class="btn-primary btn" @click.prevent="handleSubmit()">Enviar</button>
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

const autoSetDate = ref(true)
const userChosedDate = ref('')
// TODO sendingNews ref
const noticiaStore = useNoticiaStore();
const userId = inject('userId', '')
noticiaStore.novaNoticia.id = userId

const handleSubmit = async () => {
	//const file: HTMLInputElement = document.querySelector('#news-cover')!;

	if (autoSetDate.value) noticiaStore.novaNoticia.dataPublicacao = new Date().toISOString()
	else noticiaStore.novaNoticia.dataPublicacao = new Date(userChosedDate.value + ':00.000Z').toISOString()
	noticiaStore.novaNoticia.id = '1' // provisório até mudar tipo do id no banco de number para string
	await noticiaStore.putNews()

	const res = noticiaStore.novaNoticiaResponse.getResponse()
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
form.form-noticia {
	display: flex;
	flex-direction: column;
	align-items: flex-start;
	justify-content: space-between;
	padding: 30px;
	height: 750px;
	width: 800px;
	margin: 3rem auto;

	.group {
		display: flex;
		align-items: flex-start;
		width: 100%;
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
		margin: 15px auto;

		button {
			margin: 0 10px;
			border-radius: 25px;
		}
	}
}
</style>