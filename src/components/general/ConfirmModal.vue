<template>
  <div
    class="modal fade show"
    data-bs-backdrop="static"
    data-bs-keyboard="false"
  >
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content modal-content-adapter">
        <div class="modal-body modal-content-font">
          {{ confirmStore.options.message }}
        </div>
        <div class="modal-footer modal-footer-adapter">
          <div class="row container-fluid">
            <div class="col-sm-6">
              <button
                type="button"
                class="green-btn-primary m-auto btn-adapter"
                @click="confirmStore.closeConfirm"
              >
                Cancelar
              </button>
            </div>
            <div class="col-sm-6">
              <button
                type="button"
                class="green-btn-primary m-auto btn-adapter"
                @click="sendConfirmEvent"
              >
                Confirmar
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { Modal } from "bootstrap"

  import { onMounted, ref } from "vue"
  import { useConfirmStore } from "../../stores/confirm/store"

  const confirmStore = useConfirmStore()

  const emit = defineEmits(["confirmTrue"])
  const bsConfirmModal = ref<Modal>()

  const props = defineProps({
    elementId: {
      type: String
    },
    parameter: {
      required: false
    }
  })

  onMounted(() => {
    const modalDOM: any = document.querySelector(`#${props.elementId}`)

    bsConfirmModal.value = Modal.getOrCreateInstance(modalDOM)!
    confirmStore.confirmInstance = bsConfirmModal.value
  })

  const sendConfirmEvent = () => {
    emit("confirmTrue", props.parameter)
  }
</script>

<style scoped lang="scss">
  .btn-adapter {
    width: 100%;
  }
  .btn-close {
    position: absolute;
    right: 20px;
  }
  .status {
    width: 30px;
    height: 30px;
    border-radius: 30px;
  }

  .modal-icon {
    width: 100px;
  }
  .modal-content-adapter {
    border-radius: 20px;
    padding: 20px 5px;
  }

  .modal-header-adapter {
    border: none;
    margin: auto;
  }

  .modal-footer-adapter {
    border: none;
  }

  .modal-content-font {
    font-size: 1.5rem;
    font-weight: 600;
  }
</style>
