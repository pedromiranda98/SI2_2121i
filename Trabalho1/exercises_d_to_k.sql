--USE L51NG7

--------------------------------------------------------------EXERCICIO D--------------------------------------------------------------

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

--------------------------------------------------------------EXERCICIO E--------------------------------------------------------------

GO
CREATE OR ALTER FUNCTION getAvailableTeam(@interv_desc nvarchar(10))
RETURNS numeric(5,0) 
AS
	BEGIN
		DECLARE @codigo numeric(5,0) 
		IF EXISTS (SELECT * FROM Equipa WHERE intervencoes_atribuidas<3)
			BEGIN
				SET @codigo = (SELECT TOP(1) id FROM Equipa ORDER BY n_elementos ASC)
			END
		RETURN @codigo
	END
GO

--------------------------------------------------------------EXERCICIO F--------------------------------------------------------------

GO
CREATE OR ALTER FUNCTION generateInterId()
RETURNS numeric(9)
AS
	BEGIN
		DECLARE @numero numeric(9)
		SET @numero = (SELECT MAX(id) FROM Intervencao)			
		RETURN @numero+1
	END 
GO

GO
CREATE OR ALTER PROCEDURE p_criaInter
		@ativo_id numeric(9),
		@valor decimal(8,2),
		@data_inicio date,
		@data_fim date,
		@periodicidade int,
		@descricao varchar(10)
AS
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- DOUBLE CHECK
	BEGIN TRANSACTION 
		BEGIN TRY
			IF (@ativo_id is null)
				PRINT('Ativo ID missing');
			ELSE
			BEGIN
				IF NOT EXISTS (SELECT * FROM Ativo WHERE id = @ativo_id)
				BEGIN
					PRINT('Ativo ID does not exist in the database')
				END 
				ELSE
				BEGIN
					DECLARE @estado varchar(20)		-- os valores possíveis sao “por atribuir”, “em análise”, “em execução” ou “concluído”
					DECLARE @id_equipa numeric(5)
					DECLARE @id numeric(9)
					SET @id = dbo.generateInterId()
					SET @id_equipa = dbo.getAvailableTeam(@descricao)
					IF (@id_equipa is null)
					BEGIN
						PRINT('There is no team available at the moment');
						SET @estado = 'por atribuir'
						INSERT INTO  Intervencao(id, descricao, estado, valor, data_inicio, data_fim, periodicidade, ativo_id) VALUES (@id, @descricao, @estado, @valor, @data_inicio, @data_fim, @periodicidade, @ativo_id)
					END
					ELSE
					BEGIN
						SET @estado = 'em análise'
						INSERT INTO Intervencao(id, descricao, estado, valor, data_inicio, data_fim, periodicidade, ativo_id) VALUES (@id, @descricao, @estado, @valor, @data_inicio, @data_fim, @periodicidade, @ativo_id)
						INSERT INTO IntervencaoEquipa(data_inicio, data_fim, id_equipa, id_intervencao) VALUES (@data_inicio, @data_fim, @id_equipa, @id)
						UPDATE Equipa SET intervencoes_atribuidas = intervencoes_atribuidas+1 WHERE id = @id_equipa
					END
				END
			END
		COMMIT;
		END TRY
		BEGIN CATCH
			ROLLBACK;
			THROW;
		END CATCH
GO

--------------------------------------------------------------EXERCICIO G--------------------------------------------------------------

GO
CREATE OR ALTER FUNCTION generateTeamId()
RETURNS numeric(5)
AS
	BEGIN
		DECLARE @numero numeric(5)
		SET @numero = (SELECT MAX(id) FROM Equipa)			
		RETURN @numero+1
	END 
GO


GO

CREATE OR ALTER PROCEDURE addNewTeam
		@localizacao varchar(50)
		--@intervencoes_atribuidas int

AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	BEGIN TRANSACTION  
		BEGIN TRY
			DECLARE @id numeric(5)
			DECLARE @n_elementos int
			DECLARE @intervencoes_atribuidas int
			SET @id = [dbo].generateTeamId()
			SET @n_elementos = 0
			SET @intervencoes_atribuidas = 0
			INSERT INTO Equipa(id, localizacao, n_elementos, intervencoes_atribuidas) VALUES (@id, @localizacao, @n_elementos, @intervencoes_atribuidas);
			COMMIT;
		END TRY
		BEGIN CATCH
			ROLLBACK;
			THROW;
		END CATCH
GO

--------------------------------------------------------------EXERCICIO H--------------------------------------------------------------

GO
CREATE OR ALTER PROCEDURE updateTeamElements
        @id numeric(9),
        @id_equipa numeric(5),
        @id_competencia numeric(9),
        @delete_or_add  int
AS
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
    BEGIN TRANSACTION 
        BEGIN TRY
            IF (@id is null OR @id_equipa is null)
                PRINT('Missing parameters');
            IF EXISTS (SELECT * FROM Equipa WHERE id = @id_equipa)
            BEGIN
                IF NOT EXISTS (SELECT * FROM Funcionario WHERE id = @id)
                    PRINT('Employee does not exist in the database');
                ELSE
                BEGIN
                    IF EXISTS (SELECT * FROM ColaboradorEquipa WHERE id = @id AND id_equipa = @id_equipa) AND (@delete_or_add=0)
                    BEGIN
                        DELETE FROM CompetenciaColaborador WHERE id_colaborador = @id
                        DELETE FROM ColaboradorEquipa WHERE id = @id
                        UPDATE Equipa SET n_elementos = n_elementos-1 WHERE id = @id_equipa
                        PRINT('Employee deleted from the team')
                    END
                    ELSE IF NOT EXISTS (SELECT * FROM ColaboradorEquipa WHERE id = @id ) AND (@delete_or_add=1)
                    BEGIN
                        INSERT INTO ColaboradorEquipa(id, id_equipa) VALUES (@id, @id_equipa)
                        INSERT INTO CompetenciaColaborador(id_competencia, id_colaborador, id_equipa) VALUES (@id_competencia, @id, @id_equipa)
                        UPDATE Equipa SET n_elementos = n_elementos+1 WHERE id = @id_equipa
                        PRINT('Employee added to the team')
                    END
                    ELSE IF NOT EXISTS (SELECT * FROM ColaboradorEquipa WHERE id = @id ) AND (@delete_or_add=0)
                    BEGIN
                        PRINT('Employee not in this team')    
                    END
					ELSE IF EXISTS (SELECT * FROM ColaboradorEquipa WHERE id = @id AND id_equipa != @id_equipa) AND (@delete_or_add=0)
                    BEGIN
                        PRINT('Inserted team is incorrect')    
                    END
					ELSE IF EXISTS (SELECT * FROM ColaboradorEquipa WHERE id = @id AND id_equipa != @id_equipa) AND (@delete_or_add=1)
                    BEGIN
                        PRINT('Employee is already part of a team')    
                    END
                    ELSE
                    BEGIN
                        Print('Employee is already in that team')
                    END
                END
            END
            ELSE PRINT('Team does not exist in the database')
                COMMIT;
        END TRY
        BEGIN CATCH
            ROLLBACK;
            THROW;
        END CATCH
GO

--------------------------------------------------------------EXERCICIO I--------------------------------------------------------------

GO

CREATE OR ALTER FUNCTION ListInterByYear(@ano numeric(4))
RETURNS @ret TABLE (
        id numeric(9),
		descricao varchar(10)
)
AS
BEGIN
    INSERT INTO @ret SELECT id, descricao FROM Intervencao WHERE YEAR(data_fim) = @ano
    RETURN
END

GO

--------------------------------------------------------------EXERCICIO J--------------------------------------------------------------

GO
CREATE OR ALTER PROCEDURE UpdateInterStatus
    @id numeric(9),
	@novo_estado varchar(20)
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED -- DOUBLE CHECK
    BEGIN TRANSACTION
        BEGIN TRY
			IF EXISTS (SELECT estado FROM Intervencao WHERE id = @id)
				BEGIN
					UPDATE Intervencao SET estado = @novo_estado WHERE id = @id
				END
			ELSE PRINT 'There is no intervention to update'
	COMMIT;
		END TRY
		BEGIN CATCH
			ROLLBACK;
			THROW;
		END CATCH
GO

--------------------------------------------------------------EXERCICIO K--------------------------------------------------------------

GO

CREATE OR ALTER VIEW ResumoInter
AS
	SELECT Intervencao.id AS inter_id, descricao, Intervencao.estado AS inter_estado, valor, data_inicio, data_fim, periodicidade, ativo_id, 
		parent_id, nome, data_aquisicao, marca, modelo, localizacao, Ativo.estado AS ativo_estado, id_tipo 
	FROM Intervencao INNER JOIN Ativo on Intervencao.ativo_id = Ativo.id

GO

GO
CREATE OR ALTER TRIGGER TriggerEstado ON ResumoInter
    instead of INSERT, DELETE, UPDATE
AS
	BEGIN TRANSACTION
	IF UPDATE(inter_estado)
		BEGIN
			PRINT 'Column inter_estado was updated'
			COMMIT TRANSACTION
		END
	ELSE
		BEGIN
			raiserror('ERROR: You can only update inter_estado',16,1)
			ROLLBACK TRANSACTION
		END
GO