import { Injectable } from "@angular/core";
import { HttpClient, HttpResponse } from "@angular/common/http";
import { ValorInicialAplicacao } from "../model/ValorInicialAplicao";
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class CalculoCDBService {
  constructor(private http: HttpClient) { }

  postEfetuarCalculoCDB(valorInicialAplicacao: ValorInicialAplicacao): Observable<any> {
    return this.http.post("https://localhost:44344/v1/EfetuarCalculo/Calcular", valorInicialAplicacao);
  }
}
