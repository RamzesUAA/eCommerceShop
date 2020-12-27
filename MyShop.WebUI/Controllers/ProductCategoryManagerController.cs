using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository<ProductCategory> context;

        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productsCategories = context.Collection().ToList();
            return View(productsCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory productCategoryToEdit = context.Find(id);
            if (productCategoryToEdit != null)
            {
                if (ModelState.IsValid)
                {
                    productCategoryToEdit.Category = productCategory.Category;

                    context.Commit();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(productCategory);
                }

            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Delete(string id)
        {
            ProductCategory producCategoryToDelete = context.Find(id);
            if (producCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(producCategoryToDelete);
            }
        }
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory producCategoryToDelete = context.Find(id);
            if (producCategoryToDelete == null)
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