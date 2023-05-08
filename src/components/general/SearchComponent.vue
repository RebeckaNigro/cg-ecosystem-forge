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
 //@ts-nocheck 
  import { onMounted, ref } from "vue"
  import { EventoSimplificado } from "../../stores/eventos/types"
  import { NoticiaSimplificada } from "../../stores/noticias/types"
  import { CustomTag } from "../../stores/tag/types"
import { IDocumentoSimplificado } from "../../stores/documentos/types"

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
    if (!searchWord.value) {
      results.value = props.items

      sendSearchResult()
      return
    }
    switch (props.type) {
      case "noticia":
        results.value = props.items.filter((item: NoticiaSimplificada) => {
          if (
            item.titulo.toLowerCase().indexOf(searchWord.value.toLowerCase()) >=
            0
          )
            return true

          const tagMatch = item.tags.find((tag: CustomTag) => {
            return tag.descricao
              .toLowerCase()
              .indexOf(searchWord.value.toLowerCase()) >= 0
              ? true
              : false
          })

          return tagMatch ? true : false
        })
        break

      case "evento":
        results.value = props.items.filter((item: EventoSimplificado) => {
			
          if (
            item.titulo.toLowerCase().indexOf(searchWord.value.toLowerCase()) >=
            0
          )
            return true

          if (
            item.local?.toLowerCase().indexOf(searchWord.value.toLowerCase()) >=
            0
          )
            return true

          const tagMatch = item.tags.find((tag: CustomTag) => {
            return tag.descricao
              .toLowerCase()
              .indexOf(searchWord.value.toLowerCase()) >= 0
              ? true
              : false
          })

          return tagMatch ? true : false
        })
        break
		
		case 'documento':
			results.value = props.items.filter((item: IDocumentoSimplificado) => {
				if(
					item.nome.toLowerCase().indexOf(searchWord.value.toLowerCase()) >= 0 
				)
					return true

				const tagMatch = item.tags.find((tag: CustomTag) => {
					return tag.descricao
					.toLowerCase()
					.indexOf(searchWord.value.toLowerCase()) >= 0
					? true
					: false
				})

				return tagMatch ? true : false
			})

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
