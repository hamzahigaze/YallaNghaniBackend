using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Helpers.Pagination;
using YallaNghani.Helpers.Results;
using YallaNghani.Services.Teachers;
using TeacherDtos = YallaNghani.DTOs.Teachers.Teacher;
using CourseDtos = YallaNghani.DTOs.Teachers.Course;
using Microsoft.AspNetCore.Authorization;

namespace YallaNghani.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : Controller
    {
        private readonly ITeachersService _teachersService;

        public TeachersController(ITeachersService teachersService)
        {
            _teachersService = teachersService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParemeters)
        {
            return _mapResult(await _teachersService.Get(paginationParemeters));
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _teachersService.GetById(id);

            return _mapResult(result);
        }

        private IActionResult _mapResult(ServiceResult result)
        {
            if (result.StatusCode == 200)
                return Ok(result);

            else if (result.StatusCode == 400)
                return BadRequest(result);

            else if (result.StatusCode == 404)
                return NotFound(result);

            else if (result.StatusCode == 403)
                return Forbid();

            return BadRequest(result);
        }
    }
}
