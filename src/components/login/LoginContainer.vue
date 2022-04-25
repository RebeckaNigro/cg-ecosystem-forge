<template>
  <section id="login">
    <header>
      <h1 class="dark-title">
        Login
      </h1>
    </header>
    <main>
      <form id="login-form" @submit.prevent="callStoreLogin(user.username, user.password)">
        <label for="username">Nome de Usuário</label>
        <input type="text" id="username" placeholder="Nome de Usuário" v-model="user.username" required>
        <label for="password">Senha</label>
        <input type="password" id="password" placeholder="*****" v-model="user.password" required>
        <button v-if="!waitingResponse" type="submit">Entrar</button>
        <div v-else class="spinner-border m-auto" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>
      </form>     
    </main>
  </section>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue';
import { useUserStore } from '../../stores/user/store'
import { useRouter } from 'vue-router';

const router = useRouter();

const userStore = useUserStore()

const user = reactive({
  username: '',
  password: ''
})

const waitingResponse = ref(false)
const callStoreLogin = async (e: string, p: string) => {
  waitingResponse.value = true
  await userStore.login(e, p)
  if (userStore.userResponse.code === 200) {
    router.push({ name: 'UserHome'})
  } else {
    // push notification
    const warning = document.createElement('span')
    warning.textContent = 'Por favor, verifique a senha e o nome de usuário.'
    warning.classList.add('m-auto')
    warning.style.color = 'red'

    const form = document.querySelector('#login-form')
    form?.append(warning)

    setTimeout(() => {
      form?.removeChild(warning)
    }, 4000);
  }
  waitingResponse.value = false
}
</script>

<style scoped lang="scss">
  section#login {
    background-color: #fff;
    border-radius: 10px;
    border: 1px solid black;
    max-width: 500px;
    width: 100%;
    height: 500px;
    padding: 40px 60px 60px 60px;
    margin: 5rem auto;

    header > h1 {
      text-align: center;
      margin-bottom: 5rem;
    }
    main {
      font-family: 'Gotham-Book';
      font-size: 1rem;
      form {
        height: 250px;
        display: flex;
        flex-direction: column;
        align-items: flex-start;
        button {
          width: 100%;
          box-shadow: 1px 1px 5px black;
        }
        input {
          width: 100%;
          margin-bottom: 2rem;
          font-family: 'Gotham-Book';
        }
      }
    }
  }
  @media (max-width: 468px) {
    section#login {
      border: 0;
    }
  }
</style>