﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Weapsy.Cqrs.Store.CosmosDB.Sql.Configuration;

namespace Weapsy.Cqrs.Store.CosmosDB.Sql.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder EnsureCosmosDbSqlDbCreated(this IApplicationBuilder app, IOptions<DomainDbConfiguration> settings)
        {
            var documentClient = app.ApplicationServices.GetRequiredService<IDocumentClient>();

            CreateDatabaseIfNotExistsAsync(documentClient, settings.Value.DatabaseId).Wait();
            CreateCollectionIfNotExistsAsync(documentClient, settings.Value.DatabaseId, settings.Value.AggregateCollectionId).Wait();
            CreateCollectionIfNotExistsAsync(documentClient, settings.Value.DatabaseId, settings.Value.CommandCollectionId).Wait();
            CreateCollectionIfNotExistsAsync(documentClient, settings.Value.DatabaseId, settings.Value.EventCollectionId).Wait();

            return app;
        }

        private static async Task CreateDatabaseIfNotExistsAsync(IDocumentClient documentClient, string databaseId)
        {
            try
            {
                await documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await documentClient.CreateDatabaseAsync(new Database { Id = databaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task CreateCollectionIfNotExistsAsync(IDocumentClient documentClient, string databaseId, string collectionId)
        {
            try
            {
                await documentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await documentClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(databaseId),
                        new DocumentCollection { Id = collectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}