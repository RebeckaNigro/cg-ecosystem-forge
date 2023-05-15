<template>
	<footer class="container-fluid sticky-bottom justify-content-between">
		
			<div class="mx-4 my-3">
				<img src="/logo_with_text.png" alt="white_logo" width="200" />
			</div>
	
			<div class="d-flex flex-column text-start">
				<a href="/privacidade_ecossistemacg.pdf" download="POLITICA DE PRIVACIDADE ECOSSISTEMA"
					class="light-title text-uppercase mb-3 fs-6">Termo de privacidade</a>
				<router-link class="light-title text-uppercase fs-6" to="/quem-somos">Quem somos</router-link>
			</div>
	
			<div class="text-start mx-4">
				<router-link class="light-title text-uppercase text-decoration-none fs-6" to="/parceiros">Parceiros</router-link>
				<ul class="p-0 light-body-text">
					<li v-for="item, index in info[0].infos" :key="index">
						{{ item }}
					</li>
				</ul>
			</div>
	
			<div class="text-start mx-4">                    
				<router-link class="light-title text-uppercase text-decoration-none fs-6" to="/documentos">Documentos</router-link>
				<ul class="p-0 light-body-text">
					<li v-for="item, index in info[1].infos" :key="index">
						{{ item }}
					</li>
				</ul>
			</div>
	
			<div class="text-start mx-4">
				<router-link class="light-title text-uppercase text-decoration-none fs-6" to="/fale-conosco">Fale conosco</router-link>
				<p class="light-body-text">contato@ecossistemadeinovacaocg.com.br</p>
			</div>

		<!--<div class="img-container">
      <img src="/logo_with_text.png" alt="white_logo" width="50" />
      <div class="ecossistema-contato d-lg-none text-start" id="below-991">
        <router-link to="/fale-conosco" class="light-title"
          >FALE CONOSCO</router-link
        >
        <h5 class="light-body-text">Contato:</h5>
        <p class="light-body-text">contato@ecossistemadeinovacaocg.com.br</p>
      </div>
      
    </div>
    <nav class="container-list d-flex align-items-center justify-content-between w-100">

		<div class="d-flex flex-column">
			<a
			  href="/privacidade_ecossistemacg.pdf"
			  download="POLITICA DE PRIVACIDADE ECOSSISTEMA"
			  class="light-title d-lg-show text-uppercase mb-3"
			  >Termo de privacidade</a
			>
			<router-link class="light-title d-lg-show text-uppercase" to="/quem-somos"
			  >Quem somos</router-link
			>
		</div>
      <div class="d-lg-none d-flex">
        <a
          href="/privacidade_ecossistemacg.pdf"
          download="POLITICA DE PRIVACIDADE ECOSSISTEMA"
          class="light-title"
          >Termo de privacidade</a
        >
        <router-link class="light-title" to="/quem-somos"
          >Quem somos</router-link
        >
        <div class="vertical-container-list d-md-none">
          <ul id="opens">
            <li
              class="light-body-text"
              v-for="(item, itemIndex) in info"
              :key="itemIndex"
            >
              <span
                :id="'topico' + itemIndex"
                class="light-title"
                @click="showItems(itemIndex)"
                >{{ item.title }}</span
              >
            </li>
          </ul>
          <div v-for="(item, index) in info.length">
            <ul :id="'hidden-' + index" v-if="selectedItem === item - 1">
              <li
                v-for="infoItens in info[selectedItem].infos"
                class="light-body-text"
              >
                {{ infoItens }}
              </li>
            </ul>
          </div>
        </div>
      </div>
      <ul id="regular" v-for="(item, itemIndex) in info" :key="itemIndex">
        <span class="light-title">{{ item.title }}</span>
        <li
          class="light-body-text"
          v-for="(info, infoIndex) in item.infos"
          :key="infoIndex"
        >
          {{ info }}
        </li>
      </ul>
      <div class="ecossistema-contato" id="hides-in-911">
        <router-link to="/fale-conosco" class="light-title"
          >FALE CONOSCO</router-link
        >
        <h5 class="light-body-text">Contato:</h5>
        <p class="light-body-text">startupsuporte@sesims.com.br</p>
      </div>
    </nav>-->
	</footer>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from "vue"

const selectedItem = ref(0)
const menusFooter = reactive(new Array())

/*function showItems(index: number) {
	selectedItem.value = index

	menusFooter.forEach((item) => {
		// Itera sobre o menu de itens para remover o highlight de outro item e reforçar o item clicado
		if (item.classList.contains("highlight")) {
			item.classList.remove("highlight")
		}
		if (item.id === "topico" + index) {
			item.classList.add("highlight")
		}
	})
}*/

/*onMounted(() => {
	// popula uma array com os elementos do footer para mudar seu background, indicando ao usuário onde clicou
	for (const item in info) {
		const el: HTMLElement = document.querySelector("#topico" + item)!

		if (parseInt(item) === 0) {
			el.classList.add("highlight")
		}

		menusFooter.push(el)
	}
})*/

const info = [
	{
		title: "PARCEIROS",
		infos: [
			"ICTIs",
			"Empresas",
			"Mecanismos de Inovação",
			"Governo",
			"Sociedade Organizada"
		]
	},
	{
		title: "DOCUMENTOS",
		infos: ["Pesquisas", "Leis", "Editais"]
	}
]
</script>

<style scoped lang="scss">
footer {
	background-color: #28353e;
	padding: 0.5rem;
	display: flex;
	flex-direction: row;
	justify-content: center;
	align-items: center;
}


ul {
	margin-bottom: 0;
	list-style: none;
	text-align: left;

	span,
	li {
		color: #fff;
	}
}

.container-list {
	max-width: 1500px;

	a,
	ul {
		flex: 1 1 auto;
	}

	.ecossistema-contato {
		h5 {
			font-size: 1rem;
		}

		h5,
		p {
			margin-top: 1rem;
			margin-bottom: 0;
		}
	}
}

.light-title {
	font-size: 1rem !important;
}

.img-container {
	height: 80px;
	display: flex;
	align-items: center;
	justify-content: center;

	img {
		width: 50%;
		min-width: 165px;
	}
}

.social {
	display: flex;
	flex-direction: column;
	justify-content: flex-start;

	li {
		text-align: start;
	}

	a {
		img {
			width: 20%;
			min-width: 25px;
		}
	}

	a:nth-child(2n) {
		margin: 0 10px;
	}
}

.light-body-text {
	font-size: 0.8rem;
}

.light-title {
	font-size: 0.6rem;
}

.vertical-container-list {
	display: flex;
	position: relative;
	height: 70px;

	ul#opens {
		height: 40px;

		li {
			text-align: start;
			margin: 5px 0;

			span:hover {
				cursor: pointer;
			}
		}
	}

	ul#hidden-0 {
		position: absolute;
		top: -20px;
		right: -150px;
	}

	ul#hidden-1 {
		position: absolute;
		top: -20px;
		right: -70px;
	}
}

.highlight {
	background: radial-gradient(#e66465, #28353e);
}

#below-991 {}

#hides-in-911 {}

@media (max-width: 1068px) {
	.img-container {
		width: 200px;
	}
}

@media (max-width: 991px) {
	.img-container {
		padding-right: 20px;
		display: flex;
		flex-direction: column;
		width: fit-content;
		align-items: flex-start;
	}

	.d-lg-show {
		display: none;
	}

	.container-list {
		align-items: flex-start;
	}

	.d-lg-none {
		flex-direction: column;
		align-items: flex-start;
		padding-left: 10px;
		height: 120px;

		a {
			margin-top: 5px;
		}
	}

	.light-title {
		font-size: 0.6rem !important;
	}

	.light-body-text {
		font-size: 0.7rem !important;
	}

	ul {
		padding-left: 5px;
	}

	ul#regular {
		max-width: 150px;
	}

	#hides-in-911 {
		display: none;
	}

	/* ul.social {
      display: none;
    }
    div.social {
      flex-direction: row;
      margin-top: 20px;
      a:nth-child(2n) {
        margin: 0;
      }
    } */
}

@media (max-width: 768px) {

	/* .container-list {
      display: none;
    } */
	ul#regular {
		display: none;
	}

	ul#opens {
		padding: 0;
	}
}

@media (max-width: 576px) {

	footer,
	.container-list {
		flex-direction: column;
		align-items: center;
	}

	footer {
		//height: 270px;
		justify-content: space-around;
	}

	.img-container {
		border-right: none;
		padding: 0;
		align-items: center;
		height: 120px;
	}

	.img-container,
	.container-list {
		max-width: 350px;
		width: 100%;
		align-items: flex-start !important;
	}

	.vertical-container-list {
		margin-top: 20px;
	}
}</style>
