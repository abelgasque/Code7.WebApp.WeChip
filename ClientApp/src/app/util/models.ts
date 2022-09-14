export class Endereco{
    id: number = 0;
    cep: string;
    rua: string;
    numero: string;
    complemento: string;
    bairro: string;
    cidade: string;
    estado: string;
}

export class Status{
    id: number = 0;
    descricao: string;
    finalizaCliente: boolean;
    contabilizarVenda: boolean;
    codigo: string;
}

export class Cliente{
    id: number = 0;
    nome: string;
    cpf: string;
    telefone: string;
    credito: number = 0;
    idStatus: number = 0;
    status = new Status();
    idEndereco: number = 0;
    endereco = new Endereco();
}

export class Produto{
    id: number = 0;
    descricao: string;
    preco: number = 0;
    tipo: string = "HARDWARE";
    codigo: Produto;
}

export class Oferta{
    id: number = 0;
    idCliente: number;
    idStatus: number;
    idProduto: number;
    produto = new Produto();
    cliente = new Cliente();
    status = null;
}