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
    public interface IProductNewService
    {
        IDataResult<List<ProductNew>> GetAll();
        IDataResult<List<ProductNew>> GetByUnitPrice();
        IDataResult<List<ProductNewDetailDto>> GetProductDetailDtos();
        //IDataResult<ProductNew> GetById(int productId);
        
    }
}
