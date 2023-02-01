using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take100.Domain.Models;

public class DietUser : DomainObject
{
    public string Name { get; set; } = default!;
    public int Age { get; set; }
    public bool IsMale { get; set; }
    public float WeightInLbs { get; set; }
    public int HeightFeetComponent { get; set; }
    public int HeightInchComponent { get; set; }
    public int HeightInInches => (12 * HeightFeetComponent) + HeightInchComponent;
    public int HoursExercisePerWeek { get; set; }
    public int Goal { get; set; }
    public float NeckCircumferenceInches { get; set; }
    public float WaistCircumferenceInches { get; set; }
    public float HipCircumferenceInches { get; set; } // only needed for females
}
