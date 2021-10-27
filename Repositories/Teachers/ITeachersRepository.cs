using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Repositories.Core;
using TeacherEntity = YallaNghani.Models.Teacher;


namespace YallaNghani.Repositories.Teacher
{
    public interface ITeachersRepository : IRepository<TeacherEntity.Teacher>
    {
    }
}
