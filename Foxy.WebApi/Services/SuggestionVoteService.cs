
using Foxy.DataLayer.Models.Support;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

public class SuggestionVoteService(IUnitOfWork unitOfWork, IRepository<SuggestionVote> repository)
{
    public async Task<IEnumerable<SuggestionVote>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<SuggestionVote?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<SuggestionVote> CreateAsync(SuggestionVote entity)
    {
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(SuggestionVote entity)
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

