Funcionalidade: Atualizar usuário
	Como consumidor de api quero atualizar um usuário

Cenário: Atualizar usuário com sucesso
Dado que sou um consumidor de api
E que tenho um usuário válido
Quando executar a url via PUT
Então a api retornará status code 200
E a mensagem 'Usuário atualizado com sucesso'

Cenário: Usuário não encontrado
Dado que sou um consumidor de api
E que que tenho um usuário com id inexistente
Quando executar a url via PUT
Então a api retornará status code 404
E a mensagem 'O usuário não foi encontrado'

Cenário: Nome de usuário inválido
Dado que sou um consumidor de api
E que tenho um usuário com nome inválido
Quando executar a url via PUT
Então a api retornará status code 400
E a mensagem 'O nome completo deve ter no mínimo 5 caracteres'

Cenário: Email de usuário já existente
Dado que sou um consumidor de api
E que tenho um usuário com email já utilizado
Quando executar a url via PUT
Então a api retornará status code 400
E a mensagem 'O email bruna@teste.com já está sendo utilizado'

