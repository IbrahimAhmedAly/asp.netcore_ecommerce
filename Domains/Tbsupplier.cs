using System;
using System.Collections.Generic;

//#nullable disable

namespace  book.Models
{
    public partial class Tbsupplier
    {
        public Tbsupplier()
        {
            TbPurchaseInvoices = new HashSet<TbPurchaseInvoice>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public virtual ICollection<TbPurchaseInvoice> TbPurchaseInvoices { get; set; }
    }
}
