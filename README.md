# README

<p align="center">
  <img src="https://github.com/XDevelopers/boti-cashback/blob/master/front/static/assets/images/github/boticario.png?raw=true" alt=""/>
</p>

# Desafio – “Eu revendedor ‘O Boticário’ quero ter benefícios de acordo com o meu volume de vendas”

> Sistema de Cashback, para revenderores do Boticário sistema de crédito para a próximas compras dos revendedores

## :information_source: Proposta

Esse desafio foi separado em duas etapas, que juntas entregam um **MVP (Mínimo Produto Viável)** bem interessante no que diz respeito a *tecnologias de vanguarda*.

A fim de atender a *futuras regras de negócio*, das mais simples às mais complexas, no `Backend` foram utilizada as algumas tecnologias, citadas e brevemente descritas abaixo.

#### 💻 Backend

- [.Net Core 3.1][dotnet-url] REST API com autenticação JWT;
- [MongoDB Cloud][mongo-url] como base de dados `NoSQL`, mas dentro do projeto já existem implementações básicas para outros tipos [ `LiteDb`, `SqLite` ];
- [Swagger][swagger-url] com objetivo de facilitar a vida dos desenvolvedores que poderão vir a utilizar a API;
- [MediatR][mediatr-url] a fim de organizar a comunicação, além de garantir um baixo acoplamento entre objetos;
- [Flunt][flunt-url] e [flunt.br][flunt-br-url] parte importante para trabalhar com o tratamento de erros e notificações da aplicação;
- [S.O.L.I.D][solid-url] para entregar o MVP com máximo dos benefícios alcançados, pela utilização destes príncipios;

Procurei seguir alguns conceitos interessantes que ao longo de minha carreira fui aprendendo e adotando como boas práticas, como por exemplo, um pouco do [Projeto Equinox][equinox-url]. Claro que sigo alguns profissionais que considero muito a opinião e conceitos.


#### 🔖 Frontend

- [Nuxt.js][nuxt-url] Um framework javascript simples e poderoso, que aumenta a produtividade;
- [VUEX][vuex-ur] foi utilizado para o gerenciamento de estado de alguns objetos;
- [Vuetify][vuetify-url] entrega diversos componentes de interface com o visual do [Materialize][materialize-url] deixando praticamente responsiva a aplicação;

Muitos podem perguntar **" Por que o [Nuxt.js][nuxt-url]? "**, mas optei por ele para esse **MVP**, pela sua simplicidade e facilidade de estender padrões e dessa forma agilizar ainda mais o desenvolvimento.






## Desenvolvido por

Marcio Lourenço


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



