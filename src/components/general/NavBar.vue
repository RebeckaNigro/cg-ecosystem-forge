<template>
	<nav class="navbar navbar-light">
		<div class="container-fluid">

			<MenuLateral/>
			
			<router-link class="navbar-brand ms-5" to="/">
				<img src="/colorful_logo.svg" alt="logo">
			</router-link>
			<section class="container-links dark-title">
				<router-link :to="link.path" v-for="link in maisInfos" class="navbar-text">{{ link.title
				}}</router-link>
			</section>
			<button type="button" class="light-title" id="login" @click="$router.push({ name: 'Login' })"
				v-if="!userStore.loggedUser.token">LOGIN</button>

			<div class="profile" v-else @click="handleProfileDropdown">
				<img src="/public/noticias/noticia-expandida/user.svg" alt="Ícone usuário">
				<span>{{ userStore.loggedUser.userName }}</span>
				<img src="/public/arrow_down_icon.svg" alt="Abrir opções">
				<div class="profile-dropdown" v-if="isProfileDropdownOpen">
					<span type="button" class="light-title" id="logout" @click="logout">Sair</span>
				</div>
			</div>


			<div class="dropdown dropstart dropdown-responsive">
				<button class="btn btn-secondary dropdown-toggle dropdown-toggle-responsive" type="button" id="menuInfos" data-bs-toggle="dropdown"
					aria-expanded="false">
					<div v-for="hamburguer in 3" class="hamburguer" />
				</button>
				<ul class="dropdown-menu" aria-labelledby="menuInfos">
					<li v-for="link in maisInfos">
						<router-link :to="link.path" class="dropdown-item">{{ link.title }}</router-link>
					</li>
				</ul>
			</div>
		</div>
	</nav>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import { RouterLink } from 'vue-router';
import router from '../../router';
import { useUserStore } from '../../stores/user/store';
import MenuLateral from './MenuLateral.vue';

defineProps<{
	isTransparent: Boolean
}>()

const isProfileDropdownOpen = ref(false)

const maisInfos = reactive([
	{
		title: 'QUEM SOMOS',
		path: '/quem-somos'
	},
	{
		title: 'PARCEIROS',
		path: '/parceiros'
	},
	{
		title: 'NOTÍCIAS',
		path: '/noticias'
	},
	{
		title: 'DOCUMENTOS & EDITAIS',
		path: '/documentos'
	},
	{
		title: 'AGENDA',
		path: '/eventos'
	},
	{
		title: 'FALE CONOSCO',
		path: '/fale-conosco'
	}
])

const userStore = useUserStore()

const logout = () => {
	userStore.logout()
	window.location.reload()
}

const handleProfileDropdown = () => {
	isProfileDropdownOpen.value = !isProfileDropdownOpen.value
}

onMounted(() => {
	// TODO: terminar centralização relativa
	const logo: HTMLElement = document.querySelector('.navbar-brand')!
	const logoWidth: number = logo.clientWidth

	const containerLinks: HTMLElement = document.querySelector('.container-links')!
	const containerCss = window.getComputedStyle(containerLinks)

	const mLeft: number = parseInt(containerCss.marginLeft)
	const fixedMargin: Number = new Number(mLeft - logoWidth)

	containerLinks.style.cssText = 'margin-left: ' + fixedMargin.toString() + ' !important';
})
</script>

<style scoped lang="scss">
nav.navbar {
	background: #EFEFEF;
	section.container-links {
		font-size: 1.2rem;
	}
}

.container-fluid {
	flex-wrap: nowrap !important;
}

.container-links {
	display: flex;
	flex-direction: row;
	margin: auto;
	flex-wrap: wrap;
}

.navbar-text {
	margin: auto 20px;
	text-decoration: none;
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


.hamburguer {
	height: 4px;
	width: 30px;
	background-color: gray;
	border-radius: 25px;
}

.dropdown-toggle-responsive {
	background: none;
	border: none;
	display: flex;
	flex-direction: column;
	height: 40px;
	justify-content: space-between;
	padding: 0;
	margin-right: 30px;
}


button#login {
	background-color: #639280;
	color: #fff;
	width: 120px;
	height: 40px;
	border-radius: 25px;
	font-size: 0.8rem;
	border: 0;
	font-family: 'Montserrat-Bold', sans-serif;
}

.profile {
	display: flex;
	justify-content: center;
	align-items: center;
	margin-right: 1rem;
	font-family: 'Montserrat-Medium', sans-serif;
	cursor: pointer;

	span {
		margin-left: .9rem;
		margin-right: .4rem;
	}

	.profile-dropdown {
		display: flex;
		position: absolute;
		top: 5.6rem;
		right: 1rem;
		flex-direction: column;
		justify-content: center;
		background: #EFEFEF;
		border: 1px solid #505050;
		border-top: none;
		border-radius: 0 0 10px 10px;
		z-index: 4;
		padding: .6rem 0;
		min-width: 180px;
		max-width: 300px;

		.profile-dropdown-text {
			text-decoration: none;
			color: #1E1E1E;
			font-family: 'Montserrat-Medium', sans-serif;
			margin-bottom: 1rem;
			padding-bottom: .6rem;
			border-bottom: 1px solid #1E1E1E;

		}

		span {
			color: #1E1E1E;
			font-size: 1rem;
			font-family: 'Montserrat-Medium', sans-serif;
		}
	}
}


@media (max-width: 991px) {
	.navbar-text {
		margin: 5px 10px;
		padding: 0;
	}

	nav.navbar {
		section.container-links {
			font-size: 1rem;
		}
	}
}

@media (max-width: 767px) {
	nav.navbar {
		section.container-links {
			font-size: 0.7rem;
		}
	}

	button#login {
		font-size: 0.6rem;
		width: 100px;
		height: 30px;
	}
}

@media (max-width: 485px) {
	.container-links {
		display: none;
	}

	a {
		img {
			width: 60px
		}
	}

	.dropdown-responsive {
		display: block;
	}

	.container-fluid {
		justify-content: space-between;
	}

	button#login {
		font-size: 0.5rem;
		width: 80px;
		height: 25px;
	}
}
</style>