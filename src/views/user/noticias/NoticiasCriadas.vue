<template>
  <div class="pb-5 pt-5">
    <div class="breadcrumb-container mb-4 mb-lg-3 container ps-md-5">
      <nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li class="breadcrumb-item unactive" @click="$router.push({ name: 'Noticias' })">
            Notícias
          </li>
          <li class="breadcrumb-item active" aria-current="page">Notícias criadas</li>
        </ol>
      </nav>
    </div>

    <ContainerCardsNoticiasCriadas />
  </div>
</template>

<script setup lang="ts">
import ContainerCardsNoticiasCriadas from '../../../components/user/noticias/noticiasCriadas/ContainerCardsNoticiasCriadas.vue';
</script>

<style scoped lang="scss"></style>
