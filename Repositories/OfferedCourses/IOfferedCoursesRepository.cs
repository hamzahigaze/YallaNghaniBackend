using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Repositories.Core;
using OfferedCourseEntity = YallaNghani.Models.OfferedCourse;
namespace YallaNghani.Repositories.OfferedCourses
{
    public interface IOfferedCoursesRepository : IRepository<OfferedCourseEntity.OfferedCourse>
    {
    }
}
