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
    
    public partial class Mensajes
    {
        public int Id { get; set; }
        public Nullable<int> idCliente1 { get; set; }
        public Nullable<int> idCliente2 { get; set; }
        public string Mensaje { get; set; }
        public Nullable<System.DateTime> FechaHora { get; set; }
    
        public virtual cliente cliente { get; set; }
        public virtual cliente cliente1 { get; set; }
    }
}
