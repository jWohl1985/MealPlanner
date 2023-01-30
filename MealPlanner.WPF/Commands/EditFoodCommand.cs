using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Take100.Domain.Models;
using Take100.WPF.State.Navigators;
using Take100.WPF.ViewModels;

namespace Take100.WPF.Commands;

public class EditFoodCommand : CommandBase
{
    private MainViewModel _mainViewModel;

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
