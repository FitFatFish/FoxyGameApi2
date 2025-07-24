using Foxy.DataLayer.Models.Games;
using Foxy.WebApi.Repositories;
namespace Foxy.WebApi.Services;
public class MatchMemberService(IUnitOfWork unitOfWork, IRepository<MatchMember> repository)
{
    public async Task<IEnumerable<MatchMember>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<MatchMember?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<MatchMember> CreateAsync(MatchMember entity)
    {
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(MatchMember entity)
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