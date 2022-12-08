<template>
  <NavBar 
    :is-transparent="false"
  />

  <div class="background">
	  <Spinner spinnerColorClass="text-dark" v-if="loadingContent"
	  />
		<div class="evento-breadcrumb">
			<nav style="--bs-breadcrumb-divider: '>';" aria-label="breadcrumb">
				<ol class="breadcrumb">
					<li class="breadcrumb-item unactive" @click="$router.push({name: 'Eventos'})">Eventos</li>
					<li class="breadcrumb-item unactive" aria-current="page">Destaques</li>
					<li class="breadcrumb-item active" aria-current="page">{{evento?.titulo}}</li>
				</ol>
			</nav>
	  	</div>

		<div class="evento-container box">

			<ContainerInformacoes
			:data="{
				eventDate: evento?.dataInicio!,
				eventId: evento?.id!,
				eventLocation: evento?.local!,
				eventName: evento?.titulo!,
				img: evento?.imagem!,
				eventLink: evento?.linkExterno!
			  }"
			/>
			<ContainerDescricao 
			   :descricaoEvento="evento?.descricao!"
			  :linkEvento="evento?.linkExterno!" 
			/>
			<!--<ContainerOrganizador />-->
		</div>
		<div class="btn-up-container" @click="handleNavigateUp">
		  <button class="btn-up">
			<img src="/public/noticias/noticia-expandida/arrow_up.svg" alt="Seta para cima">
			Subir pro topo
		  </button>
	  </div>
  </div>
  <FooterComponent />
</template>

<script setup lang="ts">
import NavBar from '../../../components/general/NavBar.vue'
import ContainerInformacoes from '../../../components/eventos/expandido/ContainerInformacoes.vue';
import ContainerDescricao from '../../../components/eventos/expandido/ContainerDescricao.vue';
import FooterComponent from '../../../components/general/FooterComponent.vue';
import { onMounted, ref } from 'vue';
import { useEventoStore } from '../../../stores/eventos/store';
import router from '../../../router';
import { IEvento } from '../../../stores/eventos/types';
import Spinner from '../../../components/general/Spinner.vue';

const eventoStore = useEventoStore()
const evento = ref<IEvento>()
const loadingContent = ref(false)

const handleNavigateUp = () => {
	window.scrollTo(0, 0)
}

onMounted(async () => {
	loadingContent.value = true
	await eventoStore.getEventById(parseInt(router.currentRoute.value.params.eventoId.toString()))
	evento.value = eventoStore.eventResponse.dado;
	loadingContent.value = false

})

</script>

<style scoped lang="scss">

.background{
	background-image: url('/public/user/background.svg');
	background-repeat: no-repeat;
	background-size: cover;
	width: 100%;
	padding-top:3rem;
	padding-bottom: 3rem;
}

.btn-up-container{
	  margin: 0 auto;
	  margin-top: 2rem;
	  display: flex;
	  justify-content: flex-end;
	  max-width: 1300px;
	  .btn-up{
		background-color: #fff;
		padding: .7rem;
		border: 1px solid #505050;
		border-radius: 10px;
		font-family: 'Montserrat-SemiBold', sans-serif;
	  }

	  .btn-up:hover{
		background-color: rgb(232, 229, 229);
	  }
  }

.evento-breadcrumb{
		display: flex;
		width: fit-content;
		margin-left:15%;
		font-size: 20px;

		.active{
			font-family: 'Montserrat-Medium', sans-serif;
		}

		.unactive{
			cursor: pointer;
			font-family: 'Montserrat-Light', sans-serif;
		}

		@media(max-width: 920px){
			font-size: 17px;
		}
	}

	.evento-container{
		margin: 0 auto;
		max-width: 1300px;

	}
  div.d-flex {
    justify-content: flex-start;
    align-items: center;
    img {
      max-width: 120px;
      min-width: 20px;
      width: 20%;
    }
    h1 {
      font-size: 2.5rem;
      text-align: start;
      margin-left: 2rem;
      p {
        font-size: 1rem;
        max-width: 450px;
        width: 100%;
        text-align: justify;
        margin-top: 10px;
        margin-bottom: 0;
        overflow: hidden;
        text-overflow: ellipsis;
          display: -webkit-box;
          -webkit-line-clamp: 4; /* number of lines to show */
                  line-clamp: 4; 
          -webkit-box-orient: vertical;
      }
    }
  }
  @media (max-width: 1200px) {
    div.d-flex {
      h1 {
        font-size: 2rem;
        text-align: start;
        margin-left: 1.5rem;
        p {
          font-size: 0.8rem;
          max-width: 350px;
        }
      }
    }
  }
  @media (max-width: 991px) {
    div.d-flex {
      padding: 0 100px;
      h1 {
        font-size: 1.5rem;
        text-align: start;
        margin-left: 1.5rem;
        p {
          max-width: 200px;
        }
      }
    }
  }
  @media (max-width: 768px) {
  div.d-flex {
    padding: 0 50px;
      h1 {
        font-size: 1rem;
        text-align: start;
        margin-left: 1rem;
        p {
          font-size: 0.7rem;
        }
      }
    }
  }
  @media (max-width: 576px) {
  div.d-flex {
      h1 {
        font-size: 0.8rem;
        text-align: start;
        margin-left: 0.8rem;
        margin-bottom: 0;
        p {
          max-width: 100px;
          text-align: start;
          margin-top: 0;
          -webkit-line-clamp: 3; /* number of lines to show */
                  line-clamp: 3; 
        }
      }
    }
  }
</style>