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
    
    public partial class Intervencao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Intervencao()
        {
            this.IntervencaoEquipas = new HashSet<IntervencaoEquipa>();
        }
    
        public decimal id { get; set; }
        public string descricao { get; set; }
        public string estado { get; set; }
        public Nullable<decimal> valor { get; set; }
        public Nullable<System.DateTime> data_inicio { get; set; }
        public Nullable<System.DateTime> data_fim { get; set; }
        public Nullable<int> periodicidade { get; set; }
        public Nullable<decimal> ativo_id { get; set; }
    
        public virtual Ativo Ativo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntervencaoEquipa> IntervencaoEquipas { get; set; }
    }
}
