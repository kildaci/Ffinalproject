using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserNewManager : IUserNewService
    {
        IUserNewDal _userNewDal;

        public UserNewManager(IUserNewDal userNewDal)
        {
            _userNewDal = userNewDal;
        }
        public void Add(UserNew userNew)
        {
            _userNewDal.Add(userNew);

        }

        public IDataResult<List<UserNew>> GetAll()
        {
            return new SuccessDataResult<List<UserNew>>(_userNewDal.GetAll());
         }

        public UserNew GetByMail(string email)
        {
            return _userNewDal.Get(u => u.Email == email);
        }

        public List<OperationClaim> GetClaims(UserNew userNew)
        {
            return _userNewDal.GetClaims(userNew);

        }
    }
}
