using Foxy.DataLayer.Models.Suggestions;
using Foxy.DataLayer.Models.Support;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

    public class SuggestionService
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Suggestion> _repository;

    public SuggestionService(IUnitOfWork unitOfWork, IRepository<Suggestion> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<IEnumerable<Suggestion>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Suggestion?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Suggestion> CreateAsync(Suggestion entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Suggestion entity)
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

