using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.DTOs.Auth;
using YallaNghani.Helpers.Results;
using YallaNghani.Services.JWTManager;
using AccountEntity = YallaNghani.Models.Account;
using TeacherEntity = YallaNghani.Models.Teacher;
using ParentEntity = YallaNghani.Models.Parent;
using AccountDtos = YallaNghani.DTOs.Accounts;
using ParentDtos = YallaNghani.DTOs.Parents.Parent;
using TeacherDtos = YallaNghani.DTOs.Teachers.Teacher;
using YallaNghani.Repositories.Accounts;
using YallaNghani.Services.Hashing;
using YallaNghani.Repositories.Parent;
using YallaNghani.Repositories.Teacher;
using AutoMapper;

namespace YallaNghani.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IJWTManagerService _jwtManager;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IParentsRepository _parentsRepositroy;
        private readonly ITeachersRepository _teachersRepository;
        private readonly IMapper _mapper;

        public AuthService(IJWTManagerService jWTManagerService, IAccountsRepository accountsRepository,
                           IPasswordHasher passowrdHasher, IParentsRepository parentsRepository,
                           ITeachersRepository teachersRepository, IMapper mapper)
        {
            _jwtManager = jWTManagerService;
            _passwordHasher = passowrdHasher;
            _accountsRepository = accountsRepository;
            _parentsRepositroy = parentsRepository;
            _teachersRepository = teachersRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<object>> Login(LoginDto dto)
        {
            var account = await _accountsRepository.GetAsync(ac => ac.UserName == dto.UserName);

            if (account == null || !_passwordHasher.Check(account.Password, dto.Password))
                return ServiceResult<Object>.BadRequest("Invalid UserName Or Password");

            var token = _jwtManager.GenerateJWT(account);

            if(account.Role == AccountEntity.AccountRole.Parent)
            {
                var parent = await _parentsRepositroy.GetAsync(p => p.AccountId == account.Id);

                parent.Messages.Reverse();
                parent.Courses.Reverse();
                foreach (var course in parent.Courses)
                {
                    course.Lessons.Reverse();
                    course.Payments.Reverse();
                }

                var parentDto = _mapper.Map<ParentDtos.ReadDto>(account);
                parentDto = _mapper.Map<ParentEntity.Parent, ParentDtos.ReadDto>(parent, parentDto);

                return ServiceResult<object>.Success(new { 
                    token = token,
                    parent = parentDto,
                    role = "parent"
                });
            }
            else if(account.Role == AccountEntity.AccountRole.Teacher)
            {
                var teacher = await _teachersRepository.GetAsync(p => p.AccountId == account.Id);

                var teacherDto = _mapper.Map<TeacherDtos.ReadDto>(account);
                teacherDto = _mapper.Map<TeacherEntity.Teacher, TeacherDtos.ReadDto>(teacher, teacherDto);
                return ServiceResult<object>.Success(new
                {
                    token = token,
                    teacher = teacherDto,
                    role = "teacher"
                });
            }
            return ServiceResult<object>.BadRequest("Un Supported Account Type!");
        }

        public async Task<ServiceResult<Object>> AdminLogin(LoginDto dto)
        {
            var account = await _accountsRepository.GetAsync(ac => ac.UserName == dto.UserName);

            if (account == null || !_passwordHasher.Check(account.Password, dto.Password))
                return ServiceResult<Object>.BadRequest("Invalid UserName Or Password");

            if (account.Role != AccountEntity.AccountRole.Admin)
                return ServiceResult<Object>.BadRequest("Only Admins Can Reach This EndPoint");

            var token = _jwtManager.GenerateJWT(account);

            return ServiceResult<Object>.Success(new
            {
                token = token,
                account = account,                
            });
        }
    }
}
