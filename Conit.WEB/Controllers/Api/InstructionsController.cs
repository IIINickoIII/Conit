using Conit.BLL.Interfaces;
using Conit.WEB.Models;
using System.Web.Http;

namespace Conit.WEB.Controllers.Api
{
    [Authorize(Roles = RoleName.AdminAndManager)]
    public class InstructionsController : ApiController
    {
        private readonly IInstructionService instructionService;

        public InstructionsController(IInstructionService serv)
        {
            instructionService = serv;
        }

        // GET /api/instructions
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var instructions = instructionService.GetAll();

            if (instructions == null)
                return NotFound();

            return Ok(instructions);
        }

        // GET /api/instructions/1
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var instructionDto = instructionService.Get(id);

            if (instructionDto == null)
                return NotFound();

            return Ok(instructionDto);
        }

        // DELETE /api/instructions/1
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var instructionDto = instructionService.Get(id);
            if (instructionDto == null)
            {
                try
                {
                    instructionService.Delete(id);
                }
                catch
                {
                    return InternalServerError();
                }
                return Ok("Item was deleted");
            }
            return NotFound();
        }
    }
}
