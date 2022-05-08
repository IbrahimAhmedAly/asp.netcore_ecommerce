using System;
using System.Collections.Generic;

//#nullable disable

namespace  book.Models
{
    public partial class VwItemsOutOfInvoice
    {
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public decimal? InvoicePrice { get; set; }
        public decimal? PurchasePrice { get; set; }
    }
}
