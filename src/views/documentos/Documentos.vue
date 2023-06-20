<template>
	<section class="container-fluid">
		<main>
			<Banner
				path="/documentos/banner.png"
				pixels-bg-height="710px"
			>
				<ExternalHeader
					title="Documentos & editais"
					paragraph="Tenha acesso aos principais documentos disponibilizados pelo Ecossistema Local de Inovação - Campo Grande - MS"
				/>
				
			</Banner>

			<div class="badge bg-success text-wrap fs-6 my-4" style="width: 8rem;">
				<span>Em breve.</span>
			</div>

			<!--<div class="destaques-content ghp">
				<ContainerDestaques />
			</div>

			<div class="documentos-content">
				<h1 class="dark-title">SESSÕES</h1>

				<div class="card-documentos-container ghp">
					<CardSessao
						title="Pesquisas"
						text="Em breve."
						:has-icon="true"
						iconPath="../../public/search_icon_2.svg"
						:has-download="false"
						content-to-download="none"
					/>
						<CardSessao
						title="Editais"
						text="Em breve."
						:has-icon="true"
						iconPath="../../public/list_icon.svg"
						:has-download="false"
						content-to-download="none"
					/>
						<CardSessao
						title="Leis"
						text="Leis que apoiam o desenvolvimento e inovação em Campo Grande e Mato Grosso do Sul."
						:has-icon="true"
						iconPath=".././public/paper_fold_icon.svg"
						:has-download="false"
						content-to-download="none"
					/>
				</div>
			</div>-->
		</main>
	</section>
</template>

<script setup lang="ts">
import Banner from '../../components/general/Banner.vue';
import CardSessao from '../../components/documentos/CardSessao.vue';
import ContainerDestaques from '../../components/documentos/ContainerDestaques.vue'
import ExternalHeader from '../../components/general/ExternalHeader.vue';

</script>

<style scoped lang="scss">
section.container-fluid {
	padding: 0;
}

.destaques-content {
	width: 100%;
	padding-bottom: 24px;
	background: #FBFBFB
;

	> h1 {
		font-weight: bold !important;
		text-transform: uppercase;
	}
}

.documentos-content {
  width: 100%;

  padding: 2rem 0;
  background: linear-gradient(0deg, rgba(0, 0, 0, 0.2) 0%, rgba(255, 255, 255, 0.2) 100%), #F8F9FA;

	> h1 {
		font-weight: bold !important;
		text-transform: uppercase;
    	font-size: 1.5rem;
	}
}

.card-documentos-container {
	display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: space-between;
  margin-top: 2rem;
}
@media (max-width: 1045px) {
  .card-documentos-container {
    justify-content: space-around;
  }
}
@media (max-width: 576px) {
 
  .noticias-content {
    > h1 {
      font-size: 1.2rem;
    }
  }
}
</style>
