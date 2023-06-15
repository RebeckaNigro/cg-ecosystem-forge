<template>
	<div class="card-container">
		<div class="edital-info">
			<p>Nome e descrição do edital</p>
			<span>Atualizado em 01/01/2000 às 12:00</span>
		</div>
		<div class="ver-edital">
			<img src="../../../../public/view_icon.svg" alt="Visualizar edital">
		</div>
	</div>
</template>

<script setup lang="ts">
</script>

<style scoped lang="scss">
.card-container{
	background: #fff;
	border: 1px solid #6B6A64;
	box-shadow: 0px 0px 50px rgba(0, 0, 0, 0.1);
	display: flex;
	padding: 1rem;
	padding-right: 3rem;
	border-radius: 10px;
	text-align: start;
	justify-content: space-between;
	align-items: center;
	margin-bottom: 1.7rem;
	cursor: pointer;

	p{
		font-weight: 600;
		font-size: 1.2rem;
		margin-bottom: .3rem;
		
	}

	span{
		font-weight: 400;
		font-size: .8rem;
	}

	.edital-info{
		padding-left: .5rem;
	}

	.ver-edital{
		cursor: pointer;
	}

}	

.card-container:hover{
	p, span{
		text-decoration: underline;
	}
	.edital-info{
		border-left: 3px solid #6B6A64;
	}
}
</style>