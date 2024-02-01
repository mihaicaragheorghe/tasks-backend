using Microsoft.EntityFrameworkCore;

using Application.Common.Models;
using Application.Common.Repository;

namespace Tasks.Infrastructure.Persistence.Repository;

public class RefreshTokenRepository(DataContext context) : IRefreshTokenRepository
{
    public async Task<int> AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<RefreshToken?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.RefreshTokens.FindAsync([userId], cancellationToken);
    }

    public async Task<RefreshToken?> GetAsync(string token, CancellationToken cancellationToken = default)
    {
        return await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token.Equals(token), cancellationToken);
    }

    public async Task<int> UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        context.RefreshTokens.Update(refreshToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        context.RefreshTokens.Remove(refreshToken);
        return await context.SaveChangesAsync(cancellationToken);
    }
}