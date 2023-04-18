using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Pregunta
    {
        private CD_Preguntas objCapaDato = new CD_Preguntas();

        public List<preguntaTest> Listar()
        {
            return objCapaDato.Listar();
        }
        public List<test> Listart()
        {
            return objCapaDato.Listart();
        }
        public int Registrar(preguntaTest obj, out string mensaje)
        {
            mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "Debe ingresar un nombre";
            }

            else if (obj.otest.idtest == 0)
            {
                mensaje = "Debe ingresa el test";
            }


            return objCapaDato.Registrar(obj, out mensaje);

        }

        public bool editar(preguntaTest obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.nombre) || string.IsNullOrWhiteSpace(obj.nombre))
            {
                mensaje = "Debe ingresar un nombre";
            }
            else if (obj.otest.idtest== 0)
            {
                mensaje = "Debe ingresar el test";
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