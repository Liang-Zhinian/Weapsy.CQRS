﻿using Newtonsoft.Json;
using Weapsy.Cqrs.Domain;

namespace Weapsy.Cqrs.Store.EF.Entities.Factories
{
    public class CommandEntityFactory : ICommandEntityFactory
    {
        public CommandEntity CreateCommand(IDomainCommand command)
        {
            return new CommandEntity
            {
                Id = command.Id,
                AggregateId = command.AggregateRootId,
                Type = command.GetType().AssemblyQualifiedName,
                Data = JsonConvert.SerializeObject(command),
                TimeStamp = command.TimeStamp,
                UserId = command.UserId,
                Source = command.Source
            };
        }
    }
}