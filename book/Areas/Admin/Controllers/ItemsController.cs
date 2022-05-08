using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book.Bl;
using book.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
//using Domains;
namespace book.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ItemsController : Controller
    {
        IItemService itemService;
        ICategoryServices categoryServices;
        public ItemsController(IItemService service,ICategoryServices category)
        {
            itemService = service;
            categoryServices = category;
        }
        public IActionResult List()
        {
            return View(itemService.GetAll());
        }
        public IActionResult Edit(int ? id)
        {
           
            ViewBag.Categories = categoryServices.GetAll();
            if (id != null)
            {
                return View(itemService.GetById(Convert.ToInt32(id)));
            }
            else
                return View();
        }
        public IActionResult Delete(int item)
        {
            itemService.Delete(item);
            return RedirectToAction("List");
        }
        [HttpPost]
        public async Task<IActionResult> Save(TbItem item, List<IFormFile> Files)
        {
            //item.ImageName = string.Empty;
            if (ModelState.IsValid)
            {
                foreach (var file in Files)
                {
                    if (file.Length > 0)
                    {
                        string ImageName = Guid.NewGuid().ToString() + ".jpg";
                        var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                        using (var stream = System.IO.File.Create(filePaths))
                        {
                            await file.CopyToAsync(stream);
                        }
                        item.ImageName = ImageName;
                    }
                }

              
                if (item.ItemId == 0)
                    itemService.Add(item);
                else
                    itemService.Edit(item);
                return RedirectToAction("List");
            }
            else
            {

                ViewBag.Categories = categoryServices.GetAll();
                return View("Edit", item);
            }
        }

    }
 }

