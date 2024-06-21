<template>
  <div class="pb-5 pt-5">
    <div class="breadcrumb-container mb-4 mb-lg-3 container ps-md-5">
      <nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li class="breadcrumb-item unactive" @click="$router.push({ name: 'Documentos' })">
            Documentos
          </li>
          <li class="breadcrumb-item active" aria-current="page">Documentos criados</li>
        </ol>
      </nav>
    </div>

    <ContainerCardsDocumentosCriados />
  </div>
</template>

<script setup lang="ts">
import ContainerCardsDocumentosCriados from '../../../components/user/documentos/documentosCriados/ContainerCardsDocumentosCriados.vue';
</script>

<style scoped lang="scss"></style>
