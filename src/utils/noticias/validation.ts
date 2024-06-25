//@ts-nocheck
import type { INoticia, Noticia } from '../../stores/noticias/types';

const titleIsValid = (t: string) => {
  if (!t || t.length < 2) return false;
  return true;
};

const subtitleIsValid = (s: string) => {
  if (!s || s.length < 2) return false;
  return true;
};

const dateIsValid = (date: string) => {
  if (!date) return false;
  return true;
};

const descriptionIsValid = (d: string) => {
  if (!d || d.length < 10) return false;
  return true;
};

// TODO validar foto

export const validateNoticiaInput = ({
  id,
  titulo,
  descricao,
  subTitulo,
  dataPublicacao,
}: INoticia): TypeError | Noticia => {
  if (!titleIsValid(titulo)) return TypeError('Insira um título.');
  if (!subtitleIsValid(subTitulo)) return TypeError('Insira um subtítulo.');
  if (!dateIsValid(dataPublicacao)) return TypeError('Selecione uma data.');
  if (!descriptionIsValid(descricao)) return TypeError('Insira uma descrição.');
  return new Noticia(id, titulo, descricao, subTitulo, dataPublicacao);
};
