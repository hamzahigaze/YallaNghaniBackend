using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherDtos = YallaNghani.DTOs.Teachers.Teacher;
using CourseDtos = YallaNghani.DTOs.Teachers.Course;
using TeacherEntity = YallaNghani.Models.Teacher;
using YallaNghani.Helpers.Results;
using YallaNghani.Repositories.Teacher;
using AutoMapper;
using YallaNghani.Helpers.Pagination;
using YallaNghani.Services.Account;
using YallaNghani.Repositories.Accounts;

namespace YallaNghani.Services.Teachers
{
    public class TeachersService : ITeachersService
    {
        private readonly ITeachersRepository _teachersRepository;
        private readonly IMapper _mapper;
        private readonly IAccountsRepository _accountsRepository;

        public TeachersService(ITeachersRepository teachersRepository, IMapper mapper,
                               IAccountsRepository accountsRepository)
        {
            _teachersRepository = teachersRepository;
            _mapper = mapper;
            _accountsRepository = accountsRepository;
        }

        public async Task<ServiceResult<PagedList<TeacherDtos.ReadDto>>> Get(PaginationParameters paginationParameters)
        {
            var teachers = (await _teachersRepository.GetAsync(e => true, paginationParameters));

            var TeachersDtos = new List<TeacherDtos.ReadDto>();

            foreach(var teacher in teachers.Items)
            {
                var account = await _accountsRepository.GetAsync(a => a.Id == teacher.AccountId);

                var dto = _mapper.Map<TeacherDtos.ReadDto>(account);
                dto = _mapper.Map<TeacherEntity.Teacher, TeacherDtos.ReadDto>(teacher, dto);

                TeachersDtos.Add(dto);
            }

            var pagedDtos = TeachersDtos.ToPagedList<TeacherDtos.ReadDto>(TeachersDtos.Count, paginationParameters.PageIndex, paginationParameters.PageSize);
            return ServiceResult<PagedList<TeacherDtos.ReadDto>>.Success(pagedDtos);
        }

        public async Task<ServiceResult<TeacherDtos.ReadDto>> GetById(string Id)
        {
            var teacher = await _teachersRepository.GetAsync(e => e.Id == Id);

            if (teacher == null)
                return ServiceResult<TeacherDtos.ReadDto>.NotFound("No Teacher Found With The Given Id");

            var account = await _accountsRepository.GetAsync(a => a.Id == teacher.AccountId);

            var dto = _mapper.Map<TeacherDtos.ReadDto>(account);
            dto = _mapper.Map<TeacherEntity.Teacher, TeacherDtos.ReadDto>(teacher, dto);

            return ServiceResult<TeacherDtos.ReadDto>.Success(dto);
        }


    }

    
}
