# README

<p align="center">
  <img src="https://github.com/XDevelopers/boti-cashback/blob/master/front/static/assets/images/github/boticario.png?raw=true" alt=""/>
</p>

# Desafio – “Eu revendedor ‘O Boticário’ quero ter benefícios de acordo com o meu volume de vendas”


> <br />
> Sistema de Cashback, para revenderores do Boticário sistema de crédito para a próximas compras dos revendedores
><br />
><br />

<br />
<br />
<h4 align="center"> 
	:heavy_check_mark: Desafio 🚀 O Boticário! :heavy_check_mark:
</h4>

<br />
<p align="center">	
	
  <img alt="Repository size" src="https://img.shields.io/github/repo-size/XDevelopers/boti-cashback">
	&nbsp;&nbsp;&nbsp;
  <a href="https://www.linkedin.com/in/marciolo/">
    <img alt="Made by Marcio Lourenço" src="https://img.shields.io/badge/Made%20by-Marcio%20Louren&ccedil;o-%2304D361">
  </a>
&nbsp;&nbsp;&nbsp;
  <a href="https://github.com/XDevelopers/boti-cashback/commits/master">
    <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/XDevelopers/boti-cashback">
  </a>
&nbsp;&nbsp;&nbsp;
  <img alt="License" src="https://img.shields.io/badge/license-MIT-brightgreen">
   <a href="https://github.com/XDevelopers/boti-cashback/stargazers">&nbsp;&nbsp;&nbsp;
    <img alt="Stargazers" src="https://img.shields.io/github/stars/XDevelopers/boti-cashback?style=social">
  </a>
</p>

<br />
<p align="center">
  <a href="#information_source-proposta">Proposta</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-backend">Backend</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#-frontend">Frontend</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#information_source-how-to-use">How to use</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#desenvolvido-por">Desenvolvido por</a>
</p>

## :information_source: Proposta

Esse desafio foi separado em duas etapas, que juntas entregam um **MVP (Mínimo Produto Viável)** bem interessante no que diz respeito a *tecnologias de vanguarda*.

A fim de atender a *futuras regras de negócio*, das mais simples às mais complexas, no `Backend` foram utilizada as algumas tecnologias, citadas e brevemente descritas abaixo.

<br />
<br />

## :rocket: Tecnologias

-----

#### 💻 Backend

- [.Net Core 3.1][dotnet-url] REST API com autenticação JWT;
- [MongoDB Cloud][mongo-url] como base de dados `NoSQL`, mas dentro do projeto já existem implementações básicas para outros tipos [ `LiteDb`, `SqLite` ];
- [Swagger][swagger-url] com objetivo de facilitar a vida dos desenvolvedores que poderão vir a utilizar a API;
- [MediatR][mediatr-url] a fim de organizar a comunicação, além de garantir um baixo acoplamento entre objetos;
- [Flunt][flunt-url] e [flunt.br][flunt-br-url] parte importante para trabalhar com o tratamento de erros e notificações da aplicação;
- [S.O.L.I.D][solid-url] para entregar o MVP com máximo dos benefícios alcançados, pela utilização destes príncipios;

Procurei seguir alguns conceitos interessantes que ao longo de minha carreira fui aprendendo e adotando como boas práticas, como por exemplo, um pouco do [Projeto Equinox][equinox-url]. Claro que sigo alguns profissionais que considero muito a opinião e conceitos.

-----

#### 🔖 Frontend

- [Nuxt.js][nuxt-url] Um framework javascript simples e poderoso, que aumenta a produtividade;
- [VUEX][vuex-ur] foi utilizado para o gerenciamento de estado de alguns objetos;
- [Vuetify][vuetify-url] entrega diversos componentes de interface com o visual do [Materialize][materialize-url] deixando praticamente responsiva a aplicação;

Muitos podem perguntar **" Por que o [Nuxt.js][nuxt-url]? "**, mas optei por ele para esse **MVP**, pela sua simplicidade e facilidade de estender padrões e dessa forma agilizar ainda mais o desenvolvimento.

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

Para o `Backend` precisará ter instalado o [.Net Core 3.1][dotnet-url] para executar os comando e o [Visual Studio Code][vs-url] se quiser editar algo. Você também pode ver **mais detalhes** no link :arrow_forward: [Backend][back-url].

----

**IMPORTANTE** - Para acessar a Base de Dados no Mongo Cloud será necessário seguir os dados do e-mail... Utilizando o arquivo anexo...

---

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

 #### Frontend
 
Para o `Frontend` precisará do [Node.js][nodejs] + [Yarn][yarn], para baixar os pacotes e executar a aplicação. Você pode ver **mais detalhes** no link :arrow_forward: [Frontend][front-url].

 ```sh
# Go into the repository
$ cd boti-cashback/front

# Install dependencies
$ yarn install --force

# Start frontend app
$ yarn dev

# running on port 5050
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


[nuxt-url]:https://nuxtjs.org/
[vuex-ur]:https://vuex.vuejs.org/
[vuetify-url]:https://vuetifyjs.com/
[materialize-url]:https://materializecss.com/

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
[front-url]: https://github.com/XDevelopers/boti-cashback/tree/master/front#-how-to-use
[back-url]: https://github.com/XDevelopers/boti-cashback/tree/master/back#-how-to-use
