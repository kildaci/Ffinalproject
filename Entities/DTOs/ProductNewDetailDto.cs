using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductNewDetailDto:IDto
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }
        public string CategoryNameSecond { get; set; }

        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
