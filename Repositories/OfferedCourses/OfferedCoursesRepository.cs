using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Repositories.Core;
using OfferedCourseEntity = YallaNghani.Models.OfferedCourse;

namespace YallaNghani.Repositories.OfferedCourses
{
    public class OfferedCoursesRepository : MongoRepository<OfferedCourseEntity.OfferedCourse>, IOfferedCoursesRepository
    {
        public OfferedCoursesRepository(IMongoDatabase database) : base(database)
        {
        }

    }
}
