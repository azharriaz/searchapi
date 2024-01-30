using Nest;

using System.Collections.Generic;
using System.Linq;

namespace CodingChallenge.Application.Common.Extensions
{
    /// <summary>
    /// Represents extension class to provide dynamic features for Container Query
    /// </summary>
    public static class QueryContainerExtensions
    {
        public static List<QueryContainer> Wildcard(this List<QueryContainer> queryContainer, string fieldName, string value)
        {
            queryContainer.Add(new WildcardQuery
            {
                Field = fieldName,
                Value = value
            });

            return queryContainer;
        }

        public static List<QueryContainer> Equal(this List<QueryContainer> queryContainer, string fieldName, string value)
        {
            queryContainer.Add(new MatchPhraseQuery
            {
                Field = fieldName,
                Query = value
            });
            return queryContainer;
        }

        public static List<QueryContainer> Lt(this List<QueryContainer> queryContainer, string fieldName, double value)
        {
            queryContainer.Add(new NumericRangeQuery
            {
                LessThan = value,
                Field = fieldName
            });
            return queryContainer;
        }

        public static List<QueryContainer> Gt(this List<QueryContainer> queryContainer, string fieldName, double value)
        {
            queryContainer.Add(new NumericRangeQuery
            {
                GreaterThan = value,
                Field = fieldName
            });
            return queryContainer;
        }

        public static List<QueryContainer> QueryString(this List<QueryContainer> queryContainer, string query, string[] fieldNames)
        {
            queryContainer.Add(new QueryStringQuery
            {
                Fields = fieldNames.Select(x => new Field(x)).ToArray(),
                Query = '*' + query + '*',
            });
            return queryContainer;
        }
    }
}

