using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingPractice_02;

namespace TrainingPractice_02.Models
{
    internal class Grid
    {
        public IEnumerable<Cell> Cells { get; protected set; }
        public readonly int seed;

        public Grid(int cols = Constants.GridCols, int rows = Constants.GridRows, int maxNumber = Constants.MaxNumber, int? seed = null)
        {
            this.seed = seed ?? DateTime.Now.Millisecond * DateTime.Now.Second;
            Initialize(cols, rows, maxNumber);
        }

        public void Activate(int id)
        {
            Cells.ElementAt(id).IsActive = true;
        }

        public void ActivateInner(int id, int innerId)
        {
            var cell = Cells.ElementAt(id);
            if (cell.Value is IEnumerable<Cell>)
            {
                var innerCells = (IEnumerable<Cell>) cell.Value;
                innerCells.ElementAt(innerId).IsActive = true;
            }
        }

        /*
         * Grid generator algorithm
         * 1. Generate grid with {cols * rows} size
         * 2. Split cells by 2 and 4 'till grid is capable to hold {maxNumber}
         * 3. Mix cells
         */
        private void Initialize(int cols, int rows, int maxNumber)
        {
            var rnd = new Random(seed);
            var capacity = cols * rows;
            var cells = new List<Cell>(capacity);

            // (1)
            for (var i = 0; i < cols * rows; i++)
            {
                cells.Add(new Cell(i));
            }

            // (2)
            while (capacity < maxNumber)
            {
                var target = cells[rnd.Next(cells.Count)];
                if (target.Value is IEnumerable<Cell> inner)
                {
                    var count = inner.Count();
                    if (count >= 4)
                    {
                        continue;
                    }

                    var updated = inner.Concat(new Cell[] {new (count)});
                    target.Value = updated.ToList();
                    capacity++;
                }
                else
                {
                    target.Value = new Cell[] {new (0), new (1)} as IEnumerable<Cell>;
                    capacity++;
                }
            }

            if (capacity != maxNumber)
            {
                throw new Exception($"Ошибка при создании сетки (Доступно: {capacity}, нужно: {maxNumber}).");
            }

            var numbers = Enumerable.Range(1, 51).ToList();
            foreach (var cell in cells)
            {

                if (cell.Value is IEnumerable<Cell>)
                {
                    foreach (var inner in (IEnumerable<Cell>) cell.Value)
                    {
                        var id = rnd.Next(numbers.Count);
                        var n = numbers[id];
                        inner.Value = n;
                        numbers.RemoveAt(id);
                    }
                }
                else
                {
                    var id = rnd.Next(numbers.Count);
                    var n = numbers[id];
                    cell.Value = n;
                    numbers.RemoveAt(id);
                }
            }

            Cells = cells;
        }
    }
}
