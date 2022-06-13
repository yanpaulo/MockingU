using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;
using System.Web;

namespace MockingU.Data.ValueConverters
{
    public class StringToListValueConverter : ValueConverter<List<string>, string>
    {
        public StringToListValueConverter() : base(e => 
            string.Join(";", e.Select(f => HttpUtility.UrlEncode(f))), 
            e => e.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(f => HttpUtility.UrlDecode(f)).ToList())
        {
        }

    }
}
