<template>
  <div class="pb-5 pt-5">
    <div class="breadcrumb-container mb-4 mb-lg-3 container ps-lg-5">
      <nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li class="breadcrumb-item unactive" @click="$router.push({ name: 'Eventos' })">
            Eventos
          </li>
          <li class="breadcrumb-item active" aria-current="page">Eventos criados</li>
        </ol>
      </nav>
    </div>

    <ContainerCardsEventosCriados />
  </div>
</template>

<script setup lang="ts">
import ContainerCardsEventosCriados from '../../../components/user/eventos/eventosCriados/ContainerCardsEventosCriados.vue';
</script>

<style scoped lang="scss"></style>
