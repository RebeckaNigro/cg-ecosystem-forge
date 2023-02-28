<template>
    <div class="pb-5">
        <div v-if="noticiaId">
            <header>
                <h1 class="dark-title mt-5 mb-5 fs-2">Edite sua notícia</h1>
            </header>
            <main>
                <FormEditarNoticia :noticia-id="noticiaId"/>
            </main>
        </div>

        <div v-else>
            <header>
                <h1 class="dark-title mt-5 mb-5 fs-2">Envie sua notícia!</h1>
            </header>
            <main>
                <FormNoticia/>
            </main>
        </div>
    </div>
</template>

<script setup lang="ts">
import {onMounted, ref} from 'vue';
import FormNoticia from '../../../components/user/noticias/FormNoticia.vue';
import FormEditarNoticia from '../../../components/user/noticias/FormEditarNoticia.vue'
import router from '../../../router';

const noticiaId = ref<number | null>(null)

onMounted(() => {
    if (router.currentRoute.value.query.idNoticia) {
        noticiaId.value = parseInt(router.currentRoute.value.query.idNoticia.toString())
    }

})
</script>

<style scoped lang="scss">

.background {
  background-color: #fff;
  background-image: url("background.svg");
  background-size: cover;
  width: 100%;
}
</style>