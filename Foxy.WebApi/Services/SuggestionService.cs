using Foxy.DataLayer.Models.Support;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

public class SuggestionService(IUnitOfWork unitOfWork, IRepository<Suggestion> repository)
{
    public async Task<IEnumerable<Suggestion>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Suggestion?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<Suggestion> CreateAsync(Suggestion entity)
    {
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Suggestion entity)
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