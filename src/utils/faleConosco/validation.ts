import { faleConosco, faleConoscoInterface } from '../../stores/comunicacao/types';

const nameIsValid = (n: string) => {
  if (!n || n.length < 2 || /[^A-zÀ-ÿ\s]/i.test(n)) return false;
  return true;
};
const emailIsValid = (e: string) => {
  if (!e || e.length < 9 || !/@/.test(e)) return false;
  return true;
};
const phoneIsValid = (p: string) => {
  if (!p || p.length < 11 || !/[0-9]/i.test(p)) return false;
  return true;
};
const empresaIsValid = (e: string) => {
  if (!e || e.length < 2) return false;
  return true;
};
const cargoIsValid = (c: string) => {
  if (!c || c.length < 3) return false;
  return true;
};
const setorIdIsValid = (s: number) => {
  return s > 0 && s < 7 ? true : false;
};
const messageIsValid = (m: string) => {
  if (!m || m.length < 10) return false;
  return true;
};

export const validateFaleConoscoInput = ({
  nome,
  emailCorporativo,
  telefone,
  empresa,
  cargo,
  setorId,
  mensagem,
}: faleConoscoInterface): TypeError | faleConosco => {
  if (!nameIsValid(nome)) return TypeError('Insira um nome válido.');
  if (!emailIsValid(emailCorporativo)) return TypeError('Insira um e-mail válido.');
  if (!phoneIsValid(telefone)) return TypeError('Insira um telefone válido.');
  if (!empresaIsValid(empresa)) return TypeError('Parece que há algo errado com o nome da empresa');
  if (!cargoIsValid(cargo)) return TypeError('Confira o cargo e tente novamente.');
  if (!setorIdIsValid(setorId)) return TypeError('Selecione um setor para enviar.');
  if (!messageIsValid(mensagem)) return TypeError('Por favor, nos envie uma mensagem!');
  return new faleConosco(nome, emailCorporativo, telefone, empresa, cargo, setorId, mensagem);
};
