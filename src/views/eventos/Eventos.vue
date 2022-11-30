<template>
	
	<NavBar 
		:is-transparent="false"
	/>
	<section>
		<Banner
			path="/eventos/banner.png"
			img-alt="agenda de eventos"
			figcaption="eventos do ecossistema"
			:img-overlay="true"
		>
			<div class="header-content d-flex h-100 position-absolute top-0 ghp">
	
				<h1 class="dark-title">EVENTOS</h1>
				<p class="fs-5">Tenha acesso aos principais documentos disponibilizados pelo Ecossistema Local de Inovação - Campo Grande - MS</p>
			</div>
	
		</Banner>
	
		<!--<div class="container">

			<div class="filters row justify-content-between align-items-end mb-3 mt-5">
				<div class="filtrar-area col">
					<FilterComponent 
						content-type="área"
						:datas="[]"
						class="mx-4"
					/>
				</div>
		
				<div class="filtrar-organizador col">
					<FilterComponent
						content-type="organizador"
						:datas="[]"
						class="mx-4"
						
					/>
		
				</div>
				<div class="pesquisar col">
					
					<input type="text" name="pesquisar" id="pesquisar" placeholder="Pesquisar" v-model="searchInputText">
					<button class="btn" id="btn-pesquisar" @click="handleSearch">
						<img src="../../../../../public/search_icon.svg" alt="Pesquisar">
					</button>
				</div>
			</div>
		</div>-->

	
		<Destaques />
		<ContainerEventos />
		<FooterComponent />


	</section>
</template>

<script setup lang="ts">
import NavBar from '../../components/general/NavBar.vue';
import Banner from '../../components/general/Banner.vue';
import Destaques from '../../components/eventos/Destaques.vue';
import ContainerEventos from '../../components/eventos/ContainerEventos.vue';
import FooterComponent from '../../components/general/FooterComponent.vue';
import FilterComponent from '../../components/general/FilterComponent.vue';
import SearchComponent from '../../components/general/SearchComponent.vue';
import { ref } from 'vue';
import { IEvento } from '../../stores/eventos/types';
import { useEventoStore } from '../../stores/eventos/store';

const searchInputText = ref('')
const eventoStore = useEventoStore()
let eventos = ref<Array<IEvento>>()

const handleSearch = () => {
	const inputTextLowerCase = searchInputText.value.trim().toLowerCase()
	
	eventos.value = eventoStore.eventos.filter((evento) => {
		if(evento.titulo){
			const tituloLowerCase = evento.titulo.toLowerCase()
			return tituloLowerCase.includes(inputTextLowerCase)
		}
		return false
		
	})
	
}
</script>

<style scoped lang="scss">

	section{
		background-color: #FBFBFB;
	.header-content {
		  justify-content: center;
	  
		  flex-direction: column;
		  h1 {
			
			text-align: start;
		  
		  }
		  p {
			font-size: 1.5rem;
			font-family: 'Montserrat-SemiBold', sans-serif;
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
	  .filtrar-area, .filtrar-organizador{
		max-width: 300px;
	  }

	  .filters{
		margin: 0 auto;
	  }
	
		.pesquisar{
	
			#pesquisar {
				border-radius: 40px 0px 0px 40px;
				border: 1px solid #6B6A64;
				border-right: unset;
				padding: .35rem .7rem .52rem .7rem;
				width: 300px;
				
				
			}
		
			#btn-pesquisar {
				border: 1px solid #6B6A64;
				
				border-radius: 0 40px 40px 0;
				background-color: #fff;
				height: 100%;
				
				img {
					max-width: 26px;
				}
			}
		
			#pesquisar:focus {
				border-color: #86b7fe;
				outline: 0;
				box-shadow: 0 0 0 .25rem rgba(13, 110, 253, .25);
			}

			@media (max-width: 580px) {
				margin-top: 1.5rem;
			}
		}
	}
  @media (max-width: 1200px) {
    .header-content {
      h1 {
        font-size: 2rem;
        text-align: start;
       
	}
	p {
	 
	  max-width: 350px;
	}
    }
  }
  @media (max-width: 991px) {
    .header-content {
      padding: 2rem;
      h1 {
        font-size: 1.5rem;
        text-align: start;
    
	}
	p {
	  max-width: 200px;
	}
    }
  }
  @media (max-width: 768px) {
	.header-content {
    padding: 0 50px;
      h1 {
        font-size: 1rem;
        text-align: start;
	}
	p {
	  font-size: 0.7rem;
	}
    }
  }
  @media (max-width: 576px) {
	.header-content {
		h1 {
			font-size: 0.8rem;
			text-align: start;
			margin-bottom: 0;
		}
		p {
		max-width: 100px;
		text-align: start;
		margin-top: 0;
		-webkit-line-clamp: 3; /* number of lines to show */
				line-clamp: 3; 
		}
    }
  }
</style>