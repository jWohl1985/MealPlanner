using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take100.Domain.Models;

namespace Take100.Domain.Services;

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
