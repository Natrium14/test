//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApp1.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Хозяева
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Хозяева()
        {
            this.Питомцы = new HashSet<Питомцы>();
        }
    
        public System.Guid ID_хозяина { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public bool Пол { get; set; }
        public string Номер_телефона { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Питомцы> Питомцы { get; set; }
    }
}
