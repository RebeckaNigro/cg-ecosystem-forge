<template>
    <div :class="computedClass" class="alert alert-dismissible w-75 m-auto my-3 message-alert fixed-top" role="alert">
       <div class="text-start">{{ alertStore.options.message }}</div>
       <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" v-if="alertStore.options.dispensable"></button>
    </div>
</template>

<script setup lang="ts">
import {onMounted, ref} from 'vue';
import {useAlertStore} from '../../stores/alert/store';

const computedClass = ref('')
const alertStore = useAlertStore()

const props = defineProps({
    mensagem: {
        type: String,
        default: 'Teste'
    },
    tipo: {
        type: String,
        default: 'info'
    },
    dispensavel: {
        type: Boolean,
        default: true
    },
    timeout: {
        type: Boolean,
        default: true
    }
})

const closeAlert = () => {
    alertStore.visible = false
}

onMounted(() => {
    if (alertStore.options.timeout) {
        setTimeout(() => {
            closeAlert()
        }, 5000)
    }

    switch (alertStore.options.type) {
        case 'error':
            computedClass.value = 'alert-danger'
            break
        case 'warning':
            computedClass.value = 'alert-warning'
            break
        case 'info':
            computedClass.value = 'alert-primary'
            break
        case 'success':
            computedClass.value = 'alert-success'
            break
    }
})


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