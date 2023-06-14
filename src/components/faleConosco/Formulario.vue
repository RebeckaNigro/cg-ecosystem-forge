<template>
  <form action="submit" class="fale-conosco ghp box">
	<!-- NOME-->
    <div class="nome fs-6">
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

	<!--EMAIL E TELEFONE-->
    <div class="two-columns fs-6">
      <div class="email">
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
      <div class="telefone">
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
    <div class="two-columns fs-6">
      <div class="empresa">
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
      <div class="cargo">
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
    <div class="setor fs-6">
      <label class="form-label-primary">Setor do destinatário</label>
      <select
	    class="form-input-primary fs-6"
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

	<!--MENSAGEM-->
    <div class="mensagem fs-6">
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

    <button
      v-if="!sendingEmail"
      type="button"
      @click="sendEmail()"
      class="green-btn"
    >
      ENVIAR
    </button>

	<Spinner v-else/>
  
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
    padding-top: 50px !important;
    padding-bottom: 50px !important;
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
    max-width: 1100px;

    margin: 5rem auto;

    font-weight: 600;
    color: #6b6a64;
    font-size: 1.2rem;
    label {
      margin-left: 2rem;
      margin-top: 0.6rem;
    }
    input {
      color: #000;
    }
    div {
      margin-top: 1rem;
    }

    .two-columns {
      display: flex;
      justify-content: space-between;
      width: 100%;
      gap: 2rem;
    }

    .nome,
    .email,
    .telefone,
    .empresa,
    .cargo,
    .setor,
    .mensagem {
      width: 100%;
    }
    input.half-size,
    select.half-size {
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

    .green-btn:hover {
      background-color: #aacbc3;
    }
    .spinner-border,
    button.green-btn {
      margin-right: 1%;
    }
  }
  @media (max-width: 768px) {
    form.fale-conosco {
      input.half-size,
      select.half-size {
        margin-right: 0;
        margin-left: 0;
        width: 100%;
      }
      textarea.form-control {
        margin-left: 0;
        margin-right: 0;
      }
      .spinner-border,
      button.green-btn {
        margin-right: 0;
      }
    }
  }
</style>
