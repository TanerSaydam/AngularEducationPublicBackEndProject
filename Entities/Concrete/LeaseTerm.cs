using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class LeaseTerm : IEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string LandmanName { get; set; }
        public string LandmanTel { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
