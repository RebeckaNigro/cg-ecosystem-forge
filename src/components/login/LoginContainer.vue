<template>
  <section id="login">
    <header>
      <h1 class="dark-title">
        Login
      </h1>
    </header>
    <main>
      <form id="login-form">
        <label for="email">E-mail</label>
        <input type="text" id="email" placeholder="E-mail" required>
        <label for="password">Senha</label>
        <input type="password" id="password" placeholder="*****" required>
        <button type="submit">Entrar</button>
      </form>     
    </main>
  </section>
</template>

<script setup lang="ts">

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