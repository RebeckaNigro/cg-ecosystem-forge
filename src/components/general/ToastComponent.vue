<template>
  <div class="position-fixed top-0 end-0 p-3" style="z-index: 11">
    <div id="liveToast" class="toast hide" role="alert" aria-live="assertive" aria-atomic="true">
      <div class="toast-header">
        <div class="status" :id="status" />
        <strong class="me-auto">{{ title }}</strong>
        <small>11 mins ago</small>
        <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
      </div>
      <div class="toast-body">
        {{ message }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
const props = defineProps<{
  title: string;
  message: string;
  status: string;
}>();

const openToast = (id: string) => {
  // gambiarra para abrir e fechar modal. FIXME: cannot find name "bootstrap"
  const toast = document.querySelector('#' + id)!;
  if (toast.classList.contains('hide')) {
    toast.classList.remove('hide');
  }
  toast.classList.add('show');
  setTimeout(() => {
    toast.classList.remove('show');
    toast.classList.add('hide');
  }, 5000);
};
</script>

<style scoped lang="scss">
.toast {
  .status {
    width: 20px;
    height: 20px;
    border-radius: 20px;
  }
  div[id='success'] {
    background-color: lightgreen;
  }
  div[id='warning'] {
    background-color: lightyellow;
  }
  div[id='error'] {
    background-color: red;
  }
}
</style>
