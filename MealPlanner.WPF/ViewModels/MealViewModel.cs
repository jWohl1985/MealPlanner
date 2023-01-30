﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Take100.Domain.Models;
using Take100.Domain.Services;
using Take100.WPF.Commands;

namespace Take100.WPF.ViewModels;

public class MealViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    private DayViewModel DayViewModel => _mainViewModel.DayViewModel;

    public Meal Meal { get; private set; }
    public Food Food { get; private set; }

    private string _servingsText;
    public string ServingsText
    {
        get => _servingsText;
        set
        {
            if (!ValidateInput(value))
            {
                _servingsText = Meal.ServingsToEat.ToString();
                return;
            }

            if (value.Last<char>() == '.')
                value.Trim('.');

            _servingsText = value;
            UpdateMealServings(float.Parse(value));
        }
    }

    public bool HasBeenEaten
    {
        get => Meal.HasBeenEaten;
        set
        {
            Meal.HasBeenEaten = value;
            UpdateMealHasBeenEaten();
        }
    }

    public float ServingsToEat => Meal.ServingsToEat;
    public string FoodNameTextDecoration => Meal.HasBeenEaten ? "Strikethrough" : "";
    public string MealCalories => (Food.Calories * Meal.ServingsToEat).ToString("0.#");
    public string MealProtein => (Food.GramsOfProtein * Meal.ServingsToEat).ToString("0.#");
    public string MealCarbs => (Food.GramsOfCarbs * Meal.ServingsToEat).ToString("0.#");
    public string MealFat => (Food.GramsOfFat * Meal.ServingsToEat).ToString("0.#");

    private string ServingSizeInfo => $"Serving size: {Food.ServingSize}";
    private string NotesInfo => string.IsNullOrEmpty(Food.Notes) ? "" : $"\nNotes: {Food.Notes}";
    public string MoreInfoTooltip => ServingSizeInfo + NotesInfo;
    public ICommand DeleteMealCommand => DayViewModel.DeleteMealCommand;
    public ICommand MarkAsEatenCommand => DayViewModel.MarkAsEatenCommand;

    public MealViewModel(MainViewModel mainViewModel, Meal meal)
    {
        _mainViewModel = mainViewModel;
        Meal = meal;
        Food = meal.Food!;
        _servingsText = Meal.ServingsToEat.ToString("0.#");
    }

    public async Task ResyncWithDatabase()
    {
        Meal? meal = await _mainViewModel.DietDataService.MealRepository.GetByIdAsync(Meal.Id);
        Food? food = await _mainViewModel.DietDataService.FoodRepository.GetByIdAsync(Food.Id);

        if (meal is not null)
            Meal = meal;

        if (food is not null)
            Food = food;
    }

    private void UpdateMealServings(float newAmount)
    {
        Meal.ServingsToEat = newAmount;
        OnPropertyChanged(nameof(ServingsToEat));
        OnPropertyChanged(nameof(MealCalories));
        OnPropertyChanged(nameof(MealProtein));
        OnPropertyChanged(nameof(MealCarbs));
        OnPropertyChanged(nameof(MealFat));
        _mainViewModel.DietDataService.MealRepository.UpdateAsync(Meal.Id, Meal);
    }

    private void UpdateMealHasBeenEaten()
    {
        OnPropertyChanged(nameof(FoodNameTextDecoration));
        _mainViewModel.DietDataService.MealRepository.UpdateAsync(Meal.Id, Meal);
    }

    private bool ValidateInput(string input)
    {
        if (false == float.TryParse(input.Trim('.'), out float result))
            return false;

        if (result < 0 || result > 100)
            return false;

        return true;
    }
}