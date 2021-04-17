using Lms.Api.Data;
using Lms.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Lms.Core.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LmsApiContext db;

        public CourseRepository(LmsApiContext db)
        {
            this.db = db;
        }

        public async Task AddAsync<T>(T added)
        {
            await db.AddAsync(added);
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await db.Course.ToListAsync();
        }

        public async Task<Course> GetCourse(int? Id)
        {
            return await db.Course
               .FirstOrDefaultAsync(m => m.Id == Id);
        }


        public void RemoveAsync(Course course)
        {
            db.Remove(course);
        }

        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;

           
        }

        public void Entry(Course course)
        {
            db.Entry(course).State = EntityState.Modified;
        }

        public bool Any(int id)
        {
            throw new NotImplementedException();
        }
    }
}
