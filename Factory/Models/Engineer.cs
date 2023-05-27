using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Factory.Models
{
    public class Engineer
    {
        public int EngineerId { get; set; }
        [Required]
        public string Name { get; set; }
        public List<EngineerMachine> EngineerMachines { get; set; } // JOIN TABLE

    }
}