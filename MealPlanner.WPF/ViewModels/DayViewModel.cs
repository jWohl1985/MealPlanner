using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MealPlanner.Domain.Models;
using MealPlanner.Domain.Services;
using MealPlanner.WPF.Commands;

namespace MealPlanner.WPF.ViewModels;

public class DayViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    private DietUser? SelectedUser => _mainViewModel.UserListViewModel.SelectedUser;

    private DayOfWeek _selectedDay;
    public DayOfWeek SelectedDay
    {
        get => _selectedDay;
        set
        {
            _selectedDay = value;
            OnPropertyChanged(nameof(SelectedDay));
            DisplayMealsForSelectedUserAndDay();
        }
    }

    public ICommand SelectDayCommand { get; set; }
    public bool IsSunday => SelectedDay is DayOfWeek.Sunday;
    public bool IsMonday => SelectedDay is DayOfWeek.Monday;
    public bool IsTuesday => SelectedDay is DayOfWeek.Tuesday;
    public bool IsWednesday => SelectedDay is DayOfWeek.Wednesday;
    public bool IsThursday => SelectedDay is DayOfWeek.Thursday;
    public bool IsFriday => SelectedDay is DayOfWeek.Friday;
    public bool IsSaturday => SelectedDay is DayOfWeek.Saturday;

    public ObservableCollection<MealViewModel> Meals { get; set; }
    public ICommand DeleteMealCommand { get; set; }
    public ICommand MarkAsEatenCommand { get; set; }
    
    public GoalComparisonViewModel GoalComparisonViewModel { get; set; }
    
    public DayViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        _mainViewModel.UserListViewModel.PropertyChanged += OnUserViewModelPropertyChange;
        Meals = new ObservableCollection<MealViewModel>();

        DeleteMealCommand = new DeleteMealCommand(_mainViewModel);
        SelectDayCommand = new SelectDayCommand(this);
        GoalComparisonViewModel = new GoalComparisonViewModel(_mainViewModel, this);
        MarkAsEatenCommand = new MarkAsEatenCommand();
    }

    private void DisplayMealsForSelectedUserAndDay()
    {
        Meals.Clear();

        if (SelectedUser is not null)
        {
            foreach (Meal meal in _mainViewModel.DietDataService.GetUserMealsByDay(SelectedUser.Id, SelectedDay))
            {
                MealViewModel mealViewModel = new(_mainViewModel, meal);
                Meals.Add(mealViewModel);
            }
        }
    }

    private void OnUserViewModelPropertyChange(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_mainViewModel.UserListViewModel.SelectedUser))
        {
            DisplayMealsForSelectedUserAndDay();
        }
    }
}
