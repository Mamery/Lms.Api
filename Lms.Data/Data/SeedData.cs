using Bogus;
using Lms.Api.Data;
using Lms.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data
{
    public class SeedData
    {
        public static async Task InitAsync(IServiceProvider services)
        {   //Part 2 regarding seeding
            using (var context = new LmsApiContext
                                 (services.GetRequiredService<DbContextOptions<LmsApiContext>>()))
            {
                if (await context.Course.AnyAsync()) return;

                var fake = new Faker("sv");
                var courses = new List<Course>();
                var modules = new List<Module>();
                for (int i = 0; i < 20; i++)
                {   
                   
                    var course = new Course
                    {

                        Title = fake.Company.CatchPhrase(),

                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),

                    };

                    courses.Add(course);
                }
                await context.AddRangeAsync(courses);

                foreach(Course course in courses)
                {
                    var module = new Module
                    {
                        Course = course,

                        Title = fake.Company.CatchPhrase(),

                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2))
                    };
                    modules.Add(module);

                }

              
                await context.AddRangeAsync(modules);

                await context.SaveChangesAsync();

            }   
        }
    }
}
