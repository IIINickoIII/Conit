using Conit.BLL.Interfaces;
using Conit.WEB.Models;
using System.IO;
using System.Web.Hosting;
using System.Web.Http;

namespace Conit.WEB.Controllers.Api
{
    [Authorize(Roles = RoleName.AdminAndManager)]
    public class PartsController : ApiController
    {
        private readonly IPartService partService;

        public PartsController(IPartService serv)
        {
            partService = serv;
        }

        // GET /api/parts
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var parts = partService.GetAll();

            if (parts == null)
                return NotFound();

            return Ok(parts);
        }

        // GET /api/parts/1
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var partDto = partService.Get(id);

            if (partDto == null)
                return NotFound();

            return Ok(partDto);
        }

        // DELETE /api/parts/1
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var partDto = partService.Get(id);
            if (partDto == null)
            {
                try
                {
                    partService.Delete(id);
                }
                catch
                {
                    return InternalServerError();
                }
                if (partDto.PictureId != null)
                {
                    var pathToPicture = HostingEnvironment.MapPath(partDto.PictureId);
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
