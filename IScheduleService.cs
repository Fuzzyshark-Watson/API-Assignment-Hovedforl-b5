using ToDoApi.DataTransferObjects;

namespace ToDoApi.Services
{
    public interface IScheduleService
    {
        Task<EducationClassCreateDTO> CreateEducationClassAsync(EducationClassCreateDTO educationClassCreateDTO);
        Task<EducationClassCreateDTO> GetEducationClassByIdAsync(long id);
        Task<IEnumerable<EducationClassCreateDTO>> GetAllEducationClassesAsync();
        Task UpdateEducationClassAsync(long id, EducationClassCreateDTO educationClassCreateDTO);
        Task DeleteEducationClassAsync(long id);
    }
}
