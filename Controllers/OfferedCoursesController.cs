using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Helpers.Results;
using YallaNghani.Services.OfferedCourses;
using OfferedCourseDtos = YallaNghani.DTOs.OfferedCourse;
using OfferedCourseEntity = YallaNghani.Models.OfferedCourse;

namespace YallaNghani.Controllers
{
    [ApiController()]
    [Route("[controller]")]
    public class OfferedCoursesController : Controller
    {
        private IOfferedCoursesService _offeredCoursesService;

        public OfferedCoursesController(IOfferedCoursesService service)
        {
            _offeredCoursesService = service;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return _mapResult(await _offeredCoursesService.Get());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return _mapResult(await _offeredCoursesService.GetById(id));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] OfferedCourseDtos.CreateDto dto)
        {
            return _mapResult(await _offeredCoursesService.Create(dto));
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] OfferedCourseDtos.UpdateDto dto)
        {
            return _mapResult(await _offeredCoursesService.Update(id, dto));
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return _mapResult(await _offeredCoursesService.Delete(id));
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
