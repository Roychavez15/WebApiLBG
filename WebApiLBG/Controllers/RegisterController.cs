using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiLBG.Models;

namespace WebApiLBG.Controllers
{
    public class RegisterController : ApiController
    {
        // GET: api/Register
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Register/5
        public string Get(int id)
        {
            return "ok";
        }

        // POST: api/Register
        public ClsRegister Post([FromBody]RegisterPost value)
        {
            string SERVIDORNEW = (string)new System.Configuration.AppSettingsReader().GetValue("Servidor", typeof(string));
            string UsuarioBase = (string)new System.Configuration.AppSettingsReader().GetValue("Usuario", typeof(string));
            string ClaveBase = (string)new System.Configuration.AppSettingsReader().GetValue("Clave", typeof(string));
            string Base = (string)new System.Configuration.AppSettingsReader().GetValue("Base", typeof(string));

            string strCadena = "";
            strCadena = strCadena + "workstation id=SOPORTE;";
            strCadena = strCadena + "packet size=4096;";
            strCadena = strCadena + "user id=" + UsuarioBase + ";";
            strCadena = strCadena + "data source=" + SERVIDORNEW + ";";
            strCadena = strCadena + "persist security info=True;";
            strCadena = strCadena + "initial catalog=" + Base + ";";
            strCadena = strCadena + "password=" + ClaveBase;


            List<ClsRegister> listTabla = new List<ClsRegister>();
            ClsRegister datos = new ClsRegister();

            if (!ConsultaCorreo(value.correo))
            {
                try
                {
                    SqlConnection con1 = new SqlConnection(strCadena);
                    SqlConnection.ClearAllPools();

                    string sql1 = "INSERT INTO usuariosApp ";
                    sql1 += "(Email, Usuario, Clave, Tipo, Fecha) values ('";
                    sql1 += value.correo + "','" + value.nombre+"','"+ RandomPassword.Generate(6, 6) + "','Usuario',cast('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' as datetime))";
                    //Label22.Text = sql1;
                    SqlCommand obj1 = new SqlCommand(sql1, con1);
                    obj1.CommandType = CommandType.Text;
                    con1.Open();
                    obj1.ExecuteNonQuery();
                    con1.Close();

                    datos.Respuesta = "Ok";
                    datos.Mensaje = "Usuario Registrado con exito, su clave provisional llegará a su correo en unos minutos";
                }
                catch (Exception ex)
                {
                    datos.Respuesta = "Error";
                    datos.Mensaje = ex.Message;
                }
            }
            else
            {
                datos.Respuesta = "Error";
                datos.Mensaje = "Correo electrónico ya se encuentra registrado";
            }
            return datos;

        }

        // PUT: api/Register/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Register/5
        public void Delete(int id)
        {
        }

        private bool ConsultaCorreo(string correo)
        {
            string SERVIDORNEW = (string)new System.Configuration.AppSettingsReader().GetValue("Servidor", typeof(string));
            string UsuarioBase = (string)new System.Configuration.AppSettingsReader().GetValue("Usuario", typeof(string));
            string ClaveBase = (string)new System.Configuration.AppSettingsReader().GetValue("Clave", typeof(string));
            string Base = (string)new System.Configuration.AppSettingsReader().GetValue("Base", typeof(string));

            string strCadena = "";
            strCadena = strCadena + "workstation id=SOPORTE;";
            strCadena = strCadena + "packet size=4096;";
            strCadena = strCadena + "user id=" + UsuarioBase + ";";
            strCadena = strCadena + "data source=" + SERVIDORNEW + ";";
            strCadena = strCadena + "persist security info=True;";
            strCadena = strCadena + "initial catalog=" + Base + ";";
            strCadena = strCadena + "password=" + ClaveBase;

            SqlConnection con = new SqlConnection(strCadena);
            SqlConnection.ClearAllPools();
            string sql = "select * from UsuariosApp where Email='" + correo + "'";
            SqlCommand obj = new SqlCommand(sql, con);
            obj.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = obj.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                dr.Close();
                con.Close();
                con.Dispose();
                dr.Dispose();

                return true;
            }
            else
            {
                dr.Close();
                con.Close();
                con.Dispose();
                dr.Dispose();
                //mal clave o no existe usuario
                return false;
            }
        }
    }
}
