using Business.Abstract;
using Business.Constants;
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
    public class ProductNewManager : IProductNewService
    {
        IProductNewDal _productNewDal;

        public ProductNewManager(IProductNewDal productNewDal)
        {
            _productNewDal = productNewDal;
            
        }
        public IDataResult<List<ProductNew>> GetAll()
        {
            return new SuccessDataResult<List<ProductNew>>(_productNewDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<ProductNew>> GetByUnitPrice()
        {
            return new SuccessDataResult<List<ProductNew>>(_productNewDal.GetAll());
        }

        public IDataResult<List<ProductNewDetailDto>> GetProductDetailDtos()
        {
            return new SuccessDataResult<List<ProductNewDetailDto>>(_productNewDal.GetProductsDetails());
        }
    }
}
