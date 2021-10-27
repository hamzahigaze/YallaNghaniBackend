using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Repositories.Core;
using TeacherEntity = YallaNghani.Models.Teacher;

namespace YallaNghani.Repositories.Teacher
{
    public class TeachersRepository : MongoRepository<TeacherEntity.Teacher>, ITeachersRepository
    {
        public TeachersRepository(IMongoDatabase database) : base(database)
        {

        }
    }
}
