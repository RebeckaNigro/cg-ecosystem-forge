<template>
    <section class="quem-somos">
        <Banner
                path="/quem-somos/banner.png"
                figcaption="about-us"
                img-alt="sobre nós"
                :imgOverlay="true"
                maxHeight="650px"
        >
            <div class="header-content d-flex h-100 position-absolute top-0 ghp">
                <!-- <h1 class="dark-title">SLOGAN PARA<br>APRESENTAR<br>QUEM SOMOS</h1> -->
                <h1 class="dark-title">ECOSSISTEMA LOCAL DE INOVAÇÃO</h1>
                <p class="subtitle">Campo Grande - MS</p>
                <p>Uma comunidade pronta para contribuir com o seu desenvolvimento!</p>
            </div>
        </Banner>


        <GeneralContainer
                title="DE ONDE SURGIMOS"
                :darkTitle="true"
                :hasKnowMoreBtn="false"
                :contentArray="[
		'O Ecossistema Local de Inovação – Campo Grande – MS, surge do desejo das instituições e unificar as competências tecnológicas de CAMPO GRANDE na direção do incentivo a inovação e tecnologia. Foi feito um estudo preliminar, para apurar o que temos de capacidade tecnológica e ciências para que essas capacidades sejam mais trabalhadas em prol do desenvolvimento socioeconômico de Campo Grande - MS. ',
		'/quem-somos/more_about_us.png'
		]"
                bgColor="#fbfbfb"
                id="our-story"
                font-size="1.3rem"
                font-family="Montserrat-Medium"
                :has-contact-btn="false"
                :has-circle-top-border="false"
                :has-circle-bottom-border="false"
        />
        <GeneralContainer
                title=""
                :darkTitle="true"
                :hasKnowMoreBtn="false"
                :contentArray="[
		'/quem-somos/our_story.png',
		'A partir disso, foi identificado que existem empresas que ainda não se beneficiam da capacidade tecnológica das instituições de ciência, tecnologia e inovação. Ou seja, mapeamos uma distância que pode ser aproveitada! Um grande grupo de apoio forma-se aqui para que as para que as ações sejam mais rápidas, efetivas e integradas em busca de melhorar a capacidade da inovação tecnológica e trazer o conhecimento para todos sobre as leis e benefícios proporcionados pela legislação.'
		]"
                bgColor="#fbfbfb"
                id="more-about-us"
                font-size="1.3rem"
                font-family="Montserrat-Medium"
                :has-contact-btn="false"
                :has-circle-top-border="false"
                :has-circle-bottom-border="false"
        />

        <GeneralContainer
                title="NOSSO PROPÓSITO"
                :darkTitle="false"
                :hasKnowMoreBtn="false"
                :contentArray="[
		'Disponibilizar caminhos norteadores para todos que desejam empreender, crescer e inovar, independente do formato ou tamanho do negócio. Nosso foco é nos tornarmos referência como polo científico e tecnológico para o Brasil.',
		'/quem-somos/purpose.png',
		]"
                bgColor="linear-gradient(180deg, rgba(255, 255, 255, 0.2) 0%, rgba(0, 0, 0, 0.2) 100%), #639280"
                id="purpose"
                font-size="1.3rem"
                font-family="Montserrat-SemiBold"
                :has-contact-btn="false"
                :has-circle-top-border="true"
                :has-circle-bottom-border="false"
        />
    </section>
</template>

<script setup lang="ts">
import {reactive} from 'vue';
import Banner from '../../components/general/Banner.vue';
import GeneralContainer from '../../components/general/GeneralContainer.vue';

const ourStoryContentArray = reactive([
    'O Ecossistema Local de Inovação – Campo Grande – MS, surge do desejo das instituições e unificar as competências tecnológicas de CAMPO GRANDE na direção do incentivo a inovação e tecnologia. Foi feito um estudo preliminar, para apurar o que temos de capacidade tecnológica e ciências para que essas capacidades sejam mais trabalhadas em prol do desenvolvimento socioeconômico de Campo Grande - MS. ',
    '/quem-somos/our_story.png'
])
const moreAboutUs = reactive([
    '/quem-somos/more_about_us.png',
    'A partir disso, foi identificado que existem empresas que ainda não se beneficiam da capacidade tecnológica das instituições de ciência, tecnologia e inovação. Ou seja, mapeamos uma distância que pode ser aproveitada! Um grande grupo de apoio forma-se aqui para que as para que as ações sejam mais rápidas, efetivas e integradas em busca de melhorar a capacidade da inovação tecnológica e trazer o conhecimento para todos sobre as leis e benefícios proporcionados pela legislação.'
])
</script>

<style scoped lang="scss">
section.quem-somos {
  background: #fbfbfb;

  .header-content {
    justify-content: center;

    flex-direction: column;

    h1 {

      text-align: start;
      font-size: 2rem;
    }

    .subtitle {
      font-size: 1.8rem;
      font-family: 'Montserrat-SemiBold', sans-serif;
    }


    p {
      font-size: 1.5rem;
      font-family: 'Montserrat-Medium', sans-serif;
      max-width: 500px;
      width: 100%;
      text-align: justify;
      margin-top: 10px;
      margin-bottom: 0;
      overflow: hidden;
      text-overflow: ellipsis;
      display: -webkit-box;
      -webkit-line-clamp: 4;
      /* number of lines to show */
      line-clamp: 4;
      -webkit-box-orient: vertical;
      color: #000;
    }

    @media(max-width: 580px) {
      h1 {
        font-size: 1.5rem;
      }

      .subtitle {
        font-size: 1.3rem;
      }


      p {
        font-size: 1.1rem;
        text-align: start;
      }
    }


  }
}

/* width */
::-webkit-scrollbar {
  width: 7px;
}

/* Track */
::-webkit-scrollbar-track {
  background-color: rgba($color: #ddd, $alpha: 1);
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: gray;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #fff;
}

@media (max-width: 1450px) {
  section.quem-somos {
    div.d-flex {
      h1 {
        font-size: 2.5rem;
      }

      p {
        font-size: 1rem;
        max-width: 400px;
      }
    }
  }
}

@media (max-width: 991px) {
  section.quem-somos {
    div.d-flex {
      h1 {
        font-size: 2rem;
      }

      p {
        font-size: 0.8rem;
        max-width: 350px;
      }
    }
  }
}

@media (max-width: 768px) {
  section.quem-somos {
    div.d-flex {
      h1 {
        font-size: 1.5rem;
      }

      p {
        font-size: 0.8rem;
        max-width: 250px;
        max-height: 150px;
      }
    }
  }
}

@media (max-width: 576px) {
  section.quem-somos {
    div.d-flex {
      h1 {
        font-size: 1rem;
      }

      p {
        font-size: 0.6rem;
        max-width: 170px;
        max-height: 100px;
      }
    }
  }
}
</style>