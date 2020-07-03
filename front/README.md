# README

<p align="center">
  <img src="https://github.com/XDevelopers/boti-cashback/blob/master/front/static/assets/images/github/boticario.png?raw=true" alt=""/>
</p>

# Desafio – “Eu revendedor ‘O Boticário’ quero ter benefícios de acordo com o meu volume de vendas”

> <br />
> Frontend
> <br />
> <br />

<br />

## Requisitos:

- Tela de cadastro de um novo revendedor(a) solicitando Nome completo, CPF, e- mail e senha; 
- Tela de login para informar e-mail e senha; 
- Tela de cadastro de compras onde deverá ser informado o código, valor e data; 
- Tela de listagem das compras cadastradas exibindo as informações de: código da compra, valor, data, % de cashback aplicado, valor do cashback e status do cadastro; 
- O status do cadastro poderá ser “Em validação”, “Reprovado” e “Aprovado”; 
- Tela para exibir o valor de cashback acumulado até o momento, esta informação virá de uma das APIs do Boticário, que é um outro sistema que agrupa e consolida todas as vendas do revendedor(a);


<br />

-----

#### 🔖 Frontend

- [Nuxt.js][nuxt-url] Um framework javascript simples e poderoso, que aumenta a produtividade;
- [VUEX][vuex-ur] foi utilizado para o gerenciamento de estado de alguns objetos;
- [Vuetify][vuetify-url] entrega diversos componentes de interface com o visual do [Materialize][materialize-url] deixando praticamente responsiva a aplicação;

Muitos podem perguntar **" Por que o [Nuxt.js][nuxt-url]? "**, mas optei por ele para esse **MVP**, pela sua simplicidade e facilidade de estender padrões e dessa forma agilizar ainda mais o desenvolvimento.

-----

## Screenshots

Algumas Screenshots do `Frontend` antes de rodar a aplicação

<p align="center">
  <img width="750px" src="https://github.com/XDevelopers/boti-cashback/blob/master/front/static/assets/images/github/screenshot_001.png?raw=true" alt=""/>
</p>

<p align="center">
  <img width="750px" src="https://github.com/XDevelopers/boti-cashback/blob/master/front/static/assets/images/github/screenshot_002.png?raw=true" alt=""/>
</p>

<p align="center">
  <img width="750px" src="https://github.com/XDevelopers/boti-cashback/blob/master/front/static/assets/images/github/screenshot_003.png?raw=true" alt=""/>
</p>

<p align="center">
  <img width="750px" src="https://github.com/XDevelopers/boti-cashback/blob/master/front/static/assets/images/github/screenshot_004.png?raw=true" alt=""/>
</p>

<p align="center">
  <img width="750px" src="https://github.com/XDevelopers/boti-cashback/blob/master/front/static/assets/images/github/screenshot_005.png?raw=true" alt=""/>
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

 #### Frontend

 Para o `Frontend` precisará do [Node.js][nodejs] + [Yarn][yarn], para baixar os pacotes e executar a aplicação.
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


[nodejs]: https://nodejs.org/
[yarn]: https://yarnpkg.com/
[vs-url]: https://code.visualstudio.com/download