<template>
	<NavBar :is-transparent="true" />
	<section class="container-fluid">
		<main>
			<Banner
				path="/noticias/banner.png"
				figcaption="workspace_image"
				img-alt="workspace_image"
			>
				<div class="banner-noticia-content ghp">
					<img src="/noticias/icone-noticia.png" alt="icon">
					<div>
						<h1 class="dark-title">Notícias</h1>
						<p class="dark-body-text">Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quia saepe ex alias consectetur, nulla, in neque iure praesentium, modi eveniet consequuntur quaerat dolorum officiis vel. Dolores nihil quo eveniet vel.</p>
					</div>
				</div>
			</Banner>

			<ContainerCardsNoticia/>
		</main>
	</section>
	<FooterComponent />
</template>

<script setup lang="ts">
import Banner from '../../components/general/Banner.vue';
import ContainerCardsNoticia from '../../components/noticias/ContainerCardsNoticia.vue';
</script>

<style scoped lang="scss">
section.container-fluid {
	padding: 0;
	margin-bottom: 40px;
}

.banner-noticia-content {
	position: absolute;
	top: 0;
	width: 100%;
	height: 100%;
	padding-left: 64px;

	display: flex;
	align-items: center;

  .dark-title {
    font-size: 2.5rem;
  }

	> img {
		width: 10%;
		min-width: 60px;
		max-width: 128px;
		margin-right: 24px;
	}

	> div {
		display: flex;
		flex-direction: column;
		align-items: flex-start;
		width: 100%;
    max-width: 500px;

		p {
			text-align: start;
			font-size: 1rem;
      max-height: 100px;
      height: 100%;
      overflow-y: auto;
		}
	}
}
@media (max-width: 1200px) {
  .banner-noticia-content {
    .dark-title {
      font-size: 2rem;
    }
    .dark-body-text {
      font-size: 0.8rem;
    }
  }
}
  @media (max-width: 991px) {
  .banner-noticia-content {
    .dark-title {
      margin-bottom: 0;
      font-size: 1.5rem;
    }
    > div {
      max-width: 300px;
      p {
        max-height: 60px;
        margin-bottom: 0;
      }
    }
  }
  }
  @media (max-width: 768px) {
  .banner-noticia-content {
    .dark-title {
      font-size: 1rem;
    }
    > div {
      max-width: 200px;
      p {
        max-height: 40px;
      }
    }
    .dark-body-text {
      font-size: 0.7rem;
    }
  }
  }
  @media (max-width: 576px) {
    .banner-noticia-content {
      .dark-title {
        font-size: 0.8rem;
      }
      .dark-body-text {
        font-size: 0.8rem;
      }
    }
  }
</style>
