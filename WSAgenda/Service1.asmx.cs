using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace WSAgenda
{
    /// <summary>
    /// Descripción breve de Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string HolaNombre(String nombre)
        {
            return "Hola " + nombre;
        }

        [WebMethod]
        public String consultaPais()
        {
            String registro = "";
            SqlConnection conn = new SqlConnection();
            //conn.ConnectionString = "Server=SYC-LP\\SQLEXPRESS;" +
              //      "Database=BDAgenda;User Id=sa; Password=12345;";
            conn.ConnectionString = "Server=WORKSTATION-PC\\SQLEXPRESS;" +
                    "Database=WSAgenda;User Id=SA; Password=123;";

            try
            {
                conn.Open();
                SqlDataAdapter cmd = new
                    SqlDataAdapter("select * from Pais;", conn);
                DataSet data = new DataSet();
                cmd.Fill(data, "datos");
                DataTable tabla = data.Tables[0];
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    Array a = tabla.Rows[i].ItemArray;
                    registro += a.GetValue(0).ToString() + ",";
                    registro += a.GetValue(1).ToString() + "*";
                }
            }
            catch (Exception ex)
            {
                registro = "Conexion No Exitosa";
            }
            finally
            {
                conn.Close();
            }
            return registro;
        }

        [WebMethod]
        public String InsertarContacto(String id, String nombre, String telefono,
                                     String correo, String pais)
        {
            SqlConnection conn = new SqlConnection();
           // conn.ConnectionString = "Server=SYC-LP\\SQLEXPRESS;" +
             //       "Database=WSAgenda;User Id=SA; Password=123;";

            conn.ConnectionString = "Server=WORKSTATION-PC\\SQLEXPRESS;" +
                    "Database=WSAgenda;User Id=SA; Password=123;";

            //WORKSTATION-PC\SQLEXPRESS;Initial Catalog=WSAgenda;User ID=SA;Password=***********
            try
            {
                conn.Open();
                String instruccion =
                    "Insert into Contacto (ContactoId,ContactoNombre," +
                              "ContactoTelefono,ContactoCorreo,IdPais) " +
                    "Values (" +
                    "'" + id + "'," +
                    "'" + nombre + "'," +
                    "'" + telefono + "'," +
                    "'" + correo + "'," +
                    "'" + pais + "'" +
                    ")";
                SqlCommand com = new SqlCommand(instruccion, conn);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            return "ok";
        }
    }
}