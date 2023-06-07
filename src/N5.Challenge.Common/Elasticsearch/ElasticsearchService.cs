using Nest;

namespace N5.Challenge.Common.Elasticsearch
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticsearchService(
            ElasticsearchSetting configuration)
        {
            var settings = new ConnectionSettings(new Uri(configuration.Url));

            _elasticClient = new ElasticClient(settings);
        }

        public async Task<bool> AddDocumentAsync<T>(T document, string index) where T : class
        {
            var indexResponse = await _elasticClient.IndexAsync(document, idx => idx.Index(index));
            return indexResponse.IsValid;
        }
    }
}
