using book.Bl;
using book.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Domains;
namespace book.Controllers
{
    public class HomeController : Controller
    {
        IItemService itemService;
        public HomeController(IItemService item)
        {
            itemService = item;
        }
        public IActionResult Index()
        {
            HomePageModel model = new HomePageModel();
            model.lstAllItems = itemService.GetAll();
            model.lstNewItems = model.lstAllItems.Take(5);
            model.lstBestSaller = model.lstAllItems.Take(7);
            model.lstCategories = 
               model.lstAllItems.GroupBy(a => a.CategoryId).Select(a => a.First()).ToList();
            return View(model);
        }
    }
}
