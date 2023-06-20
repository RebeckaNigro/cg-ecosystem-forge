<template>
	<form action="submit" class="fale-conosco container p-5 box d-flex flex-wrap justify-content-between ">
		
	<!-- NOME-->
	<div class="row w-100">
		<div class="col-12">
		  <label  class="form-label-primary" for="name-input">Nome</label>
		  <input
			  class="form-input-primary"
			v-model="contactForm.nome"
			type="text"
			name="person-name"
			id="name-input"
			:class="v$.nome.$error ? 'is-invalid' : ''"
		  />
		  <div v-if="v$.nome.$error" class="invalid-feedback">
			  {{ v$.nome.$errors[0].$message }}
			</div>
		</div>
	</div>

	<!--EMAIL E TELEFONE-->
	<div class="row w-100">
		
		<div class="col-md-6 col-12">
			<label for="email-input"  class="form-label-primary">Email corporativo</label>
			<input
			class="form-input-primary"
				v-model="contactForm.emailCorporativo"
				type="text"
				name="corporate-email"
				id="email-input"
				:class="v$.emailCorporativo.$error ? 'is-invalid' : ''"
			/>
			<div v-if="v$.emailCorporativo.$error" class="invalid-feedback">
				{{ v$.emailCorporativo.$errors[0].$message }}
			</div>
		</div>
		<div class="col-md-6 col-12">
			<label for="phone"  class="form-label-primary">Telefone</label>
			<input
			class="form-input-primary"
				v-model="contactForm.telefone"
				type="tel"
				name="phone"
				id="phone"
				maxlength="11"
				:class="v$.telefone.$error ? 'is-invalid' : ''"
			/>
			<div v-if="v$.telefone.$error" class="invalid-feedback">
				{{ v$.telefone.$errors[0].$message }}
			</div>
		</div>
	
	</div>

	<!--EMPRESA E CARGO -->
    <div class="row w-100">
      <div class="col-md-6 col-12">
        <label class="form-label-primary" for="company-input">Empresa</label>
        <input
		 class="form-input-primary"
          v-model="contactForm.empresa"
          type="text"
          name="company"
          id="company-input"
		  :class="v$.empresa.$error ? 'is-invalid' : ''"
        />
		<div v-if="v$.empresa.$error" class="invalid-feedback">
          {{ v$.empresa.$errors[0].$message }}
        </div>
      </div>
      <div class="col-md-6 col-12">
        <label class="form-label-primary" for="cargo-input">Cargo</label>
        <input
		  class="form-input-primary"
          v-model="contactForm.cargo"
          type="text"
          name="job-position"
          id="cargo-input"
		  :class="v$.cargo.$error ? 'is-invalid' : ''"
        />
		<div v-if="v$.cargo.$error" class="invalid-feedback">
          {{ v$.cargo.$errors[0].$message }}
        </div>
      </div>
    </div>

	<!--SETOR-->
	<div class="row w-100">
		<div class="col-12">
		  <label class="form-label-primary">Setor do destinatário</label>
		  <select
			class="form-input-primary "
			v-model="contactForm.setorId"
			aria-label="Default select example"
			:disabled="waitingSetores"
			:class="v$.setorId.$error ? 'is-invalid' : ''"
		  >
			<option value="0" selected>Selecione um setor</option>
			<option
			  v-for="(setor, index) in comunicacaoStore.faleConoscoSetores"
			  :key="setor.id"
			  :value="setor.id"
			>
			  {{ setor.descricao }}
			</option>
		  </select>
		  <div v-if="v$.setorId.$error" class="invalid-feedback">
			  {{ v$.setorId.required.$message }}
			</div>
		</div>
	</div>

	<!--MENSAGEM-->
	<div class="row w-100">
		<div class="col">
		  <label class="form-label-primary" for="message">Mensagem</label>
		  <textarea
			class="form-input-primary"
			v-model="contactForm.mensagem"
			name="message"
			id="message"
			cols="30"
			rows="10"
			:class="v$.mensagem.$error ? 'is-invalid' : ''"
		  ></textarea>
		  <div v-if="v$.mensagem.$error" class="invalid-feedback">
			  {{ v$.mensagem.$errors[0].$message }}
			</div>
		</div>
	</div>

	<div class="row w-100">

		<div class="col">
			<button type="button" class="green-btn-outlined" @click="$router.back()">
				VOLTAR
			</button>
		</div>

		<div class="col">
			<button
			  v-if="!sendingEmail"
			  type="button"
			  @click="sendEmail()"
			  class="green-btn-primary"
			>
			  ENVIAR
			</button>
		
			<Spinner v-if="sendingEmail"/>
		</div>



	</div>
  
  </form>
</template>

<script setup lang="ts">
  import { computed, onMounted, reactive, ref } from "vue"
  import { useComunicacaoStore } from "../../stores/comunicacao/store"
  import useValidate from "@vuelidate/core"
  import {
    required,
    email,
    minLength,
    sameAs,
    helpers,
between,
maxLength,
minValue
  } from "@vuelidate/validators"

  import { useModalStore } from "../../stores/modal/store"
  import Spinner from "../general/Spinner.vue"

  const comunicacaoStore = useComunicacaoStore()
  const modalStore = useModalStore()
  const sendingEmail = ref(false)
  const waitingSetores = ref(false)
  const contactForm = ref({
    nome: "",
    emailCorporativo: "",
    telefone: "",
    empresa: "",
    cargo: "",
    setorId: 0,
    mensagem: ""
  })

  const formularioRules = computed(() => {
	return {
		nome: {
			required: helpers.withMessage("Nome é obrigatório.", required)
		},
		emailCorporativo: {
			required: helpers.withMessage('Email é obrigatório.', required), email
		},
		telefone: {
			required: helpers.withMessage('Telefone é obrigatório.', required),
			minLength: minLength(10),
			maxLength: maxLength(11)
		},
		empresa: {
			required: helpers.withMessage('Empresa é obrigatório.', required)
		},
		cargo: {
			required: helpers.withMessage('Cargo é obrigatório.', required)
		},
		setorId: {
			required: helpers.withMessage('Selecione um setor.', required),
			minValue: minValue(1)
		},
		mensagem: {
			required: helpers.withMessage('Mensagem é obrigatório.', required)
		},
	}
  })

  const v$ = useValidate(formularioRules, contactForm)

  const sendEmail = async () => {
	v$.value.$validate()

	console.log(v$.value);
	
	if(!v$.value.$error){
		sendingEmail.value = true
	
		sendingEmail.value = await comunicacaoStore.sendFaleConosco(
		  contactForm.value.nome,
		  contactForm.value.emailCorporativo,
		  contactForm.value.telefone,
		  contactForm.value.empresa,
		  contactForm.value.cargo,
		  contactForm.value.setorId,
		  contactForm.value.mensagem
		)
		const res = comunicacaoStore.faleConoscoResponse.getResponse()
		if (res.code === 200) {
		  modalStore.showSuccessModal("Mensagem enviada com sucesso!")
		} else if (res.code === 661 || res.code === 666) {
		  console.error(res.message)
		} else {
		  modalStore.showWarningModal("Erro ao enviar mensagem. " + res.message)
		}
	}
  }
//   const evalNumberInput = (e: KeyboardEvent) => {
//     if (isNaN(parseInt(e.key))) {
//       contactForm.value.telefone = contactForm.value.telefone.slice(
//         0,
//         contactForm.value.telefone.indexOf(e.key)
//       )
//     }
//   }
  
  onMounted(async () => {
    waitingSetores.value = true
    waitingSetores.value = await comunicacaoStore.getFaleConoscoSetores()
	waitingSetores.value = false
  })
</script>

<style scoped lang="scss">
  form.fale-conosco {
    max-width: 1000px;
    margin: 5rem auto;
    color: #6b6a64;
 
    label {
      margin-top: 0.6rem;
    }
    div {
      margin-top: 1rem;
    }
    
  }

</style>
