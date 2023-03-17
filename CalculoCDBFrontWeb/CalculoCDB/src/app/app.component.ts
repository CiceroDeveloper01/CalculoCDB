import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Notificacao } from './model/Notificacao';
import { ResultadoInvestimento } from './model/ResultadoInvestimento';
import { ValorInicialAplicacao } from './model/ValorInicialAplicao';
import { CalculoCDBService } from './service/calculo-cdb.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'CalculoCDBFrontEnd';

  resultadoInvestimento: ResultadoInvestimento;
  notificacao: Notificacao;
  showResult: boolean = false;
  showNotification: boolean = false;

  constructor(
    private calculoCDBService: CalculoCDBService,
    private formBuilder: FormBuilder
  ) {
    this.resultadoInvestimento = { prazoInvestimento: 0, valorBruto: 0, valorLiquido: 0 };
    this.notificacao = { message: "", property: "" };
  }

  formCalculoInvestimento: any = FormGroup;
  ngOnInit(): void {
    this.formCalculoInvestimento = this.formBuilder.group({
      prazoInvestimento: [null],
      valorInicialInvestimento: [null]
    });
  }

  efetuarCalculoCDB() {
    let valorInicialAplicacao: ValorInicialAplicacao =
    {
      PrazoInvestimento: Number.parseInt(this.formCalculoInvestimento.get('prazoInvestimento')?.value),
      ValorInicial: Number.parseFloat(this.formCalculoInvestimento.get('valorInicialInvestimento')?.value)
    };

    this.calculoCDBService.postEfetuarCalculoCDB(valorInicialAplicacao).subscribe(
      (resultCommand: any) => {
        if (resultCommand.success) {
          this.resultadoInvestimento = resultCommand.data;
          this.showResult = true;
          this.showNotification = false;
        }
        else {
          this.notificacao = resultCommand.data;
          this.showResult = false;
          this.showNotification = true;
        }
      }, (error) => {
        console.log(error.error)

        this.notificacao = error.error.data[0];
        this.showResult = false;
        this.showNotification = true;
      }
    );
  }
}
