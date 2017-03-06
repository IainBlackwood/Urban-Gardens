using AcleUrbanGardens.Domain;
using AcleUrbanGardens.Web.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;
using AcleUrbanGardens.Web.Infrastructure;
using System.Data.Entity;

namespace AcleUrbanGardens.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly AcleUrbanGardensDb _db;

        public ProductController(AcleUrbanGardensDb db)
        {
            _db = db;
        }

        // GET: Product
        public ActionResult Index()
        {
            var viewModel = new IndexProductViewModel();
            viewModel.Categories = _db.Categories;
            viewModel.Products = _db.Products.Where(p => p.IsDeleted == false);
            viewModel.CategorySelectList = GetCategorySelectListItems(null);
            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            // if the is is not supplied
            if (id == null)
            {
                // return error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // get the product from the database
            Product product = _db.Products.Find(id);
            // if its null
            if (product == null)
            {
                // return not found error
                return HttpNotFound();
            }
            
            return View(product);
        }        

        // GET: Product/Create
        [HttpGet]
        public ActionResult Create(int? categoryId)
        {
            // create model 
            var viewModel = new CreateProductViewModel();
            
            // TODO: get the user role 
            viewModel.CreatedBy = "Admin";
            viewModel.CreateDate = DateTime.UtcNow;
            // create select list items for the drop down list categories
            viewModel.Categories = GetCategorySelectListItems(categoryId);
            viewModel.CreatedFromCategoryDetail = false;

            // if the categoryId was provided
            if (categoryId != null)
                // set flag to state was created from category page
                viewModel.CreatedFromCategoryDetail = true;
            else
                // get the default category (Unassigned-Products)
                categoryId = _db.Categories.Single(c => c.Name == Constants.CATEGORY_UNASSIGNED_PRODUCTS).Id;

            // set the selected category name
            viewModel.CategoryName = _db.Categories.Find(categoryId).Name;
            // get the selected category for the drop down
            viewModel.SelectedCategory = GetSelectedItem(viewModel.Categories, categoryId);

            // assing the view model values
            viewModel.CategoryId = categoryId;

            return View(viewModel);
        }

        private SelectListItem GetSelectedItem(IEnumerable<SelectListItem> items, int? id)
        {
            SelectListItem selectedCategory = new SelectListItem();
            foreach (SelectListItem item in items)
            {
                if (item.Value == id.ToString())
                {
                    selectedCategory = item;
                    break;
                }
            }

            return selectedCategory;
        }

        private IEnumerable<SelectListItem> GetCategorySelectListItems(int? categoryId)
        {
            IEnumerable<SelectListItem> categories = _db.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

            // if we have a categoryId 
            if(categoryId != null)
            {
                // find the SelectListItem and set its select value to true set all other false
                foreach(SelectListItem item in categories)
                {
                    if (item.Value == categoryId.ToString())
                        item.Selected = true;
                    else
                        item.Selected = false;
                }
            }

            return categories;
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name,LongDescription,ShortDescription,CreatedBy,CreateDate,ImagePath,CategoryId,CreatedFromCategoryDetail")] CreateProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // get the category from the db
                var category = _db.Categories.Find(viewModel.CategoryId);
                // create a new product to insert
                var product = new Product();
                // assign the viewModel values to the new product
                product.Name = viewModel.Name;
                product.LongDescription = viewModel.LongDescription;
                product.ShortDescription = viewModel.ShortDescription;
                product.ImagePath = viewModel.ImagePath;
                product.CreateDate = DateTime.UtcNow;
                product.CreatedBy = "Admin";
                product.CategoryId = category.Id;
                
                // add the product to the Categories list of products
                category.Products.Add(product);

                // save the changes
                _db.SaveChanges();

                // decide which return to action to use
                if(viewModel.CreatedFromCategoryDetail)
                {
                    return RedirectToAction("Details", "Category", new { Id = viewModel.CategoryId });
                }
                else
                {
                    return RedirectToAction("Index", "Product", new { Id = viewModel.CategoryId });
                }
            }
            return View(viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = _db.Products.Find(id);
            product.IsDeleted = true;
            _db.Entry(product).State = EntityState.Modified;
            //_db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var viewModel = new EditProductViewModel();
            viewModel.Id = product.Id;
            viewModel.Name = product.Name;
            viewModel.ShortDescription = product.ShortDescription;
            viewModel.LongDescription = product.LongDescription;
            viewModel.CreateDate = product.CreateDate;
            viewModel.CreatedBy = product.CreatedBy;
            viewModel.UpdateDate = product.UpdateDate;
            viewModel.UpdatedBy = product.UpdatedBy;
            viewModel.ImagePath = product.ImagePath;
            viewModel.CategoryId = product.CategoryId;
            viewModel.FromCategoryDetail = product.FromCategoryDetail;
            // create select list items for the drop down list categories
            viewModel.Categories = GetCategorySelectListItems(viewModel.CategoryId);
            viewModel.SelectedCategory = GetSelectedItem(viewModel.Categories, product.CategoryId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LongDescription,ShortDescription,CreatedBy,CreateDate,ImagePath,CategoryId")] EditProductViewModel viewModel)
        {
            // fist we need to see if the category id is null
            if (viewModel.CategoryId == null)
            {
                // get the unassigned value as default.
                viewModel.CategoryId = Convert.ToInt32(viewModel.Categories.Single(c => c.Text == Constants.CATEGORY_UNASSIGNED_PRODUCTS).Value);
            }
            viewModel.Categories = GetCategorySelectListItems(viewModel.CategoryId);
            // get the selected category
            viewModel.SelectedCategory = GetSelectedItem(viewModel.Categories, viewModel.CategoryId);

            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.Id = viewModel.Id;
                product.Name = viewModel.Name;
                product.ShortDescription = viewModel.ShortDescription;
                product.LongDescription = viewModel.LongDescription;
                product.CreateDate = viewModel.CreateDate;
                product.CreatedBy = viewModel.CreatedBy;
                product.UpdateDate = DateTime.UtcNow;
                product.UpdatedBy = "Admin";
                product.CategoryId = viewModel.CategoryId;
                product.FromCategoryDetail = viewModel.FromCategoryDetail;
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Details", new { Id = product.Id });
            }        

            return View(viewModel);
        }


        [HttpGet]
        public ActionResult DeleteAllProductsByCategory(string categoryId)
        {
            if (categoryId == null)
            {
                // return http error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = new DeleteAllProductsByCategory();
            viewModel.products = _db.Products.Where(p => p.CategoryId.ToString() == categoryId);
            viewModel.CategoryId = Convert.ToInt32(categoryId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAllProductsByCategory([Bind(Include = "CategoryId")] DeleteAllProductsByCategory viewModel)
        {
            var categoryId = Request["CategoryId"];
            // if model state is valid
            if (ModelState.IsValid)
            {
                // for each product in the list of products for deletion?
                // should i actually edit them an set a flag so they are not shown
                foreach (Product product in _db.Products.Where(p => p.CategoryId.ToString() == categoryId))
                {
                    //_db.Products.Remove(product);
                    product.IsDeleted = true;
                    _db.Entry(product).State = EntityState.Modified;
                }

                _db.SaveChanges();

                // todo add code to delete the products
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}