using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BasketManager : IBasketService
    {
        private readonly IBasketDal _basketDal;
        private readonly IProductService _productService;     

        public BasketManager(IBasketDal basketDal, IProductService productService)
        {
            _productService = productService;
            _basketDal = basketDal;
        }

        [TransactionScopeAspect]
        public IResult Add(Basket basket)
        {
            var product = _productService.Get(basket.ProductId).Data;
            if (product.InventoryQuantity < basket.Quantity)
            {
                return new ErrorResult(Messages.QuantityIsBiggerThanStock);
            }

            var result = _basketDal.GetList(p=> p.ProductId == basket.ProductId).FirstOrDefault();
            if (result != null)
            {
                result.Quantity = basket.Quantity + result.Quantity;
                _basketDal.Update(result);
            }
            else
            {
                _basketDal.Add(basket);
            }            

            product.InventoryQuantity = product.InventoryQuantity - basket.Quantity;
            _productService.Update(product);

            return new SuccessResult(Messages.AddedBasket);
        }

        [TransactionScopeAspect]
        public IResult Delete(Basket basket)
        {
            var product = _productService.Get(basket.ProductId).Data;

            _basketDal.Delete(basket);

            product.InventoryQuantity = product.InventoryQuantity + basket.Quantity;
            _productService.Update(product);

            return new SuccessResult(Messages.DeletedBasket);
        }

        
        public IResult DeleteForOrder(Basket basket)
        {
            _basketDal.Delete(basket);            

            return new SuccessResult();
        }

        public IDataResult<Basket> GetById(int id)
        {
            return new SuccessDataResult<Basket>(_basketDal.Get(p => p.Id == id));
        }

        public Basket GetByProductId(int productId)
        {
            var result = _basketDal.Get(p=> p.ProductId == productId);
            return result;
        }

        public IDataResult<List<BasketDto>> GetListDto()
        {
            return new SuccessDataResult<List<BasketDto>>(_basketDal.GetListDto());
        }

        [TransactionScopeAspect]
        public IResult Update(Basket basket)
        {
            var product = _productService.Get(basket.ProductId).Data;
            var basketRecord = _basketDal.Get(p => p.Id == basket.Id);

            if ((product.InventoryQuantity + basketRecord.Quantity) < basket.Quantity)
            {
                return new ErrorResult(Messages.QuantityIsBiggerThanStock);
            }

            product.InventoryQuantity = product.InventoryQuantity + basketRecord.Quantity - basket.Quantity;
            _productService.Update(product);

            _basketDal.Update(basket);


            return new SuccessResult(Messages.UpdatedBasket);
        }
    }
}
