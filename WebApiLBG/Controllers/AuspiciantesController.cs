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
    public class AuspiciantesController : ApiController
    {
        // GET: api/Auspiciantes
        public IList<Auspicios> Get()
        {
            //return new string[] { "value1", "value2" };

            List<Auspicios> listTabla = new List<Auspicios>();

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
            string sql = "SELECT RUTA FROM AUSPICIOS";

            SqlCommand obj = new SqlCommand(sql, con);
            obj.CommandType = CommandType.Text;
            con.Open();

            SqlDataReader dr = obj.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Auspicios posTabla = new Auspicios();
                    posTabla.archivo = dr[0].ToString().Trim();
                    listTabla.Add(posTabla);
                }
            }
            else
            {

            }
            dr.Close();
            con.Close();
            con.Dispose();
            dr.Dispose();
            return listTabla;
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //Context.Response.Write(js.Serialize(listTabla));
        }

        // GET: api/Auspiciantes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Auspiciantes
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/Auspiciantes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Auspiciantes/5
        public void Delete(int id)
        {
        }
    }
}
