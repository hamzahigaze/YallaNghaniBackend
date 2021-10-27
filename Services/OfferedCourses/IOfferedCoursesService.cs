using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Helpers.Results;
using OfferedCourseDtos = YallaNghani.DTOs.OfferedCourse;
using OfferedCourseEntity = YallaNghani.Models.OfferedCourse;

namespace YallaNghani.Services.OfferedCourses
{
    public interface IOfferedCoursesService
    {
        Task<ServiceResult<IList<OfferedCourseDtos.ReadDto>>> Get();

        Task<ServiceResult<OfferedCourseDtos.ReadDto>> GetById(string id);

        Task<ServiceResult<OfferedCourseDtos.ReadDto>> Create(OfferedCourseDtos.CreateDto dto);
        
        Task<ServiceResult<OfferedCourseDtos.ReadDto>> Update(string id, OfferedCourseDtos.UpdateDto dto);
        
        Task<ServiceResult> Delete(string id);

    }
}
