USE SI2Trab1

GO

CREATE OR ALTER PROCEDURE updateTeamElements
		@id numeric(9),
		@id_equipa numeric(5),
		@id_competencia numeric(9)
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
					IF EXISTS (SELECT * FROM ColaboradorEquipa WHERE id = @id)
					BEGIN
						DELETE FROM CompetenciaColaborador WHERE id_colaborador = @id
						DELETE FROM ColaboradorEquipa WHERE id = @id
					END
					ELSE
					BEGIN
						INSERT INTO ColaboradorEquipa(id, id_equipa) VALUES (@id, @id_equipa)
						INSERT INTO CompetenciaColaborador(id_competencia, id_colaborador, id_equipa) VALUES (@id_competencia, @id, @id_equipa)
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

SELECT * FROM ColaboradorEquipa;
SELECT * FROM CompetenciaColaborador;

-- Já existente (Apaga os registos com estes dados)
EXEC updateTeamElements @id = 111222333, @id_equipa = 30000, @id_competencia = 123
-- Não existente (Insere os dados nas tabelas)
EXEC updateTe