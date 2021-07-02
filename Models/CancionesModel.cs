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
        public static Cancion CreateNew(Cancion CancionNueva, out string Fallas)
        {
            Fallas = "";
            MySqlCommand miComando = new MySqlCommand();
            miComando.Connection = Util.getConnection();
            miComando.CommandType = CommandType.Text;
            miComando.CommandText = "insert into canciones (Titulo, Genero, Lanzamiento) values (@Titulo, @Genero, @Lanzamiento)";
            miComando.Parameters.AddWithValue("@Titulo", CancionNueva.Titulo);
            miComando.Parameters.AddWithValue("@Genero", CancionNueva.Genero);
            miComando.Parameters.AddWithValue("@Lanzamiento", CancionNueva.Lanzamiento);

            try
            {
                miComando.ExecuteNonQuery();

                miComando.CommandText = "select max(Id) as nuevo_Id from canciones";
                miComando.Parameters.Clear();
                CancionNueva.Id = (int)miComando.ExecuteScalar();

                MySqlDataReader Lector = miComando.ExecuteReader();

                if (Lector.HasRows)
                {
                    Console.WriteLine("Lector tiene columnas");
                } else
                {
                    Console.WriteLine("lector no tiene columnas");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Ocurrió un error - codigo: " + e.Number);
                if (e.Number == 1062)
                {
                    Fallas = "Registro duplicado";
                } else
                {
                    Fallas = e.Message;
                }
                CancionNueva = null;
            }
            catch (SystemException e)
            {
                Console.WriteLine("Ocurrió un error: " + e.Message);
                CancionNueva = null;
            }
            return CancionNueva;
        }

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
