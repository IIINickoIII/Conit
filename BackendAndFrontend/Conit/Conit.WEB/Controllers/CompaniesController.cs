using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.BLL.Models;
using Conit.WEB.Models;
using Conit.WEB.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Conit.WEB.Controllers
{
    [Authorize(Roles = RoleName.AdminAndManager)]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService companyService;

        private FileManager fileManager;

        public CompaniesController(ICompanyService companyService)
        {
            this.companyService = companyService;
            fileManager = new FileManager();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var companyDtos = companyService.GetAll();
            var companiesViewModel =
                Mapper.Map<IEnumerable<CompanyViewModel>>(companyDtos);

            if (User.IsInRole(RoleName.Admin) || User.IsInRole(RoleName.Manager))
                return View("CompaniesList", companiesViewModel);
            else
                return View("CompaniesReadOnlyList", companiesViewModel);
        }


        [AllowAnonymous]
        public ActionResult Details(int companyDtoId)
        {
            var companyDto = companyService.Get(companyDtoId);
            var companyViewModel = Mapper.Map<CompanyViewModel>(companyDto);

            return View("CompanyDetails", companyViewModel);
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult New()
        {
            var companyViewModel = new CompanyViewModel();

            return View("CompanyForm", companyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminAndManager)]
        public async Task<ActionResult> Save(CompanyViewModel companyViewModel)
        {
            var companyDto = Mapper.Map<CompanyDto>(companyViewModel);

            companyDto.AspNetUserId = User.Identity.Name;

            if (!ModelState.IsValid)
            {
                companyViewModel = new CompanyViewModel();
                return View("CompanyForm", companyViewModel);
            }
            if (companyDto.Id == 0)
            {
                var path = fileManager.GeneratePictureName("/Files/Companies/");
                companyDto.PictureId = path;

                OperationDetails operationDetails = await companyService.AddAsync(companyDto);

                if (operationDetails.Succedeed)
                {
                    if (companyViewModel.Picture.Upload != null)
                    {
                        companyViewModel.Picture.Upload
                            .SaveAs(Server.MapPath(path));
                    }
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    return View("CompanyForm", companyViewModel);
                }
            }
            else
            {
                var pictureLink = companyDto.PictureId;
                bool delete = true;
                if (pictureLink == null)
                {
                    companyDto.PictureId = fileManager.GeneratePictureName("/Files/Companies/");
                    delete = false;
                }

                OperationDetails operationDetails = await companyService.EditAsync(companyDto);

                if (operationDetails.Succedeed)
                {
                    if (companyViewModel.Picture.Upload != null)
                    {
                        if (delete)
                        {
                            var pathToPicture = Server.MapPath(pictureLink);
                            if (fileManager.FileExists(pathToPicture))
                            {
                                fileManager.Delete(pathToPicture);
                            }
                        }
                        companyViewModel.Picture.Upload
                                .SaveAs(Server.MapPath(pictureLink));
                    }
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    return View("CompanyForm", companyViewModel);

                }
            }
            return RedirectToAction("Index", "Companies");
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Edit(int companyDtoId)
        {
            var companyDto = companyService.Get(companyDtoId);

            var companyViewModel = Mapper.Map<CompanyViewModel>(companyDto);

            return View("CompanyForm", companyViewModel);
        }

        [Authorize(Roles = RoleName.AdminAndManager)]
        public ActionResult Delete(int companyDtoId)
        {
            companyService.Delete(companyDtoId);

            return RedirectToAction("Index", "Companies");
        }
    }
}