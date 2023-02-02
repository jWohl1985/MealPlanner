using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.Domain.Models;
using MealPlanner.Domain.Services;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.Commands;

public class AddFoodToMealPlanCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;

    private IRepository<Meal> _mealRepository => _mainViewModel.DietDataService.MealRepository;
    private UserListViewModel _userViewModel => _mainViewModel.UserListViewModel;
    private DayViewModel _dayViewModel => _mainViewModel.DayViewModel;
    
    public AddFoodToMealPlanCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override bool CanExecute(object? parameter)
    {
        return _userViewModel.SelectedUser is not null;
    }

    public override async void Execute(object? parameter)
    {
        FoodViewModel foodViewModel = (FoodViewModel)parameter!;
        Food food = foodViewModel.Food;
        Meal addedMeal = new()
        {
            DietUserId = _userViewModel.SelectedUser!.Id,
            FoodId = food.Id,
            DayNumber = (int)_dayViewModel.SelectedDay,
            ServingsToEat = 1,
        };

        await _mealRepository.CreateAsync(addedMeal);

        addedMeal.Food = food;
        _dayViewModel.Meals.Add(new MealViewModel(_mainViewModel, addedMeal));
    }
}
