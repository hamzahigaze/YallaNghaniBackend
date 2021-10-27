using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Repositories.Core;
using ParentEntity = YallaNghani.Models.Parent;

namespace YallaNghani.Repositories.Parent
{
    public class ParentsRepository : MongoRepository<ParentEntity.Parent>, IParentsRepository
    {
        public ParentsRepository(IMongoDatabase database) : base(database)
        {

        }
    }
}
