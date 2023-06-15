<template>
  <section class="contatos-parceiro ghp">
    <header>
      <h3 class="dark-title ms-3">
        Contato
      </h3>
    </header>
    <main class="box-contato">
      <nav id="anchors">
        <a class="light-body-text letter">{{ emailContato }}</a>
        <a class="light-body-text phone">{{ telefone }}</a>
		<div class="pin">
			<img src="/parceiros/parceiro-expandido/pin_icon.svg" alt="">
			<a class="light-body-text">{{ endereco }}</a>

		</div>
        <a class="light-body-text world" :href="website" target="_blank">{{ website }}</a>
      </nav>
      <nav id="social">
        <a :href="instaLink" target="blank" class="ms-0">
          <img src="/instagram_icon.svg" alt="Instagram">
        </a>
		<a :href="instaLink" target="blank">
          <img src="/facebook_icon.svg" alt="Facebook">
        </a>
        <a :href="linkedinLink" target="blank">
          <img src="/linkedin_icon.svg" alt="Linkedin">
        </a>
      </nav>
    </main>
  </section>
</template>

<script setup lang="ts">
  const props = defineProps<{
    instaLink: string
    linkedinLink: string
    wppLink: string
    emailContato: string
    telefone: string
    website: string
    endereco: string
  }>()
</script>

<style scoped lang="scss">
  section.contatos-parceiro {
    background-color: #f6f6f6;
    display: flex;
    flex-direction: column;
    align-items: flex-start;

	h3{
		font-weight: 500;
	}
    .box-contato {
      background-color: #fff;
      min-height: 300px;
      height: 100%;
      max-width: 600px;
      padding: 1rem;
      margin-bottom: 3rem;
      display: flex;
      flex-direction: column;
	  border: 1px solid #6B6A64;
	  border-radius: 10px;
      #anchors {
        width: 90%;
        display: flex;
        flex-direction: column;
        align-items: flex-start;
        a {
          font-size: 1rem;
          margin: 10px 0;
          text-align: justify;
		  color: #000;
        }
      }
      #social {
        width: 10%;
        display: flex;
        flex-direction: row;
        a {
          margin: 10px 1rem;
        }
      }
      .world, .letter, .phone {
		width: 100%;
        padding-left: 35px;
        background-repeat: no-repeat;
        background-position: center left;
        background-size: contain
      }
	  .pin{
		display: flex;
		justify-content: start;
		align-content: flex-start;
		align-items: flex-start;
		img{
			margin-top: .7rem;
		}
	  }
      .world {
        background-image: url('/parceiros/parceiro-expandido/www_icon.svg');
      }
      .letter {
        background-image: url('/parceiros/parceiro-expandido/letter_icon.svg');
      }
      .phone {
        background-image: url('/parceiros/parceiro-expandido/phone_icon.svg');
      }
    }
  }
</style>