using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Helpers.Pagination;
using YallaNghani.Helpers.Results;
using AccountDtos = YallaNghani.DTOs.Accounts;

namespace YallaNghani.Services.Account
{
    public interface IAccountsService
    {
        Task<ServiceResult<AccountDtos.ReadDto>> Create(AccountDtos.CreateDto dto);

        Task<ServiceResult<PagedList<AccountDtos.ReadDto>>> Get(PaginationParameters paginationParameters);

        Task<ServiceResult<AccountDtos.ReadDto>> GetById(string id);

        Task<ServiceResult<AccountDtos.ReadDto>> UpdateProfile(string id, AccountDtos.UpdateProfileDto dto);

        Task<ServiceResult> Delete(string id);

        Task<ServiceResult> UpdatePassword(string id, AccountDtos.UpdatePasswordDto dto);

        Task<ServiceResult<string>> UpdateProfileImage(string id, IFormFile file);

        Task<ServiceResult> ResetPassowrd(AccountDtos.ResetPasswordDto dto);
    }
}
