<template>
  <div class="card-documentos-content">
    <div>
      <img v-if="hasIcon" :src="iconPath" />
      <h1 class="dark-title">{{title}}</h1>
    </div>
    

    <p>{{ text }}</p>

    <button v-if="!hasDownload" type="button" @click="$router.push({ name: 'DocumentosPesquisa', params: { tipoDocumento: title.toLowerCase()}})">
		Ver mais
	</button>
    <a v-else :href="contentToDownload" :download="title" class="btn-simulator">
      <button type="button" class="hide light-title">Ver mais</button>
    </a>
  </div>
</template>

<script setup lang="ts">

  const props = defineProps<{
    title: string
    iconPath: string
    text: string
    hasIcon: boolean
    contentToDownload: string
    hasDownload: boolean
  }>()

  const downloadContent = async () => {
    alert('tem que implementar');
  }
</script>

<style lang="scss" scoped>
.card-documentos-content {
  max-width: 325px;
  width: 100%;
  border: 1px solid black;
  border-radius: 10px;
  padding: 28px 24px;
  display: flex;
  height: 325px;
  flex-direction: column;
  margin: 10px 0;
  align-items: center;
  box-shadow: 0px 0px 30px rgba(0, 0, 0, 0.1);
  background: #fff;
  
  .hide {
    background-color: unset;
    border: none;
    font-size: 14px;
    font-weight: 600;
	text-transform: uppercase;
  }
  > div {
    display: flex;
    align-items: center;

    h1 {
      margin: 0;
      margin-left: 12px;
      font-weight: bold;
      font-size: 1.4rem;
      font-weight: 600;
      overflow: hidden;
      text-overflow: ellipsis;
      display: -webkit-box;
      -webkit-line-clamp: 3; /* number of lines to show */
      line-clamp: 3; 
      -webkit-box-orient: vertical;
    }

    img {
      width: 36px;
      height: 32px;
    }
  }

  > p {
	font-size: .8rem;
    margin: 20px 0;
    text-align: justify;
    height: 100%;
    overflow-y: auto;
    max-height: 168px;
  }

  > button, .btn-simulator {
    padding: 6px 28px;
    align-self: center;
    border-radius: 20px;
    border: 0;
	font-weight: 700;
    background-color: #639280;
    color: #ffffff;
    font-weight: 600;
    text-transform: uppercase;
    font-size: 14px;
  }
  > button:hover, .btn-simulator:hover  {
    background-color: #85a69a;
  }
  /* width */
  ::-webkit-scrollbar {
    width: 7px;
  }

  /* Track */
  ::-webkit-scrollbar-track {
    background-color: rgba($color: #ddd, $alpha: 1);
  }

  /* Handle */
  ::-webkit-scrollbar-thumb {
    background: gray;
  }

  /* Handle on hover */
  ::-webkit-scrollbar-thumb:hover {
    background: #fff;
  }
}
@media (max-width: 576px) {
  .card-documentos-content {
    width: 200px;
    height: 250px;
    > div > h1 {
      font-size: 0.8rem;
    }
    > p {
      font-size: 0.7rem;
    }
  }
}
</style>