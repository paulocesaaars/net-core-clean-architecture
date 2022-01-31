using Deviot.Administration.Domain.ValueObjects.Pagination;

namespace Deviot.Administration.Domain.Entities
{
    public class Pagination
    {
        public PageNumber PageNumber { get; protected set; }

        public PageSize PageSize { get; protected set; }

        public Pagination(int pageNumber = 0, int pageSize = 1000)
        {
            PageNumber = new PageNumber(pageNumber);

            PageSize = new PageSize(pageSize);
        }
    }
}
