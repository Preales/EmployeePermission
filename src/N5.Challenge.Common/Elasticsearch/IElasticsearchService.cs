﻿namespace N5.Challenge.Common.Elasticsearch
{
    public interface IElasticsearchService
    {
        Task<bool> AddDocumentAsync<T>(T document, string index) where T : class;
    }
}