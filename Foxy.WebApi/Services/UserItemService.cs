using Foxy.DataLayer.Models.Users;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

public class UserItemService(IUnitOfWork unitOfWork, IRepository<UserItem> repository)
{
    public async Task<IEnumerable<UserItem>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<UserItem?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<UserItem> CreateAsync(UserItem entity)
    {
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(UserItem entity)
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