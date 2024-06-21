<template>
  <div class="card-evento">
    <div class="img-container">
      <img
        :src="
          evento.arquivo
            ? 'data:image/png;base64, ' + evento.arquivo
            : '/eventos/eventoExpandido/default-event-cover.svg'
        "
        alt="Imagem do evento"
        :class="{ 'img-evento-encerrado': encerrado }"
      />
    </div>
    <div class="infos-container">
      <h1 class="dark-title" :class="{ 'texto-evento-encerrado': encerrado }">
        {{ encerrado ? evento.titulo + ' (ENCERRADO)' : evento.titulo }}
      </h1>
      <time class="data-evento"
        >{{ brDateString(evento.dataInicio) }} - {{ brDateString(evento.dataTermino) }}</time
      >
      <address class="endereco-evento">{{ evento.local }}</address>

      <a
        href="#"
        class="ver-detalhes"
        @click="$router.push({ name: 'EventoExpandido', params: { eventoId: evento.id } })"
        >Ver detalhes</a
      >
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { brDateString } from '../../utils/formatacao/datetime';
import { IEventoSimplificado } from '../../stores/eventos/types';

const props = defineProps<{
  evento: IEventoSimplificado;
}>();

const encerrado = ref(false);

onMounted(() => {
  // verifica se o evento já foi encerrado
  if (new Date(props.evento.dataTermino) < new Date()) {
    encerrado.value = true;
  }
});
</script>

<style scoped lang="scss">
.img-evento-encerrado {
  opacity: 0.5;
}

.texto-evento-encerrado {
  color: #6b6a64;
}
.card-evento {
  display: flex;
  flex-direction: column;
  width: 300px;
  height: 400px;
  border-radius: 10px;
  background-color: #fff;

  .img-container {
    height: 65%;
    display: flex;
    justify-content: center;
    align-items: center;
    img {
      width: -webkit-fill-available;
      height: -webkit-fill-available;
      border-top-left-radius: 10px;
      border-top-right-radius: 10px;
    }
  }
  .infos-container {
    border-top: 3px solid lightgray;
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    justify-content: center;
    height: 50%;
    padding: 10px;

    h1 {
      white-space: nowrap;
      overflow: hidden;
      text-overflow: ellipsis;
      width: 100%;
      text-align: start;
      font-weight: 600;
      font-size: 1.2rem;
    }

    .data-evento {
      font-weight: 500;
    }

    .endereco-evento {
      font-weight: 400;
    }

    .ver-detalhes {
      color: #1e1e1e;
      font-weight: 500;
      font-size: 0.8rem;
      align-self: center;
      margin: 0.3rem;
    }

    time,
    address {
      background-repeat: no-repeat;
      background-position: center left;
      background-size: contain;
      padding-left: 30px;
      margin: 5px 0;
    }
    time {
      background-image: url('/public/calendar_icon.svg');
    }
    address {
      background-image: url('/public/location_icon.svg');
    }
  }
}
@media (max-width: 1230px) {
  .card-evento {
    .infos-container {
      h1 {
        font-size: 1rem;
      }
    }
  }
}
@media (max-width: 1200px) {
  .card-evento {
    margin: 10px auto !important;
  }
}
@media (max-width: 576px) {
  .card-evento {
    height: 350px;
    max-width: 250px;
    width: 100%;
  }
}
</style>
