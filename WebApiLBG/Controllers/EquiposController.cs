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
    public class EquiposController : ApiController
    {
        // GET: api/Equipos
        public IEnumerable<Equipos> Get()
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

            List<Equipos> listTabla = new List<Equipos>();


            SqlConnection con = new SqlConnection(strCadena);
            SqlConnection.ClearAllPools();
            string sql = "SELECT [NOMBRE],[NOMBRECORTO],[FECHAFUNDACION],[DIRECCION],[TELEFONOS],[EMAIL]";
            sql += " FROM EQUIPOS ";
            sql += " ORDER BY NOMBRECORTO ASC";

            SqlCommand obj = new SqlCommand(sql, con);
            obj.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = obj.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Equipos datos = new Equipos();
                    datos.Nombre = dr[0].ToString().Trim();
                    datos.NombreCorto = dr[1].ToString().Trim();
                    datos.Fundacion = dr[2].ToString().Trim();
                    datos.Direccion = dr[3].ToString().Trim();
                    datos.Telefonos = dr[4].ToString().Trim();
                    datos.Email = dr[5].ToString().Trim();
                    datos.Logo = "http://181.211.113.66/lbg1/logo.aspx?id=" + dr[1].ToString().Trim();
                    listTabla.Add(datos);
                }
            }

            dr.Close();
            con.Close();
            con.Dispose();
            dr.Dispose();

            return listTabla;
        }

        // GET: api/Equipos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Equipos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Equipos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Equipos/5
        public void Delete(int id)
        {
        }
    }
}
