PARTNER GROUP TEST

RESUMO DO PROJETO:
Este projeto é uma API em .NET Core 2.1, responsável por realizar um CRUD de duas entidades (Patrimonio e Marca). Onde tento utilizar boas práticas para diminuir o acoplamento e permitir a reutilização do código. 

ARQUITETURA BASEADA EM DDD:
  •	INFRA: Responsável pela acesso e comunicação com o Banco de Dados;
  •	REPOSITORY: Implementa a lógica necessária para acessar a fonte de dados;
  •	SERVICES: Responsável pela manipulação das regras de negócio;
  •	SHARED KERNEL: Compartilha funcionalidades que poderão ser utilizadas em diversas partes no sistema;
  •	DOMAIN: Aqui estão as classes POCO e Interfaces 
  •	API: É o start da aplicação onde estão os ‘Controllers’ 
TESTE UNITÁRIO:
  •	Criei um projeto de testes, onde utilizo Moq para gerar o retorno fake dos métodos e realizar o teste de uma forma mais controlada.
BANCO DE DADOS:
  Para o teste, fiz o acesso ao banco de dados local do SqlServer. 
  Referente a manipulação dos dados, criei Stored Procedures para os comandos de Busca e Inserção. Para os comandos de Atualização e Deleção, estão fixos. Toda manipulação é feita com ADO.
RETRONO DA APLICAÇÃO:
  A API retorna um objeto denominado APIReturn, com as seguintes propriedades:
    •	Content: Conteúdo do retorno do banco de dados serializado em JSON;
    •	StatusCode: Número do status da requisição;
    •	Message: Mensagem de sucesso ou erro;
    •	ErrorMessage: 
