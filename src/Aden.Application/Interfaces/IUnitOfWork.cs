namespace Aden.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ISpecificationRepository Specifications { get; }
    ISubmissionRepository Submissions { get; }
    int Complete();
}