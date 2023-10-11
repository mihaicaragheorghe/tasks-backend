using Tasks.Application.Common.Models;

namespace Tasks.Application.Common.Repository;

public interface IRefreshTokenRepository
{
    Task<int> AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    Task<RefreshToken?> GetAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(Guid userId, CancellationToken cancellationToken = default);
}