using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using morshop.app.Components;
using morshop.app.Identity;
using morshop.app.Models;
using morshop.business.Abstract;
using morshop.entity;
using System.Collections.Generic;


namespace morshop.app.Controllers
{   
    [Authorize(Roles ="admin")]//Yetkilendirilmiş kullanıcılar bu controller'a erişebilecek
    public class AdminController:Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public AdminController(IProductService productService, ICategoryService categoryService,RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager)
        {
            _productService=productService;
            _categoryService=categoryService;
            _roleManager=roleManager;
            _userManager=userManager;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
                
        
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles.ToList());
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel roleModel)
        {
            if(!ModelState.IsValid)
            {
                return View(roleModel);
            }
            var result = await _roleManager.CreateAsync(new IdentityRole(roleModel.Name));
            if(result.Succeeded)
            {
                return RedirectToAction("RoleList","Admin");
            }

            else
            {
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("",err.Description);
                }
            }
            return View(roleModel);
        }

        public async Task<IActionResult> RoleEdit(string id)
        {
            var roleEditModel = new RoleEditModel();
            var role = await _roleManager.FindByIdAsync(id);
            roleEditModel.Id=role.Id;
            roleEditModel.Name=role.Name;
            return View(roleEditModel);
        }
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel roleEditModel)
        {
            if(!ModelState.IsValid)
            {
                return View(roleEditModel);
            }
            var role = await _roleManager.FindByIdAsync(roleEditModel.Id);
            role.Name=roleEditModel.Name;
            var result = await _roleManager.UpdateAsync(role);
            if(result.Succeeded)
            {
                TempData["Message"]="Başarıyla Güncellendi!";
                return RedirectToAction("RoleList","Admin");
            }
            return View(roleEditModel);
        }
        [HttpPost]
        public async Task<IActionResult> RoleDelete(string Id)
        {
            if(Id==null)
            {
                return View();;
            }
            var role = await _roleManager.FindByIdAsync(Id);
            var result = await _roleManager.DeleteAsync(role);
            return RedirectToAction("RoleList","Admin");;
        }



        ///////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> UserList()
        {   
            return View(_userManager.Users.ToList());
        }
        public async Task<IActionResult> UserRoleEdit(string id)
        {   
            var user = await _userManager.FindByIdAsync(id);
            var roles = _roleManager.Roles;
            TempData["userId"]=user.Id;
            var userRoles=await _userManager.GetRolesAsync(user);
            List<RoleAssignModel> roleAssignList=new List<RoleAssignModel>(){};
            foreach(var role in roles)
            {
                RoleAssignModel m = new RoleAssignModel();
                m.RoleId=role.Id;
                m.RoleName=role.Name;
                m.Exists=userRoles.Contains(role.Name);
                roleAssignList.Add(m);
            }

            return View(roleAssignList);
        }
        [HttpPost]
        public async Task<IActionResult> UserRoleEdit(List<RoleAssignModel> roleAssignList)
        {   
            var userId=TempData["userId"];
            var user=await _userManager.FindByIdAsync(userId.ToString());
            Console.WriteLine(user.UserName);
            foreach(var role in roleAssignList)
            {
                
                if(role.Exists)
                {
                    var result = await _userManager.AddToRoleAsync(user,role.RoleName);
                    foreach(var err in result.Errors)
                    {
                        Console.WriteLine(err);
                    }
                }
                else
                {
                    var result =await _userManager.RemoveFromRoleAsync(user,role.RoleName);
                    foreach(var err in result.Errors)
                    {
                        Console.WriteLine(err);
                    }
                }
            
            }
            return Redirect("/admin/userlist");
        }

        public async Task<IActionResult> UserEdit(string id)
        {   
            if(id!=null)
            {
                var user = await _userManager.FindByIdAsync(id);
                if(user!=null)
                {
                    ViewBag.UserRoles = await _userManager.GetRolesAsync(user);
                    return View("UserEdit",new UserModel{
                        Id=user.Id,
                        FirstName=user.FirstName,
                        LastName=user.LastName,
                        UserName=user.UserName,
                        EmailAddress=user.Email,
                        IsEmailConfirmed=user.EmailConfirmed
                    });
                }
            }
            return RedirectToAction("UserList","Admin");
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserModel userModel)
        {   
            if(userModel.Id!=null)
            {
                var user = await _userManager.FindByIdAsync(userModel.Id);
                if(user!=null)
                {
                    user.FirstName=userModel.FirstName;
                    user.LastName=userModel.LastName;
                    user.UserName=userModel.UserName;
                    user.Email=userModel.EmailAddress;
                    user.EmailConfirmed=userModel.IsEmailConfirmed;
                    var result = await _userManager.UpdateAsync(user);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("UserEdit");
                    }
                }
            }
            return View("UserEdit",userModel);
        }
        //************************************************************************************************************************

        public IActionResult ProductList()
        {
            var adminProductListComponent = new AdminProductListComponent()
            {
                Products = _productService.GetAll(),
                Categories = _categoryService.GetAll()
            };
            return View("ProductList",adminProductListComponent);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories=_categoryService.GetAll();
            return View("AddProduct");
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel productModel, IFormFile file)
        {   
            if(!ModelState.IsValid)
            {   ViewBag.Categories=_categoryService.GetAll();
                return View(productModel);
            }
            else
            {
                Random rnd = new Random();
                var product = new Product
                {
                    Name=productModel.Name,
                    Description=productModel.Description,
                    CurrentPrice=productModel.CurrentPrice,
                    CategoryId=productModel.CategoryId,
                    NumberOfSales=0,
                    IsApproved=false,
                    Url=productModel.Name.ToLower().Trim().Replace(" ","-").
                                                            Replace('ö', 'o').
                                                            Replace('ü', 'u').
                                                            Replace('ğ', 'g').
                                                            Replace('ş', 's').
                                                            Replace('ı', 'i').
                                                            Replace('ç', 'c')+"-"+rnd.Next(0,1000000).ToString()
                };
                if(file!=null)
                {
                    
                    var extention = Path.GetExtension(file.FileName);
                    var randomName=string.Format($"{Guid.NewGuid()}{extention}");
                    product.ImageUrl=randomName; //dosyanın adını alır
                    var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Images",randomName); //dosyanın yolunu belirler
                    using(var stream = new FileStream(path,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    _productService.Create(product);
                    var adminProductListComponent = new AdminProductListComponent()
                    {
                        Products = _productService.GetAll(),
                        Categories = _categoryService.GetAll(),
                        Message = "Ürün başarıyla oluşturuldu."
                    };
                    return View("ProductList",adminProductListComponent);
                }
                else
                {
                    ViewBag.Categories=_categoryService.GetAll();
                    return View(productModel);
                }
            }
        }

        [HttpPost]
        public IActionResult DeleteProduct(int Id)
        {
            var product = _productService.GetProductById(Id);
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Images",product.ImageUrl); //dosyanın yolunu belirler
            System.IO.File.Delete(path);
            _productService.Delete(product);
            var adminProductListComponent = new AdminProductListComponent()
            {
                Products = _productService.GetAll(),
                Categories = _categoryService.GetAll(),
                Message = "Ürün başarıyla silindi."
            };
            return View("ProductList",adminProductListComponent);
        }


        public IActionResult EditProduct(int id)
        {
            var product=_productService.GetProductById(id);
            var productModel=new ProductModel()
            {
                Id=product.Id,
                Name=product.Name,
                Description=product.Description,
                CurrentPrice=product.CurrentPrice,
                ImageUrl=product.ImageUrl,
                CategoryId=product.CategoryId,
                IsApproved=product.IsApproved
            };
            ViewBag.Categories=_categoryService.GetAll();
            return View("EditProduct",productModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel productModel, IFormFile file)
        {
            var product=_productService.GetProductById(productModel.Id);
            if(product.Name==productModel.Name&&
                product.Description==productModel.Description&&
                product.CurrentPrice==productModel.CurrentPrice&&
                product.IsApproved==productModel.IsApproved&&
                product.CategoryId==productModel.CategoryId)
            {
                return Redirect("/admin/productList");
            }

            if(!ModelState.IsValid)
            {
                ViewBag.Categories=_categoryService.GetAll();
                return View("EditProduct",productModel);
            }
            else
            {
                product.Name=productModel.Name;
                product.Description=productModel.Description;
                product.CategoryId=productModel.CategoryId;
                product.IsApproved=productModel.IsApproved;
                product.ImageUrl=productModel.ImageUrl;
                var temp=product.CurrentPrice;
                product.CurrentPrice=productModel.CurrentPrice;
                product.PreviousPrice=temp;
                var adminProductListComponent = new AdminProductListComponent()
                {
                    Products = _productService.GetAll(),
                };
                    
                if(file!=null)
                {
                    var path1 = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Images",product.ImageUrl); //dosyanın yolunu belirler
                    System.IO.File.Delete(path1);

                    var extention = Path.GetExtension(file.FileName);//dosyanın uzantısını alır
                    var randomName=string.Format($"{Guid.NewGuid()}{extention}");
                    product.ImageUrl=randomName; //dosyanın adını alır
                    var path2 = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Images",randomName); //dosyanın yolunu belirler
                    using(var stream = new FileStream(path2,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    _productService.Update(product);
                    adminProductListComponent.Message="Ürün başarıyla güncellendi.";
                    adminProductListComponent.Categories = _categoryService.GetAll();
                    return View("ProductList",adminProductListComponent);
                }
                else
                {
                    if(productModel.ImageUrl!=null)
                    {
                        _productService.Update(product);
                        adminProductListComponent.Message="Ürün başarıyla güncellendi.";
                        adminProductListComponent.Categories = _categoryService.GetAll();
                        return View("ProductList",adminProductListComponent);
                    }
                    else
                    {
                        ViewBag.Categories=_categoryService.GetAll();
                        return View("EditProduct",productModel);
                    }
                    
                }
                
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult CategoryList()
        {
            var adminCategoryListComponent = new AdminCategoryListComponent();
            adminCategoryListComponent.Categories=_categoryService.GetAll();
            return View("CategoryList",adminCategoryListComponent);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        public IActionResult DeleteCategory(int Id) //dönecem
        {
            var adminCategoryListComponent = new AdminCategoryListComponent();
            adminCategoryListComponent.Categories=_categoryService.GetAll();
            
            var products = _productService.GetProductsByCategoryId(Id);
            if(products.Count<=0||products==null)
            {
                var Category=_categoryService.GetCategoryById(Id);
                _categoryService.Delete(Category);
                adminCategoryListComponent.Message="Ürün silindi!";
                return RedirectToAction("CategoryList",adminCategoryListComponent);
            }
            else
            {
                adminCategoryListComponent.Message="Kategoriyi silemezsiniz bu kategoriye bağlı ürünler var!";
                return View("CategoryList",adminCategoryListComponent);
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult AddCategory()
        {
            return View("AddCategory");
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryModel categoryModel)
        {
            var adminCategoryListComponent = new AdminCategoryListComponent();
            if(!ModelState.IsValid)
            {
                return View("AddCategory",categoryModel);
            }
            
            var rnd=new Random();
            var category=new Category();
            category.Name=categoryModel.Name;
            category.Url=categoryModel.Name.ToLower().Trim().Replace(" ","-").
                                                            Replace('ö', 'o').
                                                            Replace('ü', 'u').
                                                            Replace('ğ', 'g').
                                                            Replace('ş', 's').
                                                            Replace('ı', 'i').
                                                            Replace('ç', 'c')+"-"+rnd.Next(0,1000000).ToString();
            _categoryService.Create(category);
            adminCategoryListComponent.Categories=_categoryService.GetAll();
            adminCategoryListComponent.Message="Kategori oluşturuldu.";

            return View("CategoryList",adminCategoryListComponent);
        }
        public IActionResult EditCategory(int Id)
        {
            var category=_categoryService.GetCategoryById(Id);
            var categoryModel=new CategoryModel();
            categoryModel.Name=category.Name;
            categoryModel.Id=category.Id;
            return View("EditCategory",categoryModel);
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel categoryModel)
        {
            if(!ModelState.IsValid)
            {
                return View("EditCategory",categoryModel);
            }
            var category = _categoryService.GetCategoryById(categoryModel.Id);
            category.Name=categoryModel.Name;
            _categoryService.Update(category);
            var adminCategoryListComponent = new AdminCategoryListComponent();
            adminCategoryListComponent.Categories=_categoryService.GetAll();
            adminCategoryListComponent.Message="Kategori Güncellendi";
            return View("CategoryList",adminCategoryListComponent);
        }
    }
}