
namespace Application.Parameters
{
    public class BaseRequestParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public BaseRequestParameters()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        public BaseRequestParameters(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize ;
        }
    }
}