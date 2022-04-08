<template>
  <form action="" class="form-noticia boring-gray-border">
    <div class="group column-direction">
      <label for="title">Título</label>
      <input type="text" id="title" class="w-100 boring-gray-border">
    </div>
    <div class="group column-direction">
      <label for="title">"Subtítulo"</label>
      <input type="text" id="subtitle" class="w-100 boring-gray-border">
    </div>
    <div class="labels-date">Publicado em</div>
    <div class="group row-direction align-center">
      <input type="radio" id="auto-date" class="boring-gray-border">    
      <label for="auto-date">Automático</label>
    </div>  
    <div class="group row-direction align-center">
      <input type="radio" id="confirm-chosen-date" class="boring-gray-border">
      <input type="date" name="calendar" id="chosen-date" class="boring-gray-border">
    </div>
    <div class="group column-direction">
      <label for="main-text">Corpo do texto</label>
      <textarea name="main-text" id="main-text" cols="30" rows="10" class="w-100" />
    </div>
    <input type="file" name="news-cover" id="news-cover" placeholder="Capa da Notícia">
    <div class="btn-container">
      <button type="submit" class="btn-primary btn">Salvar rascunho</button>
      <button type="submit" class="btn-primary btn">Pré-visualizar</button>
      <button type="submit" class="btn-primary btn">Enviar</button>
    </div>
  </form>
</template>

<script setup lang="ts">

</script>

<style scoped lang="scss">
  form.form-noticia {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    justify-content: space-between;
    padding: 30px;
    height: 750px;
    width: 800px;
    margin: 3rem auto;

    .group {
      display: flex;
      align-items: flex-start;
      width: 100%;
    }
    .column-direction {
      flex-direction: column;
    }
    .row-direction {
      flex-direction: row;
    }
    .align-center {
      align-items: center;
    }
    .btn-container {
      margin: 15px auto;
      button {
        margin: 0 10px;
        border-radius: 25px;
      }
    }
  }
</style>