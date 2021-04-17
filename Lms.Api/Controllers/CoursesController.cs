using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Api.Data;
using Lms.Core.Entities;
using Lms.Data.Data.Repositories;
using Lms.Core.Repositories;
using AutoMapper;
using Lms.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;

namespace Lms.Api.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
       // private readonly LmsApiContext _context;

        private readonly IUoW uow;
        private readonly IMapper mapper;

        public CoursesController(IUoW uow, IMapper mapper)
        {
           // _context = context;
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            //await _context.Course.ToListAsync();
            //  return Ok(await uow.CourseRepository.GetAllCourses());
            var courses = await uow.CourseRepository.GetAllCourses();
            var dto = mapper.Map<IEnumerable<CourseDto>>(courses);
            return Ok(dto);

        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            // var course = await _context.Course.FindAsync(id);
            var course = Ok(await uow.CourseRepository.GetCourse(id));

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            uow.CourseRepository.Entry(course);
            
              

            try
            {
                // await _context.SaveChangesAsync();
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            //_context.Course.Add(course);
            await uow.CourseRepository.AddAsync(course);
           // await _context.SaveChangesAsync();
            await uow.CompleteAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await uow.CourseRepository.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }

            uow.CourseRepository.RemoveAsync(course);
            await uow.CompleteAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return uow.CourseRepository.Any(id);
        }


        //Only thing that has been changed will and not the entire json
        [HttpPatch("{courseId}")]
        public async Task<IActionResult> PatchCourse(int courseId,JsonPatchDocument<CourseDto> patchDocument)
        {
            var course = await uow.CourseRepository.GetCourse(courseId);
            if (course == null)
            {
                return NotFound();
            }

            var model = mapper.Map<CourseDto>(course);

            patchDocument.ApplyTo(model, ModelState);

            mapper.Map(model, course);

            if (await uow.CourseRepository.SaveAsync())
            {
                return Ok(mapper.Map<CourseDto>(course));
            }
            else
            {

            }

            return StatusCode(500);
        }





    }
}
