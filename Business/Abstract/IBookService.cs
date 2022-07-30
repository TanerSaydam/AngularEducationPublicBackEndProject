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
    public interface IBookService
    {
        IResult Add(Book book);
        IResult RentABook(LeaseTerm leaseTerm);
        IResult DeleteRent(LeaseTerm leaseTerm);
        IResult ReturnBook(Book book);
        IResult Update(Book book);
        IResult Delete(Book book);
        IDataResult<Book> GetByGuid(string guid);
        IDataResult<IList<Book>> GetList();
        IDataResult<IList<BookDto>> GetBookRentCountList();
    }
}
