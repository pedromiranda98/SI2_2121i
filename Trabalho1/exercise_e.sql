GO

GO
CREATE OR ALTER FUNCTION getAvailableTeam(@interv_desc nvarchar(10))
RETURNS numeric(5,0) 
AS
	BEGIN
		DECLARE @codigo numeric(5,0) 
		
		BEGIN
			IF exists (SELECT * FROM Equipa WHERE intervencoes_atribuidas=0)
				BEGIN
					set @codigo = (SELECT TOP(1) id FROM Equipa WHERE intervencoes_atribuidas = 0)
				END
			ELSE
				RAISERROR('There is no team available at the moment', 15, 1);
		END
	END 
GO