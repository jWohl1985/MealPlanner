using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Take100.Domain.Models;

namespace Take100.EntityFramework.SeedData;

internal class SeedDietUserData : IEntityTypeConfiguration<DietUser>
{
    public void Configure(EntityTypeBuilder<DietUser> builder)
    {
        DietUser[] users = SeedDietUsers();
        builder.HasData(users);
    }

    public static DietUser[] SeedDietUsers()
    {
        DietUser john = new()
        { 
            Id = 1, 
            Name = "John",
            Age = 37,
            IsMale = true,
            WeightInLbs = 145,
            HeightFeetComponent = 5,
            HeightInchComponent = 9,
            HoursExercisePerWeek = 3,
            Goal = 0,
            NeckCircumferenceInches = 13,
            WaistCircumferenceInches = 32,
        };

        DietUser mary = new()
        { 
            Id = 2, 
            Name = "Mary",
            Age = 54,
            IsMale = false,
            WeightInLbs = 170,
            HeightFeetComponent = 5,
            HeightInchComponent = 5,
            HoursExercisePerWeek = 1,
            Goal = 0,
            NeckCircumferenceInches = 15,
            WaistCircumferenceInches = 38,
            HipCircumferenceInches = 38,
        };

        return new DietUser[] { john, mary };
    }
}
