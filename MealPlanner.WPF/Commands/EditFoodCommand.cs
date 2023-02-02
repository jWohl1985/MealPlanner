using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MealPlanner.Domain.Models;
using MealPlanner.WPF.State.Navigators;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.Commands;

public class EditFoodCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;

    public EditFoodCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override void Execute(object? parameter)
    {
        FoodViewModel foodViewModel = (FoodViewModel)parameter!;
        Food existingFood = foodViewModel.Food;

        _mainViewModel.Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.CreateOrUpdateFoodView);
        CreateOrUpdateFoodViewModel editFoodViewModel = (CreateOrUpdateFoodViewModel)_mainViewModel.Navigator.CurrentViewModel;

        Food tempCopyOfFoodToEdit = new()
        {
            Id = existingFood.Id,
            Name = existingFood.Name,
            Calories = existingFood.Calories,
            GramsOfProtein = existingFood.GramsOfProtein,
            GramsOfCarbs = existingFood.GramsOfCarbs,
            GramsOfFat = existingFood.GramsOfFat,
            ServingSize = existingFood.ServingSize,
            Notes = existingFood.Notes,
        };

        editFoodViewModel.Food = tempCopyOfFoodToEdit;
    }
}
