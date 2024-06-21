<template>
  <div>
    <div class="mx-auto card-form box p-5">
      <div class="text-center mt-3 mb-2">
        <h1 class="titulo-principal">ESQUECI MINHA SENHA</h1>
      </div>
      <form id="login-form">
        <div>
          <label for="email" class="form-label-primary">Digite seu e-mail</label>
          <input
            type="text"
            id="email"
            class="form-input-primary"
            :class="v$.email.$error ? 'is-invalid' : ''"
            v-model="usuario.email"
            placeholder="e-maildousuario@email.com"
          />
          <div v-if="v$.email.$error" class="invalid-feedback">
            {{ v$.email.$errors[0].$message }}
          </div>
        </div>

        <div class="button-container">
          <div class="row">
            <div class="col-sm-6 mb-4 mb-md-0">
              <button
                type="button"
                class="green-btn-outlined"
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
import { ref, computed } from 'vue';
import useValidate from '@vuelidate/core';
import { required, email, helpers } from '@vuelidate/validators';
import Spinner from '../general/Spinner.vue';
import { useModalStore } from '../../stores/modal/store';
import { useRecuperacaoSenhaStore } from '../../stores/recuperacao-senha/store';
import { useRouter } from 'vue-router';

const modalStore = useModalStore();
const recuperacaoSenhaStore = useRecuperacaoSenhaStore();
const router = useRouter();
const usuario = ref({
  email: '',
});

const waitingResponse = ref(false);

const usuarioRules = computed(() => {
  return {
    email: {
      required: helpers.withMessage('E-mail é obrigatório.', required),
      email: helpers.withMessage('E-mail inválido.', email),
    },
  };
});

const v$ = useValidate(usuarioRules, usuario);

const enviar = async () => {
  // modalStore.showSuccessModal("O código de recuperação foi enviado para o e-mail informado")
  v$.value.$validate();

  if (!v$.value.$error) {
    waitingResponse.value = true;
    recuperacaoSenhaStore.info.email = usuario.value.email;
    await recuperacaoSenhaStore.enviarEmailComCodigo(usuario.value.email);
    if (recuperacaoSenhaStore.response.code === 200) {
      modalStore.showSuccessModal('O código de recuperação foi enviado para o e-mail informado');
      setTimeout(() => {
        modalStore.closeModal();
        router.push('/codigo');
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
