using Foxy.DataLayer.Models.Games;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

public class GameService(IUnitOfWork unitOfWork, IRepository<Game> repository)
{
    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Game?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<Game> CreateAsync(Game entity)
    {
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Game entity)
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

