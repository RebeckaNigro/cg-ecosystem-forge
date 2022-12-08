<template>
  <NavBar 
    :is-transparent="false"
  />
  	<div class="background pt-5 pb-5">
	  <div class="noticia-breadcrumb">
		<nav style="--bs-breadcrumb-divider:'>';" aria-label="breadcrumb">
			<ol class="breadcrumb">
				<li class="breadcrumb-item unactive" @click="$router.push({name: 'Noticias'})">Notícias</li>
				<li class="breadcrumb-item unactive" aria-current="page">Destaques</li>
				<!--<li class="breadcrumb-item active" aria-current="page">Ciências</li>-->
			</ol>
		</nav>
	  </div>
	  <section class="noticia-expandida ghp box pt-3">
		<header class="d-flex">
		  <h1>{{noticia.titulo}}</h1>
		  <p>{{noticia.subTitulo}}</p>
		  <!--<p class="nome-autor">Nome do autor</p>-->
		  <time>{{friendlyDateTime(noticia.dataPublicacao)}}</time>
		</header>
		<img class="w-100" src="/noticias/noticia-expandida/cover.png" alt="capa da notícia">
		<p id="first-paraghap">
		 {{noticia.subTitulo}}
		</p>
		<main>
		  
		  <article class="news-body">
			{{noticia.descricao}}
		  </article>
	
		  <!--<img src="/noticias/noticia-expandida/fakevideo.png" alt="fake-video" class="w-100">-->
		</main>
		<!--<NoticiasRelacionadas />-->
	  </section>

	  <div class="btn-up-container" @click="handleNavigateUp">
		  <button class="btn-up">
			<img src="/public/noticias/noticia-expandida/arrow_up.svg" alt="Seta para cima">
			Subir pro topo
		  </button>
	  </div>
  	</div>
 
  <FooterComponent/>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useNoticiaStore } from '../../../stores/noticias/store';
import { INoticia } from '../../../stores/noticias/types';
import { friendlyDateTime } from '../../../utils/formatacao/datetime';

const store = useNoticiaStore()
const noticia = ref<INoticia>({
		id: "13",
		titulo: "Em meio a cobranças, Salgado vai ao CT do Vasco e conversa com jogadores e comissão técnica",
		descricao: "O mandatário discutiu junto com os líderes do futebol o planejamento do Vasco daqui em diante e fez algumas ponderações, principalmente em relação à parte física do elenco. Pegou mal, por exemplo, o fato de alguns jogadores reclamarem de cãibra no primeiro jogo da Série B. Foi solicitada uma revisão dos protocolos de preparação física e avaliações individuais dos atletas. Em um momento da visita, Salgado conversou com Nenê sobre o episódio do empate com o Vila Nova, na última sexta. O meia-atacante saiu irritado ao ser substituído no segundo tempo da partida e jogou a braçadeira de capitão longe. Os dois colocaram os ''pingos nos is'' e deixaram a polêmica para trás. O veterano ainda deve se pronunciar publicamente sobre a situação.",
		subTitulo: "Presidente discute planejamento do departamento de futebol, solicita revisão dos protocolos e fala com Nenê sobre episódio da última sexta-feira, quando meia se irritou com substituição",
		dataPublicacao: "2022-04-12T01:28:06.807",
	})
  const handleNavigateUp = () => {
	window.scrollTo(0, 0)
  }

  onMounted(() => {
	
	
  })
</script>

<style scoped lang="scss">
  .background{
	background-image: url('/public/user/background.svg');
	background-repeat: no-repeat;
	background-size: cover;
	width: 100%;
  }

  .btn-up-container{
	  margin: 0 auto;
	  display: flex;
	  justify-content: flex-end;
	  max-width: 1300px;
	  .btn-up{
		background-color: #fff;
		padding: .7rem;
		border: 1px solid #505050;
		border-radius: 10px;
		font-family: 'Montserrat-SemiBold', sans-serif;
	  }

	  .btn-up:hover{
		background-color: rgb(232, 229, 229);
	  }
  }

  .noticia-expandida {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
	margin: 3rem auto;
	max-width: 1300px;
    header {
      flex-direction: column;
      margin-top: 3rem;
	  margin-bottom: 3rem;
	  width: 100%;
      h1 {
		color: #000;
        font-size: 2rem;
		text-align: start;
		font-family: 'Montserrat-SemiBold', sans-serif;
      }
      p {
        font-family: 'Montserrat-Regular', sans-serif;
        text-align: start;
        font-size: 1.2rem;
      }
	  time{
		color: #6B6A64;
		font-size: 1rem;
		text-align: start;
	  }
	  .nome-autor{
		font-family: 'Montserrat-SemiBold', sans-serif;
		font-size: 1rem;
		color: #000;
		background: url('/public/noticias/noticia-expandida/user.svg');
		background-repeat: no-repeat;
		background-position-y: center;
		background-size: contain;
		padding-left: 2rem;
	  }

    }
	#first-paraghap{
	  font-family: 'Montserrat-Medium', sans-serif;
	  text-align: start;
	  font-size: .8rem;
	}
   .news-body{
		font-family: 'Montserrat-Regular', sans-serif;
	  	text-align: start;
		font-size: 1.25rem;
		margin-top: 1.5rem;
		margin-bottom: 3rem;
   }

}
	.noticia-breadcrumb{
		display: flex;
		width: fit-content;
		margin-left:15%;
		font-size: 20px;

		.active{
			font-family: 'Montserrat-Medium', sans-serif;
		}

		.unactive{
			cursor: pointer;
			font-family: 'Montserrat-Light', sans-serif;
		}

		@media(max-width: 920px){
			font-size: 17px;
		}
	}
  @media (max-width: 1200px) {
  .noticia-expandida-banner{
    .text-container {
      h1 {
        font-size: 2rem;
      }
      p {
        font-size: 0.8rem;
      }
    }
  }
  }
  @media (max-width: 991px) {
    .noticia-expandida-banner{
      .text-container {
        max-width: 300px;
        h1 {
          font-size: 1.5rem;
        }
        p {
          max-height: 60px;
        }
      }
    }
  .noticia-expandida {
    header {
      margin-top: 2rem;
      h1 {
        font-size: 1.5rem;
      }
      p.dark-body-text {
        font-size: 1.2rem;
      }
    }
    .dark-body-text {
      font-size: 1rem;
      text-align: justify;
    }
    main {
      h3 {
        font-size: 1.5rem;
      }
    }
  }
  }
  @media (max-width: 768px) {
    .noticia-expandida-banner{
      .text-container {
        max-width: 200px;
        h1 {
          font-size: 1rem;
        }
        p {
          max-height: 40px;
          font-size: 0.7rem;
        }
      }
    }
  }
  @media (max-width: 576px) {
    .noticia-expandida-banner{
      .text-container {
        h1 {
          font-size: 0.8rem;
        }
      }
    }
    .noticia-expandida {
      header {
        h1 {
          font-size: 1.3rem;
        }
        p.dark-body-text {
          font-size: 1rem;
        }
      }
      .dark-body-text {
        font-size: 1rem;
        text-align: justify;
      }
      main {
        h3 {
          font-size: 1.3rem;
        }
      }
    }
  }
</style>