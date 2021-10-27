using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Helpers.Results;
using AuthDtos = YallaNghani.DTOs.Auth;
namespace YallaNghani.Services.Auth
{
    public interface IAuthService 
    {
        Task<ServiceResult<Object>> Login(AuthDtos.LoginDto dto);

        Task<ServiceResult<Object>> AdminLogin(AuthDtos.LoginDto dto);
    }
}
