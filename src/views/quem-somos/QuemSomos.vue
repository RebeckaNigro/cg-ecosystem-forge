<template>
  <section class="quem-somos">
    <Banner
      path="/quem-somos/banner.png"
      figcaption="about-us"
      img-alt="sobre nós"
      :imgOverlay="false"
      maxHeight="650px"
    >
      <ExternalHeader
        title="Ecossistema de inovação"
        subtitle="Campo Grande - MS"
        paragraph="Uma comunidade pronta para contribuir com o seu desenvolvimento!"
      />
    </Banner>

    <div class="container p-md-5 border-0">
      <h3 class="text text-uppercase">DE ONDE SURGIMOS</h3>

      <div class="row mt-5">
        <div class="col-md-6 d-flex flex-column justify-content-center">
          <div class="d-flex flex-column ms-lg-5">
            <p class="text mb-2">
              O Ecossistema Local de Inovação - Campo Grande - MS, surge do desejo das instituições
              e unificar as competências tecnológicas de CAMPO GRANDE na direção do incentivo a
              inovação e tecnologia.
            </p>

            <p class="text">
              Foi feito um estudo preliminar, para apurar o que temos de capacidade tecnológica e
              ciências para que essas capacidades sejam mais trabalhadas em prol do desenvolvimento
              socioeconômico de Campo Grande - MS.
            </p>
          </div>
        </div>

        <div class="col-md-6 d-flex position-relative parte1-foto">
          <img src="/fundo-achatado-3.svg" alt="" class="position-absolute parte1-fundo" />
          <img
            src="/quem-somos/more_about_us.png"
            alt="Foto ilustrativa"
            class="align-self-start img-fluid"
          />
        </div>
      </div>

      <div class="row flex-row-reverse mt-5">
        <div class="col-md-6 d-flex flex-column justify-content-center">
          <div class="d-flex flex-column justify-content-center ms-lg-5">
            <p class="text mb-2">
              A partir disso, foi identificado que existem empresas que ainda não se beneficiam da
              capacidade tecnológica das instituições de ciência, tecnologia e inovação. Ou seja,
              mapeamos uma distância que pode ser aproveitada!
            </p>

            <p class="text">
              Um grande grupo de apoio forma-se aqui para que as para que as ações sejam mais
              rápidas, efetivas e integradas em busca de melhorar a capacidade da inovação
              tecnológica e trazer o conhecimento para todos sobre as leis e benefícios
              proporcionados pela legislação.
            </p>
          </div>
        </div>

        <div class="col-md-6 d-flex position-relative parte2-foto">
          <img src="/fundo-achatado-4.svg" alt="" class="position-absolute parte2-fundo" />
          <img
            src="/quem-somos/our_story.png"
            alt="Foto ilustrativa"
            class="align-self-start img-fluid"
          />
        </div>
      </div>
    </div>
    <div class="purpose-container">
      <div class="circle-top"></div>
      <div class="container">
        <div class="row mt-4 p-md-5 justify-content-around">
          <div class="col-md-4 d-flex flex-column justify-content-center">
            <div class="d-flex flex-column text-start">
              <h3 class="text-uppercase">Missão</h3>
              <p class="mb-2">
                Apoiar o município de Campo Grande na transformação para um ambiente promotor do
                empreendedorismo inovador conectando a sociedade às oportunidades por meio da
                ciência, tecnologia e inovação.
              </p>
              <h3 class="text-uppercase mt-5">Visão</h3>
              <p class="mb-2">
                Ser referência nacional e internacional como ecossistema para desenvolvimento e
                atração de negócios inovadores apoiando a construção de Cidades mais Humanas,
                Inteligentes e Sustentáveis.
              </p>
            </div>
          </div>

          <div class="position-relative col-md-6 d-flex justify-content-center align-items-center">
            <img src="/fundo-achatado-1.svg" alt="" class="position-absolute img-achatada" />
            <img
              src="/quem-somos/purpose.png"
              alt="Foto ilustrativa"
              class="align-self-start img-fluid img-ilustrativa"
            />
          </div>
        </div>

        <div class="row flex-row-reverse mt-4 p-md-5">
          <div class="col-md-6 d-flex flex-column justify-content-center">
            <div class="d-flex flex-column text-start">
              <h3 class="text-uppercase">Valores</h3>
              <p class="mb-2">
                Ser referência nacional e internacional como ecossistema para desenvolvimento e
                atração de negócios inovadores apoiando a construção de Cidades mais Humanas,
                Inteligentes e Sustentáveis.
              </p>
              <h3 class="text-uppercase mt-5">Nosso propósito</h3>
              <p class="mb-2">
                Disponibilizar caminhos norteadores para todos que desejam empreender, crescer e
                inovar, independente do formato ou tamanho do negócio. Nosso foco é nos tornarmos
                referência como polo científico e tecnológico para o Brasil.
              </p>
            </div>
          </div>
          <div class="position-relative col-md-6 d-flex justify-content-center align-items-center">
            <img src="/fundo-achatado-2.svg" alt="" class="position-absolute img-achatada" />
            <img
              src="/quem-somos/purpose-2.png"
              alt="Foto ilustrativa"
              class="align-self-start img-fluid img-ilustrativa"
            />
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import Banner from '../../components/general/Banner.vue';

import ExternalHeader from '../../components/general/ExternalHeader.vue';
</script>

<style scoped lang="scss">
section.quem-somos {
  background: #fbfbfb;

  h3.text {
    font-weight: 600;
    font-size: 28px;
  }
  p.text {
    font-weight: 500;
    max-width: 460px;
    text-align: justify;
    font-size: 18px;
    text-indent: 60px;
  }

  .parte1-foto {
    z-index: 1;
    .parte1-fundo {
      width: 95%;
      height: 100%;
      z-index: -1;
    }
  }

  .parte2-foto {
    z-index: 1;
    .parte2-fundo {
      width: 93%;
      height: 98%;
      z-index: -1;
    }
  }

  img.img-ilustrativa {
    width: 80%;
    filter: drop-shadow(0px 0px 35px rgba(0, 0, 0, 0.15));
  }

  .purpose-container {
    background: var(--linear-gradient-orange-2);
    color: #fff;

    .img-achatada {
      width: 85%;
    }
    h3 {
      font-weight: 700;
    }

    p {
      font-weight: 500;
      width: 450px;
    }
    .circle-top {
      height: 25px;
      width: 50px;
      border-bottom-left-radius: 8rem;
      border-bottom-right-radius: 8rem;
      margin: 0 auto;
      position: relative;
      top: 0;
      background: #fbfbfb;
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

/* MONITORES 4K E AFINS  */
@media screen and (min-width: 2550px) {
  section.quem-somos {
    h3.text {
      font-size: 34px;
    }
    p.text {
      font-size: 20px;
    }
  }
}

/* TABLETS */
@media screen and (max-width: 1200px) {
  section.quem-somos {
    h3.text {
      font-size: 22px;
      padding-top: 40px;
    }
    p.text {
      font-size: 13px;
    }

    .purpose-container {
      padding: 0 10px 10px 10px;
    }
  }
}

/* CELULARES */
@media (max-width: 580px) {
  section.quem-somos {
    h3.text {
      font-size: 22px;
      padding-top: 40px;
    }
    p {
      font-size: 15px;

      padding: 0 10px;
    }

    img {
      width: 100%;
      margin: 10px 0;
    }
    .purpose-container {
      h3 {
        padding: 0 10px;
      }
      p {
        font-weight: 500;
        width: 350px;
        padding: 0 10px;
      }
    }
  }
}
</style>
