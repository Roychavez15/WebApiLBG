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
    public class PartidosFechaController : ApiController
    {
        // GET: api/PartidosFecha
        public IEnumerable<PartidosFecha> Get()
        {
            string SERVIDORNEW = (string)new System.Configuration.AppSettingsReader().GetValue("Servidor", typeof(string));
            string UsuarioBase = (string)new System.Configuration.AppSettingsReader().GetValue("Usuario", typeof(string));
            string ClaveBase = (string)new System.Configuration.AppSettingsReader().GetValue("Clave", typeof(string));
            string Base = (string)new System.Configuration.AppSettingsReader().GetValue("Base", typeof(string));

            string campeonato = Campeonato();

            string salida = "";
            string strCadena = "";
            strCadena = strCadena + "workstation id=SOPORTE;";
            strCadena = strCadena + "packet size=4096;";
            strCadena = strCadena + "user id=" + UsuarioBase + ";";
            strCadena = strCadena + "data source=" + SERVIDORNEW + ";";
            strCadena = strCadena + "persist security info=True;";
            strCadena = strCadena + "initial catalog=" + Base + ";";
            strCadena = strCadena + "password=" + ClaveBase;

            List<PartidosFecha> listTabla = new List<PartidosFecha>();

            SqlConnection con1 = new SqlConnection(strCadena);

            string sql1 = "";

            sql1 = "SELECT [dia],[hora]";
            sql1 += " ,(select nombrecorto from equipos where equipos.id=codLoc) 'LOCAL'";
            sql1 += " ,(select nombrecorto from equipos where equipos.id=codVis) 'VISITANTE'";
            sql1 += " ,(SELECT SUM(GOLES) FROM JUGADOS WHERE JUGADOS.ID_PARTIDO = PARTIDOS.ID AND JUGADOS.ID_EQUIPO = PARTIDOS.CODLOC) 'GOLESL'";
            sql1 += " ,(SELECT SUM(GOLES) FROM JUGADOS WHERE JUGADOS.ID_PARTIDO = PARTIDOS.ID AND JUGADOS.ID_EQUIPO = PARTIDOS.CODVIS) 'GOLESV'";
            //sql1 += " ,CASE WHEN(SELECT SUM(GOLES) FROM JUGADOS WHERE JUGADOS.ID_PARTIDO= PARTIDOS.ID AND JUGADOS.ID_EQUIPO= PARTIDOS.CODLOC) !=NULL";
            //sql1 += " THEN(SELECT SUM(GOLES) FROM JUGADOS WHERE JUGADOS.ID_PARTIDO= PARTIDOS.ID AND JUGADOS.ID_EQUIPO= PARTIDOS.CODLOC) ELSE GOLESL END AS 'GOLESL'";
            //sql1 += " ,CASE WHEN(SELECT SUM(GOLES) FROM JUGADOS WHERE JUGADOS.ID_PARTIDO= PARTIDOS.ID AND JUGADOS.ID_EQUIPO= PARTIDOS.CODVIS) !=NULL";
            //sql1 += " THEN(SELECT SUM(GOLES) FROM JUGADOS WHERE JUGADOS.ID_PARTIDO= PARTIDOS.ID AND JUGADOS.ID_EQUIPO= PARTIDOS.CODVIS) ELSE GOLESV END AS 'GOLESV'";
            sql1 += " ,[pl],[pv],[categoria],[jugado],(SELECT NOMBRECORTO FROM EQUIPOS WHERE EQUIPOS.ID=PARTIDOS.COD_VOCAL) 'VOCAL',codLoc,codVis,estado,DATEDIFF(ss, [dia_partido], GETDATE())/60";
            sql1 += " ,[golesl],[golesv],[jugado]";
            sql1 += " FROM partidos(nolock) where Campeonato=" + campeonato;
            sql1 += " and DATEPART(dy, dia)>=(select top 1 DATEPART(dy, dia) from partidos where campeonato = " + campeonato + " order by CAST(dia as datetime) desc) - 3";
            sql1 += " and DATEPART(YEAR, dia)=" + DateTime.Now.Year.ToString();
            sql1 += " order by CAST(dia+' ' + hora as datetime) asc";


            SqlCommand obj1 = new SqlCommand(sql1, con1);
            obj1.CommandType = CommandType.Text;
            con1.Open();

            int contador = 1;
            int contador1 = 1;
            SqlDataReader dr1 = obj1.ExecuteReader();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    //<p class="text-center">Center aligned text.</p>
                    string diasemana = "";
                    string Mes = "";
                    if (dr1[0].ToString().Trim() != "")
                    {
                        DateTime dia = Convert.ToDateTime(dr1[0].ToString().Trim());
                        if (dia.DayOfWeek == DayOfWeek.Monday)
                        {
                            diasemana = "Lun.";
                        }
                        else if (dia.DayOfWeek == DayOfWeek.Tuesday)
                        {
                            diasemana = "Mar.";
                        }
                        else if (dia.DayOfWeek == DayOfWeek.Wednesday)
                        {
                            diasemana = "Mié.";
                        }
                        else if (dia.DayOfWeek == DayOfWeek.Thursday)
                        {
                            diasemana = "Jue.";
                        }
                        else if (dia.DayOfWeek == DayOfWeek.Friday)
                        {
                            diasemana = "Vie.";
                        }
                        else if (dia.DayOfWeek == DayOfWeek.Saturday)
                        {
                            diasemana = "Sáb.";
                        }
                        else if (dia.DayOfWeek == DayOfWeek.Sunday)
                        {
                            diasemana = "Dom.";
                        }

                        if (dia.Month == 1)
                        {
                            Mes = "Ene.";
                        }
                        else if (dia.Month == 2)
                        {
                            Mes = "Feb.";
                        }
                        else if (dia.Month == 3)
                        {
                            Mes = "Mar.";
                        }
                        else if (dia.Month == 4)
                        {
                            Mes = "Abr.";
                        }
                        else if (dia.Month == 5)
                        {
                            Mes = "May.";
                        }
                        else if (dia.Month == 6)
                        {
                            Mes = "Jun.";
                        }
                        else if (dia.Month == 7)
                        {
                            Mes = "Jul.";
                        }
                        else if (dia.Month == 8)
                        {
                            Mes = "Ago.";
                        }
                        else if (dia.Month == 9)
                        {
                            Mes = "Sep.";
                        }
                        else if (dia.Month == 10)
                        {
                            Mes = "Oct.";
                        }
                        else if (dia.Month == 11)
                        {
                            Mes = "Nov.";
                        }
                        else if (dia.Month == 12)
                        {
                            Mes = "Dic.";
                        }

                        PartidosFecha datos = new PartidosFecha();
                        datos.Dia = diasemana + " " + dia.Day.ToString() + " " + Mes;
                        datos.Hora= dr1[1].ToString().Trim();
                        datos.Local= dr1[2].ToString().Trim();
                        datos.Visitante= dr1[3].ToString().Trim();
                        datos.GolesLocal= dr1[4].ToString().Trim();
                        datos.GolesVisitante= dr1[5].ToString().Trim();
                        datos.Categoria= dr1[8].ToString().Trim();
                        datos.Jugado= dr1[9].ToString().Trim();
                        if (dr1[10].ToString().Trim() != "")
                        {
                            datos.Vocal = "Vocal: " + dr1[10].ToString().Trim();
                        }
                        else
                        {
                            datos.Vocal = "Vocal: Directivo";
                        }
                        datos.Estado= dr1[13].ToString().Trim();
                        datos.Tiempo= dr1[14].ToString().Trim();
                        datos.GolesL= dr1[15].ToString().Trim();
                        datos.GolesV= dr1[16].ToString().Trim();

                        if (dr1[13].ToString().Trim() == "0")//por jugar
                        {
                            if (dr1[17].ToString().Trim() == "0")//finalizado por retiro de equipo
                            {
                                datos.Tiempo = "";
                                datos.GolesL = "";
                                datos.GolesV = "";
                                datos.GolesLocal = "";
                                datos.GolesVisitante = "";
                            }
                            else
                            {
                                datos.Tiempo = "";
                                datos.GolesL = dr1[15].ToString().Trim();
                                datos.GolesV = dr1[16].ToString().Trim();
                                datos.GolesLocal = dr1[15].ToString().Trim();
                                datos.GolesVisitante = dr1[16].ToString().Trim();
                            }
                        }
                        else if (dr1[13].ToString().Trim() == "1")//jugandose
                        {
                            if (dr1[4].ToString().Trim() != "")
                            {
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[2].ToString().Trim() + "'/> " + dr1[2].ToString().Trim() + "<span class='badge bg-blue pull-right'>" + dr1[4].ToString().Trim() + "</span></h6> </div>";
                                datos.GolesL = dr1[4].ToString().Trim();
                                datos.GolesLocal = dr1[4].ToString().Trim();
                            }
                            else
                            {
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[2].ToString().Trim() + "'/> " + dr1[2].ToString().Trim() + "<span class='badge bg-blue pull-right'>0</span></h6> </div>";
                                datos.GolesL = "0";
                                datos.GolesLocal = "0";
                            }
                            if (dr1[5].ToString().Trim() != "")
                            {
                                datos.GolesV = dr1[5].ToString().Trim();
                                datos.GolesVisitante = dr1[5].ToString().Trim();
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[3].ToString().Trim() + "'/> " + dr1[3].ToString().Trim() + "<span class='badge bg-blue pull-right'>" + dr1[5].ToString().Trim() + "</span></h6> </div>";
                            }
                            else
                            {
                                datos.GolesV = "0";
                                datos.GolesVisitante = "0";
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[3].ToString().Trim() + "'/> " + dr1[3].ToString().Trim() + "<span class='badge bg-blue pull-right'>0</span></h6> </div>";
                            }                            
                            datos.Tiempo = dr1[14].ToString().Trim()+"'";
                        }
                        else if (dr1[13].ToString().Trim() == "2")//finalizado
                        {
                            if (dr1[4].ToString().Trim() != "")
                            {
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[2].ToString().Trim() + "'/> " + dr1[2].ToString().Trim() + "<span class='badge bg-blue pull-right'>" + dr1[4].ToString().Trim() + "</span></h6> </div>";
                                datos.GolesL = dr1[4].ToString().Trim();
                                datos.GolesLocal = dr1[4].ToString().Trim();
                            }
                            else
                            {
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[2].ToString().Trim() + "'/> " + dr1[2].ToString().Trim() + "<span class='badge bg-blue pull-right'>" + dr1[15].ToString().Trim() + "</span></h6> </div>";
                                datos.GolesL = dr1[15].ToString().Trim();
                                datos.GolesLocal = dr1[15].ToString().Trim();
                            }
                            if (dr1[5].ToString().Trim() != "")
                            {
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[3].ToString().Trim() + "'/> " + dr1[3].ToString().Trim() + "<span class='badge bg-blue pull-right'>" + dr1[5].ToString().Trim() + "</span></h6> </div>";
                                datos.GolesV = dr1[5].ToString().Trim();
                                datos.GolesVisitante = dr1[5].ToString().Trim();

                            }
                            else
                            {
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[3].ToString().Trim() + "'/> " + dr1[3].ToString().Trim() + "<span class='badge bg-blue pull-right'>" + dr1[16].ToString().Trim() + "</span></h6> </div>";
                                datos.GolesV = dr1[16].ToString().Trim();
                                datos.GolesVisitante = dr1[16].ToString().Trim();
                            }
                            datos.Tiempo = "";
                            //salida += "<span class='count_bottom'><i class='green'><i class='fa fa-clock-o'></i> Finalizado </i></span>";
                        }
                        else if (dr1[13].ToString().Trim() == "D")//DESCANSO
                        {
                            if (dr1[4].ToString().Trim() != "")
                            {
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[2].ToString().Trim() + "'/> " + dr1[2].ToString().Trim() + "<span class='badge bg-blue pull-right'>" + dr1[4].ToString().Trim() + "</span></h6> </div>";
                                datos.GolesL = dr1[4].ToString().Trim();
                                datos.GolesLocal = dr1[4].ToString().Trim();
                            }
                            else
                            {
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[2].ToString().Trim() + "'/> " + dr1[2].ToString().Trim() + "<span class='badge bg-blue pull-right'>0</span></h6> </div>";
                                datos.GolesL = "0";
                                datos.GolesLocal = "0";
                            }
                            if (dr1[5].ToString().Trim() != "")
                            {
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[3].ToString().Trim() + "'/> " + dr1[3].ToString().Trim() + "<span class='badge bg-blue pull-right'>" + dr1[5].ToString().Trim() + "</span></h6> </div>";
                                datos.GolesV = dr1[5].ToString().Trim();
                                datos.GolesVisitante = dr1[5].ToString().Trim();
                            }
                            else
                            {
                                //salida += "<div><h6><img Height='20px' Width='20px' src='logo.aspx?id=" + dr1[3].ToString().Trim() + "'/> " + dr1[3].ToString().Trim() + "<span class='badge bg-blue pull-right'>0</span></h6> </div>";
                                datos.GolesV = "0";
                                datos.GolesVisitante = "0";
                            }
                            datos.Tiempo = "";
                            //salida += "<span class='count_bottom'><i class='green'><i class='fa fa-clock-o'></i> Entretiempo </i></span>";
                        }

                        datos.LogoL = "http://181.39.104.93:5015/logo.aspx?id=" + dr1[2].ToString().Trim();
                        datos.LogoV = "http://181.39.104.93:5015/logo.aspx?id=" + dr1[3].ToString().Trim();
                        listTabla.Add(datos);
                    }
                    else
                    {
                        ////SIN PARTIDOS
                    }
                }
            }
            else
            {

            }

            con1.Close();
            con1.Dispose();

            return listTabla;
        }

        // GET: api/PartidosFecha/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PartidosFecha
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PartidosFecha/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PartidosFecha/5
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
