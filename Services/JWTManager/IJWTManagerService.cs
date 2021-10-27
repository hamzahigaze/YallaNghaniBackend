using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountEntity = YallaNghani.Models.Account;

namespace YallaNghani.Services.JWTManager
{
    public interface IJWTManagerService
    {
        public string GenerateJWT(AccountEntity.Account account);
    }
}
