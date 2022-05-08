using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book.Models;
//using Domains;
namespace book.Bl
{
    public interface ICategoryServices{
        List<TbCategory> GetAll();
    }
    public class ClsCategories : ICategoryServices
    {
        StoreWebsiteContext ctx;
        public ClsCategories(StoreWebsiteContext context)
        {
            ctx = context;
        }
        public List<TbCategory> GetAll()
        {
            return ctx.TbCategories.ToList();
        }
    }
}
