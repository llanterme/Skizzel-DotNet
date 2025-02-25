//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Skizzel.Infrastructure.Catalog
{
    using System;
    using System.Collections.Generic;
    
    public partial class Receipt
    {
        public Receipt()
        {
            this.Images = new HashSet<Image>();
        }
    
        public int ReceiptId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string DateCreated { get; set; }
        public string Alias { get; set; }
        public string FilterDate { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual User User { get; set; }
    }
}
