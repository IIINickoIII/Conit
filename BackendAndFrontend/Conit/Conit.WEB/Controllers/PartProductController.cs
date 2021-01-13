using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.WEB.Models;
using Conit.WEB.ViewModels;
using System.Web.Mvc;

namespace Conit.WEB.Controllers
{
    public class PartProductController : Controller
    {
        private readonly IPartProductService partProductService;

        private readonly IPartService partService;

        private readonly IProductService productService;

        public PartProductController(IPartProductService partProductService, IPartService partService, IProductService productService)
        {
            this.partProductService = partProductService;
            this.partService = partService;
            this.productService = productService;
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult New(int productId)
        {
            var partProductViewModel = new PartProductViewModel();

            partProductViewModel.ProductId = productId;
            partProductViewModel.ProductDto = productService.Get(productId);
            partProductViewModel.PartDtos = partService.GetAll();
            partProductViewModel.ProductDtos = productService.GetAll();

            return View("PartProductForm", partProductViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Save(PartProductViewModel partProductViewModel)
        {
            var partProductId = partProductViewModel.Id;
            var partProductDto = Mapper.Map<PartProductDto>(partProductViewModel);

            if (!ModelState.IsValid)
            {
                partProductViewModel = new PartProductViewModel();
                partProductViewModel.PartDtos = partService.GetAll();
                partProductViewModel.ProductDtos = productService.GetAll();

                return View("PartProductForm", partProductViewModel);
            }
            if (partProductDto.Id == 0)
            {
                partProductService.Add(partProductDto);
            }
            else
            {
                partProductService.Edit(partProductDto);
            }
            return RedirectToAction("Details", "Products", new { @productDtoId = partProductViewModel.ProductId });
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Delete(int partId, int productId)
        {
            partProductService.Delete(partId, productId);

            return RedirectToAction("Details", "Products", new { @productDtoId = productId });
        }
    }
}