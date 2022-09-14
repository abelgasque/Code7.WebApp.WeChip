import { Component, OnInit } from '@angular/core';
import { UtilService } from '../util/util.service';
import { Cliente, Endereco, Oferta, Produto } from '../util/models';

@Component({
  selector: 'app-oferta',
  templateUrl: './oferta.component.html',
  styleUrls: ['./oferta.component.css']
})
export class OfertaComponent implements OnInit {

  listaClientes: any[] = [];
  cliente = new Cliente();
  clienteFiltro = new Cliente();
  listaProdutos: any[] = [];
  listaStatus: any[] = [];
  listaProdutosCadastro: any[] = [];
  display: boolean = false;
  displayProduto: boolean = false;
  oferta = new Oferta();
  tiposProduto = [
    { label: 'Hardware', value: 'HARDWARE' },
    { label: 'Software', value: 'SOFTWARE' }
  ];
  produtoSelecao = new Produto();

  constructor(private util: UtilService) { 
    this.carregarProdutos();
  }

  ngOnInit() { }

  limparForm(){
    this.oferta = new Oferta();
    this.displayProduto = false
  }

  novaOferta(){
    this.oferta = new Oferta();
    this.display = true;
  }

  abrirDisplaySelecaoProduto(){
    this.oferta.produto = new Produto();
    this.displayProduto = true;
  }

  cancelarSelecao(){
    this.oferta.produto = new Produto();
    this.displayProduto = false;
  }

  salvarSelecao(){
    this.oferta.produto = this.produtoSelecao;
    this.displayProduto = false;
  }

  salvarOferta(){
    console.log(this.oferta);
    if(this.oferta.cliente.endereco == null && this.oferta.produto.tipo == "HARDWARE"){
      this.util.showWarn("Para inserir produto do tipo hardware cadastre o endereço do cliente!");
    }else{
      this.util.adicionarOferta(this.oferta).subscribe(
        (response:any) =>{
          this.getOfertaPorCliente(response.idCliente);
          this.display = false;
          this.carregarProdutos();
          this.limparForm();
          this.util.showSuccess("Oferta adicionada com sucesso!");
        },
        (erro: any)=>{
          console.log(erro);
          this.util.showError("Erro ao adicionar oferta!");
        }
      );
    }
  }

  getEnderecoPorCep(){
    if(this.cliente.endereco.cep == null || this.cliente.endereco.cep == undefined){
      this.util.showWarn("Insira um CEP");
    }else{
      this.util.getEnderecoPorCep(this.cliente.endereco.cep).subscribe(
        (response: any) =>{
          this.cliente.endereco.estado = response.uf;
          this.cliente.endereco.cidade = response.localidade;
          this.cliente.endereco.bairro = response.bairro;
          this.util.showSuccess("Endereço pesquisado com sucesso!");
        },
        (erro: any) => {
          console.log(erro);
          this.util.showError("Erro ao buscar endereço do cliente!");
        }
      );
    }
  }

  editarCliente(){
    this.util.atualizarCliente(this.cliente.id, this.cliente).subscribe(
      (response:any) =>
      {
        this.util.showSuccess("Cliente atualizado com sucesso!");
        this.cliente = response;
      },
      (erro:any) =>
      {
        this.util.showError("Erro ao editar cliente!");
        console.log(erro);
      }
    );
  }

  getClientById(){
    if(this.clienteFiltro.id == null || this.clienteFiltro.id == undefined){
      this.util.showWarn("Selecione um cliente no filtro de pesquisas!");
    }else{
      this.util.getClienteById(this.clienteFiltro.id).subscribe(
        (response: any) =>{
          if(response.endereco == null){
            response.endereco = new Endereco();
          }
          if(response.id != null){
            this.getOfertaPorCliente(response.id);
            this.getStatusPorCliente(response.id);
            this.cliente = response;
            
          }
          
        },
        (erro: any)=>{
          console.log(erro);
          this.util.showError("Erro ao buscar cliente!");
        }
      );
    }
  }
  
  getStatusPorCliente(id: number){
    this.listaStatus = [];
    this.util.getStatusByClienteId(id).subscribe(
      (response: any) => {
        if(response.length > 0){
          this.listaStatus = response;
        }
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }

  getOfertaPorCliente(id: number){
    this.listaProdutos = [];
    this.util.getOfertasByClienteId(id).subscribe(
      (response: any) => {
        if(response.length > 0){
          for(let i=0;i<response.length;i++){
            this.listaProdutos.push(response[i].produto);
          }
        }
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }

  carregarProdutos(){
    this.util.getAllProdutos().subscribe(
      (response: any) => {
        this.listaProdutosCadastro = response;
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }

  carregarClientes(){
    this.util.getAllClientes().subscribe(
      (response: any) => {
        this.listaClientes = response;
      },
      (erro: any) => {
        console.log(erro);
      }
    );
  }
}
