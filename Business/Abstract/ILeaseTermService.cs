using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILeaseTermService
    {
        IResult Add(LeaseTerm leaseTerm);
        IResult Update(LeaseTerm leaseTerm);
        IResult Delete(LeaseTerm leaseTerm);
        IDataResult<LeaseTerm> GetById(int id);
        IDataResult<IList<LeaseTerm>> GetListByBook(int bookId);
        IDataResult<IList<LeaseTerm>> GetList();
    }
}
