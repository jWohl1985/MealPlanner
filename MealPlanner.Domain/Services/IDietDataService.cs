using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPlanner.Domain.Models;

namespace MealPlanner.Domain.Services;

public interface IDietDataService
{
    IRepository<DietUser> DietUserRepository { get; }
    IRepository<Food> FoodRepository { get; }
    IRepository<Meal> MealRepository { get; }
    IRepository<WeightLogEntry> WeightLogRepository { get; }
    IEnumerable<Meal> GetUserMealsByDay(int userId, DayOfWeek day);
    IEnumerable<DietUser> GetAllDietUsers();
    IEnumerable<WeightLogEntry> GetUserWeightLogs(int userid);
}
