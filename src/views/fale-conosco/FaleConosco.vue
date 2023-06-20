<template>
    <Banner
            path="/fale-conosco/fale-conosco-banner.png"
            figcaption="costumer-service"
            img-alt="costumer-service"
    >
		<ExternalHeader
			title="Fale conosco"
			paragraph="Tenha acesso aos principais documentos disponibilizados pelo Ecossistema Local de Inovação - Campo
                Grande - MS"
		/>
    </Banner>
    <Formulario/>
</template>

<script setup lang="ts">
import Banner from '../../components/general/Banner.vue';
import Formulario from '../../components/faleConosco/Formulario.vue';
import ExternalHeader from '../../components/general/ExternalHeader.vue';
</script>

<style scoped lang="scss">

</style>