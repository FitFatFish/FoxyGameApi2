using Foxy.DataLayer.Models.Games;
using Foxy.DataLayer.Models.Users;
using Foxy.WebApi.Repositories;

namespace Foxy.WebApi.Services
{
    public class UserProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserProfile> _repository;

        public UserProfileService(IUnitOfWork unitOfWork, IRepository<UserProfile> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<UserProfile?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<UserProfile> CreateAsync(UserProfile entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }
    }
}
