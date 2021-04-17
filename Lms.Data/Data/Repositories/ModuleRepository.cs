using Lms.Api.Data;
using Lms.Core.Entities;
using Lms.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly LmsApiContext db;

        public ModuleRepository(LmsApiContext db)
        {
            this.db = db;
        }

        public async Task AddAsync<T>(T added)
        {
            await db.AddAsync(added);
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await db.Module.ToListAsync();
        }

        public async Task<Module> GetModule(int? Id)
        {
            return await db.Module
               .FirstOrDefaultAsync(m => m.Id == Id);
        }


        public void RemoveAsync(Module module)
        {
            db.Remove(module);
        }

        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;


        }

        public void Entry(Module module)
        {
            db.Entry(module).State = EntityState.Modified;
        }

        public bool Any(int id)
        {
            throw new NotImplementedException();
        }

       
    }
}
