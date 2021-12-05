GO
-------------------------------INSERIR PESSOA-------------------------------

CREATE OR ALTER PROCEDURE AddNewPerson
	@id numeric(9,0),
	@email varchar(25),
	@telemovel numeric(9,0),
	@nome varchar(50),
	@data_nascimento date,
	@endereco varchar(100),
	@profissao varchar(20)
	

AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- DOUBLE CHECK
	BEGIN TRANSACTION  
		BEGIN TRY
			IF (@id is null OR @email is null OR @telemovel is null OR @nome is null OR @data_nascimento is null OR @endereco is null OR @profissao is null)
				RAISERROR('Missing parameters', 15, 1);
			INSERT INTO Funcionario (id, email, telemovel, nome, data_nascimento, endereco, profissao) VALUES (@id ,@email,@telemovel,@nome,@data_nascimento,@endereco,@profissao);
			COMMIT;
		END TRY
		BEGIN CATCH 
			ROLLBACK;
			THROW;
		END CATCH
GO

-------------------------------EDITAR PESSOA-------------------------------

CREATE OR ALTER PROCEDURE EditPerson
	@id numeric(9,0),
	@email varchar(25),
	@telemovel numeric(9,0),
	@nome varchar(50),
	@data_nascimento date,
	@endereco varchar(100),
	@profissao varchar(20)
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED 
	BEGIN TRANSACTION 
		BEGIN TRY
			IF (@id is null OR @email is null OR @telemovel is null OR @nome is null OR @data_nascimento is null OR @endereco is null OR @profissao is null)
				RAISERROR('Missing parameters', 15, 1);
			IF NOT EXISTS (SELECT * FROM Funcionario WHERE id=@id)
				RAISERROR('That person doesn''t exist', 15, 1);
			UPDATE Funcionario
				SET email = @email, telemovel=@telemovel, nome=@nome, data_nascimento = @data_nascimento, endereco = @endereco, profissao = @profissao
				WHERE id = @id;
			COMMIT;
		END TRY
		BEGIN CATCH
			ROLLBACK;
			THROW;
		END CATCH
GO

-------------------------------REMOVER PESSOA-------------------------------
GO
	CREATE OR ALTER PROCEDURE RemovePerson
		@id numeric(9,0)
AS 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 
BEGIN TRANSACTION 
	BEGIN TRY 
		IF(@id is null) 
			RAISERROR('Missing id parameter', 15,1);
		IF EXISTS (SELECT * FROM ColaboradorEquipa WHERE id=@id)
			PRINT('This person is inserted in a team and cannot be deleted');
		ELSE
			DELETE FROM Funcionario WHERE id = @id;
		COMMIT;
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
GO
