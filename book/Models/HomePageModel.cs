using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book.Models;

//using Domains;
namespace book.Models
{
    public class HomePageModel
    {
        public IEnumerable<TbSlider> lstSliderImages { get; set; }
        public IEnumerable<TbItem> lstNewItems { get; set; }
        public IEnumerable<TbItem> lstAllItems { get; set; }
        public IEnumerable<TbItem> lstCategories { get; set; }
        public IEnumerable<TbItem> lstBestSaller { get; set; }
    }
}
