<template>
  <div>
    <div class="mx-auto card-form box p-5">
      <div class="text-center mt-3 mb-2">
        <h1 class="titulo-principal">LOGIN</h1>
      </div>
      <form
        id="login-form"
        @submit.prevent="callStoreLogin(user.username, user.password)"
      >
        <div>
          <label for="username" class="form-label-primary">E-mail</label>
          <input
            type="text"
            id="username"
            class="form-input-primary"
            v-model="user.username"
            placeholder="e-maildousuario@email.com"
            required
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
            v-model="user.password"
            required
          />
        </div>

        <div class="row">
          <div class="col-8 text-start">
            <input
              type="checkbox"
              id="lembrarUsuario"
              class="mx-2"
              v-model="lembrarUsuario"
            />
            <span>Lembrar meu usuário.</span>
          </div>
          <div class="col-4 text-end">
            <a href="">Esqueceu sua senha?</a>
          </div>
        </div>

        <div class="button-container">
          <button
            type="submit"
            class="green-btn-primary full-size"
            v-if="!waitingResponse"
            @click="cadastrar"
          >
            ENTRAR
          </button>
          <div v-else class="spinner-border m-auto" role="status">
            <span class="visually-hidden">Loading...</span>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { reactive, ref } from "vue"
  import { useUserStore } from "../../stores/user/store"
  import { useRouter } from "vue-router"

  const router = useRouter()

  const userStore = useUserStore()

  const user = reactive({
    username: "",
    password: ""
  })

  const waitingResponse = ref(false)
  const lembrarUsuario = ref(false)
  const passwordVisibility = ref(false)
  const visibilityIconRef = ref("src/assets/icons/visibility-off.svg")
  const changePasswordVisibility = () => {
    passwordVisibility.value = !passwordVisibility.value

    visibilityIconRef.value = passwordVisibility.value
      ? "src/assets/icons/visibility-on.svg"
      : "src/assets/icons/visibility-off.svg"
  }

  const callStoreLogin = async (e: string, p: string) => {
    waitingResponse.value = true
    await userStore.login(e, p)
    if (userStore.userResponse.code === 200) {
      router.push({ name: "UserHome" })
    } else {
      // push notification
      const warning = document.createElement("span")
      warning.textContent = "Por favor, verifique a senha e o nome de usuário."
      warning.classList.add("m-auto")
      warning.style.color = "red"

      const form = document.querySelector("#login-form")
      form?.append(warning)

      setTimeout(() => {
        form?.removeChild(warning)
      }, 4000)
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
