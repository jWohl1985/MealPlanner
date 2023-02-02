using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.Commands;

public class MarkAsEatenCommand : CommandBase
{
    public override void Execute(object? parameter)
    {
        MealViewModel mealViewModel = (MealViewModel)parameter!;
        mealViewModel.HasBeenEaten = !mealViewModel.HasBeenEaten;
    }
}
