IF(db_id(N'Maintain4ver') IS NULL) CREATE DATABASE Maintain4ver; -- Create the database if it doesn't exist yet

USE Maintain4ver;

GO

CREATE OR ALTER PROCEDURE CreateTables
AS

	CREATE TABLE Funcionario (
		id numeric(9) primary key,
		email varchar(25),
		telemovel numeric(9),
		nome varchar(50),
		data_nascimento date, -- dd-mm-aaaa
		endereco varchar(100),
		profissao varchar(20)
	);
	
	CREATE TABLE Tipo (
		id numeric(5) primary key,
		descricao varchar(100),
	);

	CREATE TABLE Ativo (
		id numeric(9) primary key,
		parent_id numeric(9) references Ativo(id),
		nome varchar(50) not null,
		data_aquisicao date not null, -- dd-mm-aaaa
		marca varchar(20),
		modelo varchar(20),
		localizacao varchar(50) not null,
		estado numeric(1) not null check ((estado=0) or (estado=1)), -- Pode ser '0' (desativado) ou '1' (operacional)
		id_tipo numeric(5) references Tipo(id)
	);
	
	CREATE TABLE Equipa(
		id numeric(5) primary key,
		localizacao varchar(50),
		n_elementos int,
		intervencoes_atribuidas int check (intervencoes_atribuidas < 4)
	);
	
	CREATE TABLE Intervencao (
		id numeric(9) primary key,
		descricao varchar(10), -- pode tomar como valores “avaria”, “rutura” ou “inspeção”
		estado varchar(20), -- os valores possíveis s˜ao “por atribuir”, “em análise”, “em execução” ou “concluído”
		valor decimal(8,2),
		data_inicio date,
		data_fim date,
		periodicidade int,
		ativo_id numeric(9) references Ativo(id)
	);

	CREATE TABLE IntervencaoEquipa (
		data_inicio date,
		data_fim date,
		id_equipa numeric(5) references Equipa(id),
		id_intervencao numeric(9) references Intervencao(id),
		primary key(id_equipa, id_intervencao)
	);
	
	CREATE TABLE Competencia (
		id numeric(10) primary key,
		descricao varchar(50)
	);

	CREATE TABLE ColaboradorEquipa (
		id numeric(9) references Funcionario(id),
		id_equipa numeric(5) references Equipa(id),
		primary key(id, id_equipa)
	);

	CREATE TABLE CompetenciaColaborador (
		id_competencia numeric(10) references Competencia(id),
		id_colaborador numeric(9),
		id_equipa numeric(5),
		primary key(id_competencia, id_colaborador, id_equipa),
		foreign key(id_colaborador, id_equipa) references ColaboradorEquipa(id, id_equipa)
	);
	
GO

EXEC CreateTables;