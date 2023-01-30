using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Take100.Domain.Models;
using Take100.Domain.Services;

namespace Take100.WPF.Commands;

public class ExportFoodDatabaseCommand : CommandBase
{
    private readonly IDietDataService _dietDataService;

    public ExportFoodDatabaseCommand(IDietDataService dietDataService)
    {
        _dietDataService = dietDataService;
    }

    public async override void Execute(object? parameter)
    {
        MessageBox.Show("You can use this to backup your food list, or send it to other users so they can import it. Select where " +
            "you want to save the file.");

        SaveFileDialog file = new SaveFileDialog();
        file.FileName = "MyFoodDatabase";
        file.DefaultExt = ".csv";
        file.Filter = "(.csv)|*.csv";

        bool? result = file.ShowDialog();

        if (result == true)
        {
            string filename = file.FileName;
            await CreateExportFile(filename);
        }
        else
        {
            return;
        }
    }

    private async Task CreateExportFile(string filename)
    {
        IEnumerable<Food> foods = await _dietDataService.FoodRepository.GetAllAsync();

        List<string> lines = new List<string>();

        foreach (Food food in foods)
        {
            lines.Add($"{food.Name},{food.ServingSize},{food.Notes},{food.Calories},{food.GramsOfProtein},{food.GramsOfCarbs},{food.GramsOfFat}");
        }

        await File.WriteAllLinesAsync(filename, lines);

        MessageBox.Show("Done!", "Database Export");
    }

}
