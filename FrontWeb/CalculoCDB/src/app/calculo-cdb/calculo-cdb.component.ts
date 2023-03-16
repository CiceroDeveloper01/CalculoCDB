import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Notificacao } from './model/Notificacao';
import { ResultadoInvestimento } from './model/ResultadoInvestimento';
import { ValorInicialAplicacao } from './model/ValorInicialAplicao';
import { CalculoCDBService } from './service/calculo-cdb.service';

@Component({
  selector: 'app-calculo-cdb',
  templateUrl: './calculo-cdb.component.html',
  styleUrls: ['./calculo-cdb.component.css']
})
export class CalculoCDBComponent implements OnInit {

  resultadoInvestimento: ResultadoInvestimento;
  notificacao: Notificacao;

  formCalculoInvestimento: FormGroup;

  ngOnInit(): void { }

  constructor(private calculoCDBService: CalculoCDBService,
              private formBuilder: FormBuilder) {
    this.resultadoInvestimento = { PrazoInvestimento: 0, ValorBruto: 0, ValorLiuquido: 0 };
    this.notificacao = { messagem: "", property: "" };
    this.formCalculoInvestimento = this.formBuilder.group({
      prazoInvestimento: [null, [Validators.required]],
      valorInicialInvestimento: [null, [Validators.required]]
    });
  }

  efetuarCalculoCDB() {
    let valorInicialAplicacao: ValorInicialAplicacao = 
                                { 
                                    PrazoInvestimento: Number.parseInt(this.formCalculoInvestimento.get('prazoInvesimento')?.value), 
                                    ValorInicial: Number.parseFloat(this.formCalculoInvestimento.get('valorInicialInvestimento')?.value) 
                                };

    this.calculoCDBService.postEfetuarCalculoCDB(valorInicialAplicacao).subscribe(
      (resultCommand: any) => {
        if (resultCommand.success) {
          this.resultadoInvestimento = JSON.parse(resultCommand.data);
        }
        else {
          this.notificacao = JSON.parse(resultCommand.data);
        }
      }, (error) => { console.error(error) }
    );
  }
}