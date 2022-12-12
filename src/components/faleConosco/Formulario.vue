<template>
  <form action="submit" class="fale-conosco ghp box">
	<div class="nome">
		<label for="name-input">Nome</label>
		<input class="form-control boring-gray-border" v-model="contactForm.nome" type="text" name="person-name" id="name-input" >
	</div>

	<div class="two-columns">

		<div class="email">
			<label for="email-input">Email corporativo</label>
			<input class="form-control boring-gray-border" v-model="contactForm.emailCorporativo" type="text" name="corporate-email" id="email-input">
		</div>
		<div class="telefone">
			<label for="phone">Telefone</label>
			<input class="form-control boring-gray-border" v-model="contactForm.telefone" type="tel" name="phone" id="phone" maxlength="11" @keyup="evalNumberInput($event)">
		</div>
	</div>

	<div class="two-columns">
		<div class="empresa">
			<label for="company-input">Empresa</label>
			<input class="form-control boring-gray-border" v-model="contactForm.empresa" type="text" name="company" id="company-input">
		</div>
		<div class="cargo">
			<label for="cargo-input">Cargo</label>
			<input class="form-control boring-gray-border" v-model="contactForm.cargo" type="text" name="job-position" id="cargo-input">
		</div>
	</div>

	<div class="setor">
		<label>Setor do destinatário</label>
		<select class="form-control form-select boring-gray-border" v-model="contactForm.setorId" aria-label="Default select example" :disabled="waitingSetores">
		  <option value="0" selected>Selecione um setor</option>
		  <option v-for="(setor, index) in comunicacaoStore.faleConoscoSetores" :key="setor.id" :value="setor.id">{{ setor.descricao }}</option>
		</select>
	</div>
    
    <div class="mensagem">
		<label for="message">Mensagem</label>
		<textarea class="form-control boring-gray-border" v-model="contactForm.mensagem" name="message" id="message" cols="30" rows="10"></textarea>
	</div>
    
    <button v-if="!sendingEmail" type="button" @click="sendEmail()" class="green-btn">ENVIAR</button>
    <div v-else class="spinner-border text-success ml-auto mt-2" role="status">
      <span class="visually-hidden">Loading...</span>
    </div>
  </form>
<ModalComponent
  :title="modalSettings.title"
  :message="modalSettings.message"
  :status="modalSettings.status"
  :modal-id="modalSettings.id"
/>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue';
import { useComunicacaoStore } from '../../stores/comunicacao/store';
import ModalComponent from '../general/ModalComponent.vue'
import { Modal } from 'bootstrap';

  const comunicacaoStore = useComunicacaoStore();
  const sendingEmail = ref(false);
  const contactForm = ref({
    nome: '',
    emailCorporativo: '',
    telefone: '',
    empresa: '',
    cargo: '',
    setorId: 0,
    mensagem: ''
  });
  const sendEmail = async () => {
    sendingEmail.value = true
    sendingEmail.value = await comunicacaoStore.sendFaleConosco(contactForm.value.nome, contactForm.value.emailCorporativo, contactForm.value.telefone, contactForm.value.empresa, contactForm.value.cargo, contactForm.value.setorId, contactForm.value.mensagem)
    const res = comunicacaoStore.faleConoscoResponse.getResponse()
    if (res.code === 200) {
      openModal('faleConoscoRes', 'Sucesso', res.message, 'success')
    } else if (res.code === 661 || res.code === 666) {
        console.error(res.message)
    } else {
      openModal('faleConoscoRes', 'Falha', res.message, 'warning')
    }
  }
  const evalNumberInput = (e: KeyboardEvent) => {
    if (isNaN(parseInt(e.key))) {
      contactForm.value.telefone = contactForm.value.telefone.slice(0, contactForm.value.telefone.indexOf(e.key))
    }
  }
  const modalSettings = reactive({
    title: '',
    message: '',
    status: '',
    id: 'faleConoscoRes'
  })
  const openModal = (modalId: string, mt: string, mm: string, ms: string) => {
    modalSettings.id = modalId
    modalSettings.title = mt
    modalSettings.message = mm
    modalSettings.status = ms
    const modalDOM: any = document.querySelector('#' + modalId)
    const bsModal = Modal.getOrCreateInstance(modalDOM)!
    bsModal.show()
  }
  const waitingSetores = ref(true)
  onMounted( async () => {
    waitingSetores.value = true;
    waitingSetores.value = await comunicacaoStore.getFaleConoscoSetores();
  })
</script>

<style scoped lang="scss">
  form.fale-conosco {
    padding-top: 50px !important;
    padding-bottom: 50px !important;
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
	max-width: 1100px;
	text-align: start;
	margin: 5rem auto;

	font-family: 'Montserrat-SemiBold', sans-serif;
	color: #6B6A64;
	font-size: 1.2rem;
	label{
		margin-left: 2rem;
		margin-top: .6rem;
	}
	input{
		color: #000;
	}
	div{
		margin-top: 1rem;
	}

	.two-columns{
		display: flex;
		justify-content: space-between;
		width: 100%;
		gap: 2rem;
	}

	.nome,.email, .telefone, .empresa, .cargo, .setor, .mensagem{
		width: 100%;
	}
    input.half-size, select.half-size {
      width: 48%;
      margin-right: 1%;
      margin-left: 1%;
    }
    .half-size:nth-child(even) {
      margin-left: 0 !important;
    }
    .half-size:nth-child(odd) {
      margin-right: 0 !important;
    }
    .form-control {
      margin-top: 10px;
      height: 50px;
    }
    textarea.form-control {
      min-height: 150px;
      margin-left: 1%;
      margin-right: 1%;
    }
    .green-btn {
      margin-left: auto;
      margin-top: 1rem;
      color: #fff;
      border-radius: 25px;
      width: 250px;
      height: 40px;
      line-height: 40px;
      text-align: center;
    }

	.green-btn:hover{
		background-color: #aacbc3;
	}
    .spinner-border, button.green-btn {
      margin-right: 1%;
    }
  }
  @media (max-width: 768px) {
    form.fale-conosco {
      input.half-size, select.half-size {
        margin-right: 0;
        margin-left: 0;
        width: 100%;
      }
      textarea.form-control {
        margin-left: 0;
        margin-right: 0;
      }
      .spinner-border, button.green-btn {
        margin-right: 0;
      }
    }
  }
</style>