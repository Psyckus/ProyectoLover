//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace loverSitios.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Correos
    {
        public int idCorreo { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public Nullable<int> idConfCorreo { get; set; }
    
        public virtual ConfCorreos ConfCorreos { get; set; }
    }
}
