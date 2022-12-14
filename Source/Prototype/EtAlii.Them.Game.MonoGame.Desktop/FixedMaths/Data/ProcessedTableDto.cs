using System.Collections.Generic;

namespace EtAlii.Them.Game.FixedMaths.Data
{
    public class ProcessedTableDto
    {
        public ProcessedTableDto(Operation operation)
        {
            Operation = operation;
            Data      = new Dictionary<int, int>();
        }

        public Operation            Operation { get; set; }
        public Dictionary<int, int> Data      { get; set; }
    }
}