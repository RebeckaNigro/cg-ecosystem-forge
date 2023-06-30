<template>
  <section class="contatos-parceiro d-flex flex-column align-items-start container px-lg-5">
    
	<h3 class="ms-5 ps-3">
		Contato
	</h3>
    
    <main class="box-contato ms-md-5">
      <nav id="anchors" class="d-flex flex-column align-items-start">
        <a class="letter text-decoration-none">{{ emailContato }}</a>
        <a class="phone text-decoration-none">{{ telefone }}</a>
		<a class="address text-decoration-none">{{ endereco }}</a>
        <a class="world text-decoration-none" :href="website" target="_blank">{{ website }}</a>
      </nav>
      <nav id="social">
        <a :href="instaLink" target="blank" class="ms-0">
          <img src="/instagram_icon.svg" alt="Instagram">
        </a>
		<!-- <a :href="instaLink" target="blank">
          <img src="/facebook_icon.svg" alt="Facebook">
        </a> -->
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
    background-color: #fbfbfb;
	font-family: 'Inter', sans-serif;
	font-weight: 500;
	h3{
		color: #1E1E1E;
		text-transform: uppercase;
		font-size: 24px;
	}
    .box-contato {
      background-color: #fff;
      min-height: 300px;
      height: 100%;
      max-width: 500px;
      padding: 1rem;
      margin-bottom: 3rem;
      display: flex;
      flex-direction: column;
	  border: 1px solid #6B6A64;
	  border-radius: 10px;

      #anchors {
        width: 90%;
        
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
      .world, .letter, .phone, .address{
		width: 100%;
        padding-left: 35px;
        background-repeat: no-repeat;
        background-position-x: left;
		background-position-y: center;
        background-size: contain
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

	  .address{
		background-image: url('/parceiros/parceiro-expandido/pin_icon.svg');
		background-size: 30px;
	  }
    }
  }

  @media screen and (max-width: 580px){
	section.contatos-parceiro {
		h3{
			font-size: 20px;
		}
	}
  }
</style>