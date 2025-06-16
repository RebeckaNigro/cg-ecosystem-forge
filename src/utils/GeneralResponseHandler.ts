export interface ResponseInterface {
  code: number;
  dado: any; // especificar tipo de dado do retorno
  message: string;
}

export class GeneralResponseHandler implements ResponseInterface {
  code: number;
  dado: any;
  message: string;

  constructor(c: number, d: any, m: string) {
    this.code = c;
    this.dado = d;
    this.message = m;
  }

  putResponse(c: number, d: any, m: string) {
    this.code = c;
    this.dado = d;
    this.message = m;
  }
  putError(c: number, m: string) {
    this.code = c;
    this.message = m;
  }
  getResponse(): ResponseInterface {
    return {
      code: this.code,
      dado: this.dado,
      message: this.message,
    };
  }
}
