using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take100.Domain.Models;

public class Food : DomainObject
{
    public string Name { get; set; } = default!;
    public string ServingSize { get; set; } = default!;
    public string? Notes { get; set; }
    public float Calories { get; set; }
    public float GramsOfProtein { get; set; }
    public float GramsOfCarbs { get; set; }
    public float GramsOfFat { get; set; }
}
