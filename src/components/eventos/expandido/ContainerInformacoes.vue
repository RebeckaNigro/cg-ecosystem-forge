<template>
  <section class="d-flex infos position-relative">
    <header class="ghp mt-5">
      <img :src="data." class="d-block w-100" alt="destaques">
    </header>
    <main class="ghp mb-5">
      <div>
        <h1 id="event-name" class="dark-title">{{ data.eventName }}</h1>
		<!--<p class="nome-autor">Nome do autor</p>-->
        <time class="dark-body-text calendar-icon">{{ friendlyDateTime(data.eventDate) }}</time>
        <address class="dark-body-text pin-icon">{{ data.eventLocation }}</address>
      </div>

	  <!-- Botao compartilhar-->
	  
      <GeneralBtn
        btnText="ACESSAR LINK"
        :isExternalLink="(data.eventLink !== null)"
        :link="data.eventLink"
        bgColor="#48947d"
        width="250px"
        textColor="#fff"
        height="40px"
        :id="'acessar-link' + data.eventId"
        class="acessar-link"
      /> 
    </main>
	<div class="divider"></div>
</section>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { friendlyDateTime } from '../../../utils/formatacao/datetime';
import GeneralBtn from '../../buttons/GeneralBtn.vue';
  // import GeneralBtn from '../../buttons/GeneralBtn.vue'
  const props = defineProps<{
    data: {
      eventId: number
      eventName: string
      eventDate: string
      eventLocation: string
      img: string | null
	  eventLink: string
    }
  }>()

  onMounted(() => {
	console.log(props.data.img);
	
  })
</script>

<style scoped lang="scss">

	.divider{
		width: 80%;
		height: 1px;
		background: #505050;
		margin: 0 auto;
	  }

  .acessar-link {
    margin: unset;
    margin-left: auto !important;
    margin-top: auto !important;
	margin-bottom: 1rem;
  }
  section.infos {
    flex-direction: column;
	
    > main {
      text-align: start;
      display: flex;
      flex-direction: row;
      h1#event-name {
        font-size: 2rem;
		margin-top: 1rem;
      }

	  .nome-autor{
		font-family: 'Montserrat-Medium', sans-serif;
		font-size: 1rem;
		color: #000;
		background: url('/public/noticias/noticia-expandida/user.svg');
		background-repeat: no-repeat;
		background-position-y: center;
		background-size: contain;
		padding-left: 2rem;
	  }

      img.company-logo {
        max-width: 125px;
        max-height: 125px;
        width: 100%;
        height: 100%;
      }
      time, address {
        font-size: 1rem;
		font-family: 'Montserrat-Medium', sans-serif;
		margin: 1rem 0;
      }
    }
  }
  .calendar-icon, .pin-icon {
    padding-left: 30px;
    background-repeat: no-repeat;
    background-position: center left;
    background-size: contain
  }
  .calendar-icon {
    background-image: url('/public/calendar_icon.svg');
  }
  .pin-icon {
    background-image: url('/public/location_icon.svg');
  }
  @media (max-width: 991px) {
    section.infos {
      > main {
        h1#event-name {
          font-size: 1.5rem;
        }
        time, address {
          font-size: 1.2rem
        }
      }
    }
  }
  @media (max-width: 768px) {
    section.infos {
      .details {
        font-size: 0.8rem !important;
        width: 120px !important;
        white-space: nowrap;
        height: 30px !important;
      }
      > main {
        h1#event-name {
          font-size: 1.3rem;
        }
        time, address {
          font-size: 1rem
        }
      }
    }
  }
  @media (max-width: 576px) {
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
</style>