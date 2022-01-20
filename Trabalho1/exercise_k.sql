--USE L51NG7
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

UPDATE ResumoInter
	SET ResumoInter.inter_estado = 'em execução'
	WHERE ResumoInter.inter_id = 100000003

SELECT * FROM ResumoInter
SELECT * FROM Intervencao
--drop view ResumoInter
--drop trigger TriggerEstado
