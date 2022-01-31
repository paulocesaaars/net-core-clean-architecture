Funcionalidade: Adicionar usuário
	Como consumidor de api quero adicionar um usuário

Cenário: Adicionar usuário com sucesso
Dado que sou um consumidor de api
E que tenho um usuário válido
Quando executar a url via POST
Então a api retornará status code 201
E a mensagem 'Usuário adicionado com sucesso'

Cenário: Nome de usuário inválido
Dado que sou um consumidor de api
E que tenho um usuário com nome inválido
Quando executar a url via POST
Então a api retornará status code 400
E a mensagem 'O nome completo deve ter no mínimo 5 caracteres'

Cenário: Email de usuário já existente
Dado que sou um consumidor de api
E que tenho um usuário com email já utilizado
Quando executar a url via POST
Então a api retornará status code 400
E a mensagem 'O email paulo@teste.com já está sendo utilizado'
