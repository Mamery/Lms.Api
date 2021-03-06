using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Repositories
{
    public interface ICourseRepository
    {

        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetCourse(int? Id);
        Task<bool> SaveAsync();
        Task AddAsync<T>(T added);
        void RemoveAsync(Course course);
        bool Any(int id);
        void Entry(Course course);
    }
}
