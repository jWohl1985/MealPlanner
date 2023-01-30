using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Take100.Domain.Models;
using Take100.Domain.Services;
using Take100.WPF.Commands;

namespace Take100.WPF.ViewModels;

public class FoodViewModel
{
    private readonly FoodDatabaseViewModel _foodListViewModel;

    public Food Food { get; private set; }
    public string Name  => Food.Name;
    public string Calories => Food.Calories.ToString("0.#");
    public string Protein => Food.GramsOfProtein.ToString("0.#");
    public string Carbs => Food.GramsOfCarbs.ToString("0.#");
    public string Fat => Food.GramsOfFat.ToString("0.#");
    public string ServingSize => Food.ServingSize;
    public ICommand AddFoodToMealPlanCommand => _foodListViewModel.AddFoodToMealPlanCommand;
    public ICommand EditFoodCommand => _foodListViewModel.EditFoodCommand;
    public ICommand DeleteFoodCommand => _foodListViewModel.DeleteFoodCommand;


    public FoodViewModel(FoodDatabaseViewModel foodListViewModel, Food food)
    {
        _foodListViewModel = foodListViewModel;
        Food = food;
    }

    public async Task ResyncWithDatabase(IDietDataService dataService)
    {
        Food? food = await dataService.FoodRepository.GetByIdAsync(Food.Id)!;

        if (food is not null)
            Food = food;
    }
}
