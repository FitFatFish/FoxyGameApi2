using Foxy.DataLayer.Models.Users;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

    public class UserItemService
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<UserItem> _repository;

    public UserItemService(IUnitOfWork unitOfWork, IRepository<UserItem> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    public async Task<IEnumerable<UserItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<UserItem?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<UserItem> CreateAsync(UserItem entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(UserItem entity)
    {
        _repository.Update(entity);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;
        _repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}

