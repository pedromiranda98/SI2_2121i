--USE L51NG7

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

SELECT * FROM Intervencao
--Working
EXEC UpdateInterStatus @id = 100000001, @novo_estado = "em execução"
--Error
EXEC UpdateInterStatus @id = 200000000, @novo_estado = "em execução"