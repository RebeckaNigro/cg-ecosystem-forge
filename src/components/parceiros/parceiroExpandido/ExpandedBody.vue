<template>
  <section class="partner-body w-100 d-flex flex-column align-items-center justify-content-center">
    <div class="container px-lg-5">
      <span>PARCEIRO DO ECOSSISTEMA</span>
      <main>
        <h1>{{ partnerName }}</h1>
        <h2 class="fs-6 cnpj" v-if="cnpj">CNPJ: {{ cnpj }}</h2>
        <article class="px-md-5 px-4">&nbsp&nbsp&nbsp{{ firstParagraph }}</article>
      </main>
      <h2>
        {{ subTitle }}
      </h2>
      <article class="second-paragraph px-md-5 px-4">&nbsp&nbsp&nbsp{{ secondParagraph }}</article>
    </div>
  </section>
</template>

<script setup lang="ts">
const props = defineProps({
  partnerName: {
    type: String,
    required: true,
    default: 'NOME DA EMPRESA',
  },
  firstParagraph: {
    type: String,
    required: true,
    default: 'body text',
  },
  subTitle: {
    type: String,
    required: false,
  },
  secondParagraph: {
    type: String,
    required: false,
  },
  cnpj: {
    type: String,
    required: false,
  },
});
</script>

<style scoped lang="scss">
section.partner-body {
  background-color: #fbfbfb;
  font-weight: 500;
  font-family: 'Inter', sans-serif;

  padding-top: 130px;

  span {
    color: gray;
    margin-top: 8rem;
    font-size: 16px;
  }
  main {
    h1 {
      font-size: 30px;
      margin-top: 1rem;
      font-weight: 600;
    }
    h2 {
      color: #505050;
      font-weight: 300;
    }
    article {
      margin-top: 3rem;
      margin-bottom: 2rem;
      text-align: justify;
      font-size: 17px;
    }
  }
  article.second-paragraph {
    padding-bottom: 3rem;
    text-align: justify;
    font-size: 17px;
  }
}
@media (max-width: 991px) {
  section.partner-body {
    span {
      font-size: 1rem;
    }
    main {
      h1 {
        font-size: 1.5rem;
        margin-top: 1rem;
      }
    }
  }
}
@media (max-width: 580px) {
  section.partner-body {
    span {
      font-size: 0.7rem;
    }
    main {
      h1 {
        margin-top: 1rem;
      }

      article {
        font-size: 15px;
      }
    }

    article.second-paragraph {
      font-size: 15px;
    }
  }
}
</style>
