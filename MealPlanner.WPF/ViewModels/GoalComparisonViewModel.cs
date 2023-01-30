using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take100.Domain.Models;
using Take100.WPF.Helpers;

namespace Take100.WPF.ViewModels;

public class GoalComparisonViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly DayViewModel _dayViewModel;

    private UserGoalCalculator GoalCalculator;

    public int CalorieGoal => GoalCalculator.CalorieGoal;
    public int ProteinGoal => GoalCalculator.ProteinGramGoal;
    public int CarbMinimumGoal => GoalCalculator.CarbGramMinimum;
    public int CarbMaximumGoal => GoalCalculator.CarbGramMaximum;
    public int FatGoal => GoalCalculator.FatGramGoal;

    public int ScheduledCalories => (int) _dayViewModel.Meals.Sum(m => m.Food.Calories * m.ServingsToEat);
    public int ScheduledProtein => (int) _dayViewModel.Meals.Sum(m => m.Food.GramsOfProtein * m.ServingsToEat);
    public int ScheduledCarbs => (int) _dayViewModel.Meals.Sum(m => m.Food.GramsOfCarbs * m.ServingsToEat);
    public int ScheduledFat => (int) _dayViewModel.Meals.Sum(m => m.Food.GramsOfFat * m.ServingsToEat);

    public int CalorieDifference => ScheduledCalories - CalorieGoal;
    public int ProteinDifference => ScheduledProtein - ProteinGoal;
    public int CarbDifference => CalculateCarbDifference();
    public int FatDifference => ScheduledFat - FatGoal;
    public string RedColor => "#d63031";
    public string GreenColor => "#00b894";
    public string CalorieDifferenceColor => Math.Abs(CalorieDifference) > UserGoalCalculator.CALORIE_WIGGLE_ROOM ? RedColor : GreenColor;
    public string ProteinDifferenceColor => Math.Abs(ProteinDifference) > UserGoalCalculator.MACRONUTRIENT_WIGGLE_ROOM ? RedColor : GreenColor;
    public string CarbDifferenceColor => Math.Abs(CarbDifference) > UserGoalCalculator.MACRONUTRIENT_WIGGLE_ROOM ? RedColor : GreenColor;
    public string FatDifferenceColor => Math.Abs(FatDifference) > UserGoalCalculator.MACRONUTRIENT_WIGGLE_ROOM ? RedColor : GreenColor;


    public GoalComparisonViewModel(MainViewModel mainViewModel, DayViewModel dayViewModel)
    {
        _mainViewModel = mainViewModel;
        _mainViewModel.UserListViewModel.PropertyChanged += OnUserViewModelChange;

        _dayViewModel = dayViewModel;
        _dayViewModel.Meals.CollectionChanged += OnMealAddedOrRemoved;
        _dayViewModel.SelectedDay = DateTime.Now.DayOfWeek;

        GoalCalculator = new UserGoalCalculator(_mainViewModel.UserListViewModel.SelectedUser!);
    }

    private void OnMealAddedOrRemoved(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (MealViewModel removedItem in e.OldItems)
                removedItem.PropertyChanged -= OnMealChanged;
        }

        if (e.NewItems is not null)
        {
            foreach (MealViewModel removedItem in e.NewItems)
                removedItem.PropertyChanged += OnMealChanged;
        }

        UpdateScheduledAmounts();
    }

    private void OnMealChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MealViewModel.ServingsToEat))
        {
            UpdateScheduledAmounts();
        }
    }

    private void OnUserViewModelChange(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_mainViewModel.UserListViewModel.SelectedUser))
        {
            GoalCalculator = new UserGoalCalculator(_mainViewModel.UserListViewModel.SelectedUser!);
            UpdateGoalAmounts();
            UpdateScheduledAmounts();
        }
    }

    public int CalculateCarbDifference()
    {
        if (ScheduledCarbs < CarbMinimumGoal)
            return ScheduledCarbs - CarbMinimumGoal;

        if (ScheduledCarbs > CarbMaximumGoal)
            return ScheduledCarbs - CarbMaximumGoal;

        return 0;
    }

    private void UpdateGoalAmounts()
    {
        OnPropertyChanged(nameof(CalorieGoal));
        OnPropertyChanged(nameof(ProteinGoal));
        OnPropertyChanged(nameof(CarbMinimumGoal));
        OnPropertyChanged(nameof(CarbMaximumGoal));
        OnPropertyChanged(nameof(FatGoal));
    }

    private void UpdateScheduledAmounts()
    {
        OnPropertyChanged(nameof(ScheduledCalories));
        OnPropertyChanged(nameof(ScheduledProtein));
        OnPropertyChanged(nameof(ScheduledCarbs));
        OnPropertyChanged(nameof(ScheduledFat));

        OnPropertyChanged(nameof(CalorieDifference));
        OnPropertyChanged(nameof(ProteinDifference));
        OnPropertyChanged(nameof(CarbDifference));
        OnPropertyChanged(nameof(FatDifference));

        OnPropertyChanged(nameof(CalorieDifferenceColor));
        OnPropertyChanged(nameof(ProteinDifferenceColor));
        OnPropertyChanged(nameof(CarbDifferenceColor));
        OnPropertyChanged(nameof(FatDifferenceColor));

        if (_mainViewModel.FoodDatabaseViewModel.OnlyShowFoodsThatMeetGoals)
        {
            _mainViewModel.FoodDatabaseViewModel.ReapplyFilters();
        }
    }
}
