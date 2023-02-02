using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.Domain.Models;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.Commands;

public class SortByCommand : CommandBase
{
    private readonly FoodDatabaseViewModel _foodDatabaseViewmodel;

    public SortByCommand(FoodDatabaseViewModel foodDatabaseViewModel)
    {
        _foodDatabaseViewmodel = foodDatabaseViewModel;
    }

    public override void Execute(object? parameter)
    {
        _foodDatabaseViewmodel.SortBy = SortByColumn(parameter!);
    }

    private SortDescription SortByColumn(object parameter)
    {
        SortDescription oldSorting = _foodDatabaseViewmodel.SortBy;

        ListSortDirection newSortDirection;
        string newPropertyName = nameof(Food) + "." + (string)parameter;

        if (oldSorting.PropertyName == newPropertyName)
        {
            newSortDirection = oldSorting.Direction switch
            {
                ListSortDirection.Ascending => ListSortDirection.Descending,
                ListSortDirection.Descending => ListSortDirection.Ascending,
                _ => ListSortDirection.Ascending,
            };
        }
        else
        {
            newSortDirection = ListSortDirection.Ascending;
        }

        return new SortDescription()
        {
            Direction = newSortDirection,
            PropertyName = newPropertyName,
        };
    }
}
