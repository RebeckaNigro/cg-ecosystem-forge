<template>
  <div class="pb-5 pt-5">
    <div class="breadcrumb-container">
      <nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li
            class="breadcrumb-item unactive"
            @click="$router.push({ name: 'NoticiasCriadas' })"
          >
            Notícias
          </li>
          <li class="breadcrumb-item active" aria-current="page">
            Notícias criadas
          </li>
        </ol>
      </nav>
    </div>

    <ContainerCardsNoticiasCriadas />
  </div>
</template>

<script setup lang="ts">
  import ContainerCardsNoticiasCriadas from "../../../components/user/noticias/noticiasCriadas/ContainerCardsNoticiasCriadas.vue"
</script>

<style scoped lang="scss"></style>
