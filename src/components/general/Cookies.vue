<template>
  <div id="cookies" class="d-flex" v-if="!useStore.choseCookies">
    <p>
      Ao clicar em "Aceitar todos os cookies", concorda com o armazenamento de cookies no seu
      dispositivo para melhorar a navegação no site, analisar a utilização do site e ajudar nas
      nossas iniciativas de marketing.
    </p>
    <div class="btns-container">
      <button class="reject dark-body-text" @click="hideNotification(true)">Rejeitar Todos</button>
      <button class="accept light-body-text" @click="hideNotification(true)">
        Aceitar todos os cookies
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useCookiesStore } from '../../stores/cookies/store';
const useStore = useCookiesStore();
const hideNotification = (userChose: boolean) => {
  useStore.choseCookies = userChose;
};
</script>

<style scoped lang="scss">
#cookies {
  width: 100%;
  padding: 0 20px;
  align-items: center;
  justify-content: center;
  position: fixed;
  bottom: 0;
  background-color: #fff;
  p {
    width: 65%;
    text-align: justify;
    margin: 0;
  }
}
.btns-container {
  width: 35%;
  text-align: center;
  .reject,
  .accept {
    margin: 15px 10px;
    height: 40px;
    border-radius: 20px;
    font-size: 0.8rem;
    white-space: nowrap;
  }
  .reject {
    border: 1px solid black;
    width: 120px;
    background-color: #fff;
  }
  .accept {
    border: 0;
    width: 180px;
    background-color: #000;
  }
}
/* @media (max-width: 991px) {
    #cookies {
      width: 100%;
      padding: 0 20px;
      align-items: center;
      justify-content: center;
      position: fixed;
      bottom: 0;
      background-color: #fff;
      p {
        width: 65%;
        text-align: justify;
        margin: 0;
      }
    }
  .btns-container {
    width: 35%;
    text-align: center;
    .reject, .accept {
      margin: 15px 10px;
      height: 40px;
      border-radius: 20px;
      font-size: 0.8rem;
      white-space: nowrap;
    }
    .reject {
      border: 1px solid black;
      width: 120px;
      background-color: #fff;
    }
    .accept {
      border: 0;
      width: 180px;
      background-color: #000;
    }
  }
  } */
</style>
