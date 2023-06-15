<template>

	<div class="useless-box">
			<div class="header-card">
				<span>{{tags}}</span>
				<img src="/public/view_icon.svg" alt="Visualizar documento">
			</div>
			
			<div class="nome-documento">{{ nomeDocumento }}</div>
			<div class="nome-autor">Nome do autor</div>
			<div class="descricao-documento">{{descricao}}</div>
			<div class="atualizacao">Atualizado em: 01/01/2000 às 12:00</div>
      	</div>
</template>

<script setup lang="ts">

const props = defineProps<{
	nomeDocumento: string
	descricao: string
	tags: string
}>()
</script>

<style lang="scss">
.useless-box {
    background-color: #fff;
	border: 1px solid #6B6A64;
	border-radius: 10px;
    display: flex;
    flex-direction: column;
    justify-content: space-evenly;
    align-items: flex-start;
    height: 300px;
    width: 300px;
    margin: 10px 0;
	padding: 1rem;

	.header-card{
		display: flex;
		justify-content: space-between;
		width: 100%;

		span{
			font-weight: 300;
			font-size: 0.8rem;
		}
	  }

	  .nome-documento{
		font-weight: 600;
		font-size: 1.5rem;
		text-align: start;
	  }

	  .nome-autor{
		font-weight: 400;
		font-size: 1rem;
	  }

	  .descricao-documento{
		font-weight: 400;
		font-size: .8rem;	
		text-align: start;  
	   }

	   .atualizacao{
		font-weight: 400;
		font-size: .7rem;
	   }
  }
</style>