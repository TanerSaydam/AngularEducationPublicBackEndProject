using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDal : EfEntityRepositoryBase<Basket, AngularContext>, IBasketDal
    {
        public List<BasketDto> GetListDto()
        {
            using (var context = new AngularContext())
            {
                var result = from basket in context.Baskets
                             join product in context.Products on basket.ProductId equals product.Id
                             select new BasketDto
                             {
                                 Id = basket.Id,
                                 ProductId = basket.ProductId,
                                 Product = product,
                                 Quantity = basket.Quantity
                             };
                return result.OrderBy(p => p.Product.Name).ToList();
            }
        }
    }
}
