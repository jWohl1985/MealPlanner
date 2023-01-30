using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Take100.Domain.Models;
using Take100.Domain.Services;
using Take100.EntityFramework;
using Take100.EntityFramework.Services;
using Take100.WPF.Commands;
using Take100.WPF.Helpers;
using Take100.WPF.State.Navigators;

namespace Take100.WPF.ViewModels;

public class FoodDatabaseViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    public ObservableCollection<FoodViewModel> Foods { get; set; }
    public ICommand AddFoodCommand { get; set; }
    public ICommand EditFoodCommand { get; set; }
    public ICommand DeleteFoodCommand { get; set; }
    public ICommand AddFoodToMealPlanCommand { get; set; }
    public ICommand SortByCommand { get; set; }
    public ICommand ExportFoodDatabaseCommand { get; set; }
    public ICommand ImportFoodDatabaseCommand { get; set; }
    public INavigator Navigator => _mainViewModel.Navigator;
    public ICollectionView FilteredFoods { get; set; }

    private bool _onlyShowFoodsThatMeetGoals;
    public bool OnlyShowFoodsThatMeetGoals
    {
        get => _onlyShowFoodsThatMeetGoals;
        set
        {
            _onlyShowFoodsThatMeetGoals = value;
            FilteredFoods.Filter = (p => ApplyFilters(p));
            OnPropertyChanged(nameof(OnlyShowFoodsThatMeetGoals));
            OnPropertyChanged(nameof(FilteredFoods));
        }
    }

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            FilteredFoods.Filter = (p => ApplyFilters(p));
            OnPropertyChanged(nameof(SearchText));
            OnPropertyChanged(nameof(FilteredFoods));
        }
    }

    private SortDescription _sortBy;

    public SortDescription SortBy
    {
        get => _sortBy;
        set
        {
            _sortBy = value;
            FilteredFoods.SortDescriptions.Clear();
            FilteredFoods.SortDescriptions.Add(_sortBy);
            OnPropertyChanged(nameof(SortBy));
            OnPropertyChanged(nameof(FilteredFoods));
        }
    }

    public FoodDatabaseViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;

        Foods = new ObservableCollection<FoodViewModel>();
        foreach (Food food in _mainViewModel.DietDataService.FoodRepository.GetAll())
        {
            Foods.Add(new FoodViewModel(this, food));
        }

        FilteredFoods = CollectionViewSource.GetDefaultView(Foods);
        SortBy = new SortDescription()
        {
            PropertyName = nameof(Food) + "." + nameof(Food.Name),
            Direction = ListSortDirection.Ascending,
        };

        AddFoodCommand = new AddFoodCommand(_mainViewModel);
        EditFoodCommand = new EditFoodCommand(_mainViewModel);
        DeleteFoodCommand = new DeleteFoodCommand(_mainViewModel);
        AddFoodToMealPlanCommand = new AddFoodToMealPlanCommand(_mainViewModel);
        SortByCommand = new SortByCommand(this);
        ExportFoodDatabaseCommand = new ExportFoodDatabaseCommand(_mainViewModel.DietDataService);
        ImportFoodDatabaseCommand = new ImportFoodDatabaseCommand(_mainViewModel);
    }

    public void ReapplyFilters() => SortBy = SortBy;

    private bool ApplyFilters(object parameter)
    {
        FoodViewModel foodViewModel = (FoodViewModel)parameter;

        if (OnlyShowFoodsThatMeetGoals)
        {
            if (!MeetsGoals(foodViewModel.Food))
                return false;
        }

        if (foodViewModel.Food.Name.ToLower().Contains(_searchText.ToLower()))
            return true;

        return false;
    }

    private bool MeetsGoals(Food food)
    {
        GoalComparisonViewModel goals = _mainViewModel.DayViewModel.GoalComparisonViewModel;

        if ((food.Calories + goals.ScheduledCalories - goals.CalorieGoal) > UserGoalCalculator.CALORIE_WIGGLE_ROOM)
            return false;

        if ((food.GramsOfProtein + goals.ScheduledProtein - goals.ProteinGoal) > UserGoalCalculator.MACRONUTRIENT_WIGGLE_ROOM)
            return false;

        if ((food.GramsOfCarbs + goals.ScheduledCarbs - goals.CarbMaximumGoal) > UserGoalCalculator.MACRONUTRIENT_WIGGLE_ROOM)
            return false;

        if ((food.GramsOfFat + goals.ScheduledFat - goals.FatGoal) > UserGoalCalculator.MACRONUTRIENT_WIGGLE_ROOM)
            return false;

        return true;
    }
}
