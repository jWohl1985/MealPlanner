using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.WPF.Commands;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.Commands;

public class ClearSearchTextCommand : CommandBase
{
    public FoodDatabaseViewModel _foodDatabaseViewModel;

    public ClearSearchTextCommand(FoodDatabaseViewModel foodDatabaseViewModel)
    {
        _foodDatabaseViewModel = foodDatabaseViewModel;
    }

    public override void Execute(object? parameter)
    {
        _foodDatabaseViewModel.SearchText = "";
    }
}
