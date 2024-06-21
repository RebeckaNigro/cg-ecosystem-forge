<template>
  <div class="pb-5 pt-5">
    <div class="breadcrumb-container">
      <nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li class="breadcrumb-item unactive" @click="$router.push({ name: 'NoticiasCriadas' })">
            Notícias
          </li>
          <li v-if="!noticiaId" class="breadcrumb-item active" aria-current="page">
            Criar Notícia
          </li>
          <li v-else class="breadcrumb-item active" aria-current="page">Editar Notícia</li>
        </ol>
      </nav>
    </div>

    <div class="pb-5">
      <div v-if="noticiaId">
        <FormNoticia :noticia-id="noticiaId" />
      </div>
      <div v-else>
        <FormNoticia />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import FormNoticia from '../../../components/user/noticias/FormNoticia.vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const noticiaId = ref<number | null>(null);

onMounted(() => {
  const id = route.params.noticiaId;

  if (id) {
    noticiaId.value = Number(id);
  }
});
</script>

<style scoped></style>
