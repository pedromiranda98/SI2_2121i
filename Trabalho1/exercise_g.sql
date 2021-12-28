USE L51NG7

GO
CREATE OR ALTER FUNCTION getNextTeamId()
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
			SET @id = [dbo].getNextTeamId()
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

EXEC addNewTeam @localizacao = 'Lisboa'

SELECT * FROM Equipa;

DELETE FROM Equipa WHERE id= 30001;

--DROP PROCEDURE addNewTeam