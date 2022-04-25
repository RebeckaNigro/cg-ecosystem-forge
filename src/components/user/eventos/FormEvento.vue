<template>
  <form class="boring-gray-border dark-body-text" id="form-evento">
    <label for="tipos-evento">Tipo de evento</label>
    <div class="tipo-evento mt-3" id="tipos-evento">
      <select name="tipo-evento" id="tipo-evento" class="boring-gray-border" v-model="evento.tipoEventoId"
        @change.prevent="buscarEnderecosPeloTipo()">
        <option :value="1">Presencial</option>
        <option :value="2">Online</option>
        <option :value="3">Palestra</option>
        <option :value="4">Workshop</option>
      </select>
      <!-- <input type="radio" name="tipo-evento" id="tipo-presencial" :value="1" v-model="evento.tipoEventoId">
      <label for="tipo-presencial">Presencial</label>
      <input type="radio" name="tipo-evento" id="tipo-online" :value="2" v-model="evento.tipoEventoId">
      <label for="tipo-online">Online</label> -->
    </div>
    <div class="nome-evento">
      <label for="nome-evento">Nome do evento *</label>
      <input type="text" class="form-control boring-gray-border w-100" id="nome-evento" v-model="evento.titulo">
    </div>
    <div class="imagem-divulgacao">
      <label for="imagem-input-mask">Imagem de divulgação (opicional)</label>
      <label for="imagem-input" />
      <input type="file" name="imagem-input" id="imagem-input">
    </div>
    <div class="datas-inicio-fim d-flex">
      <div class="inicio mt-0">
        <label for="data-inicio">Data e hora de início</label>
        <input type="datetime-local" name="data-inicio" id="data-inicio" class="boring-gray-border"
          v-model="evento.dataInicio">
      </div>
      <div class="fim mt-0">
        <label for="data-termino">Data e hora de término</label>
        <input type="datetime-local" name="data-termino" id="data-termino" class="boring-gray-border"
          v-model="evento.dataTermino">
      </div>
    </div>
    <div class="descricao-evento">
      <label for="descricao">Descrição do evento</label>
      <textarea name="descricao" id="descricao" cols="30" rows="10" class="boring-gray-border"
        v-model="evento.descricao" />
    </div>
    <div class="local-evento">
      <label for="local-evento">Local do evento *</label>
      <select name="local" id="local-evento" class="boring-gray-border" v-model="evento.enderecoId"
        @change="setAddressInputFields(evento.enderecoId)">
        <option selected disabled value="0">Selecione ou cadastre um novo endereço</option>
        <option :value="null">Cadastrar novo endereço</option>
        <option :value="address.enderecoId" v-for="(address, index) in existingAddresses" :key="address.enderecoId">{{
          `${address.logradouro} ${address.numero}`
        }}</option>
      </select>
    </div>
    <div id="novo-endereco" class="novo-endereco">
      <div class="informa-endereco-ou-local">
        <label for="informa-endereco-ou-local">Informe o endereço ou o nome do local do evento *</label>
        <input type="text" name="informa-endereco-ou-local" id="informa-endereco-ou-local" class="boring-gray-border"
          :disabled="evento.enderecoId !== null" v-model="evento.local">
      </div>
      <div class="cep">
        <label for="cep">CEP *</label>
        <!-- <input type="text" name="cep" id="cep" class="boring-gray-border" :disabled="evento.enderecoId !== null" v-model="evento.endereco.cep" maxlength="7"> -->
        <input type="text" name="cep" id="cep" class="boring-gray-border" :disabled="evento.enderecoId !== null"
          v-model="novoEndereco.cep" maxlength="8" @input.prevent="handleCEPInput()">
      </div>
      <div class="rua">
        <label for="rua">Rua *</label>
        <input type="text" name="rua" id="rua" class="boring-gray-border" :disabled="evento.enderecoId !== null"
          v-model="novoEndereco.logradouro">
      </div>
      <div class="numero">
        <label for="numero">Número *</label>
        <input type="text" name="numero" id="numero" class="boring-gray-border" :disabled="evento.enderecoId !== null"
          v-model="novoEndereco.numero">
      </div>
      <div class="complemento">
        <label for="complemento">Complemento</label>
        <input type="text" name="complemento" id="complemento" class="boring-gray-border"
          :disabled="evento.enderecoId !== null" v-model="novoEndereco.pontoReferencia">
      </div>
      <div class="ponto-referencia">
        <label for="ponto-referencia">Ponto de referência</label>
        <input type="text" name="ponto-referencia" id="ponto-referencia" class="boring-gray-border"
          :disabled="evento.enderecoId !== null" v-model="novoEndereco.complemento">
      </div>
      <div class="cidade-estado d-flex justify-content-between">
        <div id="cidade">
          <label for="cidade-input">Cidade *</label>
          <input type="text" name="cidade-input" id="cidade-input" class="boring-gray-border"
            :disabled="evento.enderecoId !== null" v-model="novoEndereco.cidade">
        </div>
        <div id="estado">
          <label for="estado-input">Estado *</label>
          <input type="text" name="estado-input" id="estado-input" class="boring-gray-border"
            :disabled="evento.enderecoId !== null" v-model="novoEndereco.uf">
        </div>
      </div>
    </div>
    <div class="exibir-maps d-flex">
      <input type="checkbox" name="exibir-maps" id="exibir-maps" v-model="evento.exibirMaps">
      <label for="exibir-maps">Exibir endereço no Google Maps</label>
    </div>
    <div class="link-ingressos">
      <label for="ingressos">Link para obtenção de ingressos</label>
      <input type="text" name="ingressos" id="ingressos" class="boring-gray-border" v-model="evento.linkExterno">
    </div>
    <div class="produtor">
      <label for="produtor">Nome do produtor *</label>
      <input type="text" name="produtor" id="produtor" class="boring-gray-border" v-model="evento.responsavel">
    </div>
    <div class="outra-descricao">
      <label for="outra-descricao">Descrição</label>
      <textarea name="outra-descricao" id="outra-descricao" cols="30" rows="10" />
    </div>
    <div class="btn-group d-flex justify-content-around">
      <button class="btn-btn-primary">Salvar rascunho</button>
      <button class="btn-btn-primary">Pré-visualizar</button>
      <button class="btn-btn-primary" @click.prevent="handleAction('publicar')">Publicar evento</button>
    </div>
  </form>
</template>
<!-- TODO: preencher outros campos quando usuário escolher endereço existente -->
<script setup lang="ts">
import { onMounted, reactive, computed } from 'vue';
import { IEvento, INovoEndereco } from '../../../stores/eventos/types';
import { useEventoStore } from '../../../stores/eventos/store'
import { getFromCEP } from '../../../utils/http'

const useStore = useEventoStore()

const novoEndereco: INovoEndereco = reactive({
  cep: '',
  logradouro: '',
  numero: '',
  complemento: '',
  pontoReferencia: '',
  bairro: '',
  cidade: '',
  uf: ''
})
const evento: IEvento = reactive({
  id: 1,
  instituicaoId: 1, // id do parceiro que o usuário logado representa
  tipoEventoId: 1,
  titulo: '',
  descricao: '',
  dataInicio: '',
  dataTermino: '',
  local: '',
  enderecoId: 1,
  endereco: novoEndereco,
  linkExterno: '',
  exibirMaps: false,
  responsavel: ''
})
const buscarEnderecosPeloTipo = async () => {
  useStore.novoEvento.tipoEventoId = evento.tipoEventoId
  await useStore.getAddresses(useStore.novoEvento.tipoEventoId.toString())
}
const handleAction = (action: string) => {
  if (evento.enderecoId) evento.endereco = null
  else evento.endereco = { ...novoEndereco }

  if (action === 'publicar') {
    // TODO: validar inputs
    useStore.$patch((state) => {
      state.novoEvento = { ...evento }
      state.novoEvento.dataInicio = new Date(state.novoEvento.dataInicio + ':00.000Z').toISOString()
      state.novoEvento.dataTermino = new Date(state.novoEvento.dataTermino + ':00.000Z').toISOString()
    })

    async function publicarEvento() {
      const file: HTMLInputElement = document.querySelector('#imagem-input')!
      if (file) await useStore.putEvent(evento, file)
      else await useStore.putEvent(evento)
    }

    publicarEvento()
  }
}
const handleCEPInput = async () => {
  // validate cep input
  if (novoEndereco.cep.length === 8) {
    if (evento.endereco) {
      const data = await getFromCEP(evento.endereco.cep)
      novoEndereco.cidade = data.city
      novoEndereco.uf = data.state
      novoEndereco.bairro = data.neighborhood
      novoEndereco.logradouro = data.street
    }
  }
}
onMounted(async () => {
  await useStore.getAddresses('1')
})
const setAddressInputFields = (enderecoId: number) => {
  if (!enderecoId) {
    novoEndereco.cep = ''
    novoEndereco.logradouro = ''
    novoEndereco.cidade = ''
    novoEndereco.uf = ''
    novoEndereco.numero = ''
    novoEndereco.complemento = ''
    novoEndereco.pontoReferencia = ''
  } else {
    for (const address of existingAddresses.value) {
      if (address.enderecoId === enderecoId) {
        novoEndereco.cep = address.cep
        novoEndereco.logradouro = address.logradouro
        novoEndereco.cidade = address.cidade
        novoEndereco.uf = address.uf
        novoEndereco.numero = address.numero
        novoEndereco.complemento = address.complemento
        novoEndereco.pontoReferencia = address.pontoReferencia
      }
    }
  }
}
const existingAddresses = computed(() => {
  return useStore.enderecosExistentes
})
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

  select[name="tipo-evento"] {
    width: 45%;
  }

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

  .datas-inicio-fim,
  .cidade-estado {
    justify-content: space-between;

    .inicio,
    .fim,
    #cidade,
    #estado {
      width: 45%;
    }
  }

  .exibir-maps {
    align-items: center;

    input {
      margin-right: 10px;
      transform: scale(2);
    }
  }
}

@media (max-width: 768px) {
  .cidade-estado {
    flex-direction: column;

    #cidade,
    #estado {
      width: 100% !important;
    }
  }
}

@media (max-width: 576px) {

  select[name="tipo-evento"] {
    width: 100% !important;
  }

  .datas-inicio-fim {
    flex-direction: column;

    .fim,
    .inicio {
      width: 100% !important;
    }

    .fim {
      margin-top: 2rem !important;
    }
  }
}

@keyframes dropNovoEndereco {
  from {
    height: 0;
  }

  to {
    height: 715px;
  }
}

@keyframes rollupNovoEndereco {
  from {
    height: 715px;
  }

  to {
    height: 0px;
  }
}
</style>