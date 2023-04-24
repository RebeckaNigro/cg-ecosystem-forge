<template>
  <div class="text-start">
    <label for="filtro-data" class="ms-3 mb-2">Filtrar por {{ field }}:</label>
    <div>
      <select
        name="filtro"
        id="filtro"
        class="form-input-primary borda-cinza-claro cinza"
        v-model="selectedOption"
        @change="doFilter($event)"
      >
        <option value="">Todas</option>
        <option
          v-for="(option, index) in menuOptions"
          :key="index"
          :value="option.value"
        >
          {{ option.text }}
        </option>
      </select>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { onMounted, ref } from "vue"
  import { adjustStringDateForTimezone } from "../../utils/formatacao/datetime"
  import { NoticiaSimplificada } from "./../../stores/noticias/types"
  import { FilterOption } from "../../stores/general/types"
  import { EventoSimplificado } from "../../stores/eventos/types"

  const props = defineProps<{
    field: string
    items: any[]
    type: string
  }>()

  const emit = defineEmits(["filter-result"])
  const menuOptions = ref<FilterOption[]>([])
  const results = ref<any[]>([])
  const selectedOption = ref("")

  onMounted(() => {
    switch (props.type) {
      case "noticia":
        menuOptions.value.push(new FilterOption("Últimos 7 dias", "7"))
        menuOptions.value.push(new FilterOption("Últimos 15 dias", "15"))
        menuOptions.value.push(new FilterOption("Últimos 30 dias", "30"))
        menuOptions.value.push(new FilterOption("Últimos 90 dias", "90"))
        break
      case "evento":
        menuOptions.value.push(new FilterOption("Últimos 7 dias", "7"))
        menuOptions.value.push(new FilterOption("Últimos 15 dias", "15"))
        menuOptions.value.push(new FilterOption("Últimos 30 dias", "30"))
        menuOptions.value.push(new FilterOption("Últimos 90 dias", "90"))
        break
    }
  })

  const doFilter = (event: any) => {
    if (event.target.value) {
      selectedOption.value = event.target.value

      if (props.type == "noticia") {
        const dataReferencia = new Date()
        dataReferencia.setDate(
          dataReferencia.getDate() - Number(selectedOption.value)
        )
        dataReferencia.setHours(0, 0, 0)

        results.value = props.items.filter((noticia: NoticiaSimplificada) => {
          return adjustStringDateForTimezone(
            noticia.dataPublicacao
          ).getTime() >= dataReferencia.getTime()
            ? true
            : false
        })
        sendSearchResult()
      } else if (props.type == "evento") {
        const dataReferencia = new Date()
        dataReferencia.setDate(
          dataReferencia.getDate() - Number(selectedOption.value)
        )
        dataReferencia.setHours(0, 0, 0)

        results.value = props.items.filter((evento: EventoSimplificado) => {
          return adjustStringDateForTimezone(evento.dataInicio).getTime() >=
            dataReferencia.getTime()
            ? true
            : false
        })
        sendSearchResult()
      }
    } else {
      results.value = props.items
      sendSearchResult()
    }
  }

  const sendSearchResult = () => {
    emit("filter-result", results.value)
  }
</script>

<style scoped></style>
