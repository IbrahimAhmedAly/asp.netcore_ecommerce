using book.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public class TbItemDiscount
    {
        [Key]
        public int ItemDiscountId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public decimal DiscountPercent { get; set; }
        [Required] 
        public DateTime EndDate { get; set; }
        public virtual TbItem Item { get; set; }

    }
}
