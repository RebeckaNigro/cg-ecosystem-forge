<template>
  <section class="container-fluid">
    <main>
      <Banner
        path="/home/fundo.png"
        figcaption="workspace_image"
        img-alt="workspace_image"
        :imgOverlay="true"
        maxHeight="650px"
      >
        <ExternalHeader
          title="ECOSSISTEMA LOCAL DE INOVAÇÃO"
          subtitle="Campo Grande - MS"
          paragraph="Uma comunidade pronta para contribuir com o seu desenvolvimento!"
        />
      </Banner>

      <div class="meet-eco-container p-5 border-0">
        <h2 class="light-title text-uppercase">Faça parte do ecossistema</h2>
        <div class="row mt-5">
          <div class="col-md-6 d-flex flex-column justify-content-center">
            <div class="d-flex flex-column align-self-center ms-lg-5 info-box">
              <p class="text mb-2">
                Nosso propósito é disponibilizar caminhos norteadores para todos que desejam
                empreender, crescer e inovar, independente do formato ou tamanho do negócio. Quer
                fazer parte disso? Faça adesão ao Pacto de Inovação abaixo.
              </p>

              <a
                class="white-btn-primary text-uppercase text-center align-self-center text-decoration-none"
                href="https://www.change.org/p/ades%C3%A3o-ao-pacto-de-inova%C3%A7%C3%A3o-ecossistema-de-inova%C3%A7%C3%A3o-de-campo-grande?recruiter=912086725&recruited_by_id=9066d550-e374-11e8-ba51-2f1d6e951c87&utm_source=share_petition&utm_campaign=share_for_starters_page&utm_medium=copylink"
                target="_blank"
                >Aderir ao pacto</a
              >
            </div>
          </div>

          <div class="col-md-6 d-none d-md-flex position-relative foto">
            <div class="borda position-absolute"></div>
            <img
              src="/home/people_and_postits.png"
              alt="Logo Ecossistema"
              class="meet-img align-self-start"
            />
          </div>
        </div>
      </div>
      <div class="circle-bottom"></div>

      <Partners :logos-url-array="logosParceiros" />
      <div class="more-info-container p-5 border-0">
        <div class="circle-top"></div>
        <h2 class="light-title text-uppercase">Mais sobre o ecossistema</h2>
        <div class="row">
          <div class="col-md-6 d-none d-md-block">
            <img src="/home/white_logo.png" alt="Logo Ecossistema" class="eco-logo img-fluid p-5" />
          </div>

          <div class="col-md-6 col-lg-5 d-flex flex-column justify-content-center">
            <p class="text mb-2 mt-3 mt-lg-0">
              A comunidade de Inovação em Campo Grande-MS está reunida para ajudar você a se
              qualificar, estruturar o seu projeto, abrir o seu negócio, facilitar processos e
              estimular o desenvolvimento socioeconômico local.
            </p>
            <p class="text mt-3 semibold-text fs-4">Quer fazer parte?</p>
            <button
              type="button"
              class="white-btn-primary text-uppercase mt-4"
              @click="$router.push('/fale-conosco')"
            >
              Fale conosco
            </button>
          </div>
        </div>
      </div>
    </main>
  </section>
</template>

<script setup lang="ts">
import GeneralContainer from '../components/general/GeneralContainer.vue';
import Banner from '../components/general/Banner.vue';
import Partners from '../components/parceiros/Partners.vue';
import ExternalHeader from '../components/general/ExternalHeader.vue';
const logosParceiros: string[] = [
  '/chumbado/home_parceiros/embrapa.png',
  '/chumbado/home_parceiros/INCT-Inst-Nacionais.png',
  '/chumbado/home_parceiros/INTERP_Incubadoras.png',
  '/chumbado/home_parceiros/LITECH.png',
  '/chumbado/home_parceiros/livinglab.png',
  '/chumbado/home_parceiros/oab.png',
  '/chumbado/home_parceiros/RedeMS.png',
  '/chumbado/home_parceiros/eli-cgde/ANGI.png',
  '/chumbado/home_parceiros/eli-cgde/coworking.png',
  '/chumbado/home_parceiros/eli-cgde/ecoinova_oficial.png',
  '/chumbado/home_parceiros/eli-cgde/eng.png',
  '/chumbado/home_parceiros/eli-cgde/ICT-INOVA-BR.png',
  '/chumbado/home_parceiros/eli-cgde/IFMS.png',
  '/chumbado/home_parceiros/eli-cgde/JERA.png',
  '/chumbado/home_parceiros/eli-cgde/ORTHOS-CGDE.png',
  '/chumbado/home_parceiros/eli-cgde/Park-Office.png',
  '/chumbado/home_parceiros/eli-cgde/RdasArteiras.png',
  '/chumbado/home_parceiros/eli-cgde/Serviço-AKI.png',
  '/chumbado/home_parceiros/eli-cgde/Statup-MS.png',
  '/chumbado/home_parceiros/eli-cgde/Território-do-Vinho.png',
  '/chumbado/home_parceiros/faculdades/Anhanguera.png',
  '/chumbado/home_parceiros/faculdades/Estácio.png',
  '/chumbado/home_parceiros/faculdades/FACSUL.png',
  '/chumbado/home_parceiros/faculdades/UNIGRAN.png',
  '/chumbado/home_parceiros/governo-ms/agraer.png',
  '/chumbado/home_parceiros/governo-ms/fundect.png',
  '/chumbado/home_parceiros/governo-ms/semadesc.png',
  '/chumbado/home_parceiros/ifms/ifms.png',
  '/chumbado/home_parceiros/pmcg-e-incubadora/incubadora-municipal.png',
  '/chumbado/home_parceiros/pmcg-e-incubadora/sidagro.png',
  '/chumbado/home_parceiros/sistema-s/Startup.png',
  '/chumbado/home_parceiros/sistema-s/Sistema-Fiems.png',
  '/chumbado/home_parceiros/sistema-s/SESI.png',
  '/chumbado/home_parceiros/sistema-s/SESC.png',
  '/chumbado/home_parceiros/sistema-s/SENAR-MS.png',
  '/chumbado/home_parceiros/sistema-s/SENAI.png',
  '/chumbado/home_parceiros/sistema-s/SENAC.png',
  '/chumbado/home_parceiros/sistema-s/SEBRAE.png',
  '/chumbado/home_parceiros/sistema-s/Instit-SENAI-Inovação.png',
  '/chumbado/home_parceiros/sistema-s/Centro-de-Inovação-SESI.png',
  '/chumbado/home_parceiros/ucdb-e-incubadoras/ucdb.jpg',
  '/chumbado/home_parceiros/ucdb-e-incubadoras/ucdb-inova.png',
  '/chumbado/home_parceiros/uems-e-incubadoras/elos.png',
  '/chumbado/home_parceiros/uems-e-incubadoras/Fênix.png',
  '/chumbado/home_parceiros/uems-e-incubadoras/uems.png',
  '/chumbado/home_parceiros/ufms-e-incubadoras/ufms.png',
  '/chumbado/home_parceiros/ufms-e-incubadoras/pime.png',
  '/chumbado/home_parceiros/ufms-e-incubadoras/aginova.png',
];
</script>

<style scoped lang="scss">
.semibold-text {
  font-weight: 600 !important;
}
section.container-fluid {
  padding: 0;

  .meet-eco-container,
  .more-info-container {
    h2.light-title {
      font-size: 1.8rem;
    }

    p.text {
      font-weight: 500;
      color: #fff;
      text-align: start;
      font-size: 1.2rem;
    }
  }

  .meet-eco-container {
    background: var(--linear-gradient-orange-1);
    height: 605px;

    .info-box {
      width: 60%;
      p.text {
        max-width: 490px;
      }

      a {
        color: #103265;
        width: 100%;
        font-size: 1.1rem;
        padding-top: 10px;
        margin-top: 3rem;
      }

      a:hover {
        color: #103265;
      }
    }

    .meet-img {
      border-radius: 10px;
      width: 70%;
    }

    .foto {
      z-index: 10;
      .borda {
        top: 7px;
        left: 10px;
        display: flex;
        height: 95%;
        width: 68%;
        border-radius: 15px;
        border: 2px solid #158a39;
        transform: skew(-12deg, -7deg);
        z-index: -1;
      }
    }
  }

  .more-info-container {
    background: var(--linear-gradient-orange-2);

    p.text {
      max-width: 560px;
    }

    button {
      width: 90%;
      font-size: 1.1rem;
      color: #103265;
    }

    .eco-logo {
      width: 60%;
    }
  }
}

.circle-bottom,
.circle-top {
  height: 25px;
  width: 50px;
  border-bottom-left-radius: 8rem;
  border-bottom-right-radius: 8rem;
  margin: 0 auto;
  position: relative;
}

.circle-bottom {
  top: -0.01rem;
  background: #c7845c;
}

.circle-top {
  top: -3.1rem;
  background: #fbfbfb;
}

/* MONITORES 4K E AFINS */
@media screen and (min-width: 2550px) {
  section.container-fluid {
    padding: 0;

    .meet-eco-container {
      height: 900px;
      .info-box {
        p.text {
          max-width: 700px;
        }

        a {
          font-size: 1.5rem;
        }
      }
    }

    .more-info-container {
      p.text {
        max-width: 700px;
      }

      p.semibold-text {
        font-size: 2rem !important;
      }
    }

    .more-info-container,
    .meet-eco-container {
      h2.light-title {
        font-size: 2.6rem;
      }

      p.text {
        font-size: 1.8rem;
      }

      button {
        font-size: 1.5rem;
      }
    }
  }

  .circle-bottom,
  .circle-top {
    height: 45px;
    width: 70px;
  }
}

/* TABLETS */
@media screen and (max-width: 1200px) {
  section.container-fluid {
    padding: 0;

    .meet-eco-container {
      height: 550px;
      .info-box {
        width: 80%;
      }
      .foto {
        .meet-img {
          width: 100%;
        }
        .borda {
          top: 5px;
          left: 10px;
          display: flex;
          height: 60%;
          width: 90%;
          border-radius: 15px;
          border: 2px solid #158a39;
          transform: skew(-15deg, -7deg);
          z-index: -1;
        }
      }
    }

    .more-info-container,
    .meet-eco-container {
      p.text {
        font-size: 1rem;
        max-width: 700px;
      }

      a,
      button {
        width: 100%;
        font-size: 1rem;
      }

      .eco-logo,
      .meet-img {
        width: 80%;
      }
    }
  }
}

/* CELULARES */
@media (max-width: 580px) {
  section.container-fluid {
    padding: 0;
    .meet-eco-container {
      height: 600px;
      h2.light-title {
        font-size: 1.6rem;
      }
      .info-box {
        width: 100%;
        p.text {
          max-width: 500px;
          font-size: 1.1rem;
        }
        a.white-btn-primary {
          width: 100%;
          margin: 1.4rem auto;
          font-size: 1.1rem;
          padding-top: 12px;
        }
      }
    }

    .more-info-container {
      height: 600px;
      padding: 2rem !important;
      h2.light-title {
        font-size: 1.6rem;
      }

      p.text {
        font-size: 1.1rem;
      }

      button {
        width: 100%;
        margin: 0.7rem auto;
        font-size: 1.2rem;
      }
    }
  }
}
</style>
