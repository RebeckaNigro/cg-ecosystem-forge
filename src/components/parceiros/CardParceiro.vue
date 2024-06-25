<template>
  <div
    class="card-parceiro"
    @click="$router.push({ name: 'ParceiroExpandido', params: { parceiroId: cardParceiro.id } })"
  >
    <img :src="cardParceiro.logo ? cardParceiro.logo : ''" alt="Logo do parceiro" />
  </div>
</template>

<script setup lang="ts">
import type { IPartnerSimplificado } from '../../stores/parceiros/types';

const props = defineProps<{
  cardParceiro: IPartnerSimplificado;
}>();
</script>

<style scoped lang="scss">
.card-parceiro {
  width: 220px;
  height: 220px;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #fff;
  // margin: 10px;
  img {
    width: inherit;
    max-height: 220px;
  }
}
.card-parceiro:hover {
  cursor: pointer;
  box-shadow: 0 0 30px 5px gray;
}
</style>
