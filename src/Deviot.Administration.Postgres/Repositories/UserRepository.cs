using AutoMapper;
using Deviot.Administration.Application.Ports.Repositories;
using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Domain.ValueObjects.Common;
using Deviot.Administration.Domain.ValueObjects.User;
using Deviot.Administration.Postgres.Base;
using Deviot.Administration.Postgres.Builders;
using Deviot.Administration.Postgres.Core;
using Deviot.Administration.Postgres.Views;

namespace Deviot.Administration.Postgres.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly IUserBuilder _userBuilder;

        private const string GENERIC_ERROR = "Houve um problema com o repositório de usuário";

        public UserRepository(
            IDbService dbService,
            IMapper mapper,
            IUserBuilder userBuilder
            ) : base(dbService, mapper)
        {
            _userBuilder = userBuilder;
        }

        public async Task<IEnumerable<User>> FindAllAsync(
            Pagination pagination, 
            string fullname, 
            string email
            )
        {
            try
            {
                var request = _userBuilder.FindAllRequest(
                    fullname, 
                    email,
                    pagination.PageSize.Value, 
                    pagination.PageNumber.Value
                    );

                var usersView = await _dbService.ExecuteQueryRequestAsync<UserView>(request);

                if (usersView is null)
                    return new List<User>();

               return _mapper.Map<IEnumerable<User>>(usersView);
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
        }

        public async Task<User> FindByIdAsync(Id id)
        {
            try
            {
                var request = _userBuilder.FindByIdRequest(id.Value);
                var userView = await _dbService.ExecuteQueryFirstOrDefaultAsync<UserView>(request);

                if(userView is null)
                    return null;

                return _mapper.Map<User>(userView);
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
        }

        public async Task<User> FindByEmailAsync(Email email)
        {
            try
            {
                var request = _userBuilder.FindByEmailRequest(email.Value);
                var userView = await _dbService.ExecuteQueryFirstOrDefaultAsync<UserView>(request);

                if (userView is null)
                    return null;

                return _mapper.Map<User>(userView);
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
        }

        public async Task<long> TotalRegistersAsync()
        {
            try
            {
                var request = _userBuilder.TotalRegistersRequest();
                return await _dbService.ExecuteQueryFirstOrDefaultAsync<int>(request);
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
        }

        public async Task InsertAsync(User user)
        {
            try
            {
                var userView = _mapper.Map<UserView>(user);
                var request = _userBuilder.InsertRequest(userView);

                await _dbService.ExecuteCommandRequestAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                var userView = _mapper.Map<UserView>(user);
                var request = _userBuilder.UpdateRequest(userView);

                await _dbService.ExecuteCommandRequestAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
        }
    }
}
