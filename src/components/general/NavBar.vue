<template>
  <nav class="navbar navbar-light">
    <div class="container-fluid flex-nowrap">
      <div class="d-flex align-items-center">
        <MenuLateral />

        <router-link class="navbar-brand" to="/">
        <img src="/logonova.svg" alt="logonova" class="img-fluid" width="89%" />
        </router-link>
      </div>
      <section class="container-links d-none d-xl-flex fs-6 m-auto flex-wrap dark-title">
        <router-link
          :to="link.path"
          v-for="link in maisInfos"
          class="navbar-text my-auto mx-lg-3 text-decoration-none"
          >{{ link.title }}</router-link
        >
      </section>

      <div class="d-flex align-items-center">
        <button
          type="button"
          class="light-title me-3"
          id="login"
          @click="$router.push({ name: 'Login' })"
          v-if="!userStore.loggedUser.token"
        >
          LOGIN
        </button>

        <div class="dropdown dropdown-center" v-if="userStore.loggedUser.token" id="profile">
          <button
            class="border-0 dropdown-toggle"
            type="button"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            <img src="/noticias/noticia-expandida/user.svg" alt="Ícone usuário" class="img-fluid" />
            <span class="px-2">{{ userStore.loggedUser.userName }}</span>
            <img src="/arrow_down_icon.svg" alt="Abrir opções" class="img-fluid" />
          </button>
          <ul class="dropdown-menu" aria-labelledby="profile">
            <li class="ps-4" id="logout" @click="logout">
              <span>Sair</span>
            </li>
          </ul>
        </div>

        <div class="dropdown dropstart d-xl-none">
          <button
            class="btn btn-secondary dropdown-toggle dropdown-toggle-responsive"
            type="button"
            id="menuInfos"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          ></button>
          <ul class="dropdown-menu" aria-labelledby="menuInfos">
            <li v-for="link in maisInfos">
              <router-link :to="link.path" class="dropdown-item">{{ link.title }}</router-link>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </nav>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import { RouterLink, useRouter } from 'vue-router';
import { useUserStore } from '../../stores/user/store';
import MenuLateral from './MenuLateral.vue';

const maisInfos = reactive([
  {
    title: 'QUEM SOMOS',
    path: '/quem-somos',
  },
  {
    title: 'PARCEIROS',
    path: '/parceiros',
  },
  {
    title: 'NOTÍCIAS',
    path: '/noticias',
  },
  {
    title: 'DOCUMENTOS & EDITAIS',
    path: '/documentos',
  },
  {
    title: 'EVENTOS',
    path: '/eventos',
  },
  {
    title: 'FALE CONOSCO',
    path: '/fale-conosco',
  },
]);

const userStore = useUserStore();
const router = useRouter();

const logout = () => {
  userStore.logout();
  router.push({ name: 'Login' });
};

onMounted(() => {
  // TODO: terminar centralização relativa
  const logo: HTMLElement = document.querySelector('.navbar-brand')!;
  const logoWidth: number = logo.clientWidth;

  const containerLinks: HTMLElement = document.querySelector('.container-links')!;
  const containerCss = window.getComputedStyle(containerLinks);

  const mLeft: number = parseInt(containerCss.marginLeft);
  const fixedMargin: Number = new Number(mLeft - logoWidth);

  containerLinks.style.cssText = 'margin-left: ' + fixedMargin.toString() + ' !important';
});
</script>

<style scoped lang="scss">
nav.navbar {
  background: #efefef;
}

#profile {
  .dropdown-menu {
    background-color: #efefef;
    font-weight: 500;
    border: 1px solid #505050;
    border-top: none;
    border-radius: 0 0 10px 10px;
    padding: 0.6rem 0;
    list-style: none;
    margin-left: 0.8rem;
  }
}
.dropdown-toggle-responsive {
  background: url('/menu_icon.svg') no-repeat;
  border: none;
  display: flex;
  height: 24px;
  width: 24px;
  padding: 0;
  margin-right: 30px;
}

.dropstart .dropdown-toggle::before,
.dropdown-toggle::after {
  display: none;
}

button#login {
  background-color: var(--orange);
  color: #fff;
  width: 120px;
  height: 40px;
  border-radius: 25px;
  font-size: 0.8rem;
  border: 0;
  font-weight: 700;
}

@media (max-width: 767px) {
  button#login {
    font-size: 0.6rem;
    width: 100px;
    height: 30px;
  }
}

@media (max-width: 1024px) {
  #profile {
    button {
      span {
        font-size: 0.9rem;
      }
    }
  }

  button#login {
    font-size: 0.5rem;
    width: 80px;
    height: 25px;
  }
}
</style>
