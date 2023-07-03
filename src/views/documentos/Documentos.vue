<template>
	<section>
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

			<div class="destaques-content container">
				<ContainerDestaques />
			</div>

			<div class="documentos-content w-100">
				<h1 class="font-semibold">SESSÕES</h1>

				<div class="row mt-4 justify-content-center gap-5">
					<CardSessao
						title="Pesquisas"
						text="Em breve."
						:has-icon="true"
						iconPath="../../public/search_icon_2.svg"
						:has-download="false"
						content-to-download="none"
						class="col-9 col-md-7 col-lg-3"
					/>
						<CardSessao
						title="Editais"
						text="Em breve."
						:has-icon="true"
						iconPath="../../public/list_icon.svg"
						:has-download="false"
						content-to-download="none"
						class="col-9 col-md-7 col-lg-3"
					/>
						<CardSessao
						title="Leis"
						text="Leis que apoiam o desenvolvimento e inovação em Campo Grande e Mato Grosso do Sul."
						:has-icon="true"
						iconPath=".././public/paper_fold_icon.svg"
						:has-download="false"
						content-to-download="none"
						class="col-9 col-md-7 col-lg-3"
					/>
				</div>
			</div>
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
.destaques-content {
	padding-bottom: 24px;
	background: #FBFBFB;
}

.documentos-content {
  padding: 2rem 0;
  background: linear-gradient(0deg, rgba(0, 0, 0, 0.2) 0%, rgba(255, 255, 255, 0.2) 100%), #F8F9FA;

	h1{
		color: #1E1E1E;
		font-size: 28px;	
	}
}

</style>
