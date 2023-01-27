CREATE PROCEDURE dbo.buscaCliCPF
         @CPF varchar(15)
  AS
         Select cpf, nome, endereco, telefone
         from clientes
         where CPF=@CPF

 ALTER PROCEDURE dbo.buscaCliNome
         @nome varchar(50)
  AS
         select cpf, nome, endereco, telefone
         from clientes
         where nome like @nome +'%'

CREATE PROCEDURE dbo.buscaTodos
  AS
         Select cpf, nome, endereco, telefone
         from clientes

CREATE PROCEDURE dbo.ExcluirCliente
         @CPF varchar(15)
  AS
         delete from clientes where cpf = @cpf
CREATE PROCEDURE dbo.inserir_alterar_Cliente
         @CPF varchar(15),
         @nome varchar(50),
         @endereco varchar (100),
         @telefone varchar(15),
         @flag int
  AS
         if (@flag = 1)
         begin
               insert into Clientes(cpf,nome,endereco,telefone)
               values(@CPF,@nome,@endereco,@telefone)
         end
         else begin
               update clientes 
               set nome = @nome, endereco = @endereco, telefone = @telefone
               where cpf = @cpf
         end