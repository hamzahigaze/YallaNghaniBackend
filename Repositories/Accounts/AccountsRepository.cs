using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Repositories.Core;
using AccountEntity = YallaNghani.Models.Account;

namespace YallaNghani.Repositories.Accounts
{
    public class AccountsRepository : MongoRepository<AccountEntity.Account>, IAccountsRepository
    {
        public AccountsRepository(IMongoDatabase database) : base(database)
        {
        }

    }
}
