using System;
using System.Collections.Generic;
using Nest;

namespace Back_End.Elastic
{
    public class Elastic : IElastic
    {
        public ElasticClient Client { get; }

        public Elastic(Uri uri)
        {
            Client = this.CreateClient(uri);
        }

        public ElasticClient CreateClient(Uri uri)
        {
            var connectionSettings = new ConnectionSettings(uri);
            connectionSettings.EnableDebugMode();
            return new ElasticClient(connectionSettings);
        }

        public ISearchResponse<T> GetResponseOfQuery<T>(string indexName, QueryContainer queryContainer, int size = 20)
            where T : class
        {
            return Client.Search<T>(s => s.Index(indexName).Query(q => queryContainer).Size(size));
        }

        public QueryContainer MakeFuzzyQuery(string query, string field, int fuzziness = -1)
        {
            return new FuzzyQuery
            {
                Field = field,
                Value = query,
                Fuzziness = fuzziness == -1 ? Fuzziness.Auto : Fuzziness.EditDistance(fuzziness)
            };
        }

        public QueryContainer MakeMatchQuery(string query, string field, int fuzziness = 0)
        {
            return new MatchQuery
            {
                Query = query,
                Field = field,
                Fuzziness = Fuzziness.EditDistance(fuzziness)
            };
        }

        public QueryContainer MakeMultiMatchQuery(string query, string[] fields, int fuzziness = 1)
        {
            return new MultiMatchQuery
            {
                Query = query,
                Fields = fields,
                Fuzziness = Fuzziness.EditDistance(fuzziness)
            };
        }

        public QueryContainer MakeTermQuery(string query, string field, double boost = 1)
        {
            return new TermQuery
            {
                Field = field,
                Value = query,
                Boost = boost
            };
        }

        public QueryContainer MakeTermsQuery(string[] queries, string field, double boost = 1)
        {
            return new TermsQuery
            {
                Field = field,
                Terms = queries,
                Boost = boost
            };
        }

        public QueryContainer MakeRangeQuery(string type, string gte, string lte,
            string field, double boost = 1)
        {
            switch (type.ToLower())
            {
                case "long":
                    return new LongRangeQuery()
                    {
                        Field = field,
                        LessThan = long.Parse(lte),
                        GreaterThan = long.Parse(gte),
                        Boost = boost
                    };
                case "date":
                    return new DateRangeQuery()
                    {
                        Field = field,
                        LessThan = DateMath.FromString(lte),
                        GreaterThan = DateMath.FromString(gte),
                        Boost = boost
                    };
                case "term":
                    return new TermRangeQuery()
                    {
                        Field = field,
                        LessThan = lte,
                        GreaterThan = gte,
                        Boost = boost
                    };

                default:
                    return new TermRangeQuery()
                    {
                        Field = field,
                        LessThan = lte,
                        GreaterThan = gte,
                        Boost = boost
                    };
            }
        }

        public QueryContainer MakeBoolQuery(QueryContainer[] must = null, QueryContainer[] filter = null,
            QueryContainer[] should = null, QueryContainer[] mustNot = null, double boost = 1)
        {
            return new BoolQuery
            {
                Must = must,
                Should = should,
                Filter = filter,
                MustNot = mustNot,
                Boost = boost
            };
        }

        public QueryContainer MakeGeoDistanceQuery(string distance, double latitude,
            double longitude, Field distanceField, double boost = 1)
        {
            return new GeoDistanceQuery
            {
                Field = distanceField,
                DistanceType = GeoDistanceType.Arc,
                Location = new GeoLocation(latitude, longitude),
                Distance = distance,
                Boost = boost
            };
        }

        public ISearchResponse<T> GetResponseOfAggregates<T>(string indexName, TermsAggregation termsAggregation)
            where T : class
        {
            return Client.Search<T>(s => s.Index(indexName).Aggregations(
                termsAggregation));
        }

        public TermsAggregation MakeTermsAggQuery(string field, string name = "", bool keyword = false)
        {
            if (name == "")
                name = field;
            return new TermsAggregation(name)
            {
                Field = field + (keyword ? ".keyword" : "")
            };
        }


        public ResponseBase CreateIndex<T>(string indexName,
            Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> settingSelector = null,
            Func<TypeMappingDescriptor<T>, ITypeMapping> mapSelector = null) where T : class
        {
            return Client.Indices.Create(indexName,
                s => s.Settings(settingSelector).Map(mapSelector));
        }

        public ResponseBase DeleteIndex(string indexName)
        {
            return Client.Indices.Delete(indexName);
        }

        public BulkResponse BulkIndex<T>(string indexName, IEnumerable<T> dataList, string idFieldName) where T : class
        {
            var bulkDescriptor = new BulkDescriptor();
            foreach (var data in dataList)
            {
                bulkDescriptor.Index<T>(x => x
                    .Index(indexName)
                    .Document(data)
                    .Id((string) data.GetType().GetProperty(idFieldName).GetValue(data))
                );
            }

            return Client.Bulk(bulkDescriptor);
        }

        public IndexResponse Index<T>(string indexName, T document, string idFieldName) where T : class
        {
            return Client.Index<T>(document, x => x
                .Index(indexName)
                .Id((string) document.GetType().GetProperty(idFieldName).GetValue(document)));
        }

        public T GetDocument<T>(string indexName, string id) where T : class
        {
            return Client.Get<T>(id, g => g.Index(indexName)).Source;
        }

        public RefreshResponse Refresh(string indexName)
        {
            return Client.Indices.Refresh(indexName);
        }

        public CatResponse<CatNodesRecord> GetCatNodes()
        {
            return Client.Cat.Nodes();
        }

        public CatResponse<CatIndicesRecord> GetCatIndices()
        {
            return Client.Cat.Indices();
        }

        public ClusterHealthResponse GetClusterHealth(string indexName,
            Func<ClusterHealthDescriptor, IClusterHealthRequest> healthSelector = null)
        {
            return Client.Cluster.Health(indexName, healthSelector);
        }

        public bool IndexExists(string indexName)
        {
            return Client.Indices.Exists(indexName).Exists;
        }
    }
}