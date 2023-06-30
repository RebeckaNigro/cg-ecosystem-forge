<template>
  <div class="mx-auto box p-lg-5 py-4 px-3 mb-5">
    <div class="text-center">
      <h2 class="fs-5 fw-bold">Últimos documentos enviados</h2>
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
            class="col-md-10 col-8 d-flex text-start align-items-center py-2 hover-pointer"
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
            class="col-md-2 col-4 d-flex align-items-center justify-content-center left-separator"
          >
            <div>
              <img
                src="/public/edit_icon.svg"
                alt=""
                class="hover-pointer img-fluid"
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
                class="hover-pointer img-fluid"
                @click="confirmDelete(documento.id)"
              />
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="text-center row mt-5">
      <div class="col-5 col-xlg-6">
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
      <div class="col-7 col-xlg-6">
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
    element-id="confirmLastsDocsModal"
    id="confirmLastsDocsModal"
  />
</template>

<script setup lang="ts">
  import { onMounted, ref } from "vue"
  import Spinner from "../../general/Spinner.vue"
  import ConfirmModal from "../../general/ConfirmModal.vue"
  import { useDocumentStore } from "../../../stores/documentos/store"
  import { useConfirmStore } from "../../../stores/confirm/store"
  import { useModalStore } from "../../../stores/modal/store"
import { Modal } from "bootstrap"

  const documentoStore = useDocumentStore()
  const confirmStore = useConfirmStore()
  const modalStore = useModalStore()
  const loadingDocs = ref(false)

  const confirmDelete = (id: number) => {
	const modalDOM: any = document.querySelector('#confirmLastsDocsModal')

    confirmStore.setConfirmInstance(Modal.getOrCreateInstance(modalDOM)!)
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

<style scoped lang="scss">
</style>
