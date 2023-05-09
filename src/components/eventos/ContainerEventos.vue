<template>
  <section id="eventos-content" class="ghp pt-5">
    <header>
      <h1 class="dark-title">EVENTOS</h1>
    </header>

	<Spinner
		spinner-color-class="text-dark"
		v-if="loadingContent"
	/>
	<div class="card-evento-container pb-5">
		<nav v-for="(container, containerIndex) in eventos" :key="containerIndex">
			<CardEvento
			:evento="container"
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
		:class="{'d-none': (page*6) > eventoStore.eventos.length }"
		
      />
	  <Spinner
		spinner-color-class="text-dark"
		v-if="loadingMoreContent"
	/>
    </footer>
		
  

  </section>
</template>

<script setup lang="ts">

import { onMounted, ref } from 'vue';
import CardEvento from './CardEvento.vue';
import GeneralBtn from '../buttons/GeneralBtn.vue';
import { useEventoStore } from '../../stores/eventos/store';
import { IEventoSimplificado } from '../../stores/eventos/types';
import Spinner from '../general/Spinner.vue';

const eventoStore = useEventoStore()
const selectedPage = ref(0)
const indicators = ref(3)
const loadingContent = ref(false)
const loadingMoreContent = ref(false)
const page = ref(1)
let eventos = ref<Array<IEventoSimplificado>>()

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
const addEventsToView = async () => {
	page.value++
	loadingMoreContent.value = true
	await eventoStore.getAllEvents(page.value)
	eventos.value = eventoStore.eventos
	loadingMoreContent.value = false
}

onMounted(async () => {
	loadingContent.value = true
	await eventoStore.getAllEvents(page.value)
	eventos.value = eventoStore.eventos
	loadingContent.value = false
	
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