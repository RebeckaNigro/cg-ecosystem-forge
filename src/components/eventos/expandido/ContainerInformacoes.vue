<template>
  <section class="d-flex infos position-relative">
    <header class="ghp mt-5">
      <img :src="data.img" class="d-block w-100" alt="destaques-carousel">
    </header>
    <main class="ghp mb-5">
      <div>
        <h1 id="event-name" class="dark-title">{{ data.eventName }}</h1>
        <time class="dark-body-text calendar-icon">{{ data.eventDate }}</time>
        <address class="dark-body-text pin-icon">{{ data.eventLocation }}</address>
      </div>
      <!-- <GeneralBtn
        btnText="VER DETALHES"
        :isExternalLink="false"
        link="#"
        bgColor="#e78f38"
        width="150px"
        textColor="#fff"
        height="40px"
        :id="'ver-mais' + data.eventId"
        class="details"
      /> -->
    </main>
  </section>
</template>

<script setup lang="ts">
  // import GeneralBtn from '../../buttons/GeneralBtn.vue'
  const props = defineProps<{
    data: {
      eventId: string
      eventName: string
      eventDate: string
      eventLocation: string
      img: string
    }
  }>()
</script>

<style scoped lang="scss">

  /* .details {
    margin: unset;
    margin-left: auto !important;
    margin-top: 1rem !important;
  } */
  section.infos {
    background-color: #f6f6f6;
    flex-direction: column;
    > main {
      text-align: start;
      display: flex;
      flex-direction: row;

      h1#event-name {
        font-size: 2rem;
      }
      img.company-logo {
        max-width: 125px;
        max-height: 125px;
        width: 100%;
        height: 100%;
      }
      time, address {
        font-size: 1.5rem
      }
    }
  }
  .calendar-icon, .pin-icon {
    padding-left: 45px;
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