using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conectar_a_sql
{
    class acceso
    {
        //private string cadena = "Source = (local),catalog = Datos, Integrated security = true";
        private SqlConnection coneccion;

        private SqlCommand comando;
        //metodo de conexion.

        private void conectar()
        {
            coneccion = new SqlConnection();
            coneccion.ConnectionString = "Data Source = ACER-130-7\\MSSQLSERVE; Initial Catalog = Datos; Integrated Security = True";
        }
        //constructor
        public acceso()
        {
            conectar();
        }
        //metodo de insertar
        public bool insertar (string tabla, int carnet, string n, string a, string e, DateTime f)
        {
            coneccion.Open();
            comando = new SqlCommand();

            comando.CommandText = "insert into " + tabla + " (carnet,nombres,apellidos,email,fecha_nac)" +
                "values ('" + carnet + "','" + n + "','" + a + "','" + e + "','" + f + "')";

            comando.Connection = coneccion;
            int i = comando.ExecuteNonQuery();
            coneccion.Close();

            if(i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool eliminar(string tabla, string condicion)
        {
            coneccion.Open();
            comando = new SqlCommand();
            comando.CommandText = "delete from " + tabla + " where " + condicion;
            comando.Connection = coneccion;
            int i = comando.ExecuteNonQuery();
            coneccion.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool actualizar(string tabla,string n, string a, string e, DateTime f,string condicion)
        {
            coneccion.Open();
            comando = new SqlCommand();

            comando.CommandText = "update " + tabla + " set nombres = '"+ n +"', apellidos = '" + a +
                "', email = '" + e + "', fecha_nac = '" + f +"' where "+ condicion;

            comando.Connection = coneccion;
            int i = comando.ExecuteNonQuery();
            coneccion.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable consultar (string tabla, string condicion)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select * from " + tabla + " where " + condicion;
            SqlDataAdapter da = new SqlDataAdapter(sql, coneccion);
            da.Fill(ds, tabla);
            dt = ds.Tables[tabla];
            return dt;
        }
        public DataTable consultar(string tabla)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select * from " + tabla;
            SqlDataAdapter da = new SqlDataAdapter(sql, coneccion);
            da.Fill(ds, tabla);
            dt = ds.Tables[tabla];
            return dt;
        }
    }
}
