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
    
    public partial class MensajesConversaciones
    {
        public int id_mensaje { get; set; }
        public Nullable<int> id_match { get; set; }
        public Nullable<int> id_remitente { get; set; }
        public Nullable<int> id_destinatario { get; set; }
        public string mensaje { get; set; }
        public Nullable<System.DateTime> fecha_envio { get; set; }
    
        public virtual cliente cliente { get; set; }
        public virtual cliente cliente1 { get; set; }
        public virtual matches matches { get; set; }
    }
}
