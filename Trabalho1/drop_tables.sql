USE Maintain4ver;

GO

CREATE OR ALTER PROCEDURE drop_tables
AS
	DROP TABLE IF EXISTS CompetenciaColaborador
	DROP TABLE IF EXISTS Competencia;
	DROP TABLE IF EXISTS ColaboradorEquipa;
	DROP TABLE IF EXISTS IntervencaoEquipa;
	DROP TABLE IF EXISTS Intervencao;
	DROP TABLE IF EXISTS Equipa;
	DROP TABLE IF EXISTS Funcionario;
	DROP TABLE IF EXISTS Ativo;
	DROP TABLE IF EXISTS Tipo;
GO

EXEC drop_tables;