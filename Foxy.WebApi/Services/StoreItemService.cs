using Foxy.DataLayer.Models.Generals;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

public class StoreItemService(IUnitOfWork unitOfWork, IRepository<StoreItem> repository)
{
    public async Task<IEnumerable<StoreItem>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<StoreItem?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<StoreItem> CreateAsync(StoreItem entity)
    {
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(StoreItem entity)
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

