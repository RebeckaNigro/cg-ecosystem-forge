<template>

  <section id="editais-container" class="container-fluid">

	<!--
		Filtros e pesquisa
	-->
    <main class="ghp row mt-5 mb-5">

	  
    </main>
  </section>
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
  .banner-editais-content {
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