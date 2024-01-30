using Nest;

namespace CodingChallenge.Application.Common.Extensions
{
    public static class SearchDescriptorExtensions
    {
        public static SearchDescriptor<T> WildcardQuery<T>(this SearchDescriptor<T> descriptor, string query)
            where T : class
        {
            return descriptor.Query(q => q.QueryString(d => d.Query('*' + query + '*')));
        }
    }
}
