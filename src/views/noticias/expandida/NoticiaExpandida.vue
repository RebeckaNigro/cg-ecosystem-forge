<template>
	<div class="pt-5 pb-5 container">
		<Spinner spinnerColorClass="text-dark" v-if="isLoadingContent" />
		<div class="noticia-breadcrumb">
			<nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
				<ol class="breadcrumb">
					<li class="breadcrumb-item unactive">
						Notícias
					</li>
					<li class="breadcrumb-item unactive" aria-current="page">
						Destaques
					</li>
					<li class="breadcrumb-item active text-break" aria-current="page">
						{{ noticia.titulo }}
					</li>
				</ol>
			</nav>
		</div>
		<section class="noticia-expandida box">
			<div class="noticia-header d-flex flex-column align-items-start">
				<h1>{{ noticia.titulo }}</h1>
				<p>{{ noticia.subTitulo }}</p>
				<p class="nome-autor">{{ noticia.autor }}</p>
				<time>{{ friendlyDateTime(noticia.dataPublicacao) }}</time>
			</div>
			<img class="w-100 img-fluid" :src="noticia.arquivo
					? 'data:image/png;base64, ' + noticia.arquivo
					: '/public/noticias/noticia-expandida/default-news-cover.svg'
				" alt="Capa da notícia" />
			<p class="subtitulo" id="first-paraghap">
				{{ noticia.subTitulo }}
			</p>
			<main>
				<article class="news-body ql-editor p-0" v-html="noticia.descricao"></article>

			</main>
			<!--<NoticiasRelacionadas />-->
		</section>

		<div class="btn-up-container" @click="handleNavigateUp">
			<button class="btn-up">
				<img src="/public/noticias/noticia-expandida/arrow_up.svg" alt="Seta para cima" />
				Subir ao topo
			</button>
		</div>
	</div>
</template>

<script setup lang="ts">
import { onMounted, ref } from "vue"
import router from "../../../router"
import { useNoticiaStore } from "../../../stores/noticias/store"
import { INoticia } from "../../../stores/noticias/types"
import { friendlyDateTime } from "../../../utils/formatacao/datetime"
import Spinner from "../../../components/general/Spinner.vue"

const store = useNoticiaStore()
const noticia = ref<INoticia>({
	id: 0,
	titulo: "",
	descricao: "",
	tags: [],
	subTitulo: "",
	dataPublicacao: "",
	arquivo: {} as File,
	autor: ""
})

const isLoadingContent = ref(false)
const handleNavigateUp = () => {
	window.scrollTo(0, 0)
}

onMounted(async () => {
	isLoadingContent.value = true
	await store.getNewsById(
		parseInt(router.currentRoute.value.params.noticiaId.toString())
	)

	noticia.value = store.response.dado
	isLoadingContent.value = false
})
</script>

<style scoped lang="scss">
.btn-up-container {
	margin: 0 auto;
	display: flex;
	justify-content: flex-end;
	max-width: 1300px;

	.btn-up {
		background-color: #fff;
		padding: 0.7rem;
		border: 1px solid #505050;
		border-radius: 10px;
		font-weight: 600;
	}

	.btn-up:hover {
		background-color: rgb(232, 229, 229);
	}
}

.noticia-expandida {
	margin: 3rem auto;
	max-width: 1300px;
	padding: 64px;

	.noticia-header {
		margin-bottom: 3rem;
		text-align: start;

		h1 {
			color: #000;
			font-size: 2rem;
			font-weight: 600;
		}

		p {
			font-weight: 400;
			font-size: 1.2rem;
		}

		time {
			color: #6b6a64;
			font-size: .9rem;
		}

		.nome-autor {
			font-weight: 600;
			font-size: 1rem;
			color: #000;
			background: url("/public/noticias/noticia-expandida/user.svg");
			background-repeat: no-repeat;
			background-position-y: center;
			background-size: contain;
			padding-left: 2rem;
		}
	}

	.subtitulo {
		font-weight: 500;
		text-align: start;
		font-size: 0.8rem;
	}

	.news-body {
		font-weight: 400;
		text-align: start;
		font-size: 1.25rem;
		margin-top: 1.5rem;
		margin-bottom: 3rem;
	}
}

@media screen and (max-width: 1200px) {
	.noticia-expandida {
		.noticia-header {
			h1 {

				font-size: 1.5rem;
			}

			p {
				font-size: 1rem;
			}

			time {
				font-size: .7rem;
			}

			.nome-autor {
				font-size: .8rem;
			}

		}
	}
}

@media screen and (max-width: 580px) {
	.noticia-expandida{
		padding: 42px 24px;

		.noticia-header {
			h1 {

				font-size: 1.4rem;
			}

			p {
				font-size: .9rem;
			}

			time {
				font-size: .6rem;
			}
			.nome-autor {
				font-size: .7rem;
			}

		}

		.subtitulo{
			font-size: .6rem;
		}
		.news-body {
			font-size: 1rem;
			
		}
	}
  }
</style>
