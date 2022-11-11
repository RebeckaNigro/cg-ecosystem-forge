<template>
	<div class="spinner-border align-self-center mt-4 mb-4" :class="spinnerColorClass" role="status">
      <span class="visually-hidden">Loading...</span>
    </div>
</template>

<script setup lang="ts">

const props = defineProps<{
	spinnerColorClass: string
}>()
</script>

<style lang="scss">
</style>