using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take100.WPF.ViewModels;

namespace Take100.WPF.Commands;

public class DeleteWeightLogEntryCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly WeightLogViewModel _weightLogViewModel;

    public DeleteWeightLogEntryCommand(MainViewModel mainViewModel, WeightLogViewModel weightLogViewModel)
    {
        _mainViewModel = mainViewModel;
        _weightLogViewModel = weightLogViewModel;
    }

    public override async void Execute(object? parameter)
    {
        WeightLogEntryViewModel viewModel = (WeightLogEntryViewModel)parameter!;

        _weightLogViewModel.WeightLogs.Remove(viewModel);
        await _mainViewModel.DietDataService.WeightLogRepository.DeleteAsync(viewModel.WeightLogEntry.Id);
    }
}
