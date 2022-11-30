<template>
	<NavBar :is-transparent="true" />
	<section class="container-fluid">
		<main>
			<Banner path="/noticias/banner.png" figcaption="workspace_image" img-alt="workspace_image">
				<div class="banner-noticia-content ghp">
					<div>
						<h1 class="dark-title">NOTÍCIAS</h1>
						<p class="fs-5">Confira as principais notícias do Ecossistema Local de Inovação - Campo Grande -
							MS</p>
					</div>
				</div>
			</Banner>

			<div class="container mt-5">

				<div class="filters row justify-content-between align-items-end ">
					<div class="filtrar-area col">
						<FilterComponent content-type="área" :datas="[]" class="mx-4" />
					</div>

					<div class="filtrar-organizador col">
						<FilterComponent content-type="organizador" :datas="[]" class="mx-4" />

					</div>
					<div class="pesquisar col">

						<input type="text" name="pesquisar" id="pesquisar" placeholder="Pesquisar">
						<button class="btn" id="btn-pesquisar">
							<img src="../../../../../public/search_icon.svg" alt="Pesquisar">
						</button>
					</div>
				</div>
			</div>


			<ContainerUltimasNoticias />
			<ContainerCardsNoticia />
		</main>
	</section>
	<FooterComponent />
</template>

<script setup lang="ts">
import Banner from '../../components/general/Banner.vue';
import FilterComponent from '../../components/general/FilterComponent.vue';
import ContainerCardsNoticia from '../../components/noticias/ContainerCardsNoticia.vue';
import ContainerUltimasNoticias from '../../components/noticias/ContainerUltimasNoticias.vue';
</script>

<style scoped lang="scss">
section.container-fluid {
	padding: 0;
	margin-bottom: 40px;

	.filtrar-area,
	.filtrar-organizador {
		max-width: 300px;
	}

	.filters {
		margin: 0 auto;
	}

	.pesquisar {
		max-width: 400px;

		#pesquisar {
			border-radius: 40px 0px 0px 40px;
			border: 1px solid #6B6A64;
			border-right: unset;
			padding: .35rem .7rem .52rem .7rem;
			width: 300px;


		}

		#btn-pesquisar {
			border: 1px solid #6B6A64;

			border-radius: 0 40px 40px 0;
			background-color: #fff;
			height: 100%;

			img {
				max-width: 26px;
			}
		}

		#pesquisar:focus {
			border-color: #86b7fe;
			outline: 0;
			box-shadow: 0 0 0 .25rem rgba(13, 110, 253, .25);
		}

		@media (max-width: 580px) {
			margin-top: 1.5rem;
		}
	}
}

.banner-noticia-content {
	position: absolute;
	top: 0;
	width: 100%;
	height: 100%;
	padding-left: 64px;

	display: flex;
	align-items: center;

	.dark-title {
		font-size: 2rem;
	}

	>img {
		width: 10%;
		min-width: 60px;
		max-width: 128px;
		margin-right: 24px;
	}

	>div {
		display: flex;
		flex-direction: column;
		align-items: flex-start;
		width: 100%;
		max-width: 500px;

		p {
			font-family: 'Montserrat-Medium', sans-serif;
			text-align: start;
			max-height: 100px;
			height: 100%;
			overflow-y: auto;
		}
	}
}

@media (max-width: 1200px) {
	.banner-noticia-content {
		.dark-title {
			font-size: 2rem;
		}

		.dark-body-text {
			font-size: 0.8rem;
		}
	}
}

@media (max-width: 991px) {
	.banner-noticia-content {
		.dark-title {
			margin-bottom: 0;
			font-size: 1.5rem;
		}

		>div {
			max-width: 300px;

			p {
				max-height: 60px;
				margin-bottom: 0;
			}
		}
	}
}

@media (max-width: 768px) {
	.banner-noticia-content {
		.dark-title {
			font-size: 1rem;
		}

		>div {
			max-width: 200px;

			p {
				max-height: 40px;
			}
		}

		.dark-body-text {
			font-size: 0.7rem;
		}
	}
}

@media (max-width: 576px) {
	.banner-noticia-content {
		.dark-title {
			font-size: 0.8rem;
		}

		.dark-body-text {
			font-size: 0.8rem;
		}
	}
}
</style>
