<template>
  <div class="pb-5 pt-5">
    <div class="breadcrumb-container">
      <nav style="--bs-breadcrumb-divider: '>'" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li
            class="breadcrumb-item unactive"
            @click="$router.push({ name: 'DocumentosCriados' })"
          >
            Documentos
          </li>
          <li
            v-if="!documentoId"
            class="breadcrumb-item active"
            aria-current="page"
          >
            Enviar Documento
          </li>
          <li v-else class="breadcrumb-item active" aria-current="page">
            Editar Documento
          </li>
        </ol>
      </nav>
    </div>

    <div class="pb-5">
      <div v-if="documentoId">
        <FormDocumento :documento-id="documentoId" />
      </div>
      <div v-else>
        <FormDocumento />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { onMounted, ref } from "vue"
  import FormDocumento from "../../../components/user/documentos/FormDocumento.vue"
  import { useRoute } from "vue-router"

  const route = useRoute()
  const documentoId = ref<number | null>(null)

  onMounted(() => {
    const id = route.params.documentoId

    if (id) {
      documentoId.value = Number(id)
    }
  })
</script>

<style scoped></style>
