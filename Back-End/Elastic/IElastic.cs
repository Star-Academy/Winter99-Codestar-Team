using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Back_End.Elastic
{
    public interface IElastic
    {
        public ElasticClient Client { get; }

        public ElasticClient CreateClient(Uri uri);

        public ISearchResponse<T> GetResponseOfQuery<T>(string indexName, QueryContainer queryContainer, int size = 20)
            where T : class;

        public QueryContainer MakeFuzzyQuery(string query, string field, int fuzziness = -1);

        public QueryContainer MakeMatchQuery(string query, string field, int fuzziness = 0);

        public QueryContainer MakeMultiMatchQuery(string query, string[] fields, int fuzziness = 1);

        public QueryContainer MakeTermQuery(string query, string field, double boost = 1);

        public QueryContainer MakeTermsQuery(string[] queries, string field, double boost = 1);

        public QueryContainer MakeRangeQuery(string type, string gte, string lte, string field, double boost = 1);

        public QueryContainer MakeBoolQuery(QueryContainer[] must = null, QueryContainer[] filter = null,
            QueryContainer[] should = null, QueryContainer[] mustNot = null, double boost = 1);

        public QueryContainer MakeGeoDistanceQuery(string distance, double latitude,
            double longitude, Field distanceField, double boost = 1);

        public ISearchResponse<T> GetResponseOfAggregates<T>(string indexName, TermsAggregation termsAggregation)
            where T : class;

        public TermsAggregation MakeTermsAggQuery(string field, string name = "", bool keyword = false);

        public ResponseBase CreateIndex<T>(string indexName,
            Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> settingSelector = null,
            Func<TypeMappingDescriptor<T>, ITypeMapping> mapSelector = null) where T : class;

        public ResponseBase DeleteIndex(string indexName);

        public BulkResponse BulkIndex<T>(string indexName, IEnumerable<T> dataList, string idFieldName) where T : class;

        public IndexResponse Index<T>(string indexName, T document, string idFieldName) where T : class;

        public T GetDocument<T>(string indexName, string id) where T : class;

        public RefreshResponse Refresh(string indexName);

        public CatResponse<CatNodesRecord> GetCatNodes();

        public CatResponse<CatIndicesRecord> GetCatIndices();

        public ClusterHealthResponse GetClusterHealth(string indexName,
            Func<ClusterHealthDescriptor, IClusterHealthRequest> healthSelector = null);

        public static void QueryResponsePrinter<T>(string queryType, ISearchResponse<T> response) where T : class
        {
            Console.WriteLine(queryType + " query:  ---------------------");
            response.Hits.ToList().ForEach(x => Console.WriteLine(x.Source.ToString()));
        }

        public static void TermAggResponsePrinter<T>(ISearchResponse<T> response, string name) where T : class
        {
            Console.WriteLine(name + " Terms Aggregation:  ---------------------");
            response.Aggregations.Terms(name).Buckets.ToList()
                .ForEach(x => Console.WriteLine(x.Key + " : " + x.DocCount));
        }
    }
}