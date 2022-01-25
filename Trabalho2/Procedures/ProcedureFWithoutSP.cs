using Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DAL;

namespace Procedures
{
    public class ProcedureFWithoutSP
    {
        private readonly string cs;

        public ProcedureFWithoutSP()
        {
            cs = Session.GetConnectionString();
        }
        public void createIntervention(int ativo_id, decimal valor, DateTime data_inicio, DateTime data_fim, int periodicidade, string descricao)
        {
            var ativoState = false;
            var estado = "por atribuir";
            try
            {
                using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(cs))
                    {
                        sqlConnection.Open();

                        using (SqlCommand sqlCommand = new SqlCommand("select id from Ativo where id = @ativo_id", sqlConnection))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            SqlParameter idParam = sqlCommand.Parameters.Add(new SqlParameter("@ativo_id", ativo_id));

                            if(sqlCommand.ExecuteScalar() != null)
                            {
                                ativoState = true;
                            }
                        }

                        if (ativoState)
                        {
                            decimal interId = 0;
                            using (SqlCommand sqlCommand = new SqlCommand("select dbo.generateInterId()", sqlConnection))
                            {
                                sqlCommand.CommandType = CommandType.Text;
                                interId = (decimal)sqlCommand.ExecuteScalar();
                            }

                            sqlConnection.Close();
                            var getAvailableTeam = new ProcedureE();
                            var teamId = getAvailableTeam.GetAvailableTeam(descricao);

                            sqlConnection.Open();

                            if (teamId != 0)
                            {
                                estado = "em análise";
                                using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO  Intervencao(id, descricao, estado, valor, data_inicio, data_fim, periodicidade, ativo_id) VALUES (@id, @descricao, @estado, @valor, @data_inicio, @data_fim, @periodicidade, @ativo_id)", sqlConnection))
                                {
                                    sqlCommand.CommandType = CommandType.Text;

                                    SqlParameter p1 = sqlCommand.Parameters.Add(new SqlParameter("@id", interId));
                                    SqlParameter p2 = sqlCommand.Parameters.Add(new SqlParameter("@descricao", descricao));
                                    SqlParameter p3 = sqlCommand.Parameters.Add(new SqlParameter("@estado", estado));
                                    SqlParameter p4 = sqlCommand.Parameters.Add(new SqlParameter("@valor", valor));
                                    SqlParameter p5 = sqlCommand.Parameters.Add(new SqlParameter("@data_inicio", data_inicio));
                                    SqlParameter p6 = sqlCommand.Parameters.Add(new SqlParameter("@data_fim", data_fim));
                                    SqlParameter p7 = sqlCommand.Parameters.Add(new SqlParameter("@periodicidade", periodicidade));
                                    SqlParameter p8 = sqlCommand.Parameters.Add(new SqlParameter("@ativo_id", ativo_id));

                                    sqlCommand.ExecuteNonQuery();
                                }

                                using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO IntervencaoEquipa(data_inicio, data_fim, id_equipa, id_intervencao) VALUES (@data_inicio, @data_fim, @id_equipa, @id)", sqlConnection))
                                {
                                    sqlCommand.CommandType = CommandType.Text;

                                    SqlParameter p1 = sqlCommand.Parameters.Add(new SqlParameter("@id", interId));
                                    SqlParameter p2 = sqlCommand.Parameters.Add(new SqlParameter("@data_inicio", data_inicio));
                                    SqlParameter p3 = sqlCommand.Parameters.Add(new SqlParameter("@data_fim", data_fim));
                                    SqlParameter p4 = sqlCommand.Parameters.Add(new SqlParameter("@id_equipa", teamId));

                                    sqlCommand.ExecuteNonQuery();
                                }

                                using (SqlCommand sqlCommand = new SqlCommand("UPDATE Equipa SET intervencoes_atribuidas = intervencoes_atribuidas+1 WHERE id = @id_equipa", sqlConnection))
                                {
                                    sqlCommand.CommandType = CommandType.Text;

                                    SqlParameter p1 = sqlCommand.Parameters.Add(new SqlParameter("@id_equipa", teamId));

                                    sqlCommand.ExecuteNonQuery();
                                }
                            }

                            else
                            {
                                using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO  Intervencao(id, descricao, estado, valor, data_inicio, data_fim, periodicidade, ativo_id) VALUES (@id, @descricao, @estado, @valor, @data_inicio, @data_fim, @periodicidade, @ativo_id)", sqlConnection))
                                {
                                    sqlCommand.CommandType = CommandType.Text;

                                    SqlParameter p1 = sqlCommand.Parameters.Add(new SqlParameter("@id", interId));
                                    SqlParameter p2 = sqlCommand.Parameters.Add(new SqlParameter("@descricao", descricao));
                                    SqlParameter p3 = sqlCommand.Parameters.Add(new SqlParameter("@estado", estado));
                                    SqlParameter p4 = sqlCommand.Parameters.Add(new SqlParameter("@valor", valor));
                                    SqlParameter p5 = sqlCommand.Parameters.Add(new SqlParameter("@data_fim", data_fim));
                                    SqlParameter p6 = sqlCommand.Parameters.Add(new SqlParameter("@periodicidade", periodicidade));
                                    SqlParameter p7 = sqlCommand.Parameters.Add(new SqlParameter("@descricao", descricao));
                                    SqlParameter p8 = sqlCommand.Parameters.Add(new SqlParameter("@ativo_id", ativo_id));

                                    sqlCommand.ExecuteNonQuery();
                                }
                            }
                        }
                        else
                        {
                            Console.Write("Ativo ID does not exist in the database");
                        }
                        sqlConnection.Close();
                    }
                    ts.Complete();
                }
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                throw exception;
            }
        }
    }
}

