//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace КП_МДК._03
{
    using System;
    using System.Collections.Generic;
    
    public partial class OffsettingAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OffsettingAccount()
        {
            this.DataForPayroll = new HashSet<DataForPayroll>();
        }
    
        public int CodeOffsettingAccount { get; set; }
        public string DecryptionOffsettingAccount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataForPayroll> DataForPayroll { get; set; }
    }
}
