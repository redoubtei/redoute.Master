using Redoute.Actualsis.IRepositonry;
using Redoute.Actualsis.IServices.System;
using Redoute.Actualsis.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redoute.Actualsis.Services.Master
{
    public class SysUserService : BasicServices<BaseUser>, ISysUserService
    {
        private IUsersRepository _repository;
        public SysUserService(IUsersRepository repository)
        {
            _repository = repository;
            this.basicRepository = repository;
        }       
    }
}
