using System.Collections.Generic;
namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    public string Name { get; set; }
    public List<Machine> Machines { get; set; } // Navigation Property
    public Machine Machine { get; set;  } // Reference Navigation property

  }
}