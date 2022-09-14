import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { MessageService } from 'primeng/components/common/messageservice';

@Injectable({
  providedIn: 'root'
})
export class UtilService {

  private urlCliente = `${environment.apiUrl}/api/cliente`
  private urlStatus = `${environment.apiUrl}/api/status`;
  private urlOferta = `${environment.apiUrl}/api/oferta`;
  private urlProduto = `${environment.apiUrl}/api/produto`;
  
  constructor(
    private http: HttpClient,
    private messageService: MessageService  
  ) { }

  adicionarOferta(entidade: any): Observable<any>{
    return this.http.post<any>(`${this.urlOferta}`, entidade);
  }

  getOfertasByClienteId(id: number): Observable<Array<any>>{
    return this.http.get<Array<any>>(`${this.urlOferta}/por-cliente/${id}`);
  }

  adicionarCliente(entidade: any): Observable<any>{
    return this.http.post<any>(`${this.urlCliente}`, entidade);
  }

  atualizarCliente(id: number, entidade: any): Observable<any>{
    return this.http.put<any>(`${this.urlCliente}/${id}`, entidade);
  }

  getClienteById(id: number): Observable<any>{
    return this.http.get<any>(`${this.urlCliente}/${id}`);
  }

  getAllClientes(): Observable<any[]>{
    return this.http.get<any>(this.urlCliente);
  }

  getStatusByClienteId(id: number): Observable<Array<any>>{
    return this.http.get<Array<any>>(`${this.urlStatus}/por-cliente/${id}`);
  }
  
  getAllProdutos(): Observable<any[]>{
    return this.http.get<any>(this.urlProduto);
  }

  getEnderecoPorCep(cep: string): Observable<any> {
    return this.http.get(`https://viacep.com.br/ws/${cep}/json/`);
  }

  showSuccess(msg: string) {
    this.messageService.add({severity:'success', summary: msg, detail:''});
  }

  showWarn(msg: string) {
      this.messageService.add({severity:'warn', summary: msg, detail:''});
  }

  showError(msg: string) {
      this.messageService.add({severity:'error', summary: msg, detail:''});
  }
}
