using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Take100.Domain.Models;
using Take100.Domain.Services;
using Take100.WPF.ViewModels;

namespace Take100.WPF.Commands;

public class DeleteFoodCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;

    private IRepository<Food> _foodRepository => _mainViewModel.DietDataService.FoodRepository;
    private FoodDatabaseViewModel _foodListViewModel => _mainViewModel.FoodDatabaseViewModel;

    public DeleteFoodCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override async void Execute(object? parameter)
    {
        FoodViewModel foodViewModel = (FoodViewModel)parameter!;

        if (ConfirmDelete(foodViewModel))
        {
            await _foodRepository.DeleteAsync(foodViewModel.Food.Id);
            _foodListViewModel.Foods.Remove(foodViewModel);

            RemoveMealsThatUseFood(foodViewModel.Food.Id);
        }
        else
        {
            return;
        }
    }

    private bool ConfirmDelete(FoodViewModel foodViewModel)
    {
        string message = $"Do you really want to delete {foodViewModel.Food.Name}? This can't be undone.";
        string title = "Confirm delete";

        MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo);

        return result is MessageBoxResult.Yes;
    }

    private void RemoveMealsThatUseFood(int id)
    {
        ObservableCollection<MealViewModel> Meals = _mainViewModel.DayViewModel.Meals;

        for (int i = Meals.Count - 1; i >= 0; i--)
        {
            if (Meals[i].Food.Id == id)
            {
                Meals.RemoveAt(i);
            }
        }
    }
}
