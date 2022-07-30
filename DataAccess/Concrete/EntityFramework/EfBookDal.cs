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
    public class EfBookDal : EfEntityRepositoryBase<Book, AngularContext>, IBookDal
    {
        public IList<BookDto> GetBookRentCountList()
        {
            using (var context = new AngularContext())
            {
                var result = from x in context.Books.ToList()
                             select new BookDto
                             {
                                 Id = x.Id,
                                 Name = x.Name,
                                 Count = context.LeaseTerms.Where(p=> p.BookId == x.Id).Count(),
                                 Guid = x.Guid,
                                 ImageUrl = x.ImageUrl,
                                 IsActive = x.IsActive,
                                 IsAvailable = x.IsAvailable,
                                 PublishDate = x.PublishDate,
                                 Writer = x.Writer
                             };
                return result.ToList();

            }
        }
    }
}
