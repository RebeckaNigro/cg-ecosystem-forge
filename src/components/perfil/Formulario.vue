<template>
  <div class="text-center mt-3 mb-2">
    <h1 class="titulo-principal">FAÇA PARTE DO ECOSSISTEMA!</h1>
  </div>
  <div class="mx-auto card-form box p-5 mb-5">
    <form action="submit">
      <div>
        <label for="nome-completo" class="form-label-primary"
          >Nome Completo</label
        >
        <input
          type="text"
          id="nome-completo"
          class="form-input-primary"
          v-model="perfil.nomeCompleto"
        />
      </div>

      <div class="row">
        <div class="col-sm-8">
          <label for="cpf" class="form-label-primary">CPF*</label>
          <input
            type="text"
            id="cpf"
            class="form-input-primary"
            v-model="perfil.cpf"
          />
        </div>
        <div class="col-sm-4">
          <label for="data-nascimento" class="form-label-primary"
            >Data de Nascimento*</label
          >
          <input
            type="date"
            id="data-nascimento"
            class="form-input-primary"
            v-model="perfil.dataNascimento"
          />
        </div>
      </div>

      <div class="row">
        <div class="col-sm-8">
          <label for="email-corporativo" class="form-label-primary"
            >E-mail</label
          >
          <input
            type="email"
            id="email-corporativo"
            class="form-input-primary"
            v-model="perfil.email"
          />
        </div>
        <div class="col-sm-4">
          <label for="telefone" class="form-label-primary">Telefone</label>
          <input
            type="text"
            id="telefone"
            class="form-input-primary"
            v-model="perfil.telefone"
          />
        </div>
      </div>

      <div>
        <label for="cargo" class="form-label-primary">Cargo*</label>
        <input
          type="text"
          id="cargo"
          class="form-input-primary"
          v-model="perfil.cargo"
        />
      </div>

      <div>
        <label for="instituicao" class="form-label-primary">Instituição</label>
        <select
          id="instituicao"
          class="form-input-primary padding-increase"
          v-model="perfil.instituicao"
        ></select>
      </div>

      <div>
        <label for="cpj-insituicao" class="form-label-primary"
          >CNPJ da Instituição</label
        >
        <input
          type="text"
          id="cpj-insituicao"
          class="form-input-primary"
          v-model="perfil.cnpjInsituicao"
        />
      </div>

      <div>
        <label for="cep" class="form-label-primary">CEP</label>
        <input
          type="text"
          id="cep"
          class="form-input-primary"
          v-model="perfil.cep"
          @blur="buscarCEP"
        />
      </div>

      <div class="row">
        <div class="col-sm-4">
          <label for="estado" class="form-label-primary">Estado</label>
          <input
            id="estado"
            class="form-input-primary padding-increase"
            v-model="perfil.uf"
            disabled
          />
        </div>
        <div class="col-sm-8">
          <label for="cidade" class="form-label-primary">Cidade</label>
          <input
            type="text"
            id="cidade"
            class="form-input-primary"
            v-model="perfil.cidade"
            disabled
          />
        </div>
      </div>

      <div class="row">
        <div class="col-sm-9">
          <label for="logradouro" class="form-label-primary">Logradouro</label>
          <input
            type="text"
            id="logradouro"
            class="form-input-primary"
            v-model="perfil.logradouro"
            disabled
          />
        </div>
        <div class="col-sm-3">
          <label for="numero" class="form-label-primary">Número</label>
          <input
            type="text"
            id="numero"
            class="form-input-primary"
            v-model="perfil.numero"
          />
        </div>
      </div>

      <div>
        <label for="bairro" class="form-label-primary">Bairro</label>
        <input
          type="text"
          id="bairro"
          class="form-input-primary"
          v-model="perfil.bairro"
          disabled
        />
      </div>

      <div class="input-icon-container">
        <label for="password" class="form-label-primary">Senha</label>

        <i class="icon">
          <img
            :src="visibilityIconRef"
            class="input-icon-image"
            @click="changePasswordVisibility"
          />
        </i>
        <input
          :type="passwordVisibility ? 'text' : 'password'"
          id="password"
          class="form-input-primary"
          placeholder="*****"
          v-model="perfil.password"
          required
        />
      </div>

      <div class="input-icon-container">
        <label for="confirmPassword" class="form-label-primary"
          >Confirmar Senha</label
        >

        <i class="icon">
          <img
            :src="confirmVisibilityIconRef"
            class="input-icon-image"
            @click="changeConfirmPasswordVisibility"
          />
        </i>
        <input
          :type="confirmPasswordVisibility ? 'text' : 'password'"
          id="confirmPassword"
          class="form-input-primary"
          placeholder="*****"
          v-model="perfil.confirmPassword"
          required
        />
      </div>

      <div class="text-start">
        <input
          type="checkbox"
          id="concorda"
          class="mx-2"
          v-model="termosDeUso"
        />
        <span>Aceito os termos de uso...</span>
      </div>

      <div class="mt-4 d-flex">
        <button type="button" class="green-btn-outlined button-specific">
          VOLTAR
        </button>
        <button
          type="button"
          class="green-btn-primary button-specific"
          @click="cadastrar"
        >
          CADASTRAR
        </button>
      </div>
    </form>
  </div>
</template>

<script setup lang="ts">
  import { onMounted, ref } from "vue"
  import { getFromCEP } from "../../utils/http"
  import { usePerfilStore } from "../../stores/perfil/store"
  import { useAlertStore } from "../../stores/alert/store"

  const perfilStore = usePerfilStore()
  const alertStore = useAlertStore()

  const passwordVisibility = ref(false)
  const confirmPasswordVisibility = ref(false)
  const visibilityIconRef = ref("src/assets/icons/visibility-off.svg")
  const confirmVisibilityIconRef = ref("src/assets/icons/visibility-off.svg")
  let termosDeUso = ref(false)
  let perfil = ref({
    nomeCompleto: "",
    cpf: "",
    dataNascimento: "",
    email: "",
    telefone: "",
    cargo: "",
    uf: "",
    cidade: "",
    logradouro: "",
    bairro: "",
    numero: "",
    cep: "",
    instituicao: "",
    cnpjInstituicao: "",
    password: "",
    confirmPassword: ""
  })

  const changePasswordVisibility = () => {
    passwordVisibility.value = !passwordVisibility.value

    visibilityIconRef.value = passwordVisibility.value
      ? "src/assets/icons/visibility-on.svg"
      : "src/assets/icons/visibility-off.svg"
  }

  const changeConfirmPasswordVisibility = () => {
    confirmPasswordVisibility.value = !confirmPasswordVisibility.value

    confirmVisibilityIconRef.value = confirmPasswordVisibility.value
      ? "src/assets/icons/visibility-on.svg"
      : "src/assets/icons/visibility-off.svg"
  }
  const instituicoes = ref({})

  const getInstituicoes = async () => {}

  const cadastrar = async () => {
    const resposta = await perfilStore.postPerfil(perfil.value)

    if (resposta.status === 200) {
      alertStore.visible = true
      alertStore.options = {
        message: "Perfil cadastrado com sucesso!",
        type: "success",
        dispensable: true,
        timeout: true
      }
    } else {
      alertStore.visible = true
      alertStore.options = {
        message: "Erro ao cadastrar perfil!",
        type: "error",
        dispensable: true,
        timeout: true
      }
    }
  }

  const buscarCEP = async () => {
    if (perfil.value.cep) {
      try {
        const enderecoCompleto = await getFromCEP(perfil.value.cep)

        if (enderecoCompleto.errors) {
          alertStore.visible = true
          alertStore.options = {
            message: "Erro ao buscar CEP!",
            type: "error",
            dispensable: true,
            timeout: true
          }
        } else {
          perfil.value.cidade = enderecoCompleto.city
          perfil.value.uf = enderecoCompleto.state
          perfil.value.bairro = enderecoCompleto.neighborhood
          perfil.value.logradouro = enderecoCompleto.street
        }
      } catch (error) {
        console.log(error)

        alertStore.visible = true
        alertStore.options = {
          message: "CEP Incorreto, digite um valor válido.",
          type: "error",
          dispensable: true,
          timeout: true
        }
      }
    }
  }

  onMounted(async () => {
    getInstituicoes()
  })
</script>

<style scoped>
  .card-form {
    width: 70%;
    margin: auto;
  }

  .titulo-principal {
    font-family: Montserrat-Bold;
    font-size: 1.5rem;
    color: black;
    margin: 5rem 0 1rem 0;
  }

  .button-specific {
    width: 50%;
    margin: 2rem 1rem;
    box-sizing: border-box;
  }
</style>
