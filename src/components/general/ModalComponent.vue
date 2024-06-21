<template>
  <div class="modal fade show" data-bs-backdrop="static" data-bs-keyboard="false">
    <div class="modal-dialog modal-dialog-centered modal-dialog-centered-adapter" id="modalBody">
      <div class="modal-content modal-content-adapter" v-if="!modalStore.loading">
        <div class="modal-header modal-header-adapter">
          <img
            :src="modalStore.options.status ? `/icons/${modalStore.options.status}-icon.svg` : ''"
            :alt="modalStore.options.status"
            class="modal-icon"
          />
        </div>
        <div class="modal-body modal-content-font">
          {{ modalStore.options.message }}
        </div>
        <div class="modal-footer modal-footer-adapter">
          <button type="button" class="green-btn-primary m-auto" @click="modalStore.closeModal">
            Fechar
          </button>
        </div>
      </div>
      <div v-else>
        <div class="modal-content modal-content-loading-adapter">
          <div class="spinner-border text-success m-auto mt-2" role="status"></div>
          <div class="text-center my-3 fs-4 verde-padrao fw-bold">Carregando...</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Modal } from 'bootstrap';

import { onMounted, ref } from 'vue';
import { useModalStore } from '../../stores/modal/store';

const modalStore = useModalStore();
const bsModal = ref<Modal>();

onMounted(() => {
  const modalDOM: any = document.querySelector('#generalModal');

  bsModal.value = Modal.getOrCreateInstance(modalDOM)!;
  modalStore.modalInstance = bsModal.value;
});
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

.modal-content-loading-adapter {
  border-radius: 20px;
  padding: 40px 50px;
  text-align: center;
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

.modal-dialog-centered-adapter {
  justify-content: center;
}
</style>
