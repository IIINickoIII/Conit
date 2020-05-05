using Conit.BLL.Interfaces;
using Conit.WEB.Models;
using System.IO;
using System.Web.Hosting;
using System.Web.Http;

namespace Conit.WEB.Controllers.Api
{
    [Authorize(Roles = RoleName.AdminAndManager)]
    public class InstructionPagesController : ApiController
    {
        private readonly IInstructionPageService instructionPageService;

        public InstructionPagesController(IInstructionPageService serv)
        {
            instructionPageService = serv;
        }

        // GET /api/instructions
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var instructionPages = instructionPageService.GetAll();

            if (instructionPages == null)
                return NotFound();

            return Ok(instructionPages);
        }

        // GET /api/instructions/1
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int instructionId)
        {
            var instructionPageDtos = instructionPageService
                .GetAll(instructionId);

            if (instructionPageDtos == null)
                return NotFound();

            return Ok(instructionPageDtos);
        }

        // DELETE /api/instructions/1
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var instructionPageDto = instructionPageService.Get(id);
            if (instructionPageDto != null)
            {
                try
                {
                    instructionPageService.Delete(id);
                }
                catch
                {
                    return InternalServerError();
                }
                if (instructionPageDto.PictureId != null)
                {
                    var pathToPicture = HostingEnvironment.MapPath(instructionPageDto.PictureId);
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
