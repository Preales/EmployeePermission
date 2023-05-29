namespace N5.Challenge.Common.Elasticsearch
{
    public interface IElasticsearchService
    {
        Task<bool> AddDocumentAsync<T>(T document) where T : class;
    }
}