# Prova-Tecnica

Projeto de um servidor API e uma aplicação console que cadastram e validam os clientes que são cadastrados.
Via API o servidor recebe o cadastro de um novo cliente eo salva em uma tabela temporaria, a API também fornece informação dos clientes ja cadastrados na tabela concreta.
A aplicação console roda periodicamente um script que pega as informações cadastras na tabela temporaria e faz a validação, se ocorrer tudo bem o cadastro é fixado na tabela
concreta e retirado da tabela temporaria, caso a validação seja negativa o cadastro somente é retirado da tabela concreta.
