//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaPresentacionGestorIndicadores.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class preguntaTest
    {
        public int idpreguntaTest { get; set; }
        public Nullable<int> idTest { get; set; }
        public string nombre { get; set; }
    
        public virtual test test { get; set; }
    }
}