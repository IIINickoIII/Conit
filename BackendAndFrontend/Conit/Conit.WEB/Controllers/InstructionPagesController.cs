using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.WEB.Models;
using Conit.WEB.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Conit.WEB.Controllers
{
    public class InstructionPagesController : Controller
    {
        private readonly IInstructionPageService instructionPageService;

        private readonly IPartService partService;

        private readonly IInstructionService instructionService;

        private readonly IProductService productService;

        private FileManager fileManager;

        public InstructionPagesController(IInstructionPageService instructionPageService, IProductService productService,
            IInstructionService instructionService, IPartService partService)
        {
            this.instructionPageService = instructionPageService;
            this.productService = productService;
            this.instructionService = instructionService;
            this.partService = partService;
            fileManager = new FileManager();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var instructionPageDtos = instructionPageService.GetAll();
            var instructionPagesViewModel =
                Mapper.Map<IEnumerable<InstructionPageViewModel>>(instructionPageDtos);

            if (User.IsInRole(RoleName.Admin) || User.IsInRole(RoleName.Manager))
                return View("InstructionPagesList", instructionPagesViewModel);
            else
                return View("InstructionPagesReadOnlyList", instructionPagesViewModel);
        }

        [AllowAnonymous]
        public ActionResult Details(int instructionPageDtoId)
        {
            var instructionPageDto = instructionPageService.Get(instructionPageDtoId);
            var instructionPageViewModel = Mapper.Map<InstructionPageViewModel>(instructionPageDto);

            return View("InstructionPageDetails", instructionPageViewModel);
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult New(int instructionId)
        {
            var instructionPageViewModel = new InstructionPageViewModel();

            instructionPageViewModel.InstructionId = instructionId;
            instructionPageViewModel.PartDtos = partService.GetAll();
            instructionPageViewModel.InstructionDtos = instructionService.GetAll();

            return View("InstructionPageForm", instructionPageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Save(InstructionPageViewModel instructionPageViewModel)
        {
            var instructionId = instructionPageViewModel.InstructionId;
            var instructionPageDto = Mapper.Map<InstructionPageDto>(instructionPageViewModel);

            if (!ModelState.IsValid)
            {
                instructionPageViewModel = new InstructionPageViewModel();

                instructionPageViewModel.InstructionId = instructionId;
                instructionPageViewModel.PartDtos = partService.GetAll();
                instructionPageViewModel.InstructionDtos = instructionService.GetAll();

                return View("InstructionPageForm", instructionPageViewModel);
            }
            if (instructionPageDto.Id == 0)
            {
                var path = fileManager.GeneratePictureName("/Files/Pages/");
                instructionPageDto.PictureId = path;

                instructionPageService.Add(instructionPageDto);

                if (instructionPageViewModel.Picture.Upload != null)
                {
                    instructionPageViewModel.Picture.Upload
                        .SaveAs(Server.MapPath(path));
                }
            }
            else
            {
                var pictureLink = instructionPageDto.PictureId;
                bool delete = true;
                if (pictureLink == null)
                {
                    instructionPageDto.PictureId = fileManager.GeneratePictureName("/Files/Pages/");
                    delete = false;
                }

                instructionPageService.Edit(instructionPageDto);

                if (instructionPageViewModel.Picture.Upload != null)
                {
                    if (delete)
                    {
                        var pathToPicture = Server.MapPath(pictureLink);
                        if (fileManager.FileExists(pathToPicture))
                        {
                            fileManager.Delete(pathToPicture);
                        }
                    }
                    instructionPageViewModel.Picture.Upload
                        .SaveAs(Server.MapPath(pictureLink));
                }
            }
            return RedirectToAction("Details", "Instructions", new { @instructionDtoId = instructionId });
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Edit(int instructionPageDtoId)
        {
            var instructionPageDto = instructionPageService.Get(instructionPageDtoId);

            var instructionPageViewModel = Mapper.Map<InstructionPageViewModel>(instructionPageDto);

            //instructionPageViewModel.InstructionId = instructionPageDtoId;
            instructionPageViewModel.PartDtos = partService.GetAll();
            instructionPageViewModel.InstructionDtos = instructionService.GetAll();

            return View("InstructionPageForm", instructionPageViewModel);
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Delete(int instructionPageDtoId)
        {
            instructionPageService.Delete(instructionPageDtoId);

            return RedirectToAction("Index", "InstructionPages");
        }
    }
}