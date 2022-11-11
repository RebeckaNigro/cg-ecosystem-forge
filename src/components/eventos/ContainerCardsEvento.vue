<template>
  <section id="container-cards-evento" class="ghp pt-5">
    <header>
      <h1 class="dark-title">TÍTULO DA SEÇÃO</h1>
    </header>
    <!-- <nav v-for="(cardPage, index) in dummyCardsPages">
      <div class="container-fluid h-100" v-if="index === selectedPage">
        <div class="row">
          <div class="col-12 col-md-6 col-xl-4" v-for="(card, index) in cardPage" :key="index">
            <CardEvento
              :hasImage="card.hasImage"
              :image="card.image"
              :nomeEvento="card.nomeEvento"
              :dataEvento="card.dataEvento"
              :enderecoEvento="card.enderecoEvento"
              :class="setSelfMargin(index)"
            />
          </div>
        </div>
      </div>
    </nav> -->
    <nav v-for="(container, containerIndex) in dummyContainer" :key="containerIndex">
      <div class="container-fluid h-100">
        <div class="row">
          <div class="col-12 col-md-6 col-xl-4" v-for="(card, index) in container" :key="index">
            <CardEvento
              :hasImage="card.hasImage"
              :image="card.image"
              :nomeEvento="card.nomeEvento"
              :dataEvento="card.dataEvento"
              :enderecoEvento="card.enderecoEvento"
              :class="setSelfMargin(index)"
            />
          </div>
        </div>
      </div>
    </nav>
    <footer>
      <!-- <button type="button" @click.prevent="setSelectedPage(selectedPage - 1)" class="carousel-control-prev-icon"/>
      <div class="box">
        <button type="button" v-for="(indicator, index) in indicators" :key="indicator" class="indicator" :class="{'active': selectedPage === index}" @click.prevent="setSelectedPage(index)"/>
      </div>
      <button type="button" @click.prevent="setSelectedPage(selectedPage + 1)" class="carousel-control-next-icon"/> -->
      <GeneralBtn 
        btnText="VER MAIS"
        :isExternalLink="false"
        link="#"
        bgColor="#e78f38"
        width="100px"
        textColor="#fff"
        height="40px"
        id="know_more"
        @click="addEventsToView()"
      />
    </footer>
  </section>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import CardEvento from './CardEvento.vue';
import GeneralBtn from '../buttons/GeneralBtn.vue';
import { useEventoStore } from '../../stores/eventos/store';

const eventoStore = useEventoStore()
const selectedPage = ref(0)
const indicators = ref(3)

const dummyContainer = reactive([
  [
    {
      hasImage: false,
      image: 'opa',
      nomeEvento: "Evento 01",
      dataEvento: "11/11/2023",
      enderecoEvento: "Campo Grande, MS",
    },
    {
      hasImage: false,
      image: 'opa',
      nomeEvento: "Evento 01",
      dataEvento: "11/11/2023",
      enderecoEvento: "Campo Grande, MS",
    },
    {
      hasImage: false,
      image: 'opa',
      nomeEvento: "Evento abc",
      dataEvento: "27/11/2023",
      enderecoEvento: "Campo Grande, MS",
    }
  ]
])
const setSelfMargin = (num: number) => {
  if (num === 1) return 'm-auto'
  return num === 0 ? 'mr-auto' : 'ml-auto'
}
const setSelectedPage = (page: number) => {
  if (page >= indicators.value) {
    selectedPage.value = 0
  } else if (page < 0) {
    selectedPage.value = indicators.value
  } else {
    selectedPage.value = page
  }
}
const addEventsToView = () => {
  dummyContainer.push([
    {
      hasImage: true,
      image: '/home/carousel/embrapa.png',
      nomeEvento: "Evento adicionado",
      dataEvento: "05/07/2024",
      enderecoEvento: "Campo Grande, MS",
    },
    {
      hasImage: false,
      image: 'opa',
      nomeEvento: "Evento 01",
      dataEvento: "11/11/2023",
      enderecoEvento: "Dourados, MS",
    },
    {
      hasImage: false,
      image: 'opa',
      nomeEvento: "Evento abc",
      dataEvento: "27/11/2023",
      enderecoEvento: "Três Lagos, MS",
    }
  ])
}

onMounted(() => {
	eventoStore.getAllEvents()
})
</script>

<style scoped lang="scss">
  #container-cards-evento {
    header {
      h1 {
        font-size: 1.5rem;
        margin-bottom: 3rem;
      }
    }
    nav {
      .container-fluid {
        padding: 0;
        height: 500px;
        margin-bottom: 2rem;
      }
    }
    footer {
      display: flex;
      flex-direction: row;
      align-items: center;
      justify-content: center;
      margin-bottom: 2rem;
      margin-top: 2rem;
      .box {
        margin-left: 15px;
        margin-right: 15px;
      }
      .indicator {
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
      }
      .carousel-control-prev-icon, .carousel-control-next-icon {
        border: 0;
        background-color: unset;
        filter: invert(1) grayscale(100);
      }
      .active {
        opacity: 1 !important;
        height: 17px !important;
        width: 17px !important;
      }
    }
  }
  @media (max-width: 991px) {
    section#container-cards-evento {
      header {
        h1.dark-title {
          font-size: 1.2rem;
        }
      }
    }
  }
</style>