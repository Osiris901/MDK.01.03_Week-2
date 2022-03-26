using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingPractice_02.Models
{
    internal class Cell
    {
        public readonly int Id;

        public object? Value = null;
        public bool IsActive = false;

        public int ChildrenCount => Value is IEnumerable<Cell> cells ? cells.Count() : 0;

        public Cell(int id)
        {
            Id = id;
        }
    }
}
