<template>
  <section id="destaques-documentos">
    <header>
      <h1 class="dark-title">DESTAQUES</h1>
    </header>
    <main>
    <nav v-for="(cardPage, index) in dummyCardsPages">
      <div class="destaques" v-if="index === selectedPage">
        <div v-for="(card, index) in cardPage" :key="index" class="useless-box">
			<div class="header-card">
				<span>{{card.tags}}</span>
				<img src="/public/view_icon.svg" alt="Visualizar documento">
			</div>
			
			<div class="nome-documento">{{ card.nomeDocumento }}</div>
			<div class="nome-autor">Nome do autor</div>
			<div class="descricao-documento">Lorem ipsum dolor sit amet consectetur. Tortor non aliquam enim in nunc at aliquam.</div>
			<div class="atualizacao">Atualizado em: 01/01/2000 às 12:00</div>
      	</div>
	  </div>
    </nav>
    </main>
    <footer>
      <button type="button" @click.prevent="setSelectedPage(selectedPage - 1)" class="carousel-control-prev-icon"/>
      <div class="box-indicator">
        <button type="button" v-for="(indicator, index) in indicators" :key="indicator" class="indicator" :class="{'active': selectedPage === index}" @click.prevent="setSelectedPage(index)"/>
      </div>
      <button type="button" @click.prevent="setSelectedPage(selectedPage + 1)" class="carousel-control-next-icon"/>
    </footer>
  </section>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue'

const selectedPage = ref(0)
const indicators = ref(2)
const dummyCardsPages = reactive([
  [
    {
      hasImage: false,
      image: 'opa',
      nomeDocumento: "Decreto CTI MS",
	  descricao: 'Lorem ipsum dolor sit amet consectetur. Tortor non aliquam enim in nunc at aliquam.',
	  tags: '#Pesquisas #Doutorado'
    },
    {
      hasImage: false,
      image: 'opa',
      nomeDocumento: "Lei 6709 - Incubadoras Municipais",
	  descricao: 'Lorem ipsum dolor sit amet consectetur. Tortor non aliquam enim in nunc at aliquam.',
	  tags: '#Pesquisas #Doutorado'
    },
    {
      hasImage: false,
      image: 'opa',
      nomeDocumento: "Politica Municipal Inovação",
	  descricao: 'Lorem ipsum dolor sit amet consectetur. Tortor non aliquam enim in nunc at aliquam.',
	  tags: '#Pesquisas #Doutorado'
    }
  ],
  [
    {
      hasImage: false,
      image: 'opa',
      nomeDocumento: "Prodes Incentivo Fiscal Isenções",
	  descricao: 'Lorem ipsum dolor sit amet consectetur. Tortor non aliquam enim in nunc at aliquam.',
	  tags: '#Pesquisas #Doutorado'
    },
  ],
])

const setSelectedPage = (page: number) => {
  if (page >= indicators.value) {
    selectedPage.value = 0
  } else if (page < 0) {
    selectedPage.value = indicators.value - 1
  } else {
    selectedPage.value = page
  }
}
</script>

<style scoped lang="scss">

  section#destaques-documentos {
    padding-top: 3rem;
    padding-bottom: 3rem;

	.dark-title{
		font-family: 'Montserrat-SemiBold', sans-serif;
	}
    .destaques {
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      flex-wrap: wrap;
      min-height: 400px;
      height: fit-content;

	  
    }
  }
  .useless-box {
    background-color: #fff;
	border: 1px solid #6B6A64;
	border-radius: 10px;
    display: flex;
    flex-direction: column;
    justify-content: space-evenly;
    align-items: flex-start;
    height: 300px;
    width: 300px;
    margin: 10px 0;
	padding: 1rem;

	.header-card{
		display: flex;
		justify-content: space-between;
		width: 100%;

		span{
			font-family: 'Montserrat-Light', sans-serif;
			font-size: 0.8rem;
		}
	  }

	  .nome-documento{
		font-family: 'Montserrat-SemiBold', sans-serif;
		font-size: 1.5rem;
		text-align: start;
	  }

	  .nome-autor{
		font-family: 'Montserrat-Regular', sans-serif;
		font-size: 1rem;
	  }

	  .descricao-documento{
		font-family: 'Montserrat-Regular', sans-serif;
		font-size: .8rem;	
		text-align: start;  
	   }

	   .atualizacao{
		font-family: 'Montserrat-Regular', sans-serif;
		font-size: .7rem;
	   }
  }
  footer {
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: center;
    margin-bottom: 2rem;
    margin-top: 2rem;
    .box-indicator {
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
  @media (max-width: 1210px) {
    section#destaques-documentos {
      .destaques {
        justify-content: space-around;
      }
    }
  }
  @media (max-width: 576px) {
    .useless-box {
      height: 200px;
      width: 200px;
    }
    .dark-title {
      font-size: 1.2rem;
    }
  }
</style>