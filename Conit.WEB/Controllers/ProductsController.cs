using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.WEB.Models;
using Conit.WEB.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Conit.WEB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        private readonly ICompanyService companyService;

        private FileManager fileManager;

        public ProductsController(IProductService productService, ICompanyService companyService)
        {
            this.productService = productService;
            this.companyService = companyService;
            fileManager = new FileManager();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var productDtos = productService.GetAll();
            var partsViewModel =
                Mapper.Map<IEnumerable<ProductViewModel>>(productDtos);

            if (User.IsInRole(RoleName.Admin) || User.IsInRole(RoleName.Manager))
                return View("ProductsList", partsViewModel);
            else
                return View("ProductsReadOnlyList", partsViewModel);
        }

        [AllowAnonymous]
        public ActionResult Details(int productDtoId)
        {
            var productDto = productService.Get(productDtoId);
            var productViewModel = Mapper.Map<ProductViewModel>(productDto);

            return View("ProductDetails", productViewModel);
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult New()
        {
            var productViewModel = new ProductViewModel();

            productViewModel.CompanyDtos = 
                (List<CompanyDto>)companyService.GetAll();

            return View("ProductForm", productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Save(ProductViewModel productViewModel)
        {
            var productDto = Mapper.Map<ProductDto>(productViewModel);

            if (!ModelState.IsValid)
            {
                productViewModel = new ProductViewModel();

                return View("ProductForm", productViewModel);
            }
            if (productDto.Id == 0)
            {
                var path = fileManager.GeneratePictureName("/Files/Products/");
                productDto.PictureId = path;

                productService.Add(productDto);

                if (productViewModel.Picture.Upload != null)
                {
                    productViewModel.Picture.Upload
                        .SaveAs(Server.MapPath(path));
                }
            }
            else
            {
                var pictureLink = productDto.PictureId;
                bool delete = true;
                if (pictureLink == null)
                {
                    productDto.PictureId = fileManager.GeneratePictureName("/Files/Products/");
                    delete = false;
                }

                productService.Edit(productDto);

                if (productViewModel.Picture.Upload != null)
                {
                    if (delete)
                    {
                        var pathToPicture = Server.MapPath(pictureLink);
                        if (fileManager.FileExists(pathToPicture))
                        {
                            fileManager.Delete(pathToPicture);
                        }
                    }
                    productViewModel.Picture.Upload
                        .SaveAs(Server.MapPath(pictureLink));
                }
            }
            return RedirectToAction("Index", "Products");
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Edit(int productDtoId)
        {
            var productDto = productService.Get(productDtoId);

            var productViewModel = Mapper.Map<ProductViewModel>(productDto);

            return View("ProductForm", productViewModel);
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Delete(int productDtoId)
        {
            productService.Delete(productDtoId);

            return RedirectToAction("Index", "Products");
        }
    }
}