using Foxy.DataLayer.Models.Games;

using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

    public class MatchService
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Match> _repository;

    public MatchService(IUnitOfWork unitOfWork, IRepository<Match> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    public async Task<IEnumerable<Match>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Match?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Match> CreateAsync(Match entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Match entity)
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

