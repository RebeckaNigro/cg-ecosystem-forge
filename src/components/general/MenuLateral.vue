<template>
  <div v-if="userStore.loggedUser.token" class="me-3">
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="offcanvas"
      data-bs-target="#offcanvasNavbar"
      aria-controls="offcanvasNavbar"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div
      class="offcanvas offcanvas-start"
      tabindex="-1"
      id="offcanvasNavbar"
      aria-labelledby="offcanvasNavbarLabel"
    >
      <div class="offcanvas-body">
        <ul class="navbar-nav">
          <li class="nav-item d-flex">
            <a
              :href="$router.resolve({ name: 'UserHome' }).href"
              class="nav-link home-menu"
              aria-current="page"
              >Home</a
            >
          </li>

          <hr class="divider" />

          <li class="nav-item dropdown d-flex flex-column align-items-start">
            <a
              class="nav-link dropdown-toggle documentos-menu"
              href="#"
              role="button"
              data-bs-toggle="collapse"
              data-bs-target="#menuDocumentOptions"
              aria-controls="menuDocumentOptions"
              aria-expanded="false"
            >
              Documentos
            </a>

            <ul class="collapse sub-item" id="menuDocumentOptions">
              <li>
                <a
                  :href="$router.resolve({ name: 'DocumentosCriados' }).href"
                  class="dropdown-item"
                  >Visualizar documentos</a
                >
              </li>
              <li>
                <a
                  :href="$router.resolve({ name: 'GerenciaDocumento' }).href"
                  class="dropdown-item"
                  >Criar novo documento</a
                >
              </li>
            </ul>
          </li>

          <hr class="divider" />

          <li class="nav-item dropdown d-flex flex-column align-items-start">
            <a
              class="nav-link dropdown-toggle noticias-menu show"
              href="#"
              role="button"
              data-bs-toggle="collapse"
              data-bs-target="#menuNewsOptions"
              aria-controls="menuNewsOptions"
              aria-expanded="false"
            >
              Notícias
            </a>

            <ul class="collapse sub-item" id="menuNewsOptions">
              <li>
                <a
                  :href="$router.resolve({ name: 'NoticiasCriadas' }).href"
                  class="dropdown-item"
                  >Visualizar notícias</a
                >
              </li>
              <li>
                <a
                  :href="$router.resolve({ name: 'GerenciaNoticia' }).href"
                  class="dropdown-item"
                  >Criar nova notícia</a
                >
              </li>
            </ul>
          </li>

          <hr class="divider" />

          <li class="nav-item dropdown d-flex flex-column align-items-start">
            <a
              class="nav-link dropdown-toggle eventos-menu show"
              href="#"
              role="button"
              data-bs-toggle="collapse"
              data-bs-target="#menuEventsOptions"
              aria-controls="menuEventsOptions"
              aria-expanded="false"
            >
              Eventos
            </a>

            <ul class="collapse sub-item" id="menuEventsOptions">
              <li>
                <a
                  :href="$router.resolve({ name: 'EventosCriados' }).href"
                  class="dropdown-item"
                  >Visualizar eventos</a
                >
              </li>
              <li>
                <a
                  :href="$router.resolve({ name: 'GerenciaEvento' }).href"
                  class="dropdown-item"
                  >Criar novo evento</a
                >
              </li>
            </ul>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { useUserStore } from "../../stores/user/store"
  import { RouterLink } from "vue-router"

  const userStore = useUserStore()
</script>

<style scoped lang="scss">
  .sub-item {
    font-size: 1rem;
    padding: 0.5rem 0;

    a {
      padding-left: 0;
    }
  }
  .navbar-light {
    .navbar-toggler {
      border: unset;
    }

    .navbar-toggler:focus {
      outline: 0;
      box-shadow: none;
    }
    .navbar-toggler-icon {
      background-image: url("../../../public/hamburguer_icon.svg");
    }
  }
  .offcanvas-start {
    width: 300px;
  }
  .navbar-nav {
    margin-top: 1.7rem;

    .divider {
      background-color: #6b6a64;
    }
    .nav-item {
	  font-weight: 500;
      font-size: 1.2rem;
      padding-left: 1.5rem;

      .nav-link {
        color: #000;
      }
      ul {
        margin-left: 3.5rem;
      }
      .home-menu,
      .noticias-menu,
      .eventos-menu,
      .documentos-menu {
        padding: 0;
        padding-left: 2.5rem;
        background-size: contain;
      }
      .home-menu {
        background: url("../../../public/home_icon.svg") no-repeat;
      }
      .documentos-menu {
        background: url("../../../public/docs_icon.svg") no-repeat;
      }
      .noticias-menu {
        background: url("../../../public/news_icon.svg") no-repeat;
      }

      .eventos-menu {
        background: url("../../../public/events_icon.svg") no-repeat;
      }
    }
  }
</style>
