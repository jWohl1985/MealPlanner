using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlanner.Domain.Models;

public class WeightLogEntry : DomainObject
{
    public int DietUserId { get; set; }
    public float WeightInLbs { get; set; }
    public DateTime Date { get; set; }

    public DietUser? DietUser { get; set; }
}
