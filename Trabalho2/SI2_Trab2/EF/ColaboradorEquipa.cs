//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SI2_Trab2.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ColaboradorEquipa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ColaboradorEquipa()
        {
            this.Competencias = new HashSet<Competencia>();
        }
    
        public decimal id { get; set; }
        public decimal id_equipa { get; set; }
    
        public virtual Equipa Equipa { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Competencia> Competencias { get; set; }
    }
}
