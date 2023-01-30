using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Take100.Domain.Models;

namespace Take100.WPF.ViewModels;

public class WeightLogEntryViewModel : ViewModelBase
{
    private readonly WeightLogViewModel _weightLogViewModel;

    private WeightLogEntry _weightLogEntry;

    public WeightLogEntry WeightLogEntry
    {
        get => _weightLogEntry;
        set
        {
            _weightLogEntry = value;
            OnPropertyChanged(nameof(WeightLogEntry));
        }
    }

    public string Date => _weightLogEntry.Date.Date.ToShortDateString();
    public string Weight => _weightLogEntry.WeightInLbs.ToString("#.#") + " lbs";
    public ICommand DeleteWeightLogEntryCommand => _weightLogViewModel.DeleteWeightLogEntryCommand;

    public WeightLogEntryViewModel(WeightLogViewModel weightLogViewModel, WeightLogEntry weightLogEntry)
    {
        _weightLogViewModel = weightLogViewModel;
        _weightLogEntry = weightLogEntry;
    }


}
