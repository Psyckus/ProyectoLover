﻿using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CapaNegocios
{
    public class CN_Clientes
    {
        private CD_Cliente objCapaDato = new CD_Cliente();




        #region reconsiderado
        public bool Reconsiderado(int cliente1, int cliente2)
        {
            return objCapaDato.Reconsiderado(cliente1, cliente2);
        }
        #endregion



        #region Rechazado
        public bool Rechazado(int cliente1, int cliente2)
        {
            return objCapaDato.Rechazado(cliente1, cliente2);
        }
        #endregion


        #region Aceptado
        public bool Aceptado(int cliente1, int cliente2)
        {
            return objCapaDato.Aceptado(cliente1, cliente2);
        }
        #endregion


        #region gusrdarSuspiro
        public bool guardarSuspiro(int cliente1, int cliente2)
        {
            return objCapaDato.guardarSuspiro(cliente1, cliente2);
        }

        #endregion



        public List<suspiro1> ListarSuspiro(int cliente1)
        {
            return objCapaDato.ListarSuspiro(cliente1);
        }


        public List<suspiro1> ListarSuspiroAceptados(int cliente1)
        {
            return objCapaDato.ListarSuspiroAceptados(cliente1);
        }


        public List<suspiro1> ListarSuspiroRecibido(int cliente2)
        {
            return objCapaDato.ListarSuspiroRecibido(cliente2);
        }





        #region Matches
        public bool match(int cliente1, int cliente2, int activo)
        {
            return objCapaDato.matches(cliente1, cliente2, activo);
        }


        #endregion


        #region guardarMatches
        public bool guardarMatch(int cliente1, int cliente2)
        {
            return objCapaDato.guardarMatch(cliente1, cliente2);
        }
        #endregion

        #region mostrarMatch
        public match1 mostrarMatch(int cliente1, int cliente2, int cliente3)
        {
            return objCapaDato.mostrarMatch(cliente1, cliente2, cliente3);
        }

        #endregion

        #region Descubrir
        public descubrir Descubrir(int cliente1, int idCliente)
        {
            return objCapaDato.Descubrir(cliente1, idCliente);
        }

        #endregion



        public bool geolocalizacion(int idCliente, string latitud, string longitud, out string mensaje)
        {
            return objCapaDato.geolocalizacion(idCliente, latitud, longitud, out mensaje);
        }



        public int Registrar(cliente obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "El nombre del cliente no puede estar vacio";
            }


            else if (string.IsNullOrEmpty(obj.email) || string.IsNullOrWhiteSpace(obj.email))
            {
                mensaje = "El email del cliente no puede estar vacio";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                obj.clave = CN_Recurso.ConvertirSha256(obj.clave);
                return objCapaDato.Registrar(obj, out mensaje);

            }
            else
            {

                return 0;

            }

        }
        public bool Pregunta1(int idCliente, string fecha_nac, out string mensaje)
        {
            return objCapaDato.Pregunta1(idCliente, fecha_nac, out mensaje);

        }
        public bool Pregunta2(int idCliente, string descripcion, out string mensaje)
        {
            return objCapaDato.Pregunta2(idCliente, descripcion, out mensaje);

        }
        public List<cliente> Listar()
        {
            return objCapaDato.Listar();
        }

        public bool CambiarClave(int idCliente, string nuevaclave, out string mensaje)
        {
            return objCapaDato.CambiarClave(idCliente, nuevaclave, out mensaje);
        }
        public bool Pregunta3(int idCliente, HttpPostedFileBase file, out string mensaje)
        {
            return objCapaDato.Pregunta3(idCliente, file, out mensaje);
        }
        public bool Pregunta3Primer(int idCliente, HttpPostedFileBase file, out string mensaje)
        {
            return objCapaDato.Pregunta3PrimeraVez(idCliente, file, out mensaje);
        }


        public bool ReestablecerClave(int idCliente, string correo, out string mensaje)
        {
            mensaje = string.Empty;
            string nuevaclave = CN_Recurso.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idCliente, CN_Recurso.ConvertirSha256(nuevaclave), out mensaje);
            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su cuenta fue restablecida correctamente</h3></br><p>Su contraseña para acceder ahora es: !clave! </p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);
                bool respuesta = CN_Recurso.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                mensaje = "No se pudo reestablecer  la Contraseña";
                return false;
            }



        }

    }
}