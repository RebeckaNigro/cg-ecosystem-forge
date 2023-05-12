<template>
	<nav class="navbar navbar-light">
		<div class="container-fluid flex-nowrap">
			<div class="d-flex align-items-center">
				<MenuLateral />

				<router-link class="navbar-brand" to="/">
					<img src="/colorful_logo.svg" alt="logo" />
				</router-link>
			</div>
			<section class="container-links d-none d-lg-flex fs-5 m-auto flex-wrap dark-title">
				<router-link :to="link.path" v-for="link in maisInfos"
					class="navbar-text my-auto mx-lg-3 text-decoration-none">{{ link.title }}</router-link>
			</section>

			<div class="d-flex align-items-center">
				<button type="button" class="light-title  me-3" id="login" @click="$router.push({ name: 'Login' })"
					v-if="!userStore.loggedUser.token">
					LOGIN
				</button>
				<!--<div class="profile dropdown dropdown-center dropdown-responsive" v-if="userStore.loggedUser.token">
					<div class="dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false" id="profile">
						<img src="/public/noticias/noticia-expandida/user.svg" alt="Ícone usuário" />
						<span>{{ userStore.loggedUser.userName }}</span>
						<img src="/public/arrow_down_icon.svg" alt="Abrir opções" />
					</div>
					<ul class="dropdown-menu profile-dropdown" aria-labelledby="profile">
						<li class="light-title" id="logout" @click="logout">
							Sair
						</li>
					</ul>
					<div class="profile-dropdown" v-if="isProfileDropdownOpen">
						<span type="button" class="light-title" id="logout" @click="logout">Sair</span>
					</div>
				</div>-->

				<div class="dropdown dropdown-center" v-if="userStore.loggedUser.token" id="profile">
					<button class="border-0 dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false"
						>
						<img src="/public/noticias/noticia-expandida/user.svg" alt="Ícone usuário" />
						<span class="px-2">{{ userStore.loggedUser.userName }}</span>
						<img src="/public/arrow_down_icon.svg" alt="Abrir opções" />
					</button>
					<ul class="dropdown-menu" aria-labelledby="profile">

						<li class="ps-4" id="logout" @click="logout">
							<span>Sair</span>
						</li>


					</ul>
				</div>

				<div class="dropdown dropstart dropdown-responsive">
					<button class="btn btn-secondary dropdown-toggle dropdown-toggle-responsive" type="button"
						id="menuInfos" data-bs-toggle="dropdown" aria-expanded="false">
					</button>
					<ul class="dropdown-menu" aria-labelledby="menuInfos">
						<li v-for="link in maisInfos">
							<router-link :to="link.path" class="dropdown-item">{{
								link.title
							}}</router-link>
						</li>
					</ul>
				</div>
			</div>
		</div>
	</nav>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from "vue"
import { RouterLink, useRouter } from "vue-router"
import { useUserStore } from "../../stores/user/store"
import MenuLateral from "./MenuLateral.vue"

const isProfileDropdownOpen = ref(false)

const maisInfos = reactive([
	{
		title: "QUEM SOMOS",
		path: "/quem-somos"
	},
	{
		title: "PARCEIROS",
		path: "/parceiros"
	},
	{
		title: "NOTÍCIAS",
		path: "/noticias"
	},
	{
		title: "DOCUMENTOS & EDITAIS",
		path: "/documentos"
	},
	{
		title: "AGENDA",
		path: "/eventos"
	},
	{
		title: "FALE CONOSCO",
		path: "/fale-conosco"
	}
])

const userStore = useUserStore()
const router = useRouter()

const logout = () => {
	userStore.logout()
	router.push({ name: "Login" })
}

const handleProfileDropdown = () => {
	isProfileDropdownOpen.value = !isProfileDropdownOpen.value
}

onMounted(() => {
	// TODO: terminar centralização relativa
	const logo: HTMLElement = document.querySelector(".navbar-brand")!
	const logoWidth: number = logo.clientWidth

	const containerLinks: HTMLElement =
		document.querySelector(".container-links")!
	const containerCss = window.getComputedStyle(containerLinks)

	const mLeft: number = parseInt(containerCss.marginLeft)
	const fixedMargin: Number = new Number(mLeft - logoWidth)

	containerLinks.style.cssText =
		"margin-left: " + fixedMargin.toString() + " !important"
})
</script>

<style scoped lang="scss">
nav.navbar {
	background: #efefef;
}

a {
	img {
		width: 70%;
		min-width: 60px;
	}
}


.dropdown-responsive {
	display: none;
}
#profile {
	.dropdown-menu{
		
		background-color: #efefef;
		font-family: "Montserrat-Medium", sans-serif;
		border: 1px solid #505050;
		border-top: none;
		border-radius: 0 0 10px 10px;
		padding: 0.6rem 0;
		list-style: none;
		margin-left: .8rem;
	}
}
.dropdown-toggle-responsive {
	background: url('/public/menu_icon.svg') no-repeat;
	border: none;
	display: flex;
	height: 24px;
	width: 24px;
	padding: 0;
	margin-right: 30px;
}


.dropstart .dropdown-toggle::before,
.dropdown-toggle::after {
	display: none;
}


button#login {
	background-color: #639280;
	color: #fff;
	width: 120px;
	height: 40px;
	border-radius: 25px;
	font-size: 0.8rem;
	border: 0;
	font-family: "Montserrat-Bold", sans-serif;
}

.profile {
	display: flex;
	justify-content: center;
	align-items: center;
	margin-right: 1rem;
	font-family: "Montserrat-Medium", sans-serif;
	cursor: pointer;

	span {
		margin-left: 0.9rem;
		margin-right: 0.4rem;
	}


}

.profile-dropdown {
	display: flex;
	position: absolute;
	top: 5.6rem;
	right: 1rem;
	flex-direction: column;
	justify-content: center;
	background: #efefef;
	font-family: "Montserrat-Medium", sans-serif;
	border: 1px solid #505050;
	border-top: none;
	border-radius: 0 0 10px 10px;
	z-index: 4;
	padding: 0.6rem 0;
	min-width: 180px;
	max-width: 300px;

	.profile-dropdown-text {
		text-decoration: none;
		color: #1e1e1e;
		font-family: "Montserrat-Medium", sans-serif;
		margin-bottom: 1rem;
		padding-bottom: 0.6rem;
		border-bottom: 1px solid #1e1e1e;
	}

	span {
		color: #1e1e1e;
		font-size: 1rem;
		font-family: "Montserrat-Medium", sans-serif;
	}
}

@media (max-width: 767px) {


	button#login {
		font-size: 0.6rem;
		width: 100px;
		height: 30px;
	}
}

@media (max-width: 1024px) {

	a {
		img {
			width: 60px;
		}
	}

	.dropdown-responsive {
		display: block;
	}


	button#login {
		font-size: 0.5rem;
		width: 80px;
		height: 25px;
	}
}
</style>
