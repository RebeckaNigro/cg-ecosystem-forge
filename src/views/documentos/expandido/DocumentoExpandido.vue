<template>
	<div class="pt-5 pb-5 container">

		<div class="evento-breadcrumb">
			<nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
				<ol class="breadcrumb">
					<li class="breadcrumb-item unactive" @click="$router.push({ name: 'DocumentosCriados' })">
						Documentos
					</li>
					<li class="breadcrumb-item unactive" aria-current="page">
						{{documento.documentoArea}}
					</li>
					<li class="breadcrumb-item active" aria-current="page">
						{{ documento.nome }}
					</li>
				</ol>
			</nav>
		</div>
		<Spinner spinnerColorClass="text-dark" v-if="loadingContent" />
		<section class="evento-expandido ghp box pt-3" v-else>
			<div class="row container-fluid">
				<div class="d-flex mt-4 mb-2">
					<div class="d-flex align-items-center mx-2">
						{{ tagsFormatadas }}
					</div>
				</div>
				<h1 class="text-start">{{ documento?.nome }}</h1>
				<div class="d-flex mt-4 mb-2">
					<img src="/icons/user-circle.svg" alt="icone usuario" class="icone-usuario" />
					<div class="d-flex align-items-center mx-2 fw-bold">
						{{ documento.instituicao }}
					</div>
				</div>
				<div class="row container-fluid">
					<div class="col-sm-6">
	
						<div class="d-flex mt-4 mb-2">
							<img src="/icons/calendar-icon.svg" alt="icone pino de endereço"
								class="icone-usuario" />
							<div class="d-flex align-items-center mx-2 fw-bold">
								{{ dataFormatada }}
							</div>
						</div>
	
					</div>
					<div class="d-flex col-sm-6 align-items-end justify-content-end">
						<div class="d-flex col-sm-4 align-items-end justify-content-end">
							<button type="button" class="green-btn-primary">
								<a :href="documento.download" class="link-btn">Download</a>
							</button>
						</div>
					</div>
				</div>

				
			</div>

			<hr class="w-100 my-4" />

			<div class="d-flex mt-4 mb-2">
				<div class="d-flex text-start mx-2 mb-4">
					{{ documento.descricao }}
				</div>
			</div>



		</section>

	</div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useDocumentStore } from '../../../stores/documentos/store';
import { IDocumento } from '../../../stores/documentos/types';
import Spinner from '../../../components/general/Spinner.vue';
import { brDateString, friendlyDateTime } from '../../../utils/formatacao/datetime';
import router from '../../../router';

const documento = ref<IDocumento>({
	id: -1,
	nome: '',
	descricao: '',
	tags: [],
	tipoDocumentoId: -1,
	documentoAreaid: -1,
	instituicaoId: -1,
	data: '',
	arquivo: {} as File,
	documentoArea: '',
	instituicao: '',
	download: '',
	autor: '',
	aprovado: true
})
const documentoStore = useDocumentStore()
const loadingContent = ref(false)
const dataFormatada = ref('')
const tagsFormatadas = ref('')

onMounted(async () => {
	loadingContent.value = true
	await documentoStore.getDocDetailsById(parseInt(router.currentRoute.value.params.documentoId.toString()))
	documento.value = documentoStore.response.dado
	dataFormatada.value = friendlyDateTime(documento.value.data)
	const prependHashtag = documento.value.tags.map((tag) => "#" + tag.descricao)
	tagsFormatadas.value = prependHashtag.join("  ")
	loadingContent.value = false
})

</script>

<style scoped lang="scss">
.icone-usuario {
	width: 30px;
}
.link-btn {
    text-decoration: none;
    color: white;
  }
</style>