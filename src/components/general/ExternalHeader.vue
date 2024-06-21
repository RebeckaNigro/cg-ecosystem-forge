<template>
  <div class="header-content d-flex h-100 position-absolute top-0">
    <h1 class="dark-title text-uppercase">{{ title }}</h1>
    <h2 class="subtitle">{{ subtitle }}</h2>
    <p>{{ paragraph }}</p>
  </div>
</template>

<script setup lang="ts">
const props = defineProps({
  title: String,
  subtitle: {
    type: String,
    default: '',
  },
  paragraph: String,
});
</script>

<style scoped lang="scss">
.header-content {
  justify-content: center;
  flex-direction: column;
  margin-left: 7rem;
  line-height: 40px;

  h1 {
    text-align: start;
    font-size: 2rem;
    margin-bottom: 0;
  }

  .subtitle {
    color: #000;
    text-align: start;
    font-size: 1.8rem;
    font-weight: 600;
    margin-top: 5px;
  }

  p {
    font-size: 1.5rem;
    font-weight: 500;
    max-width: 550px;
    width: 100%;
    text-align: start;
    margin-bottom: 0;
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 4;
    /* number of lines to show */
    line-clamp: 4;
    -webkit-box-orient: vertical;
    color: #000;
  }
}

/* NOTEBOOKS E AFINS */
@media screen and (min-width: 2550px) {
  .header-content {
    h1 {
      font-size: 2.5rem;
    }

    .subtitle {
      font-size: 2.3rem;
    }

    p {
      font-size: 2rem;
      text-align: start;
      max-width: 700px;
    }
  }
}

/* TABLETS */
@media screen and (max-width: 1200px) {
  .header-content {
    margin-left: 1.5rem !important;
    padding-top: 1rem;

    h1 {
      font-size: 1.5rem;
    }

    .subtitle {
      font-size: 1.3rem;
    }

    p {
      font-size: 1.1rem;
      text-align: start;
    }
  }
}

/* CELULARES */
@media (max-width: 580px) {
  .header-content {
    margin-left: 1rem !important;
    padding-top: 1rem;

    h1 {
      font-size: 1.2rem;
    }

    .subtitle {
      font-size: 1rem;
    }

    p {
      font-size: 1.1rem;
      text-align: start;
      line-height: 32px;
    }
  }
}
</style>
