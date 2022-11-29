<template>
  <div class="card-evento">
    <div class="img-container">
      <img :src="image" alt="event-image" v-if="hasImage">
      <span v-else class="dark-body-text">{{ nomeEvento }}</span>
    </div>
    <div class="infos-container">
      <h1 class="dark-title">{{ nomeEvento }}</h1>
      <time class="data-evento">{{ dataEvento }}</time>
      <address class="endereco-evento">{{ enderecoEvento }}</address>

	  <a href="#" class="ver-detalhes">Ver detalhes</a>
    </div>
  </div>
</template>

<script setup lang="ts">
  const props = defineProps<{
    hasImage: boolean
    image: string
    nomeEvento: string
    dataEvento: string
    enderecoEvento: string
  }>()
</script>

<style scoped lang="scss">
  .card-evento {
    display: flex;
    flex-direction: column;
    width: 300px;
    height: 400px;
	border-radius: 10px;
	background-color: #fff;
    .dark-body-text {
      font-size: 1rem;
    }
    .img-container {
      height: 65%;
      display: flex;
      justify-content: center;
      align-items: center;
      img {
        width: -webkit-fill-available;
      }
    }
    .infos-container {
      border-top: 3px solid lightgray;
      display: flex;
      flex-direction: column;
      align-items: flex-start;
      justify-content: center;
      height: 50%;
      padding: 10px;

      h1 {
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
        width: 100%;
        text-align: start;
		font-family: 'Montserrat-SemiBold', sans-serif;
		font-size: 1.2rem;
      }

	  .data-evento{
		font-family: 'Montserrat-Medium', sans-serif;
	  }

	  .endereco-evento{
		font-family: 'Montserrat-Regular', sans-serif;
	  }

	  .ver-detalhes{
		color: #1E1E1E;
		font-family: 'Montserrat-Medium', sans-serif;
		font-size: 0.8rem;
		align-self: center;
		margin: 0.3rem;
	  }

      time, address {
        background-repeat: no-repeat;
        background-position: center left;
        background-size: contain;
        padding-left: 30px;
        margin: 5px 0;
      }
      time {
        background-image: url('/public/calendar_icon.svg')
      }
      address {
        background-image: url('/public/location_icon.svg')
      }
    }
  }
  @media (max-width: 1230px) {
    .card-evento {
      .dark-body-text {
        font-size: 1rem;
      }
      .infos-container {
        h1 {
          font-size: 1rem;
        }
        time.dark-body-text, address.dark-body-text {
          font-size: 0.8rem;
        }
      }
    }
  }
  @media (max-width: 1200px) {
    .card-evento {
      margin: 10px auto !important;
    }
  }
  @media (max-width: 576px) {
    .card-evento {
      height: 350px;
      max-width: 250px;
      width: 100%;
    }
  }
</style>