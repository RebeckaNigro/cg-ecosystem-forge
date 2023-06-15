<template>
	<footer class="container-fluid sticky-bottom justify-content-center">
		<div class="row align-items-center justify-content-center w-100">
			<div class="col-11 col-md-2 col-lg-auto ms-2 mx-lg-4 my-3 d-flex ">
				<img src="/logo_with_text.png" alt="white_logo" width="200" class="img-fluid" />
			</div>
	
			<div class="col-12 col-md-2 col-lg-auto ms-2 mx-lg-4 d-flex flex-column text-start text-lg-center">
				<a href="/politica-privacidade.pdf"
					class="light-title text-uppercase mb-3 fs-6">Termo de privacidade</a>
				<router-link class="light-title text-uppercase fs-6" to="/quem-somos">Quem somos</router-link>
			</div>
	
			<div class="col-12 col-md-2 col-lg-auto ms-2 mx-lg-4 text-start my-3 my-lg-0">
				<router-link class="light-title text-uppercase text-decoration-none fs-6" to="/parceiros">Parceiros</router-link>
				<ul class="p-0 light-body-text">
					<li v-for="item, index in info[0].infos" :key="index">
						{{ item }}
					</li>
				</ul>
			</div>
	
			<div class="col-12 col-md-2 col-lg-auto ms-2 mx-lg-4 text-start my-3 my-lg-0">                    
				<router-link class="light-title text-uppercase text-decoration-none fs-6" to="/documentos">Documentos</router-link>
				<ul class="p-0 light-body-text">
					<li v-for="item, index in info[1].infos" :key="index">
						{{ item }}
					</li>
				</ul>
			</div>
	
			<div class="col-12 col-md-3 col-lg-auto ms-2 mx-lg-4 text-start my-3 my-lg-0">
				<router-link class="light-title text-uppercase text-decoration-none fs-6" to="/fale-conosco">Fale conosco</router-link>
				<p class="light-body-text text-break">contato@ecossistemadeinovacaocg.com.br</p>
			</div>
		</div>

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
	min-height: 200px;
	height: auto;
	max-height: 600px;
	background-color: #28353e;
	padding: 0.5rem;
	display: flex;
	flex-direction: row;
	justify-content: center;
	align-items: center;

	a:hover{
		color: #e2e2e2;
	}

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


.light-body-text {
	font-size: 0.8rem;
}


@media screen and (min-width: 2550px) {
	.light-title {
		font-size: 1.4rem !important;
	}

	.light-body-text {
		font-size: 1rem;
	}

	.img-fluid{
		width: 90%;
	}
}

@media (max-width: 991px) {

	.light-title {
		font-size: 0.6rem !important;
	}

	.light-body-text {
		font-size: 0.7rem !important;
	}
	
}


</style>
