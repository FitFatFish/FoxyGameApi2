using Foxy.DataLayer.Models.Games;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

public class GameCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<GameCategory> _repository;

    public GameCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.Repository<GameCategory>();
    }

    public async Task<IEnumerable<GameCategory>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<GameCategory?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<GameCategory> CreateAsync(GameCategory entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(GameCategory entity)
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