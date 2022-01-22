using System;
using System.Configuration;

namespace Services
{
    public static class Session
    {
        public static String GetConnectionString()
        {
            String cs = ConfigurationManager.ConnectionStrings["base dados"].ConnectionString;
            if (cs == null)
            {
                throw new Exception("Connection String must be configured in the config file!");
            }
            return cs;
        }

    }
}
