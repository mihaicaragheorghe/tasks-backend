using Tasks.Application.Common.Models;
using Tasks.Application.Common.Repository;

namespace Tasks.Infrastructure.Persistence.Repository;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DataContext _context;

    public RefreshTokenRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<RefreshToken?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens.FindAsync(new object[] { userId }, cancellationToken);
    }

    public async Task<int> UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        _context.RefreshTokens.Update(refreshToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        _context.RefreshTokens.Remove(refreshToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}