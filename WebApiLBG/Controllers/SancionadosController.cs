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
    public class SancionadosController : ApiController
    {
        // GET: api/Sancionados
        public IEnumerable<Sancionados> Get()
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

            List<Sancionados> listTabla = new List<Sancionados>();


            SqlConnection con = new SqlConnection(strCadena);
            SqlConnection.ClearAllPools();
            //string sql = "SELECT ";
            //sql += "[identificacion] AS IDENTIFICACION";
            //sql += ",(select NOMBRECORTO FROM Equipos WHERE Equipos.Id=(SELECT Equipo FROM JugadoresCampeonato WHERE JugadoresCampeonato.Identificacion = SancionesJ.identificacion AND JugadoresCampeonato.Tipo ='J' AND JugadoresCampeonato.Campeonato =" + campeonato + ")) AS EQUIPO";
            //sql += ",(select NUMERO from JugadoresCampeonato where JugadoresCampeonato.Identificacion =SancionesJ.identificacion and JugadoresCampeonato.tipo='J' AND JugadoresCampeonato.Campeonato =" +campeonato+ ") AS N ";
            //sql += ",(select nombres from JugadoresCampeonato where JugadoresCampeonato.Identificacion =SancionesJ.identificacion and JugadoresCampeonato.tipo='J' AND JugadoresCampeonato.Campeonato =" + campeonato + ") AS JUGADOR";
            //sql += ",(SELECT etapa FROM partidos WHERE partidos.Id=SancionesJ.id_partido) AS ETAPA";
            //sql += ",(SELECT FECHA FROM partidos WHERE partidos.Id=SancionesJ.id_partido) AS FECHA";
            //sql += ",[articulo] AS ARTICULO";
            //sql += ",[literal] AS LITERAL";
            //sql += ",[sancion] AS SANCION";
            //sql += ",[tipotiempo]  AS TIEMPO";
            //sql += ",SANCION - (SELECT COUNT (*) FROM partidos WHERE ";
            //sql += " partidos.Id>SancionesJ.id_partido";
            //sql += " AND campeonato =" + campeonato;
            //sql += " AND jugado=1 ";
            //sql += " AND ((partidos.codLoc =(SELECT JugadoresCampeonato.EQUIPO FROM JugadoresCampeonato WHERE JugadoresCampeonato.identificacion =SancionesJ.identificacion AND JugadoresCampeonato.Tipo ='J' AND JugadoresCampeonato.campeonato=" + campeonato + "))";
            //sql += " OR partidos.codVis =(SELECT JugadoresCampeonato.EQUIPO FROM JugadoresCampeonato WHERE JugadoresCampeonato.identificacion =SancionesJ.identificacion AND JugadoresCampeonato.Tipo ='J' AND JugadoresCampeonato.campeonato=" + campeonato + "))";
            //sql += " ) as FALTAN ";
            //sql += " FROM SancionesJ";
            //sql += " WHERE campeonato=" + campeonato;
            //sql += " AND ";
            //sql += " (SELECT COUNT (*) FROM partidos WHERE ";
            //sql += " partidos.Id>SancionesJ.id_partido";
            //sql += " AND campeonato =" + campeonato;
            //sql += " AND jugado=1 ";
            //sql += " AND ((partidos.codLoc =(SELECT JugadoresCampeonato.EQUIPO FROM JugadoresCampeonato WHERE JugadoresCampeonato.identificacion =SancionesJ.identificacion AND JugadoresCampeonato.Tipo ='J' AND JugadoresCampeonato.campeonato=" + campeonato + "))";
            //sql += " OR partidos.codVis =(SELECT JugadoresCampeonato.EQUIPO FROM JugadoresCampeonato WHERE JugadoresCampeonato.identificacion =SancionesJ.identificacion AND JugadoresCampeonato.Tipo ='J' AND JugadoresCampeonato.campeonato=" + campeonato + "))";
            //sql += " ) < SancionesJ.SANCION";
            //sql += " ORDER BY (SELECT etapa FROM partidos WHERE partidos.Id=SancionesJ.id_partido) desc, ";
            //sql += " (SELECT FECHA FROM partidos WHERE partidos.Id=SancionesJ.id_partido) desc, ";
            //sql += " (select NOMBRECORTO FROM Equipos WHERE Equipos.Id=(SELECT Equipo FROM JugadoresCampeonato WHERE JugadoresCampeonato.Identificacion = SancionesJ.identificacion AND JugadoresCampeonato.Tipo ='J' AND JugadoresCampeonato.Campeonato =" + campeonato + "))";

            string sql = @"                
                BEGIN TRY
                    WITH EquiposNombres AS (
                        SELECT Id, NOMBRECORTO
                        FROM Equipos
                    ),
                    Jugadores AS (
                        SELECT Identificacion, NUMERO, nombres, EQUIPO
                        FROM JugadoresCampeonato
                        WHERE tipo = 'J' AND Campeonato = @CAMPEONATO
                    ),
                    PartidosInfo AS (
                        SELECT Id, etapa, DIA, FECHA, codLoc, codVis, jugado, campeonato
                        FROM partidos
                    ),
                    SancionesCalculadas AS (
                        SELECT
                            S.id_partido,
                            S.identificacion,
                            S.ID_EQUIPO,
                            S.articulo,
                            S.literal,
                            S.sancion,
                            S.tipotiempo,
                            E.NOMBRECORTO AS EQUIPO,
                            J.NUMERO,
                            J.nombres AS JUGADOR,
                            P.etapa,
                            P.DIA,
                            P.FECHA,
                            CASE 
                                WHEN S.tipotiempo = 'MESES' THEN 
                                    CASE
                                        WHEN DATEADD(MONTH, S.SANCION, P.DIA) > GETDATE() THEN DATEDIFF(DAY, GETDATE(), DATEADD(MONTH, S.SANCION, P.DIA))
                                        ELSE 0
                                    END
                                WHEN S.tipotiempo = 'AÑOS' THEN 
                                    CASE
                                        WHEN DATEADD(YEAR, S.SANCION, P.DIA) > GETDATE() THEN DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, S.SANCION, P.DIA))
                                        ELSE 0
                                    END
                                ELSE S.SANCION - (
                                    SELECT COUNT(*) 
                                    FROM PartidosInfo P1
                                    WHERE P1.Id > S.id_partido
                                    AND P1.campeonato = @CAMPEONATO
                                    AND P1.jugado = 1 
                                    AND (P1.codLoc = S.ID_EQUIPO OR P1.codVis = S.ID_EQUIPO)
                                )
                            END AS FALTAN
                        FROM SancionesJ S
                        LEFT JOIN EquiposNombres E ON E.Id = S.ID_EQUIPO
                        LEFT JOIN Jugadores J ON J.Identificacion = S.identificacion
                        LEFT JOIN PartidosInfo P ON P.Id = S.id_partido
                        WHERE S.campeonato = @CAMPEONATO
                    )
                    SELECT
                        identificacion AS IDENTIFICACION,
                        EQUIPO,
                        NUMERO AS N,
                        JUGADOR,
                        ETAPA,
                        FECHA AS FECHA,
                        articulo AS ARTICULO,
                        literal AS LITERAL,
                        sancion AS SANCION,
                        tipotiempo AS TIEMPO,
                        FALTAN
                    FROM SancionesCalculadas
                    WHERE FALTAN > 0
                END TRY
                BEGIN CATCH  
                    -- Manejo de errores
                    SELECT ERROR_MESSAGE() AS ErrorMessage;
                END CATCH;
                ";
            SqlCommand obj = new SqlCommand(sql, con);
            obj.CommandType = CommandType.Text;
            obj.Parameters.AddWithValue("@CAMPEONATO", campeonato);
            con.Open();

            SqlDataReader dr = obj.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Sancionados datos = new Sancionados();
                    datos.Identificacion = dr[0].ToString().Trim();
                    datos.Nombre = dr[3].ToString().Trim();
                    datos.Sancion = dr[8].ToString().Trim()+ " " + dr[9].ToString().Trim()+ " Faltan: " + dr[10].ToString().Trim();
                    datos.Equipo = dr[1].ToString().Trim();
                    datos.Numero = dr[2].ToString().Trim();
                    datos.Foto = "http://181.39.104.93:5015/ImageHandler.ashx?ImID=" + dr[0].ToString().Trim();
                    datos.Articulos = "Etapa: "+dr[4].ToString().Trim()+ " Fecha: " + dr[5].ToString().Trim()+ " Art: " + dr[6].ToString().Trim()+ " Lit: " + dr[7].ToString().Trim(); 
                    listTabla.Add(datos);
                }
            }

            dr.Close();
            con.Close();
            con.Dispose();
            dr.Dispose();

            return listTabla;
        }

        // GET: api/Sancionados/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sancionados
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Sancionados/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sancionados/5
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
    }
}
