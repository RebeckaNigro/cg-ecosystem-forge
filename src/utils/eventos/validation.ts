//@ts-nocheck
import { Evento, IEvento } from '../../stores/eventos/types';

const eventNameIsValid = (en: string) => {
  if (!en || en.length < 2) return false;
  return true;
};

// TODO validar se a data de inicio nao é maior que a de termino
// TODO validar se a data de inicio nao é menor que a data atual
const beginDatetimeIsValid = (d: string) => {
  if (!d) return false;
  return true;
};

const endDatetimeIsValid = (d: string) => {
  if (!d) return false;
  return true;
};

const eventDescriptionIsValid = (des: string) => {
  if (!des || des.length < 10) return false;
  return true;
};

/*const addressIsValid = (a: INovoEndereco | null) => {
	
	if(!a.logradouro || !a.cep || !a.numero || !a.complemento || !a.pontoReferencia || !a.cidade || !a.uf) return false
	return true
}*/

//TODO usar regex pra validar a url
const ticketLinkIsValid = (l: string) => {
  if (!l) return false;
  return true;
};

const producerNameIsValid = (n: string) => {
  if (!n || n.length < 2 || /[^A-zÀ-ÿ\s]/i.test(n)) return false;
  return true;
};

// TODO validar o endereço
export const validateEventoInput = (evento: IEvento): TypeError | Evento => {
  if (!eventNameIsValid(evento.titulo))
    return TypeError('O nome deve possuir 2 caracteres ou mais');
  if (!beginDatetimeIsValid(evento.dataInicio)) return TypeError('Selecione uma data de início');
  if (!endDatetimeIsValid(evento.dataTermino)) return TypeError('Selecione uma data de término');
  if (!eventDescriptionIsValid(evento.descricao))
    return TypeError('A descrição deve possuir 10 caracteres ou mais');

  return new Evento(
    evento.id,
    evento.instituicaoId,
    evento.tipoEventoId,
    evento.titulo,
    evento.descricao,
    evento.dataInicio,
    evento.dataTermino,
    evento.local,
    evento.enderecoId,
    evento.endereco,
    evento.linkExterno,
    evento.exibirMaps,
    evento.responsavel,
    null
  );
};
