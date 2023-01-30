using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Take100.Domain.Models;
using Take100.Domain.Services;

namespace Take100.EntityFramework.Services;

public class DietDataService : IDietDataService
{
    private readonly DietContextFactory _contextFactory;

    public DietDataService(DietContextFactory contextFactory, 
        IRepository<DietUser> userRepository, 
        IRepository<Food> foodRepository, 
        IRepository<Meal> mealRepository,
        IRepository<WeightLogEntry> weightLogRepository)
    {
        _contextFactory = contextFactory;
        DietUserRepository = userRepository;
        FoodRepository = foodRepository;
        MealRepository = mealRepository;
        WeightLogRepository = weightLogRepository;
    }

    public IRepository<DietUser> DietUserRepository { get; private set; }
    public IRepository<Food> FoodRepository { get; private set; }
    public IRepository<Meal> MealRepository { get; private set; }
    public IRepository<WeightLogEntry> WeightLogRepository { get; private set; }

    public IEnumerable<DietUser> GetAllDietUsers()
    {
        using DietContext context = _contextFactory.CreateDbContext();
        return context.Users.ToList();
    }

    public IEnumerable<Meal> GetUserMealsByDay(int userId, DayOfWeek day)
    {
        using DietContext context = _contextFactory.CreateDbContext();
        return context.Meals
            .Include(m => m.Food)
            .Where(m => m.DietUserId == userId && m.DayNumber == (int)day)
            .ToList();
    }

    public IEnumerable<WeightLogEntry> GetUserWeightLogs(int userId)
    {
        using DietContext context = _contextFactory.CreateDbContext();
        return context.WeightLogs
            .Include(w => w.DietUser)
            .Where(w => w.DietUserId == userId)
            .OrderByDescending(w => w.Date)
            .ToList();
    }
}
