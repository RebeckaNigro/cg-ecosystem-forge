import { createWebHistory, createRouter, RouteRecordRaw } from 'vue-router';
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

const routes: RouteRecordRaw[] = [
	{
		path: '/',
		name: 'Home',
		component: Home,
	},
	{
		path: '/fale-conosco',
		name: 'FaleConosco',
		component: FaleConosco,
	},
	{
		path: '/parceiros',
		name: 'Parceiros',
		component: Parceiros,
	},
	{
		path: '/parceiros/:parceiroId',
		name: 'ParceiroExpandido',
		component: ParceiroExpandido,
	},
	{
		path: '/quem-somos',
		name: 'QuemSomos',
		component: QuemSomos,
	},
	{
		path: '/eventos',
		name: 'Eventos',
		component: Eventos,
	},
  {
    path: '/eventos/:eventoId',
    name: 'EventoExpandido',
    component: EventoExpandido
  },
	{
		path: '/noticias',
		name: 'Noticias',
		component: Noticias,
	},
	{
		path: '/noticias/:noticiaId',
		name: 'NoticiaExpandida',
		component: NoticiaExpandida,
	},
	{
		path: '/documentos',
		name: 'Documentos',
		component: Documentos,
	},
  {
    path: '/documentos/:tipoDocumento',
    name: 'DocumentosPesquisa',
    component: DocumentosPesquisa
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
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
  }
];

const router = createRouter({
	history: createWebHistory(),
	routes,
});

export default router;
