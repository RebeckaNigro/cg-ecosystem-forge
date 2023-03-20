<template>
  <div class="input-icon-container overflow-hidden">
    <i class="icon overflow-hidden">
      <img
        src="../../../public/search_icon.svg"
        class="input-icon-image"
        @click=""
      />
    </i>
    <input
      type="text"
      name="pesquisar"
      id="pesquisar"
      placeholder="Pesquisar"
      class="form-input-primary search-input px-3"
    />
  </div>
</template>

<script setup lang="ts"></script>

<style scoped>
  .search-input {
    border-radius: 50px;
    border-color: lightgray;
  }
</style>
