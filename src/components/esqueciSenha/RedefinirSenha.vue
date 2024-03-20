<template>
  <div>
    <div class="mx-auto card-form box p-5">
      <div class="text-center mt-3 mb-2">
        <h1 class="titulo-principal">REDEFINIR SENHA</h1>
      </div>
      <form id="login-form">
        <div class="input-icon-container">
          <div class="input-icon-container mb-3">
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
              :class="v$.password.$error ? 'is-invalid' : ''"
              placeholder="*****"
              v-model="usuario.password"
            />
            <div v-if="v$.password.$error" class="invalid-feedback">
              {{ v$.password.$errors[0].$message }}
            </div>
          </div>

          <div class="input-icon-container mb-3">
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
              :class="v$.confirmPassword.$error ? 'is-invalid' : ''"
              placeholder="*****"
              v-model="usuario.confirmPassword"
            />
            <div v-if="v$.confirmPassword.$error" class="invalid-feedback">
              {{ v$.confirmPassword.$errors[0].$message }}
            </div>
          </div>
        </div>

        <div class="button-container">
          <div class="row">
            <div class="col-sm-6">
              <button
                type="button"
                class="green-btn-outlined mx-2"
                @click="$router.push({ name: 'Login' })"
              >
                VOLTAR
              </button>
            </div>
            <div class="col-sm-6">
              <button
                type="button"
                class="green-btn-primary full-size"
                :disabled="waitingResponse"
                @click="enviar"
              >
                ENVIAR
              </button>
            </div>
          </div>

          <Spinner v-if="waitingResponse" />
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref, computed } from "vue";
import { useUserStore } from "../../stores/user/store";
import { useAlertStore } from "../../stores/alert/store";
import { useRouter } from "vue-router";
import useValidate from "@vuelidate/core";
import Spinner from "../general/Spinner.vue";
import { required, email, helpers, sameAs } from "@vuelidate/validators";
import { isPasswordValid } from "./../../utils/validator/validations";
import { useRecuperacaoSenhaStore } from "../../stores/recuperacao-senha/store";
import { useModalStore } from "../../stores/modal/store";

const router = useRouter();

const alertStore = useAlertStore();
const userStore = useUserStore();

const usuario = ref({
  password: "",
  confirmPassword: "",
});

const passwordVisibility = ref(false);
const confirmPasswordVisibility = ref(false);
const visibilityIconRef = ref("/visibility-off.svg");
const confirmVisibilityIconRef = ref("/visibility-off.svg");

const recuperacaoSenhaStore = useRecuperacaoSenhaStore();
const modalStore = useModalStore();
const waitingResponse = ref(false);
const usuarioRules = computed(() => {
  return {
    password: {
      required,
      passwordFormat: helpers.withMessage(
        "Formato da senha: De 8 a 15 caracteres, devendo conter pelo menos: 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caracter especial.",
        isPasswordValid
      ),
    },
    confirmPassword: {
      required,
      sameAs: helpers.withMessage(
        "Senhas não conferem.",
        sameAs(usuario.value.password)
      ),
    },
  };
});

const v$ = useValidate(usuarioRules, usuario);

const changePasswordVisibility = () => {
  passwordVisibility.value = !passwordVisibility.value;

  visibilityIconRef.value = passwordVisibility.value
    ? "/visibility-on.svg"
    : "/visibility-off.svg";
};

const changeConfirmPasswordVisibility = () => {
  confirmPasswordVisibility.value = !confirmPasswordVisibility.value;

  confirmVisibilityIconRef.value = confirmPasswordVisibility.value
    ? "/visibility-on.svg"
    : "/visibility-off.svg";
};

const enviar = async () => {
  v$.value.$validate();

  if (!v$.value.$error) {
    waitingResponse.value = true;
    await recuperacaoSenhaStore.redefinirSenha(
      usuario.value.password,
      usuario.value.confirmPassword
    );
    if (recuperacaoSenhaStore.response.code === 200) {
      modalStore.showSuccessModal(recuperacaoSenhaStore.response.message);
      setTimeout(() => {
        modalStore.closeModal();
        router.push("/login");
      }, 3000);
      waitingResponse.value = false;
    } else {
      waitingResponse.value = false;
      modalStore.showErrorModal(recuperacaoSenhaStore.response.message);
    }
  }
  waitingResponse.value = false;
};
</script>

<style scoped lang="scss">
.titulo-principal {
  font-weight: 700;
  font-size: 1.5rem;
  color: black;
  margin: 1rem 0 3rem 0;
}

.card-form {
  width: 50%;
  margin: 12% auto;
  transition: width 0.8s;
}

.button-container {
  margin: 3rem 0 1rem 0;
}

@media (max-width: 768px) {
  .card-form {
    width: 90%;
    margin: 10% auto;
  }
}
</style>
