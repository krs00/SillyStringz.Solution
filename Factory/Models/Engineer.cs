using System.Collections.Generic;
namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }
    public string Name { get; set; }
       public List<EngineerMachine> EngineerMachines { get; set; } // JOIN TABLE

  }
}