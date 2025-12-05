using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiLBG.Models;

namespace WebApiLBG.Controllers
{
    public class TablaPosController : ApiController
    {
        // GET: api/Tabla
        //public IEnumerable<Tabla> Get()
        //{
        //    string SERVIDORNEW = (string)new System.Configuration.AppSettingsReader().GetValue("Servidor", typeof(string));
        //    string UsuarioBase = (string)new System.Configuration.AppSettingsReader().GetValue("Usuario", typeof(string));
        //    string ClaveBase = (string)new System.Configuration.AppSettingsReader().GetValue("Clave", typeof(string));
        //    string Base = (string)new System.Configuration.AppSettingsReader().GetValue("Base", typeof(string));

        //    string campeonato = Campeonato();
        //    string etapa = Etapa(campeonato, "MAXIMA");

        //    string strCadena = "";
        //    strCadena = strCadena + "workstation id=SOPORTE;";
        //    strCadena = strCadena + "packet size=4096;";
        //    strCadena = strCadena + "user id=" + UsuarioBase + ";";
        //    strCadena = strCadena + "data source=" + SERVIDORNEW + ";";
        //    strCadena = strCadena + "persist security info=True;";
        //    strCadena = strCadena + "initial catalog=" + Base + ";";
        //    strCadena = strCadena + "password=" + ClaveBase;

        //    List<Tabla> listTabla = new List<Tabla>();


        //    SqlConnection con = new SqlConnection(strCadena);
        //    SqlConnection.ClearAllPools();
        //    string sql = "";
        //    sql = "SELECT ";
        //    sql += " ROW_NUMBER() OVER(PARTITION BY X.GRUPO ORDER BY SUM(X.PUNTOS + X.BONIFICACION) DESC ,SUM(X.GL-X.GR) DESC";
        //    sql += " ,SUM(X.GL) DESC";
        //    sql += " ,SUM(X.GR) ASC) 'POS'";
        //    sql += " ,X.EQUIPO 'EQUIPO'";
        //    sql += " ,SUM(X.PJ) 'PJ'";
        //    sql += " ,SUM(X.PG) 'PG'";
        //    sql += " ,SUM(X.PP) 'PP'";
        //    sql += " ,SUM(X.PE) 'PE'";
        //    sql += " ,SUM(X.GL) 'GF'";
        //    sql += " ,SUM(X.GR) 'GC'";
        //    sql += " ,SUM(X.PUNTOS) 'PUNTOS'";
        //    sql += " ,SUM(X.GL - X.GR) 'GD'";
        //    sql += " ,X.GRUPO 'GRUPO'";
        //    sql += " ,SUM(BONIFICACION) 'BONIFICACION'";
        //    sql += " ,SUM(X.PUNTOS + X.BONIFICACION) 'TOTAL'";
        //    sql += " FROM";
        //    sql += " (";
        //    sql += " select codloc";
        //    sql += " , (SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = PARTIDOS.CODLOC) 'EQUIPO'";
        //    sql += " ,COUNT(*) 'PJ'";
        //    sql += " ,SUM(CASE WHEN golesl > golesv THEN 1 ELSE 0 END) 'PG'";
        //    sql += " ,SUM(CASE WHEN golesl < golesv THEN 1 ELSE 0 END) 'PP'";
        //    sql += " ,SUM(CASE WHEN golesl = golesv THEN 1 ELSE 0 END) 'PE'";
        //    sql += " ,SUM(pl) 'puntos'";
        //    sql += " ,sum(golesl) 'GL'";
        //    sql += " ,sum(golesv) 'GR'";
        //    sql += " ,(SELECT TOP 1 GRUPO FROM EquiposCampeonato WHERE EquiposCampeonato.ID = partidos.codLOC";
        //    sql += " AND Campeonato = " + campeonato + " and categoria = 'maxima' AND Etapa = " + etapa + ") 'GRUPO'";
        //    sql += " ,0 'BONIFICACION'";
        //    sql += " FROM PARTIDOS";
        //    sql += " where Campeonato = " + campeonato;
        //    sql += " and categoria = 'maxima'";
        //    sql += " and etapa = " + etapa;
        //    sql += " AND jugado = 1";
        //    sql += " GROUP BY CODLOC";
        //    sql += " UNION all";
        //    sql += " select codVIS";
        //    sql += " ,(SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = PARTIDOS.codVis) 'EQUIPO'";
        //    sql += " ,COUNT(*) 'PJ'";
        //    sql += " ,SUM(CASE WHEN golesl < golesv THEN 1 ELSE 0 END) 'PG'";
        //    sql += " ,SUM(CASE WHEN golesl > golesv THEN 1 ELSE 0 END) 'PP'";
        //    sql += " ,SUM(CASE WHEN golesl = golesv THEN 1 ELSE 0 END) 'PE'";
        //    sql += " ,SUM(pV) 'puntos'";
        //    sql += " ,sum(golesV) 'GL'";
        //    sql += " ,sum(golesL) 'GR'";
        //    sql += " ,(SELECT TOP 1 GRUPO FROM EquiposCampeonato WHERE EquiposCampeonato.ID = partidos.codVIS";
        //    sql += " AND Campeonato = " + campeonato + " and categoria = 'maxima' AND Etapa = " + etapa + ") 'GRUPO'";
        //    sql += " ,0 'BONIFICACION'";
        //    sql += " FROM PARTIDOS";
        //    sql += " where Campeonato = " + campeonato;
        //    sql += " and categoria = 'maxima'";
        //    sql += " and etapa = " + etapa;
        //    sql += " AND jugado = 1";
        //    sql += " GROUP BY CODvis";
        //    sql += " UNION ALL";
        //    sql += " select Id_Equipo";
        //    sql += " ,(SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = SancionesE.Id_Equipo) 'EQUIPO'";
        //    sql += " ,0 'PJ'";
        //    sql += " ,0 'PG'";
        //    sql += " ,0 'PP'";
        //    sql += " ,0 'PE'";
        //    sql += " ,SUM(PUNTOS) 'puntos'";
        //    sql += " ,sum(goles) 'GL'";
        //    sql += " ,0";
        //    sql += " ,(SELECT TOP 1 GRUPO FROM EquiposCampeonato WHERE EquiposCampeonato.ID = SancionesE.Id_Equipo";
        //    sql += " AND Campeonato = " + campeonato + " and categoria = 'maxima' AND Etapa = " + etapa + ") 'GRUPO'";
        //    sql += " ,0 'BONIFICACION'";
        //    sql += " FROM SancionesE";
        //    sql += " where Campeonato = " + campeonato;
        //    sql += " and categoria = 'maxima'";
        //    sql += " and etapa = " + etapa;
        //    sql += " GROUP BY Id_Equipo";
        //    sql += " UNION ALL";
        //    sql += " SELECT ID_EQUIPO";
        //    sql += " ,(SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = Bonificaciones.Id_Equipo) 'EQUIPO'";
        //    sql += " ,0 'PJ'";
        //    sql += " ,0 'PG'";
        //    sql += " ,0 'PP'";
        //    sql += " ,0 'PE'";
        //    sql += " ,0 'puntos'";
        //    sql += " ,0 'GL'";
        //    sql += " ,0";
        //    sql += " ,(SELECT TOP 1 GRUPO FROM EquiposCampeonato WHERE EquiposCampeonato.ID = Bonificaciones.Id_Equipo";
        //    sql += " AND Campeonato = " + campeonato + " and categoria = 'maxima' AND Etapa = " + etapa + ") 'GRUPO'";
        //    sql += " ,SUM(PUNTOS) 'BONIFICACION'";
        //    sql += " FROM Bonificaciones";
        //    sql += " where Campeonato = " + campeonato;
        //    sql += " and categoria = 'maxima'";
        //    sql += " and etapa = " + etapa;
        //    sql += " GROUP BY Id_Equipo";
        //    sql += " ) X";
        //    sql += " GROUP BY X.EQUIPO, X.GRUPO";
        //    sql += " ORDER BY";
        //    sql += " X.GRUPO ASC";
        //    sql += " , SUM(X.PUNTOS + X.BONIFICACION) DESC";
        //    sql += " ,SUM(X.GL - X.GR) DESC";
        //    sql += " ,SUM(X.GL) DESC";
        //    sql += " ,SUM(X.GR) ASC";

        //    SqlCommand obj = new SqlCommand(sql, con);
        //    obj.CommandType = CommandType.Text;
        //    con.Open();
        //    SqlDataReader dr = obj.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            Tabla datos = new Tabla();
        //            datos.Posicion = Convert.ToInt32(dr[0].ToString().Trim());
        //            datos.Equipo = dr[1].ToString().Trim();
        //            datos.Pj = Convert.ToInt32(dr[2].ToString().Trim());
        //            datos.Pg= Convert.ToInt32(dr[3].ToString().Trim());
        //            datos.Pp = Convert.ToInt32(dr[4].ToString().Trim());
        //            datos.Pe = Convert.ToInt32(dr[5].ToString().Trim());
        //            datos.Gf = Convert.ToInt32(dr[6].ToString().Trim());
        //            datos.Gc = Convert.ToInt32(dr[7].ToString().Trim());
        //            datos.Puntos= Convert.ToInt32(dr[8].ToString().Trim());
        //            datos.Gd = Convert.ToInt32(dr[9].ToString().Trim());
        //            datos.Grupo = dr[10].ToString().Trim();
        //            datos.Bonificacion = Convert.ToDecimal(dr[11].ToString().Trim());
        //            datos.Total = Convert.ToDecimal(dr[12].ToString().Trim());
        //            datos.Categoria = "MAXIMA";
        //            datos.Logo = "http://181.39.104.93:5015/logo.aspx?id=" + dr[1].ToString().Trim();
        //            listTabla.Add(datos);
        //        }
        //    }

        //    dr.Close();
        //    con.Close();
        //    con.Dispose();
        //    dr.Dispose();

        //    ////////PRIMERA
        //    etapa = Etapa(campeonato, "PRIMERA");
        //    SqlConnection con1 = new SqlConnection(strCadena);
        //    SqlConnection.ClearAllPools();
        //    string sql1 = "";
        //    sql1 = "SELECT ";
        //    sql1 += " ROW_NUMBER() OVER(PARTITION BY X.GRUPO ORDER BY SUM(X.PUNTOS + X.BONIFICACION) DESC ,SUM(X.GL-X.GR) DESC";
        //    sql1 += " ,SUM(X.GL) DESC";
        //    sql1 += " ,SUM(X.GR) ASC) 'POS'";
        //    sql1 += " ,X.EQUIPO 'EQUIPO'";
        //    sql1 += " ,SUM(X.PJ) 'PJ'";
        //    sql1 += " ,SUM(X.PG) 'PG'";
        //    sql1 += " ,SUM(X.PP) 'PP'";
        //    sql1 += " ,SUM(X.PE) 'PE'";
        //    sql1 += " ,SUM(X.GL) 'GF'";
        //    sql1 += " ,SUM(X.GR) 'GC'";
        //    sql1 += " ,SUM(X.PUNTOS) 'PUNTOS'";
        //    sql1 += " ,SUM(X.GL - X.GR) 'GD'";
        //    sql1 += " ,X.GRUPO 'GRUPO'";
        //    sql1 += " ,SUM(BONIFICACION) 'BONIFICACION'";
        //    sql1 += " ,SUM(X.PUNTOS + X.BONIFICACION) 'TOTAL'";
        //    sql1 += " FROM";
        //    sql1 += " (";
        //    sql1 += " select codloc";
        //    sql1 += " , (SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = PARTIDOS.CODLOC) 'EQUIPO'";
        //    sql1 += " ,COUNT(*) 'PJ'";
        //    sql1 += " ,SUM(CASE WHEN golesl > golesv THEN 1 ELSE 0 END) 'PG'";
        //    sql1 += " ,SUM(CASE WHEN golesl < golesv THEN 1 ELSE 0 END) 'PP'";
        //    sql1 += " ,SUM(CASE WHEN golesl = golesv THEN 1 ELSE 0 END) 'PE'";
        //    sql1 += " ,SUM(pl) 'puntos'";
        //    sql1 += " ,sum(golesl) 'GL'";
        //    sql1 += " ,sum(golesv) 'GR'";
        //    sql1 += " ,(SELECT TOP 1 GRUPO FROM EquiposCampeonato WHERE EquiposCampeonato.ID = partidos.codLOC";
        //    sql1 += " AND Campeonato = " + campeonato + " and categoria = 'PRIMERA' AND Etapa = " + etapa + ") 'GRUPO'";
        //    sql1 += " ,0 'BONIFICACION'";
        //    sql1 += " FROM PARTIDOS";
        //    sql1 += " where Campeonato = " + campeonato;
        //    sql1 += " and categoria = 'PRIMERA'";
        //    sql1 += " and etapa = " + etapa;
        //    sql1 += " AND jugado = 1";
        //    sql1 += " GROUP BY CODLOC";
        //    sql1 += " UNION all";
        //    sql1 += " select codVIS";
        //    sql1 += " ,(SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = PARTIDOS.codVis) 'EQUIPO'";
        //    sql1 += " ,COUNT(*) 'PJ'";
        //    sql1 += " ,SUM(CASE WHEN golesl < golesv THEN 1 ELSE 0 END) 'PG'";
        //    sql1 += " ,SUM(CASE WHEN golesl > golesv THEN 1 ELSE 0 END) 'PP'";
        //    sql1 += " ,SUM(CASE WHEN golesl = golesv THEN 1 ELSE 0 END) 'PE'";
        //    sql1 += " ,SUM(pV) 'puntos'";
        //    sql1 += " ,sum(golesV) 'GL'";
        //    sql1 += " ,sum(golesL) 'GR'";
        //    sql1 += " ,(SELECT TOP 1 GRUPO FROM EquiposCampeonato WHERE EquiposCampeonato.ID = partidos.codVIS";
        //    sql1 += " AND Campeonato = " + campeonato + " and categoria = 'PRIMERA' AND Etapa = " + etapa + ") 'GRUPO'";
        //    sql1 += " ,0 'BONIFICACION'";
        //    sql1 += " FROM PARTIDOS";
        //    sql1 += " where Campeonato = " + campeonato;
        //    sql1 += " and categoria = 'PRIMERA'";
        //    sql1 += " and etapa = " + etapa;
        //    sql1 += " AND jugado = 1";
        //    sql1 += " GROUP BY CODvis";
        //    sql1 += " UNION ALL";
        //    sql1 += " select Id_Equipo";
        //    sql1 += " ,(SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = SancionesE.Id_Equipo) 'EQUIPO'";
        //    sql1 += " ,0 'PJ'";
        //    sql1 += " ,0 'PG'";
        //    sql1 += " ,0 'PP'";
        //    sql1 += " ,0 'PE'";
        //    sql1 += " ,SUM(PUNTOS) 'puntos'";
        //    sql1 += " ,sum(goles) 'GL'";
        //    sql1 += " ,0";
        //    sql1 += " ,(SELECT TOP 1 GRUPO FROM EquiposCampeonato WHERE EquiposCampeonato.ID = SancionesE.Id_Equipo";
        //    sql1 += " AND Campeonato = " + campeonato + " and categoria = 'PRIMERA' AND Etapa = " + etapa + ") 'GRUPO'";
        //    sql1 += " ,0 'BONIFICACION'";
        //    sql1 += " FROM SancionesE";
        //    sql1 += " where Campeonato = " + campeonato;
        //    sql1 += " and categoria = 'PRIMERA'";
        //    sql1 += " and etapa = " + etapa;
        //    sql1 += " GROUP BY Id_Equipo";
        //    sql1 += " UNION ALL";
        //    sql1 += " SELECT ID_EQUIPO";
        //    sql1 += " ,(SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = Bonificaciones.Id_Equipo) 'EQUIPO'";
        //    sql1 += " ,0 'PJ'";
        //    sql1 += " ,0 'PG'";
        //    sql1 += " ,0 'PP'";
        //    sql1 += " ,0 'PE'";
        //    sql1 += " ,0 'puntos'";
        //    sql1 += " ,0 'GL'";
        //    sql1 += " ,0";
        //    sql1 += " ,(SELECT TOP 1 GRUPO FROM EquiposCampeonato WHERE EquiposCampeonato.ID = Bonificaciones.Id_Equipo";
        //    sql1 += " AND Campeonato = " + campeonato + " and categoria = 'PRIMERA' AND Etapa = " + etapa + ") 'GRUPO'";
        //    sql1 += " ,SUM(PUNTOS) 'BONIFICACION'";
        //    sql1 += " FROM Bonificaciones";
        //    sql1 += " where Campeonato = " + campeonato;
        //    sql1 += " and categoria = 'PRIMERA'";
        //    sql1 += " and etapa = " + etapa;
        //    sql1 += " GROUP BY Id_Equipo";
        //    sql1 += " ) X";
        //    sql1 += " GROUP BY X.EQUIPO, X.GRUPO";
        //    sql1 += " ORDER BY";
        //    sql1 += " X.GRUPO ASC";
        //    sql1 += " , SUM(X.PUNTOS + X.BONIFICACION) DESC";
        //    sql1 += " ,SUM(X.GL - X.GR) DESC";
        //    sql1 += " ,SUM(X.GL) DESC";
        //    sql1 += " ,SUM(X.GR) ASC";

        //    SqlCommand obj1 = new SqlCommand(sql1, con1);
        //    obj1.CommandType = CommandType.Text;
        //    con1.Open();
        //    SqlDataReader dr1 = obj1.ExecuteReader();
        //    if (dr1.HasRows)
        //    {
        //        while (dr1.Read())
        //        {
        //            Tabla datos = new Tabla();
        //            datos.Posicion = Convert.ToInt32(dr1[0].ToString().Trim());
        //            datos.Equipo = dr1[1].ToString().Trim();
        //            datos.Pj = Convert.ToInt32(dr1[2].ToString().Trim());
        //            datos.Pg = Convert.ToInt32(dr1[3].ToString().Trim());
        //            datos.Pp = Convert.ToInt32(dr1[4].ToString().Trim());
        //            datos.Pe = Convert.ToInt32(dr1[5].ToString().Trim());
        //            datos.Gf = Convert.ToInt32(dr1[6].ToString().Trim());
        //            datos.Gc = Convert.ToInt32(dr1[7].ToString().Trim());
        //            datos.Puntos = Convert.ToInt32(dr1[8].ToString().Trim());
        //            datos.Gd = Convert.ToInt32(dr1[9].ToString().Trim());
        //            datos.Grupo = dr1[10].ToString().Trim();
        //            datos.Bonificacion = Convert.ToDecimal(dr1[11].ToString().Trim());
        //            datos.Total = Convert.ToDecimal(dr1[12].ToString().Trim());
        //            datos.Categoria = "PRIMERA";
        //            datos.Logo = "http://181.39.104.93:5015/logo.aspx?id=" + dr1[1].ToString().Trim();
        //            listTabla.Add(datos);
        //        }
        //    }

        //    dr1.Close();
        //    con1.Close();
        //    con1.Dispose();
        //    dr1.Dispose();


        //    return listTabla;
        //}

        public IEnumerable<Tabla> Get()
        {
            string SERVIDORNEW = ConfigurationManager.AppSettings["Servidor"];
            string UsuarioBase = ConfigurationManager.AppSettings["Usuario"];
            string ClaveBase = ConfigurationManager.AppSettings["Clave"];
            string Base = ConfigurationManager.AppSettings["Base"];

            string campeonato = Campeonato();

            string strCadena = $"workstation id=SOPORTE;packet size=4096;user id={UsuarioBase};" +
                               $"data source={SERVIDORNEW};persist security info=True;" +
                               $"initial catalog={Base};password={ClaveBase}";
            List<Tabla> listTabla = new List<Tabla>();

            //var categorias = new List<string> { "MAXIMA", "PRIMERA", "SEGUNDA", "SUB18", "SUB40" };
            List<string> categorias = GetCategoriasActivas(strCadena, Convert.ToInt32(campeonato));

            // Si no hay categorías activas, retornamos vacío
            if (categorias.Count == 0)
                return listTabla;
            foreach (var categoria in categorias)
            {
                string etapa = Etapa(campeonato, categoria);

                // 🔥 Generamos el mismo SQL pero usando la categoría variable
                string sql = GenerarSqlTablaPosiciones(campeonato, etapa, categoria);

                using (SqlConnection con = new SqlConnection(strCadena))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Tabla datos = new Tabla();
                        datos.Posicion = Convert.ToInt32(dr["POS"]);
                        datos.Equipo = dr["EQUIPO"].ToString().Trim();
                        datos.Pj = Convert.ToInt32(dr["PJ"]);
                        datos.Pg = Convert.ToInt32(dr["PG"]);
                        datos.Pp = Convert.ToInt32(dr["PP"]);
                        datos.Pe = Convert.ToInt32(dr["PE"]);
                        datos.Gf = Convert.ToInt32(dr["GF"]);
                        datos.Gc = Convert.ToInt32(dr["GC"]);
                        datos.Puntos = Convert.ToInt32(dr["PUNTOS"]);
                        datos.Gd = Convert.ToInt32(dr["GD"]);
                        datos.Grupo = dr["GRUPO"].ToString().Trim();
                        datos.Bonificacion = Convert.ToDecimal(dr["BONIFICACION"]);
                        datos.Total = Convert.ToDecimal(dr["TOTAL"]);
                        datos.Categoria = categoria;
                        datos.Logo = "http://181.39.104.93:5015/logo.aspx?id=" + datos.Equipo;

                        listTabla.Add(datos);
                    }
                }
            }

            return listTabla;
        }


        // GET: api/Tabla/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Tabla
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Tabla/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tabla/5
        public void Delete(int id)
        {
        }

        private string Campeonato()
        {
            string campeonato = DateTime.Now.Year.ToString("0000");
            string etapa = Etapa(campeonato, "MAXIMA");

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
            string sql = "SELECT TOP 1 CAMPEONATO FROM CONFIGURACIONES ORDER BY CAMPEONATO DESC";

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
        private string Etapa(string Campeonato, string Categoria)
        {
            string etapa = "";
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
            string sql = "SELECT TOP 1 etapa FROM etapas where campeonato=" + Campeonato + " and categoria='" + Categoria + "' ORDER BY etapa DESC";

            SqlCommand obj = new SqlCommand(sql, con);
            obj.CommandType = CommandType.Text;
            con.Open();

            SqlDataReader dr = obj.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    etapa = dr[0].ToString().Trim();
                }

            }
            else
            {
                etapa = "";
            }
            dr.Close();
            con.Close();
            con.Dispose();
            dr.Dispose();
            return etapa;

        }
        private List<string> GetCategoriasActivas(string strCadena, int campeonato)
        {
            List<string> categorias = new List<string>();

            using (SqlConnection con = new SqlConnection(strCadena))
            {
                string sql = @"
                        SELECT DISTINCT Categoria
                        FROM Etapas
                        WHERE isEnd = '0'
                          AND Campeonato = @Campeonato";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Campeonato", campeonato);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    categorias.Add(dr["Categoria"].ToString().Trim());
                }
            }

            return categorias;
        }
        private string GenerarSqlTablaPosiciones(string campeonato, string etapa, string categoria)
        {
            return $@"
                SELECT 
                    ROW_NUMBER() OVER
                    (
                        PARTITION BY X.GRUPO 
                        ORDER BY 
                            SUM(X.PUNTOS + X.BONIFICACION) DESC,
                            SUM(X.GL - X.GR) DESC,
                            SUM(X.GL) DESC,
                            SUM(X.GR) ASC
                    ) AS POS,
                    X.EQUIPO AS EQUIPO,
                    SUM(X.PJ) AS PJ,
                    SUM(X.PG) AS PG,
                    SUM(X.PP) AS PP,
                    SUM(X.PE) AS PE,
                    SUM(X.GL) AS GF,
                    SUM(X.GR) AS GC,
                    SUM(X.PUNTOS) AS PUNTOS,
                    SUM(X.GL - X.GR) AS GD,
                    X.GRUPO AS GRUPO,
                    SUM(X.BONIFICACION) AS BONIFICACION,
                    SUM(X.PUNTOS + X.BONIFICACION) AS TOTAL
                FROM
                (

                    SELECT
                        codloc,
                        (SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = PARTIDOS.CODLOC) AS EQUIPO,
                        COUNT(*) AS PJ,
                        SUM(CASE WHEN golesl > golesv THEN 1 ELSE 0 END) AS PG,
                        SUM(CASE WHEN golesl < golesv THEN 1 ELSE 0 END) AS PP,
                        SUM(CASE WHEN golesl = golesv THEN 1 ELSE 0 END) AS PE,
                        SUM(pl) AS PUNTOS,
                        SUM(golesl) AS GL,
                        SUM(golesv) AS GR,
                        (SELECT TOP 1 GRUPO 
                         FROM EquiposCampeonato 
                         WHERE EquiposCampeonato.ID = partidos.codLOC
                           AND Campeonato = {campeonato}
                           AND Categoria = '{categoria}'
                           AND Etapa = {etapa}) AS GRUPO,
                        0 AS BONIFICACION
                    FROM PARTIDOS
                    WHERE Campeonato = {campeonato}
                      AND Categoria = '{categoria}'
                      AND Etapa = {etapa}
                      AND jugado = 1
                    GROUP BY CODLOC

                    UNION ALL

                    SELECT
                        codVIS,
                        (SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = PARTIDOS.codVis) AS EQUIPO,
                        COUNT(*) AS PJ,
                        SUM(CASE WHEN golesl < golesv THEN 1 ELSE 0 END) AS PG,
                        SUM(CASE WHEN golesl > golesv THEN 1 ELSE 0 END) AS PP,
                        SUM(CASE WHEN golesl = golesv THEN 1 ELSE 0 END) AS PE,
                        SUM(pV) AS PUNTOS,
                        SUM(golesV) AS GL,
                        SUM(golesL) AS GR,
                        (SELECT TOP 1 GRUPO 
                         FROM EquiposCampeonato 
                         WHERE EquiposCampeonato.ID = partidos.codVIS
                           AND Campeonato = {campeonato}
                           AND Categoria = '{categoria}'
                           AND Etapa = {etapa}) AS GRUPO,
                        0 AS BONIFICACION
                    FROM PARTIDOS
                    WHERE Campeonato = {campeonato}
                      AND Categoria = '{categoria}'
                      AND Etapa = {etapa}
                      AND jugado = 1
                    GROUP BY CODvis

                    UNION ALL


                    SELECT
                        Id_Equipo,
                        (SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = SancionesE.Id_Equipo) AS EQUIPO,
                        0 AS PJ,
                        0 AS PG,
                        0 AS PP,
                        0 AS PE,
                        SUM(PUNTOS) AS PUNTOS,
                        SUM(goles) AS GL,
                        0 AS GR,
                        (SELECT TOP 1 GRUPO 
                         FROM EquiposCampeonato 
                         WHERE EquiposCampeonato.ID = SancionesE.Id_Equipo
                           AND Campeonato = {campeonato}
                           AND Categoria = '{categoria}'
                           AND Etapa = {etapa}) AS GRUPO,
                        0 AS BONIFICACION
                    FROM SancionesE
                    WHERE Campeonato = {campeonato}
                      AND Categoria = '{categoria}'
                      AND Etapa = {etapa}
                    GROUP BY Id_Equipo

                    UNION ALL

                    SELECT
                        Id_Equipo,
                        (SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID = Bonificaciones.Id_Equipo) AS EQUIPO,
                        0 AS PJ,
                        0 AS PG,
                        0 AS PP,
                        0 AS PE,
                        0 AS PUNTOS,
                        0 AS GL,
                        0 AS GR,
                        (SELECT TOP 1 GRUPO 
                         FROM EquiposCampeonato 
                         WHERE EquiposCampeonato.ID = Bonificaciones.Id_Equipo
                           AND Campeonato = {campeonato}
                           AND Categoria = '{categoria}'
                           AND Etapa = {etapa}) AS GRUPO,
                        SUM(PUNTOS) AS BONIFICACION
                    FROM Bonificaciones
                    WHERE Campeonato = {campeonato}
                      AND Categoria = '{categoria}'
                      AND Etapa = {etapa}
                    GROUP BY Id_Equipo
                ) X
                GROUP BY X.EQUIPO, X.GRUPO
                ORDER BY 
                    X.GRUPO ASC,
                    SUM(X.PUNTOS + X.BONIFICACION) DESC,
                    SUM(X.GL - X.GR) DESC,
                    SUM(X.GL) DESC,
                    SUM(X.GR) ASC;
                ";
        }

    }
}
