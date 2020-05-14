using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.DAL.Entities;
using Conit.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conit.BLL.Services
{
    public class PartProductService : IPartProductService
    {
        IUnitOfWork Database { get; set; }

        public PartProductService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Add(PartProductDto partProductDto)
        {
            if (partProductDto == null)
            {
                throw new ArgumentNullException("partProductDto");
            }

            var partProduct = Mapper.Map<PartProduct>(partProductDto);

            partProduct.DateOfAdding = DateTime.Now;

            Database.PartProducts.Add(partProduct);
            Database.Save();
        }

        public void Delete(int partId, int productId)
        {
            var partProductInDb = Database.PartProducts
                .SingleOrDefault(p => p.PartId == partId & p.ProductId == productId);
                

            if (partProductInDb == null)
            {
                throw new Exception("No PartProduct with such id in Database");
            }

            Database.PartProducts.Remove(partProductInDb);
            Database.Save();
        }

        public void Edit(PartProductDto partProductDto)
        {
            if (partProductDto == null)
            {
                throw new ArgumentNullException("partProductDto");
            }

            var partProduct =
                Mapper.Map<PartProduct>(partProductDto);

            Database.PartProducts.Update(partProduct);
            Database.Save();
        }

        public PartProductDto Get(int partProductDtoId)
        {
            var partProductInDb =
                Database.PartProducts.Get(partProductDtoId);

            if (partProductInDb == null)
            {
                throw new Exception("No PartProduct with such Id in Database.");
            }

            var partProductDto =
                Mapper.Map<PartProductDto>(partProductInDb);

            return partProductDto;
        }

        public IEnumerable<PartDto> GetAllPartsOfProduct(int productId)
        {
            var partIdsInDb = Database.PartProducts
                .GetAll()
                .Where(p => p.ProductId == productId)
                .Select(p => p.PartId)
                .ToList();

            if (partIdsInDb == null)
            {
                throw new Exception("No records in PartProducts Table.");
            }

            var partsInDb = new List<Part>();

            foreach(var id in partIdsInDb)
            {
                try
                {
                    var part = Database.Parts.Get(id.Value);
                    partsInDb.Add(part);
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            var partDtos =
                Mapper.Map<IEnumerable<PartDto>>(partsInDb);

            return partDtos;
        }

        public IEnumerable<ProductDto> GetAllProductsWithPart(int partId)
        {
            var productIdsInDb = Database.PartProducts
                .GetAll()
                .Where(p => p.PartId == partId)
                .Select(p => p.ProductId)
                .ToList();

            if (productIdsInDb == null)
            {
                throw new Exception("No records in PartProducts Table.");
            }

            var productsInDb = new List<Product>();

            foreach (var id in productIdsInDb)
            {
                try
                {
                    var product = Database.Products.Get(id.Value);
                    productsInDb.Add(product);
                }
                catch
                {
                    throw new Exception("Maybe product was deleted.");
                }
            }

            var productDtos =
                Mapper.Map<IEnumerable<ProductDto>>(productsInDb);

            return productDtos;
        }
    }
}
