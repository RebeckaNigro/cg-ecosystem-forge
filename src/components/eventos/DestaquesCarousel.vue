<template>
  <div id="destaquesCarousel" class="carousel slide carousel-dark" data-bs-ride="carousel">
    <div class="carousel-indicators">
      <button type="button" data-bs-target="#destaquesCarousel" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
      <button type="button" data-bs-target="#destaquesCarousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
      <button type="button" data-bs-target="#destaquesCarousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
    </div>
    <div class="carousel-inner">
      <div v-for="(data, index) in carouselData" :key="index" class="carousel-item" :class="{'active': index === 0}">
        <img src="../../../public/eventos/event_img.png" class="d-block w-100" alt="destaques-carousel">
        <section class="d-flex infos mt-2">
          <main>
            <h1 id="event-name" class="dark-title">{{ data.titulo }}</h1>
            <time class="dark-body-text calendar-icon">{{ friendlyDateTime(data.dataInicio) }}</time>
            <address class="dark-body-text pin-icon">{{ data.local }}</address>
          </main>
          <GeneralBtn
            btnText="VER DETALHES"
            :isExternalLink="false"
            link="#"
            bgColor="#e78f38"
            width="150px"
            textColor="#fff"
            height="40px"
            :id="'ver-mais' + data.id"
            class="details"
          />
        </section>
      </div>
    </div>
    <div class="indicators-container">
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
import { IUltimoEvento } from '../../stores/eventos/types';
import GeneralBtn from '../buttons/GeneralBtn.vue';
import {friendlyDateTime} from '../../utils/formatacao/datetime'
  const props = defineProps<{
    carouselData: IUltimoEvento[]
  }>()
</script>

<style scoped lang="scss">
  #destaquesCarousel {
    h1 {
      font-size: 1.5rem;
    }
    section > main {
      text-align: start;
      time, address {
        font-size: 1rem
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
    background-image: url('/eventos/calendar_icon.png');
  }
  .pin-icon {
    background-image: url('/eventos/pin_icon.png');
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