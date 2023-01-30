using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take100.Domain.Models;

public class Meal : DomainObject
{
    public int DietUserId { get; set; }
    public int FoodId { get; set; }
    public int DayNumber { get; set; }
    public float ServingsToEat { get; set; }
    public bool HasBeenEaten { get; set; }

    public DayOfWeek DayOfWeek => (DayOfWeek)DayNumber;
    public float Calories => Food is not null ? Food.Calories * ServingsToEat : 0;
    public float GramsOfProtein => Food is not null ? Food.GramsOfProtein * ServingsToEat : 0;
    public float GramsOfCarbs => Food is not null ? Food.GramsOfCarbs * ServingsToEat : 0;
    public float GramsOfFat => Food is not null ? Food.GramsOfFat * ServingsToEat : 0;

    public Food? Food { get; set; }
    public DietUser? DietUser { get; set; }
}
