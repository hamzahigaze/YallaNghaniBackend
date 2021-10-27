using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.DTOs.Accounts;
using YallaNghani.Helpers.Pagination;
using YallaNghani.Helpers.Results;
using YallaNghani.Repositories.Accounts;
using YallaNghani.Repositories.Parent;
using YallaNghani.Repositories.Teacher;
using YallaNghani.Services.Hashing;
using YallaNghani.Services.Parents;
using YallaNghani.Services.Teachers;
using AccountDtos = YallaNghani.DTOs.Accounts;
using AccountEntity = YallaNghani.Models.Account;
using ParentEntity = YallaNghani.Models.Parent;
using TeacherEntity = YallaNghani.Models.Teacher;
using Microsoft.AspNetCore.Hosting;

namespace YallaNghani.Services.Account
{
    public class AccountsService : IAccountsService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IMapper _mapper;
        private readonly IParentsRepository _parentsRepository;
        private readonly ITeachersRepository _teachersRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AccountsService(IPasswordHasher passwordHasher, IAccountsRepository accountsRepository,
                              IMapper mapper, IParentsRepository ParentsRepository, 
                              ITeachersRepository TeachersRepository, 
                              IWebHostEnvironment hostEnvironment)
        {
            _passwordHasher = passwordHasher;
            _accountsRepository = accountsRepository;
            _mapper = mapper;
            _parentsRepository = ParentsRepository;
            _teachersRepository = TeachersRepository;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<ServiceResult<ReadDto>> Create(AccountDtos.CreateDto dto)
        {
            if (!await _IsUniqueUserName(dto.UserName))
                return ServiceResult<AccountDtos.ReadDto>.BadRequest("User Name Already Exsist");

            var account = _mapper.Map<AccountEntity.Account>(dto);

            account.Password = _passwordHasher.Hash(account.Password);

            await _accountsRepository.AddAsync(account);

            if (account.Role == AccountEntity.AccountRole.Parent)
            {
                var parent = new ParentEntity.Parent { AccountId = account.Id };

                await _parentsRepository.AddAsync(parent);

            }
            else if(account.Role == AccountEntity.AccountRole.Teacher)
            {
                var teacher = new TeacherEntity.Teacher { AccountId = account.Id };

                await _teachersRepository.AddAsync(teacher);
            }


            return ServiceResult<AccountDtos.ReadDto>.Success(_mapper.Map<AccountDtos.ReadDto>(account));
        }

        public async Task<ServiceResult<PagedList<AccountDtos.ReadDto>>> Get(PaginationParameters paginationParameters)
        {
            var accounts = (await _accountsRepository.GetAsync(e => true, paginationParameters));

            var accountsDtos = accounts.Items.Select(Account => _mapper.Map<AccountDtos.ReadDto>(Account));

            var accountsList = accountsDtos.ToPagedList<AccountDtos.ReadDto>(accounts.Items.Count, paginationParameters.PageIndex, paginationParameters.PageSize);

            return ServiceResult<PagedList<AccountDtos.ReadDto>>.Success(accountsList);
        }

        public async Task<ServiceResult<AccountDtos.ReadDto>> GetById(string id)
        {
            var account = await _accountsRepository.GetAsync(e => e.Id == id);

            if (account == null)
                return ServiceResult<AccountDtos.ReadDto>.NotFound("No Account Found With The Given Id");

            return ServiceResult<AccountDtos.ReadDto>.Success(_mapper.Map<AccountDtos.ReadDto>(account));
        }

        public async Task<ServiceResult<AccountDtos.ReadDto>> UpdateProfile(string id, AccountDtos.UpdateProfileDto dto)
        {
            var account = await _accountsRepository.GetAsync(e => e.Id == id);

            if (account == null)
                return ServiceResult<AccountDtos.ReadDto>.NotFound("No Account Found With The Given Id");

            var dtoProperties = dto.GetType().GetProperties();

            dtoProperties.Where(p => p.GetValue(dto) != null)
                         .ToList()
                         .ForEach((property) =>
                         {

                             var propertyName = property.Name;
                             var propertyValue = property.GetValue(dto);
                             account.GetType().GetProperty(propertyName).SetValue(account, propertyValue);
                         });

            await _accountsRepository.UpdateAsync(account);

            return ServiceResult<AccountDtos.ReadDto>.Success(_mapper.Map<AccountDtos.ReadDto>(account));
        }

        public async Task<ServiceResult> Delete(string id)
        {
            var account = await _accountsRepository.GetAsync(a => a.Id == id);

            if (account == null)
                return ServiceResult.NotFound("No account with the given id");


            if (account.Role == AccountEntity.AccountRole.Parent)
            {
                var parent = await _parentsRepository.GetAsync(p => p.AccountId == id);

                if (parent == null)
                    return ServiceResult.NotFound("No parent was found that linked with the account id");

                foreach(var course in parent.Courses)
                {
                    var teacher = await _teachersRepository.GetAsync(t => t.Id == course.TeacherId);

                    if (teacher != null)
                    { 
                        teacher.Courses.RemoveAll(c => c.Id == course.Id);
                        await _teachersRepository.UpdateAsync(teacher);       
                    }
                }
                await _parentsRepository.DeleteAsync(parent);

            }
            else if (account.Role == AccountEntity.AccountRole.Teacher)
            {
                var teacher = await _teachersRepository.GetAsync(t => t.AccountId == id);

                if (teacher == null)
                    return ServiceResult.NotFound("No teacher was found that linked with the account id");

                var parentsList = await _parentsRepository.GetAsync((x) => true, new PaginationParameters());

                foreach(var Parent in parentsList.Items)
                {
                    foreach(var course in Parent.Courses)
                    {
                        if (course.TeacherId == teacher.Id)
                            return ServiceResult.BadRequest("The teacher you are trying to delete is linked with courses for some parents. Please change any appearence of the teacher and try again.");
                    }
                }

                await _teachersRepository.DeleteAsync(teacher);

            }
            else if(account.Role == AccountEntity.AccountRole.Admin)
            {
                var accountsList = await _accountsRepository.GetAsync(a => a.Role == AccountEntity.AccountRole.Admin,
                                                                      new PaginationParameters());

                if (accountsList.Items.Count <= 1)
                    return ServiceResult.BadRequest("Can't delete this admin account because it's only one exist");

            }
            else
            {
                return ServiceResult.BadRequest("Can't delete accounts of this role");
            }

            await _accountsRepository.DeleteAsync(account);

            return ServiceResult.Success();

        }

        public async Task<ServiceResult> UpdatePassword(string id, AccountDtos.UpdatePasswordDto dto)
        {
            var account = await _accountsRepository.GetAsync(e => e.Id == id);

            if(account == null)
                return ServiceResult<AccountDtos.ReadDto>.NotFound("No Account Found With The Given Id");

            if (!_passwordHasher.Check(account.Password, dto.OldPassword))
                return ServiceResult.BadRequest("Incorrect Password");

            account.Password = _passwordHasher.Hash(dto.NewPassword);

            await _accountsRepository.UpdateAsync(account);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ResetPassowrd(AccountDtos.ResetPasswordDto dto)
        {
            var account = await _accountsRepository.GetAsync(e => e.Id == dto.AccountId);

            if (account == null)
                return ServiceResult<AccountDtos.ReadDto>.NotFound("No Account Found With The Given Id");

            account.Password = _passwordHasher.Hash(dto.Password);

            await _accountsRepository.UpdateAsync(account);

            return ServiceResult.Success();
        }

        public async Task<ServiceResult<string>> UpdateProfileImage(string id, IFormFile file)
        {
            if (file.Length > 0)
            {
                var profilesImagesDirectoryPath = Path.Combine(_hostEnvironment.WebRootPath,"images","users","profiles");

                var account = await _accountsRepository.GetAsync(a => a.Id == id);

                if(account.ImageUrl != null && File.Exists(Path.Combine(profilesImagesDirectoryPath, account.ImageUrl)))
                {
                    File.Delete(Path.Combine(profilesImagesDirectoryPath, account.ImageUrl));
                }

                var newFileName = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                                         .Replace("=","").Replace("+","").Replace("/","").Replace("\\","");

                newFileName = $"{newFileName}.jpg";

                var newFilePath = Path.Combine(profilesImagesDirectoryPath, newFileName);

                var stream = File.Create(newFilePath);

                await file.CopyToAsync(stream);

                stream.Close();

                account.ImageUrl = newFileName;

                await _accountsRepository.UpdateAsync(account);

                return ServiceResult<string>.Success(account.ImageUrl);
            }

            return ServiceResult<string>.BadRequest("Error While Proccessing The File");
        }

        private async Task<bool> _IsUniqueUserName(string userName)
        {
            return (await _accountsRepository.GetAsync(e => e.UserName == userName)) == null;
        }

        private async Task<bool> _IsUniquePhoneNumber(string PhoneNumber)
        {
            return (await _accountsRepository.GetAsync(e => e.PhoneNumber == PhoneNumber)) == null;
        }
    }
}
