<template>
  <NavBar :is-transparent="false"/>
  <div class="background">
	  <Banner 
		path="/fale-conosco/fale-conosco-banner.png"
		figcaption="costumer-service"
		img-alt="costumer-service"
	  >
		<div class="banner-fale-conosco ghp">
		
		  <h1 class="dark-title">FALE CONOSCO</h1>
		  <p>
			  Tenha acesso aos principais documentos disponibilizados pelo Ecossistema Local de Inovação - Campo Grande - MS
		  </p>
		</div>
	  </Banner>
	  <Formulario />

  </div>
  <FooterComponent />
</template>

<script setup lang="ts">
import Banner from '../../components/general/Banner.vue';
import Formulario from '../../components/faleConosco/Formulario.vue';
</script>

<style scoped lang="scss">

.background{
	background-image: url('/public/user/background.svg');
	background-repeat: no-repeat;
	background-size: cover;
	width: 100%;
	padding-bottom: 3rem;
}

.banner-fale-conosco {
	position: absolute;
	top: 0;
	width: 100%;
	height: 100%;
	padding-left: 64px;
	display: flex;
	align-items: start;
	justify-content: center;
	flex-direction: column;


	.dark-title {
		font-size: 2rem;
	}
	h1 {
		font-size: 2rem;
		text-align: start;
	}
	p{
		text-align: start;
		font-size: 1.5rem;
		font-family: 'Montserrat-Medium', sans-serif;
	}

}
 
  @media (max-width: 1200px) {
    div.d-flex {
      h1 {
        font-size: 2rem;
        text-align: start;
        margin-bottom: 2rem !important;
        margin-left: 1.5rem;
      }
    }
  }
  @media (max-width: 991px) {
    div.d-flex {
      padding: 0 100px;
      h1 {
        font-size: 1.5rem;
        text-align: start;
        margin-bottom: 2rem !important;
        margin-left: 1.5rem;
      }
    }
  }
  @media (max-width: 768px) {
  div.d-flex {
    padding: 0 50px;
      h1 {
        font-size: 1rem;
        text-align: start;
        margin-bottom: 2rem !important;
        margin-left: 1rem;
      }
    }
  }
  @media (max-width: 576px) {
  div.d-flex {
      h1 {
        font-size: 0.8rem;
        text-align: start;
        margin-bottom: 1rem !important;
        margin-left: 1rem;
      }
    }
  }
</style>