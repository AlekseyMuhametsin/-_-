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
    
    public partial class AbsentForAReason
    {
        public int NumberAbsence { get; set; }
        public int PersonnelNumberAbsence { get; set; }
        public int CodeAbsence { get; set; }
        public int DaysAbsence { get; set; }
        public int HoursAbsence { get; set; }
        public string MonthAbsence { get; set; }
    
        public virtual DayCode DayCode { get; set; }
        public virtual Month Month { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
