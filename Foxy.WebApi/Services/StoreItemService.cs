using Foxy.DataLayer.Models.Generals;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

    public class StoreItemService
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<StoreItem> _repository;

    public StoreItemService(IUnitOfWork unitOfWork, IRepository<StoreItem> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<IEnumerable<StoreItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<StoreItem?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<StoreItem> CreateAsync(StoreItem entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(StoreItem entity)
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

