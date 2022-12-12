<template>
  <section class="partner-body ghp">
    <span class="dark-title">PARCEIRO DO ECOSSISTEMA</span>
    <main>
      <h1 class="dark-title">{{ partnerName }}</h1>
      <article>
        {{ firstParagraph }}
      </article>
    </main>
    <h2>
      {{ subTitle }}
    </h2>
    <article class="second-paragraph">
      {{ secondParagraph }}
    </article>
  </section>
</template>

<script setup lang="ts">
const props = defineProps({
  partnerName: {
    type: String,
    required: true,
    default: 'NOME DA EMPRESA'
  },
  firstParagraph: {
    type: String,
    required: true,
    default: 'body text'
  },
  subTitle: {
    type: String,
    required: false,
  },
  secondParagraph: {
    type: String,
    required: false
  }
})
</script>

<style scoped lang="scss">
  section.partner-body {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    background-color: #f6f6f6;
	font-family: 'Montserrat-Medium', sans-serif;
    span {
      color: gray;
      margin-top: 8rem;
      font-size: 1.2rem;
    }
    main {
      h1 {
        font-size: 2rem;
        margin-top: 1rem;
      }
      article {
        margin-top: 3rem;
        margin-bottom: 2rem;
      }
    }
    article.second-paragraph {
      padding-bottom: 3rem;
    }
    h2[class="dark-title"] {
      margin-right: auto;
    }
    .dark-body-text {
      font-size: 1rem;
      text-align: justify;
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
  @media (max-width: 576px) {
    section.partner-body {
      span {
        font-size: 0.7rem;
      }
      main {
        h1 {
          font-size: 1.2rem;
          margin-top: 1rem;
        }
      }
    }
  }
</style>