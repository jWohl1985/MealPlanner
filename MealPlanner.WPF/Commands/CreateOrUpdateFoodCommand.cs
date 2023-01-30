using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take100.Domain.Models;
using Take100.WPF.State.Navigators;
using Take100.WPF.ViewModels;

namespace Take100.WPF.Commands;

public class CreateOrUpdateFoodCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly CreateOrUpdateFoodViewModel _createOrUpdateFoodViewModel;

    private FoodDatabaseViewModel FoodListViewModel => _mainViewModel.FoodDatabaseViewModel;
    private DayViewModel DayViewModel => _mainViewModel.DayViewModel;

    public CreateOrUpdateFoodCommand(MainViewModel mainViewModel, CreateOrUpdateFoodViewModel createOrUpdateFoodViewModel)
    {
        _mainViewModel = mainViewModel;
        _createOrUpdateFoodViewModel = createOrUpdateFoodViewModel;
        _createOrUpdateFoodViewModel.PropertyChanged += OnViewModelChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return !_createOrUpdateFoodViewModel.HasErrors;
    }

    public override async void Execute(object? parameter)
    {
        Food food = (Food)parameter!;

        Food? existingFood = await _mainViewModel.DietDataService.FoodRepository.GetByIdAsync(food.Id);

        if (existingFood is null)
        {
            await _mainViewModel.DietDataService.FoodRepository.CreateAsync(food);
            FoodListViewModel.Foods.Add(new FoodViewModel(FoodListViewModel, food));
        }
        else
        {
            await _mainViewModel.DietDataService.FoodRepository.UpdateAsync(food.Id, food);

            FoodViewModel foodViewModel = FoodListViewModel.Foods.Where(f => f.Food.Id == food.Id).First();
            await foodViewModel.ResyncWithDatabase(_mainViewModel.DietDataService);

            IEnumerable<MealViewModel> mealsUsingFood = DayViewModel.Meals.Where(m => m.Food.Id == food.Id);

            foreach (MealViewModel mealViewModel in mealsUsingFood)
            {
                await mealViewModel.ResyncWithDatabase();
            }
        }
        
        _mainViewModel.Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.MainView);
    }

    public void OnViewModelChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_createOrUpdateFoodViewModel.NameErrors)
            || e.PropertyName == nameof(_createOrUpdateFoodViewModel.CalorieErrors)
            || e.PropertyName == nameof(_createOrUpdateFoodViewModel.ProteinErrors)
            || e.PropertyName == nameof(_createOrUpdateFoodViewModel.CarbErrors)
            || e.PropertyName == nameof(_createOrUpdateFoodViewModel.FatErrors)
            || e.PropertyName == nameof(_createOrUpdateFoodViewModel.ServingSizeErrors))
        {
            OnCanExecuteChanged();
        }
    }
}
