<template>
    <!-- <router-link to="/parceiros/id">clica aqui</router-link> -->
    <Banner
            path="/parceiros/banner.png"
            figcaption="parceiros do ecossistema"
            imgAlt="ecosystem partners"
            maxHeight="unset"
    >
        <div class="header-content d-flex h-100 position-absolute top-0 ghp">
            <h1 class="dark-title">PARCEIROS</h1>
            <p>
                Confira os parceiros atuais do Ecossistema Local de Inovação - Campo Grande - MS
            </p>

        </div>
    </Banner>
    <section class="parceiros ghp">
        <ContainerCardsParceiro v-for="(container, index) in parceirosContainers" :key="index"
                                :tipo-de-instituicao="container.tipoInstituicao"
                                :parceiros="container.parceiros"
                                :container-id="index"
        />
    </section>
</template>

<script setup lang="ts">
import NavBar from '../../components/general/NavBar.vue';
import FooterComponent from '../../components/general/FooterComponent.vue';
import ContainerCardsParceiro from '../../components/parceiros/ContainerCardsParceiro.vue';
import Banner from '../../components/general/Banner.vue';
import {useParceirosStore} from '../../stores/parceiros/store';
import {onMounted, reactive} from 'vue';

const parceirosContainers = reactive([
    {
        tipoInstituicao: 'ICTIs',
        parceiros: [
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/uems-e-incubadoras/uems.png',
                logoAlt: 'Uems',
                parceiroId: 2
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/ufms-e-incubadoras/ufms.png',
                logoAlt: 'Ufms',
                parceiroId: 3
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/eli-cgde/ICT-INOVA-BR.png',
                logoAlt: 'ICT Inova BR',
                parceiroId: 8
            },
        ]
    },
    {
        tipoInstituicao: 'EMPRESAS',
        parceiros: [
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/eli-cgde/RdasArteiras.png',
                logoAlt: 'República das arteiras',
                parceiroId: 16
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/eli-cgde/eng.png',
                logoAlt: 'ENG',
                parceiroId: 9
            }
        ]
    },
    {
        tipoInstituicao: 'MECANISMOS DE INOVAÇÃO',
        parceiros: [
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/sistema-s/Startup.png',
                logoAlt: 'Startup Sesi Fiems',
                parceiroId: 19
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/ufms-e-incubadoras/aginova.png',
                logoAlt: 'Aginova',
                parceiroId: 4
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/ucdb-e-incubadoras/ucdb.jpg',
                logoAlt: 'UCDB',
                parceiroId: 1
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/pmcg-e-incubadora/incubadora-municipal.png',
                logoAlt: 'Incubadora Municipal',
                parceiroId: 13
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/eli-cgde/ecoinova_oficial.png',
                logoAlt: 'EcoInova MS',
                parceiroId: 14
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/livinglab.png',
                logoAlt: 'Livinglab',
                parceiroId: 15
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/ufms-e-incubadoras/pime.png',
                logoAlt: 'Pime',
                parceiroId: 10
            }
        ]
    },
    {
        tipoInstituicao: 'GOVERNO',
        parceiros: [
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/governo-ms/fundect.png',
                logoAlt: 'Fundect',
                parceiroId: 7
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/governo-ms/semagro.png',
                logoAlt: 'Semagro',
                parceiroId: 11
            }
        ]
    },
    {
        tipoInstituicao: 'SOCIEDADE ORGANIZADA',
        parceiros: [
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/sistema-s/Sistema-Fiems.jpg',
                logoAlt: 'Sistema Fiems',
                parceiroId: 18
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/sistema-s/SENAR-MS.png',
                logoAlt: 'Senar',
                parceiroId: 5
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/sistema-s/SENAC.png',
                logoAlt: 'SENAC',
                parceiroId: 6
            },
            {
                hasLogo: true,
                logo: '/chumbado/home_parceiros/sistema-s/SENAI.png',
                logoAlt: 'SENAI',
                parceiroId: 12
            }
        ]
    }
])
const parceirosStore = useParceirosStore()
const getPartnerInfos = async () => {
    parceirosStore.$reset()
    const res = await fetch('/chumbado/parceiro_expandido/parceiros.json')
    const infos = await res.json()
    for (const i of infos) {
        parceirosStore.setPartner(i)
    }
}
onMounted(() => {
    getPartnerInfos()
})
</script>

<style scoped lang="scss">
section.parceiros {
  background-color: #f6f6f6;
  padding-top: 3rem !important;
  padding-bottom: 3rem !important;

}

.header-content {
  justify-content: center;

  flex-direction: column;

  h1 {

    text-align: start;
    font-size: 2rem;
  }

  p {
    font-size: 1.5rem;
    font-family: 'Montserrat-Medium', sans-serif;
    max-width: 700px;
    width: 100%;
    text-align: start;
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

    p {
      font-size: 1.1rem;
      text-align: start;
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
      font-size: 0.8rem;
      max-width: 350px;
    }
  }
}

@media (max-width: 991px) {
  .header-content {
    padding: 0 100px;

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
      max-width: 200px;
      text-align: start;
      margin-top: 0;
      -webkit-line-clamp: 3; /* number of lines to show */
      line-clamp: 3;
    }
  }
}
</style>