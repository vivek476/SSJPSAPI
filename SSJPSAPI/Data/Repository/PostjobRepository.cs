using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Repository
{
    public class PostjobRepository : IPostjob
    {
        private readonly ApplicationDbContext _context;

        public PostjobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Postjob>> GetAllAsync()
        {
            return await _context.Postjobs.ToListAsync();
        }

        public async Task<Postjob> GetByIdAsync(int id)
        {
            return await _context.Postjobs.FindAsync(id);
        }

        public async Task<Postjob> AddAsync(Postjob postjob)
        {
            _context.Postjobs.Add(postjob);
            await _context.SaveChangesAsync();
            return postjob;
        }

        public async Task<bool> UpdateAsync(Postjob postjob)
        {
            if (!_context.Postjobs.Any(p => p.Id == postjob.Id))
                return false;

            _context.Entry(postjob).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var job = await _context.Postjobs.FindAsync(id);
            if (job == null)
                return false;

            _context.Postjobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
