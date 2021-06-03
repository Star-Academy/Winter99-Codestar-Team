using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Back_End.Elastic
{
    public interface IElastic<T> where T : class
    {
        // todo : remove not useful methods
        public ElasticClient Client { get; }

        public ElasticClient CreateClient(Uri uri);

        public ISearchResponse<T> GetResponseOfQuery(QueryContainer queryContainer, int size = 20);

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

        public ISearchResponse<T> GetResponseOfAggregates(TermsAggregation termsAggregation);

        public TermsAggregation MakeTermsAggQuery(string field, string name = "", bool keyword = false);

        public ResponseBase CreateIndex(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> settingSelector = null,
            Func<TypeMappingDescriptor<T>, ITypeMapping> mapSelector = null);

        public ResponseBase DeleteIndex(string indexName);

        public BulkResponse BulkIndex(IEnumerable<T> dataList, Func<T, string> typeIndexId);

        public IndexResponse Index(T document, Func<T, string> typeIndexId);

        public T GetDocument(string id);

        public RefreshResponse Refresh();

        public CatResponse<CatNodesRecord> GetCatNodes();

        public CatResponse<CatIndicesRecord> GetCatIndices();

        public ClusterHealthResponse GetClusterHealth(
            Func<ClusterHealthDescriptor, IClusterHealthRequest> healthSelector = null);

        public IEnumerable<T> QueryResponseList(ISearchResponse<T> response);

        public static void TermAggResponsePrinter(ISearchResponse<T> response, string name)
        {
            Console.WriteLine(name + " Terms Aggregation:  ---------------------");
            response.Aggregations.Terms(name).Buckets.ToList()
                .ForEach(x => Console.WriteLine(x.Key + " : " + x.DocCount));
        }

        public bool IndexExists();
    }
}