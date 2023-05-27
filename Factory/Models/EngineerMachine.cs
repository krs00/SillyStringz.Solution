using System.Collections.Generic;
namespace Factory.Models
{
  public class EngineerMachine
  {
    public int EngineerMachineId { get; set; } // Primary Key

    public int EngineerId { get; set; } // Foreign Key
    public Engineer Engineer { get; set; } // Reference Navigation Property

    public int MachineId { get; set; } // Foreign Key
    public Machine Machine { get; set; } // Reference Navigation Property
  }
}