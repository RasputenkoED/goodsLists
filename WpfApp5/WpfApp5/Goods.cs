//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp5
{
    using System;
    using System.Collections.Generic;
    
    public partial class Goods
    {
        public int id_goods { get; set; }
        public string article { get; set; }
        public string title { get; set; }
        public string unit { get; set; }
        public int price { get; set; }
        public int max_discount { get; set; }
        public int id_manufacturer { get; set; }
        public int id_supplier { get; set; }
        public int id_category { get; set; }
        public int discount { get; set; }
        public int count { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
