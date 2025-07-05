
using Foxy.DataLayer.Models.Support;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

    public class SuggestionVoteService
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<SuggestionVote> _repository;

    public SuggestionVoteService(IUnitOfWork unitOfWork, IRepository<SuggestionVote> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<IEnumerable<SuggestionVote>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<SuggestionVote?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<SuggestionVote> CreateAsync(SuggestionVote entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(SuggestionVote entity)
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

