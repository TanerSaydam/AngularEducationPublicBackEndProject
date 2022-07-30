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
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IPaymentService _paymentService;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public OrderManager(IOrderDal orderDal, IPaymentService paymentService, IProductService productService, IBasketService basketService)
        {
            _orderDal = orderDal;
            _paymentService = paymentService;
            _productService = productService;
            _basketService = basketService;
        }

        [TransactionScopeAspect]
        public IResult Add(OrderAddDto orderDto)
        {
            orderDto.Payment.Date = DateTime.Now.ToString();
            var result = _paymentService.Add(orderDto.Payment);
            foreach (var order in orderDto.Baskets)
            {
                //var product = _productService.Get(order.ProductId);
                Order orderModel = new Order()
                {
                    Id = 0,
                    PaymentId = result.Data.Id,
                    Price = order.Product.Price,
                    ProductId = order.ProductId,
                    ProductName = order.Product.Name,
                    Quantity = order.Quantity
                };
                
                _orderDal.Add(orderModel);

                var basket = _basketService.GetById(order.Id).Data;

                _basketService.DeleteForOrder(basket);
            }
            
            return new SuccessResult(Messages.OrderAdded);
        }

        public IResult Delete(Order order)
        {
            _orderDal.Delete(order);
            return new SuccessResult(Messages.OrderDeleted);
        }

        public IDataResult<Order> Get(int id)
        {
            return new SuccessDataResult<Order>(_orderDal.Get(x => x.Id == id));
        }

        public IDataResult<List<OrderDto>> GetListDto()
        {
            return new SuccessDataResult<List<OrderDto>>(_orderDal.GetListDto());
        }

        public IResult Update(Order order)
        {
            _orderDal.Update(order);
            return new SuccessResult(Messages.OrderUpdated);
        }
    }
}
