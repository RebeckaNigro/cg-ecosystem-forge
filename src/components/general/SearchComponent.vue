<template>
  <div class="input-icon-container overflow-hidden">
    <i class="icon overflow-hidden">
      <img
        src="../../../public/search_icon.svg"
        class="input-icon-image"
        @click="search"
      />
    </i>
    <input
      type="text"
      name="pesquisar"
      id="pesquisar"
      placeholder="Pesquisar"
      class="form-input-primary search-input px-3"
      v-model="searchWord"
      @keydown.enter="search"
    />
  </div>
</template>

<script setup lang="ts">
  import { onMounted, ref } from "vue"
  import { NewsTag, NoticiaSimplificada } from "../../stores/noticias/types"

  const props = defineProps({
    items: {
      type: Array,
      required: true
    },
    type: String
  })

  const emit = defineEmits(["search-result"])
  const results = ref([])
  const searchWord = ref("")

  const search = () => {
    switch (props.type) {
      case "noticia":
        results.value = props.items.filter((item: NoticiaSimplificada) => {
          if (
            item.titulo.toLowerCase().indexOf(searchWord.value.toLowerCase()) >=
            0
          )
            return true

          const tagMatch = item.tags.find((tag: NewsTag) => {
            return tag.descricao
              .toLowerCase()
              .indexOf(searchWord.value.toLowerCase()) >= 0
              ? true
              : false
          })

          return tagMatch ? true : false
        })
        break
    }

    sendSearchResult()
  }

  const sendSearchResult = () => {
    emit("search-result", results.value)
  }

  onMounted(() => {})
</script>

<style scoped>
  .search-input {
    border-radius: 50px;
    border-color: lightgray;
  }
</style>
