<!-- este componente é um container de prósito geral segmentado por colunas que recebe título, imagem (ou não) e textos. -->
<template>
  <div id="top-border" class="circle-top-border" v-if="hasCircleTopBorder"></div>
  <section class="container-fluid general-container" :id="id">
    <h1 :class="[darkTitle ? 'dark-title' : 'light-title']" class="mb-5 fs-2">{{ title }}</h1>
    <main class="d-flex">
      <div
        v-for="(content, index) in contentArray"
        :key="index"
        :class="isUrl(content) ? 'is-img' : 'is-article'"
      >
        <img v-if="isUrl(content)" :src="content" alt="ilustrative-image" />
        <article v-else :class="[darkTitle ? 'dark-body-text' : 'light-body-text']">
          {{ content }}
          <GeneralBtn
            v-if="hasKnowMoreBtn"
            btnText="SAIBA MAIS"
            :isExternalLink="false"
            link="/quem-somos"
            bgColor="#fff"
            width="200px"
            textColor="#639280"
            height="40px"
            id="home-saiba-mais"
          />

          <GeneralBtn
            v-if="hasContactBtn"
            btnText="FALE CONOSCO"
            :isExternalLink="false"
            link="/fale-conosco"
            bgColor="#fff"
            width="200px"
            textColor="#639280"
            height="40px"
            id="home-fale-conosco"
            class="m-0 mt-4"
          />
        </article>
      </div>
    </main>
  </section>
  <div id="bottom-border" class="circle-bottom-border" v-if="hasCircleBottomBorder"></div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import GeneralBtn from '../buttons/GeneralBtn.vue';

const props = defineProps<{
  title: string;
  darkTitle: boolean;
  hasKnowMoreBtn: boolean;
  contentArray: string[];
  bgColor: string;
  id: string;
  fontSize: string;
  fontFamily: string;
  hasContactBtn: boolean;
  hasCircleTopBorder: boolean;
  hasCircleBottomBorder: boolean;
}>();

const isUrl = (text: string) => {
  const lastChars = text.length - 5;
  if (
    text.includes('.jpg', lastChars) ||
    text.includes('.png', lastChars) ||
    text.includes('.svg', lastChars) ||
    text.includes('.jpeg', lastChars)
  ) {
    return true;
  }
  return false;
};
onMounted(() => {
  const container: HTMLElement = document.querySelector('#' + props.id)!;
  container.style.background = props.bgColor;

  const article: HTMLElement = container.getElementsByTagName('article')[0];
  article.style.fontFamily = props.fontFamily;
  article.style.fontSize = props.fontSize;
});
</script>

<style scoped lang="scss">
.circle-bottom-border,
.circle-top-border {
  height: 25px;
  width: 50px;
  border-bottom-left-radius: 8rem;
  border-bottom-right-radius: 8rem;
  margin: 0 auto;
}

.circle-bottom-border {
  background: #81a799;
}

.circle-top-border {
  position: relative;
  top: 1.5rem;
  background: #fbfbfb;
}
section.general-container {
  padding-top: 40px !important;
  padding-bottom: 40px !important;

  article {
    text-align: start;
  }
}

main.d-flex > div {
  flex: 1 1 0;
  justify-content: center;

  img {
    max-width: 100%;
    border-radius: 10px;
  }
}

main.d-flex > div {
  display: flex;
  align-items: center;
}

.is-article article {
  font-size: 1rem;
  text-align: justify;
  display: flex;
  flex-direction: column;
  margin: 0 15px;
  max-height: inherit;
  overflow-y: auto;
  padding-right: 10px;
}

@media (max-width: 767px) {
  section.general-container {
    padding-top: 20px !important;
    padding-bottom: 20px !important;

    h1 {
      font-size: 1.2rem;
    }
  }
}

@media (max-width: 576px) {
  section.general-container {
    height: 400px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;

    h1 {
      font-size: 1rem;
    }

    article {
      text-align: center;
      height: 400px;
      font-size: 1rem;
    }

    #home-saiba-mais {
      margin-top: 2rem;
    }
  }

  main.d-flex {
    flex-direction: column;

    div.is-article {
      margin-bottom: 10px;
    }
  }

  main.d-flex > div.is-img {
    display: none;
  }

  section > h1 {
    margin-bottom: 1.5rem !important;
  }
}
</style>
