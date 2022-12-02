<template>
  <NavBar 
    :is-transparent="false"
  />
  <ContainerInformacoes
    :data="{
      eventDate: '22/12/22',
      eventId: 'kk',
      eventLocation: 'acapulco',
      eventName: 'Challenge',
      img: '/eventos/event_img.png'
    }"
  />
  <ContainerDescricao />
  <ContainerOrganizador />
  <FooterComponent />
</template>

<script setup lang="ts">
import NavBar from '../../../components/general/NavBar.vue'
import Banner from '../../../components/general/Banner.vue';
import ContainerInformacoes from '../../../components/eventos/expandido/ContainerInformacoes.vue';
import ContainerDescricao from '../../../components/eventos/expandido/ContainerDescricao.vue';
import ContainerOrganizador from '../../../components/eventos/expandido/ContainerOrganizador.vue';
import FooterComponent from '../../../components/general/FooterComponent.vue';
import { IUltimoEvento } from '../../../stores/eventos/types';
import { ref } from 'vue';
import { useEventoStore } from '../../../stores/eventos/store';

const eventoStore = useEventoStore()

</script>

<style scoped lang="scss">
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