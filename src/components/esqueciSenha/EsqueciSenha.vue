<template>
  <div>
    <div class="mx-auto card-form box p-5">
      <div class="text-center mt-3 mb-2">
        <h1 class="titulo-principal">ESQUECI MINHA SENHA</h1>
      </div>
      <form id="login-form">
        <div>
          <label for="email" class="form-label-primary"
            >Digite seu e-mail</label
          >
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
          <div
            v-if="waitingResponse"
            class="spinner-border m-auto"
            role="status"
          >
            <Spinner />
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { reactive, ref, computed } from "vue"
  import { useUserStore } from "../../stores/user/store"
  import { useAlertStore } from "../../stores/alert/store"
  import { useRouter } from "vue-router"
  import useValidate from "@vuelidate/core"
  import { required, email, helpers } from "@vuelidate/validators"
  import Spinner from "../general/Spinner.vue"

  const router = useRouter()

  const alertStore = useAlertStore()
  const userStore = useUserStore()

  const usuario = ref({
    email: ""
  })

  const waitingResponse = ref(false)

  const usuarioRules = computed(() => {
    return {
      email: {
        required: helpers.withMessage("E-mail é obrigatório.", required),
        email: helpers.withMessage("E-mail inválido.", email)
      }
    }
  })

  const v$ = useValidate(usuarioRules, usuario)

  const enviar = async () => {
    v$.value.$validate()

    if (!v$.value.$error) {
      waitingResponse.value = true
    }
    waitingResponse.value = false
  }
</script>

<style scoped lang="scss">
  .titulo-principal {
    font-family: Montserrat-Bold;
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

  //  section#login {
  //    background-color: #fff;
  //    border-radius: 10px;
  //    border: 1px solid black;
  //    max-width: 500px;
  //    width: 100%;
  //    height: 500px;
  //    padding: 40px 60px 60px 60px;
  //    margin: 5rem auto;
  //
  //    header > h1 {
  //      text-align: center;
  //      margin-bottom: 5rem;
  //    }
  //    main {
  //      font-family: 'Gotham-Book';
  //      font-size: 1rem;
  //      form {
  //        height: 250px;
  //        display: flex;
  //        flex-direction: column;
  //        align-items: flex-start;
  //        button {
  //          width: 100%;
  //          box-shadow: 1px 1px 5px black;
  //        }
  //        input {
  //          width: 100%;
  //          margin-bottom: 2rem;
  //          font-family: 'Gotham-Book';
  //        }
  //      }
  //    }
  //  }
  //  @media (max-width: 468px) {
  //    section#login {
  //      border: 0;
  //    }
  //  }
</style>
