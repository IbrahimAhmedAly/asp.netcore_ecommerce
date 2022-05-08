using System;
using System.Collections.Generic;

//#nullable disable

namespace  book.Models
{
    public partial class TbCustomer
    {
        public TbCustomer()
        {
            TbSalesInvoices = new HashSet<TbSalesInvoice>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public virtual ICollection<TbSalesInvoice> TbSalesInvoices { get; set; }
    }
}
