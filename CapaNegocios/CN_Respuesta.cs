using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Respuesta
    {
        private CD_Respuesta objCapaDato = new CD_Respuesta();

        public List<respuestaTest> Listar()
        {
            return objCapaDato.Listar();
        }
        public List<preguntaTest> ListarP()
        {
            return objCapaDato.ListarP();
        }
        public int Registrar(respuestaTest obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (obj.opregunta.idPreguntaTest== 0)
            {
                mensaje = "Debe ingresar un tipo de pregunta";
            }

            else if (string.IsNullOrEmpty(obj.respuesta) || string.IsNullOrWhiteSpace(obj.respuesta))
            {
                mensaje = "Debe ingresar un nombre";
            }
            return objCapaDato.Registrar(obj, out mensaje);


        }




        public bool editar(respuestaTest obj, out string mensaje)
        {
            mensaje = string.Empty;
            if (obj.opregunta.idPreguntaTest == 0)
            {
                mensaje = "Debe ingresar una categoria";
            }

            else if (string.IsNullOrEmpty(obj.respuesta) || string.IsNullOrWhiteSpace(obj.respuesta))
            {
                mensaje = "Debe ingresar una respuesta";
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
    }
}
