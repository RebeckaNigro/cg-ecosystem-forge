<template>
  <div :class="computedClass" role="alert">
    <div class="text-start">{{ alertStore.options.message }}</div>
    <button
      type="button"
      class="btn-close"
      aria-label="Close"
      v-if="alertStore.options.dispensable"
      @click="closeAlert"
    ></button>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useAlertStore } from '../../stores/alert/store';

const computedClass = ref('alert alert-dismissible w-75 m-auto my-3 fixed-top message-alert');
const alertStore = useAlertStore();

const closeAlert = () => {
  alertStore.visible = false;
};

onMounted(() => {
  if (alertStore.options.timeout) {
    setTimeout(() => {
      closeAlert();
    }, 10000);
  }

  switch (alertStore.options.type) {
    case 'error':
      computedClass.value += ' alert-danger';
      break;
    case 'warning':
      computedClass.value += ' alert-warning';
      break;
    case 'info':
      computedClass.value += ' alert-primary';
      break;
    case 'success':
      computedClass.value += ' alert-success';
      break;
  }
});
</script>

<style scoped>
.message-alert {
  opacity: 0.9;
  animation: slide-message 2s;
}

@keyframes slide-message {
  from {
    opacity: 0;
    transform: translate3d(0px, -50px, 0px);
  }
  to {
    opacity: 0.9;
    transform: translate3d(0px, 0px, 0px);
  }
}
</style>
