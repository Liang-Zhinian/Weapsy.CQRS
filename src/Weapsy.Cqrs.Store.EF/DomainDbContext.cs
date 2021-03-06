﻿using Microsoft.EntityFrameworkCore;
using Weapsy.Cqrs.Store.EF.Entities;

namespace Weapsy.Cqrs.Store.EF
{
    public class DomainDbContext : DbContext
    {
        public DomainDbContext(DbContextOptions<DomainDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AggregateEntity>()
                .ToTable("DomainAggregate");

            builder.Entity<CommandEntity>()
                .ToTable("DomainCommand");

            builder.Entity<EventEntity>()
                .ToTable("DomainEvent");
        }

        public DbSet<AggregateEntity> Aggregates { get; set; }
        public DbSet<CommandEntity> Commands { get; set; }
        public DbSet<EventEntity> Events { get; set; }
    }
}
