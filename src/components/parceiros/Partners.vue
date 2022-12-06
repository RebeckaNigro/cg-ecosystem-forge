<template> <!-- este é o componente que vai na tela home, o carrosel de empresas parceiras -->
  <section class="container-fluid partners-container ghp">
    <h1 class="dark-title fs-2">EMPRESAS PARCEIRAS</h1>
    <main>
      <div id="partnersCarousel" class="carousel slide carousel-dark" data-bs-ride="carousel" pause="true">
        <div class="carousel-inner" pause="true">
		 
			<div class="carousel-item" v-for="(logo, index) in logosUrlArray" :key="index" :class="{'active': index === 0}">
				<img :src="logo" class="d-block" alt="logo parceiro" >
			</div>
        
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#partnersCarousel" data-bs-slide="prev">
          <span class="carousel-control-prev-icon" aria-hidden="true"></span>
          <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#partnersCarousel" data-bs-slide="next">
          <span class="carousel-control-next-icon" aria-hidden="true"></span>
          <span class="visually-hidden">Next</span>
        </button>
      </div>
    </main>
  </section>
</template>

<script setup lang="ts">

 const props = defineProps<{
   logosUrlArray: string[]
 }>()

 
</script>

<style scoped lang="scss">
  section.partners-container {
    // padding: 40px 200px !important;
    height: 500px;
    display: flex;
    flex-direction: column;
    justify-content: center;
  }
  .carousel-item {
    img {
      /* min-width: 240px !important; */
      margin: auto;
      width: auto;
      max-width: 500px;
      height: 300px;
    }
  }
  @media (max-width: 768px) {
    section.partners-container {
      h1 {
        font-size: 1.2rem;
      }
    }
  }
</style>