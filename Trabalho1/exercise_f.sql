--USE L51NG7

GO
CREATE OR ALTER FUNCTION getAvailableTeam(@interv_desc nvarchar(10))
RETURNS numeric(5,0) 
AS
	BEGIN
		DECLARE @codigo numeric(5,0) 
		IF EXISTS (SELECT * FROM Equipa WHERE intervencoes_atribuidas<3)
			BEGIN
				set @codigo = (SELECT TOP(1) id FROM Equipa WHERE intervencoes_atribuidas < 3)
			END
		RETURN @codigo
	END 
GO

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
				DECLARE @estado varchar(20)		-- os valores possíveis sao “por atribuir”, “em análise”, “em execução” ou “concluído”
				DECLARE @id_equipa numeric(5)
				DECLARE @id numeric(9)
				SET @id = dbo.generateInterId()
				SET @id_equipa = dbo.getAvailableTeam(@descricao)
				--PRINT(@id_equipa)
				IF (@id_equipa is null)
				BEGIN
					PRINT('There is no team available at the moment');
					SET @estado = 'por atribuir'
					INSERT INTO  Intervencao(id, descricao, estado, valor, data_inicio, data_fim, periodicidade, ativo_id) VALUES (@id, @descricao, @estado, @valor, @data_inicio, @data_fim, @periodicidade, @ativo_id)
				END
				ELSE
				BEGIN
					INSERT INTO Intervencao(id, descricao, estado, valor, data_inicio, data_fim, periodicidade, ativo_id) VALUES (@id, @descricao, @estado, @valor, @data_inicio, @data_fim, @periodicidade, @ativo_id)
					INSERT INTO IntervencaoEquipa(data_inicio, data_fim, id_equipa, id_intervencao) VALUES (@data_inicio, @data_fim, @id_equipa, @id)
					UPDATE Equipa SET intervencoes_atribuidas = intervencoes_atribuidas+1 WHERE id = @id_equipa
				END
			END
		COMMIT;
		END TRY
		BEGIN CATCH
			ROLLBACK;
			THROW;
		END CATCH
GO

------------------------------------ TESTES ------------------------------------

EXEC p_criaInter @ativo_id = 000000001, @valor = 28.10 , @data_inicio = '2000-11-10', @data_fim = '2000-11-15', @periodicidade = 5, @descricao = 'avaria'

SELECT * FROM Ativo
SELECT * FROM Intervencao
SELECT * FROM IntervencaoEquipa

DELETE FROM IntervencaoEquipa WHERE id_intervencao = 100000004
DELETE FROM Intervencao WHERE id = 100000004
