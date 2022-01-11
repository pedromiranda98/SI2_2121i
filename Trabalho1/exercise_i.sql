--USE L51NG7

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

SELECT * FROM ListInterByYear(2017)
SELECT * FROM Intervencao
DROP FUNCTION ListInterByYear