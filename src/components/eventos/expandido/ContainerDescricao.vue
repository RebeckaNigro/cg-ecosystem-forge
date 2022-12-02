<template>
  <section id="descricao-evento" class="ghp">
    <header class="mt-5">
      <h1 class="dark-title">DESCRIÇÃO DO EVENTO</h1>
    </header>
    <main>
      <article class="dark-body-text">
        {{descricaoEvento}}
      </article>
    </main>
    <footer class="mb-5">
      <GeneralBtn
        btnText="ACESSAR LINK DO EVENTO"
        :isExternalLink="linkEvento !== null"
        :link="linkEvento!"
        bgColor="#48947d"
        width="300px"
        textColor="#fff"
        height="40px"
        id="link-evento"
      />
    </footer>
  </section>

</template>

<script setup lang="ts">
import GeneralBtn from '../../buttons/GeneralBtn.vue';

const props = defineProps<{
	descricaoEvento: string,
	linkEvento: string | null
}>()
</script>

<style scoped lang="scss">
  section#descricao-evento {
    min-height: 400px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-between;
    main > article {
      font-size: 1rem;
      text-align: justify;
    }
  }
  @media (max-width: 768px) {
    section#descricao-evento {
      header > h1 {
        font-size: 1.3rem;
      }
    }
  }
  @media (max-width: 576px) {
    section#descricao-evento {
      header > h1 {
        font-size: 1rem;
      }
      main > article {
        font-size: 0.8rem;
      }
    }
  }
</style>