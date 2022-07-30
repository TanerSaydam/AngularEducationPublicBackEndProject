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
    public interface IBasketService
    {
        IResult Add(Basket basket);
        IResult Update(Basket basket);
        IResult Delete(Basket basket);
        IResult DeleteForOrder(Basket basket);
        IDataResult<List<BasketDto>> GetListDto();
        IDataResult<Basket> GetById(int id);
        Basket GetByProductId(int productId);
    }
}
