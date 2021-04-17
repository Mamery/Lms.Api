using Lms.Api.Data;
using Lms.Core.Entities;
using Lms.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data.Repositories
{
   public class UoW : IUoW
    {

        private readonly LmsApiContext db;
        public ICourseRepository CourseRepository { get; private set; }
        public IModuleRepository ModuleRepository { get; private set; }

        public UoW(LmsApiContext db)
        {
            this.db = db;
            CourseRepository = new CourseRepository(db);
            ModuleRepository = new ModuleRepository(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }

        
    }
}
