<template>
	<NavBar :is-transparent="false" />
  <Banner
    path="/documentos/pesquisas.png"
    img-alt="documentos hub"
    figcaption="cover documentos"
	:img-overlay="true"
  >
    <div class="banner-documentos-content ghp">
      <div>
		<h1 class="dark-title" v-if="leisContainer">LEIS</h1>
        <h1 class="dark-title" v-else>PESQUISAS</h1>
		<p class="dark-body-text" v-if="leisContainer">Leis que apoiam o desenvolvimento e inovação em Campo Grande e Mato Grosso do Sul.</p>
        <p class="dark-body-text" v-else>Tenha acesso às principais pesquisas disponibilizados pelo Ecossistema Local de Inovação - Campo Grande - MS</p>
      </div>
    </div>
  </Banner>
  <section id="pesquisa-documento" class="container-fluid">

	<!--
		Filtros e pesquisa
	-->
    <main class="ghp row mt-5 mb-5">

	  <p class="dark-title text-start">Todos</p>

	  <div class="d-flex pesquisas-container" v-if="!leisContainer">
		<CardDocumento
			nome-documento="Nome do documento"
	  
		  descricao="Lorem ipsum dolor sit amet consectetur. Tortor non aliquam enim in nunc at aliquam."
		  tags="#Pesquisas #Doutorado"
		/>

		<CardDocumento
			nome-documento="Nome do documento"
	  
		  descricao="Lorem ipsum dolor sit amet consectetur. Tortor non aliquam enim in nunc at aliquam."
		  tags="#Pesquisas #Doutorado"
		/>

		<CardDocumento
			nome-documento="Nome do documento"
	  
		  descricao="Lorem ipsum dolor sit amet consectetur. Tortor non aliquam enim in nunc at aliquam."
		  tags="#Pesquisas #Doutorado"
		/>
	</div>
      <div v-for="(doc, index) in docs.value" class="col-12 col-sm-6 col-lg-4 w-fit-content" v-if="leisContainer">
        <CardSessao
          :title="doc.title"
          iconPath="none"
          :text="doc.description"
          :hasIcon="false"
          :contentToDownload="doc.downloadPath"
          :hasDownload="true"
        />
      </div>
    </main>
  </section>
  <FooterComponent />
</template>

<script setup lang="ts">
import { computed, onBeforeMount, reactive, ref } from 'vue';
import Banner from '../../../components/general/Banner.vue';

import { useRoute } from 'vue-router';
import CardDocumento from '../../../components/documentos/CardDocumento.vue';
import CardSessao from '../../../components/documentos/CardSessao.vue';

const route = useRoute()
const leisContainer = ref(false)
  const dummyResearchs = reactive([
    {
      title: 'NOME DA PESQUISA',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DA PESQUISA',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DA PESQUISA',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DA PESQUISA',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DA PESQUISA',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DA PESQUISA',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DA PESQUISA',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DA PESQUISA',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DA PESQUISA',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    }
  ])
  const dummyEdicts = reactive([
    {
      title: 'NOME DO EDITAL',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DO EDITAL',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DO EDITAL',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DO EDITAL',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DO EDITAL',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DO EDITAL',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DO EDITAL',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DO EDITAL',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    },
    {
      title: 'NOME DO EDITAL',
      description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Possimus vel quidem reprehenderit iste vero exercitationem veniam! Eveniet eius sequi dolorem cupiditate? Porro alias harum similique aliquid eos soluta deserunt distinctio?',
      downloadPath: ''
    }
  ])
  const docsToShow = computed( async () => {
    if (route.params.tipoDocumento === 'pesquisas') return dummyResearchs
    else if (route.params.tipoDocumento === 'editais') return dummyEdicts
    else {
      const docs = await getDocs()
      return docs
    }
  })
  const docs: any = reactive([])
  const getDocs = async () => {
    const raw = await fetch('/chumbado/documentos/documentos.json');
    return await raw.json()
  }
  onBeforeMount( async () => {
    if (route.params.tipoDocumento === 'leis') {
      docs.value = await getDocs()
	  leisContainer.value = true
    }
  })
</script>

<style scoped lang="scss">
  .banner-documentos-content {
    position: absolute;
    top: 0;
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    img {
      width: 10%;
      min-width: 60px;
      max-width: 128px;
      margin-right: 24px;
    }
    h1 {
		font-size: 2rem;
        text-align: start;
    }

	p{
		font-family: 'Montserrat-Medium', sans-serif;
		font-size: 1.5rem;
		text-align: start;
		max-width: 70%;
	}
    
  }
  section#pesquisa-documento {
    main {
      justify-content: space-between;
		
	  .pesquisas-container{
		justify-content: space-between;
		width: 100%;
	  }
      .w-fit-content {
        width: fit-content !important;
      }
    }
  }
  @media (max-width: 991px) {
    section#pesquisa-documento {
      main {
        justify-content: space-around;
      }
    } 
  }
</style>