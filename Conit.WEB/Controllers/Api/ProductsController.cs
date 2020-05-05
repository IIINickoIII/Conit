using Conit.BLL.Interfaces;
using Conit.WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;

namespace Conit.WEB.Controllers.Api
{
    [Authorize(Roles = RoleName.AdminAndManager)]
    public class ProductsController : ApiController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService serv)
        {
            productService = serv;
        }

        // GET /api/products
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var products = productService.GetAll();

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        // GET /api/products/1
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var productDto = productService.Get(id);

            if (productDto == null)
                return NotFound();

            return Ok(productDto);
        }

        // DELETE /api/products/1
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var productDto = productService.Get(id);
            if (productDto == null)
            {
                try
                {
                    productService.Delete(id);
                }
                catch
                {
                    return InternalServerError();
                }
                if (productDto.PictureId != null)
                {
                    var pathToPicture = HostingEnvironment.MapPath(productDto.PictureId);
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
