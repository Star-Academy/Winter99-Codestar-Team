using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Nest;

namespace Back_End.Elastic
{
    public abstract class Elastic<T> : IElastic<T> where T : class
    {
        private readonly string _indexName;
        public ElasticClient Client { get; }

        protected Elastic(IConfiguration configuration, string indexName)
        {
            _indexName = indexName;
            Client = CreateClient(new Uri(configuration["elasticUri"]));
            if (!IndexExists())
            {
                CreateIndex(mapSelector: CreateMapping);
            }
        }

        protected abstract ITypeMapping CreateMapping(TypeMappingDescriptor<T> mappingDescriptor);

        public ElasticClient CreateClient(Uri uri)
        {
            var connectionSettings = new ConnectionSettings(uri);
            connectionSettings.EnableDebugMode(); //todo : remove debug mode in production
            return new ElasticClient(connectionSettings);
        }

        public ISearchResponse<T> GetResponseOfQuery(QueryContainer queryContainer, int size = 20)
        {
            return Client.Search<T>(s => s.Index(_indexName).Query(_ => queryContainer).Size(size));
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

        public ISearchResponse<T> GetResponseOfAggregates(TermsAggregation termsAggregation)
        {
            return Client.Search<T>(s => s.Index(_indexName).Aggregations(
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


        public ResponseBase CreateIndex(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> settingSelector = null,
            Func<TypeMappingDescriptor<T>, ITypeMapping> mapSelector = null)
        {
            return Client.Indices.Create(_indexName,
                s => s.Settings(settingSelector).Map(mapSelector));
        }

        public ResponseBase DeleteIndex(string indexName)
        {
            return Client.Indices.Delete(indexName);
        }

        public BulkResponse BulkIndex(IEnumerable<T> dataList, Func<T, string> typeIndexId)
        {
            var bulkDescriptor = new BulkDescriptor();
            foreach (var data in dataList)
            {
                bulkDescriptor.Index<T>(x => x
                    .Index(_indexName)
                    .Document(data)
                    .Id(typeIndexId.Invoke(data))
                );
            }

            return Client.Bulk(bulkDescriptor);
        }

        public IndexResponse Index(T document, Func<T, string> typeIndexId)
        {
            return Client.Index(document, x => x
                .Index(_indexName)
                .Id(typeIndexId.Invoke(document)));
        }

        public T GetDocument(string id)
        {
            return Client.Get<T>(id, g => g.Index(_indexName)).Source;
        }

        public RefreshResponse Refresh()
        {
            return Client.Indices.Refresh(_indexName);
        }

        public CatResponse<CatNodesRecord> GetCatNodes()
        {
            return Client.Cat.Nodes();
        }

        public CatResponse<CatIndicesRecord> GetCatIndices()
        {
            return Client.Cat.Indices();
        }

        public ClusterHealthResponse GetClusterHealth(
            Func<ClusterHealthDescriptor, IClusterHealthRequest> healthSelector = null)
        {
            return Client.Cluster.Health(_indexName, healthSelector);
        }

        public bool IndexExists()
        {
            return Client.Indices.Exists(_indexName).Exists;
        }
    }
}