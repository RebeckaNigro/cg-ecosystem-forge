<template>
	<section class="container-fluid">
		<main>
			<Banner path="/home/fundo.png" figcaption="workspace_image" img-alt="workspace_image" :imgOverlay="true"
				maxHeight="650px">
				<ExternalHeader
					title="ECOSSISTEMA LOCAL DE INOVAÇÃO"
					subtitle="Campo Grande - MS"
					paragraph="Uma comunidade pronta para contribuir com o seu desenvolvimento!"
				/>
		
			</Banner>

			<div class="meet-eco-container p-5 border-0">

				<h2 class="light-title text-uppercase">Conheça o ecossistema</h2>
				<div class="row mt-5">

					<div class="col-md-6 d-flex flex-column justify-content-center ">
						<div class="d-flex flex-column align-self-center ms-lg-5 ">

							<p class="text mb-2">
								Entenda como surgiu e qual o proprósito da comunidade
							</p>

							<button type="button" class="white-btn-primary text-uppercase align-self-start mt-4"
								@click="$router.push('/quem-somos')">Saiba mais</button>
						</div>

					</div>

					<div class="col-md-6 d-none d-md-flex">
						<img src="/home/people_and_postits.png" alt="Logo Ecossistema" class="meet-img align-self-start">
					</div>
				</div>
			</div>
			<div class="circle-bottom"></div>

			<Partners :logos-url-array="logosParceiros" />
			<div class="more-info-container p-5 border-0">
				<div class="circle-top"></div>
				<h2 class="light-title text-uppercase">Mais sobre o ecossistema</h2>
				<div class="row">
					<div class="col-md-6 d-none d-md-block">
						<img src="/home/white_logo.png" alt="Logo Ecossistema" class="eco-logo img-fluid p-5">
					</div>

					<div class="col-md-6 d-flex flex-column justify-content-center">
						<p class="text mb-2 mt-3 mt-lg-0">
							A comunidade de Inovação em Campo Grande-MS está reunida para ajudar você a se qualificar,
							estruturar o seu projeto, abrir o seu negócio, facilitar processos e estimular o desenvolvimento
							socioeconômico local.
						</p>
						<p class="text mt-0">Quer fazer parte? </p>
						<button type="button" class="white-btn-primary text-uppercase mt-4"
							@click="$router.push('/fale-conosco')">Fale conosco</button>
					</div>
				</div>

			</div>

		</main>
	</section>
</template>

<script setup lang="ts">
import GeneralContainer from '../components/general/GeneralContainer.vue';
import Banner from '../components/general/Banner.vue'
import Partners from '../components/parceiros/Partners.vue';
import ExternalHeader from '../components/general/ExternalHeader.vue';
const logosParceiros: string[] = [
	'/chumbado/home_parceiros/embrapa.png',
	'/chumbado/home_parceiros/INCT-Inst-Nacionais.png',
	'/chumbado/home_parceiros/INTERP_Incubadoras.png',
	'/chumbado/home_parceiros/LITECH.png',
	'/chumbado/home_parceiros/livinglab.png',
	'/chumbado/home_parceiros/oab.png',
	'/chumbado/home_parceiros/RedeMS.png',
	'/chumbado/home_parceiros/eli-cgde/ANGI.png',
	'/chumbado/home_parceiros/eli-cgde/coworking.png',
	'/chumbado/home_parceiros/eli-cgde/ecoinova_oficial.png',
	'/chumbado/home_parceiros/eli-cgde/eng.png',
	'/chumbado/home_parceiros/eli-cgde/ICT-INOVA-BR.png',
	'/chumbado/home_parceiros/eli-cgde/IFMS.png',
	'/chumbado/home_parceiros/eli-cgde/JERA.png',
	'/chumbado/home_parceiros/eli-cgde/ORTHOS-CGDE.png',
	'/chumbado/home_parceiros/eli-cgde/Park-Office.png',
	'/chumbado/home_parceiros/eli-cgde/RdasArteiras.png',
	'/chumbado/home_parceiros/eli-cgde/Serviço-AKI.png',
	'/chumbado/home_parceiros/eli-cgde/Statup-MS.png',
	'/chumbado/home_parceiros/eli-cgde/Território-do-Vinho.png',
	'/chumbado/home_parceiros/faculdades/Anhanguera.png',
	'/chumbado/home_parceiros/faculdades/Estácio.png',
	'/chumbado/home_parceiros/faculdades/FACSUL.png',
	'/chumbado/home_parceiros/faculdades/UNIGRAN.png',
	'/chumbado/home_parceiros/governo-ms/agraer.png',
	'/chumbado/home_parceiros/governo-ms/fundect.png',
	'/chumbado/home_parceiros/governo-ms/semadesc.png',
	'/chumbado/home_parceiros/ifms/ifms.png',
	'/chumbado/home_parceiros/pmcg-e-incubadora/incubadora-municipal.png',
	'/chumbado/home_parceiros/pmcg-e-incubadora/sidagro.png',
	'/chumbado/home_parceiros/sistema-s/Startup.png',
	'/chumbado/home_parceiros/sistema-s/Sistema-Fiems.png',
	'/chumbado/home_parceiros/sistema-s/SESI.png',
	'/chumbado/home_parceiros/sistema-s/SESC.png',
	'/chumbado/home_parceiros/sistema-s/SENAR-MS.png',
	'/chumbado/home_parceiros/sistema-s/SENAI.png',
	'/chumbado/home_parceiros/sistema-s/SENAC.png',
	'/chumbado/home_parceiros/sistema-s/SEBRAE.png',
	'/chumbado/home_parceiros/sistema-s/Instit-SENAI-Inovação.png',
	'/chumbado/home_parceiros/sistema-s/Centro-de-Inovação-SESI.png',
	'/chumbado/home_parceiros/ucdb-e-incubadoras/ucdb.jpg',
	'/chumbado/home_parceiros/ucdb-e-incubadoras/ucdb-inova.png',
	'/chumbado/home_parceiros/uems-e-incubadoras/elos.png',
	'/chumbado/home_parceiros/uems-e-incubadoras/Fênix.png',
	'/chumbado/home_parceiros/uems-e-incubadoras/uems.png',
	'/chumbado/home_parceiros/ufms-e-incubadoras/ufms.png',
	'/chumbado/home_parceiros/ufms-e-incubadoras/pime.png',
	'/chumbado/home_parceiros/ufms-e-incubadoras/aginova.png',
]

</script>

<style scoped lang="scss">
section.container-fluid {
	padding: 0;

	.header-content {
		justify-content: center;
		flex-direction: column;
		margin-left: 7rem;

		h1 {
			text-align: start;
			font-size: 2rem;
			margin-bottom: 0;
		}

		.subtitle {
			font-size: 1.8rem;
			font-family: 'Montserrat-SemiBold', sans-serif;
		}


		p {
			font-size: 1.5rem;
			font-family: 'Montserrat-Medium', sans-serif;
			max-width: 500px;
			width: 100%;
			text-align: justify;
			margin-top: 10px;
			margin-bottom: 0;
			overflow: hidden;
			text-overflow: ellipsis;
			display: -webkit-box;
			-webkit-line-clamp: 4;
			/* number of lines to show */
			line-clamp: 4;
			-webkit-box-orient: vertical;
			color: #000;
		}



	}

	.meet-eco-container,
	.more-info-container {
		h2.light-title {
			font-size: 1.8rem;
		}

		p.text {
			font-family: 'Montserrat-SemiBold', sans-serif;
			color: #fff;
			text-align: justify;
			font-size: 1.2rem;
		}
	}

	.meet-eco-container {
		background: linear-gradient(180deg, rgba(0, 0, 0, 0.2) 0%, rgba(255, 255, 255, 0.2) 100%), #639280;


		p.text {
			max-width: 300px;
		}

		button {
			width: 70%;
			font-size: 1.1rem;
		}

		.meet-img {
			border-radius: 10px;
			width: 70%;
		}
	}

	.more-info-container {
		background: linear-gradient(180deg, rgba(255, 255, 255, 0.2) 0%, rgba(0, 0, 0, 0.2) 100%), #639280;

		p.text {
			max-width: 560px;
		}

		button {
			width: 35%;
			font-size: 1.1rem;
		}

		.eco-logo {
			width: 50%;
		}
	}
}

.circle-bottom,
.circle-top {
	height: 25px;
	width: 50px;
	border-bottom-left-radius: 8rem;
	border-bottom-right-radius: 8rem;
	margin: 0 auto;
	position: relative;
}

.circle-bottom {
	top: -0.1rem;
	background: #81A799;

}

.circle-top {
	top: -3.1rem;
	background: #fbfbfb;

}

/* NOTEBOOKS E AFINS */
@media screen and (min-width: 2550px) {

	section.container-fluid {
		padding: 0;

		.header-content {
			margin-left: 2.5rem;


			h1 {
				font-size: 2.5rem;
			}

			.subtitle {
				font-size: 2.3rem;
			}


			p {
				font-size: 2rem;
				text-align: start;
				max-width: 700px;
			}

		}

		.meet-eco-container {
			p.text {
				max-width: 450px;
			}
		}

		.more-info-container,
		.meet-eco-container {
			h2.light-title {
				font-size: 2.6rem;
			}

			p.text {
				font-size: 1.8rem;
			}

			button {
				font-size: 1.5rem;
			}
		}

	}

	.circle-bottom,
	.circle-top {
		height: 45px;
		width: 70px;
	}


}

/* TABLETS */
@media screen and (max-width: 1200px) {

	section.container-fluid {
		padding: 0;

		.header-content {
			margin-left: 1.5rem;
			padding-top: 1rem;

			h1 {
				font-size: 1.5rem;
			}

			.subtitle {
				font-size: 1.3rem;
			}


			p {
				font-size: 1.1rem;
				text-align: start;
			}

		}

		.more-info-container,
		.meet-eco-container {
			p.text {
				font-size: .9rem;
			}

			button {
				width: 60%;
				font-size: 1rem;
			}

			.eco-logo,
			.meet-img {
				width: 80%;
			}

		}

	}
}

/* CELULARES */
@media(max-width: 580px) {
	section.container-fluid {
		padding: 0;

		.header-content {
			margin-left: 1rem;
			padding-top: 1rem;

			h1 {
				font-size: 1.2rem;
			}

			.subtitle {
				font-size: 1rem;
			}


			p {
				font-size: 1.1rem;
				text-align: start;
			}

		}

		.meet-eco-container {
			height: 380px;
		}

		.more-info-container {
			height: 450px;
		}

		.more-info-container,
		.meet-eco-container {

			h2.light-title {
				font-size: 1.5rem;
			}

			button {
				width: 60%;
				margin: .7rem auto;
				font-size: .9rem;
			}
		}
	}


}
</style>