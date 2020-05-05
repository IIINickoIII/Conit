using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.DAL.Entities;
using Conit.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace Conit.BLL.Services
{
    public class ProductService : IProductService
    {
        IUnitOfWork Database { get; set; }

        public ProductService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Add(ProductDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException("productDto");
            }

            var product = Mapper.Map<Product>(productDto);

            Database.Products.Add(product);
            Database.Save();
        }

        public void Delete(int productDtoId)
        {
            var productInDb = Database.Products.Get(productDtoId);

            if (productInDb == null)
            {
                throw new Exception("No product with such Id in the Database.");
            }

            Database.Products.Remove(productInDb);
            Database.Save();
        }

        public void Edit(ProductDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException("productDto");
            }

            var product = Mapper.Map<Product>(productDto);

            Database.Products.Update(product);
            Database.Save();
        }

        public ProductDto Get(int productDtoId)
        {
            var productInDb = Database.Products.Get(productDtoId);

            if (productInDb == null)
            {
                throw new Exception("No product with such Id in the Database.");
            }

            var productDto = Mapper.Map<ProductDto>(productInDb);

            return productDto;
        }

        public IEnumerable<ProductDto> GetAll()
        {
            var productsInDb = Database.Products.GetAll();

            if (productsInDb == null)
            {
                throw new Exception("No records in Products Table.");
            }

            var productDtos =
                Mapper.Map<IEnumerable<ProductDto>>(productsInDb);

            return productDtos;
        }
    }
}
