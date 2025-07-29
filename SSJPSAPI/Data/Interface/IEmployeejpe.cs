using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Interface
{
    public interface IEmployeejpe
    {
        IEnumerable<Employeejpe> GetAll();
        Task<Employeejpe?> GetByEmployeeIdAsync(int employeeId);
        Task<Employeejpe> CreateAsync(Employeejpe model, IFormFile? imageFile);
        Task<Employeejpe?> UpdateAsync(int employeeId, IFormCollection form, IFormFile? file);
        Task<bool> DeleteAsync(int id);
    }
}
