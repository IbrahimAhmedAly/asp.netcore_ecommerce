using System;
using System.Collections.Generic;

//#nullable disable

namespace  book.Models
{
    public partial class TbCashTransaction
    {
        public int? CashTransactionId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? CashDate { get; set; }
        public decimal? CashValue { get; set; }
    }
}
