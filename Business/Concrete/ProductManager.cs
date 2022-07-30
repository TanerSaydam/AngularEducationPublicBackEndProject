using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;
using FluentValidation;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;
using System.Threading;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run
                (                
                CheckIfProductNameExists(product.Name)                
                );

            if (result != null)
            {
                return result;
            }

            product.CodeGuid = Guid.NewGuid().ToString();

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        [SecuredOperation("Admin")]
        [CacheAspect]
        public IDataResult<Product> GetById(string guid)
        {
            
            return new SuccessDataResult<Product>(_productDal.Get(p => p.CodeGuid == guid));
        }        

        public IDataResult<Product> Get(int id)
        {

            return new SuccessDataResult<Product>(_productDal.Get(p => p.Id == id));
        }

        //[SecuredOperation("Admin")]
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<IList<Product>> GetAll()
        {            
            return new SuccessDataResult<IList<Product>>(_productDal.GetList().ToList());
        }


        //[SecuredOperation("Admin")]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }        

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetList(p => p.Name == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<IList<ProductDetailDto>> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public IDataResult<IList<Product>> GetListByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IResult TranscaptionalOperation(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
