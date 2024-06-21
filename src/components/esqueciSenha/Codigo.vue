<template>
  <div>
    <div class="mx-auto card-form box p-5">
      <div class="text-center mt-3 mb-2">
        <h1 class="titulo-principal">Inserir código de recuperação</h1>
      </div>
      <form id="login-form">
        <div>
          <label for="codigo" class="form-label-primary">Código de recuperação</label>
          <input
            type="text"
            id="codigo"
            class="form-input-primary"
            :class="v$.codigo.$error ? 'is-invalid' : ''"
            v-model="usuario.codigo"
            placeholder=""
          />
          <div v-if="v$.codigo.$error" class="invalid-feedback">
            {{ v$.codigo.$errors[0].$message }}
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
          <div v-if="waitingResponse" class="spinner-border m-auto" role="status">
            <Spinner />
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import useValidate from '@vuelidate/core';
import { required, helpers, numeric } from '@vuelidate/validators';
import Spinner from '../general/Spinner.vue';
import { useRecuperacaoSenhaStore } from '../../stores/recuperacao-senha/store';
import { useRouter } from 'vue-router';

const usuario = ref({
  codigo: '',
});
const recuperacaoSenhaStore = useRecuperacaoSenhaStore();
const router = useRouter();
const waitingResponse = ref(false);

const usuarioRules = computed(() => {
  return {
    codigo: {
      required: helpers.withMessage('Código é obrigatório.', required),
      codigo: helpers.withMessage('Código inválido.', numeric),
    },
  };
});

const v$ = useValidate(usuarioRules, usuario);

const enviar = async () => {
  v$.value.$validate();
  if (!v$.value.$error) {
    recuperacaoSenhaStore.info.otp = usuario.value.codigo;
    router.push('/redefinir-senha');
    waitingResponse.value = true;
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
