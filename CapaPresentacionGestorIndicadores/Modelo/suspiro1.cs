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
    
    public partial class suspiro1
    {
        public int idSuspiro { get; set; }
        public Nullable<int> cliente2 { get; set; }
        public Nullable<int> cliente1 { get; set; }
        public Nullable<int> idEstado { get; set; }
        public Nullable<System.DateTime> fechaRegistro { get; set; }
    
        public virtual cliente cliente { get; set; }
        public virtual cliente cliente3 { get; set; }
        public virtual estado estado { get; set; }
    }
}
