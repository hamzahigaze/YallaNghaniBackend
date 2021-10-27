using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.DTOs.OfferedCourse;
using YallaNghani.Helpers.Results;
using YallaNghani.Repositories.OfferedCourses;
using OfferedCourseEntity = YallaNghani.Models.OfferedCourse;
using OfferedCourseDtos = YallaNghani.DTOs.OfferedCourse;
using YallaNghani.Helpers.Pagination;

namespace YallaNghani.Services.OfferedCourses
{
    public class OfferedCoursesService : IOfferedCoursesService
    {
        private readonly IMapper _mapper;
        private readonly IOfferedCoursesRepository _offeredCoursesRepository;

        public OfferedCoursesService(IMapper mapper, IOfferedCoursesRepository repository)
        {
            _mapper = mapper;
            _offeredCoursesRepository = repository;
        }

        public async Task<ServiceResult<OfferedCourseDtos.ReadDto>> Create(OfferedCourseDtos.CreateDto dto)
        {
            var offerdCourse = _mapper.Map<OfferedCourseEntity.OfferedCourse>(dto);

            await _offeredCoursesRepository.AddAsync(offerdCourse);

            return ServiceResult<OfferedCourseDtos.ReadDto>.Success(_mapper.Map<OfferedCourseDtos.ReadDto>(offerdCourse));
        }

        public async Task<ServiceResult> Delete(string id)
        {
            var offerdCourse = await _offeredCoursesRepository.GetAsync(o => o.Id == id);

            if (offerdCourse == null)
                return ServiceResult.NotFound("No offered course was found with the given id");


            await _offeredCoursesRepository.DeleteAsync(offerdCourse);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult<IList<OfferedCourseDtos.ReadDto>>> Get()
        {
            var offeredCourses = await _offeredCoursesRepository.GetAsync(o => true, new PaginationParameters());

            var readDtos = offeredCourses.Items.Select(o => _mapper.Map<OfferedCourseDtos.ReadDto>(o)).ToList();

            return ServiceResult<IList<OfferedCourseDtos.ReadDto>>.Success(readDtos);
        }

        public async Task<ServiceResult<OfferedCourseDtos.ReadDto>> GetById(string id)
        {
            var offeredCourse = await _offeredCoursesRepository.GetAsync(o => o.Id == id);

            if (offeredCourse == null)
                return ServiceResult<OfferedCourseDtos.ReadDto>.NotFound("No offered course was found with the given id");

            return ServiceResult<OfferedCourseDtos.ReadDto>.Success(_mapper.Map<OfferedCourseDtos.ReadDto>(offeredCourse));
        }

        public async Task<ServiceResult<OfferedCourseDtos.ReadDto>> Update(string id, UpdateDto dto)
        {
            var offeredCourse = await _offeredCoursesRepository.GetAsync(o => o.Id == id);

            if (offeredCourse == null)
                return ServiceResult<OfferedCourseDtos.ReadDto>.NotFound("No offered course was found with the given id");

            var dtoProperties = dto.GetType().GetProperties().ToList();

            dtoProperties.ForEach((property) =>
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(dto);

                if (propertyValue != null)
                    offeredCourse.GetType().GetProperty(propertyName).SetValue(offeredCourse, propertyValue);
            });

            await _offeredCoursesRepository.UpdateAsync(offeredCourse);

            return ServiceResult<OfferedCourseDtos.ReadDto>.Success(_mapper.Map<OfferedCourseDtos.ReadDto>(offeredCourse));

        }
    }
}
