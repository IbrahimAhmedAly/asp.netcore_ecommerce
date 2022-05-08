using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Json;
using Domains;
namespace book.Bl
{
    public interface IItemService
    {
        List<TbItem> GetAll();
        List<VwItemCategory> GetAllItems();
        List<TbItem> GetRelatedItems(decimal price);
        List<TbItem> GetUpSellItems();
        TbItem GetById(int id);
        TbItem GetByIdWithImages(int id);
        bool Add(TbItem item);
        bool Edit(TbItem item);
        bool Delete(int itemId);
    }
    public class ClsItems : IItemService
    {
        StoreWebsiteContext ctx;
        public ClsItems(StoreWebsiteContext context)
        {
            ctx = context;
        }
 
        public List<TbItem> GetAll()
        {
            List<TbItem> lstItems = ctx.TbItems.Include(a => a.Category).
            OrderByDescending(a => a.CreationDate).ToList();
            return lstItems;
        }
        public List<VwItemCategory> GetAllItems()
        {
            List<VwItemCategory> lstItems = ctx.VwItemCategories.ToList();
            return lstItems;
        }

        public List<TbItem> GetUpSellItems()
        {
            var query = (from items in ctx.TbItems
                         join
                         discount in ctx.TbItemDiscount
                         on items.ItemId equals discount.ItemId
                         where discount.EndDate >= DateTime.Now
                         select items).Include(a => a.Category);
            return query.ToList();
        }
     
        public List<TbItem> GetRelatedItems(decimal price)
        {
           
                decimal start = price - 3000;
                decimal end = price + 3000;
                List<TbItem> lstItems = ctx.TbItems.Include(a => a.Category).Where(a => a.SalesPrice >= start && a.SalesPrice <= end).
                OrderByDescending(a => a.CreationDate).ToList();
                return lstItems;
            

         
        }

        public TbItem GetById(int id)
        {
            TbItem item = ctx.TbItems.FirstOrDefault(a => a.ItemId == id);
            return item;
        }

        public TbItem GetByIdWithImages(int id)
        {
            try
            {
                TbItem item = ctx.TbItems.Include(a => a.TbItemImages).FirstOrDefault(a => a.ItemId == id);
                return item;
            }
            catch (Exception ex)
            {
                return new TbItem();
            }

        }

        public bool Add(TbItem item)
        {
            try
            {
                ctx.Add<TbItem>(item);
                //ctx.TbItems.Add(item);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(TbItem item)
        {
            try
            {
                //TbItems OldItem = ctx.TbItems.Where(a => a.ItemId == item.ItemId).FirstOrDefault();
                //OldItem.CategoryId = item.CategoryId;
                ctx.Entry(item).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int itemId)
        {
            try
            {
                TbItem OldItem = ctx.TbItems.Where(a => a.ItemId == itemId).FirstOrDefault();
                ctx.TbItems.Remove(OldItem);
                //ctx.Entry(item).State = EntityState.Deleted;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
