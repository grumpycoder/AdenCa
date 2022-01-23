namespace Aden.Application.Interfaces;

public interface ISubmissionRepository: IRepository<Domain.Entities.Submission>
{
    Task<Domain.Entities.Submission> GetSubmissionWithSpecification(int id);
}