using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.Domain.Models;
using MealPlanner.Domain.Services;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.Commands;

public class DeleteMealCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;

    private IRepository<Meal> _mealRepository => _mainViewModel.DietDataService.MealRepository;
    private DayViewModel _dayViewModel => _mainViewModel.DayViewModel;

    public DeleteMealCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override async void Execute(object? parameter)
    {
        MealViewModel mealViewModel = (MealViewModel)parameter!;

        await _mealRepository.DeleteAsync(mealViewModel.Meal.Id);
        _dayViewModel.Meals.Remove(mealViewModel);
    }
}
