import { createWebHistory, createRouter, RouteRecordRaw } from 'vue-router';
import { useUserStore } from '../stores/user/store';
import Home from '../views/Home.vue';
import FaleConosco from '../views/fale-conosco/FaleConosco.vue';
import Parceiros from '../views/parceiros/Parceiros.vue';
import ParceiroExpandido from '../views/parceiros/expandido/ParceiroExpandido.vue';
import QuemSomos from '../views/quem-somos/QuemSomos.vue';
import Eventos from '../views/eventos/Eventos.vue';
import EventoExpandido from '../views/eventos/expandido/EventoExpandido.vue'
import Noticias from '../views/noticias/Noticias.vue';
import NoticiaExpandida from '../views/noticias/expandida/NoticiaExpandida.vue';
import Documentos from '../views/documentos/Documentos.vue';
import DocumentosPesquisa from '../views/documentos/pesquisa/DocumentosPesquisa.vue'
import Login from '../views/login/Login.vue'
import UserHome from '../views/user/UserHome.vue'
import GerenciaNoticia from '../views/user/noticias/GerenciaNoticia.vue'
import GerenciaEvento from '../views/user/eventos/GerenciaEvento.vue'

const routes: RouteRecordRaw[] = [
	{
		path: '/',
		name: 'Home',
		component: Home,
    meta: {
      public: true
    }
	},
	{
		path: '/fale-conosco',
		name: 'FaleConosco',
		component: FaleConosco,
    meta: {
      public: true
    }
	},
	{
		path: '/parceiros',
		name: 'Parceiros',
		component: Parceiros,
    meta: {
      public: true
    }
	},
	{
		path: '/parceiros/:parceiroId',
		name: 'ParceiroExpandido',
		component: ParceiroExpandido,
    meta: {
      public: true
    }
	},
	{
		path: '/quem-somos',
		name: 'QuemSomos',
		component: QuemSomos,
    meta: {
      public: true
    }
	},
	{
		path: '/eventos',
		name: 'Eventos',
		component: Eventos,
    meta: {
      public: true
    }
	},
  {
    path: '/eventos/:eventoId',
    name: 'EventoExpandido',
    component: EventoExpandido,
    meta: {
      public: true
    }
  },
	{
		path: '/noticias',
		name: 'Noticias',
		component: Noticias,
    meta: {
      public: true
    }
	},
	{
		path: '/noticias/:noticiaId',
		name: 'NoticiaExpandida',
		component: NoticiaExpandida,
    meta: {
      public: true
    }
	},
	{
		path: '/documentos',
		name: 'Documentos',
		component: Documentos,
    meta: {
      public: true
    }
	},
  {
    path: '/documentos/:tipoDocumento',
    name: 'DocumentosPesquisa',
    component: DocumentosPesquisa,
    meta: {
      public: true
    }
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
    meta: {
      public: true
    }
  },
  {
    path: '/user/Home',
    name: 'UserHome',
    component: UserHome
  },
  {
    path: '/user/noticias',
    name: 'GerenciaNoticia',
    component: GerenciaNoticia
  },
  {
    path: '/user/eventos',
    name: 'GerenciaEvento',
    component: GerenciaEvento
  }
];

const router = createRouter({
	history: createWebHistory(),
	routes,
});

router.beforeEach((routeTo) => {
  const userStore = useUserStore()
  if (!routeTo.meta.public && !userStore.loggedUser.token && routeTo.name !== 'Login') {
    return { name: 'Login'}
  }
})

export default router;
