--------------------------------------------------------------EXERCICIO D--------------------------------------------------------------

--------------------------------------------------------------EXERCICIO E--------------------------------------------------------------

DECLARE @id_equipa numeric(5)
SET @id_equipa = dbo.getAvailableTeam('description')
PRINT(@id_equipa)

--------------------------------------------------------------EXERCICIO F--------------------------------------------------------------

EXEC p_criaInter @ativo_id = 000000001, @valor = 28.10 , @data_inicio = '2000-11-10', @data_fim = '2000-11-15', @periodicidade = 5, @descricao = 'avaria'

SELECT * FROM Ativo
SELECT * FROM Intervencao
SELECT * FROM IntervencaoEquipa

DELETE FROM IntervencaoEquipa WHERE id_intervencao = 100000004
DELETE FROM Intervencao WHERE id = 100000004

--------------------------------------------------------------EXERCICIO G--------------------------------------------------------------

EXEC addNewTeam @localizacao = 'Lisboa'

SELECT * FROM Equipa;

DELETE FROM Equipa WHERE id= 30001;

--------------------------------------------------------------EXERCICIO H--------------------------------------------------------------

SELECT * FROM Equipa;
SELECT * FROM ColaboradorEquipa;
SELECT * FROM CompetenciaColaborador;
SELECT * FROM Funcionario

-- Apaga
EXEC updateTeamElements @id = 111222555, @id_equipa = 30000, @id_competencia = 123, @delete_or_add = 0
-- Não pertence a essa equipa
EXEC updateTeamElements @id = 111222444, @id_equipa = 20000, @id_competencia = 123, @delete_or_add = 0
-- Não existe na base de dados
EXEC updateTeamElements @id = 111222999, @id_equipa = 30000, @id_competencia = 123, @delete_or_add = 0

-- Adiciona
EXEC updateTeamElements @id = 111222555, @id_equipa = 30000, @id_competencia = 123, @delete_or_add = 1
-- Já pertence a uma equipa
EXEC updateTeamElements @id = 111222444, @id_equipa = 20000, @id_competencia = 123, @delete_or_add = 1
-- Já pertence a essa equipa
EXEC updateTeamElements @id = 111222333, @id_equipa = 30000, @id_competencia = 123, @delete_or_add = 1


--------------------------------------------------------------EXERCICIO I--------------------------------------------------------------

SELECT * FROM ListInterByYear(2017)

SELECT * FROM Intervencao
DROP FUNCTION ListInterByYear

--------------------------------------------------------------EXERCICIO J--------------------------------------------------------------

--Working (estado do ativo 100000002 passa de "em análise" para "em execução")
EXEC UpdateInterStatus @id = 100000002, @novo_estado = "em execução"
--Error (a intervenção não existe)
EXEC UpdateInterStatus @id = 200000000, @novo_estado = "em execução"

SELECT * FROM Intervencao

--------------------------------------------------------------EXERCICIO K--------------------------------------------------------------

UPDATE ResumoInter
SET ResumoInter.inter_estado = 'em execução'
WHERE ResumoInter.inter_id = 100000003

SELECT * FROM ResumoInter
SELECT * FROM Intervencao

DROP VIEW ResumoInter
DROP TRIGGER TriggerEstado