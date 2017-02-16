using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcleUrbanGardens.Domain;
using AcleUrbanGardens.Web.Infrastructure;
using AcleUrbanGardens.Web.Models;

namespace AcleUrbanGardens.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private AcleUrbanGardensDb _db;

        public CategoryController(AcleUrbanGardensDb db)
        {
            _db = db;
        }

        // GET: Category
        public ActionResult Index()
        {
            return View(_db.Categories.ToList().Where(c => c.ParentId == null).OrderBy(c => c.Name));
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            // get the main category we are looking at list of products
            category.Products = _db.Products.Where(p => p.CategoryId == id).ToList();

            // get the first level of sub category
            category.Children = _db.Categories.Where(c => c.ParentId == id).OrderBy(c => c.Name).ToList();
            // if we got some children 
            if( category.Children.Count > 0)
            {
                // get any further sub categories
                category.Children = GetChildren(category.Children, id);
            }


            return View(category);
        }

        private ICollection<Category> GetChildren(ICollection<Category> children, int? id)
        {
            foreach (var subCategory in children)
            {
                subCategory.Products = _db.Products.Where(p => p.CategoryId == subCategory.Id).ToList();
                subCategory.Children = _db.Categories.Where(c => c.ParentId == subCategory.Id).OrderBy(c => c.Name).ToList();
                if (subCategory.Children.Count > 0)
                    subCategory.Children = GetChildren(subCategory.Children, subCategory.Id);
            }

            return children;
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            var model = new CreateCategoryViewModel();
            // get the user role 
            model.CreatedBy = "Admin";
            // need to initialise the date. it will get reset when the category is saved to the database with upto date datetime value
            model.CreateDate = DateTime.UtcNow;
            return View(model);
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,CreatedBy,CreateDate")] CreateCategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category();
                category.Name = categoryViewModel.Name;
                category.Description = categoryViewModel.Description;
                category.CreateDate = categoryViewModel.CreateDate;
                category.CreatedBy = categoryViewModel.CreatedBy;
                
                _db.Categories.Add(category);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            // use this to inspect model state errors
            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            return View(categoryViewModel);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var model = new EditCategoryViewModel();
            model.Id = category.Id;
            model.Name = category.Name;
            model.Description = category.Description;
            model.CreateDate = category.CreateDate;
            model.CreatedBy = category.CreatedBy;
            model.UpdateDate = category.UpdateDate;
            model.UpdatedBy = category.UpdatedBy;

            return View(model);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CreatedBy,CreateDate,UpdatedBy,UpdateDate")] EditCategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category();
                category.Id = categoryViewModel.Id;
                category.Name = categoryViewModel.Name;
                category.Description = categoryViewModel.Description;
                category.CreateDate = categoryViewModel.CreateDate;
                category.CreatedBy = categoryViewModel.CreatedBy;
                category.UpdateDate = DateTime.UtcNow;
                category.UpdatedBy = "Admin"; // add roles code
                _db.Entry(category).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Details", new { Id = category.Id });
            }
            return View(categoryViewModel);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            // if no id was supplied
            if (id == null)
            {
                // return http error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // try to get the category
            Category category = _db.Categories.Find(id);

            // if the category is null
            if (category == null)
            {
                // return 404
                return HttpNotFound();
            }
            
            // if the category is Unassigned-Products
            if(category.Name == Constants.CATEGORY_UNASSIGNED_PRODUCTS)
            {
                return RedirectToAction("ErrorDeleteUnassignedCategory", new { id = category.Id });
            }
            else
            {
                // create the delete category view model
                var viewModel = new DeleteCategoryViewModel();

                // assign the category values to the view model
                viewModel.Id = category.Id;
                viewModel.Name = category.Name;
                viewModel.Description = category.Description;
                viewModel.CreateDate = category.CreateDate;
                viewModel.CreatedBy = category.CreatedBy;
                viewModel.UpdateDate = category.UpdateDate;
                viewModel.UpdatedBy = category.UpdatedBy;
                viewModel.UnassignedCategoryId = _db.Categories.Single(c => c.Name == Constants.CATEGORY_UNASSIGNED_PRODUCTS).Id;
                viewModel.UnassignedCategory = _db.Categories.Single(c => c.Name == Constants.CATEGORY_UNASSIGNED_PRODUCTS).Name;
                viewModel.Products = category.Products;

                // return the model to the view
                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult ErrorDeleteUnassignedCategory(int? id)
        {
            // if no id was supplied
            if (id == null)
            {
                // return http error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // try to get the category
            Category category = _db.Categories.Find(id);

            // if the category is null
            if (category == null)
            {
                // return 404
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // get the category to be deleted from the db
            Category category = _db.Categories.Find(id);
            // get list of products
            var products = _db.Products;

            // for each product in list of products
            foreach (Product product in products)
            {
                // if the product category is the one we are deleting
                if (product.CategoryId == id)
                {
                    // set the products category to the default (Unassigned-Products)
                    product.CategoryId = _db.Categories.Single(c => c.Id == 16).Id;
                    // set the products state to modified so save changes will cause data to be
                    _db.Entry(product).State = EntityState.Modified;
                }
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult CreateSubCategory([Bind(Include = "CategoryId")] int? categoryId)
        {
            if (categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = new CreateSubCategoryViewModel();
            // get the user role 
            viewModel.CreatedBy = "Admin";
            // need to initialise the date. it will get reset when the category is saved to the database with upto date datetime value
            viewModel.CreateDate = DateTime.UtcNow;
            viewModel.ParentId = categoryId;
            viewModel.Parent = _db.Categories.Find(categoryId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubCategory([Bind(Include = "Name,Description,ParentId,CreatedBy,CreateDate")] CreateSubCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var parent = _db.Categories.Find(viewModel.ParentId);
                var category = new Category();
                category.Name = viewModel.Name;
                category.Description = viewModel.Description;
                category.CreateDate = DateTime.UtcNow;
                category.CreatedBy = "Admin";
                category.ParentId = viewModel.ParentId;

                _db.Categories.Add(category);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            // use this to inspect model state errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
