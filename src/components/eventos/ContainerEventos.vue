<template>
  <section id="eventos-content" class="ghp pt-5">
    <header>
      <h1 class="dark-title">EVENTOS</h1>
    </header>

	
	<div class="card-evento-container mb-5">
		<nav v-for="(container, containerIndex) in eventos" :key="containerIndex">
			<CardEvento
			:hasImage="false"
			:image="container.arquivo"
			:nomeEvento="container.titulo"
			:dataInicio="container.dataInicio"
			:dataTermino="container.dataTermino"
			:enderecoEvento="container.local"
			:eventoId="container.id"
			/>
		</nav>
	</div>
	
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
        bgColor="#639280"
        width="200px"
        textColor="#fff"
        height="40px"
        id="know_more"
        @click="addEventsToView()"
		class="mb-4"
		:class="{'d-none': lastIndex > eventoStore.eventos.length }"
      />
    </footer>
		
  

  </section>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import CardEvento from './CardEvento.vue';
import GeneralBtn from '../buttons/GeneralBtn.vue';
import { useEventoStore } from '../../stores/eventos/store';
import { IUltimoEvento } from '../../stores/eventos/types';

const eventoStore = useEventoStore()
const selectedPage = ref(0)
const indicators = ref(3)

const lastIndex = ref(3)
let eventos = ref<Array<IUltimoEvento>>()

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
	lastIndex.value += 3
	
	eventos.value = eventoStore.eventos.slice(0, lastIndex.value)
}

onMounted(async () => {
	await eventoStore.getAllEvents()
	eventos.value = eventoStore.eventos.slice(0, 3)
})
</script>

<style scoped lang="scss">
  #eventos-content {
	background: linear-gradient(180deg, rgba(255, 255, 255, 0.2) 0%, rgba(0, 0, 0, 0.2) 100%), #F8F9FA;

    header {
      h1 {
        font-size: 2rem;
		font-family: 'Montserrat-SemiBold', sans-serif;
        margin-bottom: 3rem;
      }
	
    } 


	.card-evento-container{
		width: 100%;
		display: grid;
		margin: 0 auto;
		grid-template-columns: 1fr 1fr 1fr;
		gap: 48px 32px;

		@media (max-width: 820px) {
			max-width: 520px;
			grid-template-columns: 1fr 1fr;
		}

		@media (max-width: 580px) {
			max-width: 280px;
			grid-template-columns: 1fr;
		}
	}
  @media (max-width: 991px) {
    section#eventos-content {
      header {
        h1.dark-title {
          font-size: 1.2rem;
        }
      }
    }
  }

  }

</style>