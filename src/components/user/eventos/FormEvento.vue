<template>
  <form class="boring-gray-border dark-body-text" id="form-evento">
    <label for="tipos-evento">Tipo de evento</label>
    <div class="tipo-evento mt-3" id="tipos-evento">
      <input type="radio" name="tipo-evento" id="tipo-presencial" :value="true">
      <label for="tipo-presencial">Presencial</label>
      <input type="radio" name="tipo-evento" id="tipo-online" :value="false">
      <label for="tipo-online">Online</label>
    </div>
    <div class="nome-evento">
      <label for="nome-evento">Nome do evento *</label>
      <input type="text" class="form-control boring-gray-border w-100" id="nome-evento">
    </div>
    <div class="imagem-divulgacao">
      <label for="imagem-input-mask">Imagem de divulgação (opicional)</label>
      <label for="imagem-input" />
      <input type="file" name="imagem-input" id="imagem-input">
    </div>
    <div class="datas-inicio-fim d-flex">
      <div class="inicio mt-0">
        <label for="data-inicio">Data e hora de início</label>
        <input type="datetime-local" name="data-inicio" id="data-inicio" class="boring-gray-border">
      </div>
      <div class="fim mt-0">
        <label for="data-termino">Data e hora de término</label>
        <input type="datetime-local" name="data-termino" id="data-termino" class="boring-gray-border">
      </div>
    </div>
    <div class="descricao-evento">
      <label for="descricao">Descrição do evento</label>
      <textarea name="descricao" id="descricao" cols="30" rows="10" class="boring-gray-border"></textarea>
    </div>
    <div class="local-evento">
      <label for="local-evento">Local do evento *</label>
      <select name="local" id="local-evento" class="boring-gray-border">
        <option value="null" disabled checked>Ainda não tem</option>
      </select>
    </div>
    <div class="informa-endereco-ou-local">
      <label for="informa-endereco-ou-local">Informe o endereço ou o nome do local do evento *</label>
      <input type="text" name="informa-endereco-ou-local" id="informa-endereco-ou-local" class="boring-gray-border">
    </div>
    <div class="nome-local">
      <label for="nome-local">Nom do local *</label>
      <input type="text" name="nome-local" id="nome-local" class="boring-gray-border">
    </div>
    <div class="cep">
      <label for="cep">CEP *</label>
      <input type="number" name="cep" id="cep" maxlength="7" class="boring-gray-border">
    </div>
    <div class="rua">
      <label for="rua">Rua *</label>
      <input type="text" name="rua" id="rua" class="boring-gray-border">
    </div>
    <div class="numero">
      <label for="numero">Número *</label>
      <input type="text" name="numero" id="numero" class="boring-gray-border">
    </div>
    <div class="complemento">
      <label for="complemento">Complemento</label>
      <input type="text" name="complemento" id="complemento" class="boring-gray-border">
    </div>
    <div class="cidade-estado d-flex justify-content-between">
      <div id="cidade">
        <label for="cidade-input">Cidade *</label>
        <input type="text" name="cidade-input" id="cidade-input" class="boring-gray-border">
      </div>
      <div id="estado">
        <label for="estado-input">Estado *</label>
        <input type="text" name="estado-input" id="estado-input" class="boring-gray-border">
      </div>
    </div>
    <div class="link-ingressos">
      <label for="ingressos">Link para obtenção de ingressos</label>
      <input type="text" name="ingressos" id="ingressos" class="boring-gray-border">
    </div>
    <div class="produtor">
      <label for="produtor">Nome do produtor *</label>
      <input type="text" name="produtor" id="produtor" class="boring-gray-border">
    </div>
    <div class="outra-descricao">
      <label for="outra-descricao">Descrição</label>
      <textarea name="outra-descricao" id="outra-descricao" cols="30" rows="10"></textarea>
    </div>
    <div class="btn-group d-flex justify-content-around">
      <button class="btn-btn-primary">Salvar rascunho</button>
      <button class="btn-btn-primary">Pré-visualizar</button>
      <button class="btn-btn-primary">Publicar evento</button>
    </div>
  </form>
</template>

<script setup lang="ts">

</script>

<style scoped lang="scss">
  .dark-body-text {
    font-size: 1.2rem;
  }
  #form-evento {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    max-width: 1000px;
    margin: auto;
    padding: 2rem;
    
    div {
      width: 100%;
      text-align: start;
      margin-top: 2rem;
      display: grid;

      label[for="imagem-input"] {
        height: 200px;
        width: 100%;
        max-width: 400px;
        background-color: #e6e6e6;
        background-image: url('/user/eventos/cloud_icon.svg');
        background-repeat: no-repeat;
        background-position: center;
      } 

      label[for="imagem-input"]:hover {
        cursor: pointer;
      }
    
      input#imagem-input {
        height: 0;
      }
    }

    .tipo-evento {
      display: flex;
      align-items: center;
      label {
        margin-right: 25px;
      }
      input {
        margin-right: 5px;
      }
    }
    .datas-inicio-fim, .cidade-estado {
      justify-content: space-between;
      .inicio, .fim, #cidade, #estado {
        width: 45%;
      }
    }
  }
</style>