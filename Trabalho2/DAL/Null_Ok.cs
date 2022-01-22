using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class Null_Ok
    {
        public static T SafeGet<T>(this SqlDataReader reader, int col)
        {
            return reader.IsDBNull(col) ? default(T) : reader.GetFieldValue<T>(col);
        }
    }
}
