using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logistica.Service;

namespace Logistica.Page
{
    public partial class Logistica : System.Web.UI.Page
    {
        double[] camiones = new double[7];
        int[] mejor_camion = new int[7];
        int idcamion = 0;
        int utilizada_nueva = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Create a list of parts.
                List<Query> list = new List<Query>();

                // Add parts to the list.
                list.Add(new Query() { id = 1, capacidad_total = "3500 KG", capacidad_utilizada = "2450 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "San Carlos, Alajuela, Costa Rica", longitud = -84.4315437700567, latitud = 10.325254725116395 });
                list.Add(new Query() { id = 2, capacidad_total = "2800 KG", capacidad_utilizada = "1960 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "Santa Cruz, Guanacaste, Costa Rica", longitud = -85.58500952571039, latitud = 10.259358032174347 });
                list.Add(new Query() { id = 3, capacidad_total = "4200 KG", capacidad_utilizada = "2940 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "Pavas, San José, Costa Rica", longitud = -84.13523534815688, latitud = 9.94691722293743 });
                list.Add(new Query() { id = 4, capacidad_total = "3500 KG", capacidad_utilizada = "2450 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "Belén, Heredia, Costa Rica", longitud = -84.18509039230187, latitud = 9.97938939885529 });
                list.Add(new Query() { id = 5, capacidad_total = "4200 KG", capacidad_utilizada = "2940 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "Golfito, Puntarenas, Costa Rica", longitud = -83.16888791284043, latitud = 8.641072422521518 });
                list.Add(new Query() { id = 6, capacidad_total = "2800 KG", capacidad_utilizada = "1960 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "Barrio Roosevelt, Limón, Costa Rica", longitud = -83.03088142073382, latitud = 9.999034466218333 });
                list.Add(new Query() { id = 7, capacidad_total = "2800 KG", capacidad_utilizada = "1960 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "La Lima, Cartago, Costa Rica", longitud = -83.94429602479312, latitud = 9.871752070724582 });


                tb_list.DataSource = list;
                tb_list.DataBind();
            }

        }
        public List<Query> Lista
        {
            get
            {
                return (List<Query>)ViewState["Lista"];
            }
            set
            {
                ViewState["Lista"] = value;
            }
        }


        private double distance(double lon1, double lat1, double lon2, double lat2, char unit = 'K')
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


        protected void Consultar_ServerClick(object sender, EventArgs e)
        {
            if ((longitud.Value.Equals("")) && (latitud.Value.Equals("")) && (peso_extra.Value.Equals("")))
            {

            }
            else
            {
                double longi = 0;
                double lat = 0;
                string lon_remplazada = longitud.Value.Replace(".", ",");
                string lat_remplazada = latitud.Value.Replace(".", ",");
                decimal lon_redondeado = decimal.Round(Convert.ToDecimal(lon_remplazada), 3, MidpointRounding.AwayFromZero);
                decimal lat_redondeado = decimal.Round(Convert.ToDecimal(lat_remplazada), 3, MidpointRounding.AwayFromZero);
                double plon = Convert.ToDouble(lon_redondeado);
                double plat = Convert.ToDouble(lat_redondeado);
                double distancia = 0;
                // Create a list of parts.
                List<Query> list = new List<Query>();

                // Add parts to the list.
                list.Add(new Query() { id = 1, capacidad_total = "3500 KG", capacidad_utilizada = "2450 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "San Carlos, Alajuela, Costa Rica", longitud = -84.4315437700567, latitud = 10.325254725116395 });
                list.Add(new Query() { id = 2, capacidad_total = "2800 KG", capacidad_utilizada = "1960 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "Santa Cruz, Guanacaste, Costa Rica", longitud = -85.58500952571039, latitud = 10.259358032174347 });
                list.Add(new Query() { id = 3, capacidad_total = "4200 KG", capacidad_utilizada = "2940 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "Pavas, San José, Costa Rica", longitud = -84.13523534815688, latitud = 9.94691722293743 });
                list.Add(new Query() { id = 4, capacidad_total = "3500 KG", capacidad_utilizada = "2450 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "Belén, Heredia, Costa Rica", longitud = -84.18509039230187, latitud = 9.97938939885529 });
                list.Add(new Query() { id = 5, capacidad_total = "4200 KG", capacidad_utilizada = "2940 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "Golfito, Puntarenas, Costa Rica", longitud = -83.16888791284043, latitud = 8.641072422521518 });
                list.Add(new Query() { id = 6, capacidad_total = "2800 KG", capacidad_utilizada = "1960 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "Barrio Roosevelt, Limón, Costa Rica", longitud = -83.03088142073382, latitud = 9.999034466218333 });
                list.Add(new Query() { id = 7, capacidad_total = "2800 KG", capacidad_utilizada = "1960 KG", ruta_ini = "Coyol, Alajuela, Costa Rica", ruta_fin = "La Lima, Cartago, Costa Rica", longitud = -83.94429602479312, latitud = 9.871752070724582 });
                int cont = 0;
                foreach (var dto in list)
                {
                    longi = dto.longitud;
                    lat = dto.latitud;
                    string lon_remplazada2 = longi.ToString().Replace(".", ",");
                    string lat_remplazada2 = lat.ToString().Replace(".", ",");
                    decimal lon_redondeado2 = decimal.Round(Convert.ToDecimal(lon_remplazada2), 3, MidpointRounding.AwayFromZero);
                    decimal lat_redondeado2 = decimal.Round(Convert.ToDecimal(lat_remplazada2), 3, MidpointRounding.AwayFromZero);
                    double plon2 = Convert.ToDouble(lon_redondeado2);
                    double plat2 = Convert.ToDouble(lat_redondeado2);
                    distancia = distance(plon, plat, plon2, plat2);
                    string distancia_remplazada = distancia.ToString().Replace(".", ",");
                    decimal distancia_redondeada = decimal.Round(Convert.ToDecimal(distancia_remplazada), 1, MidpointRounding.AwayFromZero);
                    distancia = Convert.ToDouble(distancia_redondeada);
                    camiones[cont] = distancia;
                    cont++;
                }
                double primero = 100000000000;
                double segundo = 100000000000;
                double tercero = 100000000000;
                double cuarto = 100000000000;
                double quinto = 100000000000;
                double sexto = 100000000000;
                double septimo = 100000000000;

                for (int i = 0; i < camiones.Length; i++)
                {
                    if (camiones[i] <= primero)
                    {
                        septimo = sexto;
                        sexto = quinto;
                        quinto = cuarto;
                        cuarto = tercero;
                        tercero = segundo;
                        segundo = primero;
                        primero = camiones[i];
                        mejor_camion[6] = mejor_camion[5];
                        mejor_camion[5] = mejor_camion[4];
                        mejor_camion[4] = mejor_camion[3];
                        mejor_camion[3] = mejor_camion[2];
                        mejor_camion[2] = mejor_camion[1];
                        mejor_camion[1] = mejor_camion[0];
                        mejor_camion[0] = i + 1;

                    }
                    else
                    if (camiones[i] <= segundo)
                    {
                        septimo = sexto;
                        sexto = quinto;
                        quinto = cuarto;
                        cuarto = tercero;
                        tercero = segundo;
                        segundo = camiones[i];
                        mejor_camion[6] = mejor_camion[5];
                        mejor_camion[5] = mejor_camion[4];
                        mejor_camion[4] = mejor_camion[3];
                        mejor_camion[3] = mejor_camion[2];
                        mejor_camion[2] = mejor_camion[1];
                        mejor_camion[1] = i + 1;
                    }
                    else
                    if (camiones[i] <= tercero)
                    {
                        septimo = sexto;
                        sexto = quinto;
                        quinto = cuarto;
                        cuarto = tercero;
                        tercero = camiones[i];
                        mejor_camion[6] = mejor_camion[5];
                        mejor_camion[5] = mejor_camion[4];
                        mejor_camion[4] = mejor_camion[3];
                        mejor_camion[3] = mejor_camion[2];
                        mejor_camion[2] = i + 1;
                    }
                    else
                    if (camiones[i] <= cuarto)
                    {
                        septimo = sexto;
                        sexto = quinto;
                        quinto = cuarto;
                        cuarto = camiones[i];
                        mejor_camion[6] = mejor_camion[5];
                        mejor_camion[5] = mejor_camion[4];
                        mejor_camion[4] = mejor_camion[3];
                        mejor_camion[3] = i + 1;
                    }
                    else
                    if (camiones[i] <= quinto)
                    {
                        septimo = sexto;
                        sexto = quinto;
                        quinto = camiones[i];
                        mejor_camion[6] = mejor_camion[5];
                        mejor_camion[5] = mejor_camion[4];
                        mejor_camion[4] = i + 1;
                    }
                    else
                    if (camiones[i] <= sexto)
                    {
                        septimo = sexto;
                        sexto = camiones[i];
                        mejor_camion[6] = mejor_camion[5];
                        mejor_camion[5] = i + 1;
                    }
                    else
                    if (camiones[i] <= septimo)
                    {
                        septimo = camiones[i];
                        mejor_camion[6] = i + 1;
                    }
                }
                cont = -1;
                int encontrar_distancia = 0;
                bool validacion_mejorcamion = false;
                bool validacion_nohaycamion = false;
                while (validacion_mejorcamion == false)
                {
                    cont++;
                    if (cont == 7)
                    {
                        validacion_nohaycamion = true;
                        validacion_mejorcamion = true;
                    }
                    else
                    {
                        foreach (var dto in list)
                        {
                            string[] stotal = dto.capacidad_total.Split(' ');

                            string[] sutilizada = dto.capacidad_utilizada.Split(' ');

                            int total = Convert.ToInt32(stotal[0]);
                            int utilizada = Convert.ToInt32(sutilizada[0]);

                            int ptotal = utilizada + Convert.ToInt32(peso_extra.Value);
                            
                            if (mejor_camion[cont] == dto.id)
                            {
                                if (ptotal <= total)
                                {
                                    validacion_mejorcamion = true;
                                    idcamion = dto.id;
                                    utilizada_nueva = ptotal;
                                    encontrar_distancia = dto.id - 1;
                                    distancia = camiones[encontrar_distancia];
                                }
                            }

                        }
                    }

                }

                string cadenaresultado = "";
                if (validacion_nohaycamion == true)
                {
                    cadenaresultado = "Por el momento no hay camiones para realizar la parada ya que ninguno tiene la capacidad de peso necesaria";
                    validacion_mejorcamion = false;
                }
                else
                    if (validacion_mejorcamion == true)
                {
                    cadenaresultado = "El camión: #" + idcamion + " es el que cumple con los requerimientos para la parada, sumando el peso de la parada se estaría usando una capacidad de: " + utilizada_nueva + "KG. Y se estaría desviando solamente " + distancia + " KM.";
                }

                resultado.Value = cadenaresultado;
            }

        }
    }
}