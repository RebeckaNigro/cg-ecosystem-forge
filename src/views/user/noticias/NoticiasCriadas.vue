<template>
    <div class="pb-5 pt-5">
        <div class="noticias-breadcrumb">

            <nav style="--bs-breadcrumb-divider:'>';" aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item unactive" @click="$router.push({name: 'Noticias'})">Notícias</li>
                    <li class="breadcrumb-item active" aria-current="page">Notícias criadas</li>
                </ol>
            </nav>
        </div>

        <ContainerCardsNoticiasCriadas/>
    </div>
</template>

<script setup lang="ts">
import {reactive} from 'vue';
import FilterComponent from '../../../components/general/FilterComponent.vue';
import FooterComponent from '../../../components/general/FooterComponent.vue';
import SearchComponent from '../../../components/general/SearchComponent.vue'
import ContainerCardsNoticia from '../../../components/noticias/ContainerCardsNoticia.vue';
import CardNoticiaCriada from '../../../components/user/noticias/noticiasCriadas/CardNoticiaCriada.vue';
import ContainerCardsNoticiasCriadas
    from '../../../components/user/noticias/noticiasCriadas/ContainerCardsNoticiasCriadas.vue'

const dummyContainer = reactive([
    {
        tags: ['Pesquisas', 'Doutorado'],
        titulo: 'Twitter falido',
        dataAtualizacao: '24/11/2022',
        horaAtualizacao: '11:20'
    },
    {
        tags: ['Pesquisas', 'Doutorado'],
        titulo: 'Twitter falido',
        dataAtualizacao: '24/11/2022',
        horaAtualizacao: '11:20'
    },
    {
        tags: ['Pesquisas', 'Doutorado'],
        titulo: 'Twitter falido',
        dataAtualizacao: '24/11/2022',
        horaAtualizacao: '11:20'
    },
    {
        tags: ['Pesquisas', 'Doutorado'],
        titulo: 'Twitter falido',
        dataAtualizacao: '24/11/2022',
        horaAtualizacao: '11:20'
    },
    {
        tags: ['Pesquisas', 'Doutorado'],
        titulo: 'Twitter falido',
        dataAtualizacao: '24/11/2022',
        horaAtualizacao: '11:20'
    },
    {
        tags: ['Pesquisas', 'Doutorado'],
        titulo: 'Twitter falido',
        dataAtualizacao: '24/11/2022',
        horaAtualizacao: '11:20'
    },
    {
        tags: ['Pesquisas', 'Doutorado'],
        titulo: 'Twitter falido',
        dataAtualizacao: '24/11/2022',
        horaAtualizacao: '11:20'
    },
])
</script>

<style scoped lang="scss">
.background {
  background-image: url('../../../../public/user/background.svg');
  background-repeat: no-repeat;
  background-size: cover;
  width: 100%;

  .noticias-breadcrumb {
    display: flex;
    width: fit-content;
    margin-left: 15%;
    font-size: 20px;

    .active {
      font-family: 'Montserrat-Medium', sans-serif;
    }

    .unactive {
      cursor: pointer;
      font-family: 'Montserrat-Light', sans-serif;
    }

    @media(max-width: 920px) {
      font-size: 17px;
    }
  }
}

.noticias-container {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  margin: 1.4rem auto;
  max-width: 1300px;
  padding: 3rem;
  border-radius: 10px;
  flex-wrap: wrap;

  .btn-criar-noticia {
    background-color: #639280;
    border: unset;
    border-radius: 30px;
    padding: .7rem 2rem;
    font-family: 'Montserrat-Bold';
    color: #fff;
    text-transform: uppercase;
    position: absolute;
    right: 18rem;
    top: 13.4rem;
  }

  .btn-criar-noticia:hover {
    background-color: #73b79d;
  }

  .filtrar-data {
    max-width: 300px;
  }

  .cards-container {
    width: 100%;
    display: grid;
    margin: 0 auto;
    grid-template-columns: 1fr 1fr 1fr;
    gap: 48px 32px;

    @media (max-width: 820px) {
      max-width: 520px;
      grid-template-columns: 1fr 1fr;
    }

    @media (max-width: 580px) {
      max-width: 280px;
      grid-template-columns: 1fr;
    }
  }

  .pesquisar {
    @media (max-width: 580px) {
      margin-top: 1.5rem;
    }
  }

  .botoes-container {
    button {
      width: 30%;
      border-radius: 30px;
      padding: .8rem 0;
      font-family: 'Montserrat-SemiBold';
      color: #fff;
      font-size: 19px;
    }

    .btn-voltar {
      background-color: #fff;
      border: 1px solid #639280;
      color: #639280;
    }

    .btn-voltar:hover {
      background-color: #ececec;

    }

    .btn-ver-mais {
      background-color: #639280;
      border: unset;
    }

    .btn-ver-mais:hover {
      background-color: #73b79d;
    }
  }

}
</style>