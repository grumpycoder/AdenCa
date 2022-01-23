namespace Aden.Application.Interfaces;

public interface ISpecificationRepository : IRepository<Domain.Entities.Specification>
{
    Task<Domain.Entities.Specification> GetSpecificationWithSubmissions(int id);
    bool GetUniqueFileNumbers(string fileNumber);
    Task<bool> GetUniqueFileNumbersAsync(string fileNumber);
    
    bool GetUniqueFileNumbers(int id, string fileNumber); 
    Task<bool> GetUniqueFileNumbersAsync(int id, string fileNumber);
}