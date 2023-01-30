using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Take100.Domain.Models;

namespace Take100.EntityFramework.SeedData;

internal class SeedWeightLogs : IEntityTypeConfiguration<WeightLogEntry>
{
    public void Configure(EntityTypeBuilder<WeightLogEntry> builder)
    {
        WeightLogEntry[] weightLogs = SeedWeightLogEntries();
        builder.HasData(weightLogs);
    }

    public WeightLogEntry[] SeedWeightLogEntries()
    {
        WeightLogEntry entry1 = new WeightLogEntry
        {
            Id = 1,
            DietUserId = 1,
            WeightInLbs = 160,
            Date = new DateTime(2022, 08, 17),
        };

        WeightLogEntry entry2 = new WeightLogEntry
        {
            Id = 2,
            DietUserId = 1,
            WeightInLbs = 158,
            Date = new DateTime(2022, 08, 24),
        };

        WeightLogEntry entry3 = new WeightLogEntry
        {
            Id = 3,
            DietUserId = 2,
            WeightInLbs = 180,
            Date = new DateTime(2023, 01, 16),
        };

        WeightLogEntry entry4 = new WeightLogEntry
        {
            Id = 4,
            DietUserId = 1,
            WeightInLbs = 179.4f,
            Date = new DateTime(2023, 01, 18),
        };

        return new WeightLogEntry[] { entry1, entry2, entry3, entry4 };
    }
}
