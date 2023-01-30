using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take100.Domain.Models;
using Take100.WPF.ViewModels;

namespace Take100.WPF.Commands;

public class CreateWeightLogEntryCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly WeightLogViewModel _weightLogViewModel;

    public CreateWeightLogEntryCommand(MainViewModel mainViewModel, WeightLogViewModel weightLogViewModel)
    {
        _mainViewModel = mainViewModel;
        _weightLogViewModel = weightLogViewModel;
        _weightLogViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_weightLogViewModel.NewEntryWeight) || e.PropertyName == nameof(_weightLogViewModel.NewEntryDate))
            OnCanExecuteChanged();
    }

    public override bool CanExecute(object? parameter)
    {
        return _weightLogViewModel.DateErrors == "" && _weightLogViewModel.WeightErrors == "";
    }

    public override async void Execute(object? parameter)
    {
        int userId = _mainViewModel.UserListViewModel.SelectedUser!.Id;
        DateTime dateTime = DateOnly.Parse(_weightLogViewModel.NewEntryDate).ToDateTime(TimeOnly.Parse("12:00AM"));
        float weight = float.Parse(_weightLogViewModel.NewEntryWeight);

        WeightLogEntry weightLogEntry = new WeightLogEntry
        {
            DietUserId = userId,
            Date = dateTime,
            WeightInLbs = weight,
        };

        _weightLogViewModel.WeightLogs.Add(new WeightLogEntryViewModel(_weightLogViewModel, weightLogEntry));
        _weightLogViewModel.NewEntryDate = "";
        _weightLogViewModel.NewEntryWeight = "";
        await _mainViewModel.DietDataService.WeightLogRepository.CreateAsync(weightLogEntry);
    }

    private bool ValidateUserInput()
    {
        float MIN_WEIGHT = 80;
        float MAX_WEIGHT = 400;

        if (false == DateTime.TryParse(_weightLogViewModel.NewEntryDate, out DateTime dateTime))
        {
            return false;
        }

        if (false == float.TryParse(_weightLogViewModel.NewEntryWeight, out float weight))
        {
            return false;
        }
        else
        {
            if (weight < MIN_WEIGHT || weight > MAX_WEIGHT)
            {
                return false;
            }
        }

        return true;
    }
}
