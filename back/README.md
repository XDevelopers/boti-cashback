# README

<p align="center">
  <img src="https://github.com/XDevelopers/boti-cashback/blob/master/front/static/assets/images/github/boticario.png?raw=true" alt=""/>
</p>

# Desafio – “Eu revendedor ‘O Boticário’ quero ter benefícios de acordo com o meu volume de vendas”

> <br />
> Backend
> <br />
> <br />


## Endpoints

 - Rota para cadastrar um novo revendedor(a) exigindo no mínimo nome completo, CPF, e- mail e senha; 
 - Rota para validar um login de um revendedor(a); 
 - Rota para cadastrar uma nova compra exigindo no mínimo código, valor, data e CPF do revendedor(a). Todos os cadastros são salvos com o status “Em validação” exceto quando o CPF do revendedor(a) for 153.509.460-56, neste caso o status é salvo como “Aprovado”; 
 - Rota para listar as compras cadastradas retornando código, valor, data, % de cashback aplicado para esta compra, valor de cashback para esta compra e status; 
 - Rota para exibir o acumulado de cashback até o momento, essa rota irá consumir essa informação de uma API externa:
    - API externa GET: `https://mdaqk8ek5j.execute-api.us-east-1.amazonaws.com/v1/cashback?cpf=12312312323`
    - headers `{ token: 'ZXPURQOARHiMc6Y0flhRC1LVlZQVFRnm' } `

## Premissas do caso de uso: 

- Os critérios de bonificação são:
    - Para até 1.000 reais em compras, o revendedor(a) receberá 10% de cashback do valor vendido no período de um ms;
    - Entre 1.000 e 1.500 reais em compras, o revendedor(a) receberá 15% de cashback do valor vendido no período de um mês;
    - Acima de 1.500 reais em compras, o revendedor(a) receberá 20% de cashback do valor vendido no período de um mês.


-----

## Estrutura Backend

 O projeto do `Backend` tem uma estrutura bem simples, porém separada em projetos, que podem ser reutilizados ao gosto do time de desenvolvimento sem atrapalhar os demais sub-projetos. Estrutura fico quase isso:
```
├───── src                         # Pasta principal com os demais projetos
├───── test                        # Pasta com projeto de testes unitários
|
├── README.md                   # Passo a Passo para executar a aplicação de back-end
├── Boticario.EuRevendedor.sln  # Arquivo da Solution para o Visual Studio
└── postman_collection.js       # Arquivo do postman para facilitar testes dos endpoints
```

#### 💻 Backend

- [.Net Core 3.1][dotnet-url] REST API com autenticação JWT;
- [MongoDB Cloud][mongo-url] como base de dados `NoSQL`, mas dentro do projeto já existem implementações básicas para outros tipos [ `LiteDb`, `SqLite` ];
- [Swagger][swagger-url] com objetivo de facilitar a vida dos desenvolvedores que poderão vir a utilizar a API;
- [MediatR][mediatr-url] a fim de organizar a comunicação, além de garantir um baixo acoplamento entre objetos;
- [Flunt][flunt-url] e [flunt.br][flunt-br-url] parte importante para trabalhar com o tratamento de erros e notificações da aplicação;
- [S.O.L.I.D][solid-url] para entregar o MVP com máximo dos benefícios alcançados, pela utilização destes príncipios;

Procurei seguir alguns conceitos interessantes que ao longo de minha carreira fui aprendendo e adotando como boas práticas, como por exemplo, um pouco do [Projeto Equinox][equinox-url]. Claro que sigo alguns profissionais que considero muito a opinião e conceitos.

-----


## Screenshots

Uma Screenshot do Swagger do `Backend` antes de rodar a aplicação

<p align="center">
  <img width="750px" src="https://github.com/XDevelopers/boti-cashback/blob/master/front/static/assets/images/github/screenshot_001_api.png?raw=true" alt=""/>
</p>


-----


## :information_source: How To Use

Para clonar essas duas aplicações você precisará do [Git](https://git-scm.com). 

Usando Linha de Comando
 ### Clonar o repositorio

 ```sh
# Clone this repository
$ git clone https://github.com/XDevelopers/boti-cashback.git
 ```

 #### Backend

Para o `Backend` precisará ter instalado o [.Net Core 3.1][dotnet-url] para executar os comando e o [Visual Studio Code][vs-url] se quiser editar algo.

**IMPORTANTE** - Para acessar a Base de Dados no Mongo Cloud será necessário seguir os dados do e-mail...


 ```sh
# Go into the repository
$ cd boti-cashback/back

# Install dependencies
$ dotnet restore

# Build projects
$ dotnet build

# Go to API project
cd src\Boticario.EuRevendedor

# Start frontend app
$ dotnet run

# running https on port 5001
 ```


 -----

## 🤔 How to contribute

-  Make a fork;
-  Create a branck with your feature: `git checkout -b my-feature`;
-  Commit changes: `git commit -m 'feat: My new feature'`;
-  Make a push to your branch: `git push origin my-feature`.

After merging your receipt request to done, you can delete a branch from yours.


-----

## Desenvolvido por

Made with :heavy_heart_exclamation: by Marcio Lourenço :wave: [Get in touch!](https://www.linkedin.com/in/marciolo/)

<a href="https://www.linkedin.com/in/marciolo/">
  <img alt="Made by Marcio Lourenço" src="https://img.shields.io/badge/Made%20by-Marcio%20Louren&ccedil;o-%2304D361">
</a>



[equinox-url]:https://github.com/EduardoPires/EquinoxProject
[solid-url]:https://medium.com/desenvolvendo-com-paixao/o-que-%C3%A9-solid-o-guia-completo-para-voc%C3%AA-entender-os-5-princ%C3%ADpios-da-poo-2b937b3fc530
[mediatr-url]:https://github.com/jbogard/MediatR/wiki
[swagger-url]:https://swagger.io/tools/swagger-ui/
[mongo-url]:https://www.mongodb.com/cloud
[dotnet-url]:https://dotnet.microsoft.com/download
[flunt-url]:https://github.com/andrebaltieri/flunt
[flunt-br-url]:https://github.com/lira92/flunt.br

[nodejs]: https://nodejs.org/
[yarn]: https://yarnpkg.com/
[vs-url]: https://code.visualstudio.com/download