/*using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ItemJogMapper
    {
        private String cs;
        public ItemJogMapper()
        {
            cs = ConfigurationManager.ConnectionStrings["base dados"].ConnectionString;
            if (cs == null)
            {
                throw new Exception("Connection String must be configured in the config file!");
            }
        }

        public List<Item_jog> GetItensJog()
        {
            var itens_jog = new List<Item_jog>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cs))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("select * from ITEM_JOG", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Item_jog ij = new Item_jog
                                {
                                    id_jogador = sqlDataReader.SafeGet<int>(0),
                                    nome_item = sqlDataReader.SafeGet<string>(1),
                                    quant = sqlDataReader.SafeGet<int>(2)
                              
                                };

                                itens_jog.Add(ij);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
            return itens_jog;
        }
    }
}*/

