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
    public class GoleadoresController : ApiController
    {
        // GET: api/Goleadores
        public IEnumerable<Goleadores> Get()
        {
            string SERVIDORNEW = (string)new System.Configuration.AppSettingsReader().GetValue("Servidor", typeof(string));
            string UsuarioBase = (string)new System.Configuration.AppSettingsReader().GetValue("Usuario", typeof(string));
            string ClaveBase = (string)new System.Configuration.AppSettingsReader().GetValue("Clave", typeof(string));
            string Base = (string)new System.Configuration.AppSettingsReader().GetValue("Base", typeof(string));

            string campeonato = Campeonato();

            string strCadena = "";
            strCadena = strCadena + "workstation id=SOPORTE;";
            strCadena = strCadena + "packet size=4096;";
            strCadena = strCadena + "user id=" + UsuarioBase + ";";
            strCadena = strCadena + "data source=" + SERVIDORNEW + ";";
            strCadena = strCadena + "persist security info=True;";
            strCadena = strCadena + "initial catalog=" + Base + ";";
            strCadena = strCadena + "password=" + ClaveBase;

            List<Goleadores> listTabla = new List<Goleadores>();


            SqlConnection con = new SqlConnection(strCadena);
            SqlConnection.ClearAllPools();
            //string sql = "select TOP 10 IDENTIFICACION,";
            ////sql1 += " (SELECT foto FROM Jugadores WHERE Jugadores.identificacion =Jugados.identificacion) AS foto,";
            //sql += " (SELECT NOMBRES FROM Jugadores WHERE Jugadores.identificacion =Jugados.identificacion) AS JUGADOR,";
            //sql += " SUM(GOLES) AS GOLES";
            //sql += " ,(SELECT NOMBRECORTO FROM Equipos WHERE Equipos.Id=Jugados.id_equipo) AS EQUIPO";
            //sql += " ,(SELECT NUMERO FROM JugadoresCampeonato WHERE JugadoresCampeonato.Identificacion =Jugados.identificacion AND JugadoresCampeonato.Campeonato =" + campeonato + " AND TIPO='J') AS NUMERO";
            //sql += " FROM JUGADOS";
            //sql += " WHERE CAMPEONATO=" + campeonato;
            ////sql1 += " WHERE CAMPEONATO=2014";
            //sql += " AND CATEGORIA='MAXIMA'";
            //sql += " GROUP BY identificacion, id_equipo";
            //sql += " ORDER BY GOLES DESC";

            string sql = "select TOP 5 IDENTIFICACION,";            
            sql += " (SELECT NOMBRES FROM Jugadores (nolock) WHERE Jugadores.identificacion =Jugados.identificacion) AS JUGADOR,";
            sql += " SUM(GOLES) AS GOLES";
            sql += " ,(SELECT NOMBRECORTO FROM Equipos (nolock) WHERE Equipos.Id=Jugados.id_equipo) AS EQUIPO";
            sql += " ,(SELECT NUMERO FROM JugadoresCampeonato (nolock) WHERE JugadoresCampeonato.Identificacion =Jugados.identificacion AND JugadoresCampeonato.Campeonato =" + campeonato + " AND TIPO='J' and Equipo=Jugados.id_equipo) AS NUMERO";
            sql += " FROM JUGADOS (nolock)";
            sql += " WHERE CAMPEONATO=" + campeonato;            
            sql += " AND CATEGORIA='MAXIMA'";
            sql += " GROUP BY identificacion, id_equipo";
            sql += " ORDER BY GOLES DESC";

            SqlCommand obj = new SqlCommand(sql, con);
            obj.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = obj.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Goleadores datos = new Goleadores();
                    datos.Identificacion = dr[0].ToString().Trim();
                    datos.Nombre = Nombres(dr[1].ToString().Trim());
                    datos.Goles = dr[2].ToString().Trim();
                    datos.Equipo = dr[3].ToString().Trim();
                    datos.Numero = dr[4].ToString().Trim();                    
                    datos.Foto = "http://181.39.104.93:5015/ImageHandler.ashx?ImID=" + dr[0].ToString().Trim();
                    datos.Categoria = "MAXIMA";
                    listTabla.Add(datos);
                }
            }

            dr.Close();
            con.Close();
            con.Dispose();
            dr.Dispose();


            SqlConnection con1 = new SqlConnection(strCadena);
            SqlConnection.ClearAllPools();
            string sql1 = "select TOP 5 IDENTIFICACION,";
            //sql1 += " (SELECT foto FROM Jugadores WHERE Jugadores.identificacion =Jugados.identificacion) AS foto,";
            sql1 += " (SELECT NOMBRES FROM Jugadores (nolock) WHERE Jugadores.identificacion =Jugados.identificacion) AS JUGADOR,";
            sql1 += " SUM(GOLES) AS GOLES";
            sql1 += " ,(SELECT NOMBRECORTO FROM Equipos (nolock) WHERE Equipos.Id=Jugados.id_equipo) AS EQUIPO";
            sql1 += " ,(SELECT NUMERO FROM JugadoresCampeonato (nolock) WHERE JugadoresCampeonato.Identificacion =Jugados.identificacion AND JugadoresCampeonato.Campeonato =" + campeonato + " AND TIPO='J' and Equipo=Jugados.id_equipo) AS NUMERO";
            sql1 += " FROM JUGADOS (nolock)";
            sql1 += " WHERE CAMPEONATO=" + campeonato;
            //sql1 += " WHERE CAMPEONATO=2014";
            sql1 += " AND CATEGORIA='PRIMERA'";
            sql1 += " GROUP BY identificacion, id_equipo";
            sql1 += " ORDER BY GOLES DESC";


            SqlCommand obj1 = new SqlCommand(sql1, con1);
            obj1.CommandType = CommandType.Text;
            con1.Open();
            SqlDataReader dr1 = obj1.ExecuteReader();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    Goleadores datos = new Goleadores();
                    datos.Identificacion = dr1[0].ToString().Trim();
                    datos.Nombre = Nombres(dr1[1].ToString().Trim());
                    datos.Goles = dr1[2].ToString().Trim();
                    datos.Equipo = dr1[3].ToString().Trim();
                    datos.Numero = dr1[4].ToString().Trim();
                    datos.Foto = "http://181.39.104.93:5015/ImageHandler.ashx?ImID=" + dr1[0].ToString().Trim();
                    datos.Categoria = "PRIMERA";
                    listTabla.Add(datos);
                }
            }

            dr1.Close();
            con1.Close();
            con1.Dispose();
            dr1.Dispose();

            //sub40
            SqlConnection con2 = new SqlConnection(strCadena);
            SqlConnection.ClearAllPools();
            string sql2 = "select TOP 5 IDENTIFICACION,";
            //sql1 += " (SELECT foto FROM Jugadores WHERE Jugadores.identificacion =Jugados.identificacion) AS foto,";
            sql2 += " (SELECT NOMBRES FROM Jugadores (nolock) WHERE Jugadores.identificacion =Jugados.identificacion) AS JUGADOR,";
            sql2 += " SUM(GOLES) AS GOLES";
            sql2 += " ,(SELECT NOMBRECORTO FROM Equipos (nolock) WHERE Equipos.Id=Jugados.id_equipo) AS EQUIPO";
            sql2 += " ,(SELECT NUMERO FROM JugadoresCampeonato (nolock) WHERE JugadoresCampeonato.Identificacion =Jugados.identificacion AND JugadoresCampeonato.Campeonato =" + campeonato + " AND TIPO='J' and Equipo=Jugados.id_equipo) AS NUMERO";
            sql2 += " FROM JUGADOS (nolock)";
            sql2 += " WHERE CAMPEONATO=" + campeonato;
            //sql1 += " WHERE CAMPEONATO=2014";
            sql2 += " AND CATEGORIA='SUB 40'";
            sql2 += " GROUP BY identificacion, id_equipo";
            sql2 += " ORDER BY GOLES DESC";


            SqlCommand obj2 = new SqlCommand(sql2, con2);
            obj2.CommandType = CommandType.Text;
            con2.Open();
            SqlDataReader dr2 = obj2.ExecuteReader();
            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    Goleadores datos = new Goleadores();
                    datos.Identificacion = dr2[0].ToString().Trim();
                    datos.Nombre = Nombres(dr2[1].ToString().Trim());
                    datos.Goles = dr2[2].ToString().Trim();
                    datos.Equipo = dr2[3].ToString().Trim();
                    datos.Numero = dr2[4].ToString().Trim();
                    datos.Foto = "http://181.39.104.93:5015/ImageHandler.ashx?ImID=" + dr2[0].ToString().Trim();
                    datos.Categoria = "SUB 40";
                    listTabla.Add(datos);
                }
            }

            dr2.Close();
            con2.Close();
            con2.Dispose();
            dr2.Dispose();

            //sub50
            SqlConnection con3 = new SqlConnection(strCadena);
            SqlConnection.ClearAllPools();
            string sql3 = "select TOP 5 IDENTIFICACION,";
            //sql1 += " (SELECT foto FROM Jugadores WHERE Jugadores.identificacion =Jugados.identificacion) AS foto,";
            sql3 += " (SELECT NOMBRES FROM Jugadores (nolock) WHERE Jugadores.identificacion =Jugados.identificacion) AS JUGADOR,";
            sql3 += " SUM(GOLES) AS GOLES";
            sql3 += " ,(SELECT NOMBRECORTO FROM Equipos (nolock) WHERE Equipos.Id=Jugados.id_equipo) AS EQUIPO";
            sql3 += " ,(SELECT NUMERO FROM JugadoresCampeonato (nolock) WHERE JugadoresCampeonato.Identificacion =Jugados.identificacion AND JugadoresCampeonato.Campeonato =" + campeonato + " AND TIPO='J' and Equipo=Jugados.id_equipo) AS NUMERO";
            sql3 += " FROM JUGADOS (nolock)";
            sql3 += " WHERE CAMPEONATO=" + campeonato;
            //sql1 += " WHERE CAMPEONATO=2014";
            sql3 += " AND CATEGORIA='SUB 50'";
            sql3 += " GROUP BY identificacion, id_equipo";
            sql3 += " ORDER BY GOLES DESC";


            SqlCommand obj3 = new SqlCommand(sql3, con3);
            obj3.CommandType = CommandType.Text;
            con3.Open();
            SqlDataReader dr3 = obj3.ExecuteReader();
            if (dr3.HasRows)
            {
                while (dr3.Read())
                {
                    Goleadores datos = new Goleadores();
                    datos.Identificacion = dr3[0].ToString().Trim();
                    datos.Nombre = Nombres(dr3[1].ToString().Trim());
                    datos.Goles = dr3[2].ToString().Trim();
                    datos.Equipo = dr3[3].ToString().Trim();
                    datos.Numero = dr3[4].ToString().Trim();
                    datos.Foto = "http://181.39.104.93:5015/ImageHandler.ashx?ImID=" + dr3[0].ToString().Trim();
                    datos.Categoria = "SUB 50";
                    listTabla.Add(datos);
                }
            }

            dr3.Close();
            con3.Close();
            con3.Dispose();
            dr3.Dispose();

            return listTabla;
        }

        // GET: api/Goleadores/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Goleadores
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Goleadores/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Goleadores/5
        public void Delete(int id)
        {
        }

        private string Campeonato()
        {
            string campeonato = DateTime.Now.Year.ToString("0000");

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
            string sql = "SELECT TOP 1 CAMPEONATO FROM CONFIGURACIONES WHERE ESTADO=1 ORDER BY CAMPEONATO DESC";

            SqlCommand obj = new SqlCommand(sql, con);
            obj.CommandType = CommandType.Text;
            con.Open();

            SqlDataReader dr = obj.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    campeonato = dr[0].ToString().Trim();

                }

            }
            else
            {

            }
            dr.Close();
            con.Close();
            con.Dispose();
            dr.Dispose();

            return campeonato;
        }
        private string Nombres(string jugador)
        {
            string[] nombres = jugador.Trim().Split(' ');
            var primernombre = "";
            var segundonombre = "";
            var primerapellido = "";
            var segundoapellido = "";

            if (nombres.Length >= 4)
            {
                primerapellido = nombres[0];
                segundoapellido = nombres[1];
                primernombre = nombres[2];
                segundonombre = nombres[3];
            }
            if (nombres.Length == 3)
            {
                primerapellido = nombres[0];
                segundoapellido = nombres[1];
                primernombre = nombres[2];
                segundonombre = "";
            }
            if (nombres.Length == 2)
            {
                primerapellido = nombres[0];
                segundoapellido = "";
                primernombre = nombres[1];
                segundonombre = "";
            }
            if (nombres.Length == 1)
            {
                primerapellido = nombres[0];
                segundoapellido = "";
                primernombre = "";
                segundonombre = "";
            }
            return primerapellido + " " + primernombre;
        }
    }
}
