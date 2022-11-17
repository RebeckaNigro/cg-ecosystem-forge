<template>
  <section id="ultimos-eventos">
    <header>
      <h1 class="dark-title p-4">Últimos eventos enviados</h1>
    </header>
    <main class="d-flex mt-3 mb-3" v-if="!isLoadingLastEvents">
      <div class="card-ultimo-evento" v-for="(card, index) in useStore.ultimosEventos">
        <img class="capa-evento" src="..." alt="capa evento">
        <span class="title">{{ card.titulo }}</span>
        <div class="when">{{ friendlyDateTime(card.dataInicio) }}</div>
        <div class="when">{{ friendlyDateTime(card.dataTermino) }}</div>
        <div class="location">{{ card.local }}</div>
		<button class="edit-btn">
			<img src="/pen-icon.svg" alt="pen_icon">
		</button>
      </div>
    </main>
    <div v-else class="spinner-border text-dark" role="status">
      <span class="visually-hidden">Loading...</span>
    </div>
    <footer>
      <button type="button" class="btn btn-primary">Ver mais</button>
      <button type="button" class="btn btn-primary" @click="$router.push({ name: 'GerenciaEvento'})">Criar evento</button>
    </footer>
  </section>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useEventoStore } from '../../../stores/eventos/store';
  const friendlyDateTime = (dateTimeString: string) => {
    const date = new Date(dateTimeString).toLocaleDateString('pt-br')
    const time = new Date(dateTimeString).toLocaleTimeString('pt-br')
    return `${date} às ${time.slice(0, time.length - 3)}`
  }
  const isLoadingLastEvents = ref(true)
  const useStore = useEventoStore()
  onMounted(() => {
    useStore.getLastEvents()
    isLoadingLastEvents.value = false
  })
</script>

<style scoped lang="scss">
  section#ultimos-eventos {
    background-color: #e6e6e6;
    height: 450px;
    padding: 1.5rem;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    header > h1 {
      font-size: 1rem;
    }
    main {
      width: 800px;
      justify-content: space-between;
      .card-ultimo-evento {
        background-color: #fff;
        width: 250px;
        min-height: 150px;
        border: 1px solid black;
        padding: 1rem;
        img.capa-evento {
          min-height: 80px;
          min-width: 200px;
          border: 1px solid black;
        }
        .when, .location {
          background-repeat: no-repeat;
          background-position-y: center;
          background-size: contain;
          margin: 5px 0;
        }
        .title, .when, .location {
          white-space: nowrap;
          text-overflow: ellipsis;
          overflow: hidden;
        }
        .title {
          font-weight: bold;
        }
        .when {
          background-image: url('/eventos/calendar_icon.png');
        }
        .location {
          background-image: url('/eventos/pin_icon.png');
        }

		.edit-btn{
			background-color: unset;
   			border: 0;
			float: right;
			img{
				max-width: 25px;
			}
			
		}
      }
    }
    footer {
      button {
        margin: 0 10px;
      }
    }
  }
</style>