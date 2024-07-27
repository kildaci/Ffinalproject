using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserNewService
    {
        IDataResult<List<UserNew>> GetAll();
        void Add(UserNew userNew);
        UserNew GetByMail(string email);
        List<OperationClaim> GetClaims(UserNew userNew);
    }
}
