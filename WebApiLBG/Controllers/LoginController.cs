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
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public ClsLogin Post([FromBody]GetLogin value)
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

            ClsLogin datos1 = new ClsLogin();

            try
            {
                SqlConnection con = new SqlConnection(strCadena);
                SqlConnection.ClearAllPools();
                string sql = "select id, usuario from UsuariosApp where Email='" + value.email + "' AND CLAVE='" + value.clave + "'";
                SqlCommand obj = new SqlCommand(sql, con);
                obj.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = obj.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        datos1.respuesta = "OK";
                        datos1.id = dr[0].ToString();
                        datos1.nombre = dr[1].ToString();
                    }
                    else
                    {
                        datos1.respuesta = "ERROR";
                        datos1.id = "0";
                        datos1.nombre = "Correo o Clave Incorrecta";
                    }
                }
                else
                {
                    datos1.respuesta = "ERROR";
                    datos1.id = "0";
                    datos1.nombre = "Correo o Clave Incorrecta";

                    //mal clave o no existe usuario

                }
                dr.Close();
                dr.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                datos1.respuesta = "ERROR";
                datos1.id = "0";
                datos1.nombre = ex.Message;
            }
            return datos1;
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
