//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiSeguridadConJWT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            this.tarjeta = new HashSet<tarjeta>();
        }
    
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string usuario1 { get; set; }
        public string correo_electronico { get; set; }
        public string contrasena { get; set; }
        public string curp { get; set; }
        public string rfc { get; set; }
        public Nullable<int> telefono { get; set; }
        public Nullable<int> id_estado { get; set; }
        public Nullable<int> id_municipio { get; set; }
        public string calle { get; set; }
        public Nullable<int> numero_exterior_calle { get; set; }
        public Nullable<int> numero_interior_calle { get; set; }
    
        public virtual estado estado { get; set; }
        public virtual municipio municipio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tarjeta> tarjeta { get; set; }
    }
}
