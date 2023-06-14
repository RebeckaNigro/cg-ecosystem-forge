<template>
  <div id="destaquesCarousel" class="carousel slide carousel-dark" data-bs-ride="carousel">
    <div class="carousel-indicators">
      <button type="button" data-bs-target="#destaquesCarousel" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
      <button type="button" data-bs-target="#destaquesCarousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
      <button type="button" data-bs-target="#destaquesCarousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
    </div>
    <div class="carousel-inner">
      <div v-for="(data, index) in carouselData" :key="index" class="carousel-item" :class="{'active': index === 0}">
        <img :src="data.arquivo 
			? 'data:image/png;base64,' + data.arquivo
			: '/eventos/eventoExpandido/default-event-cover.svg'" 
			class="d-block w-100" :alt="data.titulo" :class="{ 'img-evento-encerrado' : checaEventoEncerrado(data)}">
        <section class="d-flex infos mt-2 pt-4 ps-5 pb-4 pe-5 align-items-center">
          <main>
            <h1 id="event-name" class="dark-title" :class="{ 'texto-evento-encerrado' : checaEventoEncerrado(data)}">{{ checaEventoEncerrado(data) ? data.titulo + ' (ENCERRADO)' : data.titulo }}</h1>
            <time class="light-body-text calendar-icon">{{ data.dataInicio ? brDateString(data.dataInicio) : ''}}</time>
            <address class="light-body-text pin-icon">{{ data.local }}</address>
          </main>
          <GeneralBtn
            btnText="VER DETALHES"
            :isExternalLink="false"
            link="#"
            bgColor="#639280"
            width="250px"
            textColor="#fff"
            height="40px"
            :id="'ver-mais' + data.id"
            class="details"
			@click="$router.push({name: 'EventoExpandido', params: {eventoId: data.id}})"
          />
        </section>
      </div>
    </div>
    <div class="indicators-container mt-4">
      <button class="carousel-control-prev" type="button" data-bs-target="#destaquesCarousel" data-bs-slide="prev">
        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Previous</span>
      </button>
      <button class="carousel-control-next" type="button" data-bs-target="#destaquesCarousel" data-bs-slide="next">
        <span class="carousel-control-next-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Next</span>
      </button>      
    </div>
  </div>
</template>

<script setup lang="ts">
import { IEventoSimplificado } from '../../stores/eventos/types';
import GeneralBtn from '../buttons/GeneralBtn.vue';
import {brDateString} from '../../utils/formatacao/datetime'
  const props = defineProps<{
    carouselData: IEventoSimplificado[]
  }>()

  function checaEventoEncerrado(evento: IEventoSimplificado){
	if(new Date(evento.dataTermino) < new Date()){
		return true	
	}
	return false
  }

</script>

<style scoped lang="scss">
	.img-evento-encerrado{
		opacity: 0.5;
	}

	.texto-evento-encerrado{
		color: #6B6A64!important;
	}
  #destaquesCarousel {
    h1 {
      font-size: 1.5rem;
    }

	.carousel-inner{
		border-radius: 20px 20px 0 0;
		box-shadow: 0px 0px 25px rgba(0, 0, 0, 0.1);

		#event-name{
			font-weight: 500;
			margin: 1rem;
		}

		img{
			height: 600px;
			
		}
	}
    section > main {
      text-align: start;
      time, address {
        font-size: 1rem;
		color: #000;
		margin: 1rem;
      }
    }
    section > .details {
      margin: 0 !important;
      margin-left: auto !important;
    }
    .carousel-control-next, .carousel-control-prev {
      z-index: 5;
      top: unset;
    }
    .carousel-indicators {
      margin-bottom: 0.5rem;
      .active {
        opacity: 1 !important;
        height: 17px !important;
        width: 17px !important;
      }
      button[type="button"] {
        box-sizing: content-box;
        flex: 0 1 auto;
        width: 15px;
        height: 15px;
        padding: 0;
        margin-right: 3px;
        margin-left: 3px;
        text-indent: -999px;
        cursor: pointer;
        background-color: #000;
        background-clip: padding-box;
        border: 0;
        opacity: .5;
        transition: opacity .6s ease;
        border-radius: 15px;
        z-index: 5;
      }
    }
    .indicators-container {
      position: relative;
      width: 160px;
      height: 32px;
      margin: auto;
    }
  }
  .calendar-icon, .pin-icon {
    padding-left: 25px;
    background-repeat: no-repeat;
    background-position: center left;
    background-size: contain
  }
  .calendar-icon {
    background-image: url('/calendar_icon.svg');
  }
  .pin-icon {
    background-image: url('/location_icon.svg');
  }
  @media (max-width: 991px) {
    .carousel-inner {
      section {
        main {
          h1#event-name {
            font-size: 1rem;
          }
          .dark-body-text {
            font-size: 0.8rem !important;
          }
        }
      }
    }
  }
  @media (max-width: 768px) {
    .carousel-inner {
      section.infos {
        .details {
          font-size: 0.8rem !important;
          width: 120px !important;
          white-space: nowrap;
          height: 30px !important;
        }
      }
    }
  }
  @media (max-width: 576px) {
    .carousel-inner {
      section.infos {
        main {
          h1#event-name {
            font-size: 0.8rem;
          }
          .dark-body-text {
            font-size: 0.6rem !important;
          }
        }
        .details {
          font-size: 0.6rem !important;
          width: 100px !important;
          white-space: nowrap;
          height: 30px !important;
        }
      }
    }
  }
</style>