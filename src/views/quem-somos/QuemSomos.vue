<template>
	<section class="quem-somos">
		<Banner path="/quem-somos/banner.png" figcaption="about-us" img-alt="sobre nós" :imgOverlay="false"
			maxHeight="650px">
			<ExternalHeader title="Ecossistema de inovação" subtitle="Campo Grande - MS"
				paragraph="Uma comunidade pronta para contribuir com o seu desenvolvimento!" />
		</Banner>

		<div class="container p-5 border-0">

			<h3 class="text text-uppercase">DE ONDE SURGIMOS</h3>

			<div class="row mt-5">
				<div class="col-md-6 d-flex flex-column justify-content-center ">
					<div class="d-flex flex-column ms-lg-5 ">

						<p class="text mb-2">
							&nbsp&nbsp&nbsp&nbsp&nbspO Ecossistema Local de Inovação - Campo Grande - MS, surge do desejo das instituições e unificar as competências tecnológicas de CAMPO GRANDE na direção do incentivo a inovação e tecnologia.
						</p>

						<p class="text ">
							&nbsp&nbsp&nbsp&nbsp&nbspFoi feito um estudo preliminar, para apurar o que temos de capacidade tecnológica e ciências para que essas capacidades sejam mais trabalhadas em prol do desenvolvimento socioeconômico de Campo Grande - MS.
						</p>

	
					</div>

				</div>

				<div class="col-md-6 d-flex">
					<img src="/quem-somos/more_about_us.png" alt="Foto ilustrativa" class="align-self-start img-fluid">
				</div>
			</div>

			<div class="row flex-row-reverse mt-5">
				<div class="col-md-6 d-flex flex-column justify-content-center ">
					<div class="d-flex flex-column justify-content-center ms-lg-5">

						<p class="text mb-2">
							&nbsp&nbsp&nbsp&nbsp&nbspA partir disso, foi identificado que existem empresas que ainda não se beneficiam da capacidade tecnológica das instituições de ciência, tecnologia e inovação. Ou seja, mapeamos uma distância que pode ser aproveitada! 
          
						</p>

						<p class="text ">
							&nbsp&nbsp&nbsp&nbsp&nbspUm grande grupo de apoio forma-se aqui para que as para que as ações sejam mais rápidas, efetivas e integradas em busca de melhorar a capacidade da inovação tecnológica e trazer o conhecimento para todos sobre as leis e benefícios proporcionados pela legislação.
						</p>

	
					</div>

				</div>

				<div class="col-md-6 d-flex">
					<img src="/quem-somos/our_story.png" alt="Foto ilustrativa" class="align-self-start img-fluid">
				</div>
			</div>
			
		</div>
		<div class="purpose-container">
			<div class="circle-top"></div>
			<div class="container">
				<h3 class="text text-uppercase mt-5">Nosso propósito</h3>
				<div class="row mt-4">
					<div class="col-md-6 d-flex flex-column justify-content-center">
						<div class="d-flex flex-column align-items-end me-lg-5 ">
		
							<p class="text mb-2">
								&nbsp&nbsp&nbsp&nbsp&nbspDisponibilizar caminhos norteadores para todos que desejam empreender, crescer e inovar, independente do formato ou tamanho do negócio. Nosso foco é nos tornarmos referência como polo científico e tecnológico para o Brasil.
							</p>
		
						</div>
		
					</div>
		
					<div class="col-md-6 d-flex">
						<img src="/quem-somos/purpose.png" alt="Foto ilustrativa" class="align-self-start img-fluid">
					</div>
				</div>
			</div>
		</div>



		<!--<GeneralContainer title="DE ONDE SURGIMOS" :darkTitle="true" :hasKnowMoreBtn="false" :contentArray="[
			'O Ecossistema Local de Inovação – Campo Grande – MS, surge do desejo das instituições e unificar as competências tecnológicas de CAMPO GRANDE na direção do incentivo a inovação e tecnologia. Foi feito um estudo preliminar, para apurar o que temos de capacidade tecnológica e ciências para que essas capacidades sejam mais trabalhadas em prol do desenvolvimento socioeconômico de Campo Grande - MS. ',
			'/quem-somos/more_about_us.png'
		]" bgColor="#fbfbfb" id="our-story" font-size="1.3rem" font-family="Montserrat-Medium" :has-contact-btn="false"
			:has-circle-top-border="false" :has-circle-bottom-border="false" />
		<GeneralContainer title="" :darkTitle="true" :hasKnowMoreBtn="false" :contentArray="[
			'/quem-somos/our_story.png',
			'A partir disso, foi identificado que existem empresas que ainda não se beneficiam da capacidade tecnológica das instituições de ciência, tecnologia e inovação. Ou seja, mapeamos uma distância que pode ser aproveitada! Um grande grupo de apoio forma-se aqui para que as para que as ações sejam mais rápidas, efetivas e integradas em busca de melhorar a capacidade da inovação tecnológica e trazer o conhecimento para todos sobre as leis e benefícios proporcionados pela legislação.'
		]" bgColor="#fbfbfb" id="more-about-us" font-size="1.3rem" font-family="Montserrat-Medium" :has-contact-btn="false"
			:has-circle-top-border="false" :has-circle-bottom-border="false" />

		<GeneralContainer title="NOSSO PROPÓSITO" :darkTitle="false" :hasKnowMoreBtn="false" :contentArray="[
			'Disponibilizar caminhos norteadores para todos que desejam empreender, crescer e inovar, independente do formato ou tamanho do negócio. Nosso foco é nos tornarmos referência como polo científico e tecnológico para o Brasil.',
			'/quem-somos/purpose.png',
		]" bgColor="linear-gradient(180deg, rgba(255, 255, 255, 0.2) 0%, rgba(0, 0, 0, 0.2) 100%), #639280" id="purpose"
			font-size="1.3rem" font-family="Montserrat-SemiBold" :has-contact-btn="false" :has-circle-top-border="true"
			:has-circle-bottom-border="false" />-->
	</section>
</template>

<script setup lang="ts">
import { reactive } from 'vue';
import Banner from '../../components/general/Banner.vue';
import GeneralContainer from '../../components/general/GeneralContainer.vue';
import ExternalHeader from '../../components/general/ExternalHeader.vue';

const ourStoryContentArray = reactive([
	'O Ecossistema Local de Inovação – Campo Grande – MS, surge do desejo das instituições e unificar as competências tecnológicas de CAMPO GRANDE na direção do incentivo a inovação e tecnologia. Foi feito um estudo preliminar, para apurar o que temos de capacidade tecnológica e ciências para que essas capacidades sejam mais trabalhadas em prol do desenvolvimento socioeconômico de Campo Grande - MS. ',
	'/quem-somos/our_story.png'
])
const moreAboutUs = reactive([
	'/quem-somos/more_about_us.png',
	'A partir disso, foi identificado que existem empresas que ainda não se beneficiam da capacidade tecnológica das instituições de ciência, tecnologia e inovação. Ou seja, mapeamos uma distância que pode ser aproveitada! Um grande grupo de apoio forma-se aqui para que as para que as ações sejam mais rápidas, efetivas e integradas em busca de melhorar a capacidade da inovação tecnológica e trazer o conhecimento para todos sobre as leis e benefícios proporcionados pela legislação.'
])
</script>

<style scoped lang="scss">
section.quem-somos {
	background: #fbfbfb;

	h3.text{
		font-weight: 600;
		font-size: 28px;
	}
	p.text{
		font-weight: 500;
		max-width: 460px;
		text-align: justify;
		font-size: 18px;
	}

	img{
		filter: drop-shadow(0px 0px 35px rgba(0, 0, 0, 0.15));
	}

	.purpose-container{
		background: linear-gradient(180deg, rgba(255, 255, 255, 0.2) 0%, rgba(0, 0, 0, 0.2) 100%), #639280;
		color: #fff;

		h3{
			font-weight: 700;
		}

		p{
			font-weight: 600;
		}
		.circle-top {
			height: 25px;
			width: 50px;
			border-bottom-left-radius: 8rem;
			border-bottom-right-radius: 8rem;
			margin: 0 auto;
			position: relative;
			top: 0;
			background: #fbfbfb;
		}

	}
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


/* MONITORES 4K E AFINS  */
@media screen and (min-width: 2550px) {
	section.quem-somos {
		h3.text{
			font-size: 34px;
		}
		p.text{
			font-size: 20px;
		}
	}
}

/* TABLETS */
@media screen and (max-width: 1200px) {
	section.quem-somos {
		h3.text{
		
			font-size: 22px;
		}
		p.text{
			font-size: 13px;
		}

		.purpose-container{
			padding: 0 10px 10px 10px;
		}
	}
}

/* CELULARES */
@media(max-width: 580px) {
	section.quem-somos {
		h3.text{
			font-size: 22px;
		}
		p.text{
			font-size: 15px;
		}
	}
}
</style>