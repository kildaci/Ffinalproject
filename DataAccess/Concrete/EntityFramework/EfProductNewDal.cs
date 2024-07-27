using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductNewDal : EfEntityRepositoryBase<ProductNew, NorthwindContext>, IProductNewDal
    {
        public List<ProductNewDetailDto> GetProductsDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.ProductNew
                             //join c in context.Categories
                             //on p.CategoryId equals c.CategoryId
                             select new ProductNewDetailDto
                             {
                                 Id = p.Id,
                                 CategoryName = p.CategoryName,
                                 CategoryNameSecond = p.CategoryNameSecond,
                                 ProductName = p.ProductName,  
                                 UnitPrice=p.UnitPrice

                                 
                             };
                return result.ToList();
            }
        }
    }
}
