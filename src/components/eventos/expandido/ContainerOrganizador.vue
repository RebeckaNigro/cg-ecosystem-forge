<template>
  <section id="organizador" class="ghp pt-5 pb-5">
    <header>
      <h1 class="dark-title mb-5">SOBRE O ORGANIZADOR</h1>
    </header>
    <main class="d-flex">
      <div class="organizador-about">
        <img v-if="false" src="" alt="" class="logo" />
        <div v-else class="logo" style="background-color: white" />
        <GeneralBtn
          btnText="PÁGINA DO ORGANIZADOR"
          :isExternalLink="true"
          link="#"
          bgColor="#c88a45"
          width="250px"
          textColor="#fff"
          height="40px"
          id="page-organizer"
        />
      </div>
      <div class="some-text-that-prototype-didnt-specified">
        <h1 class="dark-title">LOREM IPSUM</h1>
        <p class="dark-body-text">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Tempore dolor velit dolorum
          beatae illum sint incidunt fugit qui explicabo, voluptatum necessitatibus soluta quam
          consequatur, assumenda sed. Atque, debitis! Fuga, eius.
        </p>
        <GeneralBtn
          btnText="MAIS EVENTOS"
          :isExternalLink="false"
          link="/eventos"
          bgColor="#48947d"
          width="150px"
          textColor="#fff"
          height="40px"
          id="more-events"
        />
      </div>
    </main>
  </section>
</template>

<script setup lang="ts">
import GeneralBtn from '../../buttons/GeneralBtn.vue';
</script>

<style scoped lang="scss">
section#organizador {
  background-color: #f6f6f6;
  min-height: 400px;
  main {
    flex-direction: row;
    align-items: center;
    justify-content: center;
    .some-text-that-prototype-didnt-specified,
    .organizador-about {
      display: flex;
      flex-direction: column;
      margin: 10px;
    }
    .organizador-about {
      .logo {
        width: 250px;
        height: 300px;
      }
    }
    .some-text-that-prototype-didnt-specified {
      max-width: 300px;
      height: 350px;
      width: 100%;
      justify-content: flex-end;
      align-items: center;
      h1 {
        margin-right: auto;
      }
      p {
        font-size: 1rem;
        text-align: justify;
        margin-bottom: 3rem;
        overflow-y: auto;
      }
      #more-events {
        margin: 0;
      }
    }
  }
}
@media (max-width: 768px) {
  section#organizador {
    header {
      h1 {
        font-size: 1.3rem;
      }
    }
  }
  .some-text-that-prototype-didnt-specified {
    h1 {
      font-size: 1.3rem;
    }
  }
}
@media (max-width: 576px) {
  section#organizador {
    main {
      flex-direction: column;
    }
  }
}
</style>
