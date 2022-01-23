namespace Aden.Application.Interfaces;

public interface ISpecificationRepository : IRepository<Domain.Entities.Specification>
{
    Task<Domain.Entities.Specification> GetSpecificationWithSubmissions(int id);
}