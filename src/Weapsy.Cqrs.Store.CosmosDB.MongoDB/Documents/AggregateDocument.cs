﻿using MongoDB.Bson.Serialization.Attributes;

namespace Weapsy.Cqrs.Store.CosmosDB.MongoDB.Documents
{
    public class AggregateDocument
    {
        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }
    }
}
