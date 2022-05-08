using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book.Models;
using book.Bl;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace book.Controllers
{
    public class ItemsController : Controller
    {
        IItemService ItemService;
        public ItemsController(IItemService itemService)
        {
            ItemService = itemService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            ItemDetailModel model = new ItemDetailModel();
            model.item = ItemService.GetByIdWithImages(id);
            model.listRelatedItems = ItemService.GetRelatedItems(model.item.SalesPrice);
            model.listUpSellItems = ItemService.GetUpSellItems();
            return View(model);
        }
        public IActionResult AddToCart(int id)
        {
            ShopingCart oShopingCart = HttpContext.Session.GetObjectFromJson<ShopingCart>("Cart");
            if (oShopingCart == null)
                oShopingCart = new ShopingCart();
            TbItem items = ItemService.GetById(id);
            ShopingCartItem ShopingItem = oShopingCart.ListItems.Where(a => a.ItemId == id).FirstOrDefault();
            if (ShopingItem != null) 
            {
                ShopingItem.Qty++;
                ShopingItem.Total = ShopingItem.Price * ShopingItem.Qty;
            }
            else
            {
                oShopingCart.ListItems.Add(new ShopingCartItem()
                {
                    ItemId = items.ItemId,
                    ItemName = items.ItemName,
                    ImageName = items.ImageName,
                    Price = items.SalesPrice,
                    Qty = 1,
                    Total = items.SalesPrice
                });
            }
      
            oShopingCart.Total = oShopingCart.ListItems.Sum(a => a.Total);
            HttpContext.Session.SetObjectAsJson("Cart", oShopingCart);
            return Redirect("/Home/Index");
        }

        public IActionResult ItemList()
        {
            return View();
        }
    }
}
