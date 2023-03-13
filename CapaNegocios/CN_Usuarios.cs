using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<usuario> Listar()
        {
            return objCapaDato.Listar();
        }

        public List<tipoIdentificacion> ListarTipoID()
        {
            return objCapaDato.ListarTipoID();
        }

        public List<rol> ListarRoles()
        {
            return objCapaDato.ListarRoles();
        }



        public int Registrar(usuario obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.identificacion == 0)
            {
                mensaje = "Debe ingresar una identificacion";
            }

            else if (obj.orol.idRol == 0)
            {
                mensaje = "Debe ingresar un rol";
            }
            else if (obj.otipoIdentificacion.idTipoIdentificacion == 0)
            {
                mensaje = "Debe ingresar un tipo de identificacion";
            }

            else if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "Debe ingresar un nombre";
            }
            else if (string.IsNullOrEmpty(obj.apellido) || string.IsNullOrWhiteSpace(obj.apellido))
            {
                mensaje = "Debe ingresar un apellido";
            }
            else if (string.IsNullOrEmpty(obj.email) || string.IsNullOrWhiteSpace(obj.email))
            {
                mensaje = "Debe ingresar un email";
            }
            

            if (string.IsNullOrEmpty(mensaje))
            {




                string clave = CN_Recurso.GenerarClave();
                string asunto = "creación de cuenta";
                string mensaje_correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su clave para acceder es: !clave! </p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = CN_Recurso.EnviarCorreo(obj.email, asunto, mensaje_correo);

                if (respuesta)
                {
                    obj.clave = CN_Recurso.ConvertirSha256(clave);
                    return objCapaDato.Registrar(obj, out mensaje);
                }
                else
                {
                    mensaje = "No se puede enviar el correo";
                    return 0;
                }



            }
            else
            {

                return 0;

            }

        }

        public bool editar(usuario obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.identificacion == 0)
            {
                mensaje = "Debe ingresar una identificacion";
            }

            else if (obj.orol.idRol == 0)
            {
                mensaje = "Debe ingresar un rol";
            }
            else if (obj.otipoIdentificacion.idTipoIdentificacion == 0)
            {
                mensaje = "Debe ingresar un tipo de identificacion";
            }

            else if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "Debe ingresar un nombre";
            }
            else if (string.IsNullOrEmpty(obj.apellido) || string.IsNullOrWhiteSpace(obj.apellido))
            {
                mensaje = "Debe ingresar un apellido";
            }
            else if (string.IsNullOrEmpty(obj.email) || string.IsNullOrWhiteSpace(obj.email))
            {
                mensaje = "Debe ingresar un email";
            }
           

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDato.editar(obj, out mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool eliminar(int id, out string mensaje)
        {
            return objCapaDato.eliminar(id, out mensaje);
        }
        public bool CambiarClave(int idusuario, string nuevaclave, out string mensaje)
        {
            return objCapaDato.CambiarClave(idusuario, nuevaclave, out mensaje);
        }
        public bool ReestablecerClave(int idusuario, string correo, out string mensaje)
        {
            mensaje = string.Empty;
            string nuevaclave = CN_Recurso.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idusuario, CN_Recurso.ConvertirSha256(nuevaclave), out mensaje);
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
