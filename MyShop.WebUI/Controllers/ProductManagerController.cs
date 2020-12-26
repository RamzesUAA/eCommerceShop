using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string id)
        {
            Product productToEdit = context.Find(id);
            if (productToEdit != null)
            {
                if (ModelState.IsValid)
                {
                    context.Update(product);
                    context.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(product);
                }

            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Delete(string id)
        {
            Product producToDelete = context.Find(id);
            if (producToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(producToDelete);
            }
        }
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult ConfirmDelete(string id)
        {
            Product producToDelete = context.Find(id);
            if (producToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}