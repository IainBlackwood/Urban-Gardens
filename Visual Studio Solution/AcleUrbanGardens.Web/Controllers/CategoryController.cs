using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AcleUrbanGardens.Domain;
using AcleUrbanGardens.Web.Infrastructure;
using AcleUrbanGardens.Web.Models;
using Microsoft.AspNet.Identity;
using System.Web;
using System.IO;

namespace AcleUrbanGardens.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private AcleUrbanGardensDb _db;
        private ApplicationDbContext _applicationDb;

        public CategoryController(AcleUrbanGardensDb db, ApplicationDbContext applicationDb)
        {
            _db = db;
            _applicationDb = applicationDb;
        }

        // GET: Category
        public ActionResult Index(int? numRows, bool? showHistoricalData)
        {
            if (numRows == null)
            {
                // set default of X per page (we can choose something suitable 0 gets all) 
                numRows = 10;
            }

            if (showHistoricalData == null)
            {
                // set default of X per page (we can choose something suitable 0 gets all) 
                showHistoricalData = Models.Constants.SHOW_HISTORICAL_DATA;
            }

            var viewModel = new IndexCategoryViewModel();
            viewModel.Users = _applicationDb.Users.ToList();
            viewModel.Categories = _db.Categories.ToList().Where(c => c.ParentId == null).OrderBy(c => c.Name);
            viewModel.RowsPerPage = Convert.ToInt32(numRows);
            viewModel.RowOptions = Models.Constants.ROW_OPTIONS;
            viewModel.ShowHistoricalData = showHistoricalData;
            
            
            return View(viewModel);
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id, int? subCatNumRows, int? prodNumRows)
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

            // create the viewModel
            var viewModel = new DetailsCategoryViewModel();
            // set the viewModels Category
            viewModel.Category = category;

            // if we are a child category (sub-category)
            if(category.ParentId != null)
            {
                viewModel.Parent = _db.Categories.Find(category.ParentId);
            }
        
            if(category.CreatedBy != null)
                viewModel.CreatedByUsername = _applicationDb.Users.Find(category.CreatedBy).UserName;

            if (category.UpdatedBy != null)
                viewModel.UpdatedByUsername = _applicationDb.Users.Find(category.UpdatedBy).UserName;
            else
                viewModel.UpdatedByUsername = string.Empty;

            // ensure we have a value
            if (subCatNumRows == null)
            {
                // set default of X per page (we can choose something suitable 0 gets all) 
                subCatNumRows = 5;
            }

            // ensure we have a value
            if (prodNumRows == null)
            {
                // set default of X per page (we can choose something suitable 0 gets all) 
                prodNumRows = 5;
            }

            viewModel.SubCategoryRowsPerCategory = Convert.ToInt32(subCatNumRows);
            viewModel.ProductRowsPerCategory = Convert.ToInt32(prodNumRows);
            viewModel.RowOptions = Models.Constants.SMALL_GRID_ROW_OPTIONS;

            return View(viewModel);
        }

        private ICollection<Category> GetChildren(ICollection<Category> children, int? id)
        {
            //TODO: Adjust so that we dont get the childrens products anymore
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
            var viewModel = new CreateCategoryViewModel();
            // get the user role 
            viewModel.CreatedBy = User.Identity.GetUserId();

            // need to initialise the date. it will get reset when the category is saved to the database with upto date datetime value
            viewModel.CreateDate = DateTime.UtcNow;
            return View(viewModel);
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,ImagePath,IsDeleted,CreatedBy,CreateDate,ImageIsInserted")] CreateCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category();
                category.Name = viewModel.Name;
                category.Description = viewModel.Description;
                category.ImagePath = viewModel.ImagePath;
                category.IsDeleted = viewModel.IsDeleted;
                category.CreateDate = viewModel.CreateDate;
                category.CreatedBy = viewModel.CreatedBy;

                // if the image was inserted
                if (viewModel.ImageIsInserted)
                {
                    // if we fail to process the new file
                    if (!ProcessFile(category.ImagePath, viewModel.ImagePath))
                        return View(viewModel);
                }

                _db.Categories.Add(category);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            // use this to inspect model state errors
            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            return View(viewModel);
        }

        public ActionResult InsertImage(string name, string description, bool IsDeleted, HttpPostedFileBase file)
        {
            var viewModel = new CreateCategoryViewModel();
            // get the user role 
            viewModel.CreatedBy = User.Identity.GetUserId();

            // need to initialise the date. it will get reset when the category is saved to the database with upto date datetime value
            viewModel.CreateDate = DateTime.UtcNow;
            viewModel.Name = name;
            viewModel.Description = description;
            viewModel.IsDeleted = IsDeleted;

            if (file != null && file.ContentLength > 0)
                try
                {
                    if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(file.FileName))))
                    {
                        ViewBag.Message = "The file already exists on the sever. Please choose another file";
                        return View("Create", viewModel);
                    }

                    // clear the temp folder of any old files left here
                    clearFolder(Server.MapPath("~/Content/Images/Temp"));
                    file.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/Temp"), Path.GetFileName(file.FileName)));
                    viewModel.ImageIsInserted = true;
                    viewModel.ImagePath = file.FileName;
                    ViewBag.Message = "File uploaded successfully. You must save the record to complete this action";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return View("Create", viewModel);
        }

        [HttpPost]
        public ActionResult UpdateImage(int? id, HttpPostedFileBase file)
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

            var viewModel = new EditCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsDeleted = category.IsDeleted,
                CreateDate = category.CreateDate,
                CreatedBy = category.CreatedBy,
                UpdateDate = category.UpdateDate,
                UpdatedBy = category.UpdatedBy,
                ImagePath = category.ImagePath
        };

            if (file != null && file.ContentLength > 0)
                try
                {
                    // if the filename is the same as the entity name
                    if (string.Equals(file.FileName.Split('.')[0].ToLower(), category.Name.ToLower()))
                    {
                        // clear the temp folder of any old files left here
                        clearFolder(Server.MapPath("~/Content/Images/Temp"));
                        file.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/Temp"), Path.GetFileName(file.FileName)));
                        viewModel.ImageIsUpdated = true;
                        viewModel.ImagePath = file.FileName;
                        ViewBag.Message = "File uploaded successfully. You must save the record to complete this action";
                    }
                    else
                    {
                        ViewBag.Message = "The filename must be the same as the category name.";
                        return View("Edit", viewModel);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return View("Edit", viewModel);
        }

        private void clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                clearFolder(di.FullName);
                di.Delete();
            }
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
            var model = new EditCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImagePath = category.ImagePath,
                IsDeleted = category.IsDeleted,
                CreateDate = category.CreateDate,
                CreatedBy = category.CreatedBy,
                UpdateDate = category.UpdateDate,
                UpdatedBy = category.UpdatedBy
            };

            return View(model);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ImagePath,IsDeleted,CreatedBy,CreateDate,UpdatedBy,UpdateDate,ImageIsUpdated")] EditCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // get the category to be updated
                var category = _db.Categories.Find(viewModel.Id);

                // if the image was updated
                if (viewModel.ImageIsUpdated)
                {
                    // if we fail to process the new file
                    if (!ProcessFile(category.ImagePath, viewModel.ImagePath))
                        return View(viewModel);
                }

                category.Id = viewModel.Id;
                category.Name = viewModel.Name;
                category.ImagePath = viewModel.ImagePath;
                category.IsDeleted = viewModel.IsDeleted;
                category.Description = viewModel.Description;
                category.CreateDate = viewModel.CreateDate;
                category.CreatedBy = viewModel.CreatedBy;
                category.UpdateDate = DateTime.UtcNow;
                category.UpdatedBy = User.Identity.GetUserId();
                _db.Entry(category).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Details", new { Id = category.Id });
            }
            return View(viewModel);
        }

        private bool ProcessFile(string oldImagePath, string newImagePath)
        {
            try
            {
                // if we have a old value 
                if(!string.IsNullOrEmpty(oldImagePath))
                {
                    // if the file exists
                    if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(oldImagePath))))
                    {
                        // delete it
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(oldImagePath)));
                    }
                }

                // move the file from temp to Images
                System.IO.File.Move(Path.Combine(Server.MapPath("~/Content/Images/Temp"), Path.GetFileName(newImagePath)),
                        Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(newImagePath)));

                // clear the temp folder of any old files left here
                clearFolder(Server.MapPath("~/Content/Images/Temp"));
                return true;
            }
            catch (IOException ioe)
            {
                ViewBag.Message = ioe.Message;
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
            }

            return false;
        }

        // GET: Category/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id, int? subCatNumRows, int? prodNumRows)
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
            if(category.Name == Models.Constants.CATEGORY_UNASSIGNED_PRODUCTS)
            {
                return RedirectToAction("ErrorDeleteUnassignedCategory", new { id = category.Id });
            }
            else
            {
                // return the model to the view
                return View(GetDeleteCategoryViewModel(id, subCatNumRows, prodNumRows, category));
            }
        }

        private DeleteCategoryViewModel GetDeleteCategoryViewModel(int? id, int? subCatNumRows, int? prodNumRows, Category category)
        {
            DeleteCategoryViewModel viewModel = InitializeDeleteCategoryViewModel(category);
            viewModel = GetDeleteCategoryDataFromDB(id, category, ref viewModel);
            viewModel = GetGridRowData(subCatNumRows, prodNumRows, ref viewModel);

            return viewModel;
        }

        private DeleteCategoryViewModel InitializeDeleteCategoryViewModel(Category category)
        {
            // create the delete category view model
            var viewModel = new DeleteCategoryViewModel
            {
                // create the category 
                Category = new Category
                {
                    // assign the category values to the view model
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ImagePath = category.ImagePath,
                    Products = category.Products,
                    CreateDate = category.CreateDate,
                    CreatedBy = category.CreatedBy,
                    UpdateDate = category.UpdateDate,
                    UpdatedBy = category.UpdatedBy,
                    IsDeleted = category.IsDeleted
                },
                // set some values to handle unassigned products
                UnassignedCategoryId = _db.Categories.Single(c => c.Name == Models.Constants.CATEGORY_UNASSIGNED_PRODUCTS).Id,
                UnassignedCategory = _db.Categories.Single(c => c.Name == Models.Constants.CATEGORY_UNASSIGNED_PRODUCTS).Name
            };
            return viewModel;
        }

        private DeleteCategoryViewModel GetDeleteCategoryDataFromDB(int? id, Category category, ref DeleteCategoryViewModel viewModel)
        {
            // if we are a child category (sub-category)
            if (category.ParentId != null)
            {
                viewModel.Parent = _db.Categories.Find(category.ParentId);
            }

            // get the main category we are looking at's list of products
            viewModel.Products = _db.Products.Where(p => p.CategoryId == id).ToList();

            // get any sub categories
            viewModel.Category.Children = _db.Categories.Where(c => c.ParentId == id).OrderBy(c => c.Name).ToList();
            // if we got some children 
            if (viewModel.Category.Children.Count > 0)
            {
                // get any further sub categories
                viewModel.Category.Children = GetChildren(viewModel.Category.Children, id);
            }

            // set the created by username / email value
            if (category.CreatedBy != null)
                viewModel.CreatedByUsername = _applicationDb.Users.Find(category.CreatedBy).UserName;

            // set the updated by username / email value
            if (category.UpdatedBy != null)
                viewModel.UpdatedByUsername = _applicationDb.Users.Find(category.UpdatedBy).UserName;
            else
                viewModel.UpdatedByUsername = string.Empty;

            return viewModel;
        }       

        private static DeleteCategoryViewModel GetGridRowData(int? subCatNumRows, int? prodNumRows, ref DeleteCategoryViewModel viewModel)
        {
            // ensure we have a value
            if (subCatNumRows == null)
            {
                // set default of X per page (we can choose something suitable 0 gets all) 
                subCatNumRows = 5;
            }

            // ensure we have a value
            if (prodNumRows == null)
            {
                // set default of X per page (we can choose something suitable 0 gets all) 
                prodNumRows = 5;
            }

            viewModel.SubCategoryRowsPerCategory = Convert.ToInt32(subCatNumRows);
            viewModel.ProductRowsPerCategory = Convert.ToInt32(prodNumRows);
            viewModel.RowOptions = Models.Constants.SMALL_GRID_ROW_OPTIONS;

            return viewModel;
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

            var viewModel = new DeleteCategoryViewModel
            {
                Category = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ImagePath = category.ImagePath,
                    CreatedBy = category.CreatedBy,
                    CreateDate = category.CreateDate,
                    UpdatedBy = category.UpdatedBy,
                    UpdateDate = category.UpdateDate,
                    IsDeleted = category.IsDeleted,
                    Products = category.Products
                }
            };

            if (category.CreatedBy != null)
                viewModel.CreatedByUsername = _applicationDb.Users.Find(category.CreatedBy).UserName;

            if (category.UpdatedBy != null)
                viewModel.UpdatedByUsername = _applicationDb.Users.Find(category.UpdatedBy).UserName;
            else
                viewModel.UpdatedByUsername = string.Empty;

            return View(viewModel);
        }


        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // get the category to be deleted from the db
            Category category = _db.Categories.Find(id);

            // if the category is null
            if (category == null)
            {
                // return 404
                return HttpNotFound();
            }

            // get list of productsthat are linked to the category
            var products = _db.Products.Where(p => p.CategoryId == id);
            int unassignedCategoryId = _db.Categories.Single(c => c.Name == Models.Constants.CATEGORY_UNASSIGNED_PRODUCTS).Id;

            // for each product in list of products
            foreach (Product product in products)
            {
                // set the products category to the default (Unassigned-Products)
                product.CategoryId = unassignedCategoryId;
                // set the products state to modified so save changes will cause data to be
                _db.Entry(product).State = EntityState.Modified;
            }

            // now find the sub-categories { we only need to get 1 level down and keep the structure }
            category.Children = _db.Categories.Where(c => c.Id == category.Id).ToList();
            // for each category child
            foreach(Category childCategory in category.Children)
            {
                childCategory.ParentId = unassignedCategoryId;
                _db.Entry(childCategory).State = EntityState.Modified;
            }

            if (!DeleteImage(category.ImagePath))
            {
                // TODO: Look at getting subCatNumRows and prodNumRows
                return View(GetDeleteCategoryViewModel(id, 0, 0, category));
            }

            // finally delete the category (Set the IsDeleted Flag)
            //_db.Categories.Remove(category);
            category.ImagePath = System.IO.Path.Combine("deleted", category.ImagePath);
            category.IsDeleted = true;
            _db.Entry(category).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool DeleteImage(string imagePath)
        {
            try
            {
                // if we have a value 
                if (!string.IsNullOrEmpty(imagePath))
                {
                    // if the file exists
                    if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(imagePath))))
                    {
                        // "delete it"
                        // move the file from Images to deleted
                        System.IO.File.Move(Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(imagePath)),
                                Path.Combine(Server.MapPath("~/Content/Images/deleted"), Path.GetFileName(imagePath)));
                    }
                }

                return true;
            }
            catch (IOException ioe)
            {
                ViewBag.Message = ioe.Message;
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }

            return false;
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
            viewModel.CreatedBy = User.Identity.GetUserId();
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
                category.CreatedBy = User.Identity.GetUserId();
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
