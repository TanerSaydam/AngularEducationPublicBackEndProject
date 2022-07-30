using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string CartNumber { get; set; }
        public string CartOwner { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
    }
}
