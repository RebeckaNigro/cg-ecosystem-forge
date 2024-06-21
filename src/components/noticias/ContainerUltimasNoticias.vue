<template>
  <div class="mt-5 container">
    <h1 class="dark-title">ÚLTIMAS NOTÍCIAS</h1>

    <div v-if="!loadingNews" class="row my-4 mx-auto">
      <div
        class="col mb-4"
        v-for="(container, containerIndex) in noticiaStore.lastNews"
        :key="containerIndex"
      >
        <CardNoticia
          :is-relacionada="false"
          :noticia="container"
          @click="$router.push({ name: 'NoticiaExpandida', params: { noticiaId: container.id } })"
        />
      </div>
    </div>
    <Spinner v-else :spinner-color-class="'text-dark'" />
    <hr class="divider" />
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useNoticiaStore } from '../../stores/noticias/store';
import CardNoticia from './CardNoticia.vue';
import Spinner from '../../components/general/Spinner.vue';

const noticiaStore = useNoticiaStore();
const loadingNews = ref(true);

onMounted(() => {
  noticiaStore.getLastNews();
  loadingNews.value = false;
});
</script>

<style scoped lang="scss">
h1 {
  font-weight: 600;
  font-size: 2rem;
  margin: 2rem 0;
}

.divider {
  height: 1px;
  background-color: #6b6a64;
}

@media screen and (max-width: 1200px) {
  h1 {
    font-size: 1.7rem;
  }
}
</style>
