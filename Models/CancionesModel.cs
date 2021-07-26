﻿using MySql.Data.MySqlClient;
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
        public static Cancion CreateNew (Cancion cancionNueva, out string Fallas)
        {
            Fallas = "";
            MySqlCommand MiComando = new MySqlCommand();
            MiComando.Connection = Util.getConnection();
            MiComando.CommandType = CommandType.Text;
            MiComando.CommandText = "insert into canciones (Titulo, Genero, Autor, Lanzamiento, UrlLetra) values (@Titulo, @Genero, @Autor, @Lanzamiento, @UrlLetra)";
            MiComando.Parameters.AddWithValue("@Titulo", cancionNueva.Titulo);
            MiComando.Parameters.AddWithValue("@Genero", cancionNueva.Genero);
            MiComando.Parameters.AddWithValue("@Autor", cancionNueva.Autor);
            MiComando.Parameters.AddWithValue("@Lanzamiento", cancionNueva.Lanzamiento);
            MiComando.Parameters.AddWithValue("@UrlLetra", cancionNueva.UrlLetra);

            try
            {
                MiComando.ExecuteNonQuery();
                try
                {
                MiComando.CommandText = "select max (id) as nuevo_id from canciones";
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ocurrio una excepcion: " + e);
                }
                
                MiComando.Parameters.Clear();
                cancionNueva.Id = (int)MiComando.ExecuteScalar();

                MySqlDataReader Lector = MiComando.ExecuteReader();

                if (Lector.HasRows)
                {
                    while (Lector.Read())
                    {
                    }
                }
            } catch (MySqlException e)
            {
                Console.WriteLine("Ocurrio un error - Codigo: " + e.Number);
                if(e.Number == 1062)
                {
                    Fallas = "Registro duplicado";
                } else
                {
                    Fallas = e.Message;
                }
                cancionNueva = null;
            } catch (SystemException e)
            {
                Console.WriteLine("Ocurrio un error - Codigo: " + e.Message);
                cancionNueva = null;
            }
            return cancionNueva;
        }

        public static Cancion Update(Cancion cancionNueva, out string Fallas)
        {
            Fallas = "";
            MySqlCommand MiComando = new MySqlCommand();
            MiComando.Connection = Util.getConnection();
            MiComando.CommandType = CommandType.Text;
            MiComando.CommandText = "insert into canciones (Id, Titulo, Genero, Autor, Lanzamiento, UrlLetra) values (@Id, @Titulo, @Genero, @Autor, @Lanzamiento, @UrlLetra)";
            MiComando.Parameters.AddWithValue("@Id", cancionNueva.Id);
            MiComando.Parameters.AddWithValue("@Titulo", cancionNueva.Titulo);
            MiComando.Parameters.AddWithValue("@Genero", cancionNueva.Genero);
            MiComando.Parameters.AddWithValue("@Autor", cancionNueva.Autor);
            MiComando.Parameters.AddWithValue("@Lanzamiento", cancionNueva.Lanzamiento);
            MiComando.Parameters.AddWithValue("@UrlLetra", cancionNueva.UrlLetra);

            try
            {
                MiComando.ExecuteNonQuery();

                MiComando.Parameters.Clear();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Ocurrio un error - Codigo: " + e.Number);
                if (e.Number == 1062)
                {
                    Fallas = "Registro no encontrado";
                }
                else
                {
                    Fallas = e.Message;
                }
                cancionNueva = null;
            }
            catch (SystemException e)
            {
                Console.WriteLine("Ocurrio un error - Codigo: " + e.Message);
                cancionNueva = null;
            }
            return cancionNueva;
        }

        public static bool DeleteById (int IdABorrar)
        {
            MySqlCommand miComando = new MySqlCommand();
            miComando.Connection = Util.getConnection();
            miComando.CommandType = CommandType.Text;
            miComando.CommandText = "delete from canciones where Id=@Id";
            miComando.Parameters.AddWithValue("@Id", IdABorrar);
            MySqlDataAdapter miAdaptador = new MySqlDataAdapter();
            miAdaptador.SelectCommand = miComando;


            Cancion cancionABuscar = new Cancion();
            cancionABuscar = GetById(IdABorrar);
            
            if (cancionABuscar == null)
            {
                return (true);
            } else
            {
                return false;
            }
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
                unaCancion.Autor = (string)miTabla.Rows[0]["Autor"];
                unaCancion.Genero = (string)miTabla.Rows[0]["Genero"];
                unaCancion.Lanzamiento = (int)miTabla.Rows[0]["Lanzamiento"];
            } else
            {
                unaCancion = null;
            }

            return unaCancion;
        }

        public static Cancion GetById (int IdABuscar)
        {
            MySqlCommand miComando = new MySqlCommand();
            miComando.Connection = Util.getConnection();
            miComando.CommandType = CommandType.Text;
            miComando.CommandText = "Select * from canciones where Id=@Id";
            miComando.Parameters.AddWithValue("@Id", IdABuscar);

            DataTable miTabla = new DataTable();
            MySqlDataAdapter miAdaptador = new MySqlDataAdapter();
            miAdaptador.SelectCommand = miComando;
            miAdaptador.Fill(miTabla);

            Cancion unaCancion = new Cancion();

            if(miTabla.Rows.Count > 0)
            {
                unaCancion.Id = IdABuscar;
                unaCancion.Titulo = (string)miTabla.Rows[0]["Titulo"];
                unaCancion.Genero = (string)miTabla.Rows[0]["Genero"];
                unaCancion.Autor = (string)miTabla.Rows[0]["Autor"];
                unaCancion.Lanzamiento = (int)miTabla.Rows[0]["Lanzamiento"];
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

            foreach (DataRow index in miTabla.Rows)
            {
                Cancion unaCancion = new Cancion();
                unaCancion.Id = (int)index["Id"];
                unaCancion.Titulo = (string)index["Titulo"];
                unaCancion.Genero = (string)index["Genero"];
                unaCancion.Lanzamiento = (int)index["Lanzamiento"];
                unaCancion.Autor = (string)index["Autor"];
                ListaADevolver.Add(unaCancion);
            }

            return ListaADevolver;
        }

        public class Cancion
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public string  Genero { get; set; }
            public int Lanzamiento { get; set; }
            public string UrlLetra { get; set; }
        }
    }
}
