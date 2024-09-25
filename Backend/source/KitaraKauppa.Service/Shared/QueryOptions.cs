using KitaraKauppa.Service.Shared;
using System.ComponentModel.DataAnnotations;

namespace KitaraKauppa.Service.Shared_Dtos
{
    public abstract class QueryOptions<TOrderByEnum> where TOrderByEnum : Enum
    {
        public string? Search { get; set; }
        public int PageNo { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public TOrderByEnum OrderWith { get; set; } 
        public OrderBy OrderBy { get; set; } = OrderBy.ASC;

        public KKResult<string> Validate()
        {
            if(this.PageNo < 0) 
                return new KKResult<string>().Fail("Least possible value for the page number should be 0");

            if(this.PageSize < 1)
                return new KKResult<string>().Fail("Least possible value for the page size should be 1");

            return new KKResult<string>().SucceededWithValue(string.Empty);
        }
    }
}
