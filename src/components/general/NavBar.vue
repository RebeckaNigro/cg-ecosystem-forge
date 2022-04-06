<template>
  <nav class="navbar navbar-light bg-light">
    <div class="container-fluid">
      <router-link class="navbar-brand" to="/">
        <img src="/colorful_logo.png" alt="logo">
      </router-link>
      <section class="container-links dark-title">
        <router-link :to="link.path" v-for="link in maisInfos" class="navbar-text">{{ link.title }}</router-link> 
      </section>
      <button type="button" class="light-title" id="login" @click="$router.push({ name: 'Login'})">LOGIN</button>
      <div class="dropdown dropstart">
        <button class="btn btn-secondary dropdown-toggle" type="button" id="menuInfos" data-bs-toggle="dropdown" aria-expanded="false">
          <div v-for="hamburguer in 3" class="hamburguer" />
        </button>
        <ul class="dropdown-menu" aria-labelledby="menuInfos">
          <li v-for="link in maisInfos">
            <router-link :to="link.path" class="dropdown-item">{{ link.title }}</router-link> 
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script setup lang="ts">
import { onMounted, reactive } from 'vue';

defineProps<{
  isTransparent: Boolean
}>()

const maisInfos = reactive([
  {
    title: 'QUEM SOMOS',
    path: '/quem-somos'
  },
  {
    title: 'PARCEIROS',
    path: '/parceiros'
  },
  {
    title: 'NOTÍCIAS',
    path: '/noticias'
  },
  {
    title: 'DOCUMENTOS',
    path: '/documentos'
  },
  {
    title: 'AGENDA',
    path: '/eventos'
  },
  {
    title: 'FALE CONOSCO',
    path: '/fale-conosco'
  }
])

onMounted(() => {
  // TODO: terminar centralização relativa
  const logo: HTMLElement = document.querySelector('.navbar-brand')!
  const logoWidth: number = logo.clientWidth

  const containerLinks: HTMLElement = document.querySelector('.container-links')!
  const containerCss = window.getComputedStyle(containerLinks)

  const mLeft: number = parseInt(containerCss.marginLeft)
  const fixedMargin: Number = new Number(mLeft - logoWidth)

  containerLinks.style.cssText = 'margin-left: ' + fixedMargin.toString() + ' !important';
})
</script>

<style scoped lang="scss">
nav.navbar {
  section.container-links {
    font-size: 1.2rem;
  }
}
.container-fluid {
  flex-wrap: nowrap !important;
}
.container-links {
  display: flex;
  flex-direction: row;
  margin: auto;
  flex-wrap: wrap;
}
.navbar-text {
  margin: auto 20px;
  text-decoration: none;
}
a {
  img {
    width: 70%;
    min-width: 60px;
  }
}
.dropdown {
  display: none;
}
.hamburguer {
  height: 4px;
  width: 30px;
  background-color: gray;
  border-radius: 25px;
}
.dropdown-toggle {
  background: none;
  border: none;
  display: flex;
  flex-direction: column;
  height: 40px;
  justify-content: space-between;
  padding: 0;
  /* margin-right: 30px; */
}
button#login {
  background-color: #ba5366;
  color: #fff;
  width: 120px;
  height: 40px;
  border-radius: 25px;
  font-size: 0.8rem;
  border: 0;
}
@media (max-width: 991px) {
  .navbar-text {
    margin: 5px 10px;
    padding: 0;
  }
  nav.navbar {
    section.container-links {
      font-size: 1rem;
    }
  }
}
@media (max-width: 767px) {
  nav.navbar {
    section.container-links {
      font-size: 0.7rem;
    }
  }
  button#login {
    font-size: 0.6rem;
    width: 100px;
    height: 30px;
  }
}
@media (max-width: 485px) {
  .container-links {
    display: none;
  }
  a {
    img {
      width: 60px
    }
  }
  .dropdown {
    display: block;
  }
  .container-fluid {
    justify-content: space-between;
  }
  button#login {
    font-size: 0.5rem;
    width: 80px;
    height: 25px;
  }
}
</style>