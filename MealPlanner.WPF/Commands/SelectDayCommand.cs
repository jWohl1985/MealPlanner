using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take100.WPF.ViewModels;

namespace Take100.WPF.Commands
{
    public class SelectDayCommand : CommandBase
    {
        private readonly DayViewModel _dayViewModel;

        public SelectDayCommand(DayViewModel dayViewModel)
        {
            _dayViewModel = dayViewModel;
        }

        public override void Execute(object? parameter)
        {
            int dayNumber = int.Parse((string)parameter!);

            DayOfWeek selectedDay = (DayOfWeek)dayNumber;

            _dayViewModel.SelectedDay = selectedDay;
        }
    }
}
