using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Helpers.Pagination;
using YallaNghani.Helpers.Results;
using TeacherDtos = YallaNghani.DTOs.Teachers.Teacher;
using CourseDtos = YallaNghani.DTOs.Teachers.Course;

namespace YallaNghani.Services.Teachers
{
    public interface ITeachersService
    {
        public Task<ServiceResult<PagedList<TeacherDtos.ReadDto>>> Get(PaginationParameters paginationParameters);

        public Task<ServiceResult<TeacherDtos.ReadDto>> GetById(string id);

    }
}
