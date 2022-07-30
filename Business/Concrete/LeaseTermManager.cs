using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LeaseTermManager : ILeaseTermService
    {
        private readonly ILeaseTermDal _leaseTermDal;

        public LeaseTermManager(ILeaseTermDal leaseTermDal)
        {
            _leaseTermDal = leaseTermDal;
        }

        public IResult Add(LeaseTerm leaseTerm)
        {
            _leaseTermDal.Add(leaseTerm);
            return new SuccessResult("Kitap kiralam işlemi başarıyla tamamlandı");
        }

        public IResult Delete(LeaseTerm leaseTerm)
        {
            return new SuccessResult("Kitap kiralam işlemi başarıyla silindi");
        }

        public IDataResult<LeaseTerm> GetById(int id)
        {
            return new SuccessDataResult<LeaseTerm>(_leaseTermDal.Get(p=>p .Id == id));
        }

        public IDataResult<IList<LeaseTerm>> GetList()
        {
            return new SuccessDataResult<IList<LeaseTerm>>(_leaseTermDal.GetList());
        }

        public IDataResult<IList<LeaseTerm>> GetListByBook(int bookId)
        {
            return new SuccessDataResult<IList<LeaseTerm>>(_leaseTermDal.GetList(p=> p.BookId == bookId));
        }

        public IResult Update(LeaseTerm leaseTerm)
        {
            return new SuccessResult("Kitap kiralam işlemi başarıyla güncellendi");
        }
    }
}
