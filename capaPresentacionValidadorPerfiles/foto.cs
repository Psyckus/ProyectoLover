//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace capaPresentacionValidadorPerfiles
{
    using System;
    using System.Collections.Generic;
    
    public partial class foto
    {
        public int idfoto { get; set; }
        public Nullable<int> idCliente { get; set; }
        public byte[] rutaFoto { get; set; }
    
        public virtual cliente cliente { get; set; }
    }
}