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
    public class EfOrderDal : EfEntityRepositoryBase<Order, AngularContext>, IOrderDal
    {
        public List<OrderDto> GetListDto()
        {
            using (var context = new AngularContext())
            {
                var result = from payment in context.Payments
                             select new OrderDto
                             {
                                 Orders = context.Orders.Where(p => p.PaymentId == payment.Id).ToList(),
                                 Payment = payment,
                                 Total = context.Orders.Where(p=> p.PaymentId == payment.Id).Sum(p => p.Quantity * p.Price)
                             };
                return result.ToList();
            }
        }
    }
}
