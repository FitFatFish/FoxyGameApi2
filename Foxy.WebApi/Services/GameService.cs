using Foxy.DataLayer.Models.Games;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services
{
    public class GameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Game> _repository;

        public GameService(IUnitOfWork unitOfWork, IRepository<Game> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Game?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Game> CreateAsync(Game entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Game entity)
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
}
