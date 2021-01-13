using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.WEB.Models;
using Conit.WEB.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Conit.WEB.Controllers
{
    public class PartsController : Controller
    {
        private readonly IPartService partService;

        private readonly IPartProductService partProductService;

        private FileManager fileManager;

        public PartsController(IPartService partService, IPartProductService partProductService)
        {
            this.partService = partService;
            this.partProductService = partProductService;
            fileManager = new FileManager();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var partDtos = partService.GetAll();
            var partsViewModel =
                Mapper.Map<IEnumerable<PartViewModel>>(partDtos);

            if (User.IsInRole(RoleName.Admin) || User.IsInRole(RoleName.Manager))
                return View("PartsList", partsViewModel);
            else
                return View("PartsReadOnlyList", partsViewModel);
        }

        [AllowAnonymous]
        public ActionResult Details(int partDtoId)
        {
            var partDto = partService.Get(partDtoId);
            var partViewModel = Mapper.Map<PartViewModel>(partDto);

            partViewModel.ProductDtos = partProductService.GetAllProductsWithPart(partDtoId);

            return View("PartDetails", partViewModel);
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult New()
        {
            var partViewModel = new PartViewModel();

            return View("PartForm", partViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Save(PartViewModel partViewModel)
        {
            var partDto = Mapper.Map<PartDto>(partViewModel);

            if (!ModelState.IsValid)
            {
                partViewModel = new PartViewModel();

                return View("PartForm", partViewModel);
            }
            if (partDto.Id == 0)
            {
                var path = fileManager.GeneratePictureName("/Files/Parts/");
                partDto.PictureId = path;

                partService.Add(partDto);

                if (partViewModel.Picture.Upload != null)
                {
                    partViewModel.Picture.Upload
                        .SaveAs(Server.MapPath(path));
                }
            }
            else
            {
                var pictureLink = partDto.PictureId;
                bool delete = true;
                if (pictureLink == null)
                {
                    partDto.PictureId = fileManager.GeneratePictureName("/Files/Parts/");
                    delete = false;
                }

                partService.Edit(partDto);

                if (partViewModel.Picture.Upload != null)
                {
                    if (delete)
                    {
                        var pathToPicture = Server.MapPath(pictureLink);
                        if (fileManager.FileExists(pathToPicture))
                        {
                            fileManager.Delete(pathToPicture);
                        }
                    }
                    partViewModel.Picture.Upload
                        .SaveAs(Server.MapPath(pictureLink));
                }
            }
            return RedirectToAction("Index", "Parts");
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Edit(int partDtoId)
        {
            var partDto = partService.Get(partDtoId);

            var partViewModel = Mapper.Map<PartViewModel>(partDto);

            return View("PartForm", partViewModel);
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Delete(int partDtoId)
        {
            partService.Delete(partDtoId);

            return RedirectToAction("Index", "Parts");
        }
    }
}