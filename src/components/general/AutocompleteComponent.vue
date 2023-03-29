<template>
  <div class="autocomplete">
    <input
      type="text"
      class="form-input-primary"
      v-model="search"
      @input="onChange"
      @keydown.down="onArrowDown"
      @keydown.up="onArrowUp"
      @keydown.enter="onEnter"
    />
    <ul class="autocomplete-results" v-show="isOpen">
      <li v-if="isLoading" class="loading">Carregando...</li>
      <li
        v-else
        class="autocomplete-result"
        v-for="(result, i) in results"
        :key="i"
        @click="setResult(result)"
        :class="{ 'is-active': i === arrowCounter }"
      >
        <span v-if="props.type == 'CustomTag'">{{ result.descricao }} </span>
        <span v-if="props.type == 'Instituicao'"
          >{{ result.razaoSocial }}
        </span>
      </li>
    </ul>
  </div>
</template>
<script setup lang="ts">
  import { ref, watch } from "vue"
  import { CustomTag } from "../../stores/tag/types"

  const emit = defineEmits(["selected-value"])
  const props = defineProps({
    items: {
      type: Array,
      required: false,
      default: () => []
    },
    type: {
      type: String,
      required: true
    }
  })

  const search = ref("")
  const results = ref<any[]>()
  const isOpen = ref(false)
  const isLoading = ref(false)
  const arrowCounter = ref(-1)

  watch(
    () => props.items,
    (value, oldValue) => {
      if (value.length !== oldValue.length) {
        results.value = value
        isLoading.value = false
      }
    }
  )

  const filterResults = () => {
    if (props.type == "CustomTag") {
      results.value = props.items.filter(
        (item: any) =>
          item.descricao.toLowerCase().indexOf(search.value.toLowerCase()) > -1
      )
    } else if (props.type == "Instituicao") {
      results.value = props.items.filter(
        (item: any) =>
          item.razaoSocial.toLowerCase().indexOf(search.value.toLowerCase()) >
          -1
      )
    }
  }

  const setResult = (result: any) => {
    search.value = result
    isOpen.value = false
  }

  const onChange = () => {
    filterResults()
    isOpen.value = true
  }

  const onArrowDown = () => {
    if (arrowCounter.value < results.value.length) {
      arrowCounter.value = arrowCounter.value + 1
    }
  }

  const onArrowUp = () => {
    if (arrowCounter.value > 0) {
      arrowCounter.value = arrowCounter.value - 1
    }
  }

  const onEnter = () => {
    if (!search.value) return

    const previousValue = search.value
    const obj = results.value[arrowCounter.value]
    search.value = ""

    if (obj) {
      emit("selected-value", obj)
      if (props.type == "Instituicao") {
        search.value = obj.razaoSocial
      }
    } else {
      if (props.type == "CustomTag") {
        const addTag = new CustomTag(previousValue)
        emit("selected-value", addTag)
      }
    }

    arrowCounter.value = -1
    isOpen.value = false
  }
</script>
<style>
  .autocomplete {
    position: relative;
  }

  .autocomplete-results {
    padding: 0;
    margin: 0;
    border: 1px solid #eeeeee;
    height: 120px;
    min-height: 1em;
    max-height: 6em;
    overflow: auto;
  }

  .autocomplete-result {
    list-style: none;
    text-align: left;
    padding: 4px 2px;
    cursor: pointer;
  }

  .autocomplete-result.is-active,
  .autocomplete-result:hover {
    background-color: #49907f;
    color: white;
  }
</style>
