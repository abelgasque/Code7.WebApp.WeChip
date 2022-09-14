import { Component, OnInit, Inject } from '@angular/core';
import { Cliente, Status } from '../util/models';
import { UtilService } from '../util/util.service';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {
  
  cliente = new Cliente();

  constructor(private util: UtilService) {
    this.novaInstanciaCliente();
  }

  ngOnInit() {}

  novaInstanciaCliente(){
    this.cliente = new Cliente();
    this.cliente.endereco = null;
    this.cliente.status = null;
  }

  salvarCliente(){
    this.util.adicionarCliente(this.cliente).subscribe(
      (response: any) => {
        this.novaInstanciaCliente();
        this.util.showSuccess("Cliente adicionado com sucesso!");
      },
      (erro: any) => {
        this.util.showError("Erro ao adicionar cliente!");
        console.log(erro);
      }
    );
  }
}
