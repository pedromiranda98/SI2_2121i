
GO

CREATE OR ALTER PROCEDURE PopulateTables
AS
	INSERT INTO Funcionario (id, email, telemovel, nome, data_nascimento, endereco, profissao) VALUES
		(111222444, 'peters@mail.pt', 961111111, 'Pedro', '1998-05-01', 'Rua 122 1111-123 Torres Vedras', 'Software Dev'),
		(111222333, 'ricardians@mail.pt', 962222222, 'Ricardo', '1997-06-24', 'Rua 123 2222-123 Almada', 'Software Dev'),
		(111222555, 'ineis@mail.pt', 963333333, 'Inês', '1997-02-14', 'Rua 124 3333-123 Mafra', 'Software Dev'),
		(111222666, 'manuel@mail.pt', 760300600, 'Manuel', '1977-02-23', 'Rua 120 1234-123 Lisboa', 'Operations'),
		(111222777, 'firight@mail.pt', 912345678, 'Diogo', '1997-12-14', 'Rua 125 3333-125 Ericeira', 'Support'),
		(111222888, 'maria@mail.pt', 962992111, 'Maria', '1989-11-04', 'Rua das Ameixas 2000-100 Loures', 'Support')

	INSERT INTO Tipo(id, descricao) VALUES
		(10101, 'Piscina'),
		(10102, 'Veículo')

	INSERT INTO Ativo (id, parent_id, nome, data_aquisicao, marca, modelo, localizacao, estado, id_tipo) VALUES
		(123456789, null, 'Piscina', '2017-11-10', null, null, 'Lisboa', 1, 10101),
		(000000001, 123456789, 'Bomba de água', '2017-11-14', 'Koma tools', '4000c', 'Lisboa', 0, 10101),
		(000000002, null, 'Carro', '2017-06-14', 'BMW', 'Serie 1', 'Porto', 1, 10102),
		(000000003, 000000002, 'Motor', '2017-02-16', 'BMW', 'Serie 1', 'Porto', 1, 10102)
		
	INSERT INTO Intervencao (id, descricao, estado, valor, data_inicio, data_fim, periodicidade, ativo_id) VALUES
		(100000001, 'avaria', 'em análise', 999.99, '2017-11-03', '2017-11-15', 3, 000000003),
		(100000002, 'avaria', 'em análise', 50000, '2017-11-09', '2017-11-15', 3, 000000002),
		(100000003, 'inspeção', 'em análise', 1000.00, '2017-11-07', '2017-11-15', 6, 123456789)
		
	INSERT INTO Equipa(id, localizacao, n_elementos, intervencoes_atribuidas) VALUES
		(10000, 'Chelas', 1, 1),
		(20000, 'Almada', 2, 1),
		(30000, 'Torres Vedras', 3, 1)

	INSERT INTO IntervencaoEquipa(data_inicio, data_fim, id_equipa, id_intervencao) VALUES
		('2017-11-03', '2017-11-15', 10000, 100000001),
		('2017-11-03', '2017-11-09', 20000, 100000002),
		('2017-05-01', '2017-11-06', 30000, 100000003)

	INSERT INTO Competencia(id, descricao) VALUES
		(123, 'Faturação'),
		(456, 'Contabilidade'),
		(789, 'Manutenção')

	INSERT INTO ColaboradorEquipa(id, id_equipa) VALUES
		(111222444, 30000),
		(111222333, 30000),
		(111222555, 30000),
		(111222666, 20000),
		(111222777, 20000),
		(111222888, 10000)

	INSERT INTO CompetenciaColaborador(id_competencia,id_colaborador,id_equipa) VALUES
		(123, 111222444, 30000),
		(123, 111222333, 30000),
		(456, 111222555, 30000),
		(789, 111222666, 20000),
		(123, 111222777, 20000),
		(456, 111222888, 10000)
GO

EXEC PopulateTables;

SELECT * FROM Funcionario;
SELECT * FROM Tipo;
SELECT * FROM Ativo;
SELECT * FROM Intervencao;
SELECT * FROM Equipa;
SELECT * FROM IntervencaoEquipa;
SELECT * FROM Competencia;
SELECT * FROM ColaboradorEquipa;
SELECT * FROM CompetenciaColaborador;


		