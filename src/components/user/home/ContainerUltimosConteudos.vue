<template>
  <section :id="'ultimos-conteudos' + contentType" class="p-4 ultimos-conteudos boring-gray-border d-flex">
    <header>
      <h1 class="dark-title">
        {{ containerTitle }}
      </h1>
    </header>
    <main>
      <div v-for="(content, index) in  lastContents" :key="index" class="d-flex w-100 m-3">
        <div class="title-box boring-gray-border">
          {{ content.title }}
        </div>
        <button type="button" :id="'edit-content-btn' + content.id" class="edit-btn">
          <img src="/pen-icon.svg" alt="pen_icon">
        </button>
      </div>
    </main>
    <footer>
      <button type="button" id="see-more" class="btn-primary light-title">Ver mais</button>
      <button type="button" id="create-new" class="green-btn light-title">Criar documento</button>
    </footer>
  </section>
</template>

<script setup lang="ts">

interface lastContent {
  title: string,
  path: string,
  id: number
}

const props = defineProps<{
  contentType: string
  containerTitle: string
  lastContents: Array<lastContent>
}>()
</script>

<style scoped lang="scss">
  .ultimos-conteudos {
    max-width: 500px;
    width: 100%;
    max-height: 350px;
    justify-content: center;
    flex-direction: column;
    
    header > h1 {
      font-size: 1rem;
    }
    main > .d-flex {
      justify-content: space-between;

      .title-box {
        width: 90%;
        white-space: nowrap;
        text-overflow: ellipsis;
      }
    }
    footer {
      button {
        font-size: 0.7rem;
        border-radius: 25px;
        height: 40px;
        margin: 0 5px;
      }
    }
  }
  .edit-btn {
    background-color: unset;
    border: 0;
    img {
      max-width: 25px;
    }
  }
</style>