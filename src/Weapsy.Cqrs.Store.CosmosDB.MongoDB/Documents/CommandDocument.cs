﻿using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Weapsy.Cqrs.Store.CosmosDB.MongoDB.Documents
{
    public class CommandDocument
    {
        [BsonId]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("aggregateId")]
        public string AggregateId { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("data")]
        public string Data { get; set; }

        [BsonElement("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [BsonElement("userId")]
        public Guid UserId { get; set; }

        [BsonElement("source")]
        public string Source { get; set; }
    }
}
