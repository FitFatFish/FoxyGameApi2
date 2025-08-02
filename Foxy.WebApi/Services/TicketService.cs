using Foxy.DataLayer.Models.Support;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services;

public class TicketService(IUnitOfWork unitOfWork, IRepository<Ticket> repository)
{
    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Ticket?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<Ticket> CreateAsync(Ticket entity)
    {
        await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Ticket entity)
    {
        try
        {
            repository.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }
     
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

