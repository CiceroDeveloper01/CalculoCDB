# CalculoCDB
Cálculo de Investimentos CDB

Visual Studio - WebpAI
Para o projeto WebApi rodar é necessário ter NetCore 7.0 instalado;
No arquivo JSON appsettings.Development deverá colocar o caminho que o banco SQLLITE deve ser criado e chamdo;
No arquivo JSON appsettings.Development deverá colocar o caminho a onde o log será armazenado;

ANGULAR
Para o projeto Angular ser executado foi desenvolvido no Node.js version  v19.7.0
No arquivo calculo-cdb.service.ts --> Verificar na linha 11 se o seu computador colocou a API neteste path https://localhost:44344/EfetuarCalculo, caso não 
tenha feito, por favor ajustar.
Ele terá um formulário simples onde se conecta com a API e realiza a proposta dos exercício.
Mesmo o projeto estando dentro da Solution abri o VisualStudioCode e rodar a pasta CalculoCDBFrontWeb no terminal com ng server

Observações Gerais
Mesmo estando num único GIT, por favor, rodar o Visual Studio para c# e executar o projeto Angular por linha de comando.
Para analise do código Sonar https://sonarcloud.io/summary/overall?id=CiceroDeveloper01_CalculoCDB e token b9751018ac219e2d593af781d41bb2c1c332437f
