//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SI2_Trab2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ativo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ativo()
        {
            this.Ativo1 = new HashSet<Ativo>();
            this.Intervencaos = new HashSet<Intervencao>();
        }
    
        public decimal id { get; set; }
        public Nullable<decimal> parent_id { get; set; }
        public string nome { get; set; }
        public System.DateTime data_aquisicao { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string localizacao { get; set; }
        public decimal estado { get; set; }
        public Nullable<decimal> id_tipo { get; set; }
    
        public virtual Tipo Tipo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ativo> Ativo1 { get; set; }
        public virtual Ativo Ativo2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Intervencao> Intervencaos { get; set; }
    }
}
