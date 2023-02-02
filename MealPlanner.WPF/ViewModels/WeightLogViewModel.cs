using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MealPlanner.Domain.Models;
using MealPlanner.WPF.Commands;
using MealPlanner.WPF.State.Navigators;

namespace MealPlanner.WPF.ViewModels;

public class WeightLogViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;

    private DietUser DietUser => _mainViewModel.UserListViewModel.SelectedUser!;
    
    public ObservableCollection<WeightLogEntryViewModel> WeightLogs { get; set; }
    public ICommand CreateWeightLogEntryCommand { get; set; }
    public ICommand DeleteWeightLogEntryCommand { get; set; }
    public INavigator Navigator => _mainViewModel.Navigator;

    private string _newEntryDate;
    public string NewEntryDate 
    {
        get => _newEntryDate;
        set
        {
            _newEntryDate = value;
            ValidateDate(value);
            OnPropertyChanged(nameof(NewEntryDate));
        }
    }

    private string _newEntryWeight;
    public string NewEntryWeight
    {
        get => _newEntryWeight;
        set
        {
            _newEntryWeight = value;
            ValidateWeight(value);
            OnPropertyChanged(nameof(NewEntryWeight));
        }
    }

    private string _dateErrors;
    public string DateErrors
    {
        get => _dateErrors;
        set
        {
            _dateErrors = value;
            OnPropertyChanged(nameof(DateErrors));
        }
    }

    private string _weightErrors;
    public string WeightErrors
    {
        get => _weightErrors;
        set
        {
            _weightErrors = value;
            OnPropertyChanged(nameof(WeightErrors));
        }
    }

    public WeightLogViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;

        _newEntryDate = DateTime.Now.ToShortDateString();
        _newEntryWeight = "";

        WeightLogs = new ObservableCollection<WeightLogEntryViewModel>();
        foreach(WeightLogEntry entry in _mainViewModel.DietDataService.GetUserWeightLogs(DietUser.Id))
        {
            WeightLogs.Add(new WeightLogEntryViewModel(this, entry));
        }

        CreateWeightLogEntryCommand = new CreateWeightLogEntryCommand(_mainViewModel, this);
        DeleteWeightLogEntryCommand = new DeleteWeightLogEntryCommand(_mainViewModel, this);

        _dateErrors = "";
        _weightErrors = "Weight must be a number. Use only digit and decimal characters, e.g. 210 or 140.8";
    }

    private void ValidateDate(string dateText)
    {
        if (!DateOnly.TryParse(dateText, out DateOnly date))
        {
            DateErrors = "Date must be entered in mm/dd/yy format, e.g. 01/28/23";
            return;
        }

        DateErrors = "";
    }

    private void ValidateWeight(string weightText)
    {
        if (!float.TryParse(weightText, out float weight))
        {
            WeightErrors = "Weight must be a number. Use only digit and decimal characters, e.g. 210 or 140.8";
            return;
        }

        if (weight < 80 || weight > 500)
        {
            WeightErrors = "Weight must be between 80 and 500";
            return;
        }

        WeightErrors = "";
    }

}
