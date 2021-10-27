using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Repositories.Core;
using AccountEntity = YallaNghani.Models.Account;

namespace YallaNghani.Repositories.Accounts
{
    public interface IAccountsRepository : IRepository<AccountEntity.Account>
    {
    }
}
