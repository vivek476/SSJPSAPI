using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Interface
{
    public interface ICompanyjpc
    {
        Task<IEnumerable<Companyjpc>> GetAllAsync();
        Task<Companyjpc> GetByEmployeeIdAsync(string employeeId);
        Task<Companyjpc> AddAsync(Companyjpc company);
        Task<Companyjpc> UpdateAsync(string employeeId, Companyjpc updatedCompany);
    }
}
