<template>
  <div class="mx-auto box p-5 mb-5">
    <div class="text-center">
      <h1 class="fs-5 fw-bold">Últimos documentos enviados</h1>
    </div>

    <Spinner v-if="loadingDocs" />

    <div class="container-fluid">
      <div
        class="custom-list-item"
        v-for="documento in documentoStore.allUserLastDocs"
        :key="documento.id"
      >
        <div class="row g-0 px-2">
          <div
            class="col-lg-10 fw-bold d-flex align-items-center py-2 hover-pointer"
            @click="
              $router.push({
                name: 'DocumentoExpandido',
                params: { documentoId: documento.id }
              })
            "
          >
            {{ documento.nome }}
          </div>
          <div
            class="col-lg-2 d-flex align-items-center justify-content-end left-separator"
          >
            <div class="container-normal">
              <img
                src="/public/edit_icon.svg"
                alt=""
                class="image-icon-button hover-pointer"
                @click="
                  $router.push({
                    name: 'GerenciaDocumento',
                    params: { documentoId: documento.id }
                  })
                "
              />
              <img
                src="/public/delete_icon.svg"
                alt=""
                class="image-icon-button hover-pointer"
                @click="confirmDelete(documento.id)"
              />
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="text-center row container-fluid mt-5 gy-2">
      <div class="col-lg-6">
        <button
          class="green-btn-outlined"
          @click="
            $router.push({
              name: 'DocumentosCriados'
            })
          "
        >
          VER MAIS
        </button>
      </div>
      <div class="col-lg-6">
        <button
          class="green-btn-primary"
          @click="$router.push({ name: 'GerenciaDocumento' })"
        >
          CRIAR DOCUMENTO
        </button>
      </div>
    </div>
  </div>

  <ConfirmModal
    v-show="confirmStore.visible"
    @confirm-true="confirmado"
    element-id="confirmModal"
    id="confirmModal"
  />
</template>

<script setup lang="ts">
  import { onMounted, ref } from "vue"
  import Spinner from "../../general/Spinner.vue"
  import ConfirmModal from "../../general/ConfirmModal.vue"
  import { useDocumentStore } from "../../../stores/documentos/store"
  import { useConfirmStore } from "../../../stores/confirm/store"
  import { useModalStore } from "../../../stores/modal/store"

  const documentoStore = useDocumentStore()
  const confirmStore = useConfirmStore()
  const modalStore = useModalStore()
  const loadingDocs = ref(false)

  const confirmDelete = (id: number) => {
    confirmStore.showConfirmModal(
      "Tem certeza que desesja remover este documento?",
      id
    )
  }

  const confirmado = async () => {
    confirmStore.closeConfirm()
	
	if(confirmStore.options.parameter != null){
		await documentoStore.deleteDoc(confirmStore.options.parameter as number)
		const res = documentoStore.response.getResponse()
		
		if (res.code === 200) {
			loadLastDocs()
			modalStore.showSuccessModal("Documento removido com sucesso!")
		} else {
			modalStore.showErrorModal("Erro ao remover documento!")
		}
	}

	
  }

  const loadLastDocs = async () => {
    loadingDocs.value = true
    await documentoStore.getUserLastDocs()
    loadingDocs.value = false
  }

  onMounted(async () => {
    loadLastDocs()
  })
</script>

<style scoped>
  .custom-list-item {
    border: 1px solid black;
    border-radius: 10px;
    padding: 10px;
    margin: 20px 0px;
    transition: box-shadow 0.5s;
  }

  .custom-list-item:hover {
    box-shadow: 0px 5px 30px rgba(0, 0, 0, 0.25);
  }
</style>
