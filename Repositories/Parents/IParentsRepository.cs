using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Repositories.Core;
using ParentEntity = YallaNghani.Models.Parent;


namespace YallaNghani.Repositories.Parent
{
    public interface IParentsRepository : IRepository<ParentEntity.Parent>
    {
    }
}
