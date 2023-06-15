<template>
    <Banner
      :path="parceiroSelecionado.coverPath"
      figcaption="graphs"
      img-alt="partner page"
      max-height="352px"
    >
    <div class="d-flex h-100 w-100 partner-logo-container">
      <div class="box">
        <img v-if="hasLogo" :src="parceiroSelecionado.logoPath" alt="Logo Parceiro" id="parceiro-logo">
        <h1 v-else class="dark-title">Logo<br>Parceiro</h1>
      </div>
    </div>
    </Banner>
    <ExpandedBody
      :partner-name="parceiroSelecionado.parceiroNome"
      :first-paragraph="parceiroSelecionado.primeiroParagrafo"
      :sub-title="parceiroSelecionado.subTitulo"
      :second-paragraph="parceiroSelecionado.segundoParagrafo"
    />
    <ContatoParceiro
      :instaLink="parceiroSelecionado.instaLink"
      :linkedinLink="parceiroSelecionado.linkedinLink"
      :wppLink="parceiroSelecionado.wppLink"
      :email-contato="parceiroSelecionado.emailContato"
      :telefone="parceiroSelecionado.telefone"
      :website="parceiroSelecionado.website"
      :endereco="parceiroSelecionado.endereco"
    />
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from 'vue';
import { useParceirosStore } from '../../../stores/parceiros/store';
import { useRoute } from 'vue-router';
import Banner from '../../../components/general/Banner.vue';
import ExpandedBody from '../../../components/parceiros/parceiroExpandido/ExpandedBody.vue';
import ContatoParceiro from '../../../components/parceiros/ContatoParceiro.vue';

const hasLogo = ref(true) // Essa variável de controle supõe que a empresa parceira tem logo.
const route = useRoute() // acesso à rota para buscar o id do parceiro
const parceirosStore = useParceirosStore() // acesso ao store
const parceiroSelecionado = computed(() => { // computed que retorna o parceiro selecionado para o template
  const p = parceirosStore.getPartner(route.params.parceiroId.toString());
  if (p) return p
  else return {
    parceiroId: '-1',
    parceiroNome: 'Algo falhou :(',
    primeiroParagrafo: '',
    subTitulo: '',
    segundoParagrafo: '',
    coverPath: '',
    logoPath: '',
    instaLink: '',
    linkedinLink: '',
    wppLink: '',
    emailContato: '',
    telefone: '',
    website: '',
    endereco: ''
  }
})
const imageWidthBiggerThanHeight = () => {
  const logo: HTMLImageElement = document.querySelector('#parceiro-logo')!
  if (!logo.complete) hasLogo.value = false
  else if (logo.clientWidth > logo.clientHeight) {
    logo.classList.add('wider-image')
  } else if (logo.clientWidth === logo.clientHeight) {
    logo.classList.add('squared-image')
  } else {
    logo.classList.add('taller-image')
  }
}
onMounted(() => {
  imageWidthBiggerThanHeight()
})
</script>

<style scoped lang="scss">
  .partner-logo-container {
    position: absolute;
    top: 150px;
    align-items: center;
    justify-content: center;

    .has-no-img {
      background-color: #fff;
      max-height: 365px;
      min-height: 120px;
      height: 100%;
      max-width: 300px;
      min-width: 120px;
      width: 100%;
    }
    .box {
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-content: center;
      background-color: #fff;
      padding: 10px;
      border-radius: 10px;
      h1.dark-title {
        text-align: center;
        font-size: 1.5rem;
      }
      .wider-image {
        max-width: 300px;
        width: 100%;
        height: 100px;
      }
      .taller-image {
        height: 200px;
        width: 150px;
      }
      .squared-image {
        height: 200px;
        width: 200px;
      }
    }
  }
  @media (max-width: 991px) {
    .partner-logo-container {
      h1.dark-title {
        font-size: 1rem !important;
      }
    }
  }
  @media (max-width: 576px) {
    .partner-logo-container {
      .box {
        max-width: 200px;
        max-height: 265px;
        min-height: 20px;
        min-width: 20px;
      }
      h1.dark-title {
        font-size: 0.5rem !important;
      }
    }
  }
  @media (max-width: 400px) {
    .partner-logo-container {
      .box {
        max-width: 100px;
      }
    }
  }
</style>