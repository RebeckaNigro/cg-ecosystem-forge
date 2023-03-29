<template>
  <div class="pb-5 pt-5">
    <div class="breadcrumb-container">
      <nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li
            class="breadcrumb-item unactive"
            @click="$router.push({ name: 'EventosCriados' })"
          >
            Eventos
          </li>
          <li
            v-if="!eventoId"
            class="breadcrumb-item active"
            aria-current="page"
          >
            Criar Evento
          </li>
          <li v-else class="breadcrumb-item active" aria-current="page">
            Editar Evento
          </li>
        </ol>
      </nav>
    </div>

    <div class="pb-5">
      <div v-if="eventoId">
        <FormEvento :evento-id="eventoId" />
      </div>
      <div v-else>
        <FormEvento />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { onMounted, ref } from "vue"
  import FormEvento from "../../../components/user/eventos/FormEvento.vue"
  import { useRoute } from "vue-router"

  const route = useRoute()
  const eventoId = ref<number | null>(null)

  onMounted(() => {
    const id = route.params.eventoId

    if (id) {
      eventoId.value = Number(id)
    }
  })
</script>

<style scoped></style>
