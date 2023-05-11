<template>
  <section class="container-fluid">
    <main>
      <Banner
        path="/home/fundo.png"
        figcaption="workspace_image"
        img-alt="workspace_image"
		:imgOverlay="true"
		maxHeight="650px"
      >

	  <div class="header-content d-flex h-100 position-absolute top-0 ghp">
		  <h1 class="dark-title">ECOSSISTEMA LOCAL DE INOVAÇÃO</h1>
		  <p class="subtitle">Campo Grande - MS</p>
		  <p>Uma comunidade pronta para contribuir com o seu desenvolvimento!</p>
	  </div>
	  </Banner>
      <GeneralContainer
        title="CONHEÇA O ECOSSISTEMA"
        :dark-title="false"
        :content-array="['Entenda como surgiu e qual é o propósito da comunidade.', '/home/people_and_postits.png']"
        :has-know-more-btn="true"
        bg-color="linear-gradient(180deg, rgba(0, 0, 0, 0.2) 0%, rgba(255, 255, 255, 0.2) 100%), #639280"
        id="presentation-container"
		font-family="Montserrat-Bold"
		font-size="1.3rem"
		:has-contact-btn="false"
		:has-circle-top-border="false"
		:has-circle-bottom-border="true"
      />
      <Partners
        :logos-url-array="logosParceiros"
      />
      <GeneralContainer
        title="MAIS SOBRE O ECOSSISTEMA"
        :dark-title="false"
        :content-array="['/home/white_logo.png', 'A comunidade de Inovação em Campo Grande – MS está reunida para ajudar você a se qualificar, estruturar o seu projeto, abrir o seu negócio, facilitar processos e estimular o desenvolvimento socioeconômico local. Quer fazer parte?']"
        :has-know-more-btn="false"
        bg-color="linear-gradient(180deg, rgba(255, 255, 255, 0.2) 0%, rgba(0, 0, 0, 0.2) 100%), #639280"
        id="more-info-container"
		font-family="Montserrat-SemiBold"
		font-size="1.3rem"
	  	:has-contact-btn="true"
		:has-circle-top-border="true"
		:has-circle-bottom-border="false"
      />
    </main>
  </section>
</template>

<script setup lang="ts">
import GeneralContainer from '../components/general/GeneralContainer.vue';
import Banner from '../components/general/Banner.vue'
import Partners from '../components/parceiros/Partners.vue';
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

		h1 {

			text-align: start;
			font-size: 2rem;
		}

		.subtitle{
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

		@media(max-width: 580px){
			h1{
				font-size: 1.5rem;
			}

			.subtitle{
				font-size: 1.3rem;
			}


			p {
				font-size: 1.1rem;
				text-align: start;
			}
		}


	}
  }
</style>