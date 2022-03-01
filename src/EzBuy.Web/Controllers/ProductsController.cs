﻿using Microsoft.AspNetCore.Mvc;
using EzBuy.Web.Models;
using System.Diagnostics;
using EzBuy.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using EzBuy.InputModels.AddEdit;
using Microsoft.AspNetCore.Identity;
using EzBuy.Models;

namespace EzBuy.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ICategoryService categoriesService;
        private readonly IProductService productService;
        private readonly UserManager<User> userManager;

        public ILogger<ProductsController> _logger { get; set; }

        public ProductsController(
            ICategoryService categoriesService, 
            IProductService productService,
            UserManager<User> userManager)
        {
            this.categoriesService = categoriesService;
            this.productService = productService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            var categories = this.categoriesService.GetAllCategories();
            this.ViewBag.Categories = categories;
            return this.View(new AddProductInputModel());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddProductInputModel input)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid arguments");
                return this.View(input);
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);
            try
            {
                int newProductId = this.productService.AddProduct(input, currentUser);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return Redirect("/");
        }
        public async Task<IActionResult> Edit()
        {
            return View();
        }
        public async Task<IActionResult> Delete()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}