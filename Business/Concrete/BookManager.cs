using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
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
    public class BookManager : IBookService
    {
        private readonly IBookDal _bookDal;
        private readonly ILeaseTermService _leaseTermService;

        public BookManager(IBookDal bookDal, ILeaseTermService leaseTermService)
        {
            _bookDal = bookDal;
            _leaseTermService = leaseTermService;
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(BookValidator))]
        public IResult Add(Book book)
        {
            book.Guid = Guid.NewGuid().ToString();
            _bookDal.Add(book);
            return new SuccessResult("Kitap kaydı başarıyla tamamlandı");
        }

        [SecuredOperation("Admin")]   
        [TransactionScopeAspect]
        public IResult Delete(Book book)
        {
            var results = _leaseTermService.GetListByBook(book.Id).Data;
            foreach (var result in results)
            {
                _leaseTermService.Delete(result);
            }

            _bookDal.Delete(book);
            return new SuccessResult("Kitap kaydı başarıyla silindi");
        }

        public IDataResult<Book> GetByGuid(string guid)
        {
            var result = _bookDal.Get(p=> p.Guid == guid);
            return new SuccessDataResult<Book>(result);
        }

        public IDataResult<IList<Book>> GetList()
        {
            var result = _bookDal.GetList().OrderBy(p=> p.Name).ToList();
            return new SuccessDataResult<IList<Book>>(result);
        }

        [SecuredOperation("Admin")]
        [TransactionScopeAspect]
        public IResult RentABook(LeaseTerm leaseTerm)
        {
            _leaseTermService.Add(leaseTerm);
            var result = _bookDal.Get(p => p.Id == leaseTerm.BookId);
            result.IsAvailable = false;
            _bookDal.Update(result);
            return new SuccessResult("Kitap kiralama işlemi başarıyla tamamlandı");
        }

        [SecuredOperation("Admin")]
        [TransactionScopeAspect]
        public IResult ReturnBook(Book book)
        {
            var findLastLease = _leaseTermService.GetListByBook(book.Id).Data.LastOrDefault();
            findLastLease.RentDate = DateTime.Now;
            _leaseTermService.Update(findLastLease);

            book.IsAvailable = true;
            _bookDal.Update(book);
            return new SuccessResult("Kitap başarıyla geri alındı");
        }

        [SecuredOperation("Admin")]
        [TransactionScopeAspect]
        public IResult DeleteRent(LeaseTerm leaseTerm)
        {
            _leaseTermService.Delete(leaseTerm);

            var findLastLease = _leaseTermService.GetListByBook(leaseTerm.BookId).Data.LastOrDefault();
            if (findLastLease == null || findLastLease.ReturnDate != null)
            {
                var result = _bookDal.Get(p => p.Id == leaseTerm.BookId);
                result.IsAvailable = true;
                _bookDal.Update(result);
            }
            
            return new SuccessResult("Kitap kiralama işlemi başarıyla silindi");
        }



        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(BookValidator))]
        public IResult Update(Book book)
        {
            _bookDal.Update(book);
            return new SuccessResult("Kitap kaydı başarıyla güncellendi");
        }

        public IDataResult<IList<BookDto>> GetBookRentCountList()
        {
            var result = _bookDal.GetBookRentCountList();
            return new SuccessDataResult<IList<BookDto>>(result);
        }
    }
}
