using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ApiNetCore.Utils;

namespace ApiNetCore.Models
{

    public class CancionesModel
    {
        public static Cancion getByTitulo (string TituloABuscar)
        {
            MySqlCommand miComando = new MySqlCommand();
            miComando.Connection = Util.getConnection();
            miComando.CommandType = CommandType.Text;
            miComando.CommandText = "Select * from canciones where Titulo=@Titulo";
            miComando.Parameters.AddWithValue("@Titulo", TituloABuscar);

            DataTable miTabla = new DataTable();
            MySqlDataAdapter miAdaptador = new MySqlDataAdapter();
            miAdaptador.SelectCommand = miComando;
            miAdaptador.Fill(miTabla);

            Cancion unaCancion = new Cancion();

            if (miTabla.Rows.Count > 0)
            {
                unaCancion.Titulo = TituloABuscar;
                unaCancion.Id = (int)miTabla.Rows[0]["Id"];
                unaCancion.Genero = (string)miTabla.Rows[0]["Genero"];
            } else
            {
                unaCancion = null;
            }

            return unaCancion;
        }
        public static List<Cancion> getAll()
        {
            List<Cancion> ListaADevolver = new List<Cancion>();

            MySqlCommand miComando = new MySqlCommand();
            miComando.Connection = Util.getConnection();
            miComando.CommandType = CommandType.Text;
            miComando.CommandText = "Select * from canciones";

            DataTable miTabla = new DataTable();
            MySqlDataAdapter miAdaptador = new MySqlDataAdapter();
            miAdaptador.SelectCommand = miComando;
            miAdaptador.Fill(miTabla);

            foreach (DataRow index in miTabla)
            {
                Cancion unaCancion = new Cancion();
                unaCancion.Id = (int)index["Id"];
                unaCancion.Titulo = (string)index["Titulo"];
                unaCancion.Genero = (string)index["Genero"];
                ListaADevolver.Add(unaCancion);
            }

            return ListaADevolver;
        }

        public class Cancion
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string  Genero { get; set; }
            public int Lanzamiento { get; set; }
        }
    }
}
