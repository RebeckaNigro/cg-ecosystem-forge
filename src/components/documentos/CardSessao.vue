<template>
  <div class="card-container d-flex flex-column align-items-center">
    <div>
      <img v-if="hasIcon" :src="iconPath" />
      <h1 class="m-0 font-semibold">{{ title }}</h1>
    </div>

    <p>{{ text }}</p>

    <button type="button" class="green-btn-primary w-50">Ver mais</button>
  </div>
</template>

<script setup lang="ts">
const props = defineProps<{
  title: string;
  iconPath: string;
  text: string;
  hasIcon: boolean;
  contentToDownload: string;
  hasDownload: boolean;
}>();

const downloadContent = async () => {
  alert('tem que implementar');
};
</script>

<style lang="scss" scoped>
.card-container {
  border: 1px solid black;
  border-radius: 10px;
  padding: 28px 24px;
  height: 325px;
  margin: 10px 0;
  box-shadow: 0px 0px 30px rgba(0, 0, 0, 0.1);
  background: #fff;

  > div {
    display: flex;
    align-items: center;

    h1 {
      margin-left: 12px;
      font-size: 24px;
      overflow: hidden;
      text-overflow: ellipsis;
      display: -webkit-box;
      -webkit-line-clamp: 3; /* number of lines to show */
      line-clamp: 3;
      -webkit-box-orient: vertical;
    }

    img {
      width: 36px;
      height: 32px;
    }
  }

  > p {
    font-size: 16px;
    margin: 20px 0;
    text-align: justify;
    height: 100%;
    overflow-y: auto;
    max-height: 168px;
  }
}
@media (max-width: 576px) {
  .card-container {
    > div > h1 {
      font-size: 1rem;
    }
    > p {
      font-size: 0.7rem;
    }
    > div > img {
      width: 24px;
      height: 24px;
    }
  }
}
</style>
