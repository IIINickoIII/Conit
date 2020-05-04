using Conit.BLL.Interfaces;
using Conit.WEB.Models;
using System.IO;
using System.Web.Hosting;
using System.Web.Http;

namespace Conit.WEB.Controllers.Api
{
    [Authorize(Roles = RoleName.AdminAndManager)]
    public class CompaniesController : ApiController
    {
        private readonly ICompanyService companyService;

        public CompaniesController(ICompanyService serv)
        {
            companyService = serv;
        }
        
        // GET /api/companies
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var companies = companyService.GetAll();

            if (companies == null)
                return NotFound();

            return Ok(companies);
        }

        // GET /api/companies/1
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var companyDto = companyService.Get(id);

            if (companyDto == null)
                return NotFound();

            return Ok(companyDto);
        }

        // DELETE api/companies/1
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var companyDto = companyService.Get(id);
            if (companyDto != null)
            {
                try
                {
                    companyService.Delete(id);
                }
                catch
                {
                    return InternalServerError();
                }
                if (companyDto.PictureId != null)
                {
                    var pathToPicture = HostingEnvironment.MapPath(companyDto.PictureId);
                    if (File.Exists(pathToPicture))
                    {
                        File.Delete(pathToPicture);
                    }
                }
                return Ok("Item was deleted");
            }
            return NotFound();
        }
    }
}