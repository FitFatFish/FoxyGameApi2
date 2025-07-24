using Foxy.DataLayer.Models.Games;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

public class MatchService(IUnitOfWork unitOfWork, IRepository<Match> repository)
{
    public async Task<IEnumerable<Match>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Match?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<Match> CreateAsync(Match entity)
    {
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Match entity)
    {
        repository.Update(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;
        repository.Remove(entity);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}

