using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.WEB.Models;
using Conit.WEB.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Conit.WEB.Controllers
{
    public class InstructionsController : Controller
    {
        private readonly IInstructionService instructionService;

        private readonly IInstructionPageService instructionPageService;

        private readonly IProductService productService;

        public InstructionsController(IInstructionService instructionService, 
            IProductService productService, IInstructionPageService instructionPageService)
        {
            this.instructionService = instructionService;
            this.productService = productService;
            this.instructionPageService = instructionPageService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var instructionDtos = instructionService.GetAll();
            var instructionsViewModel =
                Mapper.Map<IEnumerable<InstructionViewModel>>(instructionDtos);

            if (User.IsInRole(RoleName.Admin) || User.IsInRole(RoleName.Manager))
                return View("InstructionsList", instructionsViewModel);
            else
                return View("InstructionsReadOnlyList", instructionsViewModel);
        }

        [AllowAnonymous]
        public ActionResult Details(int instructionDtoId)
        {
            var instructionDto = instructionService.Get(instructionDtoId);
            var instructionViewModel = Mapper.Map<InstructionViewModel>(instructionDto);

            instructionViewModel.Pages = instructionPageService.GetAll(instructionDtoId);

            return View("InstructionDetails", instructionViewModel);
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult New()
        {
            var instructionViewModel = new InstructionViewModel();

            instructionViewModel.ProductDtos = productService.GetAll();

            return View("InstructionForm", instructionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Save(InstructionViewModel instructionViewModel)
        {
            var instructionDto = Mapper.Map<InstructionDto>(instructionViewModel);

            if (!ModelState.IsValid)
            {
                instructionViewModel = new InstructionViewModel();

                return View("InstructionForm", instructionViewModel);
            }
            if (instructionDto.Id == 0)
            {
                instructionService.Add(instructionDto);
            }
            else
            {
                instructionService.Edit(instructionDto);
            }
            return RedirectToAction("Index", "Instructions");
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Edit(int instructionDtoId)
        {
            var instructionDto = instructionService.Get(instructionDtoId);

            var instructionViewModel = Mapper.Map<InstructionViewModel>(instructionDto);

            instructionViewModel.ProductDtos = productService.GetAll();

            return View("InstructionForm", instructionViewModel);
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Delete(int instructionDtoId)
        {
            instructionService.Delete(instructionDtoId);

            return RedirectToAction("Index", "Instructions");
        }
    }
}