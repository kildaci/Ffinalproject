using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductNewDal : IProductNewDal
    {
        List<ProductNew> _productNews;
        public InMemoryProductNewDal()
        {
            throw new NotImplementedException();

        }
        public void Add(ProductNew entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductNew entity)
        {
            throw new NotImplementedException();
        }

        public ProductNew Get(Expression<Func<ProductNew, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<ProductNew> GetAll(Expression<Func<ProductNew, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<ProductNewDetailDto> GetProductsDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(ProductNew entity)
        {
            throw new NotImplementedException();
        }
    }
}
