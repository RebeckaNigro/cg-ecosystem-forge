<template>
  <div class="text-center mt-3 mb-2">
    <h1 class="titulo-principal">FAÇA PARTE DO ECOSSISTEMA!</h1>
  </div>
  <div class="mx-auto card-position box p-5 mb-5">
    <form @submit.prevent="cadastrar">
      <div class="mb-3">
        <label for="nome-completo" class="form-label-primary"
          >Nome Completo*</label
        >
        <input
          type="text"
          id="nome-completo"
          class="form-input-primary"
          :class="v$.nomeCompleto.$error ? 'is-invalid' : ''"
          v-model="perfil.nomeCompleto"
        />
        <div v-if="v$.nomeCompleto.$error" class="invalid-feedback">
          {{ v$.nomeCompleto.$errors[0].$message }}
        </div>
      </div>

      <div class="row mb-3">
        <div class="col-sm-8">
          <label for="cpf" class="form-label-primary">CPF*</label>
          <input
            type="text"
            id="cpf"
            class="form-input-primary"
            :class="v$.cpf.$error ? 'is-invalid' : ''"
            v-model="perfil.cpf"
          />
          <div v-if="v$.cpf.$error" class="invalid-feedback">
            {{ v$.cpf.$errors[0].$message }}
          </div>
        </div>
        <div class="col-sm-4">
          <label for="data-nascimento" class="form-label-primary"
            >Data de Nascimento*</label
          >
          <input
            type="date"
            id="data-nascimento"
            class="form-input-primary"
            :class="v$.dataNascimento.$error ? 'is-invalid' : ''"
            v-model="perfil.dataNascimento"
          />
          <div v-if="v$.dataNascimento.$error" class="invalid-feedback">
            {{ v$.dataNascimento.$errors[0].$message }}
          </div>
        </div>
      </div>

      <div class="row mb-3">
        <div class="col-sm-8">
          <label for="email-corporativo" class="form-label-primary"
            >E-mail*</label
          >
          <input
            type="email"
            id="email-corporativo"
            class="form-input-primary"
            :class="v$.email.$error ? 'is-invalid' : ''"
            v-model="perfil.email"
          />
          <div v-if="v$.email.$error" class="invalid-feedback">
            {{ v$.email.$errors[0].$message }}
          </div>
        </div>
        <div class="col-sm-4">
          <label for="telefone" class="form-label-primary">Telefone*</label>
          <input
            type="text"
            id="telefone"
            class="form-input-primary"
            v-model="perfil.telefone"
			:class="v$.telefone.$error ? 'is-invalid' : ''"
          />
		  <div v-if="v$.telefone.$error" class="invalid-feedback">
			{{ v$.telefone.$errors[0].$message }}
		  </div>
        </div>
      </div>

      <div class="mb-3">
        <label for="cargo" class="form-label-primary">Cargo*</label>
        <input
          type="text"
          id="cargo"
          class="form-input-primary"
          :class="v$.cargo.$error ? 'is-invalid' : ''"
          v-model="perfil.cargo"
        />
        <div v-if="v$.cargo.$error" class="invalid-feedback">
          {{ v$.cargo.$errors[0].$message }}
        </div>
      </div>

      <div class="mb-3">
        <div class="text-start">
          <label for="cep" class="form-label-primary d-inline"
            >CEP*
            <a
              href="https://buscacepinter.correios.com.br/app/endereco/index.php"
              >(Não sabe o CEP?)</a
            ></label
          >
        </div>

        <input
          type="text"
          id="cep"
          class="form-input-primary"
          :class="v$.cep.$error ? 'is-invalid' : ''"
          v-model="perfil.cep"
          @blur="buscarCEP"
        />

        <div v-if="v$.cep.$error" class="invalid-feedback">
          {{ v$.cep.$errors[0].$message }}
        </div>
      </div>

      <div class="row mb-3">
        <div class="col-sm-4">
          <label for="estado" class="form-label-primary">Estado</label>
          <input id="estado" class="form-input-primary" v-model="perfil.uf" />
        </div>
        <div class="col-sm-8">
          <label for="cidade" class="form-label-primary">Cidade</label>
          <input
            type="text"
            id="cidade"
            class="form-input-primary"
            v-model="perfil.cidade"
          />
        </div>
      </div>

      <div class="row mb-3">
        <div class="col-sm-9">
          <label for="logradouro" class="form-label-primary">Logradouro</label>
          <input
            type="text"
            id="logradouro"
            class="form-input-primary"
            v-model="perfil.logradouro"
          />
        </div>
        <div class="col-sm-3">
          <label for="numero" class="form-label-primary">Número*</label>
          <input
            type="text"
            id="numero"
            class="form-input-primary"
            :class="v$.numero.$error ? 'is-invalid' : ''"
            v-model="perfil.numero"
          />
          <div v-if="v$.numero.$error" class="invalid-feedback">
            {{ v$.numero.$errors[0].$message }}
          </div>
        </div>
      </div>

      <div class="mb-3">
        <label for="bairro" class="form-label-primary">Bairro</label>
        <input
          type="text"
          id="bairro"
          class="form-input-primary"
          v-model="perfil.bairro"
        />
      </div>

      <div class="input-icon-container mb-3">
        <label for="password" class="form-label-primary">Senha*</label>

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
          :class="v$.password.$error ? 'is-invalid' : ''"
          placeholder="*****"
          v-model="perfil.password"
        />
        <div v-if="v$.password.$error" class="invalid-feedback">
          {{ v$.password.$errors[0].$message }}
        </div>
      </div>

      <div class="input-icon-container mb-3">
        <label for="confirmPassword" class="form-label-primary"
          >Confirmar Senha*</label
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
          :class="v$.confirmPassword.$error ? 'is-invalid' : ''"
          placeholder="*****"
          v-model="perfil.confirmPassword"
        />
        <div v-if="v$.confirmPassword.$error" class="invalid-feedback">
          {{ v$.confirmPassword.$errors[0].$message }}
        </div>
      </div>

      <div class="text-start mb-3">
        <input
          type="checkbox"
          id="concorda"
          class="mx-2"
          v-model="termosDeUso"
        />
        <span>Eu li e concordo com os 
			<a href="/politica-privacidade.pdf">
				Termos de Uso e Políticas de Privacidade.
			</a>
		</span>
      </div>

      <Spinner v-if="sendingPerfil" />

      <div class="mt-4 d-flex">
        <button type="button" class="green-btn-outlined button-specific">
          VOLTAR
        </button>
        <button type="submit" class="green-btn-primary button-specific">
          CADASTRAR
        </button>
      </div>
    </form>
  </div>
</template>

<script setup lang="ts">
//@ts-nocheck
  import { onMounted, ref, computed } from "vue"
  import { getFromCEP } from "../../utils/http"
  import { usePerfilStore } from "../../stores/perfil/store"
  import { useAlertStore } from "../../stores/alert/store"
  import useValidate from "@vuelidate/core"
  import {
    required,
    email,
    minLength,
	maxLength,
    sameAs,
    helpers
  } from "@vuelidate/validators"
  import { isPasswordValid } from "./../../utils/validator/validations"
  import Spinner from "../general/Spinner.vue"

  const perfilStore = usePerfilStore()
  const alertStore = useAlertStore()

  const passwordVisibility = ref(false)
  const confirmPasswordVisibility = ref(false)
  const visibilityIconRef = ref("/visibility-off.svg")
  const confirmVisibilityIconRef = ref("/visibility-off.svg")
  let termosDeUso = ref(false)
  let sendingPerfil = ref(false)
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
    instituicaoId: "5",
    password: "",
    confirmPassword: ""
  })

  const formularioRules = computed(() => {
    return {
      nomeCompleto: {
        required: helpers.withMessage("Nome é obrigatório.", required)
      },
      cpf: { required, minLength: minLength(11) },
      dataNascimento: {
        required: helpers.withMessage(
          "Data de nascimento é obrigatória.",
          required
        )
      },
      email: { required, email },
      telefone: {
		required: helpers.withMessage('Telefone é obrigatório.', required),
		minLength: minLength(10),
		maxLength: maxLength(11)
	  },
      cargo: {
        required: helpers.withMessage("Cargo é obrigatório.", required)
      },
      uf: {},
      cidade: {},
      logradouro: {},
      bairro: {},
      numero: {
        required: helpers.withMessage("Número é obrigatório.", required)
      },
      cep: { required: helpers.withMessage("CEP é obrigatório.", required) },
      instituicaoId: {},
      password: {
        required,
        passwordFormat: helpers.withMessage(
          "Formato da senha: De 8 a 15 caracteres, devendo conter pelo menos: 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caracter especial.",
          isPasswordValid
        )
      },
      confirmPassword: {
        required,
        sameAs: helpers.withMessage(
          "Senhas não conferem.",
          sameAs(perfil.value.password)
        )
      }
    }
  })

  const v$ = useValidate(formularioRules, perfil)

  const changePasswordVisibility = () => {
    passwordVisibility.value = !passwordVisibility.value

    visibilityIconRef.value = passwordVisibility.value
      ? "/visibility-on.svg"
      : "/visibility-off.svg"
  }

  const changeConfirmPasswordVisibility = () => {
    confirmPasswordVisibility.value = !confirmPasswordVisibility.value

    confirmVisibilityIconRef.value = confirmPasswordVisibility.value
      ? "/visibility-on.svg"
      : "/visibility-off.svg"
  }
  const instituicoes = ref({})

  const getInstituicoes = async () => {}

  const cadastrar = async () => {
    if (!termosDeUso.value) {
      alertStore.showWarningMessage("Você precisa aceitar os termos de uso!")
    } else {
      v$.value.$validate()

      console.dir(v$.value)
      if (!v$.value.$error) {
        sendingPerfil.value = true

        await perfilStore.postPerfil(perfil.value)
        sendingPerfil.value = false
        const resposta = perfilStore.perfilResponse.getResponse()
		
        if (resposta.code === 200) {
          alertStore.showTimeoutSuccessMessage("Perfil cadastrado com sucesso!")
          resetForm()
        }else if(resposta.code === 661){
			alertStore.showTimeoutErrorMessage(resposta.message)
		} else {
          alertStore.showTimeoutErrorMessage("Erro ao cadastrar perfil!")
        }
      }
    }
  }

  const resetForm = () => {
    perfil.value = {
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
      instituicaoId: "5",
      password: "",
      confirmPassword: ""
    }

    termosDeUso.value = false
  }

  const buscarCEP = async () => {
    if (perfil.value.cep) {
      try {
        const enderecoCompleto = await getFromCEP(perfil.value.cep)

        if (enderecoCompleto.errors) {
          alertStore.showTimeoutErrorMessage("Erro ao buscar CEP!")
        } else {
          perfil.value.cidade = enderecoCompleto.city
          perfil.value.uf = enderecoCompleto.state
          perfil.value.bairro = enderecoCompleto.neighborhood
          perfil.value.logradouro = enderecoCompleto.street
        }
      } catch (error) {
        console.log(error)
        alertStore.showTimeoutErrorMessage(
          "CEP Incorreto, digite um valor válido."
        )
      }
    }
  }

  onMounted(async () => {
    getInstituicoes()
  })
</script>

<style scoped>
  .titulo-principal {
    font-weight: 700;
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
