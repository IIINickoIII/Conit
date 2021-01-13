using Conit.BLL.Interfaces;
using Conit.WEB.Models;
using System.Web.Http;

namespace Conit.WEB.Controllers.Api
{
    [Authorize(Roles = RoleName.AdminAndManager)]
    public class PartProductController : ApiController
    {
        private readonly IPartProductService partProductService;

        public PartProductController(IPartProductService serv)
        {
            partProductService = serv;
        }

        // GET /api/partproduct?productId=1
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAllPartsByProductId(int productId)
        {
            var parts = partProductService.GetAllPartsOfProduct(productId);

            if (parts == null)
                return NotFound();

            return Ok(parts);
        }

        // GET /api/partproduct?partId=1
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAllProductsByPartId(int partId)
        {
            var products = partProductService.GetAllProductsWithPart(partId);

            if (products == null)
                return NotFound();

            return Ok(products);
        }
    }
}
