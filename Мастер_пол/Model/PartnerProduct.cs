//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Мастер_пол.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PartnerProduct
    {
        public int PartnerProductId { get; set; }
        public int ProductId { get; set; }
        public int PartnerId { get; set; }
        public int PartnerProductCount { get; set; }
        public System.DateTime PartnerProductDateSale { get; set; }
    
        public virtual Partner Partner { get; set; }
        public virtual Product Product { get; set; }
    }
}
