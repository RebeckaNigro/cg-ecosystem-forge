<template>
  <section id="noticias-relacionadas">
    <h1 class="dark-title">NOTÍCIAS RELACIONADAS</h1>
    <main class="d-flex">
      <CardNoticia 
        :is-relacionada="true"
		:noticia="null"
      />
      <CardNoticia 
        :is-relacionada="true"
		:noticia="null"
      />
    </main>
  </section>
</template>

<script setup lang="ts">
import CardNoticia from './CardNoticia.vue';
</script>

<style scoped lang="scss">
  section#noticias-relacionadas {
    margin-top: 2rem;
    margin-bottom: 2rem;
    h1 {
      text-align: start;
    }
    main {
      justify-content: start;
      align-items: center;
      flex-wrap: wrap;
    }
  }
@media (max-width: 768px) {
  section#noticias-relacionadas {
    main {
      justify-content: center;
    }
  }
}
@media (max-width: 576px) {
  
  section#noticias-relacionadas {
    h1 {
      font-size: 1.2rem;
    }
  }
}
</style>