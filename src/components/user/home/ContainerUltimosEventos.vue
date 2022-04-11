<template>
  <section id="ultimos-eventos">
    <header>
      <h1 class="dark-title">Últimos eventos enviados</h1>
    </header>
    <main class="d-flex mt-3 mb-3">
      <div class="card-ultimo-evento" v-for="(card, index) in dummyCards">
        <img src="..." alt="capa evento">
        <div class="when">{{ card.when}}</div>
        <div class="location">{{ card.location}}</div>
      </div>
    </main>
    <footer>
      <button type="button" class="btn btn-primary">Ver mais</button>
      <button type="button" class="btn btn-primary" @click="$router.push({ name: 'GerenciaEvento'})">Criar evento</button>
    </footer>
  </section>
</template>

<script setup lang="ts">
import { reactive } from 'vue'
  const dummyCards = reactive([
    {
      when: 'whenwhenwhen',
      location: 'locationlocation'
    },
    {
      when: 'whenwhenwhen',
      location: 'locationlocation'
    },
    {
      when: 'whenwhenwhen',
      location: 'locationlocation'
    }
  ])
</script>

<style scoped lang="scss">
  section#ultimos-eventos {
    background-color: #e6e6e6;
    height: 350px;
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
        img {
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
        .when {
          background-image: url('/eventos/calendar_icon.png');
        }
        .location {
          background-image: url('/eventos/pin_icon.png');
        }
      }
    }
  }
</style>