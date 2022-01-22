--USE L51NG7

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
					IF EXISTS (SELECT * FROM ColaboradorEquipa WHERE id = @id ) AND (@delete_or_add=0)
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
					ELSE
					BEGIN
						Print('Employee already in the team')
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

------------------------------------------ TESTES ------------------------------------------

SELECT * FROM Equipa;
SELECT * FROM ColaboradorEquipa;
SELECT * FROM CompetenciaColaborador;
SELECT * FROM Funcionario

--UPDATE Equipa SET n_elementos = 2 WHERE id = 30000

-- Adiciona
EXEC updateTeamElements @id = 111222333, @id_equipa = 30000, @id_competencia = 123, @delete_or_add = 1

-- Apaga
EXEC updateTeamElements @id = 111222555, @id_equipa = 30000, @id_competencia = 123, @delete_or_add = 0