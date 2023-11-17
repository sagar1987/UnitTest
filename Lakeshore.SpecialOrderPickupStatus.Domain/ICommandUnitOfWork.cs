
namespace Lakeshore.SpecialOrderPickupStatus.Domain;

public interface ICommandUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
