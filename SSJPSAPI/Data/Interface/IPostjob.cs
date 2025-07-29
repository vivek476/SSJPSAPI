using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Interface
{
    public interface IPostjob
    {
        Task<IEnumerable<Postjob>> GetAllAsync();
        Task<Postjob> GetByIdAsync(int id);
        Task<Postjob> AddAsync(Postjob postjob);
        Task<bool> UpdateAsync(Postjob postjob);
        Task<bool> DeleteAsync(int id);
    }
}
