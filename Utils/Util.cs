using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNetCore.Utils
{
    public static class Util
    {
        public static MySqlConnection getConnection()
        {
            MySqlConnection miConexion = new MySqlConnection();
            miConexion.ConnectionString = "Server=127.0.0.1;Uid=root;Pwd=1234;Database=SpotifyLocalBDD";
            miConexion.Open();
            return miConexion;
        }
    }
}
