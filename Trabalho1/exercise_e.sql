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