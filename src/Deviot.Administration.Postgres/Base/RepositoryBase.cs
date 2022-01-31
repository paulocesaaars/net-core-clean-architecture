using AutoMapper;
using Deviot.Administration.Postgres.Core;

namespace Deviot.Administration.Postgres.Base
{
    public abstract class RepositoryBase
    {
        protected readonly IDbService _dbService;

        protected readonly IMapper _mapper;

        protected string Schema => _dbService.Schema;

        public RepositoryBase(
            IDbService dbService, 
            IMapper mapper
            )
        {
            _dbService = dbService;
            _mapper = mapper;
        }
    }
}
