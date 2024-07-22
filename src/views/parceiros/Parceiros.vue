<template>
  <!-- <router-link to="/parceiros/id">clica aqui</router-link> -->
  <Banner
    path="/parceiros/banner.png"
    figcaption="parceiros do ecossistema"
    imgAlt="ecosystem partners"
    maxHeight="unset"
  >
    <ExternalHeader
      title="Parceiros"
      subtitle=""
      paragraph="Confira os parceiros atuais do Ecossistema Local de Inovação - Campo Grande - MS"
    />
  </Banner>
  <section class="parceiros">
    <ContainerCardsParceiro :items="parceirosContainers" />
  </section>
</template>

<script setup lang="ts">
import ContainerCardsParceiro from '../../components/parceiros/ContainerCardsParceiro.vue';
import Banner from '../../components/general/Banner.vue';
import { useParceirosStore } from '../../stores/parceiros/store';
import { onMounted, reactive } from 'vue';
import ExternalHeader from '../../components/general/ExternalHeader.vue';

const parceirosContainers = reactive([
  {
    tipoInstituicao: 'ICTIs',
    parceiros: [
      {
        logo: '/chumbado/home_parceiros/uems-e-incubadoras/uems.png',
        id: 2,
        nome: 'UEMS',
      },
      {
        logo: '/chumbado/home_parceiros/ufms-e-incubadoras/ufms.png',
        id: 3,
        nome: 'UFMS',
      },
      {
        logo: '/chumbado/home_parceiros/eli-cgde/ICT-INOVA-BR.png',
        id: 8,
        nome: 'ICT Inova Brasil',
      },
      {
        logo: '/chumbado/home_parceiros/eli-cgde/IST.png',
        id: 24,
        nome: 'INSTITUTO SENAI DE TECNOLOGIA - EFICIÊNCIA OPERACIONAL',
      },
      {
        logo: "/chumbado/home_parceiros/eli-cgde/BIOMASSA.png",
        id: 25,
        nome: 'ISI Biomassa',
      },
    ],
    toggle: true,
  },
  {
    tipoInstituicao: 'EMPRESAS',
    parceiros: [
      {
        logo: '/chumbado/home_parceiros/eli-cgde/eng.png',
        id: 9,
        nome: 'ENG Soluções Tecnológicas',
      },
      {
        logo: '/chumbado/home_parceiros/FREITAS.png',
        id: 22 ,
        nome: 'Freitas e Castello Consultoria em Negócios',
      },
      {
        logo: '/chumbado/home_parceiros/LUIZ.png',
        id: 23 ,
        nome: 'Luiz Bino Advocacia Empresarial',
      },
    ],
    toggle: true,
  },
  {
    tipoInstituicao: 'MECANISMOS DE INOVAÇÃO',
    parceiros: [
      {
        logo: '/chumbado/home_parceiros/sistema-s/Startup.png',
        id: 19,
        nome: 'Startup Sesi Fiems',
      },
      {
        logo: '/chumbado/home_parceiros/ufms-e-incubadoras/aginova.png',
        id: 4,
        nome: 'AGINOVA',
      },
      {
        logo: '/chumbado/home_parceiros/pmcg-e-incubadora/incubadora-municipal.png',
        id: 13,
        nome: 'Incubadora Municipal',
      },
      {
        logo: '/chumbado/home_parceiros/livinglab.png',
        id: 15,
        nome: 'Living Lab MS',
      },
      {
        logo: '/chumbado/home_parceiros/ufms-e-incubadoras/pime.png',
        id: 10,
        nome: 'PIME',
      },
      {
        logo: '/chumbado/home_parceiros/RedeMS.png',
        id: 21,
        nome: 'RedeMS',
      },
    ],
    toggle: true,
  },
  {
    tipoInstituicao: 'GOVERNO',
    parceiros: [
      {
        logo: '/chumbado/home_parceiros/governo-ms/fundect.png',
        id: 7,
        nome: 'FUNDECT',
      },
      {
        logo: '/chumbado/home_parceiros/governo-ms/semadesc.png',
        id: 11,
        nome: 'SEMADESC',
      },
    ],
    toggle: true,
  },
  {
    tipoInstituicao: 'SOCIEDADE ORGANIZADA',
    parceiros: [
      {
        logo: '/chumbado/home_parceiros/sistema-s/SESI.png',
        id: 18,
        nome: 'SESI',
      },
      {
        logo: '/chumbado/home_parceiros/sistema-s/SENAR-MS.png',
        id: 5,
        nome: 'SENAR MS',
      },
      {
        logo: '/chumbado/home_parceiros/sistema-s/SENAC.png',
        id: 6,
        nome: 'SENAC',
      },
      {
        logo: '/chumbado/home_parceiros/sistema-s/SENAI.png',
        id: 12,
        nome: 'SENAI',
      },
      {
        logo: '/chumbado/home_parceiros/sistema-s/SEBRAE.png',
        id: 20,
        nome: 'SEBRAE',
      },
    ],
    toggle: true,
  },
]);
const parceirosStore = useParceirosStore();
const getPartnerInfos = async () => {
  parceirosStore.$reset();
  const res = await fetch('/chumbado/parceiro_expandido/parceiros.json');
  const infos = await res.json();
  for (const i of infos) {
    parceirosStore.setPartner(i);
  }
};
onMounted(() => {
  getPartnerInfos();
});
</script>

<style scoped lang="scss">
section.parceiros {
  background-color: #f6f6f6;
  padding-top: 3rem !important;
  padding-bottom: 3rem !important;
  padding: 3rem 9.3rem;
}

@media (max-width: 1200px) {
  section.parceiros {
    padding: 3rem 2rem;
  }
}

</style>
