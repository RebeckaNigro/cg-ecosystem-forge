<template>
  <div class="box mx-auto py-5 px-4 p-lg-5 container position-relative">
    <h1 class="dark-title fs-4 text-start">Eventos criados</h1>

    <button
      class="fab green-btn-primary"
      @click="$router.push({ name: 'GerenciaEvento' })"
    >
      + CRIAR NOVO EVENTO
    </button>

    <div class="row align-items-end mt-4 mb-5 justify-content-between px-2">
      <div class="col-xs-12 col-sm-6 col-md-5 col-lg-4 my-2">
        <!-- COMPONENTE DE FILTRO -->
        <FilterComponent
          field="data"
          :items="eventoStore.eventosUsuarioLogado"
          type="evento"
          @filter-result="filtrarEventos"
        />
      </div>

      <div class="col-xs-12 col-sm-6 col-md-5 col-lg-4 my-2">
        <!-- COMPONENTE DE BUSCA -->
        <SearchComponent
          :items="eventoStore.eventosUsuarioLogado"
          type="evento"
          @search-result="filtrarEventos"
        />
      </div>
    </div>

    <div class="container-fluid row justify-content-between g-0">
      <!-- LOADING SPINNER -->
      <Spinner v-if="loadingEvents" />

      <!-- CARD DE RASCUNHO -->
      <CardEventoCriado
        v-if="rascunho"
        :evento="rascunho"
        class="col-xs-12 col-sm-6 col-lg-4 p-2"
        @update-list="reloadEventos"
        :isRascunho="true"
      />

      <!-- CARDS DO USUÁRIO -->
      <CardEventoCriado
        v-for="(evento, index) in eventos"
        :key="index"
        :evento="evento"
        class="col-xs-12 col-sm-6 col-lg-4 p-2"
        @update-list="reloadEventos"
        :isRascunho="false"
      />
    </div>

    <div class="row container-fluid mb-3 mt-5">
      <div class="col-sm-6">
        <button
          class="green-btn-outlined button-specific"
          @click="$router.back()"
        >
          Voltar
        </button>
      </div>

      <div class="col-sm-6">
        <button
          class="green-btn-primary button-specific"
          @click="addEventsToView"
        >
          Ver mais
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { onMounted, reactive, ref } from "vue"
  import FilterComponent from "../../../../components/general/FilterComponent.vue"
  import SearchComponent from "../../../../components/general/SearchComponent.vue"
  import CardEventoCriado from "../../../../components/user/eventos/eventosCriados/CardEventoCriado.vue"
  import Spinner from "../../../../components/general/Spinner.vue"
  import { useEventoStore } from "../../../../stores/eventos/store"
  import {
    IEventoSimplificado,
    EventoSimplificado
  } from "../../../../stores/eventos/types"

  const eventoStore = useEventoStore()
  const page = ref(1)
  const loadingEvents = ref(false)
  let eventos = ref<Array<IEventoSimplificado>>()

  const rascunho = ref<EventoSimplificado>()

  const filtrarEventos = (eventosFiltrados: Array<IEventoSimplificado>) => {
    eventos.value = eventosFiltrados
  }

  const addEventsToView = async () => {
    page.value++
	loadingEvents.value = true
	await eventoStore.getUserEvents(page.value)
	eventos.value = eventoStore.eventosUsuarioLogado
	loadingEvents.value = false
  }

  const reloadEventos = async () => {
    loadingEvents.value = true
    if (!localStorage.getItem("eventoRascunho")) rascunho.value = undefined
    await eventoStore.getUserEvents(page.value)
    eventos.value = eventoStore.eventosUsuarioLogado
    loadingEvents.value = false
  }

  const verificaRascunho = () => {
    const eventoRascunhoStr = localStorage.getItem("eventoRascunho")

    if (eventoRascunhoStr) {
      const eventoRascunho = JSON.parse(eventoRascunhoStr)

      rascunho.value = new EventoSimplificado(
        eventoRascunho.evento.id,
        eventoRascunho.evento.tags,
        eventoRascunho.evento.titulo,
        eventoRascunho.evento.dataInicio,
        eventoRascunho.evento.dataTermino,
        eventoRascunho.evento.local,
        eventoRascunho.evento.arquivo
      )
    }
  }

  onMounted(async () => {
    loadingEvents.value = true
    await eventoStore.getUserEvents(page.value)
    eventos.value = eventoStore.eventosUsuarioLogado
    verificaRascunho()
    loadingEvents.value = false
	
  })
</script>

<style scoped lang="scss">

  .fab {
    position: absolute;
    top: -1.2rem;
    right: 3rem;
	width: 25%;
  }

  .button-specific {
    width: 100%;
    margin: 1rem 1rem;
    box-sizing: border-box;
  }

  @media screen and (min-width: 2550px) {
	.fab{
		width: 20%;
	}
  }

  @media screen and (max-width: 1200px) {
	.fab{
		width: 35%;
	}
  }

  @media screen and (max-width: 580px){
	.fab{
		width: 40%;
		font-size: 12px;
		right: 1rem;
	}
  }
</style>
