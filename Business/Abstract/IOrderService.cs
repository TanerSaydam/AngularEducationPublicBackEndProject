using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<OrderDto>> GetListDto();
        IDataResult<Order> Get(int id);
        IResult Add(OrderAddDto order);
        IResult Update(Order order);
        IResult Delete(Order order);
    }
}
