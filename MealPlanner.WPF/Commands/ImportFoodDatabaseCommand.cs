using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using MealPlanner.Domain.Models;
using MealPlanner.WPF.ViewModels;

namespace MealPlanner.WPF.Commands;

public class ImportFoodDatabaseCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;
    private FoodDatabaseViewModel FoodDatabaseViewModel => _mainViewModel.FoodDatabaseViewModel;

    public ImportFoodDatabaseCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public async override void Execute(object? parameter)
    {
        MessageBox.Show("You can use this to import a list of foods that you exported earlier, " +
            "or that you got from another user. This will not overwrite any existing data. Select the file that you want to import.");

        OpenFileDialog file = new OpenFileDialog();
        file.DefaultExt = ".csv";
        file.Filter = "(.csv)|*.csv";

        bool? result = file.ShowDialog();

        if (result == true)
        {
            await ImportFile(file.FileName);
        }
    }

    private async Task ImportFile(string fileName)
    {
        string[] lines = await File.ReadAllLinesAsync(fileName);
        List<Food> foods = new List<Food>();

        for (int i = 0; i < lines.Length; i++)
        {
            string[] properties = lines[i].Split(',');

            try
            {
                Food food = new()
                {
                    Name = properties[0],
                    ServingSize = properties[1],
                    Notes = properties[2],
                    Calories = float.Parse(properties[3]),
                    GramsOfProtein = float.Parse(properties[4]),
                    GramsOfCarbs = float.Parse(properties[5]),
                    GramsOfFat = float.Parse(properties[6]),
                };

                foods.Add(food);

                if (_mainViewModel.FoodDatabaseViewModel.Foods.Where(f => f.Food.Name == food.Name).Any())
                {
                    throw new Exception("Food already exists");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Food already exists")
                {
                    MessageBox.Show($"Duplicate food detected -- {properties[0]}. You must delete or rename this food before importing.");
                }
                else
                {
                    MessageBox.Show($"There was a problem parsing the file.\n " +
                        $"Line {i}: food name - {properties[0]}\n" +
                        $"Please ensure the file format is correct.");
                }

                return;
            }
        }

        foreach (Food food in foods)
        {
            await _mainViewModel.DietDataService.FoodRepository.CreateAsync(food);
            FoodDatabaseViewModel.Foods.Add(new FoodViewModel(FoodDatabaseViewModel, food));
        }
    }
}
