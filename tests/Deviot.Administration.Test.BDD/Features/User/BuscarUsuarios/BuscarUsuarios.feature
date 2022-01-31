Funcionalidade: Buscar usuários
	Como consumidor de api quero buscar todos usuários

Cenário: Obter todos usuários
Dado que sou um consumidor de api
E que tenho parâmetros de pesquisa 'fullname' com valor 'paulo'
Quando executar a url via GET
Então a api retornará status code 200
E todos usuários encontrados

Cenário: Nenhum usuário encontrado
E que tenho parâmetros de pesquisa 'fullname' com valor 'nenhum'
Dado que sou um consumidor de api
Quando executar a url via GET
Então a api retornará status code 204
E a mensagem 'Nenhum usuário foi encontrado'
