using Foxy.DataLayer.Models.Games;

using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

    public class MatchMemberService
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<MatchMember> _repository;

    public MatchMemberService(IUnitOfWork unitOfWork, IRepository<MatchMember> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<IEnumerable<MatchMember>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<MatchMember?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<MatchMember> CreateAsync(MatchMember entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(MatchMember entity)
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

