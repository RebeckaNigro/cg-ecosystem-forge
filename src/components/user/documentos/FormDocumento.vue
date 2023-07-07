<template>
	<h1 class="dark-title mt-5 mb-5 fs-2 text-center" v-if="documento.id < 0">
		ENVIE SEU DOCUMENTO!
	</h1>
	<h1 class="dark-title mt-5 mb-5 fs-2 text-center" v-else>
		EDITE SEU DOCUMENTO!
	</h1>
	<form class="container box p-5 mb-5">
		<!-- NOME -->
		<div class="mb-3">
			<label for="nome" class="form-label-primary">Nome do documento*</label>
			<input type="text" id="nome" class="form-input-primary" :class="v$.nome.$error ? 'is-invalid' : ''"
				v-model="documento.nome" />
			<div v-if="v$.nome.$error" class="invalid-feedback">
				{{ v$.nome.$errors[0].$message }}
			</div>
		</div>

		<!-- DESCRIÇÃO -->
		<div class="mb-3">
			<label for="descricao" class="form-label-primary">Descrição do documento</label>
			<input type="text" id="descricao" class="form-input-primary" v-model="documento.descricao" />
		</div>

		<!-- SESSÃO -->
		<div class="mb-3">
			<label for="descricao" class="form-label-primary">Sessão*</label>
			<select class="form-input-primary" id="descricao" name="descricao" v-model="documento.documentoAreaid">
				<option value="1">Pesquisa</option>
				<option value="2">Edital</option>
				<option value="3">Lei</option>
			</select>
		</div>

		<!-- INSTITUIÇÕES -->
		<div class="mb-3">
			<label for="tags" class="form-label-primary">Instituição responsável*</label>
			<AutocompleteComponent type="Instituicao" :items="instituicoes" @selected-value="setInstituicao"
				:class="v$.instituicaoId.$error ? 'is-invalid' : ''" />
			<div v-if="v$.instituicaoId.$error" class="invalid-feedback">
				{{ v$.instituicaoId.$errors[0].$message }}
			</div>
		</div>

		<div class="container-fluid text-start" v-if="selectedValue">
			<div class="tag-element mx-1 mb-4">
				<span>{{ selectedValue.razaoSocial }}</span>
				<img src="/icons/close-white.svg" alt="Remover Instituicao" @click="deleteSelectedInstituicao" />
			</div>
		</div>

		<!-- TAGS -->
		<div class="mb-3">
			<label for="tags" class="form-label-primary">Tags (Opcional)</label>
			<AutocompleteComponent type="CustomTag" :items="allTags" @selected-value="addTag" />
		</div>

		<div class="container-fluid text-start">
			<div class="tag-element mx-1 mb-4" v-for="(tag, index) in documento.tags" :key="index">
				<span>{{ tag.descricao }}</span>
				<img src="/icons/close-white.svg" alt="Remover tag" @click="deleteTag(index)" />
			</div>
		</div>

    <!-- ARQUIVO -->
    <div class="mb-3 mt-4">
      <label for="arquivo" class="form-label-primary">Upload do Arquivo*</label>
      <div class="imagem-divulgacao">
		<div class="row align-items-center">
			<div class="col-12 col-md">
			  <label for="imagem-input" class="borda-cinza">
				SELECIONE OU ARRASTE O DOCUMENTO PARA ENVIAR
				<br>
				Tipo de arquivo suportado: pdf
			  </label>
			  <input
				class="form-input-primary"
				type="file"
				name="imagem-input"
				accept=".pdf"
				id="imagem-input"
				@change="(e) => onFileChanged(e)"
			  />
			</div>
			<div class="col-12 col-md">
				<span id="nome-imagem" class="form-label-primary">{{ fileName }}</span>
			</div>
		</div>
      </div>
    </div>

		<Spinner v-if="sendingDocs" />

		<div class="row">
			<div class="col-sm-6 mt-md-3">
				<button type="button" class="green-btn-outlined w-100 mt-4 mb-3 my-md-4" @click="resetarDocumento">
					CANCELAR
				</button>
			</div>

			<div class="col-sm-6 fs-xs-1 mt-md-3">
				<button type="button" class="green-btn-primary w-100 my-3 my-md-4" @click="cadastrar">
					ENVIAR
				</button>
			</div>
		</div>
	</form>

	<ConfirmModal v-show="confirmStore.visible" @confirm-true="confirmado" element-id="confirmModal" id="confirmModal" />
</template>

<script setup lang="ts">
//@ts-nocheck

import useValidate from "@vuelidate/core"
import { ref, onMounted } from "vue"
import { useRoute } from "vue-router"
import { useDocumentStore } from "../../../stores/documentos/store"
import { useModalStore } from "../../../stores/modal/store"
import { useAlertStore } from "../../../stores/alert/store"
import { useConfirmStore } from "../../../stores/confirm/store"
import { useTagStore } from "../../../stores/tag/store"
import { useInstituicaoStore } from "../../../stores/instituicao/store"
import { required, helpers, minValue } from "@vuelidate/validators"
import ConfirmModal from "../../general/ConfirmModal.vue"
import AutocompleteComponent from "../../general/AutocompleteComponent.vue"
import Spinner from "../../general/Spinner.vue"
import { CustomTag } from "../../../stores/tag/types"
import { Instituicao } from "../../../stores/instituicao/types"

const documentStore = useDocumentStore()
const alertStore = useAlertStore()
const modalStore = useModalStore()
const confirmStore = useConfirmStore()
const tagStore = useTagStore()
const instituicaoStore = useInstituicaoStore()
const route = useRoute()

const sendingDocs = ref(false)
const arquivo = ref<File | null>()
const selectedValue = ref<Instituicao>()
const instituicoes = ref<Array<Instituicao>>()

const documento = ref({
	id: -1,
	nome: "",
	descricao: "",
	tags: new Array<CustomTag>(),
	documentoAreaid: 1,
	instituicaoId: -1,
	data: "",
	arquivo: File
})

const fileSizeValidation = () => {
	if (!arquivo.value) return false
	return arquivo.value.size < 5 * 1024 * 1024
}

const documentoRules = ref({
	nome: {
		required: helpers.withMessage("Título é obrigatório.", required)
	},
	instituicaoId: {
		minValueValue: helpers.withMessage(
			"Instituição é obrigatória.",
			minValue(0)
		)
	}
})

const v$ = useValidate(documentoRules, documento)

const allTags = ref<CustomTag[]>()
const fileName = ref("")

onMounted(async () => {
	const id = route.params.documentoId

	await buscarTags()
	await buscarInstituicoes()

	if (id) {
		await documentStore.getDocDetailsById(Number(id))
		if (documentStore.response.code == 200) {
			documento.value = documentStore.response.dado

			selectedValue.value = instituicoes.value?.find(
				(item) => item.id == documento.value.instituicaoId
			)

		} else {
			alertStore.showTimeoutErrorMessage("Erro ao carregar documento!")
		}

	}


})


const confirmado = () => {
	// confirmStore.closeConfirm()
	// noticia.value.dataPublicacao = dateAndTimeToDatetime(data.value, hora.value)
	// let rascunho = new NoticiaRascunho(noticia.value)
	// localStorage.setItem("noticiaRascunho", JSON.stringify(rascunho))
	// modalStore.showSuccessModal("Rascunho salvo com sucesso!")
}

const buscarTags = async () => {
	await tagStore.getTags()
	allTags.value = tagStore.allTags
}

const buscarInstituicoes = async () => {
	await instituicaoStore.getInstituicoes()

	if (instituicaoStore.response.code == 200) {
		instituicoes.value = instituicaoStore.allInstituicoes
	} else {
		alertStore.showTimeoutErrorMessage("Erro ao carregar instituições!")
	}
}

const onFileChanged = (event: Event) => {
	const target = event.target as HTMLInputElement
	if (target && target.files) {
		arquivo.value = target.files[0]
		fileName.value = target.files[0].name
	}

	if (!fileSizeValidation()) {
		alertStore.showTimeoutWarningMessage("Imagem deve ter no máximo 5MB")
		arquivo.value = null
		fileName.value = ""
		return
	}
}

const cadastrar = async () => {
	const formValidation = await v$.value.$validate()

	documento.value.data = new Date().toISOString()
	documento.value.arquivo = arquivo.value

	if (documento.value.id > 0) {
		sendingDocs.value = true
		await documentStore.putDoc(documento.value)
		sendingDocs.value = false
		const res = documentStore.response.getResponse()
		if (res.code === 200) {
			resetarDocumento()
			modalStore.showSuccessModal("Documento editado com sucesso!")
		} else if (res.code === 661) {
			console.error(res.message)
		} else {
			modalStore.showErrorModal("Erro ao editar documento!")
		}
	} else {
		sendingDocs.value = true
		await documentStore.postDocument(documento.value)
		sendingDocs.value = false
		const res = documentStore.response.getResponse()
		console.log(res);

		if (res.code === 200) {
			resetarDocumento()
			modalStore.showSuccessModal("Documento cadastrado com sucesso!")
		} else if (res.code === 661) {
			console.error(res.message)
		} else {
			modalStore.showErrorModal("Erro ao cadastrar documento!")
		}
	}
}

const resetarDocumento = () => {
	documento.value = {
		id: -1,
		nome: "",
		descricao: "",
		tags: [],
		documentoAreaid: -1,
		instituicaoId: -1,
		data: "",
		arquivo: File
	}
	fileName.value = ""
}

const deleteTag = (index: number) => {
	documento.value.tags.splice(index, 1)
}

const addTag = (selectedTag: CustomTag) => {
	const tagFound = documento.value.tags.find((tag: CustomTag) => {
		return tag.descricao == selectedTag.descricao
	})

	if (!tagFound) {
		documento.value.tags.push(selectedTag)
	}
}

const setInstituicao = (selectedInstituicao: Instituicao) => {
	selectedValue.value = selectedInstituicao
	if (selectedInstituicao)
		documento.value.instituicaoId = selectedInstituicao.id
}

const deleteSelectedInstituicao = () => {
	selectedValue.value = undefined
	documento.value.instituicaoId = -1
}
</script>

<style scoped lang="scss"> form {
 	max-width: 1000px;
 }

  .imagem-divulgacao div {
    margin-top: 0;
    width: fit-content;
  }
  .imagem-divulgacao label {
    height: 200px;
    width: 100%;
    max-width: 400px;
    margin-left: 0;
	padding-top: 80px;
    background-color: #fff;
    background-image: url("/user/eventos/cloud_icon.svg");
    background-repeat: no-repeat;
    background-position: 50% 20%;
	cursor: pointer;
  }

 input#imagem-input {
 	height: 0;
 	padding: 0;
 	border: 0;
 }

 span#nome-imagem {
 	align-self: center;
 	text-align: center;
 }

 .max-image-width {
 	max-width: 400px;
 }
</style>
