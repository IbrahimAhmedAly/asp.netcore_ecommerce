using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book.Models
{
    public class ItemDetailModel
    {
        public TbItem item { get; set; }
        public List<TbItem> listRelatedItems { get; set; }
        public List<TbItem> listUpSellItems { get; set; }
    }
}
