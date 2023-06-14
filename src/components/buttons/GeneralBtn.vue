<template>
  <button type="button" class="btn btn-primary general-btn" :id="id" @mouseover="glowBg($el)" @mouseout="unglowBg($el)">
    <router-link v-if="!isExternalLink" :to="link" class="text">{{ btnText }}</router-link>
    <a v-else :href="link" class="text">{{ btnText }}</a>
  </button>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';

  const props = defineProps<{
    // TODO: set prop to change background color when mouse cursos is over button
    btnText: string
    isExternalLink: boolean
    link: string
    bgColor: string
    width: string
    textColor: string
    height: string
    id: string
  }>()

const applyCss = (bgColor: string, w: string, h: string, textColor: string) => {
  // TODO: set properties to anchors as well
  const btn: HTMLElement = document.querySelector('#' + props.id)!
  btn.style.cssText = `
    background-color: ${bgColor};
    width: ${w};
    height: ${h};
  `
  const text: HTMLElement = btn.querySelector('.text')!
  text.style.cssText = `
    color: ${textColor};
    text-decoration: none;
  `
}
const glowBg = (element: any) => {
  element.style.backgroundColor = '#8eb0a5'
}
const unglowBg = (element: any) => {
  element.style.backgroundColor = props.bgColor
}
onMounted(() => {
  applyCss(props.bgColor, props.width, props.height, props.textColor)
})
</script>

<style scoped lang="scss">
  .general-btn {
    border-radius: 25px;
    border: unset;
    font-weight: 700;
    font-weight: 1000;
    font-size: 1rem;
    margin: auto;
    margin-top: 10px;
    .text {
      text-decoration: none;
    }
    .general-btn:hover {
      background-color: #8eb0a5;
      outline: none;
    }
  }
</style>